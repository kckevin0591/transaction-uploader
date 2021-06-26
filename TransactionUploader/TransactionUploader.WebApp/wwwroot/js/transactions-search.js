var SearchAjax = (function () {
    return {
        OnBegin: function () {
            console.log('begin search');
        },
        OnComplete: function (data) {
            console.log('search done', data);
        }
    };
})();