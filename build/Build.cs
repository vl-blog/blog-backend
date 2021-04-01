using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.TeamCity;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[TeamCity(
    TeamCityAgentPlatform.Unix,
    Version = "2020.2",
    VcsTriggeredTargets = new[] {nameof(Test), nameof(Up)},
    NonEntryTargets = new[] {nameof(Restore)},
    ExcludedTargets = new[] {nameof(Clean)})]
[GitHubActions(
    "build",
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] {"master"},
    InvokedTargets = new[] {nameof(Compile)})]
[GitHubActions(
    "test",
    GitHubActionsImage.WindowsLatest,
    GitHubActionsImage.UbuntuLatest,
    GitHubActionsImage.MacOsLatest,
    OnPushBranches = new[] {"master"},
    InvokedTargets = new[] {nameof(Test)})]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Up);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Parameter("Check to wipe the database data on shutdown")]
    readonly bool WipeDatabaseData;

    [Parameter("The name of docker-compose project")]
    readonly string ProjectName = "blog";

    [Solution] readonly Solution Solution;
    
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    
    [PathExecutable("docker-compose")] readonly Tool DockerCompose;
    
    Target Down => _ => _
        .Executes(() =>
        {
            string command = $"-p {ProjectName} down";
            if (WipeDatabaseData)
                command += " --volumes";
            DockerCompose(command, SourceDirectory);
        });

    Target Up => _ => _
        .DependsOn(Down)
        .Executes(() =>
        {
            DockerCompose($"-p {ProjectName} up --build -d --remove-orphans", SourceDirectory);
        });

    Target Clean => _ => _
        .Executes(() =>
        {
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    [Partition(2)] readonly Partition TestPartition;
    IEnumerable<Project> TestProjects => TestPartition.GetCurrent(Solution.GetProjects("*Tests"));
    AbsolutePath TestResultDirectory => OutputDirectory / "test-results";
    
    Target Test => _ => _
        .Partition(() => TestPartition)
        .DependsOn(Compile)
        .Produces(TestResultDirectory / "*.trx")
        .Executes(() =>
        {
            DotNetTest(_ => _
                    .SetConfiguration(Configuration)
                    .SetVerbosity(DotNetVerbosity.Minimal)
                    .SetNoBuild(InvokedTargets.Contains(Compile))
                    .SetResultsDirectory(TestResultDirectory)
                    .CombineWith(TestProjects, (_, v) => _
                        .SetProjectFile(v)
                        .SetLogger($"trx;LogFileName={v.Name}.trx")),
                completeOnFailure: false);
        });
}
