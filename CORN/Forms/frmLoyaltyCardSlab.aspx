<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Forms/PageMaster.master"
    CodeFile="frmLoyaltyCardSlab.aspx.cs" Inherits="Forms_frmLoyaltyCardSlab" Title="CORN :: Reward Slabs" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function TypeChange() {

            var list = document.getElementById("ctl00_ctl00_mainCopy_cphPage_rblType"); //Client ID of the radiolist
            var inputs = list.getElementsByTagName("input");
            var selected;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    selected = inputs[i];
                    break;
                }
            }
            document.getElementById('ctl00_ctl00_mainCopy_cphPage_hfType').value = selected.value;
            if (selected.value == '2') {
                document.getElementById('divItem1').style.display = "none";
                document.getElementById('divItem2').style.display = "none";
                document.getElementById('divPoint').style.display = "block";
            }
            else {
                document.getElementById('divItem1').style.display = "block";
                document.getElementById('divItem2').style.display = "block";
                document.getElementById('divPoint').style.display = "none";
            }
        }

        function pageLoad() {

            if (document.getElementById('ctl00_ctl00_mainCopy_cphPage_hfType').value == '2') {
                document.getElementById('divItem1').style.display = "none";
                document.getElementById('divItem2').style.display = "none";
                document.getElementById('divPoint').style.display = "block";
            }
            else {
                document.getElementById('divItem1').style.display = "block";
                document.getElementById('divItem2').style.display = "block";
                document.getElementById('divPoint').style.display = "none";
            }

            TypeChange();

            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

        function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            var chkBoxCount;
            var i;
            if (chkBox.checked == true) {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            } else {
                chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (i = 0; i < chkBoxCount.length; i++) {

                    chkBoxCount[i].checked = false;
                }
            }
        }
        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
            var chkBoxList = document.getElementById('<%= ChbDistributorList.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }

    </script>
    <div class="main-contents">
        <div class="container employee-infomation">

            <div class="row top">
                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnsearch">
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" ID="hfSlabID" />
                                <asp:LinkButton ID="btndummy2" runat="server" UseSubmitBehavior="false" />
                                <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                    OnClick="btnAdd_Click">
                               <span class="fa fa-plus-circle"></span>Add
                                </asp:LinkButton>
                                <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server"
                                    OnClientClick="javascript:return confirm('Are you sure you want to perform this action?');return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                <!-- POP UP MODEL-->
                                <cc1:ModalPopupExtender ID="mPopUpLocation" runat="server" PopupControlID="pnlParameters"
                                    TargetControlID="btndummy2" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                    CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlParameters" runat="server" Style="display: none; width: 100%;">
                                    <div class="modal-dialog" style="width: 70%;">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" runat="server" class="close">
                                                    <span>&times;</span><span class="sr-only">Close</span></button>
                                                <h1 class="modal-title" id="myModalLabel">Add New Reward Slabs</h1>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-10">
                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server"></asp:CheckBox>
                                                        <label>All Locations</label>
                                                        <asp:Panel ID="Panel1" runat="server" Width="255px" Height="150px" ScrollBars="Vertical"
                                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                                                            <asp:CheckBoxList ID="ChbDistributorList" Font-Size="Small"
                                                                CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                                onclick="UnCheckSelectAll()"
                                                                runat="server" Width="236px">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-3" style="vertical-align: middle;">
                                                        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            onchange="TypeChange();" Enabled="false">
                                                            <asp:ListItem Value="1" Text="Item Base"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Point Base" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:HiddenField runat="server" ID="hfType" />
                                                    </div>
                                                </div>
                                                <div class="row" style="height: 20px;">
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="row" style="height: 20px;">
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="row" id="divItem1">
                                                    <div class="col-md-3">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Item</label>
                                                        <dx:ASPxComboBox ID="ddlItem" runat="server" CssClass="form-control">
                                                        </dx:ASPxComboBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="divItem2">
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points From</label>
                                                        <asp:TextBox ID="txtPointsFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbePointsFrom"
                                                            TargetControlID="txtPointsFrom" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points To</label>
                                                        <asp:TextBox ID="txtPointsTo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbePointsTo"
                                                            TargetControlID="txtPointsTo" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Cash</label>
                                                        <asp:TextBox ID="txtCash" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbeCash"
                                                            TargetControlID="txtCash" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Discount</label>
                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbeDiscount"
                                                            TargetControlID="txtDiscount" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Free Unit</label>
                                                        <asp:TextBox ID="txtFreeUnit" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbeFreeUnit"
                                                            TargetControlID="txtFreeUnit" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points Deduction</label>
                                                        <asp:TextBox ID="txtPointDeduction" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftbetPointDeduction"
                                                            TargetControlID="txtPointDeduction" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                                <div class="row" id="divPoint" style="display: none;">
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points From</label>
                                                        <asp:TextBox ID="txtPointsFrom2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6"
                                                            TargetControlID="txtPointsFrom2" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points To</label>
                                                        <asp:TextBox ID="txtPointsTo2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7"
                                                            TargetControlID="txtPointsTo2" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Cash</label>
                                                        <asp:TextBox ID="txtCash2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender8"
                                                            TargetControlID="txtCash2" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Discount</label>
                                                        <asp:TextBox ID="txtDiscount2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9"
                                                            TargetControlID="txtDiscount2" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>
                                                            <span class="fa fa-caret-right rgt_cart"></span>Points Deduction</label>
                                                        <asp:TextBox ID="txtPointDeduction2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender11"
                                                            TargetControlID="txtPointDeduction2" ValidChars="0987654321."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" onblur="Change(this, event)"
                                                    OnClick="btnSave_Click" onfocus="Change(this, event)" Text="Save" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" onblur="Change(this, event)"
                                                    OnClick="btnCancel_Click" onfocus="Change(this, event)" Text="Cancel" />
                                                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink1"
                                                    runat="server">dummy</a>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanelDetail" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLoyaltySlab" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    HorizontalAlign="Center" OnRowEditing="gvLoyaltySlab_RowEditing" AutoGenerateColumns="False"
                                    EmptyDataText="No Record exist"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="lngLoyaltyCardSalabID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="intLoyaltyCardSalabTypeID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="intItemID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="decPointsFrom" HeaderText="Points From" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="decPointsTo" HeaderText="Points To" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="decCash" HeaderText="Cash" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="decDiscount" HeaderText="Discount" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="decFreeUnit" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="decPointsDeduction" HeaderText="Points Deduction" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />
                                        </asp:BoundField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" class="fa fa-pencil">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head"></HeaderStyle>

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
