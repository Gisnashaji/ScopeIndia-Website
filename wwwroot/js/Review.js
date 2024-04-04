document.addEventListener("DOMContentLoaded", function () {
    const stars = document.querySelectorAll(".star-rating i");
    const ratingInput = document.getElementById("selected-rating");

    stars.forEach((star) => {
        star.addEventListener("click", function () {
            const selectedRating = star.dataset.index;
            ratingInput.value = selectedRating;

            
            stars.forEach((s) => {
                s.classList.remove("highlight");
            });

           
            for (let i = 0; i < selectedRating; i++) {
                stars[i].classList.add("highlight");
            }
        });
    });
});
