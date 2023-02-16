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
using System.Drawing;

namespace WebSystem
{
    public partial class Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["codeKvanit"] = CreateImage(4, ValidType.NumericAndEnglishChar);
            }
        }
        /// <summary>
        /// 验证码的类型
        /// </summary>
        public enum ValidType
        {
            /// <summary>
            /// 只有数字
            /// </summary>
            Numeric,
            /// <summary>
            /// 数字和英文字符
            /// </summary>
            NumericAndEnglishChar,
            /// <summary>
            /// 中文字符
            /// </summary>
            ChineseChar
        }
        /// <summary>
        /// 生成一个随机文字图片,保存在 Session["code1"]
        /// </summary>
        /// <param name="count">图片中字的个数</param>
        /// <returns>生成的文字</returns>
        public string CreateImage(int count, ValidType type)
        {
            string ValidCode = GenCode(count, type);

            switch (type)
            {
                case ValidType.Numeric:
                    CreateCheckCodeImage(ValidCode, 13.5);
                    break;
                case ValidType.NumericAndEnglishChar:
                    CreateCheckCodeImage(ValidCode, 14);
                    break;
                case ValidType.ChineseChar:
                    CreateCheckCodeImage(ValidCode, 22.5);
                    break;
                default:
                    break;
            }
            return ValidCode;
        }
        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <param name="num">随机出几个字符</param>
        /// <returns>随机出的字符串</returns>
        private string GenCode(int num, ValidType type)
        {
            string str;
            switch (type)
            {
                case ValidType.Numeric:
                    str = "0123456789";
                    break;
                case ValidType.NumericAndEnglishChar:
                    str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                case ValidType.ChineseChar:
                    //常用498个汉字
                    str = "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严";
                    break;
                default:
                    str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
            }
            char[] chastr = str.ToCharArray();
            string code = "";
            Random rd = new Random();
            int i;
            for (i = 0; i < num; i++)
            {
                code += str.Substring(rd.Next(0, str.Length), 1);
            }
            return code;
        }
        /// <summary>
        /// 生成图片（增加背景噪音线、前景噪音点）
        /// </summary>
        /// <param name="checkCode">随机出字符串</param>
        private void CreateCheckCodeImage(string checkCode, double codeWidth)
        {
            if (checkCode.Trim() == "" || checkCode == null)
                return;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)(checkCode.Length * codeWidth), 22);
            //string path = Server.MapPath("~/images/1.jpg");
            //System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            //System.Drawing.Bitmap image = new System.Drawing.Bitmap(path);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                // 画图片的背景噪音线
                int i;
                for (i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new System.Drawing.Font("Verdana", 12, (System.Drawing.FontStyle.Bold));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);
                //System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush( Color.Blue);
                g.DrawString(checkCode, font, brush, 2, 2, new System.Drawing.StringFormat());
                //画图片的前景噪音点
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
                //清除该页输出缓存，设置该页无缓存
                //Response.Buffer = true;
                //Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
                //Response.Expires = 0;
                //Response.CacheControl = "no-cache";
                //Response.AppendHeader("Pragma", "No-Cache");
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/JPEG";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                g.Dispose();
                image.Dispose();
            }
            catch
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}

