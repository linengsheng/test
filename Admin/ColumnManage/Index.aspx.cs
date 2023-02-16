using KvanitWS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSystem.Base;

namespace WebSystem.Admin.ColumnManage
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetRight();
                LoadType(null);
            }
        }

        void SetRight()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
            if (null != cookie)
            {
                btnAdd.Visible = TRight.Select(cookie.Values["role_id"], "column", "add");
            }
            else
            {
                btnAdd.Visible = false;
            }
        }

        private void LoadType(TreeNode node)
        {
            DataTable dt = TColumn.Select();
            if (dt != null)
            {
                if (node == null)
                {
                    //加载父栏目
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["parent_id"].ToString() == "0")
                        {
                            string tntext = "";
                            tntext = dr["column_name"].ToString();

                            //权限
                            HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                            if (null != cookie)
                            {
                                if (TRight.Select(cookie.Values["role_id"], "column", "add"))
                                {
                                    tntext += "&nbsp;&nbsp;<a href='Edit.aspx?cmd=add&pid=" + dr["column_id"].ToString() + "' title='添加子栏目'>[添加]</a>";
                                }
                                if (TRight.Select(cookie.Values["role_id"], "column", "edit"))
                                {
                                    tntext += "&nbsp;&nbsp;<a href='Edit.aspx?cmd=edit&id=" + dr["column_id"].ToString() + "' title='编辑'>[编辑]</a>";
                                }
                                if (TRight.Select(cookie.Values["role_id"], "column", "delete"))
                                {
                                    tntext += "&nbsp;&nbsp;<a onclick='return confirm(\"删除数据不可恢复，您确认要删除吗？\");' href='Delete.aspx?id=" + dr["column_id"].ToString() + "' title='删除'>[删除]</a>";
                                }
                            }

                            TreeNode tn = new TreeNode(tntext, dr["column_id"].ToString());
                            tvData.Nodes.Add(tn);
                            tn.SelectAction = TreeNodeSelectAction.None;
                            tn.Expanded = true;
                            LoadType(tn);
                        }
                    }
                }
                else
                {
                    //加载子栏目
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["parent_id"].ToString() != "0")
                        {
                            if (dr["parent_id"].ToString() == node.Value)
                            {
                                string tntext = "";
                                tntext += dr["column_name"].ToString();

                                //权限
                                HttpCookie cookie = HttpContext.Current.Request.Cookies["KvanitWebSystem"];
                                if (null != cookie)
                                {
                                    //if (TRight.Select(cookie.Values["role_id"], "column", "add"))
                                    //{
                                    //    tntext += "&nbsp;&nbsp;<a href='Edit.aspx?cmd=add&pid=" + dr["column_id"].ToString() + "' title='添加子栏目'>[添加]</a>";
                                    //}
                                    if (TRight.Select(cookie.Values["role_id"], "column", "edit"))
                                    {
                                        tntext += "&nbsp;&nbsp;<a href='Edit.aspx?cmd=edit&id=" + dr["column_id"].ToString() + "' title='编辑'>[编辑]</a>";
                                    }
                                    if (TRight.Select(cookie.Values["role_id"], "column", "delete"))
                                    {
                                        tntext += "&nbsp;&nbsp;<a onclick='return confirm(\"删除数据不可恢复，您确认要删除吗？\");' href='Delete.aspx?id=" + dr["column_id"].ToString() + "' title='删除'>[删除]</a>";
                                    }
                                }

                                TreeNode tn = new TreeNode(tntext, dr["column_id"].ToString());
                                node.ChildNodes.Add(tn);
                                tn.SelectAction = TreeNodeSelectAction.None;
                                tn.Expanded = true;
                                if (tn.Depth > 0)
                                {
                                    //默认只展开第一级
                                    tn.Expanded = false;
                                }
                                LoadType(tn);
                            }
                        }
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx?cmd=add&pid=0");
        }
    }
}