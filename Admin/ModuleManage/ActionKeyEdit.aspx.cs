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
    public partial class ActionKeyEdit : BasePage
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
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                lbType.Text = "添加功能操作关键字";
                //获取下一个排序
                txtSortNO.Text = TActionKey.SelectNextSortNO();
            }
            else if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "edit")
            {
                lbType.Text = "编辑功能操作关键字";
                if (Request.QueryString["id"] != null)
                {
                    DataTable dt = TActionKey.SelectByID(Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtactionkeyname.Text = dt.Rows[0]["actionkey_name"].ToString();
                        txtactionkeycode.Text = dt.Rows[0]["actionkey_code"].ToString();
                        txtSortNO.Text = dt.Rows[0]["SortNO"].ToString();
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cmd"] != null && Request.QueryString["cmd"].ToString() == "add")
            {
                //是否存在相同功能名称
                string actionkeyname = txtactionkeyname.Text.Trim().ToLower();
                DataTable dt = TActionKey.SelectByName(actionkeyname);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Alert("功能名称已经存在！请重新输入！");
                    txtactionkeyname.Focus();
                    return;
                }
                //是否存在相同功能标识码
                string actionkeycode = txtactionkeycode.Text.Trim().ToLower();
                DataTable dt2 = TActionKey.SelectByCode(actionkeycode);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    Alert("功能标识码已经存在！请重新输入！");
                    txtactionkeycode.Focus();
                    return;
                }
                if (TActionKey.Add(txtactionkeyname.Text.Trim(), txtactionkeycode.Text.Trim(), txtSortNO.Text.Trim()))
                {
                    TSysUserOperatelog.Add("添加功能操作关键字[" + txtactionkeyname.Text.Trim() + "]");
                    Alert("提交成功！", "ActionKey.aspx");
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
                    //是否存在相同功能名称
                    string actionkeyname = txtactionkeyname.Text.Trim().ToLower();
                    DataTable dt = TActionKey.SelectByName(actionkeyname, Request.QueryString["id"]);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Alert("功能名称已经存在！请重新输入！");
                        txtactionkeyname.Focus();
                        return;
                    }
                    //是否存在相同功能标识码
                    string actionkeycode = txtactionkeycode.Text.Trim().ToLower();
                    DataTable dt2 = TActionKey.SelectByCode(actionkeycode, Request.QueryString["id"]);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        Alert("功能标识码已经存在！请重新输入！");
                        txtactionkeycode.Focus();
                        return;
                    }
                    if (TActionKey.Edit(Request.QueryString["id"], txtactionkeyname.Text.Trim(), txtactionkeycode.Text.Trim(), txtSortNO.Text.Trim()))
                    {
                        TSysUserOperatelog.Add("编辑功能操作关键字[" + txtactionkeyname.Text.Trim() + "]");
                        Alert("提交成功！", "ActionKey.aspx");
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