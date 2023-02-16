<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSystem.Admin.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>后台管理系统</title>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="left" style="min-width:900px">
        <div style="font-weight: bold; font-size:25px; text-align:center; margin-top:200px;">
            欢迎使用后台管理系统！
        </div>
        <div style="margin:10px; line-height:30px;" >
            <asp:Literal runat="server" ID="ltlwork"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>