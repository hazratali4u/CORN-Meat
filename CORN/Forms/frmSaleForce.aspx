<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSaleForce.aspx.cs" Inherits="Forms_frmSaleForce" Title="CORN :: Employee Information" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript"  src="../AjaxLibrary/ValidateDotsAndNumbers.js"></script>
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();
            oControl.value = "Wait...";
            oControl.disabled = true;
        }
       

    function calendarShown(sender, args) {
        sender._popupBehavior._element.style.zIndex = 10005;
    }
    function ValidateForm() {
        var str;
        str = document.getElementById('<%=txtUserName.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Employee name is required.');
            return false;
        }
        str = document.getElementById('<%=txtNICNo.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('NIC no. is required.');
            return false;
        }

        str = document.getElementById('<%=txtMobileNo.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Mobile no. is required.');
            return false;
        }
        str = document.getElementById('<%=txtAddress2.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Permanent address is required.');
            return false;
        }

        str = document.getElementById('<%=drpShift.ClientID%>').GetValue();

        if (str == "0") {
            alert("Please select shift");
            return false;
        }
        str = document.getElementById('<%=txtLoginId.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Login id is required');
            return false;
        }
        str = document.getElementById('<%=txtpassword.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Password is required.');
            return false;
        }
        return true;
    }

    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }

        headerCheckBox.checked = checked;
    }

    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">

            <div class="row top">
                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnsearch">
                    <div class="col-md-4">
                        <div class="search">
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search" CssClass="form-control"
                                TabIndex="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-left: -60px;">
                        <asp:LinkButton ID="btnsearch" OnClick="btnFilter_Click" runat="server" Text="Search"
                            CssClass="btn btn-success"><i class="fa fa-search"  style="font-size:20px;"></i></asp:LinkButton>
                    </div>
                </asp:Panel>
                <div class="col-md-offset-5 col-md-3 ">
                    <div class="btnlist pull-right">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                    OnClick="btnAdd_Click">
                               <span class="fa fa-plus-circle"></span>Add
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="hbutton"></asp:LinkButton>
                                <asp:LinkButton ID="btnActive" runat="server" class="btn btn-success" OnClick="btnActive_Click"
                                    OnClientClick="javascript:return confirm('Are you sure you want to perform this action?');return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                <!-- POP UP MODEL-->
                                <cc1:ModalPopupExtender ID="mPopUpLocation" runat="server" PopupControlID="pnlParameters"
                                    TargetControlID="hbutton" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                    CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlParameters" runat="server" Style="display: none;" ScrollBars="Auto"  DefaultButton="btnSave">
                                    <div class="modal-dialog" style="width: 830px; margin-left: 0px">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" class="close" runat="server" onserverclick="btnClose_Click">
                                                    <span>&times;</span><span class="sr-only">Close</span></button>
                                                <h1 class="modal-title" id="myModalLabel">
                                                    <span></span>Add New Employee Information</h1>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>

                                                    <div class="modal-body">
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <asp:Literal ID="lblErrorMsg" runat="server" Visible="false"></asp:Literal>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label id="lbldesignationID"><span class="fa fa-caret-right rgt_cart"></span>Base Location</label>
                                                                
                                                                
                                                                <dx:ASPxComboBox ID="ddDistributorId" runat="server" CssClass="form-control" AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged">

                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Name</label>
                                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Designation</label>
                                                                
                                                                <dx:ASPxComboBox ID="ddDesignation" runat="server" CssClass="form-control">

                                                                </dx:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>N.I.C No</label>
                                                                <asp:TextBox ID="txtNICNo" runat="server" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Department</label>
                                                                <dx:ASPxComboBox  ID="drpDepartment" runat="server" CssClass="form-control">
                                                                </dx:ASPxComboBox>

                                                            </div>
                                                            <div class="col-md-3">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Joining Date</label>
                                                                <asp:TextBox ID="txtJoinDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1" style="margin-top: 30px">
                                                                <asp:ImageButton ID="ibtnJoinDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                    Width="30px" />
                                                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnJoinDate"
                                                                    TargetControlID="txtJoinDate" OnClientShown="calendarShown">
                                                                </cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label id="lblMobileNo"><span class="fa fa-caret-right rgt_cart"></span>Mobile No</label>
                                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="ftetxtMobileNo" runat="server" FilterType="Custom"
                                                                    TargetControlID="txtMobileNo" ValidChars="0123456789+-">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label id="lblPhNo"><span class="fa fa-caret-right rgt_cart"></span>Phone No</label>
                                                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="ftetxtPhoneNo" runat="server" FilterType="Custom"
                                                                    TargetControlID="txtPhoneNo" ValidChars="0123456789+-">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Email Address</label>
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                    ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ValidationGroup="emailvalidate"
                                                                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Shift</label>
                                                                <dx:ASPxComboBox ID="drpShift" runat="server" CssClass="form-control">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Employee Discount</label>
                                                                <asp:TextBox ID="txtEMCDiscount" runat="server" CssClass="form-control "></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                                    TargetControlID="txtEMCDiscount" ValidChars="0123456789.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="col-md-4">
                                                                 <label><span class="fa fa-caret-right rgt_cart"></span>Approval Authority </label>
                                                                <dx:ASPxComboBox ID="drpApprovalAuthority" runat="server" CssClass="form-control">
                                                                </dx:ASPxComboBox>
                                                            </div>
                                                            
                                                        </div>
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <label><span class="fa fa-caret-right rgt_cart"></span>Card Type</label>
                                                                        <dx:ASPxComboBox ID="drpCardType" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpCardType_SelectedIndexChanged"
                                                                            AutoPostBack="true">
                                                                        </dx:ASPxComboBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:Literal id="ltrlCardNo" runat="server"><label><span class="fa fa-caret-right rgt_cart"></span>Card No</label></asp:Literal>
                                                                        <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                               

                                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtPurchasing" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>


                                                                <div class="col-md-4">
                                                                    <asp:Literal id="ltrlAmountLimit" runat="server"><label><span class="fa fa-caret-right rgt_cart"></span>Amount Limit</label></asp:Literal>
                                                                       
                                                                    <asp:TextBox ID="txtAmountLimit" runat="server" CssClass="form-control"
                                                                         onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                                 
                                                                </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Permanent Address</label>
                                                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label><span class="fa fa-caret-right rgt_cart"></span>Present Address</label>
                                                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        
                                                                <asp:TextBox ID="txtLoginId" valign="top" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                                <asp:TextBox ID="txtpassword" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                         
                                                        <div class="row">
                                                            <div class="col-md-offset-9 col-md-3 ">
                                                                <div class="btnlist pull-right">
                                                                    <asp:HiddenField ID="hfStatus" runat="server" Value="Active" />
                                                                    <asp:HiddenField ID="hfLoyalCard" runat="server" />
                                                                    <asp:HiddenField ID="hfUsedId" Value="0" runat="server" />
                                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" ValidationGroup="emailvalidate" CssClass="btn btn-success" CausesValidation="true" />
                                                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" CausesValidation="false" ValidationGroup="emailvalidate" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row center">
                <div class="col-md-12">
                    <div class="emp-table">
                        <asp:UpdatePanel ID="UpdatePanelDetail" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Grid_users" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    HorizontalAlign="Center" AutoGenerateColumns="False" OnPageIndexChanging="Grid_users_PageIndexChanging" PageSize="20"
                                    OnRowEditing="Grid_users_RowEditing" AllowPaging="True" EmptyDataText="No Record exist">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>

                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="USER_ID" HeaderText="User Id" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USER_CODE" HeaderText="Code" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USER_NAME" HeaderText="Name" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NIC_NO" HeaderText="NIC No" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PHONE" HeaderText="Phone No" ReadOnly="true" Visible="false">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOBILE" HeaderText="Mobile" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EMAIL" HeaderText="Email" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS1" HeaderText="Present Address" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS2" HeaderText="Permanent Address" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SLASH_DESC" HeaderText="Designation" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USER_TYPE_ID" HeaderText="USERTYPE_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATE_JOIN" HeaderText="DATE_JOIN" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dept_ID" HeaderText="Dept_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EMC_LimitPerDay" HeaderText="Discount" ReadOnly="true">
                                            <ItemStyle CssClass="grdDetail"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_ID" HeaderText="SHIFT_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AMOUNT_LIMIT" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Manager_UserID" HeaderText="Manager_UserID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARD_TYPE_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="strCardNo" HeaderText="strCardNo" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" class="fa fa-pencil">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="10" NextPageText=">" PreviousPageText="<" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
