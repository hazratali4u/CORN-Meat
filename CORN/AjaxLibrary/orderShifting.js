

function waitLoading(msg) {
    new $.Zebra_Dialog('<strong>' + msg + '</strong>', {
        'buttons': false,
        'position': ['top + 120'],
        'auto_close': 600
    });
}

function doPostbackTables() {
    if (document.getElementById("hfShiftType").value == "Dine In") {
        GetPendingTables();
    }
}

//========== #region Load===================\\

//======Sale Force
    function loadSaleForce() {
      $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderShifting.aspx/LoadSaleForce", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ customerType: document.getElementById("hfShiftType").value }),
                    success: addSaleForce
                }
            );
    
}
    function addSaleForce(saleForce) {

    saleForce = JSON.stringify(saleForce);
    var result = jQuery.parseJSON(saleForce.replace(/&quot;/g, '"'));
    saleForce = eval(result.d);


    var listItems = "";
    for (var i = 0; i < saleForce.length; i++) {
        listItems += "<option value='" + saleForce[i].USER_ID + "'>" + saleForce[i].USER_NAME + "</option>";
    }

    $("#ddlOrderBooker").html(listItems);

}

//=======Tables
    function GetPendingTables() {
            $.ajax
                    (
                        {
                            type: "POST", //HTTP method
                            url: "frmOrderShifting.aspx/SelectPendingTables", //page/method name
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: LoadPendingTables,
                            error: OnError
                        }
                    );
        }
    function LoadPendingTables(pendingTables) {


            var dv_lstTable = document.getElementById("dv_lstTable");
            while (dv_lstTable.hasChildNodes()) {
                dv_lstTable.removeChild(dv_lstTable.lastChild);
            }

            pendingTables = JSON.stringify(pendingTables);
            var pendingTables = jQuery.parseJSON(pendingTables.replace(/&quot;/g, '"'));
            pendingTables = eval(pendingTables.d);

            var tableId = 0;
            for (var i = 0, len = pendingTables.length; i < len; ++i) {
                if (i == 0) {
                    tableId = pendingTables[i].TABLE_ID;
                }
                createTableButtons(pendingTables[i].TABLE_NO, pendingTables[i].TABLE_ID);
            }

        }
    function createTableButtons(value, id) {
            
            var element = document.createElement("input");
            element.setAttribute("type", "button");
            element.setAttribute("value", value);
            element.setAttribute("name", value);
            element.setAttribute("id", id);
            element.setAttribute("class", "box-last-vc");
            element.onclick = function () { // Note this is a function

                var elementId = this.id;//for getting Table id in loop
                var flag = true;

                $('#tbl-pending-bills tr').each(function (row, tr) {
                    if ($(tr).find('td:eq(14)').text() == elementId) {

                        flag = false;

                        $("#hfTableId").val(0);
                        $("#hfTableNo").val("");

                        Error("This Table is already Hold");

                        var elementButton = document.getElementById(elementId);
                        elementButton.parentNode.removeChild(elementButton);

                        return;
                    }
                });

                if (flag) {
                    $("#hfTableId").val(this.id);
                    $("#hfTableNo").val(this.value);
                    changeTableClass(this);
                }
            };

            var dv_lstTable = document.getElementById("dv_lstTable");
            dv_lstTable.appendChild(element);
        }

    function changeTableClass(btn) {
        var a, n;
        var a = document.getElementById("dv_lstTable").children;
        for (n = 0; n < a.length; n++) {
            a[n].style["background-color"] = "#d4def7";
        }
        btn.style["background-color"] = "#53b4b5";

    }


