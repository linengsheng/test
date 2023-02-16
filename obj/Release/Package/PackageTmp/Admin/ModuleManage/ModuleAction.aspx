<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleAction.aspx.cs" Inherits="WebSystem.Admin.ModuleManage.ModuleAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>设置模块功能</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .tdH {background-color:#E8F6FF; width:100px; text-align:right;
              border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;}
        .tdR {border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;}
    </style>     
</head>
<body>
    <form id="form1" runat="server" style="min-width:1000px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        设置模块功能
    </div>
    <hr />
    <div style="margin:10px;">
        <table style="line-height:22px;border-collapse:collapse; border:1px solid #7EBFF5;width:100%;">
        <tr style="height:30px" >
            <td class="tdH">
                模块名称：
            </td>
            <td class="tdR">
                <asp:Label runat="server" ID="lbModulename"></asp:Label>    
            </td>            
        </tr>    
        <tr style="height:30px">
            <td class="tdH">
                模块功能：
            </td>
            <td class="tdR" style="line-height:30px">
                <asp:CheckBoxList ID="cblModuleaction" runat="server" RepeatColumns="5" CellSpacing="10" CellPadding="10" RepeatDirection="Horizontal"></asp:CheckBoxList>
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
