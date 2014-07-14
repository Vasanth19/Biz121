function cbrController($scope, $http, $location, dataService) {
    $scope.errorMessage = '';
    
    //#region Pull Apps
    $scope.applications = [];

    if (dataService.IsInitDataReady() == false) {
        dataService.getApps()
       .then(function (_apps) {
           //Success
           $scope.applications = _apps;
       }, function () { //Error
           handleError(status);
           console.log("Error Occured while fetching applications " + status);
       });
    }
    else {
        $scope.applications = dataService.apps;
    }

    //#endregion 

    //#region RP Variables and methods
    $scope.newReceivePort = {
        "interfaceName": "ESSBase_File_From_SAP",
        "description": "",
        "applicationName": "",
        "name": "RP_ESSBase_File_From_SAP"
    };
    $scope.newReceiveLocation = {
        "name": "",
        "transportType": "",
        "address": "C:\\BizTalkFolders\\FileTransfer\\In\\*.1t"

    };
    //Post RP
    $scope.saveRP = function () {

        //Associate RL with RP
        $scope.newReceivePort.rLs = [];
        $scope.newReceivePort.rLs.push($scope.newReceiveLocation);

        dataService.addRP($scope.newReceivePort).then(function () {
            //Success
            $scope.successMessage = true;
            $scope.errorMessage = null;
            
        }, function (result,status) {
            //Error
            $scope.errorMessage = result.data.message;
        });
    };

    //#endregion

    //#region SP variables and methods

    $scope.newSendPort = {
        "name": "SP_ESSBase_File_To_SFTP",
        "description": null,
        "applicationName": "BizTalk Application 1",
        "retryCount": 15,
        "retryInterval": 2,
        "transportType": "FILE",
        "address": "",
        "pipelineName": "Microsoft.BizTalk.DefaultPipelines.PassThruTransmit"
    };

   
    //Post RP
    $scope.saveSP = function () {
        $scope.newSendPort.subscribeToRP = $scope.newReceivePort.name;
        dataService.addSP($scope.newSendPort).then(function () {
            //Success
            $scope.successMessage = true;
            $scope.errorMessage = null;

        }, function (result, status) {
            //Error
            $scope.errorMessage = result.data.message;

        });
    };

    //#endregion

    //#region $watches

    $scope.$watch('newReceivePort.interfaceName', function () {
        $scope.newReceivePort.name = "RP_" + $scope.newReceivePort.interfaceName;
        $scope.newReceiveLocation.name = "RL_" + $scope.newReceivePort.interfaceName;
    }, true);

    $scope.$watch('newReceivePort.source', function () {
        if ($scope.newReceivePort.source == "SAP") {
            $scope.newReceiveLocation.transportType = "FILE";
            $scope.newReceiveLocation.address = "C:\\BizTalkFolders\\FileTransfer\\";
        }
        else if ($scope.newReceivePort.source == "SFTP") {
            $scope.newReceiveLocation.transportType = "SFTP";
            $scope.newReceiveLocation.address = "sftp://devsftp:22/folder/Xyx/";
        }
    }, true);

    $scope.$watch('newSendPort.target', function () {
        if ($scope.newSendPort.target == "SAP") {
            $scope.newSendPort.transportType = "FILE";
            $scope.newSendPort.address = "C:\\BizTalkFolders\\FileTransfer\\Out\\%SourceFileName%";

        }
        else if ($scope.newSendPort.target == "SFTP") {
            $scope.newSendPort.transportType = "SFTP";
            $scope.newSendPort.address = "sftp://devsftp:22/folder/Xyx/";
        }
    }, true);

    //#endregion

    //#region FMR
    $scope.fmr = {
        "rpName": $scope.newReceivePort.name,
        "spName": $scope.newSendPort.name,
        "emailInfo":
                    {
                        "to": "vasanths@iadb.org",
                        "from": "BizTalkdev@iadb.org",
                        "host": "smtpapp@iadb.org",
                        "subject": "Hello There",
                        "body": "File failed for the interface with Receive Port " + $scope.newReceivePort.name +
                            " and Send Port " + $scope.newSendPort.name + " and File Name : %SourceFileName%"
                    }
    }

    //Post RP
    $scope.saveFMR = function () {
        
        dataService.addFMR($scope.fmr).then(function () {
            //Success
            $scope.successMessage = true;
            $scope.errorMessage = null;

        }, function (result, status) {
            //Error
            $scope.errorMessage = result.data.message;

        });
    };

    //#endregion

    $scope.resetOnNext = function () {
        $scope.successMessage = false;
        $scope.errorMessage = null;
       // $scope.newReceivePort = null;
    }

    $scope.isUnchanged = function (rp) {
        return angular.equals(rp, $scope.master);
    };

    //#region to Delete

    $scope.address = {};
    $scope.refreshAddresses = function (address) {
        var params = { address: address, sensor: false };
        return $http.get(
          'http://maps.googleapis.com/maps/api/geocode/json',
          { params: params }
        ).then(function (response) {
            $scope.addresses = response.data.results;
        });
    };



   
    $scope.url = '/ng-App/data/people.json';
    $scope.person = {};


    $scope.people = [
      { name: 'Adam', email: 'adam@email.com', age: 10 },
      { name: 'Amalie', email: 'amalie@email.com', age: 12 },
      { name: 'Wladimir', email: 'wladimir@email.com', age: 30 },
      { name: 'Samantha', email: 'samantha@email.com', age: 31 },
      { name: 'Estefanía', email: 'estefanía@email.com', age: 16 },
      { name: 'Natasha', email: 'natasha@email.com', age: 54 },
      { name: 'Nicole', email: 'nicole@email.com', age: 43 },
      { name: 'Adrian', email: 'adrian@email.com', age: 21 }
    ];

    $scope.fetchPeople = function () {
        $http.get($scope.url).then(function (result) {
            $scope.people = result.data;
        }
        ,
         function (result) {
             console.log("people fetch failed");
         });
    };
    //$scope.fetchPeople();
    //#endregion

}