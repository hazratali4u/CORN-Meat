<%@ Page Title="CORN :: Item Deals" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmItemDeals.aspx.cs" Inherits="Forms_frmItemDeals" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript">


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtCategoryQty.ClientID%>').value;
            if (str == null) {
                alert('Must Enter Category Quantity');
                return false;
            }
            return true;
        }

        function pageLoad() {

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

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            </div>

            <div class="main-contents">
                <div class="container user_assignment">
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>Deal Item</label>
                            <dx:ASPxComboBox ID="drpDealtem" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDealtem_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                           
                        </div>
                        <div class="col-md-3">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>Category</label>
                            <dx:ASPxComboBox ID="drpCateogry" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="drpCateogry_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-2">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>Quantity</label>
                            <asp:TextBox ID="txtCategoryQty" runat="server" CssClass="form-control" Text="1"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender runat="server" ID="ftbeCatQty"
                            TargetControlID="txtCategoryQty" ValidChars="0987654321.">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>
                            </label>
                            <asp:CheckBox ID="chkIsChoice" runat="server" Text=" Is Choiced"
                                Checked="false" Font-Bold="true"></asp:CheckBox>

                        </div>
                        <div class="col-md-2">
                            <label>
                                <span class="fa fa-caret-right rgt_cart"></span>
                            </label>
                            <asp:CheckBox ID="chkIsActive" runat="server" Text=" Is Active"
                                Checked="true" Font-Bold="true"></asp:CheckBox>

                        </div>
                    </div>
                    <div class="row bottom">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span class="fa fa-caret-right rgt_cart"></span>Item Assignment
                                </div>
                                <div class="panel-body">
                                    <div class="module_contents">
                                        <asp:HiddenField runat="server" ID="hfCategoryId" Value="0" />
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstUnAssignItem" runat="server" Height="350px"
                                                CssClass="select"></asp:ListBox>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="navs navslist">
                                                <div class="nxt arrow">
                                                    <span>
                                                        <asp:LinkButton ID="btnAssignItem" OnClick="btnAssignItem_Click" CssClass="fa  fa-angle-right"
                                                            runat="server" /></span>
                                                </div>
                                                <div class="nxt arrow">
                                                    <span>
                                                        <asp:LinkButton ID="btnAssignAllItem" OnClick="btnAssignAllItem_Click"
                                                            CssClass="fa  fa-angle-double-right" runat="server" /></span>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Panel runat="server" ID="pnlList" Height="350px" ScrollBars="Vertical" BorderWidth="1px"
                                                BorderStyle="Solid" BorderColor="Silver">
                                                <asp:GridView ID="grdDeals" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                    AutoGenerateColumns="False" Width="100%" OnRowDeleting="grdDeals_RowDeleting"
                                                    EmptyDataText="No Item exist" OnRowDataBound="grdDeals_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="intDealID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="lngDealDetailID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ITEM_DEAL_ID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ITEM_DEAL_NAME" HeaderText="Deal Item" ReadOnly="true"></asp:BoundField>
                                                        <asp:BoundField DataField="CATEGORY_DEAL_ID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CATEGORY_ID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CATEGORY_NAME" HeaderText="Category" ReadOnly="true">

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QUANTITY" HeaderText="Qty" ReadOnly="true">
                                                            <ItemStyle CssClass="grdDetail" Width="5%"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" CssClass="grdHead" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_ID" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name" ReadOnly="true">
                                                            <ItemStyle Width="20%"></ItemStyle>

                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IS_CatChoiceBased" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Item Choiced">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIsItemChoice" runat="server" Checked="true" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Optional Item">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIsOptional" runat="server" Checked="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQty" runat="server" Width="30" Text='<%# Eval("SKU_QUANTITY") %>' />
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="ftbeQty"
                                                                    TargetControlID="txtQty" ValidChars="0987654321.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="X" ToolTip="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"/>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IS_ChoiceBased" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IS_Optional" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IS_Active" ReadOnly="true">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                         <asp:TemplateField HeaderText="Active">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIsItemActive" runat="server" Checked="true" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                         <asp:BoundField DataField="Status">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>

                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-offset-5 col-md-3 ">
                        <div class="btnlist pull-right">
                            <button type="button" id="btnAdd" runat="server" style="display:none;"/>
                            <cc1:ModalPopupExtender ID="mPopUpSection" runat="server" PopupControlID="pnlParameters"
                                TargetControlID="btnAdd" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                CancelControlID="btnCancel">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="pnlParameters" runat="server" Style="display: none;">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h2>Are you sure you want to InActive the deal!</h2>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-offset-4 col-md-4 ">
                                                            <div class="btnlist pull-right">
                                                                <asp:Button CssClass="btn btn-success" runat="server" ID="btnOk" OnClick="btnOk_Click" Text="Ok" />
                                                                 <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-8">

                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