//=======Pending Bills
    function GetPendingBills() {
            $.ajax
                    (
                        {
                            type: "POST", //HTTP method
                            url: "frmOrderShifting.aspx/SelectPendingBills", //page/method name
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ customerType: document.getElementById("hfCustomerType").value }),
                            success: LoadPendingBills
                        }
                    );
        }
    function LoadPendingBills(pendingBills) {

            pendingBills = JSON.stringify(pendingBills);
            pendingBills = jQuery.parseJSON(pendingBills.replace(/&quot;/g, '"'));
            pendingBills = eval(pendingBills.d);
            $("#tbl-pending-bills").empty();
           
            for (var i = 0, len = pendingBills.length; i < len; i++) {
                if (pendingBills[i].INVOICE_ID > 0) {

                    var row = $('<tr ><td style="display:none;">' + pendingBills[i].INVOICE_ID + '</td><td class="leftval">' + pendingBills[i].TABLE_NO + '</td><td class="rightval">' + pendingBills[i].AMOUNTDUE + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT_TYPE + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="display:none;">' + pendingBills[i].orderBookerId + '</td><td style="display:none;">' + pendingBills[i].approvedBy + '</td><td style="display:none;">' + pendingBills[i].SERVICE_CHARGES + '</td><td style="font-size: 12px; color: red;">' + diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td style="display:none;">' + pendingBills[i].CUSTOMER_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].ORDER_NO + '</td><td style="display:none;">' + pendingBills[i].TABLE_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].CARD_NO + '</td><td style="display:none;">' + pendingBills[i].CARD_TYPE_ID + '</td><td style="display:none;">' + pendingBills[i].CARD_POINTS + '</td><td style="display:none;">' + pendingBills[i].PURCHASING + '</td><td style="display:none;">' + pendingBills[i].AMOUNT_LIMIT + '</td></tr>');
                }
                $('#tbl-pending-bills').append(row);

            }
            $("#lblCounter1").text("(" + 0 + ")");
            $("#lblCounter2").text("(" + 0 + ")");
            $("#lblCounter3").text("(" + 0 + ")");

            if (pendingBills.length != "0") {
                $("#lblCounter1").text("(" + pendingBills[0].Takeaway + ")");
                $("#lblCounter2").text("(" + pendingBills[0].Delivery + ")");
                $("#lblCounter3").text("(" + pendingBills[0].DineIn + ")");
            }
        }

//========== #endregion Load===================\\

    $(document).ready(function () {

   

    $('#txtCustomerSearch').keyup(function () {

        if ($('#txtCustomerSearch').val().length > 3) {
            LoadAllCustomers();
        }
        else if ($('#txtCustomerSearch').val().length < 4) {
            $("#tbl-customers").empty();
        }
    });

    waitLoading('Loading');

    $(".nav-ic img").click(function () {
        $(".exit-text").slideToggle("slow", function () {
        });
    });

    GetPendingBills();
    GetPendingTables();
    lnkCustomerType(null, document.getElementById("hfCustomerType").value);

    loadSaleForce();

    //On Pending Bill row Click
    $("#tbl-pending-bills").delegate("tr", "click", function () {

        ShowBill(this);
    });


});

//#region Service Type Call in ShowBill
function activeLink() {

    $("#lnkDineIn").removeClass("active");
    $("#lnkTakeaway").removeClass("active");
    $("#lnkDelivery").removeClass("active");

    if ($("#TableNo1").text() == "DLY") {
        $("#lnkDelivery").addClass("active");
        document.getElementById("hfCustomerType").value = "Delivery";

    } else if ($("#TableNo1").text() == "TKY") {

        $("#lnkTakeaway").addClass("active");
        document.getElementById("hfCustomerType").value = "Takeaway";

    } else {

        $("#lnkDineIn").addClass("active");
        document.getElementById("hfCustomerType").value = "Dine In";
    }
}


///Qty [3] ,C1 [12], IS Void [13],Deal Price [21],A_Price[20], Deal ID [21], C2 [23], Deal Qty [27] ,Cat Qty [28], ,Cat Choice [29], Deal_name [32]
///UOM_ID": (33),"intStockMUnitCode": 34, "Stock_to_SaleOperator": 35, "Stock_to_SaleFactor": 36


function NewOrder() {

    GetPendingBills();

    $("#hfTableNo").val("");
    $("#hfTableId").val("0");
    $("#hfCustomerId").val("0");
    if (document.getElementById("hfShiftType").value == "Dine In") {
        $("#TableNo1").text("N/A");
    }
    $("#OrderNo1").text("N/A");
    $("#txtCoverTable").val('');
    $("select#ddlOrderBooker").prop('selectedIndex', 0);
    $("#txtTakeawayCustomer").val('');
    
    doPostbackTables();
}


