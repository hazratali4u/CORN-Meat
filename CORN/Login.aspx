<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Login :: Welcome</title>
    <link href="OrderPOS/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link href="OrderPOS/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="login-page">
            <asp:UpdatePanel ID="upLogin" runat="server">   
                <ContentTemplate>
                    <div class="container">
                        <div class="row login-contents">
                            <div class="col-md-12">
                                <div class="logo">
                                    &nbsp;&nbsp;&nbsp;
                                <img src="imgMaster/logo.png" class="img-responsive" alt="" />
                                    <h1>Welcome</h1>
                                    <p>
                                        <div runat="server" id="dvError" visible="false" style='padding: 10px; margin: 5px; background-color: #fbefef; border: Solid 1px #bb0000; color: #bb0000; font-size: 12px; font-family: Verdana; width: 100%'>
                                    <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
                                      </div>
                                        <p>
                                        </p>
                                        <p>
                                            <div id="dvLicense" runat="server" style="padding: 10px; margin: 5px; background-color: #fbefef; border: Solid 1px #bb0000; color: #bb0000; font-size: 12px; font-family: Verdana; width: 100%" visible="false">
                                                <asp:Label ID="lblLicenseMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                            <p>
                                            </p>
                                            <div class="from-group">
                                                <asp:TextBox ID="txtLogin" runat="server" class="form-control" placeholder="User Name" />
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="*******" TextMode="Password" />
                                                <asp:Button ID="btnSignIn" runat="server" CssClass="btn btn-success" OnClick="btnSignIn_Click" Text="Login" />
                                            </div>
                                            <p>
                                            </p>
                                            <p>
                                            </p>
                                        </p>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
    <script src="AjaxLibrary/jquery-1.10.2.js" type="text/javascript" language="javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="AjaxLibrary/bootstrap.min.js" type="text/javascript" language="javascript"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
    </script>
</body>
</html>
