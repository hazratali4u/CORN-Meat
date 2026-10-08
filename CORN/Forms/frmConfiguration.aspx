<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmConfiguration.aspx.cs" Inherits="Forms_frmConfiguration" Title="CORN :: Configuration" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
       
        function Message(msg) {
            alert(msg);
        }
        
        function SMSTypeChange(sender) {

            if (sender.GetValue().toString() == "Web Service") {
                document.getElementById('<%=divComPort.ClientID %>').style.display = "none";
                document.getElementById('<%=divBaudRate.ClientID %>').style.display = "none";
                document.getElementById('<%=divTimeOut.ClientID %>').style.display = "none";

                document.getElementById('<%=divUserId.ClientID %>').style.display = 'block';
                document.getElementById('<%=divPassword.ClientID %>').style.display = 'block';
                document.getElementById('<%=divMask.ClientID %>').style.display = 'block';
            }
            else if (sender.GetValue().toString() == "GSM") {

                document.getElementById('<%=divComPort.ClientID %>').style.display = "block";
                document.getElementById('<%=divBaudRate.ClientID %>').style.display = 'block';
                document.getElementById('<%=divTimeOut.ClientID %>').style.display = 'block';

                document.getElementById('<%=divUserId.ClientID %>').style.display = 'none';
                document.getElementById('<%=divPassword.ClientID %>').style.display = 'none';
                document.getElementById('<%=divMask.ClientID %>').style.display = 'none';
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>

                            <dx:ASPxComboBox ID="drpLocation" runat="server" CssClass="form-control"
                                OnSelectedIndexChanged="drpLocation_SelectedIndexChanged" AutoPostBack="true">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Page Setting</div>
                                <div class="panel-body" style="height: 100%">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Margin Left</label>
                                            <asp:TextBox ID="txtMarginLeft" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                ValidChars="0123456789." TargetControlID="txtMarginLeft"></cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-md-6">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Margin Right</label>
                                            <asp:TextBox ID="txtMarginRight" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                ValidChars="0123456789." TargetControlID="txtMarginRight"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Margin Top</label>
                                            <asp:TextBox ID="txtMarginTop" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom"
                                                ValidChars="0123456789." TargetControlID="txtMarginTop"></cc1:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-md-6">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Margin Bottom</label>
                                            <asp:TextBox ID="txtMarginBottom" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom"
                                                ValidChars="0123456789." TargetControlID="txtMarginBottom"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Orientation</label>


                                            <dx:ASPxComboBox ID="drpOrientation" runat="server" CssClass="form-control">
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Paper Format</label>


                                            <dx:ASPxComboBox ID="drpPaperFormat" runat="server" CssClass="form-control">
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Printer Setting</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Default Printer</label>
                                            <asp:TextBox ID="txtDefaultPrinter" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Address</label>
                                            <asp:TextBox ID="txtPrinterAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Port</label>
                                            <asp:TextBox ID="txtPrinterPort" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>No. of Copies</label>
                                            <asp:TextBox ID="txtNoofCopies" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtNoofCopies"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="chkDirectPrinting" runat="server" />
                                            &nbsp;&nbsp;
                                                Direct Printing
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Email Setting</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>SMTP Server</label>
                                            <asp:TextBox ID="txtSMTPServer" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>User Name</label>
                                            <asp:TextBox ID="txtEmailUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Password</label>
                                            <asp:TextBox ID="txtEmailPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Port</label>
                                            <asp:TextBox ID="txtEmailPort" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:CheckBox ID="chkIsEnabledSSL" runat="server" />
                                            &nbsp;&nbsp;
                                                Is Enabled SSL
                                        </div>
                                        <div class="col-md-7">
                                            <asp:CheckBox ID="chkUseDefaulCredentials" runat="server" />
                                            &nbsp;&nbsp;
                                                Use Default Credentials
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">SMS Setting</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label><span class="fa fa-caret-right rgt_cart"></span>Sending Type</label>
                                          
                                            
                                      <dx:ASPxComboBox ID="drpSMSSendingType"  
                                          runat="server" CssClass="form-control">
                                          <%-- <ClientSideEvents SelectedIndexChanged="SMSTypeChange" />--%>
                                          <ClientSideEvents SelectedIndexChanged="function(s, e) { SMSTypeChange(s); }" />
                                     </dx:ASPxComboBox>

                                        </div>
                                    </div>
                                    <div class="row" id="divComPort" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblComPort" runat="server"><span class="fa fa-caret-right rgt_cart"></span>COM Port</label>
                                            <asp:TextBox ID="txtSMSCOMPort" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" id="divBaudRate" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblBaudRate" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Baud Rate</label>
                                            <asp:TextBox ID="txtSMSBaudRate" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" id="divTimeOut" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblTimeout" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Timeout</label>
                                            <asp:TextBox ID="txtSMSTimeout" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtSMSTimeout"></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="row" id="divUserId" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblUserID" runat="server"><span class="fa fa-caret-right rgt_cart"></span>User ID</label>
                                            <asp:TextBox ID="txtSMSUserID" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" id="divPassword" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblPassword" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Password</label>
                                            <asp:TextBox ID="txtSMSPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" id="divMask" runat="server">
                                        <div class="col-md-12">
                                            <label id="lblMask" runat="server"><span class="fa fa-caret-right rgt_cart"></span>Mask</label>
                                            <asp:TextBox ID="txtSMSMask" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Inventory Setting</div>
                                <div class="panel-body">
                                    <div class="row" style="display: none">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="chkIsEncripted" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Use Encripted Credentials
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 10px">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="chkClosingStockStatus" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Show Closing Stock Status
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 10px">
                                        <div class="col-md-12">
                                            <asp:CheckBox ID="chkOversaleAllowed" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Negative Stock Allowed
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-bottom: 10px">
                                            <asp:CheckBox ID="chkExpiryAllowed" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Expiry Allowed
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-bottom: 10px">
                                            <asp:CheckBox ID="chkRawMaterialOnProduction" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Raw Material Deduction Allowed on Production
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-bottom: 10px">
                                            <asp:CheckBox ID="chkPackageMaterialDeductionOnSale" Checked="true" runat="server" />
                                            &nbsp;&nbsp;
                                                Package Material Deduction On Sale
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-offset-1 col-md-3 ">
                            <div class="btnlist pull-right">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" Visible="false" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
