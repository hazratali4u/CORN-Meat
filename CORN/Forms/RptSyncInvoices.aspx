<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="RptSyncInvoices.aspx.cs" Inherits="Forms_RptSyncInvoices" Title="CORN :: Sync Invoice" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" CssClass="">
                <Items>
                    <dx:ListEditItem Text="Level 3" Value="Level 3" Selected="true" />
                    <dx:ListEditItem Text="Level 4" Value="Level 4" />
                </Items>
            </dx:ASPxComboBox>

        </div>
    </form>
</body>
</html>
