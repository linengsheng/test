<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Right2.aspx.cs" Inherits="WebSystem.Admin.RoleManage.Right2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>权限设置</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    
    <style type="text/css">
        .table2 { line-height:22px;border-collapse:collapse; border:1px solid #7EBFF5;width:99%;}
        .tdH {background-color:#E8F6FF;  text-align:center; border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;}
        .tdR {border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;text-align:left;}
        .tdR2 {border-collapse:collapse; border:1px solid #7EBFF5; padding:2px 0px 2px 5px;text-align:left;background-color:#f6f6f6;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        权限设置
    </div>
    <hr />
    <div align="center" style="margin:10px;min-width:800px;">
        <div style="margin-left:10px; margin-bottom:10px;font-weight:bold;" align="left" >
            角色：<asp:Label runat="server" ID="lbrolename"></asp:Label>
        </div>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table2" OnRowDataBound="gvData_RowDataBound" DataKeyNames="module_id">
            <Columns>
                <asp:BoundField DataField="Parent_Name" HeaderText="上级模块" HeaderStyle-CssClass="tdH" ItemStyle-CssClass="tdR" /> 
                <asp:BoundField DataField="module_name" HeaderText="模块" HeaderStyle-CssClass="tdH" ItemStyle-CssClass="tdR" />                
                <asp:TemplateField HeaderText="权限" HeaderStyle-CssClass="tdH" ItemStyle-CssClass="tdR" >
                    <ItemTemplate>
                        <asp:CheckBoxList runat="server" ID="cbright" RepeatDirection="Horizontal" CellPadding="10"></asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div align="center" style="margin:10px">
            <asp:Button runat ="server" ID="btnSubmit" onclick="btnSubmit_Click"  ValidationGroup="g1" Text=" 提 交 "  style="cursor:pointer;width:70px; height:22px;" />&nbsp;&nbsp;
            <input type="reset" id="btnReset"  value =" 重 置 " style ="cursor:pointer;width:70px; height:22px;" />&nbsp;&nbsp;
            <input type="button"  id="btnReturn"  value =" 返 回 " style ="cursor:pointer;width:70px; height:22px;" onclick="window.location.href = 'Index.aspx';" />
        </div>
    </div>
    </form>
</body>
</html>