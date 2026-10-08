<%@ Page Title="CORN :: Supplier Opening" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="fmPrincipalOpening.aspx.cs" Inherits="Forms_fmPrincipalOpening" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

    </script>
    <script type="text/javascript">

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode;

            if (charCode == 9 || charCode == 8) {
                return true;
            }
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                return false;
            }
            if (charCode == 31 || charCode < 48 || charCode > 57)
                return false;
            return true;
        }
    </script>

    <div class="main-contents">
        <div class="container employee-infomation">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="masterId" runat="server" Value="0" />
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                            <dx:ASPxComboBox ID="drpDistributor" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" CssClass="form-control">
                            </dx:ASPxComboBox>
                            <label><span class="fa fa-caret-right rgt_cart"></span>Supplier</label>
                            <dx:ASPxComboBox ID="drpPrincipal" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4" style="min-width: 80px;">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Opening Type</label>
                            <asp:RadioButtonList ID="rblOpening" runat="server"
                                RepeatDirection="Horizontal" style="width: 250px;box-shadow: none;margin-bottom: 20px;    border-radius: 0px !important;height: 34px;padding: 6px 12px;font-size: 14px;line-height: 1.42857143;color: #555;background-color: #fff;background-image: none;border: 1px solid #ccc;">
                                <asp:ListItem Value="0" Text="Credit" Selected="True" style="margin-left: 15px;margin-top: 10px;"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Debit" style="margin-top: 10px;"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Date</label>
                            <asp:TextBox ID="txtOpeningDate" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-md-1">
                            <asp:ImageButton ID="ibOpeningDate" runat="server" Width="30px"
                                Style="padding-top: 25px;" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                            <cc1:CalendarExtender ID="ceOpeningDate" runat="server" TargetControlID="txtOpeningDate"
                                PopupButtonID="ibOpeningDate" Format="dd-MMM-yyyy" OnClientShown="calendarShown"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Balance</label>
                            <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="form-control"
                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Remarks</label>

                            <asp:TextBox ID="txtOpeningBalanceRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 ">
                                <asp:Button ID="btnSaveOpeningBalance" runat="server" OnClick="btnOpeningBalance_Click"
                                    CssClass="btn btn-success" Text="Save" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                    CssClass="btn btn-danger" Text="Cancel" />
                        </div>
                    </div>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
