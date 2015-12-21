<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day18.aspx.cs" Inherits="Angular.Day18" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-bind</title>
    <script type="text/javascript">
        function Ctrl($scope) {
            $scope.name = "Anns";
        }
    </script>
</head>
<body>
    <div>
        ng-dblclick：
        <button ng-dblclick="count = count+1" ng-init="count=0">
            Increment (on double click)
        </button>
        {{count}}
    </div>
    <br />
    <div>
        <label>
            ng-disabled： Click me to toggle:<input type="checkbox" ng-model="checked" /></label>
        <button ng-model="button" ng-disabled="checked">
            button</button></div>
    <br />
    <div>
        ng-click：
        <button ng-click="count2=count2+1" ng-init="cnt=0">
            click</button>
        {{count2}}
    </div>
    <br />
    <div ng-controller="Ctrl">
        ng-bind：<br />
        <label>
           &nbsp; &nbsp; Enter name:
            <input type="text" ng-model="name"></label><br>
      &nbsp; &nbsp;   Hello, <span ng-bind="name"></span>
    </div>
</body>
</html>
