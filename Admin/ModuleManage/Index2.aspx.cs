using KvanitWS.DataAccess;
using WebSystem.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSystem.Admin.ModuleManage
{
    public partial class Index2 : BasePage
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
                DataTable dt = TModule.SelectByParentid("0");
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
            Response.Redirect("ModuleEdit.aspx?cmd=add&pid=0");
        }
        
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string module_id = e.CommandArgument.ToString();
            Response.Redirect("ModuleEdit.aspx?cmd=edit&pid=0&id=" + module_id);
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string module_id = e.CommandArgument.ToString();
            string module_name = "";
            DataTable dt2 = TModule.SelectByParentid(module_id);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                Alert("该模块下面子模块，不能删除！");
                return;
            }
            DataTable dt = TModule.SelectByID(module_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                module_name = dt.Rows[0]["module_name"].ToString();
            }
            if (TModule.Delete(module_id))
            {
                TSysUserOperatelog.Add("删除模块信息[" + module_name + "]");
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