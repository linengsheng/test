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

namespace WebSystem.Admin.RoleManage
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
                    if (!TRight.Select(cookie.Values["role_id"], "role", "add"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
                else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "role", "edit"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
            }
        }

        void InitPage()
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                lbType.Text = "添加角色";
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                lbType.Text = "编辑角色";
                if (Request.QueryString["id"] != null)
                {
                    DataTable dt = TRole.Select(Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtrolename.Text = dt.Rows[0]["role_name"].ToString();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //是否存在相同名称
                if (!string.IsNullOrEmpty(txtrolename.Text.Trim()))
                {
                    DataTable dt = TRole.SelectByName(txtrolename.Text.Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Alert("该名称已经存在！");
                        txtrolename.Focus();
                        return;
                    }
                }
                if (TRole.Add(txtrolename.Text.Trim()))
                {
                    TSysUserOperatelog.Add("添加角色信息[" + txtrolename.Text.Trim() + "]");
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
                    //是否存在相同名称
                    if (!string.IsNullOrEmpty(txtrolename.Text.Trim()))
                    {
                        DataTable dt = TRole.SelectByName(txtrolename.Text.Trim(), Request.QueryString["id"]);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Alert("该名称已经存在！");
                            txtrolename.Focus();
                            return;
                        }
                    }
                    if (TRole.Edit(Request.QueryString["id"], txtrolename.Text.Trim()))
                    {
                        TSysUserOperatelog.Add("编辑角色信息[" + txtrolename.Text.Trim() + "]");
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
