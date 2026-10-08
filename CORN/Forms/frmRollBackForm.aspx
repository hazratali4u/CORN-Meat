<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRollBackForm.aspx.cs" Inherits="Forms_frmRollBackForm" Title="CORN :: Rollback Order" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div class="main-contents">
        <div class="container employee-infomation">
            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 100px">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/OrderPOS/images/wheel.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row" style="display:none">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Transaction Type</label>
                            <asp:DropDownList ID="DrpDocumentType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Order</asp:ListItem>
                                <asp:ListItem Value="1">Invoice</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                           
                            
                                <dx:ASPxComboBox ID="drpDistributor" runat="server" CssClass="form-control"
                                     AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"></dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label1" runat="server" Width="94px" Text="Principal" CssClass="lblbox" Visible="False"></asp:Label>
                            <asp:DropDownList ID="DrpPrincipal" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label6" runat="server" Width="94px" Text="Sale Force" CssClass="lblbox" Visible="False"></asp:Label>
                            <asp:DropDownList ID="DrpOrderBooker" runat="server" class="form-control" Visible="False">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:DropDownList ID="DrpLenged" runat="server" class="form-control" Visible="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                   
                    <div class="row">
                        <div class="col-md-offset-1 col-md-3 ">
                            <div class="btnlist pull-right">
                                <asp:Button ID="btnGetOrder" OnClick="btnGetOrder_Click" CssClass="btn btn-success" runat="server" Text="Get Data" />
                                <asp:Button ID="btnPost" runat="server" Text="Rollback" CssClass="btn btn-danger" OnClick="btnPost_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row center">
            
            <div class="col-md-12">
                
                <div class="emp-table">
                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Width="100%" Height="250px" ScrollBars="Auto">
                                
                                <asp:GridView ID="GrdOrder" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    DataKeyNames="SALE_INVOICE_ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbInvoice" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_TYPE" HeaderText="Customer" ReadOnly="true">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TABLE_NO" HeaderText="Table" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="Document Id" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="LASTUPDATE_DATE" HeaderText="Document Time"
                                            DataFormatString="{0:hh:mm:ss tt}" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="GST Amount" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount" ReadOnly="true"></asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head" />
                                </asp:GridView>
                                <asp:GridView ID="GrdCheque" runat="server" Visible="False"
                                    HorizontalAlign="Center" BorderColor="White" BackColor="White" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CHEQUE_PROCESS_ID" HeaderText="CHEQUE_PROCESS_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbInvoice" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Voucher_No" HeaderText="Voucher No" Visible="False" ReadOnly="true">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Cheque Date" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHEQUE_AMOUNT" DataFormatString="{0:F2}" HeaderText="Cheque Amount" ReadOnly="true">
                                            <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                                   
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
                   
            </div>
                
        </div>

    </div>
</asp:Content>
