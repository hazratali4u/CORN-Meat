<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        try
        {
            CORNBusinessLayer.Classes.UserController UserCtl = new CORNBusinessLayer.Classes.UserController();

            if (Session["UserID"] != null)
            {
                UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
                Session.Clear();
                Response.Redirect("../Login.aspx");
            }
            else
            {
                Session.Clear();
                Response.Redirect("../Login.aspx");
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally {
            Session.Clear();
            Response.Redirect("../Login.aspx");
        }
    }

</script>
