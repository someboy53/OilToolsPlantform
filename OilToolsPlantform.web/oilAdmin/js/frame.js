
function api(data, func, url) {
    $.ajax({
        type: "post",
        data: data,
        url: url,
        async: true,
        dataType: "json",
        success: function (res) {
            if (res.ErrorCode == "A_0") {
                func && func(res);
                canLoad = true;
            } else {
                console.log(res);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}