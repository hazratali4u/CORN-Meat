using System;
using System.Web.UI;

public partial class Forms_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["UserID"]) > 0 && Page.Request.UrlReferrer != null)
        //{
           
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!IsPostBack)
        {
            if (Session["Message"] != null)
            {
                lblMessage.Text = Session["Message"].ToString();

            }
        }
    }
}