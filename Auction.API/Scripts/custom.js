function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function (e) {
    if (!e.target.matches('.dropbtn')) {
        var myDropdown = document.getElementById("myDropdown");
        if (myDropdown.classList.contains('show')) {
            myDropdown.classList.remove('show');
        }
    }
}

var createLotPrice = document.querySelector(".createLotPrice");
var createLotStep = document.querySelector(".createLotStep")

if (createLotPrice != null) {
    createLotPrice.oninput = function () {
        if (createLotPrice.value <= 0) {
            createLotPrice.value = "";
        }
    }
}

if (createLotStep != null) {
    createLotStep.oninput = function () {
           if (createLotStep.value <= 0) {
                createLotStep.value = "";
           }
        }
    }

