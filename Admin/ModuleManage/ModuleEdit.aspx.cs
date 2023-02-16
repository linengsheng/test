using WebSystem.Base;
using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSystem.Admin.ModuleManage
{
    public partial class ModuleEdit : BasePage
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
            string cmdkey = Request.QueryString["cmd"];
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                if (cmdkey == "add")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "module", "add"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
                else if (cmdkey == "edit")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "module", "edit"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
            }
        }

        void InitPage()
        {
            ddlModule.DataSource = TModule.SelectByParentid("0");
            ddlModule.DataValueField = "module_id";
            ddlModule.DataTextField = "module_name";
            ddlModule.DataBind();

            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() == "0")
            {
                ddlModule.Items.Insert(0, new ListItem("", "0"));
                ddlModule.SelectedIndex = 0;
                ddlModule.Enabled = false;
            }

            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                lbType.Text = "添加模块";
                //获取下一个排序
                txtSortNO.Text = TModule.SelectNextSortNO(ddlModule.SelectedValue);
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                lbType.Text = "编辑模块";
                if (Request.QueryString["id"] != null)
                {
                    DataTable dt = TModule.SelectByID(Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ddlModule.SelectedValue = dt.Rows[0]["parent_id"].ToString();
                        txtmodulename.Text = dt.Rows[0]["module_name"].ToString();
                        txtmodulecode.Text = dt.Rows[0]["module_code"].ToString();
                        txtSortNO.Text = dt.Rows[0]["SortNO"].ToString();
                        txtmoduleurl.Text = dt.Rows[0]["module_url"].ToString();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //是否存在相同模块名称
                string modulename = txtmodulename.Text.Trim().ToLower();
                DataTable dt = TModule.SelectByName(modulename);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Alert("功能名称已经存在！请重新输入！");
                    txtmodulename.Focus();
                    return;
                }
                //是否存在相同模块标识码
                string modulecode = txtmodulecode.Text.Trim().ToLower();
                DataTable dt2 = TModule.SelectByCode(modulecode);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    Alert("功能标识码已经存在！请重新输入！");
                    txtmodulecode.Focus();
                    return;
                }
                if (TModule.Add(ddlModule.SelectedValue, txtmodulename.Text.Trim(), txtmodulecode.Text.Trim(), txtSortNO.Text.Trim(), txtmoduleurl.Text.Trim()))
                {
                    TSysUserOperatelog.Add("添加模块[" + txtmodulename.Text.Trim() + "]");
                    if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() == "0")
                    {
                        Alert("提交成功！", "Index2.aspx");
                    }
                    else
                    {
                        Alert("提交成功！", "Index.aspx");
                    }
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
                    //是否存在相同模块名称
                    string modulename = txtmodulename.Text.Trim().ToLower();
                    DataTable dt = TModule.SelectByName(modulename, Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Alert("功能名称已经存在！请重新输入！");
                        txtmodulename.Focus();
                        return;
                    }
                    if (TModule.Edit(Request.QueryString["id"], ddlModule.SelectedValue, txtmodulename.Text.Trim(), txtmodulecode.Text.Trim(), txtSortNO.Text.Trim(), txtmoduleurl.Text.Trim()))
                    {
                        TSysUserOperatelog.Add("编辑模块[" + txtmodulename.Text.Trim() + "]");
                        if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() == "0")
                        {
                            Alert("提交成功！", "Index2.aspx");
                        }
                        else
                        {
                            Alert("提交成功！", "Index.aspx");
                        }
                    }
                    else
                    {
                        Alert("提交失败！");
                    }
                }
            }
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //重新获取对应模块下一个排序
                txtSortNO.Text = TModule.SelectNextSortNO(ddlModule.SelectedValue);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() == "0")
            {
                Response.Redirect("Index2.aspx");
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
}