//Call on Pending Bills Row Click, And on HoldOrder Save When Takeaway
function ShowBill(tr) {

   

    $("#OrderNo1").text($(tr).find("td:eq(0)").text());
    $("#hfTableId").val($(tr).find("td:eq(14)").text());

    $("#hfTableNo").val($(tr).find("td:eq(1)").text());

    $("#txtTakeawayCustomer").val($(tr).find("td:eq(1)").text());


    $("#TableNo1").text($(tr).find("td:eq(10)").text());

    $("#hfCustomerId").val($(tr).find("td:eq(11)").text());
    $("#hfCustomerNo").val($(tr).find("td:eq(15)").text());

    if ($(tr).find("td:eq(5)").text() != "null") {
        $('#txtCoverTable').val($(tr).find("td:eq(5)").text());
    }



    activeLink(); //Active css of Main Buttons on selection of pending bills

   
}

function confirmation() {
    if (confirm("Are you sure you want to Shift Order?"))
        return true;
    else return false;
}
$('#dvHold').click(function (e) {
    if (confirmation()) {
       // $.blockUI({ message: $('#question'), css: { width: '275px' } });

        if ($("#OrderNo1").text() == "N/A") {
            Error("Plz Select Order");

            return;
        }

        if (($("#lnkDineIn2").attr("class")) == "box active") {
            if (($("#hfTableNo").val() == "") || ($("#hfTableNo").val() == "DLY") || ($("#hfTableNo").val() == "TKY")) {

                Error("Table not selected");

                return;
            }


            if ($("#ddlOrderBooker").val() == null) {

                Error("Please select Order Taker");
                return;
            }

            if ($("#hfIsCoverTable").val() == "1") {

                if (($("#txtCoverTable").val() == "0") || ($("#txtCoverTable").val() == "")) {
                    Error("Cover Table not entered");
                    return;
                }
            }
        }
        if (($("#lnkTakeaway2").attr("class")) == "box active") {

            if ($("#ddlOrderBooker").val() == null) {

                Error("Please select Order Taker");
                return;
            }
        }
        if (($("#lnkDelivery2").attr("class")) == "box active") {

            if ($("#ddlOrderBooker").val() == null) {

                Error("Please select Delivery Man");
                return;
            }

            if (($("#hfCustomerId").val() == "0")) {

                Error("Please select customer");
                document.getElementById('divCustomer').style.visibility = 'visible';

                return;
            }

        }

        var me = $(this);
        e.preventDefault();

        if (me.data('requestRunning')) {
            return;
        }

        me.data('requestRunning', true);
        $("#dvHold").attr("disabled", true);

        // var Id = $('select#ddlOrderBooker option:selected').val();
        var e = document.getElementById("ddlOrderBooker");
        var Id = e.options[e.selectedIndex].value;
        $.ajax
                ({
                    type: "POST", //HTTP method
                    url: "frmOrderShifting.aspx/ShiftOrder", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ invoiceId: $("#OrderNo1").text(), orderBooker: Id, coverTable: $('#txtCoverTable').val(), customerType: document.getElementById("hfShiftType").value, CustomerID: document.getElementById("hfCustomerId").value, takeAwayCustomer: $("#txtTakeawayCustomer").val(), bookerName: $('select#ddlOrderBooker option:selected').text(), tabId: $("#hfTableId").val() }),
                    success: OrderSaved,
                    error: OnError,
                    complete: function () {
                        $("#dvHold").attr("disabled", false);
                        me.data('requestRunning', false);

                    }
                });
    }
});


function OrderSaved() {


    ClearOrder();

}

function Error(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Error', 'type': 'error' });
}
function Succes(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Success', 'type': 'information' });
}

function OnError(xhr, errorType, exception) {

    var responseText;

    try {
        responseText = jQuery.parseJSON(xhr.responseText);
        $.Zebra_Dialog(responseText.Message, { 'title': 'Error', 'type': 'error' });

    } catch (e) {
        responseText = xhr.responseText;
        $.Zebra_Dialog(responseText, { 'title': 'Error', 'type': 'error' });
    }

}
// #endregion HoldOrder

//#region Time Calculation

function BillTime() {

    var refresh = 5000; // Refresh rate in milli seconds

    mytime = setTimeout('display_ct()', refresh);

    GetPendingBills();

}

//--Used in BillTime();
function display_ct() {
    var strcount
    var d = new Date()

    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());

    document.getElementById('ct').innerHTML = h + ":" + m;
    tt = BillTime();
}

