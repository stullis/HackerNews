var app = angular.module('HackerNews',['ngMaterial','ngRoute']);


app.config(
    function ($routeProvider)
    {
        $routeProvider
            .when('/HackerNewsBest', {
                Title: 'Best of Hacker News',
                templateUrl: 'app/HackerNewsBest/hackerNewsBestView.html',
                controller: 'hackerNewsBestController as bestNewsCtrl'
            }).otherwise({ redirectTo: "/HackerNewsBest" });
    }

);