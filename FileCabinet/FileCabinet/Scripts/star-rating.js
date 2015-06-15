$("#raiting > span").click(function () {
    var ratingElem = $(this).parent('#raiting');
    var spanIndex = $(this).index();
    //alert($(this).parent('#raiting').attr('postId'));
    $.ajax({
        type: "POST",
        url: '/Articles/SetRating',
        data: {
            postId: ratingElem.attr('postId'),
            rating: spanIndex
        },
        success: function (data) {
            ratingElem.children('div').text("|" + data.average + "|");
            ratingElem.children('span').each(function (index, val) {
                if (index >= -1 + spanIndex) {
                    $(this).text('\u2605');
                    $(this).css('color', 'gold');
                }
                else {
                    $(this).text('\u2606');
                    $(this).css('color', 'black');
                }
            });
        },
        failed: function () {
            alert('failed');
        }
    });
});