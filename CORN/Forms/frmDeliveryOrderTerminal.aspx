<%@ Page Language="C#" AutoEventWireup="true" 
    CodeFile="frmDeliveryOrderTerminal.aspx.cs" Inherits="Forms_frmDeliveryOrderTerminal" %>



<!doctype html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" charset="utf-8" />
    <title>CORN :: Delivery Order Terminal</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/zebra_dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/tree-style.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../images/favicon.png" />
</head>

<body onload="BillTime()">

    <form id="Form1" method="POST" runat="server">


        <asp:ScriptManager ID="MainScriptManager" runat="server" AsyncPostBackTimeout="30000"
            EnablePartialRendering="true" />

        <asp:HiddenField runat="server" ID="hfProduct" />
        <asp:HiddenField runat="server" ID="hfOrderedproducts" />
        <asp:HiddenField runat="server" ID="hfSalesTax" />
        <asp:HiddenField runat="server" ID="hfIsCoverTable" />
        <asp:HiddenField runat="server" ID="hfInvoiceId" />
        <asp:HiddenField runat="server" ID="hfCurrentWorkDate" />
        <asp:HiddenField runat="server" ID="hfskuId" />

        <input type="hidden" value="Delivery Order" name="Delivery Order" id="hfPageType"/>

        <div class="header-wrapper">
            <span id='ct' style="display: none;"></span><%--Getting Current Time of System For Calculating Order Time--%>
            <div class="container" style="margin-bottom: 5px;">
                 <div class="row">
                    <div class="s1-left">
                        <div class="logom logo_main">
                           <img id="imgLogo" src="../images/watch.png" runat="server" alt="../images/watch.png" />
                        </div>
                         <div class="s1-right">
                        <div class="user-detail">
                            Welcome - <span class="user-detail-bold">
                                <asp:Literal ID="lblUserName" runat="server" Text="Label"></asp:Literal></span><br />
                            <asp:Literal ID="lblDateTime" runat="server" Text="Label"></asp:Literal>
                        </div>
                        <div class="nav-ic">
                            <img src="../images/nav-opt.png" alt="Nav" />
                            <div class="exit-text">
                                <asp:LinkButton runat="server" ID="lnkExit" OnClick="lnkExit_OnClick">Exit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                        <div class="s1-center">
                           
                            <div class="order-tab">
                                Table No<br />
                                <span class="order-tab-blk">
                                    <label id="TableNo1">
                                        N/A</label></span>
                            </div>
                            <div class="order-tab">
                                Order No<br />
                                <span class="order-tab-blk">
                                    <label id="OrderNo1">
                                        N/A</label>
                                    <label id="MaxOrderNo" style="display: none;"></label>
                                </span>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>                    
                    <div class="clear">
                    </div>
                </div>
                <div class="col-md-12 gray-bg">
                    <div class="clear">
                        </div>
                    <div class="col-md-6 table-top">
                        <div class="col-md-12 table-top" style="padding-top: 0px; padding-bottom: 10px; padding-left: 5px;">
                            <div class="table-bar">
                               
                                <div class="col-md-12 vac-det-Kitchen ">
                                    <div style="background-color: #7dab49; padding: 5px 0px 5px 30px; color: #fff;">
                                        Pending Orders
                                    </div>
                                    <div class="height-465 scrolla peb" style="width: 100%;">
                                        <table class="porder" id="tbl-pending-bills" style="width: 100%;">
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 table-top">
                        <div class="table-bar">
                            <div class="emp-table scrolla" style="height: 500px;">
                                <table class="table table-striped table-bordered table-hover table-condensed cf">
                                    <thead class="cf head" style="background-color: #7dab49;">
                                        <tr>
                                            <th class="numeric table-text-head">SECTION
                                            </th>
                                            <th class="numeric table-text-head">ITEM(S)
                                            </th>
                                            <th class="table-text-head" style="text-align: center;" >QTY
                                            </th>
                                            
                                            
                                        </tr>
                                    </thead>
                                    <tbody id="tble-ordered-products">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    
                </div>
            </div>
        </div>

        
        <script src="../AjaxLibrary/jquery-1.7.1.min.js"></script>
        
        <script type="text/javascript" src="../AjaxLibrary/zebra_dialog.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/KitchenOrderTaking.js"></script>
    </form>
</body>
</html>


