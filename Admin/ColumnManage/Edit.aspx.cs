using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin.ColumnManage
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
            string cmdkey = Request.QueryString["cmd"];
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                if (cmdkey == "add")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "column", "add"))
                    {
                        AlertTop("您没有该权限！", "../Default.aspx");
                    }
                }
                else if (cmdkey == "edit")
                {
                    if (!TRight.Select(cookie.Values["role_id"], "column", "edit"))
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
                lbType.Text = "添加栏目";
                //获取下一个排序
                txtSortNO.Text = TColumn.SelectNextSortNO(Request.QueryString["pid"]);
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                lbType.Text = "编辑栏目";
                if (Request.QueryString["id"] != null)
                {
                    DataTable dt = TColumn.SelectByID(Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtcolumnname.Text = dt.Rows[0]["column_name"].ToString();
                        txtSortNO.Text = dt.Rows[0]["SortNO"].ToString();
                        txtcolumnurl.Text = dt.Rows[0]["column_url"].ToString();
                        lbcolumnid.Text = "(栏目ID:" + Request.QueryString["id"] + ")";
                        DataTable dt3 = TColumn.SelectByID(dt.Rows[0]["parent_id"].ToString());
                        if (dt3 != null && dt3.Rows.Count > 0)
                        {
                            lbparentname.Text = dt3.Rows[0]["column_name"].ToString();
                        }
                        else
                        {
                            lbparentname.Text = "[无]";
                        }
                    }
                }
            }
            if (Request.QueryString["pid"] != null)
            {
                DataTable dt2 = TColumn.SelectByID(Request.QueryString["pid"]);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    lbparentname.Text = dt2.Rows[0]["column_name"].ToString();
                }
                else
                {
                    lbparentname.Text = "[无]";
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //是否存在相同栏目名称
                string columnname = txtcolumnname.Text.Trim().ToLower();
                DataTable dt = TColumn.SelectByName(columnname);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Alert("栏目名称已经存在！请重新输入！");
                    txtcolumnname.Focus();
                    return;
                }
                if (TColumn.Add(Request.QueryString["pid"], txtcolumnname.Text.Trim(), txtSortNO.Text.Trim(), txtcolumnurl.Text.Trim()))
                {
                    TSysUserOperatelog.Add("添加栏目[" + txtcolumnname.Text.Trim() + "]");
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
                    //是否存在相同栏目名称
                    string columnname = txtcolumnname.Text.Trim().ToLower();
                    DataTable dt = TColumn.SelectByName(columnname, Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Alert("栏目名称已经存在！请重新输入！");
                        txtcolumnname.Focus();
                        return;
                    }
                    if (TColumn.Edit(Request.QueryString["id"], txtcolumnname.Text.Trim(), txtSortNO.Text.Trim(), txtcolumnurl.Text.Trim()))
                    {
                        TSysUserOperatelog.Add("编辑栏目[" + txtcolumnname.Text.Trim() + "]");
                        Alert("提交成功！", "Index.aspx");
                    }
                    else
                    {
                        Alert("提交失败！");
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}