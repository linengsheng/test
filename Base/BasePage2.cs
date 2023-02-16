using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebSystem.Base
{
    public class BasePage2 : System.Web.UI.Page
    {
        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="msg">提示信息</param>
        protected void Alert(object msg)
        {
            ClientScriptManager cs = this.Page.ClientScript;
            Random rand = new Random();
            string script = "<script type='text/javascript'>alert('{0}');</script>";
            script = string.Format(script, msg != null ? msg.ToString() : string.Empty);
            cs.RegisterStartupScript(this.GetType(), this.ClientID + rand.Next(100000), script);
        }

        /// <summary>
        /// 弹出提示框并转到顶页
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">转到页面</param>
        protected void AlertTop(object msg, string url)
        {
            ClientScriptManager cs = this.Page.ClientScript;
            Random rand = new Random();
            string script = "<script type='text/javascript'>alert('{0}');top.document.location.href='{1}';</script>";
            script = string.Format(script, msg != null ? msg.ToString() : string.Empty, url);
            cs.RegisterStartupScript(this.GetType(), this.ClientID + rand.Next(100000), script);
        }

        /// <summary>
        /// 弹出提示框并转到另一页
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">转到页面</param>
        protected void Alert(object msg, string url)
        {
            ClientScriptManager cs = this.Page.ClientScript;
            Random rand = new Random();
            string script = "<script type='text/javascript'>alert('{0}');document.location.href='{1}';</script>";
            script = string.Format(script, msg != null ? msg.ToString() : string.Empty, url);
            cs.RegisterStartupScript(this.GetType(), this.ClientID + rand.Next(100000), script);
        }
    }
}
