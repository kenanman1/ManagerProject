function tooglePassword() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
let clear = $("#clearAllButton");
if (clear) {
    console.log("kek");
    clear.on("click", function () {

        $('input[name="searchText"]').val('')

        $('select[name="searchFilter"]').val('0');
    });
}

let searchForm = $("#search-form");
if (searchForm) {

    searchForm.on("submit", function (event) {

        var searchText = $("#search-text").val();

        var searchFilter = $("#search-filter").val();

        if (searchText.trim() === "") {
            event.preventDefault();
            alert("Please enter a search term.");
        }
        else if (searchFilter === "0") {
            event.preventDefault();
            alert("Please select a filter.");
        }
    });
}

$('.capitalize').on('input', function () {
    let inputValue = $(this).val();

    if (inputValue.length > 0) {
        inputValue = inputValue.charAt(0).toUpperCase() + inputValue.slice(1);
        $(this).val(inputValue);
    }
});


let button = $(".group-edit-delete");
let classToRemove = "btn-group";
let classToAdd = "btn-group-vertical";
let mediaQuery = window.matchMedia("(max-width: 1200px)");

function handleMediaQueryChange(mediaQuery) {
    if (mediaQuery.matches) {
        button.removeClass(classToRemove);
        button.addClass(classToAdd);
    } else {
        button.removeClass(classToAdd).addClass(classToRemove);
    }
}

mediaQuery.addListener(handleMediaQueryChange);
handleMediaQueryChange(mediaQuery);


const addReg = $('<li class="nav-item registration-link"> <a href="/Account/Registration" class="nav-link registration-link">Registration</a></li>');
let reg = $(".registration-link");
let mediaQuery2 = window.matchMedia("(max-width: 992px)");
function handleMediaQueryChange2(mediaQuery2) {
    if (mediaQuery2.matches) {
        if ($(".logout").length === 0) {
            $(".add").append(addReg);
            reg.remove();
        }
    } else {
        $(".add2").prepend(reg);
        addReg.remove();
    }
}

mediaQuery2.addListener(handleMediaQueryChange2);
handleMediaQueryChange2(mediaQuery2);

