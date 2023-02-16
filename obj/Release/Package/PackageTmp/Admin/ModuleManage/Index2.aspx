<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="WebSystem.Admin.ModuleManage.Index2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>上级模块管理</title>
<link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="min-width:1000px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        上级模块管理
    </div>
    <hr />
    <div >
        <div style="margin:10px;" align="right">
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text=" 添 加 " style="cursor:pointer;width:70px; height:22px;" />
            &nbsp;&nbsp;<input type="button"  id="btnReturn"  value =" 返 回 " style ="cursor:pointer;width:70px; height:22px;" onclick="location.href='Index.aspx';" />
        </div>
        <div align="center" style="margin:10px">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:BoundField DataField="module_name" HeaderText="模块名称" />
                <asp:BoundField DataField="module_code" HeaderText="模块标识码" />
                <asp:TemplateField HeaderText="操作" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("module_id")%>' >&nbsp;编辑&nbsp;</asp:LinkButton>
                        <asp:LinkButton ID="btnDel" runat="server" OnCommand="btnDel_Command" CommandArgument='<%# Eval("module_id")%>' OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');">&nbsp;删除&nbsp;</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="150px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <div style="margin:10px">
            <label style="color:red">模块关系到系统功能的正常使用，请勿随意修改。</label>
        </div>
    </div>
    </form>
</body>
</html>
