

function waitLoading() {
    new $.Zebra_Dialog('<strong>Loading</strong>', {
        'buttons': false,
        'position': ['top + 150'],
        'auto_close': 500
    });
}


$(document).ready(function () {

   
    waitLoading();
   
    $(".nav-ic img").click(function () {
        $(".exit-text").slideToggle("slow", function () {
        });
    });
    
    GetPendingBills();

    

});


//#region Sale Force
function loadSaleForce() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmKitchenOrderTaking.aspx/LoadSaleForce", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                
                success: addSaleForce


            }
        );
}
function addSaleForce(saleForce) {

    saleForce = JSON.stringify(saleForce);
    var result = jQuery.parseJSON(saleForce.replace(/&quot;/g, '"'));

    saleForce = eval(result.d);

    // var opt = document.createElement("option");

    var listItems = "";
    for (var i = 0; i < saleForce.length; i++) {


        listItems += "<option value='" + saleForce[i].USER_ID + "'>" + saleForce[i].USER_NAME + "</option>";
    }

    $("#ddlOrderBooker").html(listItems);
}
//#endregion Sale Force

function GetPendingBill(saleInvoiceMasterId) {

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmKitchenOrderTaking.aspx/GetPendingBill", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'saleInvoiceMasterId':'" + saleInvoiceMasterId + "'}",
                success: LoadPendingBill
            }
        );
}
function LoadPendingBill(products) {

    products = JSON.stringify(products);
    var result = jQuery.parseJSON(products.replace(/&quot;/g, '"'));
    products = eval(result.d);

    
    $("#tble-ordered-products").empty();

    for (var i = 0, len = products.length; i < len; ++i) {
        addProductToOrderedProduct(products, i, $("#OrderNo1").text());
    }
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

}




//Products Grid---------------------------------------------------------------\\



function addProductToOrderedProduct(lstProducts, i, invoiceId) {

    if (lstProducts[i].DESC != "") {
        var row = $('<tr ><td class="table-text2">' + lstProducts[i].SECTION + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '(' + lstProducts[i].DESC + ')' + '</td><td  style="text-align: center;"><input type="text"  style="text-align: center;" size="2" disabled="true" value="' + lstProducts[i].QTY + '" ></td></tr>');
    }
    else {
        var row = $('<tr ><td class="table-text2">' + lstProducts[i].SECTION + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME +  '</td><td  style="text-align: center;"><input type="text"  style="text-align: center;" size="2" disabled="true" value="' + lstProducts[i].QTY + '" ></td></tr>');

    }
    $("#tble-ordered-products").append(row);
}


//-----------------------------------------------------------------------------\\

function storeTblValues() {
    var tableData = new Array();
    $('#tble-ordered-products tr').each(function (row, tr) {

        tableData[row] = {
            "SKU_ID": $(tr).find('td:eq(0)').text(),
            "SKU_NAME": $(tr).find('td:eq(1)').text(),
            "QTY": $(tr).find('td:eq(3) input').val(),
            "T_PRICE": $(tr).find('td:eq(5)').text(),
            "AMOUNT": $(tr).find('td:eq(6)').text(),
            //7 column is of delete button
            "PRNTR": $(tr).find('td:eq(8)').text(),
            "PR_COUNT": $(tr).find('td:eq(9)').text(),
            "SEC_ID": $(tr).find('td:eq(10)').text(),
            "SECTION": $(tr).find('td:eq(11)').text(),
            "H": $(tr).find('td:eq(12)').text(),
            "VOID": $(tr).find('td:eq(13)').text(),
            "CAT_ID": $(tr).find('td:eq(14)').text(),
            "IS_DESC": $(tr).find('td:eq(16)').text(),
            "DESC": $(tr).find('td:eq(17)').text()

        }
    });
    return tableData;
}

function GetPendingBills() {
    if (document.getElementById('hfPageType').value == "Delivery Order") {
        $.ajax
                (
                    {
                        type: "POST", //HTTP method
                        url: "frmDeliveryOrderTerminal.aspx/SelectPendingBills", //page/method name
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: LoadPendingBills
                    }
                );
    }
    else {
        $.ajax
                (
                    {
                        type: "POST", //HTTP method
                        url: "frmKitchenOrderTaking.aspx/SelectPendingBills", //page/method name
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: LoadPendingBills
                    }
                );
    }

}

function LoadPendingBills(pendingBills) {

    pendingBills = JSON.stringify(pendingBills);
    pendingBills = jQuery.parseJSON(pendingBills.replace(/&quot;/g, '"'));
    pendingBills = eval(pendingBills.d);

    $("#tbl-pending-bills").empty();

    for (var i = 0, len = pendingBills.length; i < len; i++) {
        if (document.getElementById('hfPageType').value == "Delivery Order") {

            var row = $('<tr ><td style="display:none;">' + pendingBills[i].INVOICE_ID + '</td><td class="leftval">' + pendingBills[i].TABLE_NO + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="font-size: 12px; color: red;">' + diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td style="display:none;" class="leftval">' + pendingBills[i].ORDER_NO + '</td><td class="leftval">' + pendingBills[i].SERVICE_TYPE + '</td><td onclick="ShowBill(this);" class="rightval"><a><span style="margin-left: 5px; cursor: pointer;" class="fa fa-pencil-square-o"></span></a></td></tr>');
        }
        else {
            var row = $('<tr ><td style="display:none;">' + pendingBills[i].INVOICE_ID + '</td><td style="display:none;" class="leftval">' + pendingBills[i].TABLE_NO + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="font-size: 12px; color: red;">' + diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td class="leftval">' + pendingBills[i].ORDER_NO + '</td><td class="leftval">' + pendingBills[i].SERVICE_TYPE + '</td><td onclick="ShowBill(this);" class="rightval"><a><span style="margin-left: 5px; cursor: pointer;" class="fa fa-pencil-square-o"></span></a></td><td onclick="SaveInvoice(this);" class="rightval"><a><span style="margin-left: 5px; cursor: pointer;" class="fa fa-times"></span></a></td></tr>');

        }

        $('#tbl-pending-bills').append(row);
    }


}

//Call on Pending Bills Row Click, And on HoldOrder Save When Takeaway
function ShowBill(obj) {

    var tr = $(obj).parent();

    $("#OrderNo1").text($(tr).find("td:eq(0)").text());
    $("#MaxOrderNo").text($(tr).find("td:eq(5)").text());
    $("#TableNo1").text($(tr).find("td:eq(4)").text());
    
    if ($(tr).find("td:eq(2)").text() != "null") {
        $('#txtCoverTable').val($(tr).find("td:eq(2)").text());
    }
    GetPendingBill($(tr).find("td:eq(0)").text());
}


function Error(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Error', 'type': 'error' });
}

function SaveInvoice(obj) {
    
    var tr = $(obj).parent();

    $.ajax
    (
        {
            type: "POST", //HTTP method
            url: "frmKitchenOrderTaking.aspx/UpdateInvoice", //page/method name
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ InvoiceId: $(tr).find("td:eq(0)").text() }),
            success: InvoiceSaved,
            error: function () { Error("An error occurred"); }
        }
    );
}

function InvoiceSaved() {

    Clear();
    GetPendingBills();
}

function Clear() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');


    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $("#MaxOrderNo").text("N/A");
    $("#txtCoverTable").val('');

   // $("select#ddlOrderBooker").prop('selectedIndex', 0);
}

function ClearOnCancel() {
    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');


    $("#hfTableNo").val("");
    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $("#MaxOrderNo").text("N/A");
    $("#txtCoverTable").val('');

   // $("select#ddlOrderBooker").prop('selectedIndex', 0);
}

//#region Time Calculation

function BillTime() {

    var refresh = 1000; // Refresh rate in milli seconds

    mytime = setTimeout('display_ct()', refresh)

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





