using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin.LaborReceiptManage
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        //初始化绑定数据
        void Bind()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    while (AspNetPager1.CurrentPageIndex > AspNetPager1.PageCount)
                    {
                        AspNetPager1.CurrentPageIndex--;
                    }

                    int totalRecordCount = 0;
                    string searchSql = "";

                    searchSql += " AND OperateUser='"+ HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")) + "' ";

                    if (!string.IsNullOrEmpty(txtId.Text.Trim()))
                    {
                        searchSql += " AND Id='" + txtId.Text.Trim() + "' ";
                    }
                    if (!string.IsNullOrEmpty(txtPicName.Text.Trim()))
                    {
                        searchSql += " AND PicName like '%" + txtPicName.Text.Trim() + "%' ";
                    }
                    if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                    {
                        searchSql += " AND UserName like '%" + txtUserName.Text.Trim() + "%' ";
                    }
                    if (!string.IsNullOrEmpty(txtAmount.Text.Trim()))
                    {
                        searchSql += " AND Amount='" + txtAmount.Text.Trim() + "' ";
                    }
                    if (!string.IsNullOrEmpty(txtLaborCompany.Text.Trim()))
                    {
                        searchSql += " AND LaborCompany like '%" + txtLaborCompany.Text.Trim() + "%' ";
                    }
                    if (!string.IsNullOrEmpty(txtOperateUser.Text.Trim()))
                    {
                        searchSql += " AND OperateUser='" + txtOperateUser.Text.Trim() + "' ";
                    }

                    DataTable dt = TLaborReceipt.SelectPage(searchSql, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out totalRecordCount);
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
            }
            catch(Exception exp)
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

        //行间样式
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
            }
        }

        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Bind();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDels_Click(object sender, EventArgs e)
        {
            string ids = GetChecked();
            if (!string.IsNullOrEmpty(ids))
            {
                string[] idList = ids.Split(',');
                foreach (string id in idList)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                    if (null != cookie)
                    {
                        DataTable dt = TLaborReceipt.SelectById(id);
                        if (dt != null)
                        {
                            if (TLaborReceipt.Delete(id))
                            {
                                TLaborReceipt.AddLog(dt.Rows[0]["PicName"].ToString(), "删除", HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")));
                            }
                        }
                    }
                }
                Alert("操作完成！");
                Bind();
            }
            else
            {
                Alert("请先选择要删除的记录！");
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
                        ids += string.IsNullOrEmpty(ids) ? gvData.DataKeys[i].Value.ToString() : @"," + gvData.DataKeys[i].Value.ToString();
                    }
                }
            }
            return ids;
        }

        //上传
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                string sysuser_id = cookie.Values["sysuser_id"];
                TLaborReceipt.DelUpMsg(sysuser_id);
            }

            Response.Redirect("Upload2.aspx");
        }

        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}