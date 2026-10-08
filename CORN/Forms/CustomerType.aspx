<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerType.aspx.cs" Inherits="Forms_CustomerType" Title="Customer Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
     <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
     </script>
    <style type="text/css">
        .cmp input {
            width: 90%;
        }

        .cmp select {
            width: 90%;
        }

        .list input {
            width: 20%;
        }

        .list {
            width: 90%;
        }

        .tblheading {
            background: #006699;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

            .tblheading td {
                color: #ffffff;
                padding: 5px 5px 5px 5px;
            }
    </style>

    <cc1:TabContainer ID="TabContainer1" runat="server" Height="450px" Width="65%"
        ActiveTabIndex="0">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                Designation
            </HeaderTemplate>
            <ContentTemplate>

                <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSaveChannelType">
                       
                        <div class="row">
                            <div class="col-md-6" style="display: none">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Code</label>
                                <asp:TextBox ID="txtChannelCode" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Name</label>
                                <asp:TextBox ID="txtChannelName" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                               
                                
                            </div>
                             <div class="col-md-6">
                                 <asp:Literal ID="lblErrorMsg" runat="server"  Visible="false"></asp:Literal>
                                  </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-5" align="right">
                                <asp:Button ID="btnSaveChannelType" OnClick="btnSaveChannelType_Click" runat="server" Text="New" ValidationGroup="vg" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>
                        <hr />
                            </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="emp-table">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdChannelData" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        AllowPaging="true" PageSize="7" OnPageIndexChanging="grdChannelData_PageIndexChanging"
                                        AutoGenerateColumns="False" OnRowEditing="grdChannelData_RowEditing" OnRowDeleting="grdChannelData_RowDeleting">

                                        <Columns>
                                            <asp:BoundField DataField="REF_ID" HeaderText="Id" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLASH_CODE" HeaderText="Code" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel" Width="20%"></ItemStyle>
                                                 <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLASH_DESC" HeaderText="Name" ReadOnly="true">
                                                <ItemStyle CssClass="grdDetail" Width="60%"></ItemStyle>
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                Department
            </HeaderTemplate>
            <ContentTemplate>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSaveBusType">
                        <div class="row">
                            <div class="col-md-10">
                              
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6" style="display: none">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Code</label>
                                <asp:TextBox ID="txtbustypeCode" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label><span class="fa fa-caret-right rgt_cart"></span>Name</label>
                                <asp:TextBox ID="txtbustypeName" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                  <asp:Literal ID="lblErrorMsgDivsion" runat="server" Visible="false"></asp:Literal>
                                </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-5" align="right">
                                <asp:Button ID="btnSaveBusType" OnClick="btnSaveBusType_Click" runat="server" Text="New" ValidationGroup="vg" CssClass="btn btn-success" />
                                <asp:Button ID="btncancelDestype" OnClick="btncancelDestype_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>
                            <hr />
                            </asp:Panel>                      
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="emp-table">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GrdBusType" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        HorizontalAlign="Center" AutoGenerateColumns="False" AllowPaging="true" PageSize="7"
                                        OnRowEditing="GrdBusType_RowEditing" OnRowDeleting="GrdBusType_RowDeleting" OnPageIndexChanging="GrdBusType_PageIndexChanging">

                                        <Columns>
                                            <asp:BoundField DataField="REF_ID" HeaderText="Id" ReadOnly="true">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLASH_CODE" HeaderText="Code" ReadOnly="true">
                                                <ItemStyle CssClass="HidePanel" Width="20%"></ItemStyle>
                                                 <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SLASH_DESC" HeaderText="Name" ReadOnly="true">
                                                <ItemStyle CssClass="grdDetail" Width="58%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" class="fa fa-trash-o" OnClientClick="javascript:return confirm('Are you sure you want to delete this?');return false;"
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="TabPanel3" Visible="false">
            <HeaderTemplate>
                Allowances
            </HeaderTemplate>
            <ContentTemplate >
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <ContentTemplate>
                        <asp:Panel ID="pnlAllowancesContent" runat="server">
                            <div class="row">
                                <div class="col-md-10">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Description</label>
                                    <asp:HiddenField ID="hdnAllowanceID" runat="server" />
                                    <asp:TextBox ID="txtAllowanceDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceDescription" ErrorMessage="Enter Allowance Description" ControlToValidate="txtAllowanceDescription" ValidationGroup="grpAllowance" runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:RadioButton Visible="false" ID="rdbValue" Width="25px" GroupName="RatioType" runat="server" />
                                        <asp:RadioButton Visible="false" ID="rdbPercentage" Width="25px" GroupName="RatioType" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Ratio</label>
                                    <asp:TextBox ID="txtAllowanceRatio" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4" align="right">
                                    <asp:Button ID="btnSaveAllowance" OnClick="btnSaveAllowance_Click" runat="server" Text="Save" ValidationGroup="grpAllowance" CssClass="btn btn-success" />
                                    <asp:Button ID="btnDiscardAllowance" OnClick="btnDiscardAllowance_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlAllowancesGrid" runat="server" ScrollBars="Auto" Height="365px">



                            <asp:LinkButton Style="margin-bottom: 10px; margin-top: 10px;" CssClass="btn btn-warning" ID="btnShowAllowancesContent" OnClick="btnShowAllowancesContent_Click"
                                runat="server">
                                                <span class="fa fa-plus-circle"></span>Add New Allowance</asp:LinkButton>
                            <asp:GridView ID="GridAllowance" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                BorderColor="SteelBlue" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GridAllowance_RowEditing">

                                <Columns>
                                    <asp:BoundField DataField="AllowanceID" HeaderText="Allowance ID">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AllowanceDescription" HeaderText="Allowance Description">
                                        <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AllowanceRatio" HeaderText="Ratio">
                                        <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RatioType" HeaderText="Ratio Type">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="rdbPercentage" HeaderText="Percentage">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="TabPanel4" Visible="false">
            <HeaderTemplate>
                Deductions
            </HeaderTemplate>
            <ContentTemplate>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlDeductionContent" runat="server">
                            <div class="row">
                                <div class="col-md-10">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Description</label>
                                    <asp:HiddenField ID="hdnDeductionID" runat="server" />
                                    <asp:TextBox ID="txtDeductionDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDeductionDescription" ErrorMessage="Enter Deduction Description"
                                        ControlToValidate="txtDeductionDescription" ValidationGroup="grpDeduction" runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:RadioButton Visible="false" ID="rdbDeductionValue" Width="25px" GroupName="DeductionRatioType" runat="server" />
                                        <asp:RadioButton Visible="false" ID="rdbDeductionPercentage" Width="25px" GroupName="DeductionRatioType" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label><span class="fa fa-caret-right rgt_cart"></span>Ratio</label>
                                    <asp:TextBox ID="txtDeductionRatio" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4" align="right">
                                    <asp:Button ID="btnSaveDeduction" OnClick="btnSaveDeduction_Click" runat="server" Text="Save" ValidationGroup="grpDeduction" CssClass="btn btn-success" />
                                    <asp:Button ID="btnDiscardDeduction" OnClick="btnDiscardDeduction_Click" runat="server" Style="margin-left: 5px" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDeductionGrid" runat="server" ScrollBars="Auto">
                            <asp:LinkButton Style="margin-bottom: 10px; margin-top: 10px;" CssClass="btn btn-warning" ID="btnShowDeductionContent" OnClick="btnShowDeductionContent_Click"
                                runat="server">
                                                <span class="fa fa-plus-circle"></span>Add New Deduction</asp:LinkButton>
                            <asp:GridView ID="GridDeduction" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                BorderColor="SteelBlue" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowEditing="GridDeduction_RowEditing">

                                <Columns>
                                    <asp:BoundField DataField="DeductionID" HeaderText="Deduction ID">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeductionDescription" HeaderText="Deduction Description">
                                        <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeductionRatio" HeaderText="Ratio">
                                        <ItemStyle CssClass="grdDetail" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RatioType" HeaderText="Ratio Type">
                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="grdDetail" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="grdHead" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

</asp:Content>
