var v = new Vue({

    el: '#GameList',

    data: {
        games: []
    },

    mounted: function () {
        var self = this;

        //Get the player data and dropdown list data
        $.ajax({
            async: true,
            contentType: "application/json",
            dataType: "json",
            url: "/Games/GetGameCollection/" + $("#UserId").val(),
            method: "GET",
            success: function (response) {
                data = response.Object;
                self.games = data;

                //abp.notify.success('The user data has been loaded', 'Success');
            },
            error: function () {
                //abp.notify.error('Failed to load user data', 'Error');
            }
        });
    }
    
    //methods: {

    //}
});