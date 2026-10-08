
$(document).ready(function () {

    document.getElementById("percentage2").disabled = true;
    document.getElementById("value2").disabled = true;
    document.getElementById("percentage").disabled = true;
    document.getElementById("value").disabled = true;
    LoadLocationDropDown();
    if ($("#hfIS_CanGiveDiscount").val() == 'False') {

        $("#ddlDiscountType option[value='0']").remove();
        $("#ddlDiscountType option[value='1']").remove();
        $("#ddlDiscountType2 option[value='0']").remove();
        $("#ddlDiscountType2 option[value='1']").remove();

    }

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
    //On Pending Bill row Click
    $("#tbl-pending-bills").delegate("tr", "click", function () {
        ShowBill(this);
    });
    //On Payments Click
    $("#Payments").click(function () {

        if ($('#hfServiceCharges').val() == "True") {//For Services Charges

            document.getElementById("lblGSTOrService").innerHTML = "Service Charges";
            document.getElementById("dvServiceCharges").setAttribute("style", "display:block;");
        }
        if ($('#hfDisable').val() == "1") {
            var tableData = storeTblValues();
            if (tableData.length > 0) {
                setTotals();
                //#region By Default Cash Mode
                document.getElementById("cash").style["background-color"] = "#7dab49";
                $('#hfPaymentType').val(0);
                CalculateBalance();
                //#ednregion
                $('#payment').show("slow");
                document.getElementById('dv_lstProducts').style.display = "none";
                document.getElementById('dv_lstCategory').style.display = "none";
                $('#txtItemCode').focus();
            }
        }
    });
});

function waitLoading(msg) {
    new $.Zebra_Dialog('<strong>' + msg + '</strong>', {
        'buttons': false,
        'position': ['top + 120'],
        'auto_close': 600
    });
}

//========== #region Load===================\\

//=======Discount User
function loadUsers(obj) {
    document.getElementById("dvLoyaltyCard").setAttribute("style", "display:none;");
    document.getElementById("dvDiscountUser").setAttribute("style", "display:none;");
    document.getElementById("dvAuthorityUser").setAttribute("style", "display:none;");
    document.getElementById("percentage").disabled = true;
    document.getElementById("value").disabled = true;
    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";

    $('#txtLoyaltyCustomer').val('');
    $('#txtDiscount').val('');
    $('#txtNoOfVisits').val('');
    $('#txtTotalPurchased').val('');
    $('#txtTotalLoyaltyDiscount').val('');
    $('#txtLoyaltyQuantity').val('');

    $('#txtTotalPoints').val('');
    $('#txtRedeemedPoints').val('');
    $('#txtBalancePoints').val('');
    $('#txtAvailableDiscount').val('');
    $('#txtAvailableCash').val('');
    
    $('#txtAllowedLimit').val('');
    $('#txtDiscountAvail').val('');
    $('#txtDiscountBalance').val('');

    if (obj.value == "2") {
        $('#lblApprovedBy').text('Password');
        $('#txtApprovedBy').hide();
        $('#txtApprovedPassword').show();

        document.getElementById('txtApprovedPassword').value = "";
        document.getElementById('txtDiscount').disabled = true;
        document.getElementById("dvLoyaltyCard").setAttribute("style", "display:block;");
        document.getElementById('txtLoyaltyCard').focus();
    }
    else if (obj.value == "1") {
        document.getElementById("dvDiscountUser").setAttribute("style", "display:block;");
        document.getElementById("dvAuthorityUser").setAttribute("style", "display:block;");
        $('#lblApprovedBy').text('Password');
        $('#txtApprovedBy').hide();
        $('#txtApprovedPassword').show();

        loadLimit(document.getElementById("ddlDiscountUser"));
        loadPassword(document.getElementById("ddlDiscountUser2"));
        DiscType(document.getElementById("value"));

        //document.getElementById('txtDiscount').disabled = false;
        document.getElementById('txtDiscount').value = $('#lblLimit').text();

        CalculateBalance();
    }
    else if (obj.value == "0") {

        if (document.getElementById("hfCustomerType").value == "Takeaway") {
            document.getElementById("percentage").disabled = false;
            document.getElementById("value").disabled = false;
        }

        document.getElementById('txtDiscount').value = document.getElementById('txtDiscount2').value;
        if (document.getElementById('txtDiscount').value != "") {
            if ($('#hfchkDiscountType').val() == 0) {
                DiscType(document.getElementById("percentage"));
            }
            else if ($('#hfchkDiscountType').val() == 1) {
                DiscType(document.getElementById("value"));
            }
        }
        else {

            document.getElementById("percentage").style["background-color"] = "#919399";
            document.getElementById("value").style["background-color"] = "#919399";
        }
        CalculateBalance();

        $('#lblApprovedBy').text('Authorized By');
        $('#txtApprovedBy').show();
        $('#txtApprovedPassword').hide();

        document.getElementById('txtApprovedPassword').value = "";
        document.getElementById('txtDiscount').disabled = true;
    }

}
//=======Discount User On Print Invoice Screen
function loadUsers2(obj) {
    document.getElementById("dvLoyaltyCard2").setAttribute("style", "display:none;");
    document.getElementById("dvDiscountUser2").setAttribute("style", "display:none;");
    document.getElementById("dvAuthorityUser2").setAttribute("style", "display:none;");
    document.getElementById("percentage2").disabled = true;
    document.getElementById("value2").disabled = true;
    //---on print invoice popup
    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";

    $('#txtLoyaltyCustomer2').val('');
    $('#txtDiscount2').val('');
    $('#txtNoOfVisits2').val('');
    $('#txtTotalPurchased2').val('');
    $('#txtTotalLoyaltyDiscount2').val('');
    $('#txtLoyaltyQuantity2').val('');

    $('#txtTotalPoints2').val('');
    $('#txtRedeemedPoints2').val('');
    $('#txtBalancePoints2').val('');
    $('#txtAvailableDiscount2').val('');
    $('#txtAvailableCash2').val('');

    $('#txtAllowedLimit2').val('');
    $('#txtDiscountAvail2').val('');
    $('#txtDiscountBalance2').val('');

    if (obj.value == "2") {
        $('#lblApprovedBy2').text('Password');
        $('#txtApprovedBy2').hide();
        $('#txtApprovedPassword2').show();

        document.getElementById('txtApprovedPassword2').value = "";
        document.getElementById('txtDiscount2').disabled = true;
        document.getElementById("dvLoyaltyCard2").setAttribute("style", "display:block;");
        document.getElementById('txtLoyaltyCard2').focus();
    }
    else if (obj.value == "1") {
        document.getElementById("dvDiscountUser2").setAttribute("style", "display:block;");
        document.getElementById("dvAuthorityUser2").setAttribute("style", "display:block;");
        $('#lblApprovedBy2').text('Password');
        $('#txtApprovedBy2').hide();
        $('#txtApprovedPassword2').show();

        loadLimit2(document.getElementById("ddlDiscountUser2"));
        loadPassword(document.getElementById("ddlDiscountUser4"));
        DiscType(document.getElementById("value2"));

        //document.getElementById('txtDiscount').disabled = false;
        document.getElementById('txtDiscount2').value = $('#lblLimit2').text();

        CalculateBalance2();
    }
    else if (obj.value == "0") {
        document.getElementById("percentage2").disabled = false;
        document.getElementById("value2").disabled = false;

        if (document.getElementById('txtDiscount2').value != "") {
            if ($('#hfchkDiscountType').val() == 0) {
                DiscType(document.getElementById("percentage2"));
            }
            else if ($('#hfchkDiscountType').val() == 1) {
                DiscType(document.getElementById("value2"));
            }
        }
        else {

            document.getElementById("percentage2").style["background-color"] = "#919399";
            document.getElementById("value2").style["background-color"] = "#919399";
        }
        CalculateBalance2();

        $('#lblApprovedBy2').text('Authorized By');
        $('#txtApprovedBy2').show();
        $('#txtApprovedPassword2').hide();

        document.getElementById('txtApprovedPassword2').value = "";
        document.getElementById('txtDiscount2').disabled = true;
    }

}

function loadLimit(obj) {
    $('#lblLimit').text(0);
    $('#tble-discount-limit').find('tr').each(function () {
        if (obj.value == $(this).find("td:eq(0)").text()) {
            $('#lblLimit').text($(this).find("td:eq(1)").text());
            return;
        }
    });
}

function loadLimit2(obj) {
    $('#lblLimit2').text(0);
    $('#tble-discount-limit2').find('tr').each(function () {
        if (obj.value == $(this).find("td:eq(0)").text()) {
            $('#lblLimit2').text($(this).find("td:eq(1)").text());
            return;
        }
    });
}

function loadPassword(obj) {
    $('#hfManagerPassword').val('');
    $('#tble-discount-user').find('tr').each(function () {
        if (obj.value == $(this).find("td:eq(0)").text()) {
            $('#hfManagerPassword').val($(this).find("td:eq(1)").text());
            return;
        }
    });

    $('#tble-discount-user2').find('tr').each(function () {
        if (obj.value == $(this).find("td:eq(0)").text()) {
            $('#hfManagerPassword').val($(this).find("td:eq(1)").text());
            return;
        }
    });
}

//=======Product and Category
function loadAllProducts() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOSCentralized.aspx/LoadAllProducts", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ LocationID: ddlLocation.GetValue() }),
                success: sethfProductValue,
                error: OnError
            }
        );
}
function sethfProductValue(products) {

    products = JSON.stringify(products);

    var result = jQuery.parseJSON(products.replace(/&quot;/g, '"'));

    products = eval(result.d);
    products = JSON.stringify(products);
    document.getElementById("hfProduct").value = products;

    loadProductCategory("btnMenuItem");

}
function loadProductCategory(obj) {
    var CategoryType = false;
    $("#hfCategoryType").val("0");

    resetColor();
    $("#hfRow").val('');
    $("#hfRowIndex").val('');
    document.getElementById('dv_lstCategory').style.display = "block";
    document.getElementById('dv_lstProducts').style.display = "block";
    a = document.getElementById("dv_lstCategory").children;
    $.ajax
   (
    {
        type: "POST", //HTTP method
        url: "frmOrderPOSCentralized.aspx/LoadProductCategroy", //page/method name
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ ItemType: CategoryType, ItemId: 0 }),
        success: addCategories,
        error: OnError
    }
   );
}

//=======Product Closing Status on Item Click
function LoadProductStatus(skuId) {
    if ($("#hfStockStatus").val() == "True") {
        document.getElementById('dvClosingStock').style.display = "block";

        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOSCentralized.aspx/LoadProductStatus", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ id: skuId, LocationID: ddlLocation.GetValue() }),
                    success: ProductStatus,
                    error: OnError
                }
            );
    }
}
function ProductStatus(productStatus) {
    productStatus = JSON.stringify(productStatus);
    var result = jQuery.parseJSON(productStatus.replace(/&quot;/g, '"'));
    productStatus = eval(result.d);

    $("#txtClosingStock").val(0);

    if (productStatus.length != "0") {
        $("#txtClosingStock").val(productStatus[0].CLOSING_STOCK);
    }
}
//=======Selected Bill Detail
function GetPendingBill(saleInvoiceMasterId) {

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOSCentralized.aspx/GetPendingBill", //page/method name
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
    $("#hfCounter").val(0);
    if (products.length > 0)
    {
        ddlCustomerPOS.SetValue(products[0].CUSTOMER_ID);
        $('#hfCustomerName').val(products[0].CUSTOMER_NAME);
        $('#txtRemarks').val(products[0].REMARKS);
    }
    for (var i = 0, len = products.length; i < len; ++i) {
        addProductToOrderedProduct(products, i, $("#OrderNo1").text());
    }
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    setTotals();
}

