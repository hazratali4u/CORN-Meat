<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSendSMS.aspx.cs" Inherits="Forms_frmSendSMS" Title="CORN :: Send SMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">


        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }


        function SelectAll() {
            var chkBoxList = document.getElementById('<%= LstCustomer.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllCustomer.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllCustomer.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstCustomer.ClientID %>');
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblError" runat="server"  ForeColor="Red" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:CheckBox ID="ChbAllCustomer" onclick="SelectAll()" runat="server" Text="All Customers"
                                         Font-Bold="true" ForeColor="Black"></asp:CheckBox>
                                </div>
                                <div class="panel-body">
                                    <div class="module_contents">
                                        <asp:Panel ID="Panel1" runat="server" Height="350px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                            <asp:CheckBoxList ID="LstCustomer" onclick="UnCheckSelectAll()"
                                                CssClass="table table-striped table-bordered table-hover table-condensed2 cf" runat="server">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Phone #</label>
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                            <label><span class="fa fa-caret-right rgt_cart"></span>SMS Message </label>
                            <asp:TextBox ID="txtSMS" runat="server" CssClass="form-control"
                                TextMode="MultiLine" Rows="12" Columns="20" Style="resize: none;"></asp:TextBox>

                            <asp:Button ID="btnSendSMS" OnClick="btnSendSMS_Click" runat="server" Text="Send SMS" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                        </div>
                        <div class="col-md-1">
                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hfNumber" runat="server"></asp:HiddenField>
        </div>
    </div>
</asp:Content>
