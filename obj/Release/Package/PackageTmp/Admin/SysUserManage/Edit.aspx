<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebSystem.Admin.SysUserManage.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑系统用户</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .tdH {background-color:#E8F6FF; width:100px; text-align:right;
              border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;}
        .tdR {border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;}
    </style>     
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        <asp:Label runat="server" ID="lbType"></asp:Label>
    </div>
    <hr />
    <div style="margin:10px;min-width:800px;">
        <table style="line-height:22px;border-collapse:collapse; border:1px solid #7EBFF5;width:100%;">
        <tr style="height:30px" >
            <td class="tdH">
                用户名：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtuserid" runat="server" MaxLength="20" Width="300px" CssClass="editBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtuserid" Display="Dynamic" ErrorMessage="*不能为空" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev2" runat="server" ErrorMessage="不能输入特殊字符" ValidationGroup="g1" SetFocusOnError="True" ValidationExpression="^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$" ControlToValidate="txtuserid" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>            
        </tr>    
        <tr style="height:30px">
            <td class="tdH">
                真实姓名：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtusername" runat="server" MaxLength="30" Width="300px" CssClass="editBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtusername" Display="Dynamic" ErrorMessage="*不能为空" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>            
        </tr>    
        <tr style="height:30px" runat="server" id="trpwd">
            <td class="tdH" >
                密码：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtuserpwd" runat="server" MaxLength="20" Width="300px" CssClass="editBox" TextMode="Password" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtuserpwd" Display="Dynamic" ErrorMessage="*不能为空" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtuserpwd" ErrorMessage="只能输入数字、字母、符号。" ValidationGroup="g1" SetFocusOnError="True" ValidationExpression="^\S+$" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="height:30px" runat="server" id="trpwd2">
            <td class="tdH" >
                确认密码：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtuserpwd2" runat="server" MaxLength="20" Width="300px" CssClass="editBox" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtuserpwd2" Display="Dynamic" ErrorMessage="*不能为空" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="密码不一致！" ControlToCompare="txtuserpwd" ControlToValidate="txtuserpwd2" Display="Dynamic" ValidationGroup="g1" SetFocusOnError="True"></asp:CompareValidator>
            </td>            
        </tr>
        <tr style="height:30px">
            <td class="tdH" >
                角色：
            </td>
            <td class="tdR">
                <asp:DropDownList runat="server" ID="ddlrole"></asp:DropDownList>
            </td>            
        </tr>
        </table>
        <div align="center" style="margin:10px">
            <asp:Button runat ="server" ID="btnSubmit" onclick="btnSubmit_Click"  ValidationGroup="g1" Text=" 提 交 "  style="cursor:pointer;width:70px; height:22px;"/>&nbsp;&nbsp;
            <input type="reset" id="btnReset"  value =" 重 置 " style ="cursor:pointer;width:70px; height:22px;" />&nbsp;&nbsp;
            <input type="button"  id="btnReturn"  value =" 返 回 " style ="cursor:pointer;width:70px; height:22px;" onclick="window.location.href='Index.aspx';" />
        </div>
    </div>
    </form>
</body>
</html>

