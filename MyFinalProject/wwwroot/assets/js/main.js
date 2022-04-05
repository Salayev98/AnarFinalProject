$(document).ready(function () {
    $(document).on("click", ".open-modal", function (e) {
        e.preventDefault()
        let url = $(this).attr("href")
        fetch(url).then(responso => responso.text()).then(data =>
            $("#quick_view .modal-dialog").html(data)
        )
        $("#quick_view").modal(true);
    })
    $(document).on("click", ".add-to-basket", function (e) {
        e.preventDefault()
        let url = $(this).attr("href")
        fetch(url).then(responso => responso.text()).then(data => 
            $(".basket-dropdown").html(data)
        )
    })
    $(document).on("click", ".minicart-remove", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(function (response) {
                return response.text();
            }).then(data => {
                $(".basket-dropdown").html(data);
            });
    })
    $(document).on("click", ".add-wish", function (e) {
        e.preventDefault()
        let url = $(this).attr("href")
        fetch(url).then(responso => responso.text()).then(data =>
            $(".wish-table").html(data)
        )
    })
    $(document).on("click", ".remove-wish", function (e) {
        e.preventDefault();
        console.log("salam")
        let url = $(this).attr("href");
        fetch(url)
            .then(function (response) {
                return response.text();
            }).then(data => {
                $(".wish-table").html(data);
            });
    })
   
})
