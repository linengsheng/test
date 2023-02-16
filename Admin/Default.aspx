<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebSystem.Admin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>润建股份有限公司-后台管理系统</title>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />
</head>
<frameset rows="80,*,30" cols="1024" frameborder="no" framespacing="0">
    <frame src="Top.aspx" name="topFrame" scrolling="no" noresize>
    <frameset id="main" cols="190,*" rows="*" frameborder="0" framespacing="5" border="0px">
        <frame src="Left.aspx" name="leftFrame" id="leftFrame" scrolling="no" style="border-left-style: solid; border-right-style: solid; border-width: 5px; border-color: #2690dd; margin-right:0px;" noresize />
        <frame src="Main.aspx" name="rightFrame" id="rightFrame" frameborder="0" scrolling="auto" />
    </frameset>
	<frame src="Bottom.aspx" name="bottomFrame" id="bottomFrame" scrolling="no" frameborder="0" noresize />
</frameset>
<body>
    
</body>
</html>
