<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day11.aspx.cs" Inherits="Angular.Day11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-change</title>
    <script type="text/javascript">
        function Controller($scope) {
            $scope.confirmed = true;
            $scope.counter = 0;
            $scope.change = function () {
                $scope.counter++;
            };

            $scope.sizes = [
            { code: 1, name: 'n1', counter: 1 },
            { code: 2, name: 'n2', counter: 2}];

            $scope.update = function () {
                angular.forEach($scope.sizes, function (value, key) {
                    value.counter = value.counter * 3;
                });
            };

            $scope.item = $scope.sizes[0];
        }
    </script>
</head>
<body>
    <div ng-controller="Controller">
        <input type="checkbox" ng-model="confirmed" ng-change="change()" />
        <label>
            使用checkbox，改變counter數字</label><br />
        <br />
        debug={{confirmed}}<br />
        counter ={{counter}}
        <hr />
        <h4>
            使用下拉選單，更新變數*3</h4>
        <select ng-options="size as size.counter for size in sizes " ng-model="item" ng-change="update()">
        </select>
        <br />
        <p>
            {{item.counter}}</p>
    </div>
</body>
</html>
