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
    public partial class Edit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRight();
                InitPage();
            }
        }

        void SetRight()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "sysuser", "add"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
                else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "sysuser", "edit"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
            }
        }

        void InitPage()
        {
            ddlrole.DataSource = TRole.Select();
            ddlrole.DataTextField = "role_name";
            ddlrole.DataValueField = "role_id";
            ddlrole.DataBind();

            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                lbType.Text = "添加系统用户";
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                lbType.Text = "编辑系统用户";
                if (Request.QueryString["id"] != null)
                {
                    DataTable dt = TSysUser.Select(Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtuserid.Text = dt.Rows[0]["username"].ToString();
                        txtusername.Text = dt.Rows[0]["realname"].ToString();                        
                        ddlrole.SelectedValue = dt.Rows[0]["role_id"].ToString();
                        txtuserid.Enabled = false;
                        trpwd.Visible = false;
                        trpwd2.Visible = false;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //是否存在相同用户名
                string username = txtuserid.Text.Trim().ToLower();
                DataTable dt = TSysUser.SelectByUsername(username);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Alert("用户名已经存在！请重新输入！");
                    txtuserid.Focus();
                    return;
                }
                string userpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtuserpwd.Text, "MD5").ToLower();
                if (TSysUser.Add(txtuserid.Text.Trim().ToLower(), txtusername.Text.Trim(), userpwd, ddlrole.SelectedValue))
                {
                    TSysUserOperatelog.Add("添加用户信息[" + txtusername.Text.Trim() + "]");
                    Alert("提交成功！", "Index.aspx");
                }
                else
                {
                    Alert("提交失败！");
                }
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                if (Request.QueryString["id"] != null)
                {
                    if (TSysUser.Edit(Request.QueryString["id"], txtusername.Text.Trim().ToLower(), ddlrole.SelectedValue))
                    {
                        TSysUserOperatelog.Add("编辑用户信息[" + txtusername.Text.Trim() + "]");
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
}
