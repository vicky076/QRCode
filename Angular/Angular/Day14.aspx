<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day14.aspx.cs" Inherits="Angular.Day14" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-href</title>
    <script type="text/javascript">
        function MainCtrl($scope) {
            $scope.y1 = "watch?v=2AUhPKJvslo";
            $scope.y2 = "watch?v=obc463n8w8E";
        }
    </script>
</head>
<body ng-controller="MainCtrl">
    <a href="https://www.youtube.com/{{ y1 }}">Youtube1</a><br />
    <%--原本href的寫法--%>
    <a ng-href="https://www.youtube.com/{{ y2 }}">Youtube</a><%--改成ng-href的寫法--%>
</body>
</html>
