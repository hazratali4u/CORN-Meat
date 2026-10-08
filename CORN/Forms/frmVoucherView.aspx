<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVoucherView.aspx.cs" Inherits="Forms_frmVoucherView" Title="CORN :: Voucher View" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="javascript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <div class="row">
                <div class="col-md-8">
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                    <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Type</label>
                    <dx:ASPxComboBox ID="DrpVoucherType" runat="server" CssClass="form-control"
                        AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged">
                    </dx:ASPxComboBox>
                </div>
                <div class="col-md-4">
                    <label><span class="fa fa-caret-right rgt_cart"></span>Voucher Sub Type</label>
                    <dx:ASPxComboBox ID="DrpVoucherSubType" runat="server" CssClass="form-control">
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" MaxLength="10" onkeyup="BlockStartDateKeyPress()"></asp:TextBox>
                </div>
                <div class="col-md-1" style="margin-top: 27px">
                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                        Width="30px" />
                </div>
                <div class="col-md-3">
                    <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" MaxLength="10" onkeyup="BlockEndDateKeyPress()"></asp:TextBox>
                </div>
                <div class="col-md-1" style="margin-top: 27px">
                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                        Width="30px" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-success" OnClick="btnView_Click" Text="View" />
                </div>
            </div>
            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                TargetControlID="txtStartDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                TargetControlID="txtEndDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="emp-table">
                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto" Width="100%" Style="margin-top: 10px">
                        <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered table-hover table-condensed cf"
                            OnRowEditing="GrdLedger_RowEditing">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous" />
                            <Columns>
                                <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" ReadOnly="true">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ledger_date" HeaderText="Voucher Date" ReadOnly="true">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No" ReadOnly="true">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DEBIT" HeaderText="Voucher Amount" ReadOnly="true">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="220px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ReadOnly="true">
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VOUCHER_TYPE_ID" HeaderText="VOUCHER_TYPE_ID" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VOUCHER_DATE" HeaderText="VOUCHER_DATE" ReadOnly="true">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="fa fa-print" CommandName="Edit" ToolTip="Print">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <%--<asp:CommandField HeaderText="Print" ShowEditButton="True" EditText="Print">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="40px" />
                                                    </asp:CommandField>--%>
                            </Columns>
                            <HeaderStyle CssClass="tblhead" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>