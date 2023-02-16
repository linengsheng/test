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

namespace WebSystem.Admin.SysUserManage
{
    public partial class SetPwd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRight();
                Bind();
            }
        }

        void SetRight()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                if (!TRight.Select(cookie.Values["role_id"], "sysuser", "setpassword"))
                {
                    AlertTop("您没有该权限！", "../Default.aspx");
                }
            }
        }

        void Bind()
        {
            if (Request.QueryString["id"] != null)
            {
                DataTable dt = TSysUser.Select(Request.QueryString["id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lbusername.Text = dt.Rows[0]["username"].ToString();
                    lbrealname.Text = dt.Rows[0]["realname"].ToString();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string userid = Request.QueryString["id"];
                string userpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtuserpwd.Text, "MD5").ToLower();
                if (TSysUser.UpdatePwd(userid, userpwd))
                {
                    TSysUserOperatelog.Add("设置用户密码[" + lbusername.Text + "]");
                    Alert("提交成功！", "Index.aspx");
                }
                else
                {
                    Alert("提交失败！");
                }
            }
        }
    }
}