//--Used In display_ct();
function addZero(i) {//Used For Adding 0 Before Hour When Hour is Less than 10
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//--Used In GetPendingBills();
function diff(start, end) {//Get The updated Time For Hold Orders 
    start = start.split(":");
    end = end.split(":");
    var startDate = new Date(0, 0, 0, start[0], start[1], 0);
    var endDate = new Date(0, 0, 0, end[0], end[1], 0);
    var diff = endDate.getTime() - startDate.getTime();
    var hours = Math.floor(diff / 1000 / 60 / 60);
    diff -= hours * 1000 * 60 * 60;
    var minutes = Math.floor(diff / 1000 / 60);

    return (hours < 10 ? "0" : "") + hours + ":" + (minutes < 10 ? "0" : "") + minutes;
}

//#endregion Time Calculation


//#region Customer

//Called on page Load for Default ServiceType and on Button Click 
//obj2 used for Pageload 
function lnkCustomerType(obj, obj2) {

    var id = "";
    if (obj != null) {
        id = obj.id;
    }
    
    waitLoading('Loading');

    $("#lnkDelivery").removeClass("active");
    $("#lnkTakeaway").removeClass("active");
    $("#lnkDineIn").removeClass("active");

    $("#lnkDelivery2").removeClass("active");
    $("#lnkTakeaway2").removeClass("active");
    $("#lnkDineIn2").removeClass("active");

    $("#txtTakeawayCustomer").val('');


    if (id == "lnkDelivery" || obj2 == "Delivery") {

        $("#lnkDelivery").addClass("active");

        document.getElementById("hfCustomerType").value = "Delivery";

        document.getElementById('lnkDelivery2').style.display = 'none';
        document.getElementById('lnkTakeaway2').style.display = "block";
        document.getElementById('lnkDineIn2').style.display = "block";

    }
    else if (id == "lnkTakeaway" || obj2 == "Takeaway") {
        $("#lnkTakeaway").addClass("active");

        document.getElementById("hfCustomerType").value = "Takeaway";
        document.getElementById('lnkTakeaway2').style.display = 'none';
        document.getElementById('lnkDelivery2').style.display = "block";
        document.getElementById('lnkDineIn2').style.display = "block";

    }
    else if (id == "lnkDineIn" || obj2 == "Dine In") {

        $("#lnkDineIn").addClass("active");

        document.getElementById("hfCustomerType").value = "Dine In";

        document.getElementById('lnkDineIn2').style.display = 'none';
        document.getElementById('lnkDelivery2').style.display = "block";
        document.getElementById('lnkTakeaway2').style.display = "block";

    }
    if (id != "") {
        
        GetPendingBills();
    }
}

function lnkCustomerType2(obj) {
    if ($("#OrderNo1").text() == "N/A") {
        Error("Plz Select Order");

        return;
    }
    $("#lnkDelivery2").removeClass("active");
    $("#lnkTakeaway2").removeClass("active");
    $("#lnkDineIn2").removeClass("active");
    $("#txtTakeawayCustomer").val('');

    if (obj.id == "lnkDelivery2") {

        $("#lnkDelivery2").addClass("active");

        document.getElementById('dvTableNo').style.display = 'block';
        document.getElementById('dvTakeawayCustomer').style.display = "none";

        document.getElementById('divCustomer').style.visibility = 'visible';

        document.getElementById("hfShiftType").value = "Delivery";
        document.getElementById('dvTableList').style.display = "none";

        $("#lblSaleForce").text("Delivery Man");

        $("#hfTableNo").val("");
        $("#hfTableId").val("0");
        $("#TableNo1").text("DLY");
        $("#txtCoverTable").val('');
        $("#txtTakeawayCustomer").val('');

        loadSaleForce();

        $('#txtCustomerSearch').val('');
        $("#tbl-customers").empty();
    }
    else if (obj.id == "lnkTakeaway2") {


        $("#lnkTakeaway2").addClass("active");

        document.getElementById('dvTakeawayCustomer').style.display = 'block';
        document.getElementById('dvTableNo').style.display = "none";

        document.getElementById('divCustomer').style.visibility = 'hidden';

        document.getElementById("hfShiftType").value = "Takeaway";
        document.getElementById('dvTableList').style.display = "none";

        $("#hfTableNo").val("");
        $("#hfTableId").val("0");
        $("#TableNo1").text("TKY");

        $("#txtCoverTable").val('');
        $("#lblSaleForce").text("Order Taker");

        loadSaleForce();
    }
    else if (obj.id == "lnkDineIn2") {


        $("#lnkDineIn2").addClass("active");

        document.getElementById('dvTableNo').style.display = 'block';
        document.getElementById('dvTakeawayCustomer').style.display = "none";

        document.getElementById('divCustomer').style.visibility = 'hidden';

        document.getElementById("hfShiftType").value = "Dine In";

        document.getElementById('dvTableList').style.display = "block";

        $("#lblSaleForce").text("Order Taker");

        loadSaleForce();

    }
    
}
function SaveCustomer() {

    if (ValidateCustomer()) {

        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderShifting.aspx/InsertCustomer", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ cardID: $('#txtCustomerCardNo').val(), CNIC: $('#txtCustomerCNIC').val(), contactNumer: $('#txtPrimaryContact').val(), contactNumer2: $('#txtOtherContact').val(), customerName: $('#txtCustomerName').val(), address: $('#txtCustomerAddress').val(), DOB: $('#txtCustomerDOB').val(), OpeningAmount: $('#txtOpeningAmount').val(), Nature: $('#txtNature').val(), email: $('#txtEmail').val() }),
                    success: ClearCustomer,
                    error: OnError
                }
            );
    }
}
function ValidateCustomer() {
    if ($('#txtCustomerName').val() == "") {
        Error("Must enter Customer Name");

        return false;
    }
    if ($('#txtPrimaryContact').val() == "") {
        Error("Must enter Primary Contact");

        return false;
    }
    if ($('#txtCustomerAddress').val() == "") {
        Error("Must enter Address");

        return false;
    }
    return true;
}

