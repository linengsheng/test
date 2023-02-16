<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebSystem.Admin.SysUserManage.Index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统用户管理</title>
<link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" style="min-width:1000px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        系统用户管理
    </div>
    <hr />
    <div >
        <div style="margin:10px;">
            用户名：<asp:TextBox runat="server" ID="txtuserid" Width="100px" CssClass="searchBox"></asp:TextBox>
            真实姓名：<asp:TextBox runat="server" ID="txtusername" Width="100px" CssClass="searchBox"></asp:TextBox>
            角色：<asp:DropDownList runat="server" ID="ddlrole" Width="105px" AutoPostBack="True" OnSelectedIndexChanged="ddlrole_SelectedIndexChanged"></asp:DropDownList>
            启用/禁用：<asp:DropDownList runat="server" ID="ddlstate" Width="105px" AutoPostBack="True" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"><asp:ListItem Value="">=全部=</asp:ListItem><asp:ListItem Value="1">启用</asp:ListItem><asp:ListItem Value="0">禁用</asp:ListItem></asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text=" 查 询 " style="cursor:pointer;width:70px; height:22px;" />
            &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text=" 添 加 " style="cursor:pointer;width:70px; height:22px;" />
        </div>
        <div align="center" style="margin:10px">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:BoundField DataField="username" HeaderText="用户名" />
                <asp:BoundField DataField="realname" HeaderText="真实姓名" />
                <asp:BoundField DataField="role_name" HeaderText="角色" />
                <asp:BoundField DataField="add_time" HeaderText="注册时间" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="最后登录时间">
                    <ItemTemplate>
                        <%# getLastLoginTime(Eval("sysuser_id").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="启用/禁用" >
                    <ItemTemplate>
                        <asp:CheckBox ID="cbstate" userid='<%# Eval("sysuser_id") %>' runat="server" OnCheckedChanged="cbstate_CheckedChanged" Checked='<%# Eval("state").ToString()=="1"?true:false %>' AutoPostBack="true" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPwd" runat="server" OnCommand="btnPwd_Command" CommandArgument='<%# Eval("sysuser_id")%>' >&nbsp;设置密码&nbsp;</asp:LinkButton>
                        <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%# Eval("sysuser_id")%>' >&nbsp;编辑&nbsp;</asp:LinkButton>
                        <asp:LinkButton ID="btnDel" runat="server" OnCommand="btnDel_Command" CommandArgument='<%# Eval("sysuser_id")%>' OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');">&nbsp;删除&nbsp;</asp:LinkButton>
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
    </div>
    </form>
</body>
</html>