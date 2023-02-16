using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin.LaborReceiptManage
{
    public partial class Upload2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            //    if (null != cookie)
            //    {
            //        string sysuser_id = cookie.Values["sysuser_id"];

            //        string yearmmdd = DateTime.Now.ToString("yyyy-MM-dd");
            //        string targetDir = "/upload_tmp/" + yearmmdd + "/";
            //        string uploadDir = "/upload/" + yearmmdd + "/";

            //        string LaborCompany = txtLaborCompany.Text.Trim();
            //        if (string.IsNullOrEmpty(LaborCompany))
            //        {
            //            Alert("所属劳务公司不能为空！");
            //            return;
            //        }

            //        HttpFileCollection files = HttpContext.Current.Request.Files;
            //        if (files.Count == 0)
            //        {
            //            Alert("请先选择上传的文件！");
            //            return;
            //        }

            //        if (!Directory.Exists(Server.MapPath(targetDir)))
            //        {
            //            Directory.CreateDirectory(Server.MapPath(targetDir));
            //        }
            //        if (!Directory.Exists(Server.MapPath(uploadDir)))
            //        {
            //            Directory.CreateDirectory(Server.MapPath(uploadDir));
            //        }
            //        string fileName = "";
            //        if (files.Count > 0)
            //        {
            //            Random rnd = new Random();
            //            for (int i = 0; i < files.Count; i++)
            //            {
            //                HttpPostedFile file = files[i];
            //                if (!string.IsNullOrEmpty(file.FileName))
            //                {
            //                    if (file.ContentLength > 0)
            //                    {
            //                        fileName = file.FileName;
            //                        string extension = Path.GetExtension(fileName);
            //                        string fileName2 = Path.GetFileNameWithoutExtension(file.FileName);
            //                        decimal Amout = 0.00m;
            //                        string UserName = "";
            //                        string PayDate = "";

            //                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".pdf")
            //                        {
            //                            string[] filenames = fileName2.Split('-');
            //                            if (filenames.Length < 2)
            //                            {
            //                                TLaborReceipt.AddUpMsg(sysuser_id, fileName2 + " 劳务费电子回单上传失败，失败原因：文件命名格式不正确", "0");
            //                                continue;
            //                            }
            //                            else
            //                            {
            //                                if (filenames[2].Length != 8)
            //                                {
            //                                    TLaborReceipt.AddUpMsg(sysuser_id, fileName2 + " 劳务费电子回单上传失败，失败原因：文件命名支付日期不正确", "0");
            //                                    continue;
            //                                }
            //                                if (!decimal.TryParse(filenames[1], out Amout))
            //                                {
            //                                    TLaborReceipt.AddUpMsg(sysuser_id, fileName2 + " 劳务费电子回单上传失败，失败原因：文件命名金额不正确", "0");
            //                                    continue;
            //                                }

            //                                UserName = filenames[0];
            //                                PayDate = filenames[2].Insert(4, "-").Insert(7, "-");
            //                            }

            //                            int num = rnd.Next(5000, 10000);
            //                            string path = "/" + uploadDir + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + extension;
            //                            file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));

            //                            TLaborReceipt.Add(fileName2, UserName, Amout.ToString("#.##"), PayDate, LaborCompany, HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")), path.Replace("//", "/"));

            //                            TLaborReceipt.AddLog(fileName2, "上传", HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")));

            //                            TLaborReceipt.AddUpMsg(sysuser_id, Path.GetFileNameWithoutExtension(file.FileName) + " 劳务费电子回单上传成功", "1");
            //                        }
            //                        else
            //                        {
            //                            TLaborReceipt.AddUpMsg(sysuser_id, fileName2 + " 劳务费电子回单上传失败，失败原因：文件格式不正确", "0");
            //                            continue;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        TLaborReceipt.AddUpMsg(sysuser_id, Path.GetFileNameWithoutExtension(file.FileName) + " 劳务费电子回单上传失败，失败原因：文件读取失败", "0");
            //                    }
            //                }
            //            }

            //            int SuccessCount = TLaborReceipt.SelectUpMsgSuccessCount(sysuser_id);
            //            int ErrorCount = TLaborReceipt.SelectUpMsgErrorCount(sysuser_id);
            //            Alert("回单已上传，其中成功 " + SuccessCount.ToString() + " 张，失败 " + ErrorCount.ToString() + " 张，详情请到结果页进行查看！", "Result.aspx");
            //        }
            //    }
            //}
        }
    }
}