function rpController($scope, $http, dataService, $filter) {

    $scope.isBusy = true;
    var orderBy = $filter('orderBy');

    $scope.order = function (predicate, reverse) {
        $scope.receivePorts = orderBy($scope.receivePorts, predicate, reverse);
    };

    $scope.receivePorts = [];

    if (dataService.IsRPReady() == false) {
        dataService.getRPs()
       .then(function (_rps) {
           //Success
           $scope.receivePorts = _rps;
           console.log($scope.receivePorts);
       }, function (status) { //Error
           handleError(status);
           console.log("Error Occured while fetching Receive Ports " + status);

       });
    }
    else {
        $scope.receivePorts = dataService.rps;
    }

}
