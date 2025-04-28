document.addEventListener("DOMContentLoaded", () => {
    const headImg = document.getElementById("characterHead");
    const navList = document.querySelector(".list-nav");

    headImg.addEventListener("click", () => {
        const currentState = headImg.dataset.state;
        const nextState = currentState === "before" ? "after" : "before";
        const BASE_PATH = window.BASE_PATH || '';
        const nextSrc = `${BASE_PATH}/img/character/head_${nextState}.png`;

        animateImageSwap(headImg, nextSrc, nextState);

        if (navList.classList.contains("hidde")) {
            navList.classList.remove("hidde");
            navList.classList.add("show");
        } else {
            navList.classList.remove("show");
            navList.classList.add("hidde");
        }
    });
});

function animateImageSwap(imgElement, newSrc, newState) {
    setTimeout(() => {
        imgElement.src = newSrc;
        imgElement.dataset.state = newState;
    }, 10);
}
