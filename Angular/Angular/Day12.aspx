<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day12.aspx.cs" Inherits="Angular.Day12" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-include </title>
    <script type="text/javascript">
        function Ctrl($scope) {
            $scope.templates =
                [{ name: 'template1.html', url: 'template1.html' },
                { name: 'template2.html', url: 'template2.html'}];
            $scope.template = $scope.templates[0]; //指定下拉選單內容，預設為第一筆。
        } 
    </script>
</head>
<body>
    <div ng-controller="Ctrl">
        <select ng-model="template" ng-options="t.name for t in templates">
            <option value="">(blank)</option>
        </select>
        url of the template: <tt>{{template.url}}</tt>
        <div>
            <div ng-include="template.url">
            </div>
        </div>
    </div>
</body>
</html>
