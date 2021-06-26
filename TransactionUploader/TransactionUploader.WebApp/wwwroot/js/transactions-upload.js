var UploadAjax = (function () {
    return {
        OnBegin: function () {
            console.log('uploading file');
        },
        OnComplete: function (data) {
            console.log('upload done', data);
            if (data.status === 200) {
                $('#displayError').addClass('invisible');
                $('#displaySuccess').removeClass('invisible');

            } else {
                $('#displaySuccess').addClass('invisible');
                $('#displayError').removeClass('invisible');

            }
        }
    };
})();