//=======Pending Bills
function GetPendingBills() {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOSCentralized.aspx/SelectPendingBills", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ customerType: 'Delivery', LocationID: ddlLocation.GetValue() }),
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
            if (pendingBills[i].SERVICE_TYPE == 'Delivery') {

                var row = $('<tr ><td style="display:none;">' + pendingBills[i].INVOICE_ID + '</td><td style="width:12%">' + pendingBills[i].ORDER_NO + '</td><td style="width:32%">' + pendingBills[i].CUSTOMER_NAME + '</td><td style="width:22%">' + pendingBills[i].TABLE_NO + '</td><td style="width:22%">' + pendingBills[i].AMOUNTDUE + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT_TYPE + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="display:none;">' + pendingBills[i].orderBookerId + '</td><td style="display:none;">' + pendingBills[i].approvedBy + '</td><td style="display:none;">' + pendingBills[i].SERVICE_CHARGES + '</td><td style="width:12%;font-size: 12px; color: red;">' + diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td style="display:none;">' + pendingBills[i].CUSTOMER_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].ORDER_NO + '</td><td style="display:none;">' + pendingBills[i].TABLE_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].CARD_NO + '</td><td style="display:none;">' + pendingBills[i].CARD_TYPE_ID + '</td><td style="display:none;">' + pendingBills[i].CARD_POINTS + '</td><td style="display:none;">' + pendingBills[i].PURCHASING + '</td><td style="display:none;">' + pendingBills[i].AMOUNT_LIMIT + '</td><td style="display:none;">' + pendingBills[i].EmpDiscountType + '</td><td align="center" onclick="SendSMS(this);"><a href="#"><span class="fa fa-mobile"></span></a></td></tr>');

            }
            else {
                var row = $('<tr ><td style="display:none;">' + pendingBills[i].INVOICE_ID + '</td><td style="width:12%">' + pendingBills[i].ORDER_NO + '</td><td style="width:32%">' + pendingBills[i].CUSTOMER_NAME + '</td><td style="width:22%">' +pendingBills[i].TABLE_NO + '</td><td style="width:22%">' +pendingBills[i].AMOUNTDUE + '</td><td style="display:none;">' +pendingBills[i].DISCOUNT + '</td><td style="display:none;">' +pendingBills[i].DISCOUNT_TYPE + '</td><td style="display:none;">' +pendingBills[i].coverTable + '</td><td style="display:none;">' +pendingBills[i].orderBookerId + '</td><td style="display:none;">' +pendingBills[i].approvedBy + '</td><td style="display:none;">' +pendingBills[i].SERVICE_CHARGES + '</td><td style="width:12%;font-size: 12px; color: red;">' +diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' +pendingBills[i].TABLE_NO2 + '</td><td style="display:none;">' +pendingBills[i].CUSTOMER_ID + '</td><td style="display:none;">' +pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' +pendingBills[i].ORDER_NO + '</td><td style="display:none;">' +pendingBills[i].TABLE_ID + '</td><td style="display:none;">' +pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' +pendingBills[i].CARD_NO + '</td><td style="display:none;">' +pendingBills[i].CARD_TYPE_ID + '</td><td style="display:none;">' +pendingBills[i].CARD_POINTS + '</td><td style="display:none;">' +pendingBills[i].PURCHASING + '</td><td style="display:none;">' +pendingBills[i].AMOUNT_LIMIT + '</td><td style="display:none;">' +pendingBills[i].EmpDiscountType + '</td></tr>');
            }
            $('#tbl-pending-bills').append(row);
        }
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

//Used in addProductToOrderTable(),LoadPendingBill(), On Payment Click, Print Invoice Click
function calculateDealPrice() {

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);

    var uniqueDeals = $.unique(orderedProducts.map(function (d) { return d.I_D_ID; }));
    uniqueDeals = uniqueDeals.sort();
    uniqueDeals = unique(uniqueDeals);

    var totalamount = 0;

    for (var j = 0; j < uniqueDeals.length; j++) {
        if (uniqueDeals[j] != 0) {
            var count = 0;
            $('#tble-ordered-products').find('tr').each(function () {
                if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                    if (uniqueDeals[j] == $(this).find("td:eq(21)").text()) {
                        if (count == 0) {
                            count += 1;
                            totalamount += parseFloat($(this).find("td:eq(20)").text()) * parseFloat($(this).find("td:eq(27)").text());
                        }
                        return;
                    }
                }
            });
        }
    }
    $("#DealPrice").text(totalamount);
}

function checkVoid(color) //check is item void or not //used on plus, delete, settotals
{
    if (color == "false") {
        return true;
    }
    else {
        return false;
    }
}

function setTotals() {

    var subtotal = 0.00;
    var grandTotal = 0.00;
    var salesTax = 0.00;

    var subtotal2 = 0.00;
    var grandTotal2 = 0.00;
    var salesTax2 = 0.00;

    if ($('#hfServiceCharges').val() == "False") {
        salesTax = document.getElementById("hfSalesTax").value;
        salesTax2 = document.getElementById("hfSalesTax").value;
    }
    else {
        salesTax = document.getElementById("txtService").value;
        salesTax2 = document.getElementById("txtService").value;
    }
    if (parseFloat(salesTax) > 0) {
        //Do nothing
    }
    else {
        salesTax = 0.00;
    }

    if (parseFloat(salesTax2) > 0) {
        //Do nothing.
    }
    else {
        salesTax2 = 0.00;
    }

    var discountType = document.getElementById("hfDiscountType").value;
    var discount = document.getElementById('txtDiscount').value;

    var discount2 = document.getElementById('txtDiscount').value;

    var balance = 0.00;
    var balance2 = 0.00;

    calculateDealPrice();

    $('#tble-invoice-products tr').each(function (row, tr) {
        if (checkVoid($(tr).find('td:eq(7)').text())) {
            subtotal += parseFloat($(tr).find("td:eq(4)").text());
        }
    });
    $('#tble-ordered-products tr').each(function (row, tr) {
        subtotal2 += parseFloat($(tr).find("td:eq(6)").text());
    });

    if (discount > 0) {
        if (discountType == 0) {
            grandTotal = subtotal;
            discount = grandTotal * (discount / 100);
            salesTax = parseFloat(salesTax) / 100 * (parseFloat(grandTotal) - parseFloat(discount));
            balance = (grandTotal - discount + salesTax).toFixed(2);

            grandTotal2 = subtotal2;
            discount2 = grandTotal2 * (discount2 / 100);
            salesTax2 = parseFloat(salesTax2) / 100 * (parseFloat(grandTotal2) - parseFloat(discount2));
            balance2 = (grandTotal2 - discount2 + salesTax2).toFixed(2);
        }
        else if (discountType == 1 || discountType == 2) {
            grandTotal = subtotal;
            salesTax = parseFloat(salesTax) / 100 * (grandTotal - discount);
            balance = (grandTotal - discount + salesTax).toFixed(2);
            grandTotal2 = subtotal2;
            salesTax2 = parseFloat(salesTax2) / 100 * (grandTotal2 - discount2);
            balance2 = (grandTotal2 - discount2 + salesTax2).toFixed(2);
        }
    }
    else {
        grandTotal = subtotal.toFixed(2);
        salesTax = (parseFloat(salesTax).toFixed(2) / 100) * subtotal.toFixed(2);
        balance = salesTax + subtotal;

        grandTotal2 = subtotal2.toFixed(2);
        salesTax2 = (parseFloat(salesTax2).toFixed(2) / 100) * subtotal2.toFixed(2);
        balance2 = salesTax2 + subtotal2;
    }

    $("#lblTotalGrossAmount").text(balance2.toFixed(2));

    //----------Payment PopUp--------------\\
    document.getElementById("subTotal").innerHTML = subtotal;
    document.getElementById("lblGSTTotal").innerHTML = salesTax;
    document.getElementById("GrandTotal").innerHTML = grandTotal;
    document.getElementById("lblBalance").innerHTML = "0";
    document.getElementById("lblPaymentDue").innerHTML = balance.toFixed(2);

    //----------Print Invoice PopUp--------------\\
    document.getElementById("subTotal2").innerHTML = subtotal;
    document.getElementById("lblGSTTotal2").innerHTML = salesTax;
    document.getElementById("GrandTotal2").innerHTML = grandTotal;
    document.getElementById("lblPaymentDue2").innerHTML = balance.toFixed(2);
}

function MaxOrderNo() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOSCentralized.aspx/LoadMaxOrderNo", //page/method name
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ LocationID: ddlLocation.GetValue() }),
                success: GetMaxOrderNo,
            }
        );
}

function GetMaxOrderNo(data) {

    var result = eval(data.d);

    $("#MaxOrderNo").text(result);

}

//#region Menu Button
function addCategories(categories) {
    var dv_lstCategory = document.getElementById("dv_lstCategory");
    while (dv_lstCategory.hasChildNodes()) {
        dv_lstCategory.removeChild(dv_lstCategory.lastChild);
    }

    categories = JSON.stringify(categories);
    var result = jQuery.parseJSON(categories.replace(/&quot;/g, '"'));
    categories = eval(result.d);

    var catId = 0;
    for (var i = 0, len = categories.length; i < len; ++i) {
        if (i == 0) {
            catId = categories[i].CAT_ID;
        }
        createCategoriesButtons(categories[i].CAT_NAME, categories[i].CAT_ID);
    }
    createProductButtons(catId, $("#hfCategoryType").val());
}
function createCategoriesButtons(value, id) {

    var element = document.createElement("input");
    element.setAttribute("type", "button");
    element.setAttribute("value", value);
    element.setAttribute("name", value);
    element.setAttribute("id", id);
    element.setAttribute("class", "box-sm");

    element.onclick = function () { // Note this is a function

        if ($("#hfDealId").val() == "0") {

            document.getElementById('dvDealQty').style.display = "none";
            document.getElementById('dvDealUpdate').style.display = "none";

            createProductButtons(this.id, $("#hfCategoryType").val());
            changeClass(this);
        }
        else {
            if (CheckCatDealQty($("#hfDealId").val())) {
                document.getElementById('dvDealQty').style.display = "none";
                document.getElementById('dvDealUpdate').style.display = "none";
                createProductButtons(this.id, $("#hfCategoryType").val());
                changeClass(this);
            }
        }
    };

    var dvLstCategory = document.getElementById("dv_lstCategory");
    dvLstCategory.appendChild(element);
}
function createProductButtons(catId, type) {

    var dvLstProducts = document.getElementById("dv_lstProducts");
    while (dvLstProducts.hasChildNodes()) {
        dvLstProducts.removeChild(dvLstProducts.lastChild);
    }

    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        //------------------------------------------------------------------
        if (lstProducts[i].I_D_ID == 0) {
            if (lstProducts[i].CAT_ID == catId && lstProducts[i].MODIFIER == type) {
                var dealId = lstProducts[i].I_D_ID;
                //Create an input type dynamically.   
                var element = document.createElement("input");
                //Assign different attributes to the element. 
                element.setAttribute("type", "button");
                element.setAttribute("value", lstProducts[i].SKU_NAME);
                element.setAttribute("name", lstProducts[i].SKU_NAME);
                element.setAttribute("id", lstProducts[i].SKU_ID);
                element.setAttribute("class", "box-sm3");
                element.onclick = function () { // Note this is a function
                    LoadProductStatus(this.id);
                    $("#hfSkuId").val(this.id);//FOR MODIFIER ITEMS

                    addProductToOrderTable(this.id, catId, dealId);
                    changeProductClass(this);
                };
                dvLstProducts.appendChild(element);
            }
        }
    }
}
function createSubProductButtons(catId, dealId, type) {

    var dvLstProducts = document.getElementById("dv_lstProducts");
    while (dvLstProducts.hasChildNodes()) {
        dvLstProducts.removeChild(dvLstProducts.lastChild);
    }
    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        if (lstProducts[i].I_D_ID != 0 && lstProducts[i].DEAL == 0) {
            if (lstProducts[i].CAT_ID == catId && lstProducts[i].I_D_ID == dealId && lstProducts[i].MODIFIER == type) {
                var CatQty = lstProducts[i].Cat_Quantity;
                if (lstProducts[i].Is_ItemChoice == "0") {
                    if (ValidateDealQty()) {//First Validate
                        if (CheckItemDealQty(dealId, catId, lstProducts[i].SKU_ID, CatQty, lstProducts[i].QTY, 1)) {
                            addDealProductToOrderTable(lstProducts[i].SKU_ID, catId, dealId, $('#txtDealQty').val());
                        }
                        else {
                            break;
                        }
                    }
                }
                else {
                    //Create an input type dynamically.   
                    var element = document.createElement("input");
                    //Assign different attributes to the element. 
                    element.setAttribute("type", "button");
                    element.setAttribute("value", lstProducts[i].SKU_NAME);
                    element.setAttribute("name", lstProducts[i].QTY);
                    element.setAttribute("id", lstProducts[i].SKU_ID);
                    element.setAttribute("class", "box-sm3");
                    element.onclick = function () { // Note this is a function
                        LoadProductStatus(this.id);
                        $("#hfSkuId").val(this.id);
                        if (ValidateDealQty()) {//First Validate

                            if (CheckItemDealQty(dealId, catId, this.id, CatQty, parseFloat(this.name), 2)) {
                                $("#hfSkuId").val(this.id);
                                addDealProductToOrderTable(this.id, catId, dealId, $('#txtDealQty').val());
                                changeProductClass(this);
                            }
                            $('#tble-ordered-products').find('tr').each(function () {//Show Update button if Deal Exist
                                if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                                    if (dealId == $(this).find("td:eq(21)").text()) {
                                        document.getElementById('dvDealUpdate').style.display = 'block';
                                        return;
                                    }
                                }
                            });
                        }
                    };
                    dvLstProducts.appendChild(element);
                }
            }
        }
    }
}
//-----------------------------------------------------------\\

