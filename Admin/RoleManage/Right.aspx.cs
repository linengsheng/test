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

namespace WebSystem.Admin.RoleManage
{
    public partial class Right : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        void InitPage()
        {
            //绑定模块列表
            ddlModule.DataSource = TModule.Select();
            ddlModule.DataValueField = "module_id";
            ddlModule.DataTextField = "module_name";
            ddlModule.DataBind();

            if (Request.QueryString["id"] != null)
            {
                //加载权限列表
                BindRight(ddlModule.SelectedValue);

                DataTable dt = TRole.Select(Request.QueryString["id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lbrolename.Text = dt.Rows[0]["role_name"].ToString();
                }
                //勾选权限
                CheckRight(ddlModule.SelectedValue);
            }
        }

        void BindRight(string module_id)
        {
            cbright.DataSource = TActionKey.SelectByModule(module_id);
            cbright.DataTextField = "actionkey_name";
            cbright.DataValueField = "actionkey_id";
            cbright.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                for (int i = 0; i < cbright.Items.Count; i++)
                {
                    string module_id = ddlModule.SelectedValue;
                    string actionkey_id = cbright.Items[i].Value;

                    DataTable dtma = TModuleAction.Select(module_id, actionkey_id);
                    if (dtma != null && dtma.Rows.Count > 0)
                    {
                        string moduleaction_id = dtma.Rows[0]["moduleaction_id"].ToString();
                        DataTable dt = TRight.SelectByID2(Request.QueryString["id"], module_id, actionkey_id);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //存在记录（更新）
                            TRight.Edit(Request.QueryString["id"], moduleaction_id, cbright.Items[i].Selected ? "1" : "0");
                        }
                        else
                        {
                            //不存在记录（添加）
                            TRight.Add(Request.QueryString["id"], moduleaction_id, cbright.Items[i].Selected ? "1" : "0");
                        }
                    }
                }
                TSysUserOperatelog.Add("设置权限[" + lbrolename.Text + "]");
            }
            Alert("提交完成！");
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRight(ddlModule.SelectedValue);
            CheckRight(ddlModule.SelectedValue);
        }

        void CheckRight(string module_id)
        {
            //勾选权限
            for (int i = 0; i < cbright.Items.Count; i++)
            {
                cbright.Items[i].Selected = TRight.SelectByID(Request.QueryString["id"], module_id, cbright.Items[i].Value);
            }
        }
    }
}
