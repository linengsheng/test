using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebSystem.Base;
using KvanitWS.DataAccess;

namespace WebSystem.Admin
{
    public partial class ChangePwd : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        void Bind()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                DataTable dt = TSysUser.Select(cookie.Values["sysuser_id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lbuserid.Text = dt.Rows[0]["username"].ToString();
                    txtusername.Text = dt.Rows[0]["realname"].ToString();

                    trpwd1.Visible = cbPwd.Checked;
                    trpwd2.Visible = cbPwd.Checked;
                    trpwd3.Visible = cbPwd.Checked;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                string userid = cookie.Values["sysuser_id"];
                if (cbPwd.Checked)
                {
                    //旧密码是否正确
                    string userpwdold = FormsAuthentication.HashPasswordForStoringInConfigFile(txtuserpwdold.Text, "MD5").ToLower();
                    DataTable dt = TSysUser.SelectById(userid, userpwdold);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string userpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtuserpwd.Text, "MD5").ToLower();
                        if (TSysUser.UpdatePwd(userid, userpwd) && TSysUser.Edit(userid, txtusername.Text.Trim()))
                        {
                            TSysUserOperatelog.Add("修改个人信息[" + lbuserid.Text + "]");
                            ClearCookie();
                            AlertTop("提交成功！请重新登录！", "Login.aspx");
                        }
                        else
                        {
                            Alert("提交失败！");
                        }
                    }
                    else
                    {
                        Alert("旧密码输入错误！");
                        txtuserpwdold.Focus();
                    }
                }
                else
                {
                    if (TSysUser.Edit(userid, txtusername.Text.Trim()))
                    {
                        TSysUserOperatelog.Add("修改个人信息[" + lbuserid.Text + "]");
                        Alert("提交成功！");
                    }
                    else
                    {
                        Alert("提交失败！");
                    }
                }
            }
        }

        void ClearCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
        }

        protected void cbPwd_CheckedChanged(object sender, EventArgs e)
        {
            trpwd1.Visible = cbPwd.Checked;
            trpwd2.Visible = cbPwd.Checked;
            trpwd3.Visible = cbPwd.Checked;
        }
    }
}
