var v = new Vue({

    el: '#app',

    data: {
        games: [],
        show: false,
        tabs: ['My Games', 'Favorite Games', 'Friend Games'],
        active: null
    },

    mounted: function () {
        var self = this;
        var userId = 3;

        //Get the player data and dropdown list data
        $.ajax({
            async: true,
            contentType: "application/json",
            dataType: "json",
            url: "/Games/GetGamesLists?userId=" + userId,
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
    },
    
    methods: {
        next () {
            this.active = this.tabs[(this.tabs.indexOf(this.active) + 1) % this.tabs.length]
        }
    }
});