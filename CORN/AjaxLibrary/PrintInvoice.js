
function ShowHideInvoiceFootGst(Gst) {
    if (Gst == 0) {

        document.getElementById("GstRow").style.display = "none";
    }
    else {
        document.getElementById("GstRow").style.display = "table-row";
    }
}
function ShowHideInvoiceFootDiscount(Discount) {

    if (Discount == "false") {
        document.getElementById("DiscountRow").style.display = "none";

    }
    else {
        document.getElementById("DiscountRow").style.display = "table-row";

    }
}
function ShowHideInvoiceFootTotal(Gst, Discount) {

    if ((Gst == 0) && (Discount == 0)) {

        document.getElementById("GrandTotalRow").style.display = "none";
    }
    else {
        document.getElementById("GrandTotalRow").style.display = "table-row";
    }
}

function PrintDeal(orderedProducts)
{
    
    var uniqueDeals = $.unique(orderedProducts.map(function (d) { return d.I_D_ID; }));
    uniqueDeals = uniqueDeals.sort();
    uniqueDeals = unique(uniqueDeals);


    for (var j = 0; j < uniqueDeals.length; j++) {
        if (uniqueDeals[j] != 0) {
            var count = 0;
            var totalamount = 0;
            $('#tble-ordered-products').find('tr').each(function () {
                if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                    if (uniqueDeals[j] == $(this).find("td:eq(21)").text()) {
                        if (count == 0) {
                            count += 1;
                            totalamount = parseFloat($(this).find("td:eq(20)").text()) * parseFloat($(this).find("td:eq(27)").text());
                            if ($(this).find("td:eq(16)").text()== "true") {

                                var row = $(' <tr><td>' + $(this).find("td:eq(1)").text() + '<br />' + $(this).find("td:eq(17)").text() + '</td><td class="text-right">' + $(this).find("td:eq(3)").text() + '</td><td class="text-right">' + $(this).find("td:eq(20)").text() + '</td><td class="text-right">' + totalamount + '</td></tr>');
                            }
                            else {
                                var row = $(' <tr><td>' + $(this).find("td:eq(1)").text() + '</td><td class="text-right">' + $(this).find("td:eq(3)").text() + '</td><td class="text-right">' + $(this).find("td:eq(20)").text() + '</td><td class="text-right">' + totalamount + '</td></tr>');
                            }
                            $('#invoiceDetailBody').append(row);

                        }
                        return;
                    }
                }
            });
        }
    }
}

