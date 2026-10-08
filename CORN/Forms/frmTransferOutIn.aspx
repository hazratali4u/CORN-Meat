<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTransferOutIn.aspx.cs" Inherits="Forms_frmTransferOutIn" Title="CORN :: Transfer In" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

    </script>
    <div class="main-contents">
        <div class="container employee-infomation">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Document No</label>
                            
                             <dx:ASPxComboBox ID="drpDocumentNo" runat="server"  CssClass="form-control"
                                AutoPostBack="true" SelectedIndex="0"
                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" >
                              
                            </dx:ASPxComboBox>
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transfer From</label>
                           
                              <dx:ASPxComboBox ID="DrpTransferFor" runat="server"  CssClass="form-control"
                                Enabled="false">
                              
                            </dx:ASPxComboBox>
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transfer To</label>
                          
                             <dx:ASPxComboBox ID="drpDistributor" runat="server"  CssClass="form-control"
                                Enabled="false">
                              
                            </dx:ASPxComboBox>
                        </div>
                    </div>


                    <asp:Label ID="lbltoLocation" runat="server" Width="63px" Text="Order No" CssClass="lblbox" Visible="false"></asp:Label></strong>
                                            
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox" Visible="false"></asp:TextBox>

                    <asp:Label ID="Label3" runat="server" Width="94px" Text="Builty No" CssClass="lblbox" Visible="false"></asp:Label></strong>
                                            
                                                <asp:TextBox ID="txtBuiltyNo" runat="server" Width="195px" CssClass="txtBox" Visible="false"></asp:TextBox>

                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row center">
                <div class="col-md-6">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Vertical"
                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        Width="100%">

                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_CODE" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name" ReadOnly="true"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="true"></asp:BoundField>
                                            <asp:BoundField DataField="UOM_ID" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="cf head" />

                                    </asp:GridView>
                                </asp:Panel>
                                <div class="row" style="margin-top:15px">
                                    <div class="col-md-12 ">
                                        <div class="btnlist pull-right">
                                            <asp:Button AccessKey="S" ID="btnTransferIn" runat="server" Text="Transfer In"
                                                UseSubmitBehavior="False" OnClick="btnTransferIn_Click" CssClass="btn btn-success"></asp:Button>
                                            <asp:Button AccessKey="C" ID="btnCancel" runat="server" Text="Cancel"
                                                UseSubmitBehavior="False" CssClass="btn btn-danger" OnClick="btnCancel_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
