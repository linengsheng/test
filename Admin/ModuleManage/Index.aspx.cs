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
                btnAdd.Visible = TRight.Select(cookie.Values["role_id"], "module", "add");
                btnActionKey.Visible = TRight.Select(cookie.Values["role_id"], "module", "add");
                btnModule.Visible = TRight.Select(cookie.Values["role_id"], "module", "add");
            }
            else
            {
                btnAdd.Visible = false;
                btnActionKey.Visible = false;
                btnModule.Visible = false;
            }
        }

        void Bind()
        {
            try
            {
                DataTable dt = TModule.Select();
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
        
        int row = 0;

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
                LinkButton btnAction = (LinkButton)e.Row.FindControl("btnAction");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");

                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    if (btnAction != null)
                    {
                        btnAction.Visible = TRight.Select(cookie.Values["role_id"], "module", "setaction");
                    }
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
                    btnAction.Visible = false;
                    btnEdit.Visible = false;
                    btnDel.Visible = false;
                }

                //合并上级模块
                int rowindex = e.Row.RowIndex;
                if (rowindex - 1 < 0) return;
                //合并第一级分类
                if (e.Row.Cells[0].Text == gvData.Rows[rowindex - 1].Cells[0].Text)
                {
                    if (gvData.Rows[row].Cells[0].RowSpan == 0)
                    {
                        gvData.Rows[row].Cells[0].RowSpan++;
                    }
                    gvData.Rows[row].Cells[0].RowSpan++;
                    gvData.Rows[row].Cells[0].CssClass = "evenRowClass";
                    e.Row.Cells[0].Visible = false;
                }
                else
                {
                    e.Row.Cells[0].CssClass = "evenRowClass";
                    row = rowindex;
                }
            }
        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModuleEdit.aspx?cmd=add");
        }

        protected void btnModule_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index2.aspx");
        }

        protected void btnActionKey_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActionKey.aspx");
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string module_id = e.CommandArgument.ToString();
            Response.Redirect("ModuleEdit.aspx?cmd=edit&id=" + module_id);
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            string module_id = e.CommandArgument.ToString();
            Response.Redirect("ModuleAction.aspx?id=" + module_id);
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string module_id = e.CommandArgument.ToString();
            string module_name = "";
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

        public string getActionKeyNames(string module_id)
        {
            string result = "";
            DataTable dt = TModule.GetActionKeyNames(module_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (result.Trim() == "")
                    {
                        result = dt.Rows[i]["actionkey_name"].ToString();
                    }
                    else
                    {
                        result += "," + dt.Rows[i]["actionkey_name"].ToString();
                    }
                }
            }
            return result;
        }
    }
}