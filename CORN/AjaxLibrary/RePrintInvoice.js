

function waitLoading(msg) {
    new $.Zebra_Dialog('<strong>' + msg + '</strong>', {
        'buttons': false,
        'position': ['top + 120'],
        'auto_close': 600
    });
}

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


//=======Pending Bills
function GetPendingBills() {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmRePrintInvoice.aspx/SelectPendingBills", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ FromDate: document.getElementById("txtFromDate").value, ToDate: document.getElementById("txtToDate").value }),
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

            var row = $('<tr><td class="leftval">' + pendingBills[i].INVOICE_ID + '</td><td class="leftval">' + pendingBills[i].TABLE_NO + '</td><td class="rightval">' + pendingBills[i].AMOUNTDUE + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT_TYPE + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="display:none;">' + pendingBills[i].orderBookerId + '</td><td style="display:none;">' + pendingBills[i].approvedBy + '</td><td style="display:none;">' + pendingBills[i].SERVICE_CHARGES + '</td><td style="display:none;">' + pendingBills[i].TIME_STAMP + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td style="display:none;">' + pendingBills[i].CUSTOMER_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].ORDER_NO + '</td><td style="display:none;">' + pendingBills[i].TABLE_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td></tr>');
            
            $('#tbl-pending-bills').append(row);
        }
    }
    
}

function GetPendingBill(saleInvoiceMasterId) {

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmRePrintInvoice.aspx/GetPendingBill", //page/method name
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
    if (products == "") {
       
        Error("No Record Found");
    }
    else {

        var listItems = "<option value='" + products[0].orderBookerId + "'>" + products[0].BOOKER_NAME + "</option>";
        $("#ddlOrderBooker").html(listItems);
        $("#ddlOrderBooker").val(products[0].orderBookerId);

        $("#OrderNo1").text(products[0].INVOICE_ID);

        $("#MaxOrderNo").text(products[0].ORDER_NO);

        $("#hfTableId").val(products[0].TABLE_ID);
        $("#hfTableNo").val(products[0].TABLE_NO);
        $("#txtTakeawayCustomer").val(products[0].TABLE_NO);
        $("#hfCurrentWorkDate").val(products[0].BILL_DATE);
        var amountDue = products[0].AMOUNTDUE;
        var discount = products[0].DISCOUNT;

        $("#TableNo1").text(products[0].TABLE_NO2);
        $('#hfPaymentType').val(products[0].PAYMENT_MODE_ID);
        $("#hfCustomerId").val(products[0].CUSTOMER_ID);
        $("#hfCustomerNo").val(products[0].CONTACT_NUMBER);
        $('#hfDiscountType').val(products[0].DISCOUNT_TYPE);
        $("#user-detail-bold").text(products[0].USER_NAME);

        if (products[0].coverTable != "null") {
            $('#txtCoverTable').val(products[0].coverTable);
        }
        if (products[0].approvedBy != "null") {
            $('#txtApprovedBy').val(products[0].approvedBy);
        }
        else {
            $('#txtApprovedBy').val('');
        }

        if (products[0].SERVICE_CHARGES != "0") {

            document.getElementById("lblGSTOrService").innerHTML = "Service Charges";
            document.getElementById("dvServiceCharges").setAttribute("style", "display:block;");

            $('#hfServiceCharges').val(products[0].SERVICE_CHARGES);
            $('#txtService').val(products[0].SERVICE_CHARGES);
        }
        else {
            $('#hfServiceCharges').val('');
            $('#txtService').val('');
        }

        activeLink();

        $('#txtCashRecieved').val(parseFloat(products[0].PAIDIN).toFixed(2));

        $('#txtDiscount').val(parseFloat(products[0].DISCOUNT).toFixed(2));
        $('#hfSalesTax').val(parseFloat(products[0].GST).toFixed(2));
        document.getElementById("subTotal").innerHTML = parseFloat(amountDue).toFixed(2);
        document.getElementById("GrandTotal").innerHTML = parseFloat(amountDue).toFixed(2);

        $("#lblBalance").text(products[0].BALANCE);

        CalculateBalance();


        for (var i = 0, len = products.length; i < len; ++i) {
            addProductToOrderedProduct(products, i);
        }
        var tableData = storeTblValues();
        tableData = JSON.stringify(tableData);
        document.getElementById('hfOrderedproducts').value = tableData;
    }
   }


