using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin
{
    public partial class Login : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearCookie();
            txtUserName.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["codeKvanit"] != null)
            {
                if (txtCode.Text.Trim().ToLower() == Session["codeKvanit"].ToString().ToLower())    //验证码不区分大小写
                {
                    //验证用户名和密码是否正确
                    string userid = txtUserName.Text.Trim().ToLower();
                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "MD5").ToLower();
                    DataTable dt = TSysUser.Select(userid, password);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //登录成功，保存cookie
                        SaveToCookie(dt.Rows[0]["sysuser_id"].ToString(), dt.Rows[0]["username"].ToString(), dt.Rows[0]["realname"].ToString(), dt.Rows[0]["role_id"].ToString());
                        TLoginlog.AddForSysUser();
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Alert("帐号或密码错误！请重新输入！");
                        txtUserName.Focus();
                    }
                }
                else
                {
                    Alert("验证码输入错误！");
                    txtCode.Focus();
                    txtPassword.Attributes.Add("value", txtPassword.Text);
                }
            }
            else
            {
                Alert("验证码已过期！请重新输入！");
                txtCode.Focus();
                txtPassword.Attributes.Add("value", txtPassword.Text);
            }
        }

        void SaveToCookie(string userid, string username, string realname, string role_id)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (cookie == null)
            {
                cookie = new HttpCookie("KvanitWebSystem");
                cookie.Name = "KvanitWebSystem";
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Values.Add("sysuser_id", userid);
                cookie.Values.Add("role_id", role_id);
                cookie.Values.Add("username", HttpUtility.UrlEncode(username, Encoding.GetEncoding("gb2312")));
                cookie.Values.Add("realname", HttpUtility.UrlEncode(realname, Encoding.GetEncoding("gb2312")));
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Values.Add("sysuser_id", userid);
                cookie.Values.Add("role_id", role_id);
                cookie.Values.Add("username", HttpUtility.UrlEncode(username, Encoding.GetEncoding("gb2312")));
                cookie.Values.Add("realname", HttpUtility.UrlEncode(realname, Encoding.GetEncoding("gb2312")));
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
        }

        void ClearCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Values.Clear();
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
        }
    }
}