//Employee Discount Validation 
function ValidateDiscount(Id) {
    if (Id === undefined || Id === null || Id === -1) {
        return true;
    }

    if (Id == "1") {
        if ($("#ddlDiscountUser").val() == null) {
            Error("Please select Employee");
            return false;
        }

        if ($("#ddlDiscountUser2").val() == null) {
            Error("Please select Authority Person");
            return false;
        }

        if (parseFloat($('#lblLimit').text()) < parseFloat($('#txtDiscount').val())) {
            Error("Discount Limit is:" + $('#lblLimit').text());
            return false;

        }
        if ($('#txtApprovedPassword').val() == "")
        {
            Error("Please enter Password");
            return false;
        }
        if ($('#hfManagerPassword').val() != $('#txtApprovedPassword').val()) {
            if ($('#txtApprovedPassword').val() == "") {
                Error("Wrong Password");
                return false;
            }
            else {
                Error("Please enter correct Password");
                return false;
            }
        }
    }
    else if (Id == "2")
    {
        if ($('#hfCardTypeId').val() == "3") {
            if (parseFloat(document.getElementById('txtDiscount').value) > parseFloat(document.getElementById('txtDiscountBalance').value)) {
                Error('Discount can not be greater than Balance Discount');
                $('#txtDiscount').focus();
                return false;
            }
        }

        if(parseFloat(document.getElementById('txtDiscount').value) > 0)
        {
            if ($('#txtApprovedPassword').val() == "") {
                Error("Please enter Password");
                return false;
            }
        }

        if ($('#hfManagerPassword').val() != $('#txtApprovedPassword').val()) {
            if ($('#txtApprovedPassword').val() == "") {
                Error("Wrong Password");
                return false;
            }
            else {
                Error("Please enter correct Password");
                return false;
            }
        }
    }
    return true;
}

function ValidateDiscount2(Id) {
    
    if (Id === undefined || Id === null)
    {   
        return true;
    }
    if (Id == "1") {
        if ($("#ddlDiscountUser2").val() == null) {
            Error("Please select Employee");
            return false;
        }

        if ($("#ddlDiscountUser3").val() == null) {
            Error("Please select Authority Person");
            return false;
        }

        if (parseFloat($('#lblLimit2').text()) < parseFloat($('#txtDiscount2').val())) {
            Error("Discount Limit is:" + $('#lblLimit2').text());
            return false;

        }
        if ($('#txtApprovedPassword2').val() == "") {
            Error("Please enter Password");
            return false;
        }
        if ($('#hfManagerPassword2').val() != $('#txtApprovedPassword2').val()) {
            if ($('#txtApprovedPassword2').val() == "") {
                Error("Wrong Password");
                return false;
            }
            else {
                Error("Please enter correct Password");
                return false;
            }
        }
    }
    else if (Id == "2") {
        if ($('#hfCardTypeId').val() == "3") {
            if (parseFloat(document.getElementById('txtDiscount2').value) > parseFloat(document.getElementById('txtDiscountBalance2').value)) {
                Error('Discount can not be greater than Balance Discount');
                $('#txtDiscount2').focus();
                return false;
            }
        }

        if (parseFloat(document.getElementById('txtDiscount2').value) > 0) {
            if ($('#txtApprovedPassword2').val() == "") {
                Error("Please enter Password");
                return false;
            }
        }

        if ($('#hfManagerPassword').val() != $('#txtApprovedPassword2').val()) {
            if ($('#txtApprovedPassword2').val() == "") {
                Error("Wrong Password");
                return false;
            }
            else {
                Error("Please enter correct Password");
                return false;
            }
        }
    }
    return true;
}

//Deal Validation----------------------------------------------------------\\
function ValidateDealQty() {
    if ($('#txtDealQty').val() == "") {
        Error("Must enter Deal Qty");
        return false;
    }
    if ($('#txtDealQty').val() == "0") {
        Error("Deal Qty should be greater than zero");
        return false;
    }
    return true;
}

//used on createCategoriesButtons
function CheckCatDealQty(dealId) {
    //------------------------------------------------------------------

    if (dealId == 0) {
        return true;
    }

    //------------------------------------------------------------------
    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    var flag = false;

    var CatQty = 0;
    var TotalCatQty = 0;
    var TotalCatGridQty = 0;

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        if (lstProducts[i].DEAL == 1) {
            if (lstProducts[i].I_D_ID == dealId) {
                var count = 0;
                var CatGridQty = 0;
                var ItemGridQty = 0;

                TotalCatQty += lstProducts[i].Cat_Quantity * parseFloat($("#txtDealQty").val());

                $('#tble-ordered-products').find('tr').each(function () {
                    if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                        if (dealId == $(this).find("td:eq(21)").text() && lstProducts[i].DIV_ID == $(this).find("td:eq(14)").text()) {
                            ItemGridQty += parseFloat($(this).find("td:eq(3) input").val());
                            if (count == 0) {
                                count++;
                                CatGridQty = parseFloat($(this).find("td:eq(27)").text()) * parseFloat($(this).find("td:eq(28)").text());
                                TotalCatGridQty += parseFloat($(this).find("td:eq(27)").text()) * parseFloat($(this).find("td:eq(28)").text());

                            }
                        }
                    }
                });
                if (ItemGridQty != CatGridQty) {
                    flag = true;
                    break;
                }
            }
        }
    }

    if (flag) {
        Error("Plz complete your Deal");
        return false;
    }

    if (TotalCatGridQty > 0) {
        if (TotalCatQty != TotalCatGridQty) {
            Error("Plz complete your Deal");
            return false;
        }
    }
    $("#hfDealId").val('0');
    return true;

}

//used in createSubProductButtons()
function CheckItemDealQty(dealId, catId, ItemId, CatQty, ItemQty, Type) {

    if ($("#tble-ordered-products tr").length > 0) {
        var flag = false;
        var flagmsg = "";
        var dealQty = 0;

        dealQty = $('#txtDealQty').val();

        $('#tble-ordered-products').find('tr').each(function () {
            if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                if (dealId == $(this).find("td:eq(21)").text() && catId == $(this).find("td:eq(14)").text()) {
                    if ($(this).find("td:eq(31)").text() == "0") {
                        if (ItemId != $(this).find("td:eq(0)").text()) {
                            flag = true;
                            return;
                        }
                        else {
                            ItemQty += parseFloat($(this).find("td:eq(3) input").val());
                        }
                    }
                    else {
                        ItemQty += parseFloat($(this).find("td:eq(3) input").val());
                    }
                }
            }
        });

        dealQty = dealQty * CatQty;

        $('#tble-ordered-products').find('tr').each(function () {
            if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                if (dealId == $(this).find("td:eq(21)").text() && catId == $(this).find("td:eq(14)").text()) {
                    dealQty = parseFloat($(this).find("td:eq(27)").text()) * parseFloat($(this).find("td:eq(28)").text());
                    return;
                }
            }
        });
        //--First Check Optional or Not
        if (flag) {

            Error("Cannot enter Optional Item");
            return false;
        }
        //--For Check Whole Deal
        if (ItemQty > dealQty) {
            ItemQty -= ItemQty;
            Error("Cannot enter Deal Qty is not equal with Item Qty");
            return false;
        }

        return true;
    }
    return true;
}

//Type used for checking click on Update button 
//1 for Updates
function CheckDealQty(dealId, catId, CatQty, Type) {

    var qty = 0;
    var dealQty = 0;

    $('#tble-ordered-products').find('tr').each(function () {
        if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
            if (dealId == $(this).find("td:eq(21)").text()) {
                qty += parseFloat($(this).find("td:eq(3) input").val());
            }
        }
    });
    //this type use on Update Deal Button Click
    if (Type == 1) {
        if (qty > $('#txtDealQty').val()) {
            Error("Deal Qty is not equal with Item Qty");
            return false;
        }
        return true;
    }


}

function UpdateDeal() {

    var dealId = $('#hfDealId').val();

    // if (CheckDealQty(dealId,0,0,1)) {

    var tableData;

    $('#tble-ordered-products').find('tr').each(function () {

        if (dealId == $(this).find("td:eq(21)").text()) {
            if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                $(this).find("td:eq(27)").text($('#txtDealQty').val());

            }
        }
    });

    tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    //}
}
//End Deal Validation----------------------------------------------------------\\

function DisableDiscount() {

    if ($('#txtLoyaltyCard').val() == "") {
        $('#dvDiscount2').find('*').prop('disabled', false);

        $('#txtDiscount2').val('');

        $('#hfCardTypeId').val('0');
        $('#hfCardPoints').val('0');
        $('#hfCardPurchasing').val('0');
        $('#hfCardAmountLimit').val('0');

        CalculateBalance2();
    }
}
function ValidateCardAmount() {
    if ($('#hfCardTypeId ').val() == "3") {
        if (parseFloat($('#txtDiscount2').val()) > parseFloat($('#hfCardAmountLimit').val())) {
            Error("Discount Limit is " + $('#hfCardAmountLimit').val());
            return false;
        }
    }
    return true;
}
//Loyalty endregion

///Qty [3] ,C1 [12], IS Void [13],Deal Price [21],A_Price[20], Deal ID [21], C2 [23], Deal Qty [27] ,Cat Qty [28], ,Cat Choice [29], Deal_name [32]
///UOM_ID": (33),"intStockMUnitCode": 34, "Stock_to_SaleOperator": 35, "Stock_to_SaleFactor": 36

//Products Grid---------------------------------------------------------------\\

