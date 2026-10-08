<%@ Page Title="CORN :: Supplier Claim" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmVendorClaim.aspx.cs" Inherits="Forms_frmVendorClaim" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        function ValidateValueForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Amount is required');
                return false;
            }
        }

    </script>
    <div class="main-contents">
        <div class="container employee-infomation">

            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 100px">
                <asp:Panel ID="Panel21" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                        TargetControlID="txtAmount" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddNew">
                        <div class="row">
                            <div class="col-md-4">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Adjustment Type</label>
                                <asp:RadioButtonList ID="RbdClaimType" runat="server" RepeatDirection="Horizontal" Width="250"
                                    AutoPostBack="True" OnSelectedIndexChanged="RbdClaimType_SelectedIndexChanged">
                                </asp:RadioButtonList>

                                <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                                </dx:ASPxComboBox>

                                <label><span class="fa fa-caret-right rgt_cart"></span>Supplier</label>
                                <dx:ASPxComboBox ID="DrpVendor" runat="server" CssClass="form-control">
                                </dx:ASPxComboBox>
                                <label><span class="fa fa-caret-right rgt_cart"></span>Account Head</label>
                                <dx:ASPxComboBox ID="DrpAccountHead" runat="server" CssClass="form-control"
                                    AutoPostBack="false">
                                </dx:ASPxComboBox>

                                <label><span class="fa fa-caret-right rgt_cart"></span>Amount</label>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                <label><span class="fa fa-caret-right rgt_cart"></span>Remarks</label>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 ">
                                <asp:Button ID="btnAddNew" runat="server" AccessKey="S" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success" Text="Save" />
                                <asp:Button ID="btnCancel" runat="server" AccessKey="S" OnClick="btnCancel_Click"
                                    CssClass="btn btn-danger" Text="Cancel" />
                            </div>
                        </div>
                        <div>
                            <asp:TextBox ID="txtTotalAmount" runat="server" Text="0" Width="100px" CssClass="HidePanel"></asp:TextBox>
                        </div>
                        <div>
                            <hr />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:HiddenField ID="URowId" runat="server" Value="0" />
                            <asp:HiddenField ID="VoucherNo" runat="server" Value="0" />
                            <asp:Panel ID="pnlValue" runat="server" Height="220px" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" ScrollBars="Vertical">
                                <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing" Width="100%" OnRowDataBound="GrdOrder_RowDataBound" ShowFooter="true">
                                    <Columns>
                                        <asp:BoundField DataField="LEDGER_ID" HeaderText="LEDGER_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCOUNT_HEAD_ID" HeaderText="ACCOUNT_HEAD_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRINCIPAL_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="VENDOR_NAME" HeaderText="Supplier" ReadOnly="true">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Account Head" ReadOnly="true">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No" ReadOnly="true">
                                            <ItemStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Balance" DataFormatString="{0:N}" HeaderText="Amount" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="true">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="glVOUCHER_NO" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit">
                                                    <img id="imgEdit" alt="" src="~/images/edit.gif" runat="server" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                    ToolTip="Delete">
                                                    <img id="imgDelete" alt="" src="~/images/delete.gif" runat="server" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>