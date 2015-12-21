<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day5.aspx.cs" Inherits="Angular.Day5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-show</title>
</head>
<body>
    <div>
        Click me:<input type="checkbox" ng-model="checked"/>
        <br />
        Show: <span ng-show="checked">I show up when your checkbox is checked.</span>
        <br />
        Hide: <span ng-hide="checked">I hide when your checkbox is checked.</span>
    </div>
</body>
</html>

<%--只需要在欲觸發的事件上綁定model名稱
接下來在你想要顯示/隱藏的物件中，加入ng-show/ng-hide就可以直接產生效果--%>