<%@ Page Title="CORN :: By Product Price" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChickenSKUPrice.aspx.cs" Inherits="Forms_frmChickenSKUPrice" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" />
    <script type="text/javascript" src="../AjaxLibrary/ValidateDotsAndNumbers.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
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
            <asp:UpdatePanel ID="pnlBillOfMaterial" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
                    <table width="100%">
                        <tr>
                            <td style="width: 65%;" valign="top">
                                <div class="row">
                                    <div class="col-md-5">
                                        <label id="lblSKUFinished"><span class="fa fa-caret-right rgt_cart"></span>Item Name</label>
                                        <dx:ASPxComboBox runat="server" ID="ddlSKUFinished" CssClass="form-control" 
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSKUFinished_SelectedIndexChanged">
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <label><span class="fa fa-caret-right rgt_cart"></span>Today's Live Bird Price</label>
                                        <asp:TextBox ID="txtTradePrice" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtTradePrice" runat="server" FilterType="Custom" ValidChars="0123456789."
                                            TargetControlID="txtTradePrice"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-md-7">
                                        <br />
                                        <asp:Button ID="btnGenerate" Text="Generate Price" runat="server" CssClass="btn btn-success" OnClick="btnGenerate_Click"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlSKU" runat="server" Width="95%" Height="180px"
                                            ScrollBars="Vertical" BorderColor="Silver" BorderStyle="Groove"
                                            BorderWidth="1px">
                                            <asp:GridView Width="100%" ID="gvSKU" runat="server" Class="table table-striped table-bordered table-hover table-condensed cf"
                                                AutoGenerateColumns="False" ShowHeader="true">
                                                <Columns>
                                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID" ReadOnly="true">
                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location" ReadOnly="true">
                                                        <ItemStyle Width="27%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU_NAME" HeaderText="Item Name" ReadOnly="true">
                                                        <ItemStyle Width="28%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ratio_Factor" HeaderText="Ratio Factor" ReadOnly="true">
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TRADE_PRICE" HeaderText="Old Rate" ReadOnly="true">
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NewPrice" HeaderText="New Rate" ReadOnly="true">
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </td>
                            <td style="width: 35%;" valign="top">
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>
                                            <asp:Label ID="Label11" runat="server" Width="100%" Font-Bold="True"
                                                Text="Location Name"></asp:Label></strong>
                                        <br>
                                        <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="100%"
                                            Font-Size="11pt" Text="SelectAll"></asp:CheckBox>
                                        <asp:Panel ID="Panel1" runat="server" Width="320px" Height="290px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">

                                            <asp:CheckBoxList ID="ChbDistributorList" onclick="UnCheckSelectAll()" runat="server" Width="300px" Font-Size="11pt">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-md-offset-4 col-md-3 ">
                            <div class="btnlist pull-right">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

