<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="frmOrderPOSCentralized.aspx.cs"
    Inherits="Forms.frmOrderPOSCentralized"
    EnableEventValidation="false" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!doctype html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" charset="utf-8" />
    <title>CORN :: Centralized Order Taking</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/zebra_dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/tree-style.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../images/favicon.png" />
    <link href="../css/calender.css" rel="stylesheet" type="text/css" />
</head>
<body onload="BillTime()">
    <form id="Form1" method="POST" runat="server">
        <asp:ScriptManager ID="MainScriptManager" runat="server" AsyncPostBackTimeout="30000"
            EnablePartialRendering="true" />
        <asp:HiddenField runat="server" ID="hfProduct" />
        <asp:HiddenField runat="server" ID="hfCustomers" />
        <asp:HiddenField runat="server" ID="hfLocations" />
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
        <%-- Used on Deletion and Decrease Qty--%>
        <asp:HiddenField runat="server" ID="hfcatId" />
        <asp:HiddenField runat="server" ID="hfUserClick" />
        <asp:HiddenField runat="server" ID="hfDisable" Value="0" />
        <asp:HiddenField runat="server" ID="hfCustomerId" Value="0" />
        <asp:HiddenField ID="hfCustomerNo" runat="server" Value="" />
        <asp:HiddenField ID="hfCustomerName" runat="server" Value="" />
        <asp:HiddenField runat="server" ID="hfBookingType" Value="0" />
        <asp:HiddenField runat="server" ID="hfReport" Value="" />
        <asp:HiddenField runat="server" ID="hfRowIndex" Value="" />
        <%-- Used for Toggle color on row Click --%>

        <%--for Yes and no's--%>
        <asp:HiddenField runat="server" ID="hfRow" Value="" />
        <asp:HiddenField ID="hfUserId" Value="" runat="server" />
        <asp:HiddenField ID="hfCounter" Value="0" runat="server" />
        <%-- Used for Serials on Item Click --%>
        <asp:HiddenField runat="server" ID="hfCheckSMS" Value="0" />
        <%-- update on Delivery bills icon Click --%>
        <asp:HiddenField runat="server" ID="hfCategoryType" Value="0" />
        <%-- update on Category Type Selection Modifer or Menu --%>
        <asp:HiddenField runat="server" ID="hfDealId" Value="0" />

        <input type="hidden" name="hfDelIndex" id="hfDelIndex" value=""><%-- Used counter C1 for index Quantity,Delete Decrease--%>
        <input type="hidden" name="hfDelId" id="hfDelId" value=""><%-- Get DealID of row index for Quantity Decrease--%>
        <input type="hidden" name="hfSkuId" id="hfSkuId" value=""><%-- Used for getting modifier Items on Selection--%>

        <asp:HiddenField runat="server" ID="hfStockStatus" Value="0" />
        <input type="hidden" name="hfCardNo" id="hfCardNo" value="">
        <input type="hidden" name="hfCardTypeId" id="hfCardTypeId" value="0">
        <input type="hidden" name="hfCardDiscount" id="hfCardDiscount" value="0">
        <input type="hidden" name="hfCardPoints" id="hfCardPoints" value="0">
        <input type="hidden" name="hfCardPurchasing" id="hfCardPurchasing" value="0">
        <input type="hidden" name="hfCardAmountLimit" id="hfCardAmountLimit" value="0">

        <asp:HiddenField runat="server" ID="hfIsCard" Value="0" />
        <input type="hidden" name="hfchkDiscountType" id="hfchkDiscountType" value="0" />

        <asp:HiddenField runat="server" ID="hfSaleType" Value="" />
        <asp:HiddenField runat="server" ID="hfCan_DineIn" Value="" />
        <asp:HiddenField runat="server" ID="hfCan_Delivery" Value="" />
        <asp:HiddenField runat="server" ID="hfCan_TakeAway" Value="" />
        <asp:HiddenField ID="hfIS_CanGiveDiscount" runat="server" Value="0" />
        <asp:HiddenField ID="hfCustomerAdd" runat="server" Value="0" />
        <span id='ct' style="display: none;"></span><%--Getting Current Time of System For Calculating Order Time--%>
        <div class="container" style="margin-bottom: 5px;">
            <div class="row">
                <div class="s1-left">
                    <div class="logom logo_main">
                        <img id="imgLogo" src="../images/watch.png" runat="server" alt="../images/watch.png" />
                    </div>
                    <div class="s1-right">
                        <div class="user-detail">
                            Welcome - <span class="user-detail-bold" id="user-detail-bold">
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
                        <label id="MaxBillNo" style="display: none;"></label>
                        <label id="MaxOrderNo" style="display: none;"></label>
                        <div class="order-tab" style="background: white;width:80%;">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:40%;">
                                        <label id="lblLocation" style="font-weight: normal;">Location</label>
                                        <br />
                                        <dx:ASPxComboBox ID="ddlDistributor" runat="server" CssClass="form-control" ClientInstanceName="ddlLocation"
                                            ClientSideEvents-SelectedIndexChanged="function(s, e) { LocationChanged(s.GetSelectedItem().value); }">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width:5%;">

                                    </td>
                                    <td style="width:40%;">
                                        <label id="lblCustomer2" style="font-weight: normal;">
                                            Customer Name</label>
                                        <br />
                                        <dx:ASPxComboBox ID="ddlPOSCustomer" runat="server" CssClass="form-control" ClientInstanceName="ddlCustomerPOS">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width:15%;vertical-align:bottom;">
                                        <button class="btn btn-toolbar" type="button" style="margin-left: -25px; font-size: 15px; color: #FFF; background-color: #7dab49;"
                                            id="btnAddCustomer" onclick="ShowCustomerPopup();">
                                            ...
                                        </button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="col-md-12 gray-bg">
                    <div class="col-md-6 delivery_main">
                        <div class="btn-bg">
                            <asp:HiddenField runat="server" ID="hfCustomerType" Value="Takeaway" />
                            
                            <div class="box active" id="lnkDelivery" aria-disabled="true" style="width:98%;">
                                <span>
                                    <img src="../images/delivery.png" alt="" /></span>
                                Delivery
                                    <label id="lblCounter2"></label>
                            </div>
                            <div id="divCustomer" style="min-width: 190%; z-index: 5000; top: 5%; left: 5%; position: absolute; background-color: #c6c6c6; padding: 0px 10px 10px 10px; border: 1px solid #000; max-width: 190%; visibility: hidden;"
                                class="container">
                                <div class="row">
                                    <div class="col-md-12 new-col">
                                        <p style="margin: 10px 40px 0px 10px;">Customer Information</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-12" style="margin-top: 15px;">
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Card ID
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtCustomerCardNo" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Name <span style="color:red;font-weight:bold;font-size:15px;">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtCustomerName" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Primary Contact <span style="color:red;font-weight:bold;font-size:15px;">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtPrimaryContact" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Other Contact
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtOtherContact" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Email
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtEmail" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />
                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Address <span style="color:red;font-weight:bold;font-size:15px;">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtCustomerAddress" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        CNIC
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtCustomerCNIC" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>

                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Opening Amount
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtOpeningAmount" runat="server" class="form-control" style="font-size: 15px; height: 28px;"
                                                        onkeypress="return onlyDotsAndNumbers(this,event);" value="0" />
                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Nature
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtNature" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />
                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Date of Birth
                                                    </label>
                                                </div>
                                                <div class="col-md-7" style="margin-top: 7px;">
                                                    <asp:TextBox ID="txtCustomerDOB" runat="server" class="form-control" Style="font-size: 15px; height: 28px;"  />
                                                </div>
                                                <div class="col-md-1" style="margin: 7px -20px;">
                                                    <input type="image" id="ibtnCustomerDOB" src="../images/calender.png" style="width: 33px" />
                                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtCustomerDOB"
                                                        PopupButtonID="ibtnCustomerDOB" Format="dd-MMM-yyyy" PopupPosition="TopRight" CssClass="calender">
                                                    </cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                                <div class="col-md-3 btn-mar">
                                                    <button class="btn btn-toolbar" type="button" style="min-width: 100px; font-size: 15px; color: #FFF; background-color: #3c8d75;"
                                                        id="btnSaveCustomer" onclick="SaveCustomer();">
                                                        Save</button>

                                                </div>
                                                <div class="col-md-4 btn-mar">
                                                    <button class="btn btn-toolbar" type="button" style="min-width: 100px; font-size: 15px; color: #FFF; background-color: #f94d72;"
                                                        id="btnCancelCustomer" onclick="CancelCustomer();">
                                                        Cancel</button>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-md-6" style="border-left: 1px solid #dadada;">

                                            <div class="col-md-12" style="margin-top: 7px;">
                                                <input type="text" id="txtCustomerSearch" class="form-control" style="font-size: 18px;"
                                                    placeholder="Search" />
                                            </div>
                                            <div class="col-md-12 table-top">
                                                <div class="table-bar">
                                                    <div class="emp-table scrolla" style="height: 400px;">
                                                        <table class="table table-striped table-bordered table-hover table-condensed cf">
                                                            <thead class="cf head" style="background-color: #7dab49;">
                                                                <tr>
                                                                    <th class="numeric table-text-head">Customer Name
                                                                    </th>
                                                                    <th class="numeric table-text-head">Card Id
                                                                    </th>
                                                                    <th class="numeric table-text-head">Contact No
                                                                    </th>
                                                                    <th class="numeric table-text-head">Address
                                                                    </th>
                                                                    <th colspan="2" class="numeric table-text-head" style="align-content: center;">Action
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="tbl-customers">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <!--2nd level start-->
                        <div class="bg-w scrolla">
                            <div class="pad" id="dv_lstCategory">
                            </div>
                        </div>
                        <!--2nd level ends-->
                    </div>
                    <div class="col-md-6 table-top item_pd_left">
                        <div class="table-bar">
                            <div class="emp-table scrolla" style="height: 240px;">
                                <table class="table table-striped table-bordered table-hover table-condensed cf">
                                    <thead class="cf head" style="background-color: #7dab49;">
                                        <tr>
                                            <th class="numeric table-text-head">ITEM(S)
                                            </th>
                                            <th class="table-text-head" style="text-align: center;" colspan="3">QTY
                                            </th>
                                            <th class="numeric table-text-head">PRICE
                                            </th>
                                            <th class="numeric table-text-head">AMOUNT
                                            </th>
                                            <th class="numeric table-text-head" colspan="2">ACTION
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
                    <div class="col-md-6 item_pd_left">
                        <div class="col-md-12" style="padding: 3px 0px 3px 0px; font-size: medium">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-3" id="dvDealQty" style="display: none;">
                                    <input type="text" id="txtDealQty" runat="server"
                                        class="form-control" placeholder="Deal Qty" onkeypress="return onlyNumbers(this,event);" />
                                </div>
                                <div class="col-md-2" id="dvDealUpdate" style="display: none;">
                                    <button type="button" runat="server"
                                        class="btn btn-toolbar" id="btnDealUpdate" onclick="UpdateDeal();">
                                        Update</button>
                                </div>
                                <div class="col-md-3" id="dvClosingStock" style="display: none">
                                    <div class="col-md-5">
                                        <label id="lblClosing" style="font-weight: normal;">
                                            Closing</label>
                                    </div>
                                    <div class="col-md-7">
                                        <input type="text" id="txtClosingStock" disabled="disabled"
                                            class="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 item_pd_left">
                        <div class="col-md-12" style="background-color: #7dab49; padding: 3px 0px 3px 30px; color: #fff; font-size: medium">
                            <div class="row">
                                <div class="col-md-6">
                                    Order No : 
                                        <label id="OrderNo2">N/A</label>
                                        <label id="OrderNo1" style="visibility:hidden;">N/A</label>
                                </div>
                                <div class="col-md-4">
                                    Net Amount : 
                                    <label id="lblTotalGrossAmount">0.00</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="col-md-12 item_pd_top" style="padding-right: 0px;">
                        <div class="col-md-6" style="padding-right: 22px">
                            <div class="bg-product scrolla col" style="padding-bottom: 0px;">
                                <div class="pad" style="margin-top: 0px;" id="dv_lstProducts">
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="col-md-6 item_pd_left">
                            <div class=" btn-bg">
                                <div class="box-last" onclick="NewOrder();">
                                    New Order<br />
                                    <span>
                                        <img src="../images/new-icon.png" alt="" /></span>
                                </div>
                                <div class="box-last" id="dvHold">
                                    Hold Order<br />
                                    <span>
                                        <img src="../images/pause.png" alt="" /></span>
                                </div>