function addProductToOrderTable(skuId, catId, dealId) {
    var tableData;
    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        var flage = false;

        if (lstProducts[i].SKU_ID == skuId && lstProducts[i].CAT_ID == catId && lstProducts[i].I_D_ID == dealId) {
            $('#tble-ordered-products').find('tr').each(function () {

                var td1 = $(this).find("td:eq(0)").text();
                var tdcat = $(this).find("td:eq(14)").text();
                var tdDeal = -1;
                if ($(this).find("td:eq(21)").text() != "") {
                    tdDeal = $(this).find("td:eq(21)").text();//finding dealID
                }
                if (skuId == td1 && catId == tdcat && tdDeal == dealId) {

                    var color = $(this).find("td:eq(1)").css("color");//finding color for modifier 
                    var next = $(this).next().find("td:eq(0)").text();//finding for modifier next row 
                    
                    if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                        if ($("#hfRow").val() == "") {//check row is selected or not if select not then update previous else add new
                            if (color == "rgb(255, 0, 0)") {
                                if (next != "") {
                                    flage = false;
                                }
                                else {
                                    if ($(this).find("td:eq(21)").text() == "0") {//when item_deal_id=0
                                        $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + 1 ).toFixed(3));
                                        $(this).find("td:eq(6)").text((parseFloat($(this).find("td:eq(5)").text()) * parseFloat($(this).find("td:eq(3) input").val())).toFixed(2));
                                    }
                                    else {
                                        if (lstProducts[i].Is_CatChoice == "0") {
                                            $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                        }
                                        else {
                                            $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + 1).toFixed(3));
                                        }
                                    }

                                    tableData = storeTblValues();
                                    tableData = JSON.stringify(tableData);
                                    document.getElementById('hfOrderedproducts').value = tableData;
                                    flage = true;
                                }
                            }
                            else {//check for Modifier if row exist after then add new else update previous

                                if ($(this).find("td:eq(19)").text() == "2") {//WHEN DIVISION IS VALUE BASE

                                    $(this).find("td:eq(5) input").val(parseFloat($(this).find("td:eq(5) input").val()) + parseFloat($(this).find("td:eq(20)").text()));
                                    $(this).find("td:eq(3) input").val(parseFloat($(this).find("td:eq(5) input").val()) / parseFloat($(this).find("td:eq(20)").text()));
                                }
                                else {

                                    if (dealId == "0") {//when item_deal_id=0
                                        $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + 1).toFixed(3));
                                        $(this).find("td:eq(6)").text((parseFloat($(this).find("td:eq(5)").text()) * parseFloat($(this).find("td:eq(3) input").val())).toFixed(2));
                                    }
                                    else {
                                        if (lstProducts[i].Is_CatChoice == "1") {
                                            $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                        }
                                        else {
                                            $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + 1).toFixed(3));
                                        }
                                    }
                                }
                                tableData = storeTblValues();
                                tableData = JSON.stringify(tableData);
                                document.getElementById('hfOrderedproducts').value = tableData;
                                flage = true;
                            }
                        }
                    }
                }
            });

            if (flage) break;

            addProductToOrderedProduct(lstProducts, i, $("#OrderNo1").text());

            break;
        }
    }
    tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    setTotals();
}
function addProductToOrderedProduct(lstProducts, i, invoiceId) {

    //--------------------For Maintaing Counter-----By Hassan------------------------------------------------\\
    //#region Counter
    var rowNo = $("#hfRow").val();//get which row is selected

    if ($("#OrderNo1").text() == "N/A") {//Check when it's new order
        if ($("#hfRowIndex").val() != "") {//Check row selection
            var x = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            x[23].innerHTML = parseFloat(parseFloat(x[23].innerHTML) + 0.1).toFixed(2);//Update C2

            var y = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            lstProducts[i].C1 = parseFloat((parseFloat(y[12].innerHTML) + parseFloat(x[23].innerHTML))).toFixed(2);
            lstProducts[i].C2 = -1;//FOR CHECK COUNT ON EDIT

        }
        else {

            $("#hfCounter").val(parseInt($("#hfCounter").val()) + 1);
            lstProducts[i].C1 = parseFloat($("#hfCounter").val()) + parseFloat(lstProducts[i].C1);
        }
    }
    else {
        if ($("#hfRowIndex").val() != "") {//Check row selection
            var x = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            x[23].innerHTML = parseFloat(parseFloat(x[23].innerHTML) + 0.1).toFixed(2);//Update C2

            var y = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            lstProducts[i].C1 = parseFloat((parseFloat(y[12].innerHTML) + parseFloat(x[23].innerHTML))).toFixed(2);
            lstProducts[i].C2 = -1;//FOR CHECK COUNT ON EDIT
        }
        else {
            if (lstProducts[i].C2 > 0 || lstProducts[i].C2 == 0) {
                $("#hfCounter").val(parseInt($("#hfCounter").val()) + 1);
                lstProducts[i].C1 = parseFloat($("#hfCounter").val());
            }
        }
    }
    //#endregion Counter
    //-----------------------------------------------------------------------------------------------\\

    var row = "";
    if (lstProducts[i].DIV_ID == "2") {//WHEN DIVISION IS VALUE BASE FOR ALIALFAZAL
        row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td><input type="text" size="4" onkeyup="plusAmount(this);" onkeypress="return onlyDotsAndNumbers(this, event);" value="' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '"></td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
    }
    else {
        if (lstProducts[i].I_D_ID == "0") {//WHEN DEALS IS OFF 

            if (lstProducts[i].MODIFIER == "0") {//CHECK IS MODIFIER OR NOT IF NOT THEN

                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
            }
            else {
                if (lstProducts[i].T_PRICE == "0") {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2" style="color: red" colspan="6">' + lstProducts[i].SKU_NAME + '</td><td style="display:none;" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td style="display:none;"><input type="text" size="2" value="' + 1 + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td style="display:none;" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td style="display:none;" class="table-text2">' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '</td><td style="display:none;" class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this); style="display:none;""><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
                else {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td style="color: red" class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this); style="display:none;""><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
            }
        }
        else {
            if (lstProducts[i].MODIFIER == "1") {
                if (lstProducts[i].T_PRICE == "0") {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2" style="color: red">' + lstProducts[i].SKU_NAME + '</td><td style="display: none;" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + 1 + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td style="display: none;" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td style="display: none;" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
                else {
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td style="color: red" class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
            }
            else {

                if (lstProducts[i].Is_ItemChoice == "1") {//WHEN DEALS IS ON AND IS_DEFAULT SET TO TRUE  
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);" ><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
                else {

                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + lstProducts[i].DEAL_QTY + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
                }
            }
        }
    }

    if ($("#hfRow").val() != "") {//in case of product is selected in grid (AddRow(this));

        var i = $("#hfRow").val();
        $('#tble-ordered-products > tr').eq(i).after(row);

    }

    else {
        $("#tble-ordered-products").append(row);
    }
}

function addDealProductToOrderTable(skuId, catId, dealId, DealQty) {
    var tableData;
    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        var flag = false;

        if (lstProducts[i].SKU_ID == skuId && lstProducts[i].CAT_ID == catId && lstProducts[i].I_D_ID == dealId) {

            $('#tble-ordered-products').find('tr').each(function () {

                var td1 = $(this).find("td:eq(0)").text();
                var tdcat = $(this).find("td:eq(14)").text();
                var tddealId = $(this).find("td:eq(21)").text();

                if (skuId == td1 && catId == tdcat && dealId == tddealId) {

                    var color = $(this).find("td:eq(1)").css("color");//finding color for modifier 
                    var next = $(this).next().find("td:eq(0)").text();//finding for modifier next row 

                    if (checkVoid($(this).find("td:eq(13)").text())) {//CHECK IS VOID OR NOT
                        if ($("#hfRow").val() == "") {//check row is selected or not if select not then update previous else add new
                            if (color == "rgb(255, 0, 0)") {
                                if (next != "") {
                                    flag = false;
                                }
                                else {

                                    if (lstProducts[i].Is_CatChoice == "0") {
                                        $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                    }
                                    else {
                                        $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                    }

                                    tableData = storeTblValues();
                                    tableData = JSON.stringify(tableData);
                                    document.getElementById('hfOrderedproducts').value = tableData;
                                    flag = true;
                                }
                            }
                            else {//check for Modifier if row exist after then add new else update previous


                                if (lstProducts[i].Is_CatChoice == "1") {
                                    $(this).find("td:eq(3) input").val(parseFloat(($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                }
                                else {
                                    $(this).find("td:eq(3) input").val((parseFloat($(this).find("td:eq(3) input").val()) + parseFloat(lstProducts[i].QTY)).toFixed(3));
                                }

                                tableData = storeTblValues();
                                tableData = JSON.stringify(tableData);
                                document.getElementById('hfOrderedproducts').value = tableData;
                                flag = true;
                            }
                        }
                    }
                }
            });


            if (flag) break;


            addDealProductToOrderedProduct(lstProducts, i, $("#OrderNo1").text(), DealQty);

            break;
        }
    }
    tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    setTotals();
}
function addDealProductToOrderedProduct(lstProducts, i, invoiceId, DealQty) {

    //--------------------For Maintaing Counter-----By Hassan---------------------------------------\\
    //#region Counter
    var rowNo = $("#hfRow").val();//get which row is selected
    if ($("#OrderNo1").text() == "N/A") {//Check when it's new order
        if ($("#hfRowIndex").val() != "") {//Check row selection
            var x = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            x[23].innerHTML = parseFloat(parseFloat(x[23].innerHTML) + 0.1).toFixed(2);//Update C2

            var y = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            lstProducts[i].C1 = parseFloat((parseFloat(y[12].innerHTML) + parseFloat(x[23].innerHTML))).toFixed(2);
            lstProducts[i].C2 = -1;//FOR CHECK COUNT ON EDIT
        }
        else {

            $("#hfCounter").val(parseInt($("#hfCounter").val()) + 1);
            lstProducts[i].C1 = parseFloat($("#hfCounter").val()) + parseFloat(lstProducts[i].C1);
        }
    }
    else {
        if ($("#hfRowIndex").val() != "") {//Check row selection
            var x = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            x[23].innerHTML = parseFloat(parseFloat(x[23].innerHTML) + 0.1).toFixed(2);//Update C2

            var y = document.getElementById("tble-ordered-products").rows[rowNo].cells;
            lstProducts[i].C1 = parseFloat((parseFloat(y[12].innerHTML) + parseFloat(x[23].innerHTML))).toFixed(2);
            lstProducts[i].C2 = -1;//FOR CHECK COUNT ON EDIT
        }
        else {
            if (lstProducts[i].C2 > 0 || lstProducts[i].C2 == 0) {
                $("#hfCounter").val(parseInt($("#hfCounter").val()) + 1);
                lstProducts[i].C1 = parseFloat($("#hfCounter").val());
            }
        }
    }
    //#endregion Counter
    //-----------------------------------------------------------------------------------------------\\

    var row = "";

    if (lstProducts[i].I_D_ID != "0") {//WHEN DEALS IS ON 
        if (lstProducts[i].MODIFIER == "1") {
            if (lstProducts[i].T_PRICE == "0") {
                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2" style="color: red">' + lstProducts[i].SKU_NAME + '</td><td style="display: none;" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + 1 + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td style="display: none;" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td style="display: none;" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + DealQty + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
            }
            else {
                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td style="color: red" class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + DealQty + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
            }
        }
        else {
            if (lstProducts[i].Is_ItemChoice == "1") {//WHEN DEALS IS ON AND IS_DEFAULT SET TO TRUE  
                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" ><span style="margin-right: 5px; cursor: pointer;" ><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;" ><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + DealQty + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
            }
            else {
                row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"  ><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + parseFloat(lstProducts[i].QTY).toFixed(3) + '"  style="text-align: center;" onkeypress="return onlyDotsAndNumbers(this,event);" onkeyup="QtyBlur(this);"></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + 0 + '</td><td class="table-text2">' + parseFloat(lstProducts[i].AMOUNT).toFixed(2) + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER + '</td><td style="display:none;">' + lstProducts[i].PR_COUNT + '</td><td style="display:none;">' + lstProducts[i].SEC_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION + '</td><td style="display:none;">' + lstProducts[i].C1 + '</td><td style="display:none;">' + lstProducts[i].VOID + '</td><td style="display:none;">' + lstProducts[i].CAT_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESC + '</td><td style="display:none;">' + lstProducts[i].PRINT + '</td><td style="display:none;">' + lstProducts[i].DIV_ID + '</td><td style="display:none;">' + lstProducts[i].A_PRICE + '</td><td style="display:none;">' + lstProducts[i].I_D_ID + '</td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">' + lstProducts[i].C2 + '</td><td style="display:none;">' + lstProducts[i].H + '</td><td style="display:none;">' + lstProducts[i].intDealID + '</td><td style="display:none;">' + lstProducts[i].lngDealDetailID + '</td><td style="display:none;">' + DealQty + '</td><td style="display:none;">' + lstProducts[i].Cat_Quantity + '</td><td style="display:none;">' + lstProducts[i].Is_CatChoice + '</td><td style="display:none;">' + lstProducts[i].Is_ItemChoice + '</td><td style="display:none;">' + lstProducts[i].Is_Optional + '</td><td style="display:none;">' + lstProducts[i].DEAL_NAME + '</td><td style="display:none;">' + lstProducts[i].UOM_ID + '</td><td style="display:none;">' + lstProducts[i].intStockMUnitCode + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;">' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;">' + lstProducts[i].DEFAULT_QTY + '</td></tr>');
            }
        }
    }
    if ($("#hfRow").val() != "") {//in case of product is selected in grid (AddRow(this));

        var i = $("#hfRow").val();
        $('#tble-ordered-products > tr').eq(i).after(row);

    }

    else {
        $("#tble-ordered-products").append(row);
    }
}

//WHEN DIVISION IS VALUE BASE
function plusAmount(obj) {

    rowIndex = obj.closest('tr');

    var price = parseFloat($(rowIndex).find('td:eq(5) input').val());

    if (obj.value != "") {

        var tprice = parseFloat($(rowIndex).find('td:eq(20)').text());

        $(rowIndex).find('td:eq(3) input').val(parseFloat(price / tprice).toFixed(2));

        var amount = parseFloat(price).toFixed(2);

        $(rowIndex).find('td:eq(6)').text(amount);



    }
    else {
        $(rowIndex).find('td:eq(3) input').val(0);
        $(rowIndex).find('td:eq(6)').text(0);
    }

    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    $('#hfDisable').val("0");

}
function plusQty(obj) {

    var rowIndex = $(obj).parent();
    if (checkVoid($(rowIndex).find('td:eq(13)').text())) {

        var qty = parseFloat($(rowIndex).find('td:eq(3) input').val());
        qty = qty + 1;

        $(rowIndex).find('td:eq(3) input').val(parseFloat(qty).toFixed(3));
        var price = $(rowIndex).find('td:eq(5)').text();
        var amount = parseFloat(qty * price).toFixed(2);
        $(rowIndex).find('td:eq(6)').text(amount);
        var tableData = storeTblValues();
        tableData = JSON.stringify(tableData);
        document.getElementById('hfOrderedproducts').value = tableData;
        setTotals();
        $('#hfDisable').val("0");
    }
}

function QtyBlur(obj) {
    
    var rowIndex = $(obj).closest('tr');
    if ($(rowIndex).find('td:eq(3) input').val() == "" || $(rowIndex).find('td:eq(3) input').val() == 0)
    {   
        Error("Quantity should be greater than 0");
        $(rowIndex).find('td:eq(3) input').val("1");
        return;
    }
    var qty = parseFloat($(rowIndex).find('td:eq(3) input').val());
    var price = $(rowIndex).find('td:eq(5)').text();
    var amount = parseFloat(qty * price).toFixed(2);
    $(rowIndex).find('td:eq(6)').text(amount);
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    setTotals();
    $('#hfDisable').val("0");
}

function resetColor() {
    $('#tble-ordered-products tr').removeClass("selected");

    //via loop----------------------------------------------------

    //var table = document.getElementById('tble-ordered-products');
    //var rows = table.getElementsByTagName("tr");

    //for (i = 0; i < rows.length; i++) {
    // if (i % 2 == 0) {
    //   rows[i].style.backgroundColor = "transparent";
    //  }
    // else {
    //     rows[i].style.backgroundColor = "#f5f5f5";
    // }
    //}

    //--------------------------------------------------------------
}

//Add new Row on Specific index for Nos and Extras
function AddRow(obj) {

    var rowNo = $(obj).closest('tr').index();//for getting on which row no click

    if ($("#hfRow").val() != rowNo) {
        resetColor();

    }

    $("#hfRow").val(rowNo);

    var rowIndex = $(obj).parent();
    $("#hfRowIndex").val(rowIndex);

    //$("#hfCounter").val($(rowIndex).find('td:eq(23)').text());

    $(obj).closest('tr').addClass('selected');


    //now call on addProductToOrderedProduct

}
//-----------------------------------------------------------------------------\\

function storeTblValues() {

    var tableData = new Array();

    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(19)').text() == "2") {

            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(3) input').val(),
                //4 for -
                "T_PRICE": $(tr).find('td:eq(5) input').val(),
                "AMOUNT": $(tr).find('td:eq(6)').text(),
                //7 column is of delete button
                "PRINTER": $(tr).find('td:eq(8)').text(),
                "PR_COUNT": $(tr).find('td:eq(9)').text(),
                "SEC_ID": $(tr).find('td:eq(10)').text(),
                "SECTION": $(tr).find('td:eq(11)').text(),
                "C1": $(tr).find('td:eq(12)').text(),
                "VOID": $(tr).find('td:eq(13)').text(),
                "CAT_ID": $(tr).find('td:eq(14)').text(),
                "INVOICE_ID": $(tr).find('td:eq(15)').text(),
                "IS_DESC": $(tr).find('td:eq(16)').text(),
                "DESC": $(tr).find('td:eq(17)').text(),
                "PRINT": $(tr).find('td:eq(18)').text(),
                "A_PRICE": $(tr).find('td:eq(20)').text(),
                "I_D_ID": $(tr).find('td:eq(21)').text(),
                //22 column for Selection
                "C2": $(tr).find('td:eq(23)').text(),
                "intDealID": $(tr).find('td:eq(25)').text(),
                "lngDealDetailID": $(tr).find('td:eq(26)').text(),
                "DEAL_QTY": $(tr).find('td:eq(27)').text(),
                "DEAL_NAME": $(tr).find('td:eq(32)').text(),
                "UOM_ID": $(tr).find('td:eq(33)').text(),
                "intStockMUnitCode": $(tr).find('td:eq(34)').text(),
                "Stock_to_SaleOperator": $(tr).find('td:eq(35)').text(),
                "Stock_to_SaleFactor": $(tr).find('td:eq(36)').text(),
                "DEFAULT_QTY": $(tr).find('td:eq(37)').text()

            }
        }
        else {

            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(3) input').val(),
                //4 for-
                "T_PRICE": $(tr).find('td:eq(5)').text(),
                "AMOUNT": $(tr).find('td:eq(6)').text(),
                //7 column is of delete button
                "PRINTER": $(tr).find('td:eq(8)').text(),
                "PR_COUNT": $(tr).find('td:eq(9)').text(),
                "SEC_ID": $(tr).find('td:eq(10)').text(),
                "SECTION": $(tr).find('td:eq(11)').text(),
                "C1": $(tr).find('td:eq(12)').text(),
                "VOID": $(tr).find('td:eq(13)').text(),
                "CAT_ID": $(tr).find('td:eq(14)').text(),
                "INVOICE_ID": $(tr).find('td:eq(15)').text(),
                "IS_DESC": $(tr).find('td:eq(16)').text(),
                "DESC": $(tr).find('td:eq(17)').text(),
                "PRINT": $(tr).find('td:eq(18)').text(),
                "A_PRICE": $(tr).find('td:eq(20)').text(),
                "I_D_ID": $(tr).find('td:eq(21)').text(),
                //22 column for Selection
                "C2": $(tr).find('td:eq(23)').text(),
                "intDealID": $(tr).find('td:eq(25)').text(),
                "lngDealDetailID": $(tr).find('td:eq(26)').text(),
                "DEAL_QTY": $(tr).find('td:eq(27)').text(),
                "DEAL_NAME": $(tr).find('td:eq(32)').text(),
                "UOM_ID": $(tr).find('td:eq(33)').text(),
                "intStockMUnitCode": $(tr).find('td:eq(34)').text(),
                "Stock_to_SaleOperator": $(tr).find('td:eq(35)').text(),
                "Stock_to_SaleFactor": $(tr).find('td:eq(36)').text(),
                "DEFAULT_QTY": $(tr).find('td:eq(37)').text()
            }
        }
    });

    return tableData;
}

