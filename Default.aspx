<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="ndl._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       .style1
       {
          width: 61px;
          height: 39px;
       }
       .style2
       {
          width: 2px;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div id="login">
      
       <table style="width:100%;">
          <tr>
             <td colspan="2" class="style2">
                <img class="style1" src="images/logo.jpg" /></td>
             <td>
                <h3>USER LOGIN</h3></td>
          </tr>
          <tr>
             <td>
                &nbsp;</td>
             <td colspan="2">
                <asp:Label ID="lblError" CssClass="myError" runat="server"></asp:Label>
             </td>
          </tr>
          <tr>
             <td>
                UserID</td>
             <td colspan="2">
                <asp:TextBox ID="txtUserID" CssClass="logText" runat="server"></asp:TextBox>
             </td>
          </tr>
          <tr>
             <td>
                Password:</td>
             <td colspan="2">
                <asp:TextBox ID="txtPassword" CssClass="logText" runat="server" 
                   TextMode="Password"></asp:TextBox>
             </td>
          </tr>
          <tr>
             <td colspan="3">
                &nbsp;</td>
          </tr>
          <tr>
             <td>
                &nbsp;</td>
             <td colspan="2">
                <asp:Button ID="btnLog" CssClass="logBtn" runat="server" Text="Login" />
             </td>
          </tr>
       </table>
      
    </div>

    </form>
</body>
</html>
