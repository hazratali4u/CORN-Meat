<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="frmOrderShifting.aspx.cs"
    Inherits="Forms.FormsfrmOrderShifting"
    EnableEventValidation="false" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!doctype html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" charset="utf-8" />
    <title>CORN :: Order Shifting</title>
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

        <asp:HiddenField runat="server" ID="hfIsCoverTable" />
        <asp:HiddenField runat="server" ID="hfLocationName" />
        <asp:HiddenField runat="server" ID="hfAddress" />
        <asp:HiddenField runat="server" ID="hfPhoneNo" />
       
        <asp:HiddenField runat="server" ID="hfInvoiceId" />
        <asp:HiddenField runat="server" ID="hfCurrentWorkDate" />
       
        <asp:HiddenField runat="server" ID="hfCustomerId" Value="0" />
        <%-- Used for Toggle color on row Click --%>

        <%--for Yes and no's--%>
        <asp:HiddenField ID="hfUserId" Value="" runat="server" />

       

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
                        <div class="order-tab" style="background: white;">
                           
                        </div>
                        <div class="order-tab" style="background: white;">
                        </div>
                        <div class="order-tab" style="background: white;">
                            <label id="lblSaleForce" style="font-weight: normal;">
                                Order Taker</label>
                            <br />
                            <span class="order-tab-blk">
                                <asp:DropDownList runat="server" ID="ddlOrderBooker" Width="88%">
                                </asp:DropDownList>
                            </span>
                        </div>
                        <div class="order-tab" style="background: white;">
                            <label id="lblCoverTable" style="font-weight: normal;">
                                Cover Table</label>
                            <br />
                            <span class="order-tab-blk">
                                <asp:TextBox runat="server" ID="txtCoverTable" Width="88%" Height="25px"
                                    onkeypress="return onlyNumbers(this,event);"></asp:TextBox>

                            </span>
                        </div>



                        <div class="clear"></div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="col-md-12 gray-bg">
                    <div class="col-md-6" >
                        <div class="btn-bg">
                            <asp:HiddenField runat="server" ID="hfCustomerType" Value="Dine In" />

                            <div class="box" id="lnkTakeaway" onclick="lnkCustomerType(this, 0);">
                                <span>
                                    <img src="../images/Cus.png" alt="" /></span>
                                Takeaway
                                    <label id="lblCounter1"></label>
                            </div>
                            <div class="box" id="lnkDelivery" onclick="lnkCustomerType(this, 0);">

                                <span>
                                    <img src="../images/delivery.png" alt="" /></span>
                                Delivery
                                    <label id="lblCounter2"></label>
                            </div>
                            <div class="box active" id="lnkDineIn" onclick="lnkCustomerType(this, 0);">

                                <span>
                                    <img src="../images/dine.png" alt="" /></span>
                                Dine In
                                    <label id="lblCounter3"></label>
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
                                                        Name
                                                    </label>
                                                </div>
                                                <div class="col-md-8" style="margin-top: 7px;">
                                                    <input type="text" id="txtCustomerName" runat="server" class="form-control" style="font-size: 15px; height: 28px;" />

                                                </div>
                                                <div class="col-md-4" style="margin-top: 7px;">
                                                    <label style="font: bold 17px sans-serif;">
                                                        Primary Contact
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
                                                        Address
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
                        <div class="bg-w scrolla" style="height:200px">
                            <div class="col-md-12" id="dvPendingBills">
                                    <div style="background-color: #7dab49; padding: 5px 0px 5px 30px; color: #fff;">
                                        PENDING BILLS
                                    </div>
                                       <table class="pbill" id="tbl-pending-bills" style="width: 100%;">
                                        </table>
                                </div>
                            
                           
                        </div>
                         <div class="clear">
                        </div>
                         <div class="col-md-12" style="background-color: #7dab49; padding: 3px 0px 3px 30px; color: #fff; font-size: medium">
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
                                
                                </div>


                            </div>
                        </div>
                        <!--2nd level ends-->
                    </div>
                    <div class="col-md-6 table-top">
                       
                            <div class="emp-table" style="height: 180px;">
                               <fieldset>
                                   <legend><label class="form-control"> Shifted to </label></legend>
                              
                            <asp:HiddenField runat="server" ID="hfShiftType" Value="Dine In" />

                            <div class="box" id="lnkTakeaway2" onclick="lnkCustomerType2(this);" style="display:none;">
                                <span>
                                    <img src="../images/Cus.png" alt="" /></span>
                                Takeaway
                            </div>
                            <div class="box" id="lnkDelivery2" onclick="lnkCustomerType2(this);" style="display:none;">

                                <span>
                                    <img src="../images/delivery.png" alt="" /></span>
                                Delivery
                            </div>
                            <div class="box active" id="lnkDineIn2" onclick="lnkCustomerType2(this);" style="display:none;">

                                <span>
                                    <img src="../images/dine.png" alt="" /></span>
                                Dine In
                            </div>
                                    </fieldset> 
                            </div>
                        <div class=" btn-bg">
                               

                                <div class="btn btn-info" id="dvHold" >
                                    Shift Order<br />
                                    <span>
                                        <img src="../images/pause.png" alt="" /></span>
                                </div>
                                 <div class="btn btn-danger" onclick="NewOrder();">
                                    Canel<br />
                                    <span>
                                        <img src="../images/new-icon.png" alt="" /></span>
                                </div>

                                <div class="clear">
                                </div>
                            </div>

                    </div>
                    <div class="clear">
                    </div>
                    <div class="col-md-6">
                       
                    </div>
                    <div class="col-md-6">
                       
                    </div>
                    <div class="clear">
                    </div>
                    <div class="col-md-12" style="padding-right: 0px;">
                        
                        <div class="col-md-6" style="padding-right: 22px">
                            <div class="bg-product scrolla col" style="padding-bottom: 0px;">
                               <div class="col-md-12" style="border-right: 1px solid cadetblue; display:none" id="dvTableList">
                                    <div style="background-color: #7dab49; padding: 5px 0px 5px 30px; color: #fff;">
                                        VACANT TABLES
                                    </div>
                                        <div class="pad" id="dv_lstTable">
                                        </div>
                                        <asp:HiddenField runat="server" ID="hfTableNo" />
                                        <asp:HiddenField ID="hfTableId" Value="0" runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="col-md-6" style="padding-left: 20px;">
                           
                        </div>
                        <div class="col-md-6 table-top" style="padding-top: 0px; padding-bottom: 10px; padding-left: 5px;">
                            
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
        

        <script src="../js/jquery-1.10.2.js"></script>
        <script src="../js/jquery-2.0.2.min.js"></script>


        <script type="text/javascript" src="../AjaxLibrary/moment-with-locales.js"></script>
        <script type="text/javascript" src="../AjaxLibrary/zebra_dialog.js"></script>
        <script src="../js/plugins/Block/jquery.blockUI.js"></script>

        <script type="text/javascript" src="../AjaxLibrary/orderShifting.js"></script>
    </form>
</body>
</html>