function storeTblValuesInvoice() {

    var tableData = new Array();

    $('#tble-invoice-products tr').each(function (row, tr) {
        tableData[row] = {
            "SKU_ID": $(tr).find('td:eq(0)').text(),
            "SKU_NAME": $(tr).find('td:eq(1)').text(),
            "QTY": $(tr).find('td:eq(2) input').val(),
            "T_PRICE": $(tr).find('td:eq(3) input').val(),
            "AMOUNT": $(tr).find('td:eq(4)').text(),
            "I_D_ID": 0,
            "VOID": false,
            "CAT_ID": 1,
            "A_PRICE": 0,
            "C1": 0,
            "C2": 0,
            "DEAL_QTY" :0,
            "intDealID": 0,
            "lngDealDetailID": 0,
            "UOM_ID": $(tr).find('td:eq(8)').text(),
            "IntStockMUnitCode": $(tr).find('td:eq(9)').text(),
            "Stock_to_SaleOperator": $(tr).find('td:eq(10)').text(),
            "Stock_to_SaleFactor": $(tr).find('td:eq(11)').text(),
            "DEFAULT_QTY": $(tr).find('td:eq(12)').text(),
            "INVOICE_ID": $("#OrderNo1").text()
        }
    });
    return tableData;
}

//Call on Pending Bills Row Click, And on HoldOrder Save When Takeaway
function ShowBill(tr) {

    $("#txtLoyaltyCard").val('');
    $("#txtLoyaltyCard2").val('');
    $("#hfCardTypeId").val(0);
    $("#hfCardPoints").val(0);
    $("#hfCardPurchasing").val(0);
    $('#hfCardAmountLimit').val(0);
    $('#hfDiscountType').val('');
    $('#hfchkDiscountType').val('');
    $('#ddlDiscountType').val(0);
    $('#ddlDiscountType2').val(0);
    $('#txtLoyaltyCustomer2').val('');
    $('#txtLoyaltyCustomer').val('');
    $('#txtAllowedLimit').val('');
    $('#txtDiscountAvail').val('');
    $('#txtDiscountBalance').val('');
    $('#txtAllowedLimit2').val('');
    $('#txtDiscountAvail2').val('');
    $('#txtDiscountBalance2').val('');

    if (document.getElementById('hfCheckSMS').value == "0") {
        $("#OrderNo1").text($(tr).find("td:eq(0)").text());
        $("#OrderNo2").text($(tr).find("td:eq(15)").text());
        $("#MaxOrderNo").text($(tr).find("td:eq(15)").text());
        var amountDue = $(tr).find("td:eq(4)").text();
        var discount = $(tr).find("td:eq(5)").text();
        $("#hfCustomerId").val($(tr).find("td:eq(12)").text());
        $("#hfCustomerNo").val($(tr).find("td:eq(16)").text());
        //region Loyalty
        $("#txtLoyaltyCard").val($(tr).find("td:eq(17)").text());
        $("#txtLoyaltyCard2").val($(tr).find("td:eq(17)").text());
        $("#hfCardTypeId").val($(tr).find("td:eq(18)").text());
        $("#hfCardPoints").val($(tr).find("td:eq(19)").text());
        $("#hfCardPurchasing").val($(tr).find("td:eq(20)").text());
        $('#hfCardAmountLimit').val($(tr).find("td:eq(21)").text());
        //endregion Loyalty
        if ($("#txtLoyaltyCard").val().length > 0 && $("#txtLoyaltyCard").val() != 'N/A') {
            //$("#txtLoyaltyCard").blur();
        }
        if ($("#txtLoyaltyCard2").val().length > 0 && $("#txtLoyaltyCard2").val() != 'N/A') {
            //$("#txtLoyaltyCard2").blur();
        }
        $('#hfDiscountType').val($(tr).find("td:eq(5)").text());
        $('#hfchkDiscountType').val($(tr).find("td:eq(5)").text());
        try {
            $('#ddlDiscountType').val($(tr).find("td:eq(5)").text());
            $('#ddlDiscountType2').val($(tr).find("td:eq(5)").text());

        } catch (e) { 
            $('#ddlDiscountType').val(-1);
            $('#ddlDiscountType2').val(-1);
        }
        
        $('#ddlDiscountType').change();
        $('#ddlDiscountType2').change();
        if ($(tr).find("td:eq(8)").text() != "null") {
            $('#txtApprovedBy2').val($(tr).find("td:eq(8)").text());
            $('#txtApprovedBy').val($(tr).find("td:eq(8)").text());
        }
        else {
            $('#txtApprovedBy2').val('');
            $('#txtApprovedBy').val('');
        }
        if ($(tr).find("td:eq(9)").text() != "0") {

            $('#txtService').val($(tr).find("td:eq(9)").text());
            $('#txtService2').val($(tr).find("td:eq(9)").text());
        }
        else {

            $('#txtService').val('');
            $('#txtService2').val('');
        }

        if (amountDue > 0) {
            GetPendingBill($(tr).find("td:eq(0)").text());
            calculateDiscount(discount, amountDue);
        }
        else {//For Delivery Type
            $("#lblTotalGrossAmount").text("0.00");
            $('#tble-ordered-products').empty();
            $('#hfOrderedproducts').val('');
        }

        $('#hfDisable').val("1");
    }
    document.getElementById('hfCheckSMS').value = "0";
}
function NewOrder() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    GetPendingBills();
    $("#hfCustomerId").val("0");
    $("#OrderNo1").text("N/A");
    $("#lblTotalGrossAmount").text("0.00");
    $("#hfCounter").val(0);
    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');
    $("#OrderNo2").text('N/A');
    $("#txtRemarks").val('');
}
//#region Printing

//For Removing Duplicates an Array Used in PrintOrder(), UniqueSECTN()
function unique(list) {
    var result = [];
    $.each(list, function (i, e) {
        if ($.inArray(e, result) == -1)
            result.push(e);
    });
    return result;
}

var sort_by = function (field, reverse, primer) {

    var key = primer ?
        function (x) { return primer(x[field]) } :
        function (x) { return x[field] };

    reverse = !reverse ? 1 : -1;

    return function (a, b) {
        return a = key(a), b = key(b), reverse * ((a > b) - (b > a));
    }
}

function PrintOrder() {

    if ($("#hfDisable").val() == "1") {
        var orderedProducts = $("#hfOrderedproducts").val();
        orderedProducts = eval(orderedProducts);

        orderedProducts = orderedProducts.sort(sort_by('C1', false, parseFloat));

        var uniqueSections = $.unique(orderedProducts.map(function (d) { return d.SEC_ID; }));

        uniqueSections = uniqueSections.sort();
        uniqueSections = unique(uniqueSections);


        for (var j = 0; j < uniqueSections.length; j++) {
            $('#detail-section-skus').empty(); // clear all skus  from invoice

            for (var i = 0, len = orderedProducts.length; i < len; i++) {

                if (orderedProducts[i].PRINT == "true") {
                    if (orderedProducts[i].SEC_ID == uniqueSections[j]) {

                        var qty = orderedProducts[i].QTY;
                        $("#SectionName").text(orderedProducts[i].SECTION);
                        $("#CustoerType").text(document.getElementById("hfCustomerType").value);
                        $("#Date").text($("#hfCurrentWorkDate").val());
                        $("#Time").text(moment().format('hh:mm A'));
                        $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());
                        var row = $('<tr ><td>' + orderedProducts[i].C1 + '</td><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + qty + '</td></tr>');
                        $('#detail-section-skus').append(row);
                    }
                }
            }
            if ($("#detail-section-skus tr").length > 0) {

                PrintInvoiceOfSection();

                $('#tble-ordered-products').empty();
                $('#hfOrderedproducts').val('');
                $("#hfCustomerId").val("0");
                $("#OrderNo1").text("N/A");
                $("#hfCounter").val(0);
                document.getElementById('dvDealUpdate').style.display = 'none';
                $("#txtDealQty").val('1');

            }
        }
    }
}

