/// <reference path="../data/Gigs.js" />
/// <reference path="directives.js" />
/// <reference path="../_partials/hmGigItem.html" />
var myDirectives = angular.module("myDirectives", []);

myDirectives.directive('hmGigItem', function() {
    return {
        restrict: "E",
        replace: true,
        scope: {
            gig: '='
        },
        templateUrl: "/ng-App/_partials/hmGigItem.html",
        controller: function($scope) {
            //Variables
            $scope.displayProps = [];
            $scope.displayProps.favorite = false;

            //http://jsfiddle.net/EQmSN/ for more zooming
            $(".zoomfancybox").fancybox({
                arrows: false,
                openEffect: 'elastic',
                openSpeed: 150,
                closeEffect: 'elastic',
                closeSpeed: 150,
                closeClick: true
            });

            //Methods
           
        }
    };
});


myDirectives.directive('hmCategorySearch', function () {
    return {
        restrict: "E",
        replace: true,
        scope: {
            categories: '=',
            selected: '&'
        },
        templateUrl: "/ng-App/_partials/hmCategorySearch.html",
        controller: function ($scope) {
            //Variables

            //Methods

        }
    };
});

//http://blog.revolunet.com/blog/2013/11/28/create-resusable-angularjs-input-component/
myDirectives.directive('cnginput', function($compile) {
    return {
        restrict: "AE",
        replace: true,
        templateUrl: "/ng-App/_partials/cngInput.html",
        scope:
        {},
        require: 'ngModel',
        compile: function(element, attrs) {
            element.attr('ng-click', 'fxn()');
            var fn = $compile(element);
            return function(scope) {
                fn(scope);
            };
        },
        link: function(scope, iElement, iAttrs, ngModelController) {
            ngModelController.$render = function () {
                iElement.find('div').text(ngModelController.$viewValue);
            };
        }
};
});


myDirectives.directive('chosen1', function () {
    var linker = function (scope, element, attr) {
        scope.$watch('people', function() {
            element.trigger('liszt:updated');
        });
        element.chosen();
    };


    var compiler = function (element, attr) {
        element.chosen();
    };

    return {
        restrict: "A",
        link: linker,
        compile:compiler
    };
});