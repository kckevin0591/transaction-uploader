var UploadAjax = (function () {
    return {
        OnBegin: function () {
            console.log('uploading file');
        },
        OnComplete: function (data) {
            console.log('upload done', data);
            if (data.status === 200) {
                $('#displayError').addClass('hidden');
                $('#displaySuccess').removeClass('hidden');

            } else {
                const content = data.responseJSON;
                if (content.errors !== undefined) {
                    let msg = content.errors[""][0];
                    $('#displayError').find('span').text(msg);
                } else {
                    $('#displayError').find('span').text(content.message);
                
                    $('#ulErrors').empty();
                    if (content.apiErrors !== undefined) {
                        content.apiErrors.forEach(function(item) {
                            $('#ulErrors').append('<li>' +item+ '</li>');
                        });
                    }
                }
                
                $('#displaySuccess').addClass('hidden');
                $('#displayError').removeClass('hidden');
            }
        }
    };
})();