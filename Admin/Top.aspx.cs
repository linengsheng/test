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
using System.Text;
using WebSystem.Base;

namespace WebSystem.Admin
{
    public partial class Top : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                var now = DateTime.Now;
                string timeTxt = "您好！";
                //if (now.Hour >= 5 && now.Hour < 12)
                //{
                //    timeTxt += "上午好！";
                //}
                //else if (now.Hour >= 12 && now.Hour <= 18)
                //{
                //    timeTxt += "下午好！";
                //}
                //else
                //{
                //    timeTxt += "晚上好！";
                //}
                string strWeek = "星期";
                switch (now.DayOfWeek)
                {
                    case DayOfWeek.Sunday: strWeek += "日"; break;
                    case DayOfWeek.Monday: strWeek += "一"; break;
                    case DayOfWeek.Tuesday: strWeek += "二"; break;
                    case DayOfWeek.Wednesday: strWeek += "三"; break;
                    case DayOfWeek.Thursday: strWeek += "四"; break;
                    case DayOfWeek.Friday: strWeek += "五"; break;
                    case DayOfWeek.Saturday: strWeek += "六"; break;
                }
                timeTxt += " 今天是" + now.ToString("yyyy年MM月dd日") + "  " + strWeek;
                lbTime.Text = timeTxt;
                lbUserName.Text = HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312"));
            }
            else
            {
                AlertTop("登录失败或登录已过期！请重新登录！", "Login.aspx");
            }
        }

        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Values.Clear();
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
            Response.Write("<script language='javascript'>top.document.location.href='" + ResolveUrl("../Default.aspx") + "';</script>");
        }
    }
}