//On KEY UP txtCustomerSearch
function LoadAllCustomers() {
    $.ajax
       (
           {
               type: "POST", //HTTP method
               url: "frmOrderShifting.aspx/LoadAllCustomers", //page/method name
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               data: JSON.stringify({ customerName: $('#txtCustomerSearch').val(), type: "Search" }),
               success: LoadAllCustomer
           }
 );
}
function LoadAllCustomer(customers) {

    customers = JSON.stringify(customers);
    var result = jQuery.parseJSON(customers.replace(/&quot;/g, '"'));

    customers = eval(result.d);

    $("#tbl-customers").empty();

    for (var i = 0, len = customers.length; i < len; i++) {
        var row = $('<tr ><td style="display:none;">' + customers[i].CUSTOMER_ID + '</td><td class="leftval">' + customers[i].CUSTOMER_NAME + '</td><td class="leftval">' + customers[i].CARD_NO + '</td><td class="leftval">' + customers[i].CONTACT_NUMBER + '</td><td class="leftval"  style="display:none;">' + customers[i].EMAIL_ADDRESS + '</td><td style="display:none;">' + customers[i].CNIC + '</td><td class="leftval"  style="display:none;">' + customers[i].REGDATE + '</td><td class="leftval">' + customers[i].ADDRESS + '</td><td style="display:none;" align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td>' + '<td align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-share-square-o"></span></a></td></tr>');
        $('#tbl-customers').append(row);
    }
}

//On Save Customer
function LoadCustomers() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderShifting.aspx/LoadAllCustomers", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ customerName: "", type: "Max" }),
                success: LoadLastCustomer
            }
        );
}
function LoadLastCustomer(customers) {

    customers = JSON.stringify(customers);
    var result = jQuery.parseJSON(customers.replace(/&quot;/g, '"'));

    customers = eval(result.d);

    $("#tbl-customers").empty();

    for (var i = 0, len = customers.length; i < len; i++) {

        var row = $('<tr ><td style="display:none;">' + customers[i].CUSTOMER_ID + '</td><td class="leftval">' + customers[i].CUSTOMER_NAME + '</td><td class="leftval">' + customers[i].CUSTOMER_CODE + '</td><td class="leftval">' + customers[i].CONTACT_NUMBER + '</td><td class="leftval"  style="display:none;">' + customers[i].EMAIL_ADDRESS + '</td><td class="leftval"  style="display:none;">' + customers[i].CNIC + '</td><td class="leftval" style="display:none;">' + customers[i].REGDATE + '</td><td class="leftval">' + customers[i].ADDRESS + '</td><td style="display:none;" align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td>' + '<td align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-share-square-o"></span></a></td></tr>');
        $('#tbl-customers').append(row);
    }

    var table = document.getElementById('tbl-customers');
    var lastRow = table.rows[table.rows.length - 1];

    ShowCustomer2(lastRow);
    HoldOrder();
}


