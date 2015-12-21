<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day2.aspx.cs" Inherits="Angular.Styles.Day2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-model</title>
</head>
<body>
    <p>
        My name is {{yourName || 'Anna'}}!</p>
    <label>
        請輸入你的名字，也和大家自我介紹吧!
    </label>
    <input type="text" ng-model="yourName">
</body>
</html>

<%--一開始需先給預設值：Anna--%>