function addProductToOrderedProduct(lstProducts, i) {

    var row = "";

    if (lstProducts[i].DIV_ID == "2") {//WHEN DIVISION IS VALUE BASE FOR ALIALFAZAL
        row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td><input  type="text" size="4"   value="' + lstProducts[i].T_PRICE + '" ></td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
    }
    else {
        if (lstProducts[i].I_D_ID == "0") {//WHEN DEALS IS OFF 
            if (lstProducts[i].MODIFIER == "0") {//CHECK IS MODIFIER OR NOT IF NOT THEN

                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td class="table-text2">' + lstProducts[i].T_PRICE + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
            }
            else {
                if (lstProducts[i].T_PRICE == "0") {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2" style="color: red" colspan="6">' + lstProducts[i].SKU_NAME + '</td> <td style="display:none;"><input type="text" size="2" value="' + 1 + '"  style="text-align: center;" disabled ></td><td style="display:none;" class="table-text2">' + lstProducts[i].T_PRICE + '</td><td style="display:none;" class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
                }
                else {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td style="color: red" class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td class="table-text2">' + lstProducts[i].T_PRICE + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');

                }
            }
        }
        else {
            if (lstProducts[i].MODIFIER == "1") {
                if (lstProducts[i].T_PRICE == "0") {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2" style="color: red">' + lstProducts[i].SKU_NAME + '</td><td align="center"><input type="text" size="2" value="' + 1 + '"  style="text-align: center;" disabled ></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display: none;" ><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
                }
                else {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td style="color: red" class="table-text2">' + lstProducts[i].SKU_NAME + '</td> <td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
                }
            }
            else {

                if (lstProducts[i].Is_ItemChoice == "1") {//WHEN DEALS IS ON AND IS_DEFAULT SET TO TRUE  
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;"  disabled></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
                }
                else {

                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td> <td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;"  disabled></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + lstProducts[i].INVOICE_ID + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td></tr>');
                }
            }
        }
    }


    $("#tble-ordered-products").append(row);

}

$(document).ready(function () {

    waitLoading('Loading');

    $(".nav-ic img").click(function () {
        $(".exit-text").slideToggle("slow", function () {
        });
    });

       
    //On Payments Click
    $("#tbl-pending-bills").delegate("tr", "click", function () {

        $(this).addClass('highlight').siblings().removeClass('highlight');

        $("#txtInvoiceNo").val($(this).find("td:eq(0)").text());
        GetPendingBill($("#txtInvoiceNo").val());
    });
    //On Payments Click
    $("#SearchInvoice").click(function () {
        GetPendingBills();
    });
    $("#PrintInvoice").click(function () {

        $('#OrderInvoice').show();
        $('#OrderInvoiceName').show();
        $('#BillNoName').show();
        $('#BillNo').show();
        $('#CompanyAddress').show();
        $('#CompanyNumber').show();
        $('#imgLogo2').show();

        $("#SaleInvoiceText").text("Duplicate Bill");
        $("#Cashier").text('Served By: ' + $("#user-detail-bold").text());
        $("#CustomerType").text($("#hfCustomerType").val());
      
        $("#InvoiceDate").text($("#hfCurrentWorkDate").val());

        if ($("#hfRegNo").val() == "") {
            document.getElementById('RegNo').style.display = "none";
        }
        else {
            $('#RegNo').show();
        }

        $("#OrderInvoice").text($("#MaxOrderNo").text());

        if (document.getElementById("hfCustomerType").value == "Takeaway") {
            document.getElementById('OrderTakerName').style.display = "none";
            document.getElementById('OrderTaker').style.display = "none";
            document.getElementById('CoverTableName').style.display = "none";
            document.getElementById('CoverTable').style.display = "none";
            $('#CustomerDetail').show();
        }
        else if (document.getElementById("hfCustomerType").value == "Delivery") {

            $("#OrderTakerName").text("D-M:");
            var OTaker = $("#ddlOrderBooker option:selected").text();
            $("#OrderTaker").text(OTaker.substring(0, 10));
            $('#OrderTakerName').show();
            $('#OrderTaker').show();
            $('#CustomerDetail').show();
            document.getElementById('CoverTableName').style.display = "none";
            document.getElementById('CoverTable').style.display = "none";
        }

        $("#BillNo").text($("#OrderNo1").text());

        //#region Products Detail

        var orderedProducts = $("#hfOrderedproducts").val();
        orderedProducts = eval(orderedProducts);
        $('#invoiceDetailBody').empty(); // clear all skus  from invoice
        $("#CustomerDetail").text(orderedProducts[0].DEAL_NAME);
        for (var i = 0, len = orderedProducts.length; i < len; i++) {
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
        //#endregion Products Detail

        var Gst = 0.00;

        if ($('#hfServiceCharges').val() == "") {
            Gst = parseFloat(document.getElementById("hfSalesTax").value).toFixed(2);
            $("#GST-text").text("G.S.T @" + parseFloat(Gst).toFixed(2) + " % :");
        }
        else {
            Gst = parseFloat(document.getElementById("txtService").value).toFixed(2);
            $("#GST-text").text("Service Chg @" + parseFloat(Gst).toFixed(2) + " % :");
        }
        if (Gst == "")
        { Gst = 0.00; }

        $("#TotalValue").text($("#subTotal").text());
        $("#GrandTotal-value").text($("#GrandTotal").text());

        var amountDue = $("#subTotal").text();

        var text = "";

        //When Discount not enter through Print invoice POpUP-----------------
        if ($("#lblDiscountTotal").text() == "0.00") {

            Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);

            $("#Discount-text").text(text);
            $("#Gst-value").text(parseFloat(Gst).toFixed(2));
            $("#lblGSTTotal").text(parseFloat(Gst).toFixed(2));
            $("#GrandTotal-value").text($("#lblPaymentDue").text());
            $("#Discount-value").text(text);
            ShowHideInvoiceFootDiscount("false");
        }
        else {
            var discount = $("#lblDiscountTotal").text();
            Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);
            if ($("#hfDiscountType").val() == "1") {
                var dic = "Discount @" + $("#txtDiscount").val() + " % :";
                $("#Discount-text").text(dic);
            }
            else {
                $("#Discount-text").text('Discount :');
            }
            $("#Gst-value").text(parseFloat(Gst).toFixed(2));
            $("#lblGSTTotal").text(parseFloat(Gst).toFixed(2));
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
      
    });
});


