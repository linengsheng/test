using KvanitWS.DataAccess;
using WebSystem.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSystem.Admin.RoleManage
{
    public partial class Right2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
                Bind();
            }
        }

        void InitPage()
        {
            DataTable dt = TRole.Select(Request.QueryString["id"]);
            if (dt != null && dt.Rows.Count > 0)
            {
                lbrolename.Text = dt.Rows[0]["role_name"].ToString();
            }
        }

        void Bind()
        {
            try
            {
                DataTable dt = TModule.Select();
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
            catch
            {
                gvData.DataSource = null;
                gvData.DataBind();
            }
        }

        int row = 0;

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

                //绑定权限
                CheckBoxList cbright = (CheckBoxList)e.Row.FindControl("cbright");
                if (cbright != null)
                {
                    //加载功能
                    cbright.DataSource = TActionKey.SelectByModule(gvData.DataKeys[e.Row.RowIndex].Value.ToString());
                    cbright.DataTextField = "actionkey_name";
                    cbright.DataValueField = "actionkey_id";
                    cbright.DataBind();

                    //勾选角色已有权限
                    for (int i = 0; i < cbright.Items.Count; i++)
                    {
                        cbright.Items[i].Selected = TRight.SelectByID(Request.QueryString["id"], gvData.DataKeys[e.Row.RowIndex].Value.ToString(), cbright.Items[i].Value);
                    }
                }

                //合并上级模块
                int rowindex = e.Row.RowIndex;
                if (rowindex - 1 < 0) return;
                if (e.Row.Cells[0].Text == gvData.Rows[rowindex - 1].Cells[0].Text)
                {
                    if (gvData.Rows[row].Cells[0].RowSpan == 0)
                    {
                        gvData.Rows[row].Cells[0].RowSpan++;
                    }
                    gvData.Rows[row].Cells[0].RowSpan++;
                    gvData.Rows[row].Cells[0].CssClass = "tdR2";
                    e.Row.Cells[0].Visible = false;
                }
                else
                {
                    e.Row.Cells[0].CssClass = "tdR2";
                    row = rowindex;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;

            if (Request.QueryString["id"] != null)
            {
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    CheckBoxList cbright = (CheckBoxList)gvData.Rows[i].FindControl("cbright");
                    if (cbright != null)
                    {
                        string module_id = gvData.DataKeys[i].Value.ToString();
                        for (int j = 0; j < cbright.Items.Count; j++)
                        {
                            string actionkey_id = cbright.Items[j].Value;

                            DataTable dtma = TModuleAction.Select(module_id, actionkey_id);
                            if (dtma != null && dtma.Rows.Count > 0)
                            {
                                string moduleaction_id = dtma.Rows[0]["moduleaction_id"].ToString();
                                DataTable dt = TRight.SelectByID2(Request.QueryString["id"], module_id, actionkey_id);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //存在记录（更新）
                                    TRight.Edit(Request.QueryString["id"], moduleaction_id, cbright.Items[j].Selected ? "1" : "0");
                                }
                                else
                                {
                                    //不存在记录（添加）
                                    TRight.Add(Request.QueryString["id"], moduleaction_id, cbright.Items[j].Selected ? "1" : "0");
                                }
                            }
                        }
                    }
                }
                TSysUserOperatelog.Add("设置权限[" + lbrolename.Text.Trim() + "]");
            }
            Alert("提交完成！", "Index.aspx");
        }
    }
}