//On Print Invoice
function PrintSaleInvoice() {

    $("#InvoiceDate").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));
    $("#CustomerType").text(document.getElementById("hfCustomerType").value);

    // if (document.getElementById("hfRegNo").value == "") {
    //   document.getElementById('RegNo').style.display = "none";
    //}
    // else {
    //    $('#RegNo').show();
    // }
    $("#cashier").text('Served By: ' + $("#user-detail-bold").text());

    if (document.getElementById("hfCustomerType").value == "Takeaway") {

        if ($("#txtCustomer").val() != "") {
            var CDetail = $("#txtCustomer").val();
            $("#CustomerDetail").text(CDetail.substring(0, 42));
        }
        document.getElementById('OrderTakerName').style.display = "none";
        document.getElementById('OrderTaker').style.display = "none";
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
        // document.getElementById('CustomerNameDetail').style.display = "none";
        $('#CustomerDetail').show();
    }
    else if (document.getElementById("hfCustomerType").value == "Delivery") {

        $("#OrderTakerName").text("D-M:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        //   $("#CustomerNameDetail").text("Name & Addr:");

        var CDetail = $("#txtCustomer").val();
        $("#CustomerDetail").text(CDetail.substring(0, 42));

        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        //  $('#CustomerNameDetail').show();
        $('#CustomerDetail').show();
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
    }
    else {


        $("#OrderTakerName").text("O-T:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        $("#CoverTableName").text("Cover Table:");
        $("#CoverTable").text($("#txtCoverTable").val());
        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        $('#CoverTableName').show();
        $('#CoverTable').show();
        document.getElementById('CustomerDetail').style.display = "none";

    }

    $("#BillNo").text($("#OrderNo1").text());

    document.getElementById('OrderInvoice').style.display = "none";
    document.getElementById('OrderInvoiceName').style.display = "none";
    document.getElementById('BillNoName').style.display = "none";
    document.getElementById('BillNo').style.display = "none";
    document.getElementById('CompanyAddress').style.display = "none";
    document.getElementById('CompanyNumber').style.display = "none";
    document.getElementById('imgLogo2').style.display = "none";
  
    $("#SaleInvoiceText").text("Provisional Bill");
    //#region Products Detail

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);

    $('#invoiceDetailBody').empty(); // clear all skus  from invoice

    for (var i = 0, len = orderedProducts.length; i < len; i++) {
        if (orderedProducts[i].I_D_ID == 0) {//For Normal Item Printing
            var qty = orderedProducts[i].QTY;
            if (qty > 0) {
                if (orderedProducts[i].IS_DESC == "true") {

                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '<br />' + orderedProducts[i].DESC + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                else {
                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                $('#invoiceDetailBody').append(row);
            }
        }
    }
    //For Deal Printing
    
    var uniqueDeals = $.unique(orderedProducts.map(function (d) { return d.I_D_ID; }));
    uniqueDeals = uniqueDeals.sort();
    uniqueDeals = unique(uniqueDeals);


    for (var j = 0; j < uniqueDeals.length; j++) {
        if (uniqueDeals[j] != 0) {
            var count = 0;
            var totalamount = 0;
            $('#tble-ordered-products').find('tr').each(function () {
                if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                    if (uniqueDeals[j] == $(this).find("td:eq(21)").text()) {
                        if (count == 0) {
                            count += 1;
                            totalamount = parseFloat($(this).find("td:eq(20)").text()) * parseFloat($(this).find("td:eq(27)").text());
                            if ($(this).find("td:eq(16)").text() == "true") {

                                var row = $(' <tr><td>' + $(this).find("td:eq(32)").text() + '<br />' + $(this).find("td:eq(17)").text() + '</td><td class="text-right">' + $(this).find("td:eq(27)").text() + '</td><td class="text-right">' + $(this).find("td:eq(20)").text() + '</td><td class="text-right">' + totalamount + '</td></tr>');
                            }
                            else {
                                var row = $(' <tr><td>' + $(this).find("td:eq(32)").text() + '</td><td class="text-right">' + $(this).find("td:eq(27)").text() + '</td><td class="text-right">' + $(this).find("td:eq(20)").text() + '</td><td class="text-right">' + totalamount + '</td></tr>');
                            }
                            $('#invoiceDetailBody').append(row);

                        }
                        return;
                    }
                }
            });
        }
    }
           
    //--------------------------------------------------------------------------


    //#endregion Products Detail

    var Gst = 0;

    if ($('#hfServiceCharges').val() == "False") {
        Gst = document.getElementById("hfSalesTax").value;
    }
    else {
        Gst = document.getElementById("txtService2").value;
        $("#GST-text").text("Service Chg @" + Gst + " % :");
    }
    if (Gst == "")
    { Gst = 0; }

    $("#TotalValue").text(parseFloat($("#subTotal").text()).toFixed(2));
    $("#GrandTotal-value").text($("#GrandTotal").text());

    var amountDue = parseFloat($("#subTotal").text()).toFixed(2);

    var text = "";
    //When Discount not enter through print invoice POpUP
    if ($("#lblDiscountTotal2").text() == "0.00") {

        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);
        $("#Discount-text").text(text);
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#GrandTotal-value").text($("#lblPaymentDue2").text());
        $("#Discount-value").text(text);


        ShowHideInvoiceFootDiscount("false");
    }
    else {
        var discount = $("#lblDiscountTotal2").text();
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);

        if ($("#hfDiscountType").val() == "0") {

            var dic = "Discount @" + $("#txtDiscount2").val() + " % :";

            $("#Discount-text").text(dic);
        }
        else {
            $("#Discount-text").text('Discount :');
        }
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#Discount-value").text($("#lblDiscountTotal2").text());
        $("#GrandTotal-value").text($("#lblPaymentDue2").text());

        ShowHideInvoiceFootDiscount("true");
    }

    $("#Gst-value").text(Math.round(Gst, 0));

    ShowHideInvoiceFootGst(Gst);


    ShowHideInvoiceFootTotal(Gst, $("#lblDiscountTotal2").text());

    $("#PayIn-text").text(text);
    $("#PayIn-value").text(text);
    $("#balance-text").text(text);
    $("#balance-value").text(text);
    $("#balance-text2").text(text);
    $("#balance-value2").text(text);
    $("#PayType").text(text);

    $.print("#dvSaleInvoice");
}

