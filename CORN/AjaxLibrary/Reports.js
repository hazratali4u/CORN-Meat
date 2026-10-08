function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

$('#btnShowReports').click(function (e) {

    if (document.getElementById('hfReport').value != "") {

        $('#reports-buttons').lightbox_me({
            centered: true,
            onLoad: function () {
                $('#reports-buttons').find('input:first').focus();
            }
        });

        var obj = jQuery.parseJSON(document.getElementById('hfReport').value);

        for (var i = 0; i < obj.length; i++) {
            var item = obj[i];
            $("#reports-buttons").find("#" + item.MODULE_KEY).css("display", "block");
        }

    }
    else {
        waitLoading('No report assign');

    }

    e.preventDefault();
});

$('#sales-summary').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectSalesSummary", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowSalesSummary
                }
            );

});

var currentdate = new Date();
var datetime =  currentdate.getDate() + "/"
                + (currentdate.getMonth() + 1) + "/"
                + currentdate.getFullYear() + " "
                + moment().format('hh:mm A')

function ShowSalesSummary(data) {

    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);
    $("#lblAddress").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    

    $("#DateTimeSummary").text(datetime);
   
    $("#g-sale").text(": " + numberWithCommas(parseFloat(data[0].GROSS_AMOUNT).toFixed(2)));

    if ($('#hfServiceCharges').val() == "False") {


        $("#gst-text").text("G.S.T");
    }
    else {

        $("#gst-text").text("Service Charges");
    }
    $("#gst-amount").text(": " + numberWithCommas(parseFloat(data[0].GST_AMOUNT).toFixed(2)));

    $("#totalAmount").text(": " + numberWithCommas(data[0].TOTAL_AMOUNT));
    $("#discount-amount").text(": " + numberWithCommas(parseFloat(data[0].DISCOUNT).toFixed(2)));
    $("#net-amount").text(": " + numberWithCommas(parseFloat(data[0].NET_TOTAL).toFixed(2)));
    $("#cash-sales").text(": " + numberWithCommas(parseFloat(data[0].CashSales).toFixed(2)));
    $("#credit-card-sales").text(": " + numberWithCommas(parseFloat(data[0].CreditCardSales).toFixed(2)));
    // $("#cash-in-hand").text(numberWithCommas(Math.round(data[0].CashInHand), 2));
    $("#cash-skimmed").text(": " + numberWithCommas(parseFloat(data[0].CashSkimmed).toFixed(2)));
    $("#cash-in-hand").text(": " + numberWithCommas(parseFloat(data[0].CashInHand).toFixed(2) - parseFloat(data[0].CashSkimmed).toFixed(2)));

    $("#location-name-sales-summary").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-sales-summary").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));
    $("#cashierSummary").text($("#user-detail-bold").text());
    
    $.print("#sales-summary-report");
}

$('#sales-detail').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectSalesDetail", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowSalesDetail
                }
            );
});

function ShowSalesDetail(data) {
    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);

    $("#DateTimeDetail").text(datetime);

    $("#lblAddress2").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    $("#cashierDetail").text($("#user-detail-bold").text());
   

    $('#sales-detail-data').empty(); // clear all skus  from invoice
    var grandTotal = 0;
    for (var i = 0, len = data.length; i < len; i++) {
        var row = $(' <tr><td>' + data[i].TableDefination_No + '</td><td class="text-right">' + numberWithCommas(parseFloat(data[i].NET_TOTAL).toFixed(2)) + '</td></tr>');
        $('#sales-detail-data').append(row);

        grandTotal += data[i].NET_TOTAL;
    }

    $("#location-name-sales-detail").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-sales-detail").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    $('#sales-detail-grand-total').text(numberWithCommas(parseFloat(grandTotal).toFixed(2)));

    $.print("#sales-detail-report");
}

$('#item-sales-detail').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectItemSalesDetail", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowItemSalesDetail
                }
            );
});

function ShowItemSalesDetail(data) {
    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);


    $("#DateTimeItemDetail").text(datetime);

    $("#lblAddress3").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    $("#cashierItemDetail").text($("#user-detail-bold").text());
    $('#item-sales-detail-data').empty(); // clear all skus  from invoice

    
    var netTotal = 0;
    var netQtyTotal = 0;

    var uniqueCategories = $.unique(data.map(function (d) { return d.CATEGORY_NAME; }));

    uniqueCategories = uniqueCategories.sort();
    uniqueCategories = unique(uniqueCategories);

    for (var j = 0; j < uniqueCategories.length; j++) {

        var grandTotal = 0;
        var grandQtyTotal = 0;

        var row1 = $('<tr style="text-align:center; background-color:black !important;"><td colspan="3" style="width:100%; color:white !important;">' + uniqueCategories[j] + '</td></tr>');
        $('#item-sales-detail-data').append(row1);

        var row2 = $('<tr><td style="width:60%;">Item Name</td><td  style="width:15%; text-align:center;" >Qty</td><td style="width:25%;" class="text-right">Amount</td></tr>');
        $('#item-sales-detail-data').append(row2);

        var line = $('<tr style="background-color:black !important; height:3px;"><td colspan="3"></td></tr>');
        $('#item-sales-detail-data').append(line);


        for (var i = 0, len = data.length; i < len; i++) {
            if (data[i].CATEGORY_NAME == uniqueCategories[j]) {

                var row3 = $('<tr style="padding-top:3px;padding-bottom:3px;"><td style="width:60%;">' + data[i].SKU_NAME + '</td><td  style="width:15%; text-align:center;" >' + data[i].QUANTITY + '</td><td style="width:25%;" class="text-right">' + numberWithCommas(parseFloat(data[i].NET_TOTAL).toFixed(2)) + '</td></tr>');
                $('#item-sales-detail-data').append(row3);

                grandQtyTotal += data[i].QUANTITY;
                grandTotal += data[i].NET_TOTAL;
                
                netQtyTotal += data[i].QUANTITY;
                netTotal += data[i].NET_TOTAL;
               
            }
        }

        var line2 = $('<tr style="background-color:black !important; height:3px;"><td colspan="3"></td></tr>');
        $('#item-sales-detail-data').append(line2);

        var row4 = $('<tr><td></td><td  style="text-align:center;">' + numberWithCommas(grandQtyTotal) + '</td><td class="text-right" >' + numberWithCommas(parseFloat(grandTotal).toFixed(2)) + '</td></tr>');
        $('#item-sales-detail-data').append(row4);

        var line3 = $('<tr style="height:7px;"><td colspan="3"></td></tr>');
        $('#item-sales-detail-data').append(line3);
    }
    $("#location-name-item-sales-detail").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-item-sales-detail").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    $('#item-sales-detail-grand-qty-total').text(numberWithCommas(netQtyTotal));

    $('#item-sales-detail-grand-total').text(numberWithCommas(parseFloat(netTotal).toFixed(2)));
   

    $.print("#item-sales-detail-report");
}