$("input[name=CityName]").autocomplete({
    select: function (e, u) {
        $(this).val(u.item.Name);
    },
    source: function (request, response) {
        $.get("/VerifyPanel/GetCities", {
            term: request.term
        }, function (data) {
            if (data.success) {
                response($.map(data.data, function (item) {
                    return {
                        label: item.NameLong,
                        value: item.Name
                    }
                }));
            }
        });
    }
});
$("input[name=CountryName]").autocomplete({
    source: function (request, response) {
        $.get("/VerifyPanel/GetCountries", {
            term: request.term
        }, function (data) {
            if (data.success) {
                response(data.data.NameLong);
            }
        });
    },
});