function CalculateBalance() {

    var balance = 0;
    var amountDue = 0;
    var discount = 0;
    var gst = 0;

    discount = document.getElementById('txtDiscount').value;

    var discountType = $('#hfDiscountType').val(); //  document.getElementById("hfDiscountType").value;
    var cashRcd = 0;

    cashRcd = document.getElementById('txtCashRecieved').value;

    var grandTotal = 0;
    grandTotal = $("#GrandTotal").text();


    if ($('#hfServiceCharges').val() != "") {
        
        gst = document.getElementById("txtService").value;
        $("#GST-text").text("Service Chg @" + gst + " % :");

    }
    else {
        gst = document.getElementById("hfSalesTax").value;
        $("#GST-text").text("G.S.T @" + gst + " % :");
    }
    
    $("#lblBalance").text('');
    $("#lblPaymentDue").text('');

    if ((discount == "")) {
        discount = 0;
    }
    if ((cashRcd == "")) {
        cashRcd = 0;
    }
    if ((gst == "")) {
        gst = 0;
    }
  
    if (discountType == 0) {

        discount = parseFloat(grandTotal) * parseFloat(discount / 100);
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount));
        balance = parseFloat((grandTotal - discount + gst)).toFixed(2);
        amountDue = parseFloat(balance).toFixed(2);
        balance = cashRcd - balance;

    }
    else if (discountType == 1) {
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount));
        balance = parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount);
        amountDue = parseFloat(balance).toFixed(2);
        balance = parseFloat((cashRcd - balance)).toFixed(2);
    }
    else {
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal - discount));
        balance = parseFloat(grandTotal) + parseFloat(gst);
        amountDue = parseFloat(balance) - parseFloat(discount);
        balance = parseFloat((cashRcd - amountDue)).toFixed(2);
    }

    if (cashRcd == 0) {
        balance = 0;
    }
    $("#lblDiscountTotal").text(parseFloat(discount).toFixed(2));
    $("#lblGSTTotal").text(parseFloat(gst).toFixed(2));
    $("#lblBalance").text(parseFloat(balance).toFixed(2));
    $("#lblPaymentDue").text(parseFloat(amountDue).toFixed(2));

}


function storeTblValues() {

    var tableData = new Array();

    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(9)').text() == "2") {

            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(2) input').val(),
                //4 for -
                "T_PRICE": $(tr).find('td:eq(3)').text(),
                "AMOUNT": $(tr).find('td:eq(4)').text(),
                "CAT_ID": $(tr).find('td:eq(5)').text(),
                "INVOICE_ID": $(tr).find('td:eq(6)').text(),
                "IS_DESC": $(tr).find('td:eq(7)').text(),
                "DESC": $(tr).find('td:eq(8)').text(),
                "A_PRICE": $(tr).find('td:eq(10)').text(),
                "I_D_ID": $(tr).find('td:eq(11)').text(),
                "DEAL_QTY": $(tr).find('td:eq(13)').text(),
                "DEAL_NAME": $(tr).find('td:eq(14)').text()
            }
        }
        else {

            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(2) input').val(),
                //4 for -
                "T_PRICE": $(tr).find('td:eq(3)').text(),
                "AMOUNT": $(tr).find('td:eq(4)').text(),
                "CAT_ID": $(tr).find('td:eq(5)').text(),
                "INVOICE_ID": $(tr).find('td:eq(6)').text(),
                "IS_DESC": $(tr).find('td:eq(7)').text(),
                "DESC": $(tr).find('td:eq(8)').text(),
                "A_PRICE": $(tr).find('td:eq(10)').text(),
                "I_D_ID": $(tr).find('td:eq(11)').text(),
                "DEAL_QTY": $(tr).find('td:eq(13)').text(),
                "DEAL_NAME": $(tr).find('td:eq(14)').text()
            }
        }
    });

    return tableData;
}

function Error(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Error', 'type': 'error' });
}
function Succes(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Success', 'type': 'information' });
}

function onlyNumbers(txt, event) {
    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}

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
