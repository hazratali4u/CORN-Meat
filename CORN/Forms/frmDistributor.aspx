<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Forms/PageMaster.master"
    CodeFile="frmDistributor.aspx.cs" Inherits="Forms_frmDistributor" Title="CORN :: New Location" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


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

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtDistributorCode.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Location code is required');
                return false;
            }
            str = document.getElementById('<%=txtDistributorName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Location name is required');
                return false;
            }

            str = document.getElementById('<%=txtcontactperson.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Contact person name is required');
                return false;
            }
            str = document.getElementById('<%=txtPhoneNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Valid phone number is required');
                return false;
            }
            str = document.getElementById('<%=txtAddress1.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Address is required');
                return false;
            }
            var lfckv = document.getElementById('<%=cbRegistered.ClientID%>').checked;
            if (lfckv) {
                str = document.getElementById('<%=txtgstno.ClientID%>').value;
                if (str == null || str.length == 0) {
                    alert('GST number is required');
                    return false;
                }
            }

            return true;
        }
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
        function MyJSFunction2() {

            var val = document.getElementById('<%=hfPic.ClientID%>').value;

            if (val == '' || val == null) {
                $('#pic').attr('src', '../images/no-image.jpg').width(150).height(168);
            } else {
                var logo = val;
                $('#pic').attr('src', '../Pics/' + logo).width(150).height(168);
            }
        }
        function readURL(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pic').attr('src', e.target.result).width(170).height(170);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        function checkDisable(checkbox)
        {
            if (checkbox.id == "ctl00_ctl00_mainCopy_cphPage_chksmsDelivery") {
                if (checkbox.checked) {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDelivery").disabled = false;
                }
                else {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDelivery").value = "";
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDelivery").disabled = true;
                }
            }
            if (checkbox.id == "ctl00_ctl00_mainCopy_cphPage_chksmsTakeAway") {
                if (checkbox.checked) {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsTakeAway").disabled = false;
                } else {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsTakeAway").value = "";
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsTakeAway").disabled = true;
                }
            }
            if (checkbox.id == "ctl00_ctl00_mainCopy_cphPage_chksmsDayClose") {
                if (checkbox.checked) {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDayClose").disabled = false;
                } else {
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDayClose").value = "";
                    document.getElementById("ctl00_ctl00_mainCopy_cphPage_txtsmsDayClose").disabled = true;

                }
            }
        }
    </script>
    <div class="main-contents">
        <div class="container employee-infomation">
            <div style="z-index: 101; left: 50%; width: 100px; position: absolute; top: 150px; height: 100px">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="27px" />
                            Wait Update.......
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </div>
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
                    <asp:LinkButton ID="btndummy" runat="server" UseSubmitBehavior="false" />
                </asp:Panel>
                <div class="col-md-offset-5 col-md-3 ">
                    <div class="btnlist pull-right">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfDistributorID" runat="server" />
                                <asp:HiddenField ID="hfPic" runat="server" />
                                <asp:LinkButton CssClass="btn btn-warning" runat="server" ID="btnAdd" Text="Add"
                                    OnClick="btnAdd_Click">
                               <span class="fa fa-plus-circle"></span>Add
                                </asp:LinkButton>
                                <asp:LinkButton class="btn btn-success" OnClick="btnActive_Click" ID="btnActive" runat="server"
                                    OnClientClick="javascript:return confirm('Are you sure you want to perform this action?');return false;">
                                    <span class="fa fa-check"></span>Active</asp:LinkButton>
                                <!-- POP UP MODEL-->
                                <cc1:ModalPopupExtender ID="mPopUpLocation" runat="server" PopupControlID="pnlParameters"
                                    TargetControlID="btnAdd" BehaviorID="ModelPopup" BackgroundCssClass="modal-background"
                                    CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlParameters" runat="server" Style="display: none;" ScrollBars="Auto" DefaultButton="btnSave">
                                    <div class="modal-dialog" style="width: 1030px; margin-left: 0px">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" class="close" runat="server" onserverclick="btnClose_Click">
                                                    <span>&times;</span><span class="sr-only">Close</span></button>

                                                <h1 class="modal-title" id="myModalLabel">
                                                    <span></span>Add New Location</h1>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <div class="modal-body" >
                                                        <div class="row">
                                                            <div class="col-md-9">
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Company</label>
                                                                    
                                                                    <dx:ASPxComboBox ID="DrpCompanyName" runat="server" CssClass="form-control">
                                                                    </dx:ASPxComboBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Location Type</label>
                                                                   
                                                                    <dx:ASPxComboBox ID="ddDistributorType" runat="server" CssClass="form-control">
                                                                    </dx:ASPxComboBox>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Code</label>
                                                                    <asp:TextBox ID="txtDistributorCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Name</label>
                                                                    <asp:TextBox ID="txtDistributorName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Contact Person</label>
                                                                    <asp:TextBox ID="txtcontactperson" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Phone</label>
                                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="ftetxtPhoneNo" runat="server" FilterType="Custom"
                                                                        TargetControlID="txtPhoneNo" ValidChars="0123456789+-">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Email Address</label>
                                                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>

                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Facebook Address</label>
                                                                    <asp:TextBox ID="txtFacebookAddress" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                    ErrorMessage="Invalid Email" ControlToValidate="txtFacebookAddress"  ValidationGroup="emailvalidate"
                                                                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" 
                                                                    Display="Dynamic"></asp:RegularExpressionValidator> --%>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>GST(%)</label>
                                                                    <asp:TextBox ID="txtServiceTax" runat="server" ng-disabled="checked5" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Address</label>
                                                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <input type="checkbox" id="chksmsDelivery" runat="server" font-bold="true" onclick="checkDisable(this)" />
                                                                    <label>Is SMS on delivery hold order</label>
                                                                    <asp:TextBox ID="txtsmsDelivery" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <input type="checkbox" id="chksmsTakeAway" ng-model="checked2" runat="server" font-bold="true" onclick="checkDisable(this)"/>
                                                                    <label>Is SMS on Take away</label>
                                                                    <asp:TextBox ID="txtsmsTakeAway" runat="server" ng-disabled="!checked2" TextMode="MultiLine" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <input type="checkbox" id="chksmsDayClose" runat="server" ng-model="checked3" font-bold="true" onclick="checkDisable(this)"/>
                                                                    <label>Is SMS on Day close</label>
                                                                    <asp:TextBox ID="txtsmsDayClose" runat="server" ng-disabled="!checked3" TextMode="MultiLine" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label><span class="fa fa-caret-right rgt_cart"></span>Contact No.</label>
                                                                    <asp:TextBox ID="txtSmsContact" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                                        TargetControlID="txtSmsContact" ValidChars="0123456789+-,">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <input type="checkbox" id="cbRegistered" ng-model="checked4" runat="server" font-bold="true" />
                                                                    <label>Is Registered</label>
                                                                    <asp:TextBox ID="txtgstno" runat="server" ng-disabled="!checked4" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-4" style="margin-top: 35px; display: none">
                                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Font-Bold="true"></asp:CheckBox>
                                                                    <label>Is Active</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3" style="margin-top: 25px;">
                                                                <div class="col-md-12">
                                                                    <label for="ctl00_ctl00_mainCopy_cphPage_fuPic" id="lbl"
                                                                        runat="server" style="cursor: pointer;">
                                                                        <img src="../images/noimage.jpg" id="pic" name="pic" width="170" height="170" />
                                                                    </label>
                                                                    <asp:FileUpload ID="fuPic" onchange="readURL(this);" Style="display: none;" runat="server"></asp:FileUpload>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <asp:CheckBox ID="chkShowLogo" runat="server" Checked="true" Font-Bold="true"></asp:CheckBox>
                                                                    <label>Show Logo</label>
                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 35px" runat="server" id="divCover" visible="false">
                                                                    <asp:CheckBox ID="chkIsCoverTable" runat="server" Font-Bold="true" Checked="true"></asp:CheckBox>
                                                                    <label>Is Cover Table</label>
                                                                    <br />
                                                                    <input type="checkbox" id="chkService" ng-model="checked5" runat="server" font-bold="true" />
                                                                    <label>Service Charges</label>
                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 35px;">
                                                                    <asp:RadioButtonList ID="rdbServiceType" runat="server" RepeatDirection="Vertical"
                                                                        Font-Bold="true">
                                                                        <%--<asp:ListItem Text="Dine In" Value="1" Selected="True"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="Delivery" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Takeaway" Value="3" Selected="True"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="row">
                                                                    <div class="btnlist pull-right" style="margin-top: 0%">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                            ErrorMessage="Invalid Email" ControlToValidate="txtEmailAddress" ValidationGroup="emailvalidate"
                                                                            ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                                                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--   ////////////////////////////////////--%>
                                                    <div class="row" style="display: none">
                                                        <div class="col-md-4">
                                                            <label><span class="fa fa-caret-right rgt_cart"></span>Searching Type</label>
                                                            <asp:DropDownList ID="ddSearchType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                                                <asp:ListItem Value="Distributor_Code">Distributor Code</asp:ListItem>
                                                                <asp:ListItem Value="Distributor_Name">Distributor Name</asp:ListItem>
                                                                <asp:ListItem Value="Contact_Person">Contact Person</asp:ListItem>
                                                                <asp:ListItem Value="CONTACT_NUMBER">Contact No</asp:ListItem>
                                                                <asp:ListItem Value="TYPENAME">Distributor Type</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtSeach" runat="server" CssClass="form-control" Style="margin-top: 24px"></asp:TextBox>
                                                            <asp:TextBox ID="txtpassword" runat="server" Visible="False" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <button id="btnFilter" class="btn btn-warning" onclick="btnFilter_Click" style="margin-top: 24px"><span class="fa fa-plus-circle"></span>Filter</button>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                </Triggers>
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

                                <asp:GridView ID="GridDistributor" AllowPaging="true" runat="server"
                                    CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grdData_PageIndexChanging" PageSize="8"
                                    OnRowEditing="GridDistributor_RowEditing" EmptyDataText="No Record exist">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DISTRIBUTOR_ID" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_REGISTERED" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUBZONE_ID" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS2" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_CODE" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Name" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="TYPENAME" HeaderText="Type" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="ADDRESS1" HeaderText="Address" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst Number" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="COMPANY_ID" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISDELETED" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" class="fa fa-pencil" ToolTip="Edit">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="GST" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsCoverTable" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERVICE_CHARGES" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FACEBOOK_ADDRESS" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERVICE_TYPE" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PIC" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHOW_LOGO" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsSMSonDeliveryHoldOrder" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="strMessageonDeliveryHoldOrder" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsSMSonTakeAway" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="strMessageonTakeAway" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsSMSonDayClose" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="strMessageDayClose" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="strContactNo" ReadOnly="true">
                                            <ItemStyle CssClass="HidePanel" />
                                            <HeaderStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="cf head"></HeaderStyle>
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