//#endregion Printing Section 


//function checkTable()
//{
//    $('#tble-ordered-products tr').each(function (row, tr) {
//        if ($(tr).find('td:eq(19)').text() == "2") {
//            if ($(tr).find('td:eq(5) input').val() == "") {
//                  return -1;
//            }
//        }
//    });
//    return 0;
//}

// #region HoldOrder
function HoldOrder() {

    if (($("#lnkDelivery").attr("class")) == "box active") {
        if (($("#hfCustomerId").val() == "0")) {
            Error("Please select customer");
            return;
        }
    }
    SaveOrder();
}

$('#dvHold').click(function (e) {
    document.getElementById("hfCustomerType").value = "2";
    if (CheckCatDealQty($("#hfDealId").val())) {
        if (($("#lnkDelivery").attr("class")) == "box active") {
            if ((ddlCustomerPOS.GetValue() == 0)) {
                Error("Please select customer");
                return;
            }
        }
        var table = document.getElementById('tble-ordered-products');
        if (table.rows.length == 0) {
            Error("Please enter Items");
            return;
        }

        var me = $(this);
        e.preventDefault();
        if (me.data('requestRunning')) {
            return;
        }

        me.data('requestRunning', true);
        $("#dvHold").attr("disabled", true);

        var Id = 0;
        $.ajax
                ({
                    type: "POST", //HTTP method
                    url: "frmOrderPOSCentralized.aspx/HoldOrder", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ orderedProducts: $('#hfOrderedproducts').val(), orderBooker: Id, coverTable: 0, customerType: "Delivery", CustomerName: ddlCustomerPOS.GetValue(), maxOrderNo: $("#MaxOrderNo").text(), printType: document.getElementById("hfBookingType").value, tableName: '0', takeAwayCustomer: '', bookerName: '', tabId: 0, LocationID: ddlLocation.GetValue(), remarks: $("#txtRemarks").val() }),
                    success: OrderSaved,
                    error: OnError,
                    complete: function () {
                        $("#dvHold").attr("disabled", false);
                        me.data('requestRunning', false);

                    }
                });
    }
});

function UniqueSection() {

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);

    var uniqueSections = $.unique(orderedProducts.map(function (d) { return d.SEC_ID; }));

    uniqueSections = uniqueSections.sort();
    uniqueSections = unique(uniqueSections);

    for (var j = 0; j < uniqueSections.length; j++) {
        $('#detail-section-skus').empty(); // clear all skus  from invoice

        for (var i = 0, len = orderedProducts.length; i < len; i++) {
            if (orderedProducts[i].PRINT == "true") {
                if (orderedProducts[i].SEC_ID == uniqueSections[j]) {

                    var qty = orderedProducts[i].QTY - orderedProducts[i].PR_COUNT;

                    var IS_VOID = orderedProducts[i].VOID;

                    if (IS_VOID == "false") {
                        if (qty > 0) {

                            $("#SectionName").text(orderedProducts[i].SECTION);
                            $("#CustoerType").text($("#hfCustomerType").val().toUpperCase());
                            $("#Date").text($("#hfCurrentWorkDate").val());
                            $("#Time").text(moment().format('hh:mm A'));
                            $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());
                            var row = $('<tr ><td>' + orderedProducts[i].C1 + '</td><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + qty.toFixed(3) + '</td></tr>');
                            $('#detail-section-skus').append(row);
                        }

                        if (orderedProducts[i].QTY == 0) {
                            if (qty == 0) {

                                $("#SectionName").text(orderedProducts[i].SECTION);
                                $("#CustoerType").text($("#hfCustomerType").val());
                                $("#Date").text($("#hfCurrentWorkDate").val());
                                $("#Time").text(moment().format('hh:mm A'));
                                $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());

                                var row = $('<tr ><td>' + orderedProducts[i].C1 + '</td><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td></tr>');
                                $('#detail-section-skus').append(row);
                            }
                        }
                    }
                }
            }
        }
        if ($("#detail-section-skus tr").length > 0) {

            PrintInvoiceOfSection();
            $('#tble-ordered-products').empty();
            $('#hfOrderedproducts').val('');
            $("#hfCustomerId").val("0");
            $("#OrderNo1").text("N/A");
            $("#hfCounter").val(0);
            document.getElementById('dvDealUpdate').style.display = 'none';
            $("#txtDealQty").val('1');
        }
    }
}

function PrintInvoiceOfSection() {
    $("#hfDisable").val("0");
    $.print("#invoice-kitchen");
}

function SaveOrder() {
    document.getElementById("hfCustomerType").value = "2";
    if (($("#lnkDelivery").attr("class")) == "box active") {
        if (($("#hfCustomerId").val() == "0")) {
            Error("Please select customer");
            return;
        }
    }
    if (CheckCatDealQty($("#hfDealId").val())) {
        $("#dvHold").attr("disabled", true);
        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOSCentralized.aspx/HoldOrder", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ orderedProducts: $('#hfOrderedproducts').val(), orderBooker: Id, coverTable: 0, customerType: "Delivery", CustomerName: ddlCustomerPOS.GetValue(), maxOrderNo: $("#MaxOrderNo").text(), printType: document.getElementById("hfBookingType").value, tableName: '0', takeAwayCustomer: '', bookerName: '', tabId: 0, LocationID: ddlLocation.GetValue() }),
                    success: function () {
                        //this case use for Delivery bcz when table is empty order saved function (unique Section) was not working 
                        if ($("#tble-ordered-products tr").length > 0) {//When Table is not empty for Delivery  else load tables
                            OrderSaved();
                        }
                        else {
                            GetPendingBills();
                        }
                        if (document.getElementById("hfCustomerType").value == "Delivery") {
                            SMS(document.getElementById('hfCustomerNo').value, "1");
                        }
                    },
                    error: OnError,
                    complete: function () {
                        $("#dvHold").attr("disabled", false);
                    }

                }
            );
    }
}

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


$('#btnVoid').click(function (e) {
    
    var me = $(this);
    e.preventDefault();

    if (me.data('requestRunning')) {
        return;
    }

    me.data('requestRunning', true);

    $.ajax
            ({
                type: "POST", //HTTP method
                url: "frmOrderPOSCentralized.aspx/VoidOrder", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ orderId: $("#OrderNo1").text() }),
               // success: ClearOnCancel,
                error: OnError,
                complete: function () {
                    ClearOnCancel();
                    me.data('requestRunning', false);
                }
            });
   
});
//----------------on Discount Type button Click---------------------------------\\
function DiscType(myDiscType) {

    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";

    //---on print invoice popup
    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";
    //-----------------------------------------------------------------------\\

    myDiscType.style["background-color"] = "#7dab49";
    var currentValue = myDiscType.value;

     document.getElementById('txtDiscount').disabled = false;
     document.getElementById('txtApprovedBy').disabled = false;

    document.getElementById('txtDiscount2').disabled = false;
    document.getElementById('txtApprovedBy2').disabled = false;

    $('#hfDiscountType').val(currentValue);

    if (myDiscType.id == 'percentage2' || myDiscType.id == 'value2') {

        CalculateBalance2();
    }
    else {

        CalculateBalance();
    }

}


//-----------------Calcualte on discount and cash Recd on Payment Click PopUp----\\ 

function CalculateBalance() {

    var balance = 0.00;
    var amountDue = 0.00;
    var discount = 0.00;
    var gst = 0.00;

    discount = document.getElementById('txtDiscount').value;

    var discountType = $('#hfDiscountType').val(); //  document.getElementById("hfDiscountType").value;
    var cashRcd = 0.00;

    cashRcd = document.getElementById('txtCashRecieved').value;

    var grandTotal = 0.00;
    grandTotal = $("#GrandTotal").text();


    if ($('#hfServiceCharges').val() == "False") {
        gst = document.getElementById("hfSalesTax").value;
    }
    else {
        gst = document.getElementById("txtService").value;
    }

    $("#lblBalance").text('');
    $("#lblPaymentDue").text('');

    if ((discount == "")) {
        discount = 0.00;
    }
    if ((cashRcd == "")) {
        cashRcd = 0.00;
    }
    if ((gst == "")) {
        gst = 0.00;
    }

    if (discountType == 0) {

        discount = (parseFloat(grandTotal) * parseFloat(discount / 100)).toFixed(2);
        gst = (parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount))).toFixed(2);
        balance = parseFloat(grandTotal) - parseFloat(discount) + parseFloat(gst);
        amountDue = balance;
        balance = cashRcd - balance;
    }
    else if (discountType == 1) {
        gst = (parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount))).toFixed(2);
        balance = (parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount)).toFixed(2);
        amountDue = balance;
        balance = (cashRcd - balance).toFixed(2);
    }
    else {
        gst = (parseFloat(gst / 100) * (parseFloat(grandTotal - discount))).toFixed(2);
        balance = parseFloat(grandTotal).toFixed(2) + parseFloat(gst).toFixed(2);
        amountDue = parseFloat(balance).toFixed(2) - parseFloat(discount).toFixed(2);
        balance = (cashRcd - amountDue).toFixed(2);
    }
    if (cashRcd == 0.00) {
        balance = 0.00;
    }

    $("#lblDiscountTotal").text(parseFloat(discount).toFixed(2));
    $("#lblGSTTotal").text(parseFloat(gst).toFixed(2));
    $("#lblBalance").text(parseFloat(balance).toFixed(2));
    $("#lblPaymentDue").text(parseFloat(amountDue).toFixed(2));
}

function changeClass(btn) {
    var a, n;
    a = document.getElementById("dv_lstCategory").children;
    for (n = 0; n < a.length; n++) {
        a[n].style["background-color"] = "#d4def7";
    }
    btn.style["background-color"] = "#7DAB49";
}
function changeSubCatClass(btn) {
    var a, n;
    var a = document.getElementById("dv_lstSubCategory").children;
    for (n = 0; n < a.length; n++) {
        a[n].style["background-color"] = "#d4def7";
    }
    btn.style["background-color"] = "#7DAB49";

}
function changeProductClass(btn) {
    var a, n;
    a = document.getElementById("dv_lstProducts").children;
    for (n = 0; n < a.length; n++) {
        a[n].style["background-color"] = "#e2e3e8";
    }
    btn.style["background-color"] = "#7DAB49";
}
// #region Item Deletion

function UserValidated(UserAuth) {

    UserAuth = JSON.stringify(UserAuth);
    var result = jQuery.parseJSON(UserAuth.replace(/&quot;/g, '"'));
    UserAuth = eval(result.d);

    document.getElementById('txtUserID').value = "";
    document.getElementById('txtUserPass').value = "";


    $('#UserValidation').hide("slow");
    if ($("#hfUserClick").val() == "Decrease") {
        $('#DecreaseConfirmation').show("slow");
    }
    else {
        $('#DeletionConfirmation').show("slow");
    }
}

function CancelUserValidation() {

    $('#UserValidation').hide("slow");
}

function deleteItem(btn) {

    var rowIndex = $(btn).parent();
    $(rowIndex).closest('tr').remove();
    var tableData = storeTblValuesInvoice();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    setTotals();
}

function deleteItemInvoice(btn) {
    var rowIndex = $(btn).parent();
    $(rowIndex).closest('tr').remove();
    var tableData = storeTblValuesInvoice();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    setTotals();
}
function deleteQty() {
    var Sku_Id = $("#hfskuId").val();
    var Cat_Id = $("#hfcatId").val();
    var C1 = $("#hfDelIndex").val();

    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(0)').text() == Sku_Id && $(tr).find('td:eq(14)').text() == Cat_Id && $(tr).find('td:eq(12)').text() == C1) {
            $(tr).find('td:eq(13)').text(true);
            $(tr).find('td:eq(7)').css('background-color', '#b20505');

        }
    });
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    setTotals();

    $("#hfskuId").val('');
    $("#hfcatId").val('');
    $("#hfDelIndex").val('')
}
function DeletionProceed() {
    $('#DeletionConfirmation').hide("slow");
    deleteQty();
    $('#hfDisable').val("0");
}
function DeletionCancel() {
    $('#DeletionConfirmation').hide("slow");
    $("#hfskuId").val('');
    $("#hfcatId").val('');
    $("#hfUserClick").val('');
    $("#hfDelIndex").val('');

}

