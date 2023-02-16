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

namespace WebSystem.Admin.LoginlogManage
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
                btnDels.Visible = TRight.Select(cookie.Values["role_id"], "sysuserloginlog", "delete");
            }
            else
            {
                btnDels.Visible = false;
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

                DateTime beginDate;
                if (txtBeginDate.Text.Trim() == "" || !DateTime.TryParse(txtBeginDate.Text.Trim(), out beginDate))
                {
                    beginDate = DateTime.Parse("1901-01-01");
                }
                else
                {
                    beginDate = DateTime.Parse(txtBeginDate.Text.Trim());
                }
                DateTime endDate;
                if (txtEndDate.Text.Trim() == "" || !DateTime.TryParse(txtEndDate.Text.Trim(), out endDate))
                {
                    endDate = DateTime.Now.AddDays(1);
                }
                else
                {
                    endDate = DateTime.Parse(txtEndDate.Text.Trim()).AddDays(1);
                }
                string sWhere = "";
                sWhere += " (username like '%" + txtUser.Text.Trim() + "%' or realname like '%" + txtUser.Text.Trim() + "%') ";
                sWhere += " and login_time >='" + beginDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and login_time <='" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                int totalRecordCount = 0;
                string refields = "sysuserloginlog_id,sysUser_ID,username,realname,Login_IP,Login_Time";
                DataTable dt = Pager.DoPager("v_sysuserloginlog", refields, "login_time desc ", sWhere, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out totalRecordCount);
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
                LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");
                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    if (btnDel != null)
                    {
                        btnDel.Visible = TRight.Select(cookie.Values["role_id"], "sysuserloginlog", "delete");
                    }
                }
                else
                {
                    btnDel.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Bind();
        }

        protected void btnDels_Click(object sender, EventArgs e)
        {
            //批量删除
            string ids = GetChecked();
            if (!string.IsNullOrEmpty(ids))
            {
                if (TLoginlog.DeletesFromSysUser(ids))
                {
                    TSysUserOperatelog.Add("批量删除系统登录日志");
                    Alert("批量删除成功！");
                    AspNetPager1.CurrentPageIndex--;
                    Bind();
                }
                else
                {
                    Alert("批量删除失败！");
                }
            }
            else
            {
                Alert("请先选择要删除的记录！");
            }
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            string loginlogid = e.CommandArgument.ToString();
            if (TLoginlog.DeleteFromSysUser(loginlogid))
            {
                TSysUserOperatelog.Add("删除系统登录日志[" + loginlogid + "]");
                Alert("删除成功！");
                Bind();
            }
            else
            {
                Alert("删除失败！");
            }
        }

        //获取选择项
        protected string GetChecked()
        {
            string ids = string.Empty;
            CheckBox cb;
            for (int i = 0; i <= gvData.Rows.Count - 1; i++)
            {
                if (gvData.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    cb = (CheckBox)gvData.Rows[i].FindControl("cbSelected");
                    if (cb != null && cb.Checked)
                    {
                        ids += string.IsNullOrEmpty(ids) ? "'" + gvData.DataKeys[i].Value.ToString() + "'" : @",'" + gvData.DataKeys[i].Value.ToString() + "'";
                    }
                }
            }
            return ids;
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime beginDate;
                if (txtBeginDate.Text.Trim() == "" || !DateTime.TryParse(txtBeginDate.Text.Trim(), out beginDate))
                {
                    beginDate = DateTime.Parse("1901-01-01");
                }
                else
                {
                    beginDate = DateTime.Parse(txtBeginDate.Text.Trim());
                }
                DateTime endDate;
                if (txtEndDate.Text.Trim() == "" || !DateTime.TryParse(txtEndDate.Text.Trim(), out endDate))
                {
                    endDate = DateTime.Now.AddDays(1);
                }
                else
                {
                    endDate = DateTime.Parse(txtEndDate.Text.Trim()).AddDays(1);
                }
                string sWhere = "";
                sWhere += " (username like '%" + txtUser.Text.Trim() + "%' or realname like '%" + txtUser.Text.Trim() + "%') ";
                sWhere += " and login_time >='" + beginDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and login_time <='" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                DataTable dt = TLoginlog.SelectOutSysUser(sWhere);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvOut.DataSource = dt;
                    gvOut.DataBind();
                }
                else
                {
                    Alert("没有数据！");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                //设置输出文件名
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("系统用户登录日志[" + DateTime.Now.ToString("yyyy-MM-dd_HH：mm：ss") + "]", System.Text.Encoding.UTF8) + ".xls");
                //设置输出流为简体中文
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                //设置输出文件类型为excel文件
                //可设为其它文件类型，修改ms-excel即可，如word文档为ms-word，同时修改输出文件名后缀为相应类型，如word为.doc
                Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                gvOut.RenderControl(oHtmlTextWriter);
                Response.Write("<meta http-equiv='content-type' content='application/ms-excel; charset=utf-8'/>" + oStringWriter.ToString());
                Response.End();
            }
            catch
            {
                Alert("导出失败！");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}

