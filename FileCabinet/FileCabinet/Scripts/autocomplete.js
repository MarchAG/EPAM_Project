function Autocomplete(data) {
    var availableTags = data.split(' ');

    $("#tags")
        .bind("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB &&
                    $(this).autocomplete("instance").menu.active) {
                event.preventDefault();
            }
        })
        .autocomplete({
            minLength: 0,
            source: function (request, response) {
                response($.ui.autocomplete.filter(
                    availableTags, request.term.split(' ').pop()));
            },
            focus: function () {
                return false;
            },
            select: function (event, ui) {
                var terms = this.value.split(' ');
                terms.pop();
                terms.push(ui.item.value);
                this.value = terms.join(" ");
                return false;
            }
        });
};