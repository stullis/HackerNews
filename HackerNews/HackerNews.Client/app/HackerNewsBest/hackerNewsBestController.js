app.controller('hackerNewsBestController', ['$scope', 'hackerNewsBestService', '$http', '$window', function ($scope, hackerNewsBestService, $http, $window) {
    $scope.bestStories;
    $scope.searchTerm;

    hackerNewsBestService.getBestStories().then(function (data) {
        $scope.bestStories = data;
    });

}]);