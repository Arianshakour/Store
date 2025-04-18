function fillPageId(page) {
    $("#Page").val(page);
    $("#filter-search").submit();
}

function changeTakeEntity() {
    debugger;
    var selectedValue = $("#takeEntityy").val();
    $("#TakeEntity").val(selectedValue);
    $("#filter-search").submit();
    $("#takeEntityy").val(selectedValue);
}
