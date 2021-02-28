const body = document.querySelector("body");

window.headerControlFunctions = {
    addListenerToSearchButton: function() {
        const searchButton = document.querySelector("#searchButton");
        const searchModal = document.querySelector(".search-modal");
        const searchModalField = document.querySelector(".search-modal__field");
        searchButton.onclick = function() {
            searchModal.classList.add("search-modal--active");
            body.classList.add("body--active");
            setTimeout(function() {
                searchModalField.classList.add("search-modal__field--fadeInDown");
            }, 300);
        };
    },
    addListenerToCloseButton: function() {
        const closeButton = document.querySelector("#closeButton");
        const searchModalField = document.querySelector(".search-modal__field");
        const searchModal = document.querySelector(".search-modal");
        closeButton.onclick = function() {
            searchModalField.classList.remove("search-modal__field--fadeInDown");
            body.classList.remove("body--active");
            setTimeout(function() {
                searchModal.classList.remove("search-modal--active");
            }, 300);
        };
    },
    addListenerToMenuButton: function() {
        const heroNav = document.querySelector(".hero__navigation");
        const menuOpen = document.querySelector(".hero__menu");
        menuOpen.onclick = function () {
            body.classList.toggle("body--active");
            heroNav.classList.toggle("hero__navigation--active");
            menuOpen.classList.toggle("hero__menu--active");
        };
    }
};