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
    public partial class Index : BasePage
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
                btnAdd.Visible = TRight.Select(cookie.Values["role_id"], "Role", "add");
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
                DataTable dt = TRole.Select();
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvData.DataSource = dt;
                    gvData.DataBind();
                }
                else
                {
                    gvData.DataSource = null;
                    gvData.DataBind();
                }
            }
            catch
            {
                gvData.DataSource = null;
                gvData.DataBind();
            }
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
                LinkButton btnRight = (LinkButton)e.Row.FindControl("btnRight");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");

                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    if (btnRight != null)
                    {
                        btnRight.Visible = TRight.Select(cookie.Values["role_id"], "role", "setright");
                    }
                    if (btnEdit != null)
                    {
                        btnEdit.Visible = TRight.Select(cookie.Values["role_id"], "role", "edit");
                    }
                    if (btnDel != null)
                    {
                        btnDel.Visible = TRight.Select(cookie.Values["role_id"], "role", "delete");
                    }
                }
                else
                {
                    btnRight.Visible = false;
                    btnEdit.Visible = false;
                    btnDel.Visible = false;                    
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx?cmd=add");
        }

        protected void btnRight_Command(object sender, CommandEventArgs e)
        {
            string role_id = e.CommandArgument.ToString();
            Response.Redirect("Right2.aspx?id=" + role_id);
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string role_id = e.CommandArgument.ToString();
            Response.Redirect("Edit.aspx?cmd=edit&id=" + role_id);
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string role_id = e.CommandArgument.ToString();
            string role_name = "";
            DataTable dt = TRole.Select(role_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                role_name = dt.Rows[0]["role_name"].ToString();
            }
            if (TRole.Delete(role_id))
            {
                TSysUserOperatelog.Add("删除角色信息[" + role_name + "]");
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
