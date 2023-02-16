<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="WebSystem.Admin.LaborReceiptManage.Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>劳务费电子回单上传结果</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="min-width:800px;">
    <div style="margin-top:10px; margin-left:10px; font-size:18px; font-weight:bold;">
        <table style="width:100%">
            <tr>
                <td>劳务费电子回单上传结果</td>
                <td align="right"></td>
            </tr>
        </table>
    </div>
    <hr />
    <div style="margin:10px;">   
        <asp:Button ID="btnUp" runat="server" OnClick="btnUp_Click" Text=" 继续上传 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text=" 返 回 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Button ID="btnOut" runat="server" OnClick="btnOut_Click" Text=" 导 出 " style="cursor:pointer;width:70px; height:22px;" />
        &nbsp;&nbsp;<asp:Label runat="server" ID="lbMsg">成功0张，失败0张</asp:Label>
    </div>
    <div style="margin:10px;" align="center">
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table" OnRowDataBound="gvData_RowDataBound" DataKeyNames="RowNum">
            <Columns>
                <asp:BoundField DataField="RowNum" HeaderText="序号" />
                <asp:BoundField DataField="UpMsg" HeaderText="错误信息" />                              
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
