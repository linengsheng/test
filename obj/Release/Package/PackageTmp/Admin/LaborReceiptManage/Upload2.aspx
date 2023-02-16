<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload2.aspx.cs" Inherits="WebSystem.Admin.LaborReceiptManage.Upload2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>劳务费电子回单上传</title>
    <link href="../../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.10.2.min.js"></script>

</head>
<body>
    <form id="uploadForm" runat="server" enctype="multipart/form-data" action="Upload3.aspx" method="post">
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
            所属劳务公司：<asp:TextBox ID="txtLaborCompany" runat="server" Width="300" MaxLength="200" CssClass="searchBox"></asp:TextBox>
        </div>
        <div style="margin:10px;">
            文件命名格式要求：姓名-金额-支付日期，且只能上传.jpg|.jpeg|.png|.gif|.bmp|.pdf格式文件
        </div>
        <div style="margin:10px;">
            <input type="file" multiple="multiple" id="PersonFile" name="MyFile" accept=".jpg,.jpeg,.gif,.png,.bmp,.pdf"  />
            <button type="button" id="submitFile" onclick="uploadFile()" style="cursor:pointer;width:70px; height:22px;" > 提 交 </button>
        </div>

    </form>
</body>
</html>

 <script type="text/javascript">
     function uploadFile() {
         var fileCount = document.getElementById("PersonFile").files.length;
         var LaborCompany = $("#txtLaborCompany").val();
         if (fileCount == 0) {
             alert("请先选择上传的文件！");
             return false;
         }
         else if (fileCount > 200) {
             alert("一次只能上传200个文件！");
             return false;
         }
         if (LaborCompany.trim() == "") {
             alert("所属劳务公司不能为空！");
             return false;
         }
         $("#submitFile").attr("disabled", true);
         document.forms[0].action = "Upload3.aspx?LaborCompany="+LaborCompany;
         document.forms[0].submit();

     }

     
 </script>
