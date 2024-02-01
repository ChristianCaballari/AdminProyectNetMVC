function initPaginationController(controller,id) {
    $("#quantityRecordsByPage").change(function () {
        const recordsByPage = $(this).val();
        if (id) {
            location.href = `/${controller}/${id}/?page=1&recordsByPage=${recordsByPage}`;
        } else {
            location.href = `/${controller}/?page=1&recordsByPage=${recordsByPage}`;

        }
    });
}
