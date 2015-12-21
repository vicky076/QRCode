<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day6.aspx.cs" Inherits="Angular.Day6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-init</title>
</head>
<body>
    <div ng-init="greeting='Hello';person='word'">
        {{greeting}} {{person}}!
    </div>
</body>
</html>
<%--model一開始載入，會先執行ng-init，我們可以利用ng-init設定初始值--%>
