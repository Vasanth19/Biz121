var dataServiceModule = angular.module("myDataService", []);

dataServiceModule.factory("dataService", function ($http, $q) {
 
    var _topics = [];
    var _rps = [];
    var _sps = [];
    var _isInit = false;

    var _isReady = function() {
        return _isInit;
    };

    var _getRPs = function () {

        var deferred = $q.defer();

        $http.get("api/v1/testrp")
            .then(function (result) {
                //Success
                angular.copy(result.data, _rps);
                _isInit = true;
                deferred.resolve(_rps);
            },
                function () {
                    deferred.reject();
                }
            );
        return deferred.promise;
    };

    var _getSPs = function () {

        var deferred = $q.defer();

        $http.get("api/v1/sp")
            .then(function (result) {
                //Success
                angular.copy(result.data, _sps);
                _isInit = true;
                deferred.resolve(_sps);
            },
                function () {
                    deferred.reject();
                }
            );
        return deferred.promise;
    };

  
    
    return {
        rps: _rps,
        getRPs: _getRPs,
        sps: _sps,
        getSPs: _getSPs,
        isReady:_isReady
    };

});
