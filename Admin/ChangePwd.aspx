<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="WebSystem.Admin.ChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改密码</title>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        修改密码
    </div>
    <hr />
    <div style="min-width:800px;">
        <table>
        <tr style="height:30px" >
            <td align="right" style="width:100px;">
                用户名：
            </td>
            <td >
                <asp:Label runat="server" ID="lbuserid"></asp:Label>
            </td>            
        </tr>            
        <tr style="height:30px">
            <td align="right" style="width:100px;">
                真实姓名：
            </td>
            <td >
                <asp:TextBox ID="txtusername" runat="server" MaxLength="50" Width="300px" CssClass="editBox" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True" Enabled="True"></asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td align="right" style="width:100px;">
                <asp:CheckBox runat="server" ID="cbPwd" Text="修改密码" oncheckedchanged="cbPwd_CheckedChanged" AutoPostBack="true" Checked="True" /></td>
            <td></td>
        </tr>
        <tr style="height:30px" runat="server" id="trpwd1" >
            <td align="right" style="width:100px;">
                旧密码：
            </td>
            <td >
                <asp:TextBox ID="txtuserpwdold" runat="server" MaxLength="20" Width="300px" CssClass="editBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtuserpwdold" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr style="height:30px" runat="server" id="trpwd2">
            <td align="right" style="width:100px;">
                新密码：
            </td>
            <td >
                <asp:TextBox ID="txtuserpwd" runat="server" MaxLength="20" Width="300px" CssClass="editBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtuserpwd" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtuserpwd" ErrorMessage="只能输入数字、字母、符号。" ValidationGroup="g1" SetFocusOnError="True" ValidationExpression="^\S+$" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>            
        </tr>
        <tr style="height:30px" runat="server" id="trpwd3">
            <td align="right" style="width:100px;">
                确认新密码：
            </td>
            <td >
                <asp:TextBox ID="txtuserpwd2" runat="server" MaxLength="20" Width="300px" CssClass="editBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtuserpwd2" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="密码不一致！" ControlToCompare="txtuserpwd" ControlToValidate="txtuserpwd2" Display="Dynamic" ValidationGroup="g1" SetFocusOnError="True"></asp:CompareValidator>
            </td>            
        </tr>
        <tr style="height:50px">
            <td>
            </td>
            <td >
                <asp:Button runat ="server" ID="btnSubmit" onclick="btnSubmit_Click"  ValidationGroup="g1" Text=" 提 交 "  style="cursor:pointer;width:70px; height:22px;"/>&nbsp;&nbsp;
                <input type="reset" id="btnReset"  value =" 重 置 " style ="cursor:pointer;width:70px; height:22px;" />
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