\                               <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 table-top item_pd_left" style="padding-top: 0px; padding-bottom: 10px;">
                            <div class="table-bar">
                                <div class="col-md-12 vac-det " id="dvPendingBills">
                                    <div style="background-color: #7dab49; padding: 5px 0px 5px 30px; color: #fff;">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width:25%;">
                                                    PENDING BILLS
                                                </td>
                                                <td style="width:75%;">
                                                    <input type="text" id="txtRemarks" class="form-control" style="font-size: 18px;width:95%;" placeholder="Order Notes" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="height-180 scrolla peb" style="width: 100%;">
                                        <table class="pbill" id="tbl-pending-bills" style="width: 100%;">
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <label id="DealPrice" style="display: none">0</label>
        </div>
        <%-- Order Print Information --%>

        <% Response.WriteFile("~/Forms/rptOrder.htm");%>

        <%-----------------------------%>

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
                                                G.S.T @<%=hfSalesTax.Value %> % :
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
        <script src="../js/jquery-1.10.2.js"></script>
        <script src="../js/jquery-2.0.2.min.js"></script>
        <% Response.WriteFile("~/Forms/popUpPayment.aspx");%>
        <%-- On Payment click --%>
        <% Response.WriteFile("~/Forms/popUpPayment2.aspx");%>
        <%-- On Print Invoice click --%>
        <% Response.WriteFile("~/Forms/rptSalesSummary.htm");%>
        <% Response.WriteFile("~/Forms/rptSalesDetail.htm");%>
        <% Response.WriteFile("~/Forms/rptItemSalesDetail.htm");%>
        <% Response.WriteFile("~/Forms/popUpUserValidation.aspx");%>
        <% Response.WriteFile("~/Forms/popUpDeletionConfirmation.aspx");%>
        <% Response.WriteFile("~/Forms/popUpDecreaseConfirmation.aspx");%>
        <script type="text/javascript" src="../AjaxLibrary/moment-with-locales.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/jQuery.print.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/zebra_dialog.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/jquery.lightbox_me.js"></script>
        <script src="../js/plugins/Block/jquery.blockUI.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/order-pos-centralized.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/PrintInvoice.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/Reports.js"></script>
    </form>
</body>
</html>
