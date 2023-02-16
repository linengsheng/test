<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSystem.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>润建股份有限公司-后台管理系统</title>
    <link href="../Css/Content.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/transparent.js" type="text/javascript"></script>
    <script type="text/javascript">
        function btnloginover() {
            $("#btnSubmit").attr("class", "btn-login2");
        }

        function btnloginout() {
            $("#btnSubmit").attr("class", "btn-login1");
        }
    </script>
</head>
<body class="login-bg">
    <form id="form1" runat="server">
    <div ></div><!--背景图的层-->
    <div class="transverse"><!--背景图的层-->
    	<div class="mainbox"><!--背景图的层-->
    		<div class="loginbox"><!--输入框的层-->
            	<div class="clerboth40"></div>
        		<dl class="input-list">
            		<dt>用户名：</dt>
                    <dd class="inputbox">
                        <asp:TextBox ID="txtUserName" AutoCompleteType="None" runat="server" TabIndex="1" CssClass="inputbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ValidationGroup="g1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </dd>
                    <div class="clerboth">&nbsp;</div>
                    <dt>密　码：</dt>
                    <dd class="inputbox">
                        <asp:TextBox ID="txtPassword" AutoCompleteType="None" TextMode="Password" runat="server" TabIndex="2" CssClass="inputbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ValidationGroup="g1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </dd>
                    <div class="clerboth">&nbsp;</div>
                    <dt>验证码：</dt>
                    <dd class="yzm">
                    	<asp:TextBox ID="txtCode" AutoCompleteType="None" runat="server" TabIndex="3" CssClass="codebox"></asp:TextBox>
                        <div title="点击更换验证码">
                            <img alt="点击更换验证码" src="Code.aspx" onclick="this.src='Code.aspx?t='+new Date().getTime()" height="29px" class="yzm-img" />
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCode" ErrorMessage="*" ValidationGroup="g1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </dd>
                    <div class="clerboth">&nbsp;</div>
                    <dt>&nbsp;</dt>
                    <dd class="btn-login">
                    	<asp:Button runat="server" ID="btnSubmit" Text=" 登 录 " OnClick="btnSubmit_Click" ValidationGroup="g1" CssClass="btn-login1" style="cursor:pointer;" onmouseover="btnloginover();" onmouseout="btnloginout();" />
                    </dd>
                </dl>
                
            </div>
        </div>
    </div>
    
    <div class="copyright"><p >CopyRight &copy; 润建股份有限公司</p></div>
    </form>
</body>
</html>