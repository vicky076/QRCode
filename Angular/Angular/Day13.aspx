<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Day13.aspx.cs" Inherits="Angular.Day13" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script src="Scripts/angular.min.js" type="text/javascript"></script>
<html ng-app>
<head runat="server">
    <title>ng-src</title>
    <script type="text/javascript">
        function MainCtrl($scope) {
            $scope.items = [
             { id: 1,
                 img: 'http://goo.gl/Qv4X4V'
             },
            { id: 2,
                img: 'http://goo.gl/h8VXPH'
            },
            { id: 3,
                img: 'http://goo.gl/YKmPn9'
            },
            { id: 4,
                img: 'http://goo.gl/lr6xH9'
            }
            ];
        }
    </script>
</head>
<body>
    <div ng-controller="MainCtrl">
        <ul>
            <li ng-repeat="item in items">
                <img ng-src="{{item.img}}" />
            </li>
        </ul>
    </div>
</body>
</html>
