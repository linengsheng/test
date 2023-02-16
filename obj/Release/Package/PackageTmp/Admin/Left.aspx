<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="WebSystem.Admin.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>后台管理系统</title>
    <%--<meta http-equiv="refresh" content="60" />--%>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
    
    <style type="text/css">
        .divList{border: 0px solid #2690dd; width:100%;height:25px; padding-top:8px; text-align:center;}
        .divTitle{border: 0px solid #2690dd; width:95%;height:20px; padding-top:5px; padding-left:10px; font-weight:bold; background-color:#2690dd; text-align:left; color:#fff;}
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div style="font-size: 15px">
        <div runat="server" id="divindex" class="divList" style="margin-top:10px;">
            <a href="LaborReceiptManage/Index.aspx" target="rightFrame" style="font-size: 15px; font-weight: bold;">劳务费电子回单上传</a>
        </div>
        <asp:Literal runat="server" ID="ltlModule"></asp:Literal>
    </div>
    </form>
</body>
</html>
