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
    public partial class Upload3 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                string sysuser_id = cookie.Values["sysuser_id"];

                string yearmmdd = DateTime.Now.ToString("yyyy-MM-dd");
                string targetDir = "/upload_tmp/" + yearmmdd + "/";
                string uploadDir = "/upload/" + yearmmdd + "/";

                string LaborCompany = Request.QueryString["LaborCompany"];
                if (string.IsNullOrEmpty(LaborCompany))
                {
                    Alert("所属劳务公司不能为空！");
                    return;
                }

                HttpFileCollection files = HttpContext.Current.Request.Files;
                if (files.Count == 0)
                {
                    Alert("请先选择上传的文件！");
                    return;
                }

                if (!Directory.Exists(Server.MapPath(targetDir)))
                {
                    Directory.CreateDirectory(Server.MapPath(targetDir));
                }
                if (!Directory.Exists(Server.MapPath(uploadDir)))
                {
                    Directory.CreateDirectory(Server.MapPath(uploadDir));
                }
                string fileName = "";
                if (files.Count > 0)
                {
                    Random rnd = new Random();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        if (!string.IsNullOrEmpty(file.FileName))
                        {
                            if (file.ContentLength > 0)
                            {
                                fileName = file.FileName;
                                string extension = Path.GetExtension(fileName);
                                string fileName2 = Path.GetFileNameWithoutExtension(file.FileName);
                                decimal Amout = 0.00m;
                                string UserName = "";
                                string PayDate = "";

                                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".bmp" || extension.ToLower() == ".pdf")
                                {
                                    string[] filenames = fileName2.Split('-');
                                    if (filenames.Length != 3)
                                    {
                                        TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件命名格式不正确。正确格式为：姓名-金额-支付日期.jpg，例如：张三-1000-20010512.jpg", "0");
                                        continue;
                                    }
                                    else
                                    {
                                        if (filenames[2].Length != 8)
                                        {
                                            TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件命名支付日期不正确，支付日期必须为8位数字。正确格式为：姓名-金额-支付日期.jpg，例如：张三-1000-20010512.jpg", "0");
                                            continue;
                                        }
                                        if (!decimal.TryParse(filenames[1], out Amout))
                                        {
                                            TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件命名金额不正确，金额必须为数值。正确格式为：姓名-金额-支付日期.jpg，例如：张三-1000-20010512.jpg", "0");
                                            continue;
                                        }

                                        UserName = filenames[0];
                                        PayDate = filenames[2].Insert(4, "-").Insert(7, "-");

                                        DateTime dtTmp;
                                        if (!DateTime.TryParse(PayDate, out dtTmp))
                                        {
                                            TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件命名格式支付日期不正确。正确格式为：姓名-金额-支付日期.jpg，例如：张三-1000-20010512.jpg", "0");
                                            continue;
                                        }
                                    }

                                    //int num = rnd.Next(5000, 10000);
                                    //string path = "/" + uploadDir + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + num + extension;
                                    string path = "/" + uploadDir + "/" + fileName2 + extension;
                                    file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));

                                    TLaborReceipt.Add(fileName2, UserName, Amout.ToString("#.##"), PayDate, LaborCompany, HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")), path.Replace("//", "/"));

                                    TLaborReceipt.AddLog(fileName2, "上传", HttpUtility.UrlDecode(cookie.Values["realname"], Encoding.GetEncoding("gb2312")));

                                    TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传成功", "1");
                                }
                                else
                                {
                                    TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件类型不正确。正确类型为：.jpg|.jpeg|.png|.gif|.bmp|.pdf", "0");
                                    continue;
                                }
                            }
                            else
                            {
                                TLaborReceipt.AddUpMsg(sysuser_id, fileName + " 劳务费电子回单上传失败，失败原因：文件读取失败，请确认文件是否完整", "0");
                            }
                        }
                    }

                    int SuccessCount = TLaborReceipt.SelectUpMsgSuccessCount(sysuser_id);
                    int ErrorCount = TLaborReceipt.SelectUpMsgErrorCount(sysuser_id);
                    Alert("回单已上传，其中成功 " + SuccessCount.ToString() + " 张，失败 " + ErrorCount.ToString() + " 张，详情请到结果页进行查看！", "Result.aspx");
                }
            }
        }
    }
}