<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="frmFinancialYear.aspx.cs" Inherits="Forms_frmFinancialYear" Title="SAMS :: Financial Year" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../js/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
        function Change(obj, evt) {
            if (evt.type == "focus") {
                obj.style.border = "1px solid black";
            }
            else if (evt.type == "blur") {
                obj.style.border = "0px";
            }
        }
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtYearName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Year name is required');
                return false;
            }
            str = document.getElementById('<%=txtStartDate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Start date is required');
                return false;
            }
            str = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Start date is required');
                return false;
            }

            return true;
        }

    </script>
    <div class="main-contents">
        <div class="col-md-12">
        </div>
        <div class="col-md-12">
            &nbsp;
        </div>
        <asp:Panel ID="pnlMainDivision" runat="server" DefaultButton="btnsearch">
            <div class="container employee-infomation">
                <div class="row top">
                    <div class="col-md-4">
                        <div class="search">
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"
                                TabIndex="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-left: -60px;">
                        <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" runat="server" Text="Search"
                            CssClass="btn btn-success"><i class="fa fa-search"  style="font-size:20px;"></i></asp:LinkButton>
                    </div>
                    <asp:LinkButton ID="btndummy2" runat="server" UseSubmitBehavior="false" />
                    <div class="col-md-offset-5 col-md-3 ">
                        <div class="btnlist pull-right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAddDivision" OnClick="btnAddDivision_Click"
                                        Text="Add" Width="100%">
                                <span class="fa fa-plus-circle"></span>Add</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                                    <asp:LinkButton Visible="false" CssClass="btn btn-success" runat="server" Text="Active" ID="btnActive"
                                        OnClick="btnActive_CLICK" OnClientClick="javasacript:return confirm('Are you sure you want to perform this action?'); return false;"><span class="fa fa-check"></span>Active</asp:LinkButton>
                                    <!-- POP UP MODEL-->
                                    <cc1:ModalPopupExtender ID="mPopupDivision" runat="server" DropShadow="False" PopupControlID="pnlDivision"
                                        TargetControlID="hbtn" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                        CancelControlID="btnCloseDivision">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="pnlDivision" DefaultButton="btnSaveCountry" runat="server" Style="display: none; margin-left: -40.5px !IMPORTANT; width: 36%">
                                        <div class="modal-dialog">
                                            <div class="modal-content" style="width: 100%">
                                                <div class="modal-header">
                                                    <button type="button" id="btnCloseDivision" runat="server" class="close">
                                                        <span>&times;</span><span class="sr-only">Close</span></button>
                                                    <h1 class="modal-title" id="H2">
                                                        <span class="fa fa-1x  fa-globe"></span>Add Financial Year</h1>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Year Name</label>
                                                            <asp:TextBox ID="txtYearName" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom"
                                                                TargetControlID="txtYearName" FilterMode="InvalidChars" InvalidChars="><">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Short Name</label>
                                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                                TargetControlID="txtShortName" FilterMode="InvalidChars" InvalidChars="><">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Start Date</label>
                                                            <asp:TextBox ID="txtStartDate" runat="server" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:ImageButton ID="ibnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                Width="30px" Style="margin-top: 24px;" />
                                                            <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnStartDate"
                                                                TargetControlID="txtStartDate" OnClientShown="calendarShown"></cc1:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>End Date</label>
                                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <label>
                                                                <span class="fa fa-caret-right rgt_cart"></span>Description</label>
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                                TargetControlID="txtDescription" FilterMode="InvalidChars" InvalidChars="><">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:RadioButtonList ID="rlistActiveInactive" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                                <asp:ListItem style="margin-left: 5px" Text="Inactive" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:RadioButtonList ID="rlistOpenClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                <asp:ListItem Text="Open" Value="1" Selected="True"></asp:ListItem>
                                                                <asp:ListItem style="margin-left: 5px" Text="Close" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <div class="col-md-6" style="text-align: left">
                                                        <asp:Label runat="server" ID="lblError" ForeColor="Red" Text="Year 2015-2016 is already active." Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnSaveCountry" runat="server" CssClass="btn btn-success" onblur="Change(this, event)"
                                                            OnClick="btnSaveCountry_CLICK" onfocus="Change(this, event)" Text="Save" />
                                                        <asp:Button ID="btnCancelCountry" OnClick="btnCancelCountry_Click" runat="server" CssClass="btn btn-danger" onblur="Change(this, event)"
                                                            onfocus="Change(this, event)" Text="Cancel" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row center">
                            <div class="col-md-12">
                                <div class="emp-table">
                                    <asp:HiddenField ID="hfCode" runat="server" />
                                    <asp:HiddenField ID="hfYearsAdded" runat="server" />
                                    <asp:HiddenField ID="hfStartDate" runat="server" />
                                    <asp:HiddenField ID="hfEndDate" runat="server" />
                                    <asp:GridView ID="Grid_Country" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                        OnRowEditing="Grid_Country_RowEditing" OnPageIndexChanging="Grid_Country_PageIndexChanging" PageSize="8" AllowPaging="true">
                                        <Columns>
                                            <asp:BoundField DataField="CODE" ReadOnly="true" HeaderText="CODE">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="YEAR NAME" ReadOnly="true" HeaderText="Year Name">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SHORT NAME" ReadOnly="true" HeaderText="Short Name">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DISPLAY NAME" ReadOnly="true" HeaderText="DISPLAY NAME">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPTION" ReadOnly="true" HeaderText="Description">
                                                <HeaderStyle Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="START DATE" ReadOnly="true" HeaderText="Start Date">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="END DATE" ReadOnly="true" HeaderText="End Date">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS OPEN" ReadOnly="true" HeaderText="Open Status">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS ACTIVE" ReadOnly="true" HeaderText="Active Status">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS_OPEN" ReadOnly="true" HeaderText="IS_OPEN">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS_ACTIVE" ReadOnly="true" HeaderText="IS_ACTIVE">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" HorizontalAlign="Center" runat="server" CommandName="Edit"
                                                        CssClass="fa fa-pencil"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
