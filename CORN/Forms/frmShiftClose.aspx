<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmShiftClose.aspx.cs" Inherits="Forms_frmShiftClose" Title="CORN :: Shift Close" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function Denomination(count, Ctrl, ctr) {
            ctr.value = count * Ctrl;
            CalculateTotal();
        }
        function ErrorMessages() {
            alert('Shift Closed Successfully');
        }
        function pageLoad() {
            var val;
            val = document.getElementById('<%=txt5000.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal5000.ClientID %>').value = val * 5000;
            val = document.getElementById('<%=txt1000.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal1000.ClientID %>').value = val * 1000;
            val = document.getElementById('<%=txt500.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal500.ClientID %>').value = val * 500;
            val = document.getElementById('<%=txt100.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal100.ClientID %>').value = val * 100;
            val = document.getElementById('<%=txt50.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal50.ClientID %>').value = val * 50;
            val = document.getElementById('<%=txt20.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal20.ClientID %>').value = val * 20;
            val = document.getElementById('<%=txt10.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal10.ClientID %>').value = val * 10;
            val = document.getElementById('<%=txt5.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal5.ClientID %>').value = val * 5;
            val = document.getElementById('<%=txt2.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal2.ClientID %>').value = val * 2;
            val = document.getElementById('<%=txt1.ClientID %>').value;
            if (val == "" || val.lenght == 0 || val == null) {
                val = 0;
            }
            document.getElementById('<%=txtCal1.ClientID %>').value = val * 1;

            CalculateTotal();
        }
        function CalculateTotal() {
            document.getElementById('<%=txtTotal.ClientID %>').value = parseInt(document.getElementById('<%=txtCal5000.ClientID %>').value)
               + parseInt(document.getElementById('<%=txtCal1000.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal500.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal100.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal50.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal20.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal10.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal5.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal2.ClientID %>').value)
                + parseInt(document.getElementById('<%=txtCal1.ClientID %>').value);
            document.getElementById('<%=txtCashSubmitted.ClientID %>').value = document.getElementById('<%=txtTotal.ClientID %>').value
            var val1 = document.getElementById('<%=txtCashInHand.ClientID %>').value;
            if (val1 == "" || val1.lenght == 0 || val1 == null) {
                val1 = 0;
            }
            var val2 = document.getElementById('<%=txtCashSubmitted.ClientID %>').value;
            if (val2 == "" || val2.lenght == 0 || val2 == null) {
                val2 = 0;
            }
            document.getElementById('<%=txtDifference.ClientID %>').value = (val1 - val2).toFixed(2);
        }
        function ValidatePassword() {

            var str;
            str = document.getElementById('<%=ddDistributorId.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select Location');
                return false;
            }
            str = document.getElementById('<%=ddUser.ClientID%>').GetValue();
            if (str == null || str.length == 0) {
                alert('Select User');
                return false;
            }

            return true;
        }
    </script>
    <style>
        .labels {
            margin-top: 11%;
        }
    </style>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblerror" runat="server" Width="312px" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Base Location</label>
                            
                             <dx:ASPxComboBox ID="ddDistributorId" runat="server" CssClass="form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged"></dx:ASPxComboBox>
                            
                        </div>
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>User</label>
                           
                              <dx:ASPxComboBox ID="ddUser" runat="server" CssClass="form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddUser_SelectedIndexChanged"></dx:ASPxComboBox>
                               
                        </div>
                        <div class="col-md-3">
                            <label id="lbltoLocation"><span class="fa fa-caret-right rgt_cart"></span>Shift</label>
                            <asp:TextBox ID="txtShift" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="5000" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt5000" runat="server" onkeyup="Denomination(5000,this.value, document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal5000'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt5000">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal5000" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Opening Cash Recieved" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtOpeningRecieved" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="1000" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt1000" runat="server" onkeyup="Denomination(1000,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal1000'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt1000">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal1000" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Shift Opening" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtOpeningCash" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="500" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt500" runat="server" onkeyup="Denomination(500,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal500'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt500">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal500" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Gross Sale" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGrossSale" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="100" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt100" runat="server" CssClass="form-control" onkeyup="Denomination(100,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal100'))" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt100">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal100" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="GST" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="50" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt50" runat="server" CssClass="form-control" onkeyup="Denomination(50,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal50'))" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt50">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal50" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Discount" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="20" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt20" runat="server" onkeyup="Denomination(20,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal20'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt20">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal20" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Net Sale" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNetSale" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="10" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt10" runat="server" onkeyup="Denomination(10,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal10'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt10">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal10" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Credit Card Sale" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCreditCardSale" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="5" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt5" runat="server" onkeyup="Denomination(5,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal5'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt5">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal5" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Cash Skimmed" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSkimming" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="2" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt2" runat="server" onkeyup="Denomination(2,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal2'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt2">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal2" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Cash In Hand" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCashInHand" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="1" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="X" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt1" runat="server" onkeyup="Denomination(1,this.value,document.getElementById('ctl00_ctl00_mainCopy_cphPage_txtCal1'))" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom"
                                    ValidChars="0123456789" TargetControlID="txt1">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCal1" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Cash Submitted" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCashSubmitted" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="Total" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" Width="312px" Text="=" CssClass="labels" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                                <asp:Label runat="server" Width="312px" Text="Difference" ForeColor="Black" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDifference" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6" style="border-left: 1px solid gray">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Shift Close" CssClass="btn btn-success" />
                            </div>
                            <div class="col-md-3" style="margin-left: -31px">
                                <asp:Button ID="btnPrint" OnClick="btnPrint_Click" runat="server" Width="90px" Text="Print" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPrint" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
