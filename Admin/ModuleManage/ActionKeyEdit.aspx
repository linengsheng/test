<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionKeyEdit.aspx.cs" Inherits="WebSystem.Admin.ModuleManage.ActionKeyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑功能操作关键字</title>
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
                功能名称：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtactionkeyname" runat="server" MaxLength="20" Width="300px" CssClass="editBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtactionkeyname" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>            
        </tr>    
        <tr style="height:30px">
            <td class="tdH">
                功能标识码：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtactionkeycode" runat="server" MaxLength="20" Width="300px" CssClass="editBox" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtactionkeycode" Display="Dynamic" ErrorMessage="*" ValidationGroup="g1" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr style="line-height:30px">
            <td class="tdH">
                排序：
            </td>
            <td class="tdR">
                <asp:TextBox ID="txtSortNO" runat="server" Width="80px" CssClass="editBox" MaxLength="10"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtSortNO" Display="Dynamic" ErrorMessage="请输入正整数！"
                    ValidationExpression="^[0-9]{1,10}" ValidationGroup="g1"></asp:RegularExpressionValidator>
            </td>
        </tr>
        </table>
        <div align="center" style="margin:10px">
            <asp:Button runat ="server" ID="btnSubmit" onclick="btnSubmit_Click"  ValidationGroup="g1" Text=" 提 交 "  style="cursor:pointer;width:70px; height:22px;"/>&nbsp;&nbsp;
            <input type="reset" id="btnReset"  value =" 重 置 " style ="cursor:pointer;width:70px; height:22px;" />&nbsp;&nbsp;
            <input type="button"  id="btnReturn"  value =" 返 回 " style ="cursor:pointer;width:70px; height:22px;" onclick="window.location.href = 'ActionKey.aspx';" />
        </div>
    </div>
    </form>
</body>
</html>
