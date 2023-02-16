using KvanitWS.DataAccess;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin.LaborReceiptManage
{
    public partial class Result : BasePage
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
                    int SuccessCount = 0;
                    int ErrorCount = 0;
                    SuccessCount = TLaborReceipt.SelectUpMsgSuccessCount(cookie.Values["sysuser_id"]);
                    ErrorCount = TLaborReceipt.SelectUpMsgErrorCount(cookie.Values["sysuser_id"]);
                    lbMsg.Text = "成功" + SuccessCount.ToString() + "张，失败" + ErrorCount.ToString() + "张";

                    DataTable dt = TLaborReceipt.SelectUpMsg(cookie.Values["sysuser_id"], "0");
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
            }
            catch (Exception exp)
            {
                gvData.DataSource = null;
                gvData.DataBind();
            }
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

        //导出
        protected void btnOut_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                if (null != cookie)
                {
                    DataTable dt = TLaborReceipt.SelectUpMsg(cookie.Values["sysuser_id"], "0");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //创建空白excel文件
                        string downDir = "/download/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                        if (!Directory.Exists(Server.MapPath(downDir)))
                        {
                            Directory.CreateDirectory(Server.MapPath(downDir));
                        }

                        NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                        NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("Sheet1");
                        string fileName = "劳务费电子回单上传结果" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                        string path = Server.MapPath("\\download") + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + fileName;
                        FileStream fs = new FileStream(path, FileMode.Create);
                        workbook.Write(fs);
                        fs.Close();

                        Export(dt, fileName);
                    }
                    else
                    {
                        Alert("没有数据！");
                    }
                }

                //if (gvData.Rows.Count > 0)
                //{
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.Charset = "GB2312";
                //    //设置输出文件名
                //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("劳务费电子回单上传结果" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "]", System.Text.Encoding.UTF8) + ".xls");
                //    //设置输出流为简体中文
                //    Response.ContentEncoding = System.Text.Encoding.UTF8;
                //    //设置输出文件类型为excel文件
                //    //可设为其它文件类型，修改ms-excel即可，如word文档为ms-word，同时修改输出文件名后缀为相应类型，如word为.doc
                //    Response.ContentType = "application/ms-excel";
                //    this.EnableViewState = false;
                //    System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
                //    System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
                //    System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                //    gvData.RenderControl(oHtmlTextWriter);
                //    Response.Write("<meta http-equiv='content-type' content='application/ms-excel; charset=utf-8'/>" + oStringWriter.ToString());
                //    Response.End();
                //}
                //else
                //{
                //    Alert("没有数据！");
                //}
            }
            catch(Exception exp)
            {
                Alert(exp.Message);
                //Alert("导出失败！");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        
        //继续上传
        protected void btnUp_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                string sysuser_id = cookie.Values["sysuser_id"];
                TLaborReceipt.DelUpMsg(sysuser_id);
            }

            Response.Redirect("Upload2.aspx");
        }

        //返回
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        void Export(DataTable dt,string path)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("Sheet1");
            sheet.SetColumnWidth(0, 3000);
            sheet.SetColumnWidth(1, 15000);

            NPOI.SS.UserModel.IRow row0 = sheet.CreateRow(0);
            row0.CreateCell(0).SetCellValue("序号");
            row0.CreateCell(1).SetCellValue("错误信息");

            NPOI.SS.UserModel.ICellStyle style0 = workbook.CreateCellStyle();
            NPOI.SS.UserModel.IFont font0 = workbook.CreateFont();
            font0.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            style0.SetFont(font0);
            style0.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style0.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style0.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style0.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            row0.Cells[0].CellStyle = style0;
            row0.Cells[1].CellStyle = style0;
            sheet.CreateFreezePane(0, 1, 0, 1);//冻结首行

            //边框
            NPOI.SS.UserModel.ICellStyle styleRow = workbook.CreateCellStyle();
            styleRow.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styleRow.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styleRow.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            styleRow.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(r + 1);
                row.CreateCell(0).SetCellValue(dt.Rows[r]["RowNum"].ToString());
                row.CreateCell(1).SetCellValue(dt.Rows[r]["UpMsg"].ToString());

                row.Cells[0].CellStyle = styleRow;
                row.Cells[1].CellStyle = styleRow;
            }

            //创建流对象并设置存储Excel文件的路径
            using (FileStream fs = new FileStream(Server.MapPath("\\download") + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                //写入Excel文件
                workbook.Write(fs);
                workbook.Close();
            }
            Response.Redirect("/download/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + path);
        }
    }
}