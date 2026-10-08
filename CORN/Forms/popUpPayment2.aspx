<div class="container" id="payment2" style="max-width: 1000px; display: none; z-index: 5000; top: 5%; left: 12%; position: absolute; background-color: #c6c6c6; padding: 10px; border: 1px solid #000; min-height: 580px;">
    <div class="row">
        <div class="col-md-12" style="margin-top:10px;">
            <div class="col-md-4">
                
                <div class="col-md-12">
                    <label id="subTotal2" style="font: bold 32px sans-serif; display: none; color: #fff;
                        background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>
                    
                    <label id="SalesTax2" style="font: bold 18px sans-serif; display: none; color: #fff;
                        background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Gross Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="GrandTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                   
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Discount Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblDiscountTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                     <div class="col-md-8" style="margin-top: 7px;">
                     
                             <label runat="server" id="lblGSTOrService2" style="font: bold 17px sans-serif;">
                             GST Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblGSTTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Payment Due</label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblPaymentDue2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                </div>
            </div>
            <div class="col-md-8" style="border-left: 1px solid #dadada;" id="divPaymentTab2">
                <div class="col-md-12" id="dvDiscount2">
                    <div class="text">
                        Discount Type</div>
                    <div class="col-md-5">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px;
                            color: #FFF; background-color: #919399;" onclick="DiscType(this);" id="percentage2" 
                            value="0">
                            %age</button></div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-5">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px;
                            color: #FFF; background-color: #919399;" onclick="DiscType(this)" id="value2" 
                            value="1">
                            Value</button></div>
                </div>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="col-md-4">
                        <label>
                            Discount</label>
                        <input type="text" id="txtDiscount2" runat="server" class="form-control" style="font-size: 20px;
                            font-weight: bold;"  onkeyup="CalculateBalance2();" value="0" onkeypress="return onlyDotsAndNumbers(this,event);" disabled="disabled"/>
                    </div>
                    <div class="col-md-8">
                        <label id="lblApprovedBy2">
                            Authorized By
                        </label>
                        <input type="text" id="txtApprovedBy2" runat="server" class="form-control"  disabled="disabled"/>
                        <input type="password" id="txtApprovedPassword2" class="form-control" style="display: none" />
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 5px; display:none;" runat="server"  id="dvServiceCharges2">
                    <div class="col-md-4">
                        <label>
                            Service Charges(%)</label>
                    </div>
                    <div class="col-md-8">
                       
                        <input type="text" id="txtService2" runat="server" class="form-control" style="font-size: 20px;
                            font-weight: bold;"  onkeyup="CalculateBalance2();" onkeypress="return onlyDotsAndNumbers(this,event);" />
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 7px;" runat="server">
                    <div class="col-md-12">
                        <label>
                            Disc Type
                        </label>
                        <select id="ddlDiscountType2" onchange="loadUsers2(this);" class="form-control">
                            <option value="-1">--Select--</option>
                            <option value="0">General</option>
                            <option value="1">Employee Discount</option>
                            <option value="2">Loyalty Card</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-12" style="display: none;" id="dvDiscountUser2">
                        <fieldset>
                            <div class="col-md-9">
                                <label>
                                    Employee Name
                                </label>
                                <select id="ddlDiscountUser4" class="form-control" onchange="loadLimit2(this);"></select>
                            </div>
                            <div class="col-md-3">
                                <label>Limit </label>
                                <label id="lblLimit2" style="font-size: 14px"></label>
                            </div>
                        </fieldset>
                    </div>
                <div class="col-md-12" style="display: none;" id="dvLoyaltyCard2">
                        <fieldset>
                            <div class="row">
                                <div class="col-md-6" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Card No</label>
                                    <input type="text" id="txtLoyaltyCard2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;"
                                        onblur="LoadLoyaltyCardDetail2();" />
                                </div>
                                <div class="col-md-6" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Customer Name</label>
                                    <input type="text" id="txtLoyaltyCustomer2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowPrivilege2" style="display: none;">
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>No Of Visits</label>
                                    <input type="text" id="txtNoOfVisits2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Purchased</label>
                                    <input type="text" id="txtTotalPurchased2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Discount</label>
                                    <input type="text" id="txtTotalLoyaltyDiscount2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Purchased Qty</label>
                                    <input type="text" id="txtLoyaltyQuantity2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowDirectorCard2" style="display: none;">
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Allowed Limit</label>
                                    <input type="text" id="txtAllowedLimit2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Availed Discount</label>
                                    <input type="text" id="txtDiscountAvail2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-3" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Balance Discount</label>
                                    <input type="text" id="txtDiscountBalance2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row" id="rowRewardCard2" style="display: none;">
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Total Points</label>
                                    <input type="text" id="txtTotalPoints2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Red. Points</label>
                                    <input type="text" id="txtRedeemedPoints2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>

                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Bal. Points</label>
                                    <input type="text" id="txtBalancePoints2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Avail. Disc.</label>
                                    <input type="text" id="txtAvailableDiscount2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                                <div class="col-md-2" style="padding-left: 5px; padding-right: 5px;">
                                    <label>Avail. Cash</label>
                                    <input type="text" id="txtAvailableCash2" class="txtBox form-control" style="font-size: 20px; font-weight: bold;" readonly="readonly" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                <div class="col-md-12" style="display: none;" id="dvAuthorityUser2">
                        <label>
                            Authority Person
                        </label>
                        <select id="ddlDiscountUser3" class="form-control" onchange="loadPassword(this);"></select>
                        <input type="hidden" value="0" id="hfManagerPassword2" />
                    </div>
                <div class="col-md-12" style="margin-top: 5px; height: 15px">
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                </div>
                <div class="col-md-6">
                    <div class="col-md-5 btn-mar">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -143px;
                            font-size: 15px; color: #FFF; background-color: #3c8d75;" id="btnSave2" onclick="UpdateOrder();">
                            Save</button>
                    </div>
                    <div class="col-md-5 btn-mar" style="margin-left: 10px;">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -133px;
                            font-size: 15px; color: #FFF; background-color: #f94d72;" id="btnCancel2" onclick="ClearOnCancel2();">
                            Cancel</button>
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
<div style="display: none;">
    <table class="table table-striped table-bordered table-hover table-condensed cf">
        <tbody id="tble-discount-limit2"></tbody>
        <tbody id="tble-discount-user2"></tbody>
    </table>
</div>