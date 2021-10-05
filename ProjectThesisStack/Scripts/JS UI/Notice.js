const seeMoreFunction = (id) => {
    console.log(id);
    var dots = document.getElementById("seeMoreDots" + id);
    var moreText = document.getElementById("noticeMore" + id);
    var btnText = document.getElementById("seeMore" + id);
    var openBtn = document.getElementById("openBtn" + id);
    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "See more>>";
        moreText.style.display = "none";
        openBtn.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "See less<<";
        moreText.style.display = "block";
        openBtn.style.display = "block";
    }
}

