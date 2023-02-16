<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActionKey.aspx.cs" Inherits="WebSystem.Admin.ModuleManage.ActionKey" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>功能关键字管理</title>
<link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="min-width:1000px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        功能关键字管理
    </div>
    <hr />
    <div >
        <div style="margin:10px;" align="right">
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text=" 添加功能关键字 " style="cursor:pointer;width:110px; height:22px;" />
            &nbsp;&nbsp;
            <input type="button"  id="btnReturn"  value =" 返回模块管理 " style ="cursor:pointer;width:110px; height:22px;" onclick="window.location.href = 'Index.aspx';" />
        </div>
        <div align="center" style="margin:10px">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ActionKey_Name" HeaderText="功能名称" />
                <asp:BoundField DataField="ActionKey_Code" HeaderText="功能标识码" />
                <asp:TemplateField HeaderText="操作" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("ActionKey_ID")%>' >&nbsp;编辑&nbsp;</asp:LinkButton>
                        <asp:LinkButton ID="btnDel" runat="server" OnCommand="btnDel_Command" CommandArgument='<%# Eval("ActionKey_ID")%>' OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');">&nbsp;删除&nbsp;</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="150px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <div style="margin:10px">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页"
                HorizontalAlign="Right" LastPageText="末页" NextPageText="下一页" OnPageChanged="AspNetPager1_PageChanged"
                PrevPageText="上一页" ShowInputBox="Always" PageSize="20">
            </webdiyer:AspNetPager>
        </div>
        <div style="margin:10px">
            <label style="color:red">功能关键字关系到系统功能的正常使用，请勿随意修改。</label>
        </div>
    </div>
    </form>
</body>
</html>
