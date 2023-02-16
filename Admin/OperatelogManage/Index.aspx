<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebSystem.Admin.OperatelogManage.Index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>系统用户操作日志管理</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../userControls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function checkAll(obj) {
            $("#<%=gvData.ClientID %> tr>td:first-child :checkbox").each(function () {
                $(this).attr('checked', $(obj).attr('checked'));
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch" style="min-width:1000px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        <table style="width:100%">
            <tr>
                <td>系统用户操作日志管理</td>
                <td align="right"></td>
            </tr>
        </table>
    </div>
    <hr />
    <div style="margin:10px;">
        帐号或姓名：<asp:TextBox ID="txtUser" runat="server" Width="90" MaxLength="20" CssClass="searchBox"></asp:TextBox>
        操作详情：<asp:TextBox ID="txtoperateinfo" runat="server" Width="90" MaxLength="20" CssClass="searchBox"></asp:TextBox>
        操作时间：<asp:TextBox ID="txtBeginDate" runat="server" Width="120px" MaxLength="20" onfocus="new WdatePicker(this,'%Y年%M月%D日',false)" CssClass="searchBox"></asp:TextBox>-
                   <asp:TextBox ID="txtEndDate" runat="server" Width="120px" MaxLength="20" onfocus="new WdatePicker(this,'%Y年%M月%D日',false)" CssClass="searchBox"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text=" 查 询 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Button ID="btnDels" runat="server" OnClick="btnDels_Click" Text=" 批量删除 " style="cursor:pointer;width:70px; height:22px;" OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');"/>
        &nbsp;&nbsp;<asp:Button ID="btnOut" runat="server" OnClick="btnOut_Click" Text=" 导 出 " style="cursor:pointer;width:70px; height:22px;" />
    </div>
    <div style="margin:10px;" align="center">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound" DataKeyNames="sysuseroperatelog_id">
            <Columns>
                <asp:TemplateField ItemStyle-Width="80px">
                    <HeaderTemplate>
                            <input id="Checkbox1" onclick="checkAll(this);" type="checkbox" />全选本页
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelected" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" BorderColor="#7EBFF7" BorderWidth="1px" BorderStyle="Solid" />
                    <HeaderStyle BorderColor="#7EBFF7" BorderWidth="1px" BorderStyle="Solid" />
                </asp:TemplateField>
                <asp:BoundField DataField="username" HeaderText="操作帐号" />
                <asp:BoundField DataField="realname" HeaderText="操作姓名" />
                <asp:BoundField DataField="operate_ip" HeaderText="操作IP" />
                <asp:TemplateField HeaderText="操作详情">
                    <ItemTemplate>
                        <div title='<%# Eval("operate_info") %>'><%# Eval("operate_info").ToString().Length > 20? Eval("operate_info").ToString().Substring(0, 20) + "..." : Eval("operate_info").ToString() %></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="operate_time" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" ItemStyle-Width="180px" />
                <asp:TemplateField HeaderText="操作" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDel" runat="server" OnCommand="btnDel_Command" CommandArgument='<%# Eval("sysuseroperatelog_id")%>' OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');">&nbsp;删除&nbsp;</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="70px" />
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
    <asp:GridView ID="gvOut" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="username" HeaderText="操作帐号" />
            <asp:BoundField DataField="realname" HeaderText="操作姓名" />
            <asp:BoundField DataField="operate_ip" HeaderText="操作IP" />
            <asp:BoundField DataField="operate_info" HeaderText="操作类型" />
            <asp:BoundField DataField="operate_time" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>

