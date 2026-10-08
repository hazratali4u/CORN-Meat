<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="frmRePrintInvoice.aspx.cs"
    Inherits="Forms_frmRePrintInvoice"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!doctype html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" charset="utf-8" />
    <title>CORN :: Point of Sales</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/zebra_dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/tree-style.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../images/favicon.png" />
    <link href="../css/calender.css" rel="stylesheet" />
</head>

<body>

    <form id="Form1" method="POST" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" AsyncPostBackTimeout="30000"
            EnablePartialRendering="true" />


        <asp:HiddenField runat="server" ID="hfProduct" />
        <asp:HiddenField runat="server" ID="hfOrderedproducts" />
        <asp:HiddenField runat="server" ID="hfSalesTax" />
        <asp:HiddenField runat="server" ID="hfIsCoverTable" />
        <asp:HiddenField runat="server" ID="hfServiceCharges" />
        <asp:HiddenField runat="server" ID="hfCompanyName" />
        <asp:HiddenField runat="server" ID="hfLocationName" />
        <asp:HiddenField runat="server" ID="hfAddress" />
        <asp:HiddenField runat="server" ID="hfPhoneNo" />
        <asp:HiddenField runat="server" ID="hfRegNo" />
        <asp:HiddenField runat="server" ID="hfFacebkId" />
        <asp:HiddenField runat="server" ID="hfCompanyEmail" />
        <asp:HiddenField runat="server" ID="hfInvoiceId" />
        <asp:HiddenField runat="server" ID="hfPaymentType" />
        <asp:HiddenField runat="server" ID="hfDiscountType" />
        <asp:HiddenField runat="server" ID="hfCurrentWorkDate" />
        <asp:HiddenField runat="server" ID="hfskuId" />
        <asp:HiddenField runat="server" ID="hfcatId" />
        <asp:HiddenField runat="server" ID="hfUserClick" />
        <asp:HiddenField runat="server" ID="hfDisable" Value="0" />
        <asp:HiddenField runat="server" ID="hfBookingType" Value="0" />
        <%-- Used for Toggle color on row Click --%>

        <asp:HiddenField ID="hfUserId" Value="" runat="server" />


        <div class="container" style="margin-bottom: 5px;">
            <div class="row">
                <div class="s1-left">
                    <div class="logom logo_main">
                        <img id="imgLogo" src="../images/watch.png" runat="server" alt="../images/watch.png" />
                    </div>
                    <div class="s1-right">
                        <div class="user-detail" style="display: none;">
                            <span class="user-detail-bold" id="user-detail-bold">
                                <asp:Literal ID="lblUserName" runat="server" Text="Label"></asp:Literal></span>
                        </div>
                        <div class="nav-ic">
                            <img src="../images/nav-opt.png" alt="Nav" />
                            <div class="exit-text">
                                <asp:LinkButton runat="server" ID="lnkExit" OnClick="lnkExit_OnClick">Exit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="s1-center">

                        <div class="order-tab" style="background: white; display: none;">
                            <label id="lblSaleForce" style="font-weight: normal;">
                                Order Taker</label>
                            <br />
                            <span class="order-tab-blk">
                                <asp:DropDownList runat="server" ID="ddlOrderBooker" Width="88%">
                                </asp:DropDownList>
                            </span>
                        </div>
                        <div class="order-tab" style="background: white; display: none;">
                            <label id="lblCoverTable" style="font-weight: normal;">
                                Cover Table</label>
                            <br />
                            <span class="order-tab-blk">
                                <asp:TextBox runat="server" ID="txtCoverTable" Width="88%" Height="25px"
                                    onkeypress="return onlyNumbers(this,event);"></asp:TextBox>

                            </span>
                        </div>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="col-md-12 gray-bg">
                    <div class="col-md-6">
                        <div class="row">

                            <div class="col-md-2">
                                <asp:HiddenField runat="server" ID="hfTableNo" />
                                <asp:HiddenField ID="hfTableId" Value="0" runat="server" />

                            </div>
                        </div>
                        <div class="row">
                            <fieldset>
                                <legend></legend>

                                <div class="col-md-3">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                                    <asp:TextBox runat="server" ID="txtFromDate"
                                        CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-1" style="margin-top: 27px">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" />
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                        TargetControlID="txtFromDate"></cc1:CalendarExtender>

                                </div>

                                <div class="col-md-3">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>

                                    <asp:TextBox runat="server" ID="txtToDate"
                                        CssClass="form-control"></asp:TextBox>


                                </div>
                                <div class="col-md-1" style="margin-top: 27px">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" />
                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                        TargetControlID="txtToDate" PopupPosition="TopLeft"></cc1:CalendarExtender>
                                </div>
                               
                                <div class="col-md-2" style="margin-top: 27px">
                                    <input type="button" class="btn btn-default" id="SearchInvoice" value=" Search" name=" Search">
                                </div>
                                <div class="col-md-2" style="margin-top: 27px">
                                    <input type="button" class="btn btn-primary" id="PrintInvoice" value="Print" name="Print">
                                </div>

                            </fieldset>
                        </div>
                        <div class="row">
                             <div class="col-md-12 table-top" style="padding-top: 0px; padding-bottom: 10px; height: 380px;"">
                            <div class="table-bar">
                                <div style="display:none;">
                                <asp:TextBox runat="server" ID="txtInvoiceNo"  
                                    onkeypress="return onlyNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12 vac-det " id="dvPendingBills" style="height:377px !important">
                                    <div style="background-color: #7dab49; padding: 5px 0px 5px 30px; color: #fff;">
                                        BILLS
                                    </div>
                                    <div class="scrolla peb" style="width: 100%; height:340px">
                                        <table class="pbill" id="tbl-pending-bills" style="width: 100%;">
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        </div>
                        <div class="clear">
                            <label id="MaxOrderNo" style="display: none;"></label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="btn-bg">
                            <asp:HiddenField runat="server" ID="hfCustomerType" Value="Dine In" />

                            <div class="box" id="lnkTakeaway">
                                <span>
                                    <img src="../images/Cus.png" alt="" /></span>
                                Takeaway
                                    <label id="lblCounter1"></label>
                            </div>
                            <div class="box" id="lnkDelivery">

                                <span>
                                    <img src="../images/delivery.png" alt="" /></span>
                                Delivery
                                    <label id="lblCounter2"></label>
                            </div>
                            <div class="clear">
                            </div>
                        </div>

                        <div class="col-md-12" style="display: none; background-color: #7dab49; padding: 3px 0px 3px 30px; color: #fff; font-size: medium">
                            <div class="row">
                                <div class="col-md-4" id="dvTakeawayCustomer" style="display: none;">
                                    <input type="text" id="txtTakeawayCustomer" runat="server"
                                        class="form-control" placeholder="Takeaway Customer" />
                                </div>
                                <div class="col-md-4" id="dvTableNo">
                                    Table No : 
                                        <label id="TableNo1">N/A</label>
                                </div>
                                <div class="col-md-4">
                                    Order No : 
                                        <label id="OrderNo1">N/A</label>
                                </div>
                                <div class="col-md-4">
                                    Net Amount : 
                                    <label id="lblTotalGrossAmount">0.00</label>
                                </div>


                            </div>
                            <div class="clear">
                            </div>
                        </div>

                        <!--2nd level start-->

                        <div class="table-bar">
                            <div class="emp-table scrolla" style="height: 400px;">
                                <table class="table table-striped table-bordered table-hover table-condensed cf">
                                    <thead class="cf head" style="background-color: #7dab49;">
                                        <tr>
                                            <th class="numeric table-text-head">ITEM(S)
                                            </th>
                                            <th class="table-text-head" style="text-align: center;">QTY
                                            </th>
                                            <th class="numeric table-text-head">PRICE
                                            </th>
                                            <th class="numeric table-text-head">AMOUNT
                                            </th>

                                        </tr>
                                    </thead>
                                    <tbody id="tble-ordered-products">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!--2nd level ends-->
                    </div>

                    <div class="clear">
                    </div>

                </div>
            </div>
        </div>

        <%-- Invoice Print Information --%>
        <div style="display: none; width: 3.2in">
            <div id="dvSaleInvoice">
                <style type="text/css">
                    #dvSaleInvoice {
                        width: 2.5in;
                    }

                    #SaleInvoice {
                        width: 2.5in;
                    }

                    #CompanyName {
                        font-size: 18px;
                        font-weight: bold;
                    }

                    #SaleInvoiceText {
                        font-size: 14px;
                    }

                    #InvoiceDate {
                        font-weight: bold;
                    }

                    #CustomerType {
                        font-weight: bold;
                    }

                    #phoneNo {
                        font-weight: bold;
                    }

                    #hrSaleInvoiceHead {
                        border: #333333 solid 1px;
                    }

                    #invoiceDetail {
                        width: 92%;
                        margin-top: 10px;
                    }

                    #invoiceDetailBody tr td {
                        border: #333333 solid 2px;
                        font-family: sans-serif;
                        font-size: 10px;
                        font-weight: normal;
                        padding: 5px;
                    }

                    #GrandTotal-value {
                        font-weight: bold;
                    }
                </style>
                <table id="SaleInvoice">
                    <tr>
                        <td colspan="2">
                            <table align="center">
                                <tr>
                                    <td class="logom2" style="width: 100%;">
                                        <img id="imgLogo2" runat="server" src="../images/watch.png" />
                                    </td>

                                </tr>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2" align="center">
                            <span style="font-size: 10px;">
                                <%=hfFacebkId.Value %></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="font-size: 16px; font-family: sans-serif; font-style: italic;">
                            <label id="SaleInvoiceText">
                                Sale Invoice</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="font-style: italic; font-family: sans-serif; font-size: 9px;">
                            <label id="CompanyAddress" style="font-size: 10px;">
                                <%=hfAddress.Value %>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="font-style: italic; font-family: sans-serif; font-size: 9px;">
                            <label id="CompanyNumber" style="font-size: 10px;">
                                Ph: &nbsp;<%=hfPhoneNo.Value %>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <label id="InvoiceDate">
                                12-dec-2014 12:02 PM
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 40%;">
                            <label id="CustomerType">
                                DINE IN</label>
                        </td>
                        <td align="right" style="width: 60%;">
                            <label id="PayType">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" style="font-style: italic; font-size: 12px; margin-bottom: 5px;">
                            <span id="RegNo" style="font-style: normal; font-size: 12px; display: none;">Sales Tax Reg No: &nbsp;<%=hfRegNo.Value%></span>
                        </td>
                    </tr>
                    <tr>
                        <%--OrderBooker and CoverTable--%>
                        <td align="left" colspan="2">
                            <span id="OrderTakerName" style="font-style: italic; font-size: 12px; width: 15%; margin-bottom: 8px; display: none;"></span>
                            <span id="OrderTaker" style="margin-left: 2%; font-size: 12px; font-style: normal; font-size: 12px; width: 5%; display: none;"></span>

                            <span id="CoverTableName" style="margin-left: 20%; font-style: italic; font-size: 12px; display: none;"></span>
                            <span id="CoverTable" style="margin-left: 1%; font-style: normal; font-size: 12px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <%--Customer Name and Address --%>
                        <td align="left" style="width: 100%" colspan="2">
                            <%--In Delivery Type used as Customer Phone 
                            <span id="CustomerNameDetail" style="font-style: italic; font-size: 12px; width: 15%; display:none;" ></span>--%>
                            <span id="CustomerDetail" style="margin-left: 0%; font-size: 12px; font-style: normal; display: none;"></span>
                        </td>
                    </tr>
                    <tr>
                        <%--Table No and Bill No --%>
                        <td align="left" style="width: 60%">
                            <span id="OrderInvoiceName" style="font-style: italic; font-size: 12px;">Order No:  </span>
                            <span id="OrderInvoice" style="margin-left: 2%; font-size: 12px; font-style: normal;"></span>
                        </td>
                        <td align="left" style="width: 40%">
                            <span id="BillNoName" style="font-style: italic; font-size: 12px;">Bill No:</span>
                            <span id="BillNo" style="margin-left: 2%; font-style: normal; font-size: 12px;"></span>
                        </td>
                    </tr>
                    <tr>
                        <%--Product Detail--%>
                        <td colspan="2">
                            <table id="invoiceDetail">
                                <thead id="invoiceDetailHead">
                                    <tr>
                                        <td style="text-align: left; font-size: 12px; font-family: sans-serif; width: 50%">Item Name
                                        </td>
                                        <td align="center" style="font-size: 12px; font-family: sans-serif; width: 10%">Qty
                                        </td>
                                        <td align="center" style="font-size: 12px; font-family: sans-serif; width: 15%">Rate
                                        </td>
                                        <td align="center" style="font-size: 12px; font-family: sans-serif; width: 15%">Amount
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="invoiceDetailBody">
                                </tbody>
                                <tfoot id="invoiceDetailFoot">
                                    <%--Calculation Detail--%>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label id="TotalValue-text">
                                                Total :</label>
                                        </td>
                                        <td align="right">
                                            <label id="TotalValue">
                                                6000</label>
                                        </td>
                                    </tr>
                                    <tr id="DiscountRow">
                                        <td colspan="3" align="right">
                                            <label id="Discount-text">
                                                Discount :
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="Discount-value">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr id="GstRow">
                                        <td colspan="3" align="right">
                                            <label id="GST-text">
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="Gst-value">
                                                600</label>
                                        </td>
                                    </tr>
                                    <tr id="GrandTotalRow">
                                        <td colspan="3" align="right">
                                            <label id="GrandTotal-text">
                                                Grand Total :
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="GrandTotal-value">
                                                6600 (Rs)</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label id="PayIn-text">
                                                Payment IN :
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="PayIn-value">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label id="balance-text">
                                                Change :
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="balance-value">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <label id="balance-text2">
                                            </label>
                                        </td>
                                        <td align="right">
                                            <label id="balance-value2">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="font-size: 10px;">
                                            <span id="Cashier" style="margin-left: 0%; font-size: 12px; font-style: normal;font-weight:bold;margin-bottom:0%"></span>
                                            <hr style="border: 1px solid black;margin-top:0%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="font-size: 10px;">
                                            <asp:Literal runat="server" ID="ltrlSlipNote"></asp:Literal>
                                            <label style="font-size: 10px;"><%=hfCompanyEmail.Value %></label>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
        </div>

        <% Response.WriteFile("~/Forms/popUpPayment.aspx");%>

        <script src="../js/jquery-1.10.2.js"></script>
        <script src="../js/jquery-2.0.2.min.js"></script>

        <script type="text/javascript" src="../AjaxLibrary/moment-with-locales.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/jQuery.print.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/zebra_dialog.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/jquery.lightbox_me.js"></script>
        <script src="../js/plugins/Block/jquery.blockUI.js"></script>

        <script type="text/javascript" src="../AjaxLibrary/RePrintInvoice.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/PrintInvoice.js"></script>
    </form>
</body>
</html>