// #endregion Item Deletion


// #region Decrease Qty


function decreaseItem(btn) {

    var rowIndex = $(btn).parent();
    var Sku_id = $(rowIndex).find('td:eq(0)').text();
    var Cat_id = $(rowIndex).find('td:eq(14)').text();
    var SkuType = $(rowIndex).find('td:eq(24)').text();//Check Hold or Not for new '' and for already Hold 'H'
    var qty = $(rowIndex).find('td:eq(3) input').val();
    if (qty > 1) {
        $("#hfskuId").val(Sku_id);
        $("#hfcatId").val(Cat_id);
        $("#hfDelId").val($(rowIndex).find('td:eq(21)').text());
        $("#hfDelIndex").val($(rowIndex).find('td:eq(12)').text());
        if (SkuType == "") {
            minusQty();
        }
        else {
            document.getElementById('txtUserID').value = "";
            document.getElementById('txtUserPass').value = "";
            $("#hfUserClick").val('Decrease');
            $('#UserValidation').show("slow");
            $('#txtUserID').focus();
        }
    }
}

function minusQty() {

    var Sku_Id = $("#hfskuId").val();
    var Cat_Id = $("#hfcatId").val();
    var Deal_Id = $("#hfDelId").val();
    var C1 = $("#hfDelIndex").val();

    var qty = 1;
    var nQty = 0;
    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(0)').text() == Sku_Id && $(tr).find('td:eq(14)').text() == Cat_Id && $(tr).find('td:eq(12)').text() == C1) {
            qty = $(tr).find('td:eq(3) input').val();
            if (Deal_Id != 0) {
                if (qty > parseFloat($(tr).find('td:eq(27)').text()) * parseFloat($(tr).find('td:eq(28)').text())) {
                    nQty = qty - 1;
                    $(tr).find('td:eq(3) input').val(parseFloat(nQty).toFixed(3));
                }
            }
            else {

                if (qty == "1") {

                } else {

                    nQty = qty - 1;
                    $(tr).find('td:eq(3) input').val(parseFloat(nQty).toFixed(3));
                }
            }
            var price = $(tr).find('td:eq(5)').text();
            var amount = parseFloat(nQty * price).toFixed(2);
            $(tr).find('td:eq(6)').text(amount);

            setTotals();
        }
    });
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    $("#hfskuId").val('');
    $("#hfcatId").val('');
    $("#hfDelId").val('');
    $("#hfDelIndex").val('');
}
function DecreaseProceed() {
    $('#DecreaseConfirmation').hide("slow");

    minusQty();

    $('#hfDisable').val("0");
}

function DecreaseCancel() {
    $('#DecreaseConfirmation').hide("slow");
    $("#hfskuId").val('');
    $("#hfcatId").val('');
    $("#hfUserClick").val('');
    $("#hfDelIndex").val('');
    $("#hfDelId").val('');

}

// #endregion Decrease Qty

//------------------------on Print INvoice Click ---------------\\

//-------get Discount,Discount Type from dataBase on Bill selectoin and set on Print Invoice,Payment PopUp--\\

function isDisable(flag) {

    if (flag == 'true') {


        document.getElementById("percentage").disabled = true;
        document.getElementById("value").disabled = true;
    }
    else {

        if (document.getElementById("hfCustomerType").value == "Takeaway") {

            document.getElementById("percentage").disabled = false;
            document.getElementById("value").disabled = false;

        }
    }
}

function isColored(flag) {
    if (flag == 'true') {

        document.getElementById("percentage2").style["background-color"] = "#7dab49";
        document.getElementById("percentage").style["background-color"] = "#7dab49";

        document.getElementById("value2").style["background-color"] = "#919399";
        document.getElementById("value").style["background-color"] = "#919399";
    }
    else {
        document.getElementById("value2").style["background-color"] = "#7dab49";
        document.getElementById("value").style["background-color"] = "#7dab49";

        document.getElementById("percentage2").style["background-color"] = "#919399";
        document.getElementById("percentage").style["background-color"] = "#919399";
    }
}
function calculateDiscount(discount, amountDue) {

    $('#lblDiscountTotal').text('0');
    $("#lblDiscountTotal2").text('0');

    document.getElementById('txtDiscount').value = "";
    document.getElementById('txtDiscount2').value = "";

    var salesTax = 0;
    if ($('#hfServiceCharges').val() == "False") {
        salesTax = document.getElementById("hfSalesTax").value;
        salesTax = parseFloat(salesTax) / 100 * amountDue;
    }
    else {
        salesTax = document.getElementById("txtService2").value;
    }
    if (salesTax == "") {
        salesTax = 0;
    }
    var Total = parseFloat(amountDue).toFixed(2);

    if (discount > 0) {

        document.getElementById('txtDiscount').value = discount;
        document.getElementById('txtDiscount2').value = discount;


        if ($('#hfDiscountType').val() == 0) {

            discount = Total * (discount / 100);

            $("#lblDiscountTotal2").text(discount.toFixed(2));
            $("#lblDiscountTotal").text(discount.toFixed(2));


            isColored('true');

        }
        else {

            $("#lblDiscountTotal2").text(discount.toFixed());
            $("#lblDiscountTotal").text(discount.toFixed(2));

            isColored('false');

        }
        document.getElementById('txtDiscount2').disabled = true;

        if (document.getElementById("hfCustomerType").value == "Takeaway") {

            isDisable('false');
        }
        else {

            isDisable('true');
        }
        document.getElementById("lblGSTTotal").innerHTML = salesTax.toFixed(2);
        document.getElementById("lblGSTTotal2").innerHTML = salesTax.toFixed(2);
    }
    else {
        isDisable(false);
    }
}

//--------------------Calcualte on discount  on Print Invoice Click PopUp----\\ 
function CalculateBalance2() {
    var balance = 0.00;
    var amountDue = 0.00;
    var discount = 0.00;
    discount = document.getElementById('txtDiscount2').value;
    var discountType = $('#hfDiscountType').val();
    var grandTotal = $("#GrandTotal2").text();
    var gst = 0.00;
    if ($('#hfServiceCharges').val() == "False") {
        gst = document.getElementById("hfSalesTax").value;
    }
    else {
        gst = document.getElementById("txtService2").value;
    }

    $("#lblBalance").text('');
    $("#lblPaymentDue").text('');

    if (discount == "") {
        discount = 0.00;
    }

    if (gst == "") {
        gst = 0.00;
    }
    if (discountType == 0) {
        discount = (parseFloat(grandTotal) * parseFloat(discount) / 100).toFixed(2);
        gst = (parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount))).toFixed(2);
        amountDue = (parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount)).toFixed(2);
        amountDue = amountDue;
    }
    else {
        gst = (parseFloat(gst / 100) * (parseFloat(grandTotal - discount))).toFixed(2);
        amountDue = (parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount)).toFixed(2);
        amountDue = amountDue;
    }
    $("#lblPaymentDue2").text(amountDue);
    $("#lblDiscountTotal2").text(discount);
    $("#lblGSTTotal2").text(gst);
}

//----------------Can enter only numbers and decimal ---------------------------\\

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
function onlyDotsAndNumbers(txt, event) {

    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        if (txt.value.indexOf(".") < 0)
            return true;
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}

//------------------------------------------------------------------------------\\

//#region Time Calculation

