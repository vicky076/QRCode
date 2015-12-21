<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day17.aspx.cs" Inherits="Angular.Day17" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>timeout</title>
    <script type="text/javascript">
        function Ctrl($scope) {
            $scope.countDown = 10;
            var timer = setInterval(function () {
                $scope.countDown--;
                $scope.$apply();
                console.log($scope.countDown);
            }, 1000);

            $scope.stop = function () {
                window.clearInterval(timer);
            };
        }
    </script>
</head>
<body ng-controller="Ctrl">
    {{countDown}}
    <button ng-click="stop()">
        Stop</button>
</body>
</html>
