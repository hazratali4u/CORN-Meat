<div class="container" id="payment" style="max-width: 100%; display: none; z-index: 5000; top: 0%; left: 0%; position: absolute; background-color: #c6c6c6; padding: 10px; border: 1px solid #000; height: 103%;">
<div class="row" style="height:15px;">

</div>
<div class="row">
    <div class="col-md-6">
        <input type="text" id="txtItemCode" class="txtBox form-control" style="font-size: 20px; font-weight: bold;"
            runat="server" placeholder="Item Code" onkeypress="AddRowToGrid(event);"/>
    </div>
</div>
<div class="col-md-12 table-top item_pd_left">
    <div class="table-bar">
        <div class="emp-table scrolla" style="height: 180px;">
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
                <tbody id="tble-invoice-products">
                </tbody>
            </table>
        </div>
    </div>
</div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-4">
                <div class="col-md-12" style="padding-bottom: 15px;">
                    <div class="text">
                        Payment Mode
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px; color: #FFF; background-color: #919399;"
                            onclick="PayType(this)" id="cash" value="0">
                            Cash
                        </button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px; color: #FFF; background-color: #919399;"
                            onclick="PayType(this)" id="credit" value="1">
                            Credit Card
                        </button>
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px; color: #FFF; background-color: #919399;"
                            onclick="PayType(this)" id="creditSale" value="2">
                            Credit Sale
                        </button>
                    </div>
                </div>
                <div class="col-md-12">
                    <label id="subTotal" style="font: bold 32px sans-serif; display: none; color: #fff; background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>

                    <label id="SalesTax" style="font: bold 18px sans-serif; display: none; color: #fff; background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Gross Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="GrandTotal" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Discount Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblDiscountTotal" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label runat="server" id="lblGSTOrService" style="font: bold 17px sans-serif;">
                            GST Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblGSTTotal" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Payment Due
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblPaymentDue" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                </div>
            </div>
            <div class="col-md-8" style="border-left: 1px solid #dadada;" id="divPaymentTab">
                <div class="col-md-12">
                    <div class="text" style="text-align:left;">
                        Discount Type
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px; color: #FFF; background-color: #919399;"
                            onclick="DiscType(this);" disabled="disabled" id="percentage"
                            value="0">
                            %age
                        </button>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px; color: #FFF; background-color: #919399;"
                            onclick="DiscType(this)" disabled="disabled" id="value"
                            value="1">
                            Value
                        </button>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 7px;">
                    <div class="col-md-4">
                        <label>
                            Discount
                        </label>
                        <input type="text" id="txtDiscount" runat="server" class="form-control" style="font-size: 20px; font-weight: bold;"
                            disabled="disabled" onkeyup="CalculateBalance();" onkeypress="return onlyDotsAndNumbers(this,event);" value="0" />
                    </div>
                    <div class="col-md-8">
                        <label id="lblApprovedBy">
                            Authorized By
                        </label>
                        <input type="text" id="txtApprovedBy" runat="server" class="form-control" disabled="disabled" />
                        <input type="password" id="txtApprovedPassword" class="form-control" style="display: none" />
                    </div>
                </div>

                <div class="col-md-12" style="margin-top: 7px; display: none;" runat="server" id="dvServiceCharges">
                    <div class="col-md-12" style="margin-top: 7px;">
                        <label>
                            Service Charges(%)
                        </label>
                    </div>
                    <div class="col-md-12">
                        <input type="text" id="txtService" runat="server" class="form-control" style="font-size: 20px; font-weight: bold;"
                            disabled="disabled" />
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 7px;" runat="server">
                    <div class="col-md-12">
                        <label>
                            Disc Type
                        </label>
                        <select id="ddlDiscountType" onchange="loadUsers(this);" class="form-control">
                              <option value="-1">--Select--</option>
                              <option value="0">General</option>
                            <option value="1">Employee Discount</option>
                            <option value="2">Loyalty Card</option>
                        </select>
                    </div>
                    <div class="col-md-12" style="display: none;" id="dvDiscountUser">
                        <fieldset>
                            <div class="col-md-9">
                                <label>
                                    Employee Name
                                </label>
                                <select id="ddlDiscountUser" class="form-control" onchange="loadLimit(this);"></select>
                            </div>
                            <div class="col-md-3">
                                <label>Limit </label>
                                <label id="lblLimit" style="font-size: 14px"></label>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-12" style="display: none;" id="dvLoyaltyCard">

                        <fieldset>
                            <div class="row">
                                <div class="col-md-6" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Card No</label>
                                    <input type="text" id="txtLoyaltyCard" class="txtBox form-control" style="font-size: 20px; font-weight: bold;"
                                        onblur="LoadLoyaltyCardDetail();" />
                                </div>
                                <div class="col-md-6" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Customer Name</label>
                                    <input type="text" id="txtLoyaltyCustomer" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowPrivilege" style="display: none;">
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>No Of Visits</label>
                                    <input type="text" id="txtNoOfVisits" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Purchased</label>
                                    <input type="text" id="txtTotalPurchased" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Discount</label>
                                    <input type="text" id="txtTotalLoyaltyDiscount" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Purchased Qty</label>
                                    <input type="text" id="txtLoyaltyQuantity" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowDirectorCard" style="display: none;">
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Allowed Limit</label>
                                    <input type="text" id="txtAllowedLimit" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Availed Discount</label>
                                    <input type="text" id="txtDiscountAvail" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Balance Discount</label>
                                    <input type="text" id="txtDiscountBalance" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowRewardCard" style="display: none;">
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Points</label>
                                    <input type="text" id="txtTotalPoints" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Red. Points</label>
                                    <input type="text" id="txtRedeemedPoints" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Bal. Points</label>
                                    <input type="text" id="txtBalancePoints" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Avail. Disc.</label>
                                    <input type="text" id="txtAvailableDiscount" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Avail. Cash</label>
                                    <input type="text" id="txtAvailableCash" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                        </fieldset>


                    </div>
                    <div class="col-md-12" style="display: none;" id="dvAuthorityUser">
                        <label>
                            Authority Person
                        </label>
                        <select id="ddlDiscountUser2" class="form-control" onchange="loadPassword(this);"></select>
                        <input type="hidden" value="0" id="hfManagerPassword" />
                    </div>
                </div>

                <div class="col-md-12" style="margin-top: 5px; height: 15px">
                </div>
            </div>
            <div class="col-md-12" style="border-top: 1px solid #dadada; border-bottom: 1px solid #dadada; padding: 5px 0 5px 0; margin-top: 10px;">
                <div class="col-md-3" style="font-size: 15px; margin-top: 7px; font-weight: bold;">
                    Payment Received
                </div>
                <div class="col-md-4 payment">
                    <input type="text" id="txtCashRecieved" class="txtBox form-control" style="font-size: 20px; font-weight: bold;"
                        runat="server" onkeyup="CalculateBalance();" onkeypress="return onlyDotsAndNumbers(this,event);" />
                </div>
                <div class="col-md-3" style="font-size: 15px; margin-top: 7px; font-weight: bold;">
                    Customer Balance
                </div>
                <div class="col-md-2">
                    <label runat="server" id="lblBalance" style="font: bold 32px sans-serif;">
                        0.00
                    </label>
                </div>
            </div>
            <div class="col-md-12" style="height:15px;">

            </div>
            <div class="col-md-12">
                <div class="col-md-5">
                </div>
                <div class="col-md-1"></div>
                <div class="col-md-6">
                    <div class="col-md-3">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -143px; font-size: 15px; color: #FFF; background-color: #3c8d75;"
                            id="btnSave" onclick="SaveInvoice();">
                            Save
                        </button>
                    </div>
                    <div class="col-md-3" style="margin-left: 10px;">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -133px; font-size: 15px; color: #FFF; background-color: #f94d72;"
                            id="btnCancel" onclick="ClearOnCancel();">
                            Cancel
                        </button>
                    </div>
                    <div class="col-md-2" style="margin-left: 10px;visibility:hidden;">
                        <button class="btn btn-danger" type="button" style="min-width: 100px; margin-left: -90px;"
                            id="btnVoid">
                            Void
                        </button>
                    </div>
                    <div class="col-md-5" style="margin-top: 15px;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<div id="reports-buttons" style="width: 420px; height: 200px; padding: 10px; display: none;">
    <div class="col-md-4 btn btn-default" style="height: 100%; display: none;" id="sales-summary">
        <a data-toggle="modal" data-target="#myModal" style="position: relative; top: 35%; transform: translateY(-50%);">Sales Summary </a>
    </div>
    <div class="col-md-4 btn btn-default" style="height: 100%; display: none;" id="sales-detail">
        <a data-toggle="modal" data-target="#myModal" style="position: relative; top: 35%; transform: translateY(-50%);">Sales Details </a>
    </div>
    <div class="col-md-4 btn btn-default" style="height: 100%; display: none;" id="item-sales-detail">
        <a data-toggle="modal" data-target="#myModal" style="position: relative; top: 35%; transform: translateY(-50%);">Item Wise Sales</a>
    </div>
</div>
<div style="display: none;">
    <table class="table table-striped table-bordered table-hover table-condensed cf">
        <tbody id="tble-discount-limit"></tbody>
        <tbody id="tble-discount-user"></tbody>
    </table>
</div>
