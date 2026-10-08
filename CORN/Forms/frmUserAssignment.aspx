<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmUserAssignment.aspx.cs"
    Inherits="Forms_frmUserAssignment" Title="CORN :: User Assignment" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/Popup.css" rel="stylesheet" />
    <script language="JavaScript" type="text/javascript">


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
                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif"  />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn" PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div class="main-contents">
                    <div class="container user_assignment">
                        <div class="row top">
                            <div class="col-md-3">
                                <label>
                                    <span class="fa fa-caret-right rgt_cart"></span>User</label>
                                
                                <dx:ASPxComboBox ID="ddUser" runat="server" CssClass="form-control" 
                                    TabIndex="1" OnSelectedIndexChanged="ddUser_SelectedIndexChanged"
                                    AutoPostBack="True"></dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row bottom">
                            <div class="col-md-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <span class="fa fa-caret-right rgt_cart"></span>Location Assignment
                                    </div>
                                    <div class="panel-body">
                                       
                                <dx:ASPxComboBox ID="ddDistributorType" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddDistributorType_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="2"></dx:ASPxComboBox>
                                        <div class="module_contents">
                                            <div class="col-md-5">
                                                <asp:ListBox ID="lstUnAssignDistributor" runat="server"
                                                    Height="350px" Width="200px" CssClass="select" TabIndex="3"></asp:ListBox>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="navs navslist">
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnAssignLocation" CssClass="fa fa-angle-right" OnClick="btnAssignLocation_Click"
                                                                runat="server" TabIndex="4" /></span>
                                                    </div>
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnUnAssignLocation" CssClass="fa fa-angle-left" OnClick="btnUnAssignLocation_Click"
                                                                runat="server" TabIndex="5" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:ListBox ID="lstAssignDistributor" runat="server"
                                                    Height="350px" Width="200px" CssClass="select" TabIndex="6"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <span class="fa fa-caret-right rgt_cart"></span>Supplier Assignment
                                    </div>
                                    <div class="panel-body">
                                        <div class="module_contents">
                                            <div class="col-md-5">
                                                <asp:ListBox ID="lstUnAssignBrand" runat="server" Height="403px" Width="200px"
                                                    CssClass="select" TabIndex="7"></asp:ListBox>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="navs navslist">
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnAssignPrincipal" OnClick="btnAssignPrincipal_Click" CssClass="fa  fa-angle-right"
                                                                runat="server" TabIndex="8" /></span>
                                                    </div>
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnAssignAllPrincipal" OnClick="btnAssignAllPrincipal_Click"
                                                                CssClass="fa  fa-angle-double-right" runat="server" TabIndex="9" /></span>
                                                    </div>
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnUnAssignAllPrincipal" CssClass="fa  fa-angle-double-left"
                                                                OnClick="btnUnAssignAllPrincipal_Click" runat="server" TabIndex="10" /></span>
                                                    </div>
                                                    <div class="nxt arrow">
                                                        <span>
                                                            <asp:LinkButton ID="btnUnAssignPrincipal" OnClick="btnUnAssignPrincipal_Click" runat="server"
                                                                CssClass="fa fa-angle-left" TabIndex="11" /></span>

                                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="False" Text="Save"
                                                            CssClass="button" TabIndex="12" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:ListBox ID="lstAssignBrand" runat="server" Height="403px"
                                                    CssClass="select" RepeatLayout="Flow" TabIndex="13"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:LinkButton ID="LinkButton5" runat="server" UseSubmitBehavior="false" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
