<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebSystem.Admin.LaborReceiptManage.Index" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>劳务费电子回单上传</title>
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
    <form id="form1" runat="server" defaultbutton="btnSearch" style="min-width:800px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        <table style="width:100%">
            <tr>
                <td>劳务费电子回单上传</td>
                <td align="right"></td>
            </tr>
        </table>
    </div>
    <hr />
    <div style="margin:10px;">
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text=" 上 传 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Button ID="btnDels" runat="server" OnClick="btnDels_Click" Text=" 删 除 " style="cursor:pointer;width:70px; height:22px;" OnClientClick="return confirm('删除数据不可恢复，您确认要删除吗？');"/>
        &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text=" 搜 索 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text=" 重 置 " style="cursor:pointer;width:70px; height:22px;" />
        <br />
        <br />
        编号：<asp:TextBox ID="txtId" runat="server" Width="90" MaxLength="20" CssClass="searchBox"></asp:TextBox>
        图片名称：<asp:TextBox ID="txtPicName" runat="server" Width="90" MaxLength="200" CssClass="searchBox"></asp:TextBox>
        姓名：<asp:TextBox ID="txtUserName" runat="server" Width="90" MaxLength="20" CssClass="searchBox"></asp:TextBox>
        金额：<asp:TextBox ID="txtAmount" runat="server" Width="90" MaxLength="20" CssClass="searchBox"></asp:TextBox>
        劳务公司：<asp:TextBox ID="txtLaborCompany" runat="server" Width="90" MaxLength="200" CssClass="searchBox"></asp:TextBox>
        操作人：<asp:TextBox ID="txtOperateUser" runat="server" Width="90" MaxLength="100" CssClass="searchBox"></asp:TextBox>        
    </div>
    <div style="margin:10px;" align="center">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound" DataKeyNames="Id">
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
                <asp:BoundField DataField="Id" HeaderText="内部编号" />
                <asp:BoundField DataField="PicName" HeaderText="图片名称" />
                <asp:BoundField DataField="UserName" HeaderText="姓名" />
                <asp:BoundField DataField="Amount" HeaderText="金额" />
                <asp:BoundField DataField="PayDate" HeaderText="支付日期" />
                <asp:BoundField DataField="LaborCompany" HeaderText="劳务公司" />
                <asp:BoundField DataField="OperateUser" HeaderText="操作人" />  
                <asp:BoundField DataField="UpTime" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" ItemStyle-Width="180px" />
                              
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin:10px">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="首页"
            HorizontalAlign="Right" LastPageText="末页" NextPageText="下一页" OnPageChanged="AspNetPager1_PageChanged"
            PrevPageText="上一页" ShowInputBox="Always" PageSize="25">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