//After Insertion Select Last Customer
function ShowCustomer2(rowIndex) {

    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    document.getElementById('hfTableNo').value = $(rowIndex).find('td:eq(1)').text();//Customer name used For Printing on Unique Section and Print Order

    document.getElementById('divCustomer').style.visibility = 'hidden';

    $("#tbl-customers").empty();
}

//Get Customer Detail on Selection Button on row click
function ShowCustomer(obj) {
   

    var rowIndex = $(obj).parent();

    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    document.getElementById('hfTableNo').value = $(rowIndex).find('td:eq(1)').text();//Customer name used For Printing on Unique Section and Print Order

    document.getElementById('divCustomer').style.visibility = 'hidden';

    $('#txtCustomerSearch').val('');
    $("#tbl-customers").empty();

    HoldOrder();
}
function HoldOrder() {

   
    if (($("#lnkDelivery2").attr("class")) == "box active") {

        if ($("#ddlOrderBooker").val() == null) {

            Error("Please select Delivery Man");
            return;
        }

        if (($("#hfCustomerId").val() == "0")) {

            Error("Please select customer");

            return;
        }
    }

    SaveOrder();

}
function SaveOrder() {

    var Id = $('select#ddlOrderBooker option:selected').val();

    if (($("#lnkDelivery2").attr("class")) == "box active") {

        if (Id == "") {
            Error("Please select Delivery Man");
            return;
        }

        if (($("#hfCustomerId").val() == "0")) {

            Error("Please select customer");

            return;
        }

    }
    $("#dvHold").attr("disabled", true);

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderShifting.aspx/ShiftOrder", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ invoiceId: $("#OrderNo1").text(), orderBooker: Id, coverTable: $('#txtCoverTable').val(), customerType: document.getElementById("hfShiftType").value, CustomerID: document.getElementById("hfCustomerId").value, takeAwayCustomer: $("#txtTakeawayCustomer").val(), bookerName: $('select#ddlOrderBooker option:selected').text(), tabId: $("#hfTableId").val() }),
                success: OrderSaved,
                error: OnError,
                complete: function () {
                    $("#dvHold").attr("disabled", false);
                }

            }
        );

}
//#endregion



//region Clear

//ON Hold Order
    function ClearOrder() {


    GetPendingBills();

    $('#hfCustomerId').val('0');
    $("#hfTableNo").val("");
    $("#hfTableId").val("0");

    $("#txtCoverTable").val('');

    if (document.getElementById("hfShiftType").value == "Dine In") {
        $("#TableNo1").text("N/A");
    }
    $("#OrderNo1").text("N/A");

    $("select#ddlOrderBooker").prop('selectedIndex', 0);

    $("#txtTakeawayCustomer").val('');

    doPostbackTables();


}

//ON Customer 
    function ClearCustomer() {
        $('#txtCustomerCardNo').val('');
        $('#txtPrimaryContact').val('');
        $('#txtOtherContact').val('');
        $('#txtCustomerName').val('');
        $('#txtCustomerAddress').val('');
        $('#txtCustomerCNIC').val('');
        $('#txtCustomerDOB').val('');
        $('#txtCustomerSearch').val('');
        $('#txtEmail').val('');
        $('#txtOpeningAmount').val('');
        $('#txtNature').val('');
        $('#txtBarcode').val('');

        document.getElementById('divCustomer').style.visibility = 'hidden';

        LoadCustomers();

    }
    function CancelCustomer() {

        $('#txtCustomerCardNo').val('');
        $('#txtPrimaryContact').val('');
        $('#txtOtherContact').val('');
        $('#txtCustomerName').val('');
        $('#txtCustomerCNIC').val('');
        $('#txtCustomerDOB').val('');
        $('#txtCustomerAddress').val('');
        $('#txtEmail').val('');
        $('#txtOpeningAmount').val('');
        $('#txtNature').val('');
        $('#txtBarcode').val('');

        // $('#divCustomer').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 'slow');
        document.getElementById('divCustomer').style.visibility = 'hidden';
        // document.getElementById('divCustomer').className = "hideCustomer"; // Fade out

    }

//----------------------------------------------------------

