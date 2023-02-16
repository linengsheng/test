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
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRight();
                InitPage();
                Bind();
            }
        }

        void SetRight()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                btnAdd.Visible = TRight.Select(cookie.Values["role_id"], "sysuser", "add");
            }
            else
            {
                btnAdd.Visible = false;
            }
        }

        void InitPage()
        {
            ddlrole.DataSource = TRole.Select();
            ddlrole.DataTextField = "role_name";
            ddlrole.DataValueField = "role_id";
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, new ListItem("=全部=", ""));
        }

        void Bind()
        {
            try
            {
                while (AspNetPager1.CurrentPageIndex > AspNetPager1.PageCount)
                {
                    AspNetPager1.CurrentPageIndex--;
                }
                int totalRecordCount = 0;
                string sWhere = "";
                sWhere = " username like '%" + txtuserid.Text.Trim() + "%' ";
                sWhere += " and realname like '%" + txtusername.Text.Trim() + "%' ";
                if (ddlrole.SelectedIndex != 0)
                {
                    sWhere += " and role_id ='" + ddlrole.SelectedValue + "' ";
                }
                if (ddlstate.SelectedIndex != 0)
                {
                    sWhere += " and state ='" + ddlstate.SelectedValue + "' ";
                }
                string refields = "SysUser_ID,UserName,RealName,Role_ID,Role_Name,State,Add_Time";
                DataTable dt = Pager.DoPager("v_sysuser", refields, "add_time desc ", sWhere, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out totalRecordCount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvData.DataSource = dt;
                    AspNetPager1.RecordCount = totalRecordCount;
                    AspNetPager1.TextBeforeInputBox = string.Format("共计{0}条{1}页/每页" + AspNetPager1.PageSize + "条&nbsp;", AspNetPager1.RecordCount, AspNetPager1.PageCount);
                    gvData.DataBind();
                }
                else
                {
                    gvData.DataSource = null;
                    AspNetPager1.RecordCount = 0;
                    AspNetPager1.TextBeforeInputBox = string.Format("共计{0}条{1}页/每页" + AspNetPager1.PageSize + "条&nbsp;", AspNetPager1.RecordCount, AspNetPager1.PageCount);
                    gvData.DataBind();
                }
            }
            catch
            {
                gvData.DataSource = null;
                AspNetPager1.RecordCount = 0;
                AspNetPager1.TextBeforeInputBox = string.Format("共计{0}条{1}页/每页" + AspNetPager1.PageSize + "条&nbsp;", AspNetPager1.RecordCount, AspNetPager1.PageCount);
                gvData.DataBind();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 != 0)
                {
                    e.Row.CssClass = "evenRowClass";
                }
                string cc = e.Row.CssClass;
                e.Row.Attributes.Add("onmouseover", "this.className='activeRowClass';");
                e.Row.Attributes.Add("onmouseout", "this.className='" + cc + "';");
                                
                //权限
                LinkButton btnPwd = (LinkButton)e.Row.FindControl("btnPwd");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");
                CheckBox cbstate = (CheckBox)e.Row.FindControl("cbstate");

                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    if (btnPwd != null)
                    {
                        btnPwd.Visible = TRight.Select(cookie.Values["role_id"], "sysuser","setpassword");
                    }
                    if (btnEdit != null)
                    {
                        btnEdit.Visible = TRight.Select(cookie.Values["role_id"], "sysuser","edit");
                    }
                    if (btnDel != null)
                    {
                        btnDel.Visible = TRight.Select(cookie.Values["role_id"], "sysuser","delete");
                    }
                    if (cbstate != null)
                    {
                        cbstate.Enabled = TRight.Select(cookie.Values["role_id"], "sysuser","enable");
                    }
                }
                else
                {
                    btnPwd.Visible = false;
                    btnEdit.Visible = false;
                    btnDel.Visible = false;
                    cbstate.Enabled = false;
                }

                //管理员帐号不能禁用和删除
                if (e.Row.Cells[0].Text.Trim().IndexOf("admin") >= 0)
                {
                    cbstate.Visible = false;
                    btnDel.Visible = false;
                    btnEdit.Visible = false;
                    btnPwd.Visible = false;
                    e.Row.Cells[6].Text = "<p style='color:red;'>管理员帐号不能禁用和删除</p>";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Bind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx?cmd=add");
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string userid = e.CommandArgument.ToString();
            Response.Redirect("Edit.aspx?cmd=edit&id=" + userid);
        }

        protected void btnPwd_Command(object sender, CommandEventArgs e)
        {
            string userid = e.CommandArgument.ToString();
            Response.Redirect("SetPwd.aspx?id=" + userid);
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string userid = e.CommandArgument.ToString();
            string realname = "";
            DataTable dt = TSysUser.Select(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                realname = dt.Rows[0]["realname"].ToString();
            }
            if (TSysUser.Delete(userid))
            {
                TSysUserOperatelog.Add("删除系统用户信息[" + realname + "]");
                Alert("删除成功！");
                Bind();
            }
            else
            {
                Alert("删除失败！");
            }
        }

        protected void cbstate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbstate = (CheckBox)sender;
            string userid = cbstate.Attributes["userid"];
            string realname = "";
            DataTable dt = TSysUser.Select(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                realname = dt.Rows[0]["realname"].ToString();
            }
            if (TSysUser.UpdateState(userid, cbstate.Checked ? "1" : "0"))
            {
                TSysUserOperatelog.Add("更改系统用户状态[" + realname + "]");
                Alert("更改状态成功！");
                Bind();
            }
            else
            {
                Alert("更改状态失败！");
            }
        }

        public string getLastLoginTime(string sysuser_id)
        {
            string result = "";
            DataTable dt = TSysUser.SelectLastLoginTime(sysuser_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = Convert.ToDateTime(dt.Rows[0]["Login_Time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Bind();
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Bind();
        }
    }
}

