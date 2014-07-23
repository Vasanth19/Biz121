/// <reference path="../data/Gigs.js" />
//1. Make controllers a object instrad of function for minimizing to work. Refer Shawn wildermuth Course 8.11 minification.

var homeIndexModule = angular.module("homeIndex", ["ngRoute", "ngSanitize", "myDataService", "myDirectives", "myFilters", "field-directive", "localytics.directives"]);


homeIndexModule.config(function($routeProvider) {
    $routeProvider.when("/", { controller: "rpController", templateUrl: "ng-App/subPages/receivePorts/receivePorts.html" });
    $routeProvider.when("/RP", { controller: "rpController", templateUrl: "ng-App/subPages/receivePorts/receivePorts.html" });
    $routeProvider.when("/cbrf", { controller: "cbrController", templateUrl: "ng-App/subPages/cbr/cbr_full.html" });
    $routeProvider.when("/cbr", { controller: "cbrController", templateUrl: "ng-App/subPages/cbr/cbr.html" });
    $routeProvider.when("/sendports", { controller: "spController", templateUrl: "ng-App/subPages/sendPorts/sendPorts.html" });
    $routeProvider.when("/logs", { controller: "logController", templateUrl: "ng-App/subPages/viewLogs/viewLogs.html" });

    $routeProvider.when("/applications", { controller: "dashboardController", templateUrl: "ng-App/subPages/applications.html" });
    $routeProvider.when("/dashboard", { controller: "dashboardController", templateUrl: "ng-App/subPages/dashboard.html" });
    $routeProvider.when("/application/:appName", { controller: "singleAppController", templateUrl: "ng-App/subPages/singleAppView.html" });



    $routeProvider.otherwise({ redirectTo: "/" });
});


// private 
function handleError(status)
{
    if (status == 401)
        window.location = "error/401";
    else if (status == 404)
        window.location = "error/404";
    else
        window.location = "error";
}


function dashboardController($scope, $http, dataService) {
    
}


function singleAppController($scope, dataService, $routeParams, $location) {

    $scope.topic = null;
    $scope.newReply = {};

    console.log($routeParams.id);

    dataService.getTopicById($routeParams.id)
        .then(function(topic) {
            //Success
            $scope.topic = topic;
        }, function() { //Error
            $location.path("#/");
        });

    $scope.addReply = function() {

        dataService.addReply($scope.topic, $scope.newReply).then(function() {
            //Success
            $scope.newReply.body = "";
        }, function() { //error

        });


    };

}

function appController($scope, $http, dataService) {
    //http://jsoneditoronline.org/

    
    $scope.filterSubcategory = "";
    $scope.isBusy = true;
    $scope.i = 0;
    $scope.data = "I am Awesome";
    $scope.someHtml = '<img src="http://angularjs.org/img/AngularJS-large.png" />';
    $scope.gigs = [];

    $scope.images = [1, 2, 3, 4, 5, 6, 7, 8];

    $scope.loadMore = function() {
        var last1 = $scope.images[$scope.images.length - 1];
        for (var i = 1; i <= 8; i++) {
            $scope.images.push(last1 + i);
        }
    };

 

    dataService.getCategories()
        .then(function (categories) {
            //Success
            $scope.categories = categories;
        }, function () { //Error
        console.log("Error Occured while fetching categories");
        });

    dataService.getGigs()
    .then(function (_gigs) {
        //Success
        $scope.gigs = _gigs;
        console.log(_gigs);
    }, function () { //Error
        console.log("Error Occured while fetching Gigs");

    });

 

    $scope.saveGig = function () {
        var newGig = {
            "title": "take a professional quality photo of a phrase or name spelt in...",
            "title_full": "take a professional quality photo of a phrase or name spelt in Scrabble tiles",
            "duration": 5,
            "price": "$5",
            "rating": 9,
            "rating_count": 255,
            "is_featured": true,
            "gig_id": 327941,
            "gig_url": "/ceppii/take-a-professional-quality-photo-of-a-phrase-or-name-spelt-in-scrabble-tiles",
            "img_medium": "<img src=\"http://cdn1.fiverrcdn.com/photos/327941/v2_162/small3.jpg?1368531638\"  alt=\"take a professional quality photo of a phrase or name spelt in Scrabble tiles\"   >",
            "video_thumb": false,
            "seller_name": "ceppii",
            "seller_img": "<img src=\"http://cdn1.fiverrcdn.com/photos/104001/thumb/springbreak2.jpg?1339889643\"    width=\"32\" height=\"32\">",
            "seller_created_at": "over 3 years",
            "seller_country_name": "United States",
            "seller_country": "us",
            "seller_url": "/ceppii",
            "seller_level": "level_two_seller",
            "gig_locale": "en",
            "seller_id": 103826
        };

        dataService.addGig(newGig).then(function (newlyCreatedGig) {
            //Success
            console.log("gig Saved");
            console.log(newlyCreatedGig);
        }, function () {
            //Error
            console.log("Error Occured while saving gig");
        });


    };

    $scope.GetResultsByCategory = function (subcategory) {
        console.log("Selected " + subcategory);
        //Get new results based on subcategory
    };

}
