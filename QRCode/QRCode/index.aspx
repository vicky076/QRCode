<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QRCode.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <span style="color: Red">訊息：
        <asp:Label ID="labMsg" runat="server"></asp:Label></span>
    <br />
    <br />
    <fieldset style="width: 1000px">
        <legend>產條碼：</legend>
        <table align="left" border="1">
            <tr>
                <td>
                </td>
                <td>
                    請輸入文字：<br />
                    <asp:TextBox ID="textBox1" runat="server" TextMode="MultiLine" Width="600px" Height="100px"></asp:TextBox>
                    <br />
                    <br />
                    圖檔格式:
                    <asp:DropDownList ID="ddlStype" runat="server">
                        <asp:ListItem Text="JPEG" Value=".jpg"></asp:ListItem>
                        <asp:ListItem Text="PNG" Value=".png"></asp:ListItem>
                        <asp:ListItem Text="GIF" Value=".gif"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    樣式：
                    <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                        <asp:ListItem Text="QRCode" Value="QR"></asp:ListItem>
                        <asp:ListItem Text="128碼" Value="128"></asp:ListItem>
                        <asp:ListItem Text="EAN-13" Value="13"></asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    Size：<asp:TextBox ID="txtSize" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnCode" runat="server" Text="產生條碼" OnClick="btnCode_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    QRCode圖檔：
                </td>
                <td colspan="2">
                    <asp:Image ID="pictureBox1" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset style="width: 800px">
        <legend>解析條碼：</legend>
        <table>
            <tr>
                <td colspan="2">
                    上傳條碼：<span style="color: Red">*限副檔名(.jpg/.jepg/.gif/.png)</span>
                    <br />
                    <asp:FileUpload ID="fupImport" runat="server" Width="300px" />
                    <asp:Button ID="btnSubmit" runat="server" Text="確定" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    圖檔：
                    <br />
                    <asp:Image ID="pictureBox2" runat="server" />
                    <br />
                    <asp:TextBox ID="txtpictureBox2" runat="server" Width="300px" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnDeCode" runat="server" Text="解析" OnClick="btnDeCode_Click" />
                    <br />  <br />
                    <span style="color: Red">訊息：
                        <asp:Label ID="labDesMsg" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtDesCode" runat="server" TextMode="MultiLine" Width="600px" Height="100px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    </form>
</body>
</html>
