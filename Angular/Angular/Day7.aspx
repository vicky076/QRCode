<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day7.aspx.cs" Inherits="Angular.Styles.Day7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-repeat</title>
    <script type="text/javascript">
        function MainCtrl($scope) {
            $scope.names = ['Lily', 'Lucy', 'Marry', 'John', 'Nana'];
            $scope.items = [
            {title: '性別', status: '1'},
            {title: '血型', status: '0'},
            { title: '生日', status: '1' }
            ];
        }
    </script>
</head>
<body>
    <div ng-controller="MainCtrl">
        <p ng-repeat="name in names">
            {{$index+1}}.{{name}}</p>
        <p>
            以上有{{names.length}}筆資料</p>
        <ul>
            <li ng-init="aa" ng-repeat="item in items">{{item.title}}</li>
        </ul>
        <p>
            以上有{{items.length}}筆資料</p>
    </div>
</body>
</html>
