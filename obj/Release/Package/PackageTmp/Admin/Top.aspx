<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="WebSystem.Admin.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>后台管理系统</title>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function changepwd() {
            window.parent.document.getElementById('rightFrame').setAttribute('src', 'ChangePwd.aspx');
        }
    </script>
</head>
<body style="background-color: #3290d0;">
    <form id="form1" runat="server">
    <div >
    <table width="100%" cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td style="height:75px">
                <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 30px; color: #FFFFFF; text-align: center; width: 250px;">
                      后台管理系统
                </div>
            </td>
            <td valign="bottom" >
            <div style="float:right; margin-right:10px;color:#fff;" align="right">
                <div style="line-height:20px">
                    <a href="javascript:changepwd();" style="color:#fff;">[修改密码]</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnExit" style="color:#fff;" runat="server" OnClientClick="return confirm('确定要退出吗？');" OnClick="lbtnExit_Click">[退 出]</asp:LinkButton></div>
                <div style="line-height:30px">
                    <asp:Label ID="lbUserName" runat="server" Text="" Font-Bold="true" ></asp:Label>
                    <asp:Label ID="lbTime" runat="server" Text=""></asp:Label></div>
            </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
