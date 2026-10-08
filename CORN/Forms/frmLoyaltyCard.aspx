<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmLoyaltyCard.aspx.cs" Inherits="Forms_frmLoyaltyCard" Title="Loyalty Card" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    
    <script type="text/javascript"  src="../AjaxLibrary/ValidateDotsAndNumbers.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }

        function ValidateForm() {
            var chkBoxP = document.getElementById('<%= chkPrivilege.ClientID %>');
            var chkBoxR = document.getElementById('<%= chkReward.ClientID %>');
            var chkBoxD = document.getElementById('<%= chkDirector.ClientID %>');
            var txtDisc = document.getElementById('<%= txtDiscount.ClientID %>').value;
            var txtLimit = document.getElementById('<%= txtAmountLimit.ClientID %>').value;
            var txtPurchase = document.getElementById('<%= txtPurchasing.ClientID %>').value;
            var txtPoints = document.getElementById('<%= txtPoints.ClientID %>').value;

            

            if (chkBoxP.checked == false && chkBoxR.checked == false && chkBoxD.checked == false) {
                alert('Atleast one card should be selected.');
                return false;
            }

            if (chkBoxP.checked == true && (txtDisc == null || txtDisc == '')) {
                alert('Discount is required.');
                return false;
            }

            if (chkBoxR.checked == true) {
                if (txtPurchase == null || txtPurchase == '') {
                    alert('Purchasing is required');
                    return false;
                }
                else if (txtPoints == null || txtPoints == '') {
                    alert('Points value is required');
                    return false;
                }
            }

            if (chkBoxD.checked == true && (txtLimit == null || txtLimit.length == 0)) {
                alert('Amount limit is required');
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
        <div class="row">
            <div class="col-md-3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server"></asp:CheckBox>
                        <label>All Locations</label>
                        <asp:Panel ID="Panel3" runat="server" Width="255px" Height="331px" ScrollBars="Vertical"
                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="White">
                            <asp:CheckBoxList ID="ChbDistributorList" Font-Size="Small"
                                CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                onclick="UnCheckSelectAll()"
                                runat="server" Width="236px">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-8" style="margin-top: 28px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <asp:Literal ID="lblErrorMsg" runat="server" Visible="false"></asp:Literal>
                            <%--
                                <div class="row">
                                <div class="col-md-4">
                                    <label style="font-size: small"><span class="fa fa-caret-right rgt_cart"></span>Card No</label>
                                    <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                --%>
                            <div class="row">
                                <div class="col-md-1" style="margin-top: 26px;">
                                    <asp:CheckBox ID="chkPrivilege" runat="server" Width="100%"
                                        Font-Size="10pt" align="right"></asp:CheckBox>
                                </div>
                                <div class="col-md-3" style="margin-top: 26px">
                                    <label>Privilege Card</label>
                                </div>
                                
                            <div class="col-md-3">
                                    <label style="font-size: small"><span class="fa fa-caret-right rgt_cart"></span>Discount (%)</label>
                                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control"
                                        onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1" style="margin-top: 26px;">
                                    <asp:CheckBox ID="chkReward" runat="server" Width="100%"
                                        Font-Size="10pt" align="right"></asp:CheckBox>
                                </div>
                                <div class="col-md-3" style="margin-top: 26px">
                                    <label>Rewards Card</label>
                                </div>
                                <div class="col-md-3">
                                    <label style="font-size: small"><span class="fa fa-caret-right rgt_cart"></span>Purchasing</label>
                                    <asp:TextBox ID="txtPurchasing" runat="server" CssClass="form-control"
                                        onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label style="font-size: small"><span class="fa fa-caret-right rgt_cart"></span>Points</label>
                                    <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control"
                                        onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1" style="margin-top: 26px;">
                                    <asp:CheckBox ID="chkDirector" runat="server" Width="100%"
                                        Font-Size="10pt" align="right"></asp:CheckBox>
                                </div>
                                <div class="col-md-3" style="margin-top: 26px">
                                    <label>Director Card</label>
                                </div>
                                <div class="col-md-3">
                                    <label style="font-size: small"><span class="fa fa-caret-right rgt_cart"></span>Amount Limit</label>
                                    <asp:TextBox ID="txtAmountLimit" runat="server" CssClass="form-control"
                                        onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                </div>
                            </div>  
                            
                        </fieldset>
                        <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-10" align="right" style="margin-top: 10px">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" ValidationGroup="vg" CssClass="btn btn-success" />
                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="margin-top: 20px">
                <div class="emp-table">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GrdLoyaltyCard" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                AllowPaging="true" PageSize="15" OnPageIndexChanging="GrdLoyaltyCard_PageIndexChanging"
                                AutoGenerateColumns="False" OnRowEditing="GrdLoyaltyCard_RowEditing" OnRowDeleting="GrdLoyaltyCard_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DISTIBUTOR_ID" HeaderText="LocationID" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARD_TYPE_ID" HeaderText="" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARD_NAME" HeaderText="Card" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" CssClass="grdDetail" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DISCOUNT" HeaderText="Status" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PURCHASING" HeaderText="Status" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="POINTS" HeaderText="Status" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AMOUNT_LIMIT" HeaderText="Status" ReadOnly="true">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" class="fa fa-pencil">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');return false;"
                                                ToolTip="Delete">            
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
