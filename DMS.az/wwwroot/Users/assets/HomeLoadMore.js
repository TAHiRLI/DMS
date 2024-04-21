$(function () {
    var skipRow = 1;
    $('#loadMore').on('click', function () {

        var dataTotalServices = $(this).data('count');


        $.ajax({
            url: "/home/loadmore",
            type: "GET",
            data: {
                skipRow: skipRow,
            },
            contentType: "application/json",
            success: function (response) {
                $('#services-container').append(response);

                console.log(dataTotalServices);

                if (dataTotalServices < (skipRow+1)*3) {
                    $('#loadMore').hide();

                }

                skipRow++;
            }
        });
    });
});