function BillTime() {

    var refresh = 1000; // Refresh rate in milli seconds

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


function SaveCustomer() {

    if (ValidateCustomer()) {

        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOSCentralized.aspx/InsertCustomer", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ cardID: $('#txtCustomerCardNo').val(), CNIC: $('#txtCustomerCNIC').val(), contactNumer: $('#txtPrimaryContact').val(), contactNumer2: $('#txtOtherContact').val(), customerName: $('#txtCustomerName').val(), address: $('#txtCustomerAddress').val(), DOB: $('#txtCustomerDOB').val(), OpeningAmount: $('#txtOpeningAmount').val(), Nature: $('#txtNature').val(), email: $('#txtEmail').val(), LocationID: ddlLocation.GetValue() }),
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
               url: "frmOrderPOSCentralized.aspx/LoadAllCustomers", //page/method name
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               data: JSON.stringify({ customerName: $('#txtCustomerSearch').val(), type: "Search", LocationID: ddlLocation.GetValue() }),
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
//Load Location
function LoadLocationDropDown() {
    $.ajax
       (
           {
               type: "POST", //HTTP method
               url: "frmOrderPOSCentralized.aspx/LoadLocation", //page/method name
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: LocationDropDown
           }
 );
}
function LocationDropDown(locations) {
    locations = JSON.stringify(locations);
    var result = jQuery.parseJSON(locations.replace(/&quot;/g, '"'));
    locations = eval(result.d);
    var locations2 = JSON.stringify(locations);
    document.getElementById("hfLocations").value = locations2;

    ddlLocation.ClearItems();
    ddlLocation.BeginUpdate();
    for (var i = 0, len = locations.length; i < len; i++) {
        ddlLocation.AddItem(locations[i].DISTRIBUTOR_NAME, locations[i].DISTRIBUTOR_ID);
    }
    ddlLocation.EndUpdate();
    ddlLocation.SetSelectedIndex(0);
    MaxOrderNo();
    loadAllProducts();
    GetPendingBills();
    LoadCustomerDropDown();

}
//Load Customer in Customer Dropdown List
function LoadCustomerDropDown() {
    $.ajax
       (
           {
               type: "POST", //HTTP method
               url: "frmOrderPOSCentralized.aspx/LoadAllCustomers", //page/method name
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               data: JSON.stringify({ customerName: '', type: "All", LocationID: ddlLocation.GetValue() }),
               success: CustomerDropDown
           }
 );
}
function CustomerDropDown(customers) {
    customers = JSON.stringify(customers);
    var result = jQuery.parseJSON(customers.replace(/&quot;/g, '"'));
    customers = eval(result.d);
    var customers2 = JSON.stringify(customers);
    document.getElementById("hfCustomers").value = customers2;

    ddlCustomerPOS.ClearItems();
    ddlCustomerPOS.AddItem('Select Customer', 0);
    var MaxID = 0;
    ddlCustomerPOS.SetSelectedIndex(0);
    if (customers.length > 0) {
        ddlCustomerPOS.BeginUpdate();
        MaxID = customers[customers.length - 1].CUSTOMER_ID;
        for (var i = 0, len = customers.length; i < len; i++) {
            ddlCustomerPOS.AddItem(customers[i].CUSTOMER_NAME + '-' + customers[i].ADDRESS + '-' + customers[i].CONTACT_NUMBER, customers[i].CUSTOMER_ID);
        }
        ddlCustomerPOS.EndUpdate();
        if ($('#hfCustomerAdd').val() == '1') {
            ddlCustomerPOS.SetValue(MaxID);
        }
    }
}
//On Save Customer
function LoadCustomers() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOSCentralized.aspx/LoadAllCustomers", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ customerName: "", type: "Max", LocationID: ddlLocation.GetValue() }),
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

        var row = $('<tr ><td style="display:none;">' + customers[i].CUSTOMER_ID + '</td><td class="leftval">' + customers[i].CUSTOMER_NAME + '</td><td class="leftval">' + customers[i].CARD_NO + '</td><td class="leftval">' + customers[i].CONTACT_NUMBER + '</td><td class="leftval"  style="display:none;">' + customers[i].EMAIL_ADDRESS + '</td><td class="leftval"  style="display:none;">' + customers[i].CNIC + '</td><td class="leftval" style="display:none;">' + customers[i].REGDATE + '</td><td class="leftval">' + customers[i].ADDRESS + '</td><td style="display:none;" align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td>' + '<td align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-share-square-o"></span></a></td></tr>');
        $('#tbl-customers').append(row);
    }

    var table = document.getElementById('tbl-customers');
    var lastRow = table.rows[table.rows.length - 1];

    ShowCustomer2(lastRow);
    HoldOrder();
    LoadLastBill();
}


//After Insertion Select Last Customer
function ShowCustomer2(rowIndex) {
    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    document.getElementById('hfCustomerNo').value = $(rowIndex).find('td:eq(3)').text();
    document.getElementById('divCustomer').style.visibility = 'hidden';
    $("#tbl-customers").empty();
}

//Get Customer Detail on Selection Button on row click
function ShowCustomer(obj) {
    var rowIndex = $(obj).parent();
    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    ddlCustomerPOS.SetValue($(rowIndex).find('td:eq(0)').text());
    document.getElementById('hfCustomerNo').value = $(rowIndex).find('td:eq(3)').text();
    document.getElementById('divCustomer').style.visibility = 'hidden';
    $('#txtCustomerSearch').val('');
    $("#tbl-customers").empty();
    HoldOrder();
    LoadLastBill();
}

function LoadLastBill() {

    setTimeout(function () {
        var table = document.getElementById('tbl-pending-bills');
        var lastRow = table.rows[table.rows.length - 1];

        if ($(lastRow).find("td:eq(11)").text() == "DLY") {

            var amountDue = $(lastRow).find("td:eq(3)").text();

            if (amountDue == 0) {

                ShowBill(lastRow);
            }
        }
        else {
            ShowBill(lastRow);
        }
    }, 1000);
}

//#endregion


//#region SMS

function SendSMS(obj) {

    var rowIndex = $(obj).parent();
    document.getElementById('hfCustomerNo').value = $(rowIndex).find('td:eq(15)').text();

    SMS(document.getElementById('hfCustomerNo').value, "2");
    document.getElementById('hfCheckSMS').value = "1";
}

//msgType: 1 for hold, 2 for Ride
function SMS(Number, Type) {
    //$.blockUI();

    //$.ajax
    //       (
    //           {
    //               type: "POST", //HTTP method
    //               url: "frmOrderPOSCentralized.aspx/SendSMS", //page/method name
    //               contentType: "application/json; charset=utf-8",
    //               dataType: "json",
    //               data: JSON.stringify({ customerNo: Number, msgType: Type }),
    //               success: function (data) {
    //                   if (data.d == "100" || data.d == "Message Sent To Telecom") {
    //                       Succes(data.d);
    //                   } else {
    //                       Error(data.d);
    //                   }
    //               },
    //               error: OnError,
    //               complete: function () {

    //                   $.unblockUI();
    //               }
    //           }
    //       );
}

//#endregion


//region Clear

//ON Hold Order
function ClearOrder() {
    $('#txtItemCode').val('');
    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $('#hfCustomerId').val('0');
    GetPendingBills();
    $('#hfDisable').val("0");
    $("#OrderNo1").text("N/A");
    $("#OrderNo2").text('N/A');
    $("#lblTotalGrossAmount").text("0.00");
    $("#hfCounter").val(0);
    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');
    $("#txtRemarks").val('');
    MaxOrderNo();
}

//ON Payment 
function Clear() {
    $('#txtItemCode').val('');
    $('#hfSaleType').val(0);
    $('#tble-invoice-products').empty();
    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $('#hfPaymentType').val('');
    $('#txtDiscount').val('');
    $('#txtCashRecieved').val('');
    $('#txtApprovedBy').val('');
    $('#txtApprovedBy2').val('');
    $("#lblDiscountTotal").text('0.00');
    $("#lblTotalGrossAmount").text("0.00");
    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#hfchkDiscountType').val('');
    $('#payment').hide("slow");
    $("#OrderNo1").text("N/A");
    $('#hfCustomerId').val('0');
    $('#hfCustomerNo').val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $("#hfCounter").val(0);
    $("#OrderNo2").text('N/A');
    $("#lblTotalGrossAmount").text('0.00');
    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');
    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";
    document.getElementById("cash").style["background-color"] = "#919399";
    document.getElementById("credit").style["background-color"] = "#919399";
    $("select#ddlDiscountType").prop('selectedIndex', 0);
    loadUsers(document.getElementById("ddlDiscountType"));
    document.getElementById('dv_lstProducts').style.display = "block";
    document.getElementById('dv_lstCategory').style.display = "block";
    $("#txtRemarks").val('');
}
function ClearOnCancel() {
    $('#hfSaleType').val(0);
    $('#tble-ordered-products').empty();
    $("#lblTotalGrossAmount").text("0.00");
    $('#tble-invoice-products').empty();
    $('#hfOrderedproducts').val('');
    $('#hfPaymentType').val('');
    $('#txtDiscount').val('');
    $('#txtCashRecieved').val('');
    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#hfchkDiscountType').val('');
    $('#txtApprovedBy').val('');
    $('#txtApprovedBy2').val('');
    $('#payment').hide("slow");
    $("#OrderNo1").text("N/A");
    $("#OrderNo2").text('N/A');
    $('#hfCustomerId').val('0');
    $('#hfCustomerNo').val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $("#hfCounter").val(0);
    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');
    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";
    document.getElementById("cash").style["background-color"] = "#919399";
    document.getElementById("credit").style["background-color"] = "#919399";
    MaxOrderNo();
    $("select#ddlDiscountType").prop('selectedIndex', 0);
    loadUsers(document.getElementById("ddlDiscountType"));
    $("#txtRemarks").val('');
    document.getElementById('dv_lstProducts').style.display = "block";
    document.getElementById('dv_lstCategory').style.display = "block";
    $('#txtItemCode').val('');
}

//ON Print Invoice
function Clear2() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $("#lblTotalGrossAmount").text("0.00");
    $('#txtDiscount2').val('');
    $('#txtApprovedBy2').val('');
    $('#txtApprovedBy').val('');
    $('#hfDiscountType').val('');
    $('#hfchkDiscountType').val('');
    $('#hfDisable').val("0");
    $("#OrderNo1").text("N/A");
    $("#OrderNo2").text('N/A');
    $("#txtService").val('');
    $("#txtService2").val('');
    $('#hfCustomerId').val('0');
    $('#hfCustomerNo').val('');
    $('#payment2').hide("slow");

    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');

    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";
    $("#txtRemarks").val('');

    $('#dvDiscount2').find('*').prop('disabled', false);

    document.getElementById('txtDiscount2').disabled = true;
    document.getElementById('txtApprovedBy2').disabled = true;

    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;

    $("#txtLoyaltyCard").val('');
    $('#hfCardTypeId').val('0');
    $('#hfCardPoints').val('0');
    $('#hfCardPurchasing').val('0');
    $('#hfCardAmountLimit').val('0');
    $('#txtItemCode').val('');
    document.getElementById('dv_lstProducts').style.display = "block";
    document.getElementById('dv_lstCategory').style.display = "block";
}
function ClearOnCancel2() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $("#lblTotalGrossAmount").text("0.00");
    $('#txtDiscount2').val('');
    $('#txtDiscount').val('');
    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#hfchkDiscountType').val('');
    $('#txtApprovedBy2').val('');
    $('#txtApprovedBy').val('');
    $("#OrderNo1").text("N/A");
    $("#OrderNo2").text('N/A');
    $('#hfCustomerId').val('0');
    $('#hfCustomerNo').val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $('#payment2').hide("slow");
    document.getElementById('dvDealUpdate').style.display = 'none';
    $("#txtDealQty").val('1');

    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";

    $('#txtItemCode').val('');

    $('#dvDiscount2').find('*').prop('disabled', false);

    document.getElementById('txtDiscount2').disabled = true;
    document.getElementById('txtApprovedBy2').disabled = true;

    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;


    $("#txtLoyaltyCard").val('');
    $('#hfCardTypeId').val('0');
    $('#hfCardPoints').val('0');
    $('#hfCardPurchasing').val('0');
    $('#hfCardAmountLimit').val('0');
    $("#txtRemarks").val('');
    MaxOrderNo();
    document.getElementById('dv_lstProducts').style.display = "block";
    document.getElementById('dv_lstCategory').style.display = "block";
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
    LoadCustomerDropDown();
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
    document.getElementById('divCustomer').style.visibility = 'hidden';
    $('#hfCustomerAdd').val('0');
}
function AddRowToGrid(e) {
    var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
    if (key == 13) {
        e.preventDefault();
        var lstProducts = document.getElementById("hfProduct").value;
        lstProducts = eval(lstProducts);
        var skuCode = $('#txtItemCode').val();
        if (skuCode != '') {
            var flage = false;
            var flagItemFound = false;
            var row = "";
            for (var i = 0, len = lstProducts.length; i < len; ++i) {
                if (lstProducts[i].SKU_CODE == skuCode) {
                    flage = false;
                    flagItemFound = true;
                    row = "";
                    $('#tble-invoice-products').find('tr').each(function () {
                        var td1 = $(this).find("td:eq(0)").text();
                        if (lstProducts[i].SKU_ID == td1) {
                            $(this).find("td:eq(2) input").val((parseFloat($(this).find("td:eq(2) input").val()) + 1).toFixed(3));
                            $(this).find("td:eq(4)").text((parseFloat($(this).find("td:eq(3) input").val()) * parseFloat($(this).find("td:eq(2) input").val())).toFixed(2));
                            flage = true;
                        }
                    });
                    if (flage) break;
                    row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" colspan="3"><input type="text" value="' + parseFloat(lstProducts[i].DEFAULT_QTY).toFixed(3) + '"  style="text-align: center;" disabled></td><td><input type="text"  value="' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '" disabled></td><td class="table-text2">' + (parseFloat(lstProducts[i].T_PRICE) * parseFloat(lstProducts[i].DEFAULT_QTY)).toFixed(2) + '</td><td align="center" onclick="deleteItemInvoice(this);"><a href="#"><span class="fa fa-times"></span></a></td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">false</td><td style="display:none;"> ' + lstProducts[i].UOM_ID + '</td><td style="display:none;"> ' + lstProducts[i].IntStockMUnitCode + '</td><td style="display:none;"> ' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;"> ' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;"> ' + lstProducts[i].DEFAULT_QTY + '</td><td style="display:none;"> ' + lstProducts[i].INVOICE_ID + '</td></tr>');
                    $("#tble-invoice-products").append(row);
                    break;
                }
            }
            if (!flagItemFound) {
                var barCode = skuCode.toString().substring(0, 7);
                var ItemWeight = skuCode.toString().substring(7, 12);
                ItemWeight = ItemWeight.slice(0, 2) + "." + ItemWeight.slice(2);

                for (var i = 0, len = lstProducts.length; i < len; ++i) {
                    if (lstProducts[i].SKU_CODE == barCode.toUpperCase()) {
                        flage = false;
                        flagItemFound = true;
                        row = "";
                        $('#tble-invoice-products').find('tr').each(function () {
                            var td1 = $(this).find("td:eq(0)").text();
                            if (lstProducts[i].SKU_ID == td1) {
                                $(this).find("td:eq(2) input").val((parseFloat($(this).find("td:eq(2) input").val()) + parseFloat(ItemWeight)).toFixed(3));
                                $(this).find("td:eq(4)").text((parseFloat($(this).find("td:eq(3) input").val()) * parseFloat($(this).find("td:eq(2) input").val())).toFixed(2));
                                flage = true;
                            }
                        });
                        if (flage) break;
                        row = $('<tr><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" colspan="3"><input type="text" value="' + parseFloat(ItemWeight).toFixed(3) + '"  style="text-align: center;" disabled></td><td><input type="text"  value="' + parseFloat(lstProducts[i].T_PRICE).toFixed(2) + '" disabled></td><td class="table-text2">' + (parseFloat(lstProducts[i].T_PRICE) * parseFloat(ItemWeight)).toFixed(2) + '</td><td align="center" onclick="deleteItemInvoice(this);"><a href="#"><span class="fa fa-times"></span></a></td><td align="center" onclick="AddRow(this);" style="display:none;"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td><td style="display:none;">false</td><td style="display:none;"> ' + lstProducts[i].UOM_ID + '</td><td style="display:none;"> ' + lstProducts[i].IntStockMUnitCode + '</td><td style="display:none;"> ' + lstProducts[i].Stock_to_SaleOperator + '</td><td style="display:none;"> ' + lstProducts[i].Stock_to_SaleFactor + '</td><td style="display:none;"> ' + lstProducts[i].DEFAULT_QTY + '</td><td style="display:none;"> ' + lstProducts[i].INVOICE_ID + '</td></tr>');
                        $("#tble-invoice-products").append(row);
                        break;
                    }
                }
            }
            if (flagItemFound) {
                $('#txtItemCode').val('');
                tableData = storeTblValuesInvoice();
                tableData = JSON.stringify(tableData);
                document.getElementById('hfOrderedproducts').value = tableData;
                setTotals();
            }
            else {
                $('#txtItemCode').focus();
                Error('Item Not Found');
            }
        }
    }
}
function ShowCustomerPopup() {
    $('#hfCustomerAdd').val('1');
    document.getElementById('divCustomer').style.visibility = 'visible';
}
function LocationChanged(SelectedValue)
{
    $('#hfCustomerAdd').val('0');
    MaxOrderNo();

    //Clear Items
    var dvLstProducts = document.getElementById("dv_lstProducts");
    while (dvLstProducts.hasChildNodes()) {
        dvLstProducts.removeChild(dvLstProducts.lastChild);
    }

    loadAllProducts();
    GetPendingBills();
    LoadCustomerDropDown();
}
//----------------------------------------------------------