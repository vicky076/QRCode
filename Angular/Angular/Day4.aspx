<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day4.aspx.cs" Inherits="Angular.Day4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-click</title>
    <script type="text/javascript">
        function TodoCrtl($scope) {
            $scope.price = 0;
            $scope.action = function () {
                $scope.price = 100;
            }
        }
    </script>
</head>
<body>
    <div ng-controller="TodoCrtl">
        目前 {{ price | number}} 元
        <br />
        請輸入價錢：
        <input type="text" ng-model="price">
        <br />
        <input type="button" value="選擇100元" ng-click="action()">
    </div>
</body>
</html>
