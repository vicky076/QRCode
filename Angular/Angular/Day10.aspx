<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day10.aspx.cs" Inherits="Angular.Day10" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head id="Head1" runat="server">
    <title>ng-switch</title>
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

            $scope.Select1 = $scope.lotteryModel[0];
        }
    </script>
</head>
<body ng-controller="lotteryCtrl">
    <select ng-model="Select1" ng-options="lotteryCtrl.ProductName for lotteryCtrl in lotteryModel">
        <option value="">-- 請選擇 --</option>
    </select><br />
    <div class="animate-switch-container" ng-switch on="Select1.ProductName">
        <div ng-switch-when="威力彩">
            威力彩</div>
        <div ng-switch-when="今彩539">
            今彩539</div>
        <div ng-switch-default>
            大樂透</div>
    </div>
</body>
</html>
<%--1.使用ng-switch on，作為篩選的依據。
2.使用ng-switch-when，決定當下是否顯示。
3.使用ng-switch-default，預設的顯示內容。--%>
