<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseEntry.aspx.cs" Inherits="Forms_frmPurchaseEntry" Title="CORN :: Stock Register" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">


    <script type="text/javascript" src="../AjaxLibrary/ValidateDotsAndNumbers.js"></script>
    <script language="JavaScript" type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function CalculateNetAmount() {

            var GrossAmount = document.getElementById('<%=txtTotalAmount.ClientID%>').value;
            var Discount = document.getElementById('<%=txtDiscount.ClientID%>').value;
            var Gst = document.getElementById('<%=txtGstAmount.ClientID%>').value;

            if (Discount == "") {
                Discount = 0;
            }
            if (Gst == "") {
                Gst = 0;
            }

            document.getElementById("<%= txtNetAmount.ClientID %>").value = (parseFloat(GrossAmount) + parseFloat(Gst) - parseFloat(Discount)).toFixed(5);
        }
        function CalculateAmount() {

            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Rate = document.getElementById('<%=txtPrice.ClientID%>').value;

            document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate).toFixed(5);
        }


        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0 && DocNo.GetText() == "New") {
                alert('Must Enter Quantity');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtPrice.ClientID%>').value;
            if ((str == null || str.length == 0) && DocNo.GetText() == "New") {
                alert('Must Enter Price');
                document.getElementById('<%=txtPrice.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            var lblInvoice = document.getElementById('<%=lblInvoice.ClientID%>').innerHTML;
            if (str == null || str.length == 0) {
                if (lblInvoice == 'Driver Name') {
                    document.getElementById('<%=txtDocumentNo.ClientID%>').focus();
                    alert('Driver Name is required');
                } else {
                    document.getElementById('<%=txtDocumentNo.ClientID%>').focus();
                    alert('Must Enter Invoice/DC No');
                }
                return false;
            }

            return true;
        }


    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transaction Type</label>
                            
                            <dx:ASPxComboBox ID="DrpDocumentType" runat="server" CssClass="form-control"
                                AutoPostBack="true" SelectedIndex="0"  ClientInstanceName="DocNo"
                                OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                <Items>
                                    <dx:ListEditItem Value="2" Text="Purchase" />
                                    <dx:ListEditItem Value="5" Text="Transfer Out"></dx:ListEditItem>
                                    <dx:ListEditItem Value="3" Text="Purchase Return"></dx:ListEditItem>
                                    <%-- <asp:ListItem Value="4">Transfer In</asp:ListItem>--%>
                                    <dx:ListEditItem Value="6" Text="Damage"></dx:ListEditItem>
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span></label>
                            <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" />

                            <dx:ASPxComboBox ID="drpDocumentNo" runat="server" CssClass="form-control" NullText="Plz Select Document No"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                            </dx:ASPxComboBox>

                        </div>
                        <div class="col-md-4">
                            <asp:Literal ID="lbltoLocation" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Supplier</asp:Literal>

                            <dx:ASPxComboBox ID="drpPrincipal" runat="server" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <span class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For" />

                            <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control"
                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                AutoPostBack="True">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">

                            <asp:Literal ID="Label4" runat="server" Visible="false"><span class="fa fa-caret-right rgt_cart"></span>Transfer To</asp:Literal>

                            <dx:ASPxComboBox ID="DrpTransferFor" runat="server" CssClass="form-control"
                                Visible="false">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <span class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="lblInvoice" runat="server" Text="INV/DC  No" MaxLength="100" />
                            <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <span class="fa fa-caret-right rgt_cart"></span>
                            <asp:Label ID="Label1" runat="server" Text="Remarks" />
                            <asp:TextBox ID="txtBuiltyNo" runat="server" MaxLength="250" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Item Description</label>
                            </div>
                            <div class="col-md-1">
                                <label><span class="fa fa-caret-right rgt_cart"></span>UOM</label>
                            </div>
                            <div class="col-md-1">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Qty</label>
                            </div>
                            <div class="col-md-2" id="divPrice" runat="server">
                                <span class="fa fa-caret-right rgt_cart"></span>
                                <label>
                                    <asp:Label ID="lblPrice" runat="server" Text="Price" /></label>
                            </div>
                            <div class="col-md-2" id="divAmount" runat="server">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Amount</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="ddlSkus" runat="server" CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSkus_SelectedIndexChanged">
                                </dx:ASPxComboBox>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="txtUOM" runat="server" CssClass="form-control"
                                    Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                    onkeypress="return onlyDotsAndNumbers(this,event);" onblur="CalculateAmount();"></asp:TextBox>
                            </div>
                            <div class="col-md-1" id="divPrice2" runat="server">
                                <asp:TextBox ID="txtPrice" onblur="CalculateAmount();" runat="server"
                                    onkeypress="return onlyDotsAndNumbers(this,event);" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2" id="divAmount2" runat="server">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Add" CssClass="btn btn-success" />
                            </div>
                            <div class="col-md-1">
                                <label>
                                    <asp:Label ID="lblStock" runat="server" Text="Stock:0"></asp:Label>
                                </label>
                            </div>
                            <div class="col-md-1">
                                <label>
                                    <label>
                                        <asp:Label ID="lblLastPrice" runat="server" Text="Price:0"></asp:Label>
                                    </label>
                                </label>
                            </div>
                        </div>
                        <div class="row center">
                            <div class="col-md-9">
                                <div class="emp-table">
                                    <asp:Panel ID="Panel2" runat="server" Height="150px" Width="100%" ScrollBars="Vertical"
                                        BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                        <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                            CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                            OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                            ShowHeader="False">
                                            <Columns>
                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_CODE" ReadOnly="true">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_NAME" ReadOnly="true">
                                                    <ItemStyle Width="30%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UOM_DESC" ReadOnly="true">
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quantity" ReadOnly="true">
                                                    <ItemStyle Width="15%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Price" ReadOnly="true">
                                                    <ItemStyle Width="15%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Amount" ReadOnly="true">
                                                    <ItemStyle Width="25%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            class="fa fa-trash-o"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-6" id="divGrossAmount" runat="server">
                                        <label><span class="fa fa-caret-right rgt_cart"></span>Gross Amount</label>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6" id="divGSTAmount" runat="server">
                                        <label><span class="fa fa-caret-right rgt_cart"></span>Gst Amount</label>
                                        <asp:TextBox ID="txtGstAmount" runat="server" CssClass="form-control" onkeyup="CalculateNetAmount();"
                                            onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" id="divDiscount" runat="server">
                                        <label><span class="fa fa-caret-right rgt_cart"></span>Discount</label>
                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" onkeyup="CalculateNetAmount();"
                                            onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6" id="divNetAmount" runat="server">
                                        <label><span class="fa fa-caret-right rgt_cart"></span>Net Amount</label>
                                        <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-2">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Total Quantity</label>
                                <asp:TextBox ID="txtTotalQuantity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-7">
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-offset-5 col-md-3 ">
                        <div class="btnlist pull-right">
                            <asp:Button ID="btnSaveDocument" AccessKey="S" OnClick="btnSaveDocument_Click" runat="server" Text="Save" UseSubmitBehavior="False" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancel" AccessKey="C" OnClick="btnCancel_Click" runat="server" Text="Cancel" UseSubmitBehavior="False" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
