<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSKU_Price.aspx.cs" Inherits="SKU_Price" Title="CORN :: Item Price" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
        }

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtFromdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Select price effective date');
                return false;
            }

            str = document.getElementById('<%=txtTradePrice.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Selling price is required');
                return false;
            }

            return true;
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

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 100px">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/OrderPOS/images/wheel.gif"></asp:ImageButton>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Panel ID="Panel23" runat="server" DefaultButton="btnSave">
                                <div class="col-md-12">
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Item Category</label>
                                    <dx:ASPxComboBox ID="ddskucategory" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddskucategory_SelectedIndexChanged" CssClass="form-control">
                                    </dx:ASPxComboBox>
                                    <label>
                                        <span class="fa fa-caret-right rgt_cart"></span>Item Name</label>
                                    <dx:ASPxComboBox runat="server" ID="ddlSkus" OnSelectedIndexChanged="ddlSkus_OnSelectedIndexChanged"
                                        CssClass="form-control" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <label>
                                                <span class="fa fa-caret-right rgt_cart"></span>Date Effected</label>
                                            <asp:TextBox Style="text-align: justify" ID="txtFromdate" TabIndex="2" runat="server"
                                                Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2" style="margin-top: 27px">

                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="30px" />

                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                TargetControlID="txtFromdate" OnClientShown="calendarShown">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Selling Price</label>
                                    <asp:TextBox ID="txtTradePrice" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftetxtTradePrice" runat="server" FilterType="Custom" ValidChars="0123456789."
                                        TargetControlID="txtTradePrice">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                                <div class="col-md-offset-3 col-md-12 ">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success"
                                        Text="Save" ValidationGroup="vg"></asp:Button>
                                    <asp:Button ID="btnViewAll" runat="server" Text="View All" CssClass="btn btn-success"
                                        OnClick="btnViewAll_Click"></asp:Button>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-md-6">
                            <asp:HiddenField ID="hfSKU" runat="server" />
                            <asp:HiddenField ID="hfSKUID" runat="server" />
                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="200px" Visible="false">
                                <asp:ListItem Text="Finish" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Raw" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <strong>
                                <asp:Label ID="Label11" runat="server" Width="100%" Font-Bold="True"
                                    Text="Location Name"></asp:Label></strong>
                            <br>
                            <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="100%"
                                Font-Size="11pt" Text="SelectAll"></asp:CheckBox>
                            <asp:Panel ID="Panel1" runat="server" Width="320px" Height="250px" ScrollBars="Vertical"
                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">

                                <asp:CheckBoxList ID="ChbDistributorList" onclick="UnCheckSelectAll()" runat="server" Width="300px" Font-Size="11pt">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </div>
                    </div>

                    <div class="row">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <hr />
                        </div>
                        <asp:Panel ID="Panel2" runat="server" Width="100%" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Height="200px" ScrollBars="Vertical">
                            <asp:GridView ID="Grid_pricedetails" runat="server"
                                CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                AutoGenerateColumns="False" HorizontalAlign="Center">
                                <Columns>
                                    <asp:BoundField DataField="distributor_name" HeaderText="Location">
                                        <ItemStyle CssClass="grdDetail" Width="25%" />
                                        <HeaderStyle CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SKU_CODE" HeaderText="Item Code">
                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
                                        <HeaderStyle CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name">
                                        <ItemStyle CssClass="grdDetail" Width="40%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Default_Qty" HeaderText="Default Qty">
                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GST_ON" HeaderText="GST">
                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="HidePanel" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DISTRIBUTOR_PRICE" Visible="false" HeaderText="Purchase Price">
                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TRADE_PRICE" HeaderText="Selling Price">
                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                        <HeaderStyle CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RETAIL_PRICE" HeaderText="Retail Price" Visible="false">
                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TAX_PRICE" HeaderText="GST (%)" Visible="false">
                                        <HeaderStyle CssClass="grdHead"></HeaderStyle>
                                        <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="DATE_EFFECTED" HeaderText="Date Effected">
                                        <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                        <HeaderStyle CssClass="grdHead" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="grdHead"></HeaderStyle>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
