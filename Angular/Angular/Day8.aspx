<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day8.aspx.cs" Inherits="Angular.Day8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>input</title>
    <script type="text/javascript">
        function Ctrl($scope) {
            $scope.value1 = true;
            $scope.value2 = 'YES';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" ng-controller="Ctrl">
    <div>
        Value1:<input type="checkbox" ng-model="value1" /><br />
        Value2:<input type="checkbox" ng-model="value2" ng-true-value="YES" ng-false-value="NO" /><br />
        <tt>value1= {{ value1 }}</tt><br />
        <tt>value2= {{ value2 }}</tt> 
    </div>
    </form>
</body>
</html>
