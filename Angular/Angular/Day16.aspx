<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day16.aspx.cs" Inherits="Angular.Day16" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-checked</title>
    <script type="text/javascript">
        function Ctrl($scope) {
            $scope.checkTexts = [
            { checkName: "Anna" },
            { checkName: "Don" },
            { checkName: "Will" },
            { checkName: "Joyce" }
            ];
        }
    </script>
</head>
<body>
    <div ng-controller="Ctrl">
        Check me to check both:
        <input type="checkbox" ng-model="master" />
        <br />
        <p ng-repeat="checkText in checkTexts">
            <input type="checkbox" ng-checked="master" />{{checkText.checkName}}
        </p>
    </div>
</body>
</html>
