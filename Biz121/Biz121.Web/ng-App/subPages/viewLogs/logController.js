function logController($scope, $http, dataService, $filter) {

    $scope.isBusy = true;
    var orderBy = $filter('orderBy');

    $scope.order = function (predicate, reverse) {
        $scope.receivePorts = orderBy($scope.receivePorts, predicate, reverse);
    };

 
    // Temp Code
    $scope.logs = [];
        dataService.getLogs()
       .then(function (_logs) {
           //Success
           $scope.logs = _logs;
           console.log($scope.logs);
       }, function (status) { //Error
           handleError(status);
           console.log("Error Occured while fetching Logs " + status);

       });
   
}
