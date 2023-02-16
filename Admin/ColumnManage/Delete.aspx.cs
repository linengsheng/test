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
    public partial class Delete : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRight();
                InitPage();
            }
            Response.Redirect("Index.aspx");
        }

        void SetRight()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                if (!TRight.Select(cookie.Values["role_id"], "column", "delete"))
                {
                    AlertTop("您没有该权限！", "../Default.aspx");
                }
            }
        }

        void InitPage()
        {
            if (Request.QueryString["id"] != null)
            {
                string Column_Name = "";
                DataTable dt = TColumn.SelectByID(Request.QueryString["id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Column_Name = dt.Rows[0]["Column_Name"].ToString();
                }
                if (TColumn.Delete(Request.QueryString["id"]))
                {
                    TSysUserOperatelog.Add("删除栏目[" + Column_Name + "]");
                    Alert("删除成功！", "Index.aspx");
                }
                else
                {
                    Alert("删除失败！", "Index.aspx");
                }
            }
        }
    }
}