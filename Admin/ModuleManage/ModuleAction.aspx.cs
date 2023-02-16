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
    public partial class ModuleAction : BasePage
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
                if (!TRight.Select(cookie.Values["role_id"], "module", "setaction"))
                {
                    AlertTop("您没有该权限！", "../Default.aspx");
                }
            }
        }

        void InitPage()
        {
            if (Request.QueryString["id"] != null)
            {
                DataTable dt = TModule.SelectByID(Request.QueryString["id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lbModulename.Text = dt.Rows[0]["Module_name"].ToString();
                }
                //加载功能操作
                cblModuleaction.DataSource = TActionKey.Select();
                cblModuleaction.DataTextField = "actionkey_name";
                cblModuleaction.DataValueField = "actionkey_id";
                cblModuleaction.DataBind();
                DataTable dt2 = TModule.GetActionKeyNames(Request.QueryString["id"]);
                //勾选模块已有功能
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < cblModuleaction.Items.Count; i++)
                    {
                        DataTable dt3=TModuleAction.Select(Request.QueryString["id"],cblModuleaction.Items[i].Value);
                        if (dt3 != null && dt3.Rows.Count > 0)
                        {
                            cblModuleaction.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                //删除原有模块功能记录
                TModuleAction.Delete(Request.QueryString["id"]);
                //添加勾选的功能
                for (int i = 0; i < cblModuleaction.Items.Count; i++)
                {
                    if (cblModuleaction.Items[i].Selected)
                    {
                        TModuleAction.Add(Request.QueryString["id"], cblModuleaction.Items[i].Value);
                    }
                }
                Alert("提交完成！", "Index.aspx");
            }
        }
    }
}