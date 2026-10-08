<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKUData.aspx.cs" Inherits="Forms_frmSKUData" Title="CORN :: Item Information" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/1.8.3jquery.min.js"></script>
    <script src="../js/angular.min.js"></script>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
        function Message(msg) {
            alert(msg);
        }
        function ValidateForm() {

            str = document.getElementById('<%=txtskuname.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Item name is required')
                return false;
            }

            str = document.getElementById('<%=txtskucode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Item Code is required')
                return false;
            }

            str = document.getElementById('<%=drpStockUnit.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select Stock unit')
                return false;
            }
            str = document.getElementById('<%=drpSaleUnit.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select Sale unit')
                return false;
            }
            str = document.getElementById('<%=drpPurchaseUnit.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select Purchase unit')
                return false;
            }

            return true;
        }

        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function ChkSerialization(chk) {
            if (chk == document.getElementById('<%=chkIsSerialized.ClientID%>')) {
                if (document.getElementById('<%=chkIsSerialized.ClientID%>').checked == true) {
                    document.getElementById('<%=txtSerialCode.ClientID%>').disabled = false;

                }
                else {
                    document.getElementById('<%=txtSerialCode.ClientID%>').disabled = true;
                    document.getElementById('<%=txtSerialCode.ClientID%>').value = "";
                }
            }
            if (chk == document.getElementById('<%=chkIsFEDItem.ClientID%>')) {
                if (document.getElementById('<%=chkIsFEDItem.ClientID%>').checked == true) {
                    document.getElementById('<%=txtFEDPercentage.ClientID%>').disabled = false;

                }
                else {
                    document.getElementById('<%=txtFEDPercentage.ClientID%>').disabled = true;
                    document.getElementById('<%=txtFEDPercentage.ClientID%>').value = "";
                }
            }
            if (chk == document.getElementById('<%=chkIsWHTItem.ClientID%>')) {
                if (document.getElementById('<%=chkIsWHTItem.ClientID%>').checked == true) {
                    document.getElementById('<%=txtWHTPercentage.ClientID%>').disabled = false;

                }
                else {
                    document.getElementById('<%=txtWHTPercentage.ClientID%>').disabled = true;
                    document.getElementById('<%=txtWHTPercentage.ClientID%>').value = "";
                }
            }
        }
    </script>

    <div class="main-contents">
        <div class="container employee-infomation">
            <div class="row top">
                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnsearch">
                    <div class="col-md-4">
                        <div class="search">
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"
                                TabIndex="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-left: -60px;">
                        <asp:LinkButton ID="btnsearch" OnClick="btnFilter_Click" runat="server" Text="Search"
                            CssClass="btn btn-success"><i class="fa fa-search"  style="font-size:20px;"></i></asp:LinkButton>
                    </div>
                    <asp:LinkButton ID="btndummy" runat="server" UseSubmitBehavior="false" />
                </asp:Panel>
                <div class="col-md-offset-5 col-md-3 ">
                    <div class="btnlist pull-right">


                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfStockSale" Value="0" runat="server" />
                                <asp:HiddenField ID="hfStockPurchase" Value="0" runat="server" />
                                <asp:HiddenField ID="hfSalePurchase" Value="0" runat="server" />
                                <asp:HiddenField ID="hfSaleStock" Value="0" runat="server" />
                                <asp:HiddenField ID="hfPurchaseSale" Value="0" runat="server" />
                                <asp:HiddenField ID="hfPurchaseStock" Value="0" runat="server" />
                                <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                    OnClick="btnAdd_Click">
                               <span class="fa fa-plus-circle"></span>Add
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="hbutton"></asp:LinkButton>
                                <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server"
                                    OnClientClick="javasacript:return confirm('Are your sure you want to perform this action?'); return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                <!-- POP UP MODEL-->
                                <ajaxToolkit:ModalPopupExtender ID="mPopUpLocation" runat="server" PopupControlID="pnlParameters"
                                    TargetControlID="hbutton" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                    CancelControlID="btnClose">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlParameters" DefaultButton="btnSave" runat="server" Style="display: none; width: 100%" ScrollBars="Auto">
                                    <div class="modal-dialog2" style="width: 70%">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" class="close" runat="server" onserverclick="btnClose_Click">
                                                    <span>&times;</span><span class="sr-only">Close</span></button>
                                                <h1 class="modal-title" id="myModalLabel">
                                                    <span></span>Add New Item Information</h1>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                                                            Height="400px" Width="100%">
                                                            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" Height="400px">
                                                                <HeaderTemplate>
                                                                    General
                                                                </HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <div class="main-contents" style="height: 100%;">
                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="modal-body">
                                                                                    <div class="row">
                                                                                        <div class="col-md-4">
                                                                                            <asp:HiddenField ID="hfSkuId" runat="server" />
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Category Type</label>
                                                                                            <dx:ASPxComboBox ID="ddlType" runat="server" AutoPostBack="true"
                                                                                                CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                                            </dx:ASPxComboBox>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Item Section</label>
                                                                                            <dx:ASPxComboBox ID="ddlSection" runat="server" CssClass="form-control">
                                                                                            </dx:ASPxComboBox>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Item Category</label>
                                                                                            <dx:ASPxComboBox ID="ddlCategory" runat="server"
                                                                                                CssClass="form-control">
                                                                                            </dx:ASPxComboBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Item Code</label>
                                                                                            <asp:TextBox ID="txtskucode" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Item Name</label>
                                                                                            <asp:TextBox ID="txtskuname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Unit Of Measure</label>
                                                                                            <dx:ASPxComboBox ID="ddlUOM" runat="server" CssClass="form-control">
                                                                                            </dx:ASPxComboBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Default Qty</label>
                                                                                            <asp:TextBox ID="txtDefaultQty" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtDefaultQty"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Selling Consumption</label>
                                                                                            <asp:TextBox ID="txtRatioFactor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtRatioFactor" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtRatioFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Weight Consumption</label>
                                                                                            <asp:TextBox ID="txtWeightConsumption" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtWeightConsumption"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-4" style="display:none;">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Print Description</label>
                                                                                            <asp:TextBox ID="txtDescription" runat="server" Rows="1" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-8">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Description</label>
                                                                                            <asp:TextBox ID="txtDescription2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="display: none;">
                                                                                        <div class="col-md-6">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Unit In Case</label>
                                                                                            <asp:TextBox ID="txtunitincase" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-4">
                                                                                            <asp:Label Visible="false" runat="server" ID="lblMin"><span class="fa fa-caret-right rgt_cart"></span>Min Level</asp:Label>
                                                                                            <asp:TextBox Visible="false" ID="txtMinLevel" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtMinLevel" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtMinLevel"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <asp:Label Visible="false" runat="server" ID="lblMax"><span class="fa fa-caret-right rgt_cart"></span>Max Level</asp:Label>
                                                                                            <asp:TextBox Visible="false" ID="txtMaxLevel" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtMaxLevel"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                        <div class="col-md-4">
                                                                                            <asp:Label Visible="false" runat="server" ID="lblReorder"><span class="fa fa-caret-right rgt_cart"></span>Reorder Level</asp:Label>
                                                                                            <asp:TextBox Visible="false" ID="txtReorderLevel" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeReorderLevel" runat="server"
                                                                                                FilterType="Custom" ValidChars="0123456789." TargetControlID="txtReorderLevel"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-6" runat="server" visible="false" id="trItemType">
                                                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Item Type</label>

                                                                                            <dx:ASPxComboBox ID="drpProductType" runat="server"
                                                                                                CssClass="form-control" SelectedIndex="0">
                                                                                                <Items>
                                                                                                    <dx:ListEditItem Text="Quantity" Value="1" Selected="True"></dx:ListEditItem>
                                                                                                    <dx:ListEditItem Text="Value" Value="2"></dx:ListEditItem>
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsInventory" runat="server" Text=" Is Inventory Item" Checked="true"
                                                                                                Font-Bold="true"></asp:CheckBox>

                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsOverSaleAllowed" runat="server" Text="OverSale Allowed"
                                                                                                Checked="false" Font-Bold="true" />
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsHasModifier" runat="server" Text=" Is has Modifier"
                                                                                                Checked="false" Font-Bold="true" Visible="false"/>
                                                                                            <asp:CheckBox ID="cbIs_WastageItem" runat="server" Text="Is Wastage Item"
                                                                                                Checked="false" Font-Bold="true"/>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-3" style="display: none;">
                                                                                            <asp:CheckBox ID="chkIsDesc" runat="server" Checked="false" Text=" Print Desc" Font-Bold="true"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsRecipe" runat="server" Text=" Is Recipe" Checked="true"
                                                                                                Font-Bold="true" Visible="false"/>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsDeal" runat="server" Text=" Is Deal Item" Checked="false" Font-Bold="true" Visible="false"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:CheckBox ID="chkIsModifier" runat="server" Text=" Is Modifier" Checked="false" Font-Bold="true" Visible="false"></asp:CheckBox>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>
                                                            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" Height="400px" Visible="false">
                                                                <HeaderTemplate>
                                                                    Detail
                                                                </HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <div class="main-contents" style="overflow-y: scroll; height: 380px;">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="modal-body" ng-app="App" ng-controller="CtrlValidation">
                                                                                    <fieldset style="border-width: thin; border-color: #eae5e5">
                                                                                        <legend>Stock</legend>
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Unit</label>
                                                                                                <dx:ASPxComboBox ID="drpStockUnit" AutoPostBack="true" OnSelectedIndexChanged="drpStockUnit_SelectedIndexChanged" runat="server" CssClass="form-control" ng-model="StockUnit">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Sale Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpStockToSaleOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Sale Factor</label>
                                                                                                <asp:TextBox ID="txtStockToSaleFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtStockToSaleFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Purchase Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpStockToPurchaseOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Purchase Factor</label>
                                                                                                <asp:TextBox ID="txtStockToPurchaseFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtStockToPurchaseFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                    </fieldset>
                                                                                    <fieldset style="border-width: thin; border-color: #eae5e5; margin-top: 5px">
                                                                                        <legend>Sale</legend>
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Unit</label>
                                                                                                <dx:ASPxComboBox ID="drpSaleUnit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSaleUnit_SelectedIndexChanged" ng-model="SaleUnit" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Purchase Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpSaletoPurchaseOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Purch. Factor</label>
                                                                                                <asp:TextBox ID="txtSaleToPurchaseFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtSaleToPurchaseFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Stock Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpSaleToStockOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Stock Factor</label>
                                                                                                <asp:TextBox ID="txtSaleToStockFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtSaleToStockFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                    </fieldset>
                                                                                    <fieldset style="border-width: thin; border-color: #eae5e5; margin-top: 5px">
                                                                                        <legend>Purchase</legend>
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Unit</label>
                                                                                                <dx:ASPxComboBox ID="drpPurchaseUnit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPurchaseUnit_SelectedIndexChanged" ng-model="PurchaseUnit" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Sale Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpPurchaseToSaleOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Sale Factor</label>
                                                                                                <asp:TextBox ID="txtPurchaseToSaleFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtPurchaseToSaleFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Stock Opr</label>
                                                                                                <dx:ASPxComboBox ID="drpPurchaseToStockOperator" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Stock Factor</label>
                                                                                                <asp:TextBox ID="txtPurchaseToStockFactor" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtPurchaseToStockFactor"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                    </fieldset>
                                                                                    <fieldset style="border-width: thin; border-color: #eae5e5; margin-top: 5px;" runat="server" id="other" visible="false">
                                                                                        <legend>Other</legend>
                                                                                        <div class="row">
                                                                                            <div class="col-md-4" style="text-align: right">
                                                                                                <label style="float: left"><span class="fa fa-caret-right rgt_cart"></span>Serial Code</label>
                                                                                                <asp:CheckBox ID="chkIsSerialized" onclick="ChkSerialization(this)" Text="Serialized" runat="server" Checked="false" Font-Bold="true" />
                                                                                                <asp:TextBox ID="txtSerialCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            </div>

                                                                                            <div class="col-md-4" style="text-align: right">
                                                                                                <label style="float: left"><span class="fa fa-caret-right rgt_cart"></span>FED %age</label>
                                                                                                <asp:CheckBox ID="chkIsFEDItem" onclick="ChkSerialization(this)" runat="server" Text="FED item" Checked="false" Font-Bold="true" />
                                                                                                <asp:TextBox ID="txtFEDPercentage" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtFEDPercentage"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-4" style="text-align: right">
                                                                                                <label style="float: left"><span class="fa fa-caret-right rgt_cart"></span>WHT %age</label>
                                                                                                <asp:CheckBox ID="chkIsWHTItem" runat="server" onclick="ChkSerialization(this)" Text="WHT Item" Checked="false" Font-Bold="true" />
                                                                                                <asp:TextBox ID="txtWHTPercentage" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtWHTPercentage"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>ERP Code</label>
                                                                                                <asp:TextBox ID="txtERPCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Unit Life Code</label>
                                                                                                <dx:ASPxComboBox ID="drpUnitLifeCode" runat="server" CssClass="form-control">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Full Age</label>
                                                                                                <asp:TextBox ID="txtAgeInDays" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAgeInDays"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Shelf Age</label>
                                                                                                <asp:TextBox ID="txtShelfAgeInDays" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                                                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtShelfAgeInDays"></ajaxToolkit:FilteredTextBoxExtender>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Status</label>
                                                                                                <dx:ASPxComboBox ID="drpStatus" runat="server" CssClass="form-control">
                                                                                                    <Items>
                                                                                                        <dx:ListEditItem Text="Active" Value="Active" Selected="True"></dx:ListEditItem>
                                                                                                        <dx:ListEditItem Text="Inactive" Value="In - Active"></dx:ListEditItem>
                                                                                                        <dx:ListEditItem Text="Out of Service" Value="Out of Service"></dx:ListEditItem>
                                                                                                        <dx:ListEditItem Text="Sold" Value="Sold"></dx:ListEditItem>
                                                                                                        <dx:ListEditItem Text="Transferred" Value="Transferred"></dx:ListEditItem>
                                                                                                    </Items>
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Hazardous</label>
                                                                                                <asp:CheckBox ID="chkIsHazardous" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Batch Item</label>
                                                                                                <asp:CheckBox ID="chkIsBatchItem" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Expiry Allowed</label>
                                                                                                <asp:CheckBox ID="chkIsExpiryAllowed" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Warehouse Item</label>
                                                                                                <asp:CheckBox ID="chkIsWarehouseItem" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Market Item</label>
                                                                                                <asp:CheckBox ID="chkIsMarketItem" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Replaceable</label>
                                                                                                <asp:CheckBox ID="chkIsReplaceable" runat="server" Checked="false" Font-Bold="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </fieldset>
                                                                                </div>

                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </ajaxToolkit:TabPanel>

                                                        </ajaxToolkit:TabContainer>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-11" style="text-align: right; margin-top: 20px; margin-right: 40px">
                                                            <asp:HiddenField ID="hfStatus" runat="server" Value="Active" />
                                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif"></asp:ImageButton>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="grdSKUData" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    HorizontalAlign="Center"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grdSKUData_PageIndexChanging" OnRowEditing="grdSKUData_RowEditing"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Record exist">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderStyle />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Brand_Id" HeaderText="Brand_Id" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SECTION_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Category_Id" HeaderText="Category_Id" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true">
                                            <ItemStyle Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_CODE" HeaderText="Bar Code" ReadOnly="true">
                                            <ItemStyle Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="Name" ReadOnly="true">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RATIO_FACTOR" HeaderText="Selling Consumption" ReadOnly="true">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISEXEMPTED" ReadOnly="true" HeaderText="Is Inventory"></asp:BoundField>
                                        <asp:BoundField DataField="IsOverSaleAllowed" HeaderText="OverSale Allowed" ReadOnly="true">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Default_Qty" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISACTIVE" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PACKSIZE" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="strDescription" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Weight_Consumption" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Is_WastageItem" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel"/>
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil" CommandName="Edit" ToolTip="Edit">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="10" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
