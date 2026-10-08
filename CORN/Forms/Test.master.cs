using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORNBusinessLayer.Classes;
using System.Collections;
using System.Text;
using System.Data;
using CORNCommon.Classes;

public partial class Forms_Test : System.Web.UI.MasterPage
{
    readonly RoleManagementController _objRole = new RoleManagementController();
    readonly DistributorController _ctlDist = new DistributorController();
    ArrayList _list;
    string _roleId;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            _list = (ArrayList)Session["EmployeeInfo"];

            if (_list != null)
            {
                _roleId = (string)_list[0];
            }
            lblUser.InnerHtml = (string)_list[1];

            lblWorkingDate.Text = ((DateTime)Session["CurrentWorkDate"]).ToString("dddd d MMM, yyyy");
          
               
                ltrlMenu.Text = CreateMenu();
            BreadCrumb.Text = GetBreadCrumb();
        }
    }
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        UserController UserCtl = new UserController();
        if (Session["UserID"] != null)
        {
            UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            Session.Clear();
            Response.Redirect("../Login.aspx");
        }
    }

    private string Menu()
    {
        StringBuilder strBreadCrumb = new StringBuilder();
        strBreadCrumb.Append("<li class='treeview'>");

        strBreadCrumb.Append("<span class='bor'><i class='fa fa-2'></i></span> <a href='#'><span>Test2</span> <i class='fa fa-angle-down pull-right'></i></a>");
        strBreadCrumb.Append("<ul class='treeview-menu'> <li class='treeview'><span class='bor1'><i class='fa fa-angle-right pull-left'></i></span><a  href='frmDistributor.aspx?LevelType=3&LevelID=9'>GEO Hierarchy</a></li>");

        strBreadCrumb.Append("<li class='treeview child-menu'>");

        strBreadCrumb.Append("<span class='bor1'><i class='fa fa-angle-right pull-left'></i></span><a href='#'>");
        strBreadCrumb.Append("<span>HR Data</span></a><ul class='treeview-menu'>");
        strBreadCrumb.Append("<li ><a id='btnlink2'>Employee Information</a></li></ul></li>");
        strBreadCrumb.Append("</ul> </li>");
        return strBreadCrumb.ToString();
    }

    private string GetBreadCrumb()
    {
        StringBuilder strBreadCrumb = new StringBuilder();
        strBreadCrumb.Append("<div>");
        strBreadCrumb.Append("<a href=\"#\"><span class='glyphicon glyphicon-home'></span>Home</a>");
        strBreadCrumb.Append("  /  ");

        if (Request.QueryString["LevelID"] != null)
        {
            DataTable dtBreadCrumb = _ctlDist.GetBreadCrumb(Convert.ToInt32(Request.QueryString["LevelID"]));


            if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
            {
                if (dtBreadCrumb.Rows[0]["ID2"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() == "12")
                        {

                            strBreadCrumb.Append("<a  href=\"# \">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a  href=\"# \">");

                        }
                        else
                        {
                            strBreadCrumb.Append("<a  href=\"#\">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION2"]);
                        strBreadCrumb.Append("</a>");
                        strBreadCrumb.Append(" /  ");
                    }
                }

                if (dtBreadCrumb.Rows[0]["ID3"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() == "12")
                        {
                            strBreadCrumb.Append("<a  href=\"# \">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a  href=\" # \">");

                        }
                        else
                        {
                            strBreadCrumb.Append("<a  href=\" # \">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION3"]);
                        strBreadCrumb.Append("</a>");
                        lblFormHead2.Text = dtBreadCrumb.Rows[0]["DESCRIPTION3"].ToString();
                        strBreadCrumb.Append(" /  ");
                    }
                }

                if (dtBreadCrumb.Rows[0]["ID4"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() == "12")
                        {
                            strBreadCrumb.Append("<a href=\" # \">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a  href=\" # \">");

                        }
                        else
                        {
                            strBreadCrumb.Append("<a  href=\" # \">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION4"]);
                        strBreadCrumb.Append("</a>");
                        lblFormHead.Text = dtBreadCrumb.Rows[0]["DESCRIPTION4"].ToString();
                    }
                }

            }
            strBreadCrumb.Append("</div>");
        }
        return strBreadCrumb.ToString();
    }

    private string CreateMenu()
    {
        int roleId = (int)Session["RoleID"];
        bool IsSystemAdmin = Convert.ToBoolean(Session["IS_SystemAdmin"]);
        DataTable dtParent = _objRole.SelectRoleWiseModule(roleId, 2, Constants.IntNullValue);
        StringBuilder sbMenu = new StringBuilder();
        sbMenu.Append("<ul class='sidebar-menu'>");
        //sbMenu.Append("<li class=''> <span class='bor'><i class='fa fa-4'></i> </span>");
        //sbMenu.Append("<a href='#'>");
        //sbMenu.Append("<span>Dashboard</span>");
        //sbMenu.Append("<i class='fa fa-angle-down pull-right'></i>");
        //sbMenu.Append("</a>");

        foreach (DataRow dr in dtParent.Rows)
        {
            DataTable dtSubMenu1 = _objRole.SelectRoleWiseModule(roleId, 3, Convert.ToInt32(dr["MODULE_ID"]));
            DataTable dtBreadCrumb = _ctlDist.GetBreadCrumb(Convert.ToInt32(Request.QueryString["LevelID"]));
            if (dtBreadCrumb.Rows.Count > 0)
            {
                if (dtBreadCrumb.Rows[0]["ID4"].ToString() == Request.QueryString["LevelID"].ToString() && dtBreadCrumb.Rows[0]["ID2"].ToString() == dr["MODULE_ID"].ToString())
                {
                    sbMenu.Append("<li class='treeview active'>");
                }
                else
                {
                    sbMenu.Append("<li class='treeview'>");
                }
            }
            else
            {
                sbMenu.Append("<li class='treeview'>");
            }
            sbMenu.Append("<span class='bor'><i class='" + dr["MODULE_CSS"].ToString() + "'></i></span>");
            sbMenu.Append("<a href='#'>");
            sbMenu.Append("<span>" + dr["MODULE_DESCRIPTION"].ToString() + "</span>");

            sbMenu.Append("<i class='fa fa-angle-down pull-right'></i>");

            sbMenu.Append("</a>");

            sbMenu.Append("<ul  class='treeview-menu'>");
            foreach (DataRow dr2 in dtSubMenu1.Rows)
            {
                DataTable dtSubMenu2;
                if (IsSystemAdmin)
                {
                    dtSubMenu2 = _objRole.SelectRoleWiseModule(Constants.IntNullValue, 6, Convert.ToInt32(dr2["MODULE_ID"].ToString()));
                }
                else
                {
                    dtSubMenu2 = _objRole.SelectRoleWiseModule(roleId, 4, Convert.ToInt32(dr2["MODULE_ID"].ToString()));
                }
                DataTable dtBreadCrumb2 = _ctlDist.GetBreadCrumb(Convert.ToInt32(Request.QueryString["LevelID"]));
                if (dtBreadCrumb2.Rows.Count > 0)
                {
                    if (dtBreadCrumb2.Rows[0]["ID4"].ToString() == Request.QueryString["LevelID"].ToString() && dtBreadCrumb2.Rows[0]["ID3"].ToString() == dr2["MODULE_ID"].ToString())
                    {
                        sbMenu.Append("<li class='treeview child-menu active'>");
                    }
                    else
                    {
                        sbMenu.Append("<li class='treeview child-menu'>");
                    }
                }
                else
                {
                    sbMenu.Append("<li class='treeview child-menu'>");
                }

                sbMenu.Append("<span class='bor1'><i class='fa fa-angle-right pull-left'></i></span>");
                sbMenu.Append("<a style='margin-left: 50px;' href='#'>" + dr2["MODULE_DESCRIPTION"].ToString() + "</a>");

                sbMenu.Append("<ul class='treeview-menu'>");

                foreach (DataRow dr3 in dtSubMenu2.Rows)
                {
                    sbMenu.Append("<li>");
                    sbMenu.Append("<a style='margin-left: 57px;' href='" + dr3["MODULE_ID"].ToString() + "?LevelType=3&amp;LevelID=" + dr3["mid"].ToString() + "' id='A1'>");
                    sbMenu.Append(dr3["MODULE_DESCRIPTION"].ToString());
                    sbMenu.Append("</a>");
                    sbMenu.Append("</li>");
                }
                sbMenu.Append("</ul>");
            }
            sbMenu.Append("</ul>");

            sbMenu.Append("</li>");
        }

        sbMenu.Append("</li>");
        sbMenu.Append("</ul>");
        return sbMenu.ToString();
    }

}
