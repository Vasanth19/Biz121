function spController($scope, $http, dataService, $filter) {

    $scope.isBusy = true;
    var orderBy = $filter('orderBy');

    $scope.order = function (predicate, reverse) {
        $scope.sendPorts = orderBy($scope.sendPorts, predicate, reverse);
    };

    $scope.sendPorts = [];

    if (dataService.IsSPReady() == false) {
        dataService.getSPs()
       .then(function (_sps) {
           //Success
           $scope.sendPorts = _sps;
       }, function () { //Error
           handleError(status);
           console.log("Error Occured while fetching Send Ports " + status);
       });
    }
    else {
        $scope.sendPorts = dataService.sps;
    }
}