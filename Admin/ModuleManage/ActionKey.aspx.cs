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
    public partial class ActionKey : BasePage
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
                btnAdd.Visible = TRight.Select(cookie.Values["role_id"], "module", "add");
            }
            else
            {
                btnAdd.Visible = false;
            }
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

                DataTable dt = Pager.DoPager("t_actionkey", "ActionKey_ID,ActionKey_Name,ActionKey_Code,SortNO", "sortno ", sWhere, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out totalRecordCount);
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
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");

                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    if (btnEdit != null)
                    {
                        btnEdit.Visible = TRight.Select(cookie.Values["role_id"], "module", "edit");
                    }
                    if (btnDel != null)
                    {
                        btnDel.Visible = TRight.Select(cookie.Values["role_id"], "module", "delete");
                    }
                }
                else
                {
                    btnEdit.Visible = false;
                    btnDel.Visible = false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActionKeyEdit.aspx?cmd=add");
        }
        
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string ActionKey_id = e.CommandArgument.ToString();
            Response.Redirect("ActionKeyEdit.aspx?cmd=edit&id=" + ActionKey_id);
        }
        
        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string ActionKey_id = e.CommandArgument.ToString();
            string ActionKey_Name = "";
            DataTable dt = TActionKey.SelectByID(ActionKey_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                ActionKey_Name = dt.Rows[0]["ActionKey_Name"].ToString();
            }
            if (TActionKey.Delete(ActionKey_id))
            {
                TSysUserOperatelog.Add("删除功能操作信息[" + ActionKey_Name + "]");
                Alert("删除成功！");
                Bind();
            }
            else
            {
                Alert("删除失败！");
            }
        }
    }
}