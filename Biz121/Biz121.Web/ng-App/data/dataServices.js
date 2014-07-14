var dataServiceModule = angular.module("myDataService", []);

dataServiceModule.factory("dataService", function ($http, $q) {
 
    //Variables to hold collections
    var _topics = [];
    var _rps = [];
    var _sps = [];
    var _apps = [];

    // Init fuctions to call the backend or local service
    var _rpInit = false;
    var _spInit = false;
    var _InitData = false;

    var _isRPReady = function ()
    {
        return _rpInit;
    }

    var _isSPReady = function () {
        return _spInit;
    }

    var _isInitDataReady = function () {
        return _InitData;
    }

    //get fuctions

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

    var _getApps = function () {

        var deferred = $q.defer();

        $http.get("api/v1/util/apps")
            .then(function (result) {
                //Success
                angular.copy(result.data, _apps);
                _InitData = true;
                deferred.resolve(_apps);
            },
                function (result) {
                    deferred.reject(result.status);
                }
            );
        return deferred.promise;
    };

    //Post functions

    var _addRP = function (newRP) {

        var deferred = $q.defer();

        $http.post("api/v1/rp", newRP)
            .then(function (result) {
                _rps.splice(0, 0, newRP);
                deferred.resolve();

            }, function (result,status) {
                deferred.reject(result,status);
            });

        return deferred.promise;
    };

    var _addSP = function (newSP) {

        var deferred = $q.defer();

        $http.post("api/v1/sp", newSP)
            .then(function (result) {
                _sps.splice(0, 0, newSP);
                deferred.resolve();

            }, function (result, status) {
                deferred.reject(result, status);
            });

        return deferred.promise;
    };

    var _addFMR = function (newFMR) {

        var deferred = $q.defer();

        $http.post("api/v1/util/fmr", newFMR)
            .then(function (result) {
                _sps.splice(0, 0, newFMR);
                deferred.resolve();

            }, function (result, status) {
                deferred.reject(result, status);
            });

        return deferred.promise;
    };

    
    return {
        //rps
        IsRPReady: _isRPReady,
        rps: _rps,
        getRPs: _getRPs,

        //sps
        IsSPReady: _isSPReady,
        sps: _sps,
        getSPs: _getSPs,
       
        //init data
        IsInitDataReady: _isInitDataReady,
        apps: _apps,
        getApps: _getApps,
        
        //Post
        addRP: _addRP,
        addSP: _addSP,
        addFMR:_addFMR
    };

});
