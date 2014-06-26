var dataServiceModule = angular.module("myDataService", []);

dataServiceModule.factory("dataService", function ($http, $q) {
 
    var _topics = [];
    var _rps = [];
    var _sps = [];
    var _rpInit = false;
    var _spInit = false;

    var _isRPReady = function ()
    {
        return _rpInit;
    }

    var _isSPReady = function () {
        return _spInit;
    }


    var _getRPs = function () {

        var deferred = $q.defer();

        $http.get("api/v1/rp")
            .then(function (result) {
                //Success
                angular.copy(result.data, _rps);
                _rpInit = true;
                deferred.resolve(_rps);
            },
                function (result) {
                    deferred.reject(result.status);
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
                _spInit = true;
                deferred.resolve(_sps);
            },
                function (result) {
                    deferred.reject(result.status);
                }
            );
        return deferred.promise;
    };

  
    
    return {
        rps: _rps,
        getRPs: _getRPs,
        sps: _sps,
        getSPs: _getSPs,
        IsRPReady: _isRPReady,
        IsSPReady: _isSPReady
    };

});
