const searchButton = document.querySelector("#searchButton")
const closeButton = document.querySelector("#closeButton")
const searchModal = document.querySelector(".search-modal")
const searchModalField = document.querySelector(".search-modal__field")

searchButton.onclick = function() {
    searchModal.classList.add("search-modal--active")
    setTimeout(function(){
        searchModalField.classList.add("search-modal__field--fadeInDown")
    }, 300)
}

closeButton.onclick = function() {
    searchModalField.classList.remove("search-modal__field--fadeInDown")
    setTimeout(function(){
        searchModal.classList.remove("search-modal--active")
    }, 300)
}