<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day3.aspx.cs" Inherits="Angular.Day3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-controller</title>
    <script type="text/javascript">
        function TodoCrtl($scope) {
            $scope.name = "Ann";
        }    
    </script>
</head>
<body>
    <p ng-controller="TodoCrtl">
        My name is {{ name }}!</p>
</body>
</html>
