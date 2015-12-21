<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day9.aspx.cs" Inherits="Angular.Day9" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-options</title>
    <script type="text/javascript">
        function lotteryCtrl($scope) {
            $scope.lotteryModel = [
          {
              id: 1,
              ProductName: '威力彩'
          },
           {
               id: 2,
               ProductName: '今彩539'
           },
            {
                id: 3,
                ProductName: '大樂透'
            }];
        }
    </script>
</head>
<body ng-controller="lotteryCtrl">
    <select ng-model="Select1" ng-options="lotteryCtrl.ProductName for lotteryCtrl in lotteryModel">
        <option value="">-- 請選擇 --</option>
    </select><br />
    <p>
        {{ Select1.ProductName }}</p>
</body>
</html>
