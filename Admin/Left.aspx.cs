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
using System.Text;
using KvanitWS.DataAccess;

namespace WebSystem.Admin
{
    public partial class Left : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindModule();
            }
        }

        void BindModule()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                ltlModule.Text = "";
                //父模块
                DataTable dt = TModule.SelectByParentid("0");
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //是否有下级模块权限
                        bool isright = false;
                        string modulelist = "";
                        DataTable dt2 = TModule.SelectByParentid(dt.Rows[i]["module_id"].ToString());
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            {
                                if (TRight.Select(cookie.Values["role_id"], dt2.Rows[j]["module_code"].ToString(), "view"))
                                {
                                    modulelist += "<div class='divList'><a href='" + dt2.Rows[j]["module_url"].ToString() + "' target='rightFrame'>" + dt2.Rows[j]["module_name"].ToString() + "</a></div>";
                                    isright = true;
                                }
                            }
                        }

                        if (isright)
                        {
                            ltlModule.Text += "<div class='divTitle'>" + dt.Rows[i]["module_name"].ToString() + "</div>";
                            ltlModule.Text += modulelist;
                        }
                    }
                }
            }
        }
    }
}
