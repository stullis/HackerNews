app.factory('hackerNewsBestService',['$http', function ($http) {
    var baseURL = 'http://hackernewswebapi.azurewebsites.net/api/';

    var hackerNewsBestService = {

        getBestStories: function () {
            return $http.get(baseURL +"story/BestStories").then(function (response) {
                return response.data;
            });
        }
    }
    return hackerNewsBestService;
}]);