//On Payment Save
function PrintPaymentInvoice() {

    $('#OrderInvoice').show();
    $('#OrderInvoiceName').show();
    $('#BillNoName').show();
    $('#BillNo').show();
    $('#CompanyAddress').show();
    $('#CompanyNumber').show();
    $('#imgLogo2').show();

    $("#SaleInvoiceText").text("Sale Bill");
    $("#Cashier").text('Served By: ' + $("#user-detail-bold").text());
    $("#CustomerType").text(document.getElementById("hfCustomerType").value);
    $("#InvoiceDate").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    if (document.getElementById("hfRegNo").value == "") {
        document.getElementById('RegNo').style.display = "none";
    }
    else {
        $('#RegNo').show();
    }

    $("#OrderInvoice").text($("#MaxOrderNo").text());

    if (document.getElementById("hfCustomerType").value == "Takeaway") {
        if ($('select#ddlCustomerPOS option:selected').text() != "") {
            var CDetail = $('select#ddlCustomerPOS option:selected').text();
            $("#CustomerDetail").text(CDetail);
        }
        document.getElementById('OrderTakerName').style.display = "none";
        document.getElementById('OrderTaker').style.display = "none";
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
        $('#CustomerDetail').show();
    }
    else if (document.getElementById("hfCustomerType").value == "Delivery") {

        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        var lstCustomers = document.getElementById("hfCustomers").value;
        lstCustomers = eval(lstCustomers);
        for (var i = 0, len = lstCustomers.length; i < len; ++i) {
            if ($('select#ddlCustomerPOS option:selected').val() == lstCustomers[i].CUSTOMER_ID) {
                $("#CustomerDetail").text(lstCustomers[i].CUSTOMER_NAME + '-' + lstCustomers[i].ADDRESS + '-' + lstCustomers[i].CONTACT_NUMBER);
                break;
            }
        }

        $('#CustomerDetail').show();
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
    }
    else {
        $("#OrderTakerName").text("O-T:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));
        $("#CoverTableName").text("Cover Table:");
        $("#CoverTable").text($("#txtCoverTable").val());
        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        $('#CoverTableName').show();
        $('#CoverTable').show();
        document.getElementById('CustomerDetail').style.display = "none";
    }
    $("#BillNo").text($("#OrderNo1").text());
    //#region Products Detail
    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);
    $('#invoiceDetailBody').empty(); // clear all skus  from invoice
    for (var i = 0, len = orderedProducts.length; i < len; i++) {
        if (orderedProducts[i].I_D_ID == 0) {//For Normal Item Printing
            var qty = orderedProducts[i].QTY;
            if (qty > 0) {
                if (orderedProducts[i].IS_DESC == "true") {
                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '<br />' + orderedProducts[i].DESC + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                else {
                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                $('#invoiceDetailBody').append(row);
            }
        }
    }
    var Gst = 0;
    if ($('#hfServiceCharges').val() == "False") {
        Gst = document.getElementById("hfSalesTax").value;
    }
    else {
        Gst = document.getElementById("txtService").value;
        $("#GST-text").text("Service Chg @" + Gst + " % :");
    }
    if (Gst == "")
    { Gst = 0; }
    $("#TotalValue").text(parseFloat($("#subTotal").text()).toFixed(2));
    $("#GrandTotal-value").text($("#GrandTotal").text());
    var amountDue = parseFloat($("#subTotal").text()).toFixed(2);
    var text = "";
    //When Discount not enter through Print invoice POpUP-----------------
    if ($("#lblDiscountTotal").text() == "0.00") {

        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);
        $("#Discount-text").text(text);
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#GrandTotal-value").text($("#lblPaymentDue").text());
        $("#Discount-value").text(text);
        ShowHideInvoiceFootDiscount("false");
    }
    else {
        var discount = $("#lblDiscountTotal").text();
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);
        if ($("#hfDiscountType").val() == "0") {
            var dic = "Discount @" + $("#txtDiscount").val() + " % :";
            $("#Discount-text").text(dic);
        }
        else {
            $("#Discount-text").text('Discount :');
        }
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#Discount-value").text($("#lblDiscountTotal").text());
        $("#GrandTotal-value").text($("#lblPaymentDue").text());
        ShowHideInvoiceFootDiscount("true");
    }
    if ($('#hfPaymentType').val() == "1") {//For Credit Card Payment
        $("#PayIn-text").text('Payment IN :');
        var cash = $("#txtCashRecieved").val();
        var credit = $("#lblPaymentDue").text();
        var credit = credit - cash;
        if (cash > 0) {
            $("#PayType").text('MOP: Mixed');
            $("#PayIn-value").text($("#txtCashRecieved").val());
            $("#balance-text").text('Credit Card:');
            $("#balance-value").text(credit)
            $("#balance-text2").text('Balance :');
            $("#balance-value2").text(0);
        }
        else {
            $("#PayType").text('MOP: Credit Card');
            $("#PayIn-value").text(credit);
            $("#balance-text").text('Balance :');
            $("#balance-value").text(0)
            $("#balance-text2").text(text);
            $("#balance-value2").text(text);
        }
    }
    else {//For Cash Payment
        $("#PayType").text('MOP: Cash');
        $("#PayIn-text").text('Payment IN :');
        $("#balance-text").text('Change :');
        $("#balance-text2").text(text);
        $("#PayIn-value").text($("#txtCashRecieved").val());
        $("#balance-value").text($("#lblBalance").text());
        $("#balance-value2").text(text);
    }
    ShowHideInvoiceFootGst(Gst);
    ShowHideInvoiceFootTotal(Gst, $("#lblDiscountTotal").text());
    $.print("#dvSaleInvoice");
}
function PrintPaymentDirectInvoice() {
    $('#OrderInvoice').show();
    $('#OrderInvoiceName').show();
    $('#BillNoName').show();
    $('#BillNo').show();
    $('#CompanyAddress').show();
    $('#CompanyNumber').show();
    $('#imgLogo2').show();
    $("#SaleInvoiceText").text("Sale Bill");
    $("#Cashier").text('Served By: ' + $("#user-detail-bold").text());
    $("#CustomerType").text(document.getElementById("hfCustomerType").value);
    $("#InvoiceDate").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));
    if (document.getElementById("hfRegNo").value == "") {
        document.getElementById('RegNo').style.display = "none";
    }
    else {
        $('#RegNo').show();
    }
    $("#OrderInvoice").text('N/A');
    if (document.getElementById("hfCustomerType").value == "Takeaway") {
        if ($('select#ddlCustomerPOS option:selected').text() != "") {
            var CDetail = $('select#ddlCustomerPOS option:selected').text();
            $("#CustomerDetail").text(CDetail);
        }
        document.getElementById('OrderTakerName').style.display = "none";
        document.getElementById('OrderTaker').style.display = "none";
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
        $('#CustomerDetail').show();
    }
    else if (document.getElementById("hfCustomerType").value == "Delivery") {
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        var lstCustomers = document.getElementById("hfCustomers").value;
        lstCustomers = eval(lstCustomers);
        for (var i = 0, len = lstCustomers.length; i < len; ++i) {
            if ($('select#ddlCustomerPOS option:selected').val() == lstCustomers[i].CUSTOMER_ID) {
                $("#CustomerDetail").text(lstCustomers[i].CUSTOMER_NAME + '-' + lstCustomers[i].ADDRESS + '-' + lstCustomers[i].CONTACT_NUMBER);
                break;
            }
        }

        $('#CustomerDetail').show();
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
    }
    else {
        $("#OrderTakerName").text("O-T:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));
        $("#CoverTableName").text("Cover Table:");
        $("#CoverTable").text($("#txtCoverTable").val());
        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        $('#CoverTableName').show();
        $('#CoverTable').show();
        document.getElementById('CustomerDetail').style.display = "none";
    }
    $("#BillNo").text($("#MaxBillNo").text());
    //#region Products Detail
    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);
    $('#invoiceDetailBody').empty(); // clear all skus  from invoice
    for (var i = 0, len = orderedProducts.length; i < len; i++) {
        if (orderedProducts[i].I_D_ID == 0) {//For Normal Item Printing
            var qty = orderedProducts[i].QTY;
            if (qty > 0) {
                if (orderedProducts[i].IS_DESC == "true") {
                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '<br />' + orderedProducts[i].DESC + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                else {
                    var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + parseFloat(orderedProducts[i].QTY).toFixed(3) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].T_PRICE).toFixed(2) + '</td><td class="text-right">' + parseFloat(orderedProducts[i].AMOUNT).toFixed(2) + '</td></tr>');
                }
                $('#invoiceDetailBody').append(row);
            }
        }
    }
    var Gst = 0;
    if ($('#hfServiceCharges').val() == "False") {
        Gst = document.getElementById("hfSalesTax").value;
    }
    else {
        Gst = document.getElementById("txtService").value;
        $("#GST-text").text("Service Chg @" + Gst + " % :");
    }
    if (Gst == "")
    { Gst = 0; }
    $("#TotalValue").text(parseFloat($("#subTotal").text()).toFixed(2));
    $("#GrandTotal-value").text($("#GrandTotal").text());
    var amountDue = parseFloat($("#subTotal").text()).toFixed(2);
    var text = "";
    //When Discount not enter through Print invoice POpUP-----------------
    if ($("#lblDiscountTotal").text() == "0.00") {
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);
        $("#Discount-text").text(text);
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#GrandTotal-value").text($("#lblPaymentDue").text());
        $("#Discount-value").text(text);
        ShowHideInvoiceFootDiscount("false");
    }
    else {
        var discount = $("#lblDiscountTotal").text();
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);
        if ($("#hfDiscountType").val() == "0") {
            var dic = "Discount @" + $("#txtDiscount").val() + " % :";
            $("#Discount-text").text(dic);
        }
        else {
            $("#Discount-text").text('Discount :');
        }
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#Discount-value").text($("#lblDiscountTotal").text());
        $("#GrandTotal-value").text($("#lblPaymentDue").text());
        ShowHideInvoiceFootDiscount("true");
    }
    if ($('#hfPaymentType').val() == "1") {//For Credit Card Payment
        $("#PayIn-text").text('Payment IN :');
        var cash = $("#txtCashRecieved").val();
        var credit = $("#lblPaymentDue").text();
        var credit = credit - cash;
        if (cash > 0) {
            $("#PayType").text('MOP: Mixed');
            $("#PayIn-value").text($("#txtCashRecieved").val());
            $("#balance-text").text('Credit Card:');
            $("#balance-value").text(credit)
            $("#balance-text2").text('Balance :');
            $("#balance-value2").text(0);
        }
        else {
            $("#PayType").text('MOP: Credit Card');
            $("#PayIn-value").text(credit);
            $("#balance-text").text('Balance :');
            $("#balance-value").text(0)
            $("#balance-text2").text(text);
            $("#balance-value2").text(text);
        }
    }
    else {//For Cash Payment
        $("#PayType").text('MOP: Cash');
        $("#PayIn-text").text('Payment IN :');
        $("#balance-text").text('Change :');
        $("#balance-text2").text(text);
        $("#PayIn-value").text($("#txtCashRecieved").val());
        $("#balance-value").text($("#lblBalance").text());
        $("#balance-value2").text(text);
    }
    ShowHideInvoiceFootGst(Gst);
    ShowHideInvoiceFootTotal(Gst, $("#lblDiscountTotal").text());
    $.print("#dvSaleInvoice");
}