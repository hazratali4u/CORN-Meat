<%@ Page Title="CORN :: Sync Transaction" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSyncInvoices.aspx.cs" Inherits="Forms_frmSyncInvoices" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <link href="../css/Popup.css" rel="stylesheet" />
    <script type="text/javascript">

        function pageLoad() {

            var popUp = $find('ModelPopup');
            if (popUp) {
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            objPanel = document.getElementById(source._PopupControlID);
            if (objPanel) {
                objPanel.style.display = 'none';
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>
                    </div>
                    <div class="row">
                        <div class="col-md-5">

                            <div class="row">
                                <asp:Label runat="server" ID="lbl_key" Visible="false"></asp:Label>
                                <div class="col-md-8">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                    
                                    <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control">

                                    </dx:ASPxComboBox>
                                </div>

                            </div>
                            <div class="row">

                                <div class="col-md-8">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>From Date</label>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" Style="margin-top: 27px" />
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                        TargetControlID="txtStartDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-8">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>To Date</label>
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="30px" Style="margin-top: 27px" />
                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                        TargetControlID="txtEndDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="btnlist pull-right">
                                        <asp:Button ID="btnGetOrder" OnClick="btnGetOrder_Click" runat="server" Text="Get Data" CssClass="btn btn-success" />

                                        <asp:Button ID="btnSync" runat="server" Text="Sync"
                                            OnClick="btnSync_Click" CssClass="btn btn-primary"
                                            OnClientClick="javascript:return confirm('Are you sure you want to Sync?');return false;" />
                                    </div>
                                    <div class="col-md-2" runat="server" id="dvCalculate" style="display: none;">

                                        <asp:HiddenField runat="server" ID="hfGrid" Value="0" />
                                        <asp:Button ID="btnCalculate" runat="server"
                                            OnClick="btnCalculate_Click" CssClass="btn btn-success" AccessKey="q" Text="Calculate" />

                                    </div>
                                </div>


                            </div>

                        </div>
                        <div class="col-md-7" runat="server" id="dvCalculation" visible="false">

                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Gross Amount</label>
                                    <asp:TextBox ID="lblGrossAmount" Text="0" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Discount</label>
                                    <asp:TextBox ID="lblDiscount" Text="0" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Gst Amount</label>
                                    <asp:TextBox ID="lblGstAmount" Text="0" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Net Amount</label>
                                    <asp:TextBox ID="lblNetAmount" Text="0" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>



                    <div class="row center">
                        <div class="col-md-12">
                            <div class="emp-table">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>--%>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="295px" ScrollBars="Vertical">
                                            <asp:GridView ID="GrdOrder" runat="server" Width="100%" Visible="false"
                                                DataKeyNames="SALE_INVOICE_ID" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TABLE_NO" HeaderText="Table" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Invoice #" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="LASTUPDATE_DATE" HeaderText="Invoice Time" DataFormatString="{0:hh:mm}" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="Customer" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="PAYMENT_TYPE" HeaderText="Pay Type" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gst Amount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount" ReadOnly="true">
                                                        <ItemStyle BorderColor="Silver" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TABLE_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GST" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GST_NUMBER" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CODE" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="cf head"></HeaderStyle>
                                            </asp:GridView>
                                            <asp:GridView ID="GrdOrder2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                                DataKeyNames="SALE_INVOICE_ID" AutoGenerateColumns="False" Visible="true">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChbInvoice" runat="server" Checked="true" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TABLE_NO" HeaderText="Table" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Invoice #" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="LASTUPDATE_DATE" HeaderText="Invoice Time" DataFormatString="{0:hh:mm}" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="Customer" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="PAYMENT_TYPE" HeaderText="Pay Type" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gst Amount" ReadOnly="true"></asp:BoundField>
                                                    <asp:BoundField DataField="NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount" ReadOnly="true">
                                                        <ItemStyle BorderColor="Silver" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TABLE_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GST" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GST_NUMBER" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CODE" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="cf head"></HeaderStyle>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>


                                </div>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

