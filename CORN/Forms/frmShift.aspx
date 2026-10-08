<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmShift.aspx.cs" Inherits="Forms_frmShift" Title="CORN :: Shift" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
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
            str = document.getElementById('<%=ddDistributorId.ClientID%>').GetValue();
            
            if (str == null || str.length == 0) {
                alert('Select location');
                return false;
            }
            str = document.getElementById('<%=txtTimeFrom.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Shift start time required.');
                return false;
            }
            str = document.getElementById('<%=txtTimeTo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Shift end time required.');
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
                    <asp:LinkButton ID="btndummy" runat="server" UseSubmitBehavior="false" />
                </asp:Panel>
                <div class="col-md-offset-5 col-md-3 ">
                    <div class="btnlist pull-right">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
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
                                <asp:Panel ID="pnlParameters" runat="server" Style="display: none;" >
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" id="btnClose" runat="server" class="close">
                                                    <span>&times;</span><span class="sr-only">Close</span></button>
                                                <h1 class="modal-title" id="myModalLabel">
                                                    <span class="fa fa-clock-o"></span>Add New Shift</h1>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-10">
                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label id="lbldesignationID"><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                                                        
                                                        <dx:ASPxComboBox ID="ddDistributorId" runat="server" CssClass="form-control"
                                                             AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged">

                                                        </dx:ASPxComboBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>
                                                            <span class="fa fa-caret-right"></span>Time From</label>
                                                        
                                                        <dx:ASPxComboBox ID="txtTimeFrom" runat="server" CssClass="form-control"
                                                            SelectedIndex="0">
                                                            <Items>
                                                            <dx:ListEditItem Text="12:00AM" Value="12:00AM"/>
                                                            <dx:ListEditItem Text="12:30AM" Value="12:30AM"/>
                                                            <dx:ListEditItem Text="1:00AM"  Value="1:00AM" />
                                                            <dx:ListEditItem Text="1:30AM"  Value="1:30AM" />
                                                            <dx:ListEditItem Text="2:00AM"  Value="2:00AM" />
                                                            <dx:ListEditItem Text="2:30AM"  Value="2:30AM" />
                                                            <dx:ListEditItem Text="3:00AM"  Value="3:00AM" />
                                                            <dx:ListEditItem Text="3:30AM"  Value="3:30AM" />
                                                            <dx:ListEditItem Text="4:00AM"  Value="4:00AM" />
                                                            <dx:ListEditItem Text="4:30AM"  Value="4:30AM" />
                                                            <dx:ListEditItem Text="5:00AM"  Value="5:00AM" />
                                                            <dx:ListEditItem Text="5:30AM"  Value="5:30AM" />
                                                            <dx:ListEditItem Text="6:00AM"  Value="6:00AM" />
                                                            <dx:ListEditItem Text="6:30AM"  Value="6:30AM" />
                                                            <dx:ListEditItem Text="7:00AM"  Value="7:00AM" />
                                                            <dx:ListEditItem Text="7:30AM"  Value="7:30AM" />
                                                            <dx:ListEditItem Text="8:00AM"  Value="8:00AM" />
                                                            <dx:ListEditItem Text="8:30AM"  Value="8:30AM" />
                                                            <dx:ListEditItem Text="9:00AM"  Value="9:00AM" />
                                                            <dx:ListEditItem Text="9:30AM"  Value="9:30AM" />
                                                            <dx:ListEditItem Text="10:00AM" Value="10:00AM"/>
                                                            <dx:ListEditItem Text="10:30AM" Value="10:30AM"/>
                                                            <dx:ListEditItem Text="11:00AM" Value="11:00AM"/>
                                                            <dx:ListEditItem Text="11:30AM" Value="11:30AM"/>
                                                            <dx:ListEditItem Text="12:00PM" Value="12:00PM"/>
                                                            <dx:ListEditItem Text="12:30PM" Value="12:30PM"/>
                                                            <dx:ListEditItem Text="1:00PM"  Value="1:00PM" />
                                                            <dx:ListEditItem Text="1:30PM"  Value="1:30PM" />
                                                            <dx:ListEditItem Text="2:00PM"  Value="2:00PM" />
                                                            <dx:ListEditItem Text="2:30PM"  Value="2:30PM" />
                                                            <dx:ListEditItem Text="3:00PM"  Value="3:00PM" />
                                                            <dx:ListEditItem Text="3:30PM"  Value="3:30PM" />
                                                            <dx:ListEditItem Text="4:00PM"  Value="4:00PM" />
                                                            <dx:ListEditItem Text="4:30PM"  Value="4:30PM" />
                                                            <dx:ListEditItem Text="5:00PM"  Value="5:00PM" />
                                                            <dx:ListEditItem Text="5:30PM"  Value="5:30PM" />
                                                            <dx:ListEditItem Text="6:00PM"  Value="6:00PM" />
                                                            <dx:ListEditItem Text="6:30PM"  Value="6:30PM" />
                                                            <dx:ListEditItem Text="7:00PM"  Value="7:00PM" />
                                                            <dx:ListEditItem Text="7:30PM"  Value="7:30PM" />
                                                            <dx:ListEditItem Text="8:00PM"  Value="8:00PM" />
                                                            <dx:ListEditItem Text="8:30PM"  Value="8:30PM" />
                                                            <dx:ListEditItem Text="9:00PM"  Value="9:00PM" />
                                                            <dx:ListEditItem Text="9:30PM"  Value="9:30PM" />
                                                            <dx:ListEditItem Text="10:00PM" Value="10:00PM"/>
                                                            <dx:ListEditItem Text="10:30PM" Value="10:30PM"/>
                                                            <dx:ListEditItem Text="11:00PM" Value="11:00PM"/>
                                                            <dx:ListEditItem Text="11:30PM" Value="11:30PM"/>
                                                            <dx:ListEditItem Text="12:00PM" Value="12:00PM"/>
                                                            <dx:ListEditItem Text="12:30PM" Value="12:30PM"/>
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                        
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>
                                                            <span class="fa fa-caret-right"></span>Time To</label>
                                                        <dx:ASPxComboBox  ID="txtTimeTo" runat="server" CssClass="form-control"
                                                            SelectedIndex="0">
                                                             <Items>
                                                            <dx:ListEditItem Text="12:00AM" Value="12:00AM"/>
                                                            <dx:ListEditItem Text="12:30AM" Value="12:30AM"/>
                                                            <dx:ListEditItem Text="1:00AM"  Value="1:00AM" />
                                                            <dx:ListEditItem Text="1:30AM"  Value="1:30AM" />
                                                            <dx:ListEditItem Text="2:00AM"  Value="2:00AM" />
                                                            <dx:ListEditItem Text="2:30AM"  Value="2:30AM" />
                                                            <dx:ListEditItem Text="3:00AM"  Value="3:00AM" />
                                                            <dx:ListEditItem Text="3:30AM"  Value="3:30AM" />
                                                            <dx:ListEditItem Text="4:00AM"  Value="4:00AM" />
                                                            <dx:ListEditItem Text="4:30AM"  Value="4:30AM" />
                                                            <dx:ListEditItem Text="5:00AM"  Value="5:00AM" />
                                                            <dx:ListEditItem Text="5:30AM"  Value="5:30AM" />
                                                            <dx:ListEditItem Text="6:00AM"  Value="6:00AM" />
                                                            <dx:ListEditItem Text="6:30AM"  Value="6:30AM" />
                                                            <dx:ListEditItem Text="7:00AM"  Value="7:00AM" />
                                                            <dx:ListEditItem Text="7:30AM"  Value="7:30AM" />
                                                            <dx:ListEditItem Text="8:00AM"  Value="8:00AM" />
                                                            <dx:ListEditItem Text="8:30AM"  Value="8:30AM" />
                                                            <dx:ListEditItem Text="9:00AM"  Value="9:00AM" />
                                                            <dx:ListEditItem Text="9:30AM"  Value="9:30AM" />
                                                            <dx:ListEditItem Text="10:00AM" Value="10:00AM"/>
                                                            <dx:ListEditItem Text="10:30AM" Value="10:30AM"/>
                                                            <dx:ListEditItem Text="11:00AM" Value="11:00AM"/>
                                                            <dx:ListEditItem Text="11:30AM" Value="11:30AM"/>
                                                            <dx:ListEditItem Text="12:00PM" Value="12:00PM"/>
                                                            <dx:ListEditItem Text="12:30PM" Value="12:30PM"/>
                                                            <dx:ListEditItem Text="1:00PM"  Value="1:00PM" />
                                                            <dx:ListEditItem Text="1:30PM"  Value="1:30PM" />
                                                            <dx:ListEditItem Text="2:00PM"  Value="2:00PM" />
                                                            <dx:ListEditItem Text="2:30PM"  Value="2:30PM" />
                                                            <dx:ListEditItem Text="3:00PM"  Value="3:00PM" />
                                                            <dx:ListEditItem Text="3:30PM"  Value="3:30PM" />
                                                            <dx:ListEditItem Text="4:00PM"  Value="4:00PM" />
                                                            <dx:ListEditItem Text="4:30PM"  Value="4:30PM" />
                                                            <dx:ListEditItem Text="5:00PM"  Value="5:00PM" />
                                                            <dx:ListEditItem Text="5:30PM"  Value="5:30PM" />
                                                            <dx:ListEditItem Text="6:00PM"  Value="6:00PM" />
                                                            <dx:ListEditItem Text="6:30PM"  Value="6:30PM" />
                                                            <dx:ListEditItem Text="7:00PM"  Value="7:00PM" />
                                                            <dx:ListEditItem Text="7:30PM"  Value="7:30PM" />
                                                            <dx:ListEditItem Text="8:00PM"  Value="8:00PM" />
                                                            <dx:ListEditItem Text="8:30PM"  Value="8:30PM" />
                                                            <dx:ListEditItem Text="9:00PM"  Value="9:00PM" />
                                                            <dx:ListEditItem Text="9:30PM"  Value="9:30PM" />
                                                            <dx:ListEditItem Text="10:00PM" Value="10:00PM"/>
                                                            <dx:ListEditItem Text="10:30PM" Value="10:30PM"/>
                                                            <dx:ListEditItem Text="11:00PM" Value="11:00PM"/>
                                                            <dx:ListEditItem Text="11:30PM" Value="11:30PM"/>
                                                            <dx:ListEditItem Text="12:00PM" Value="12:00PM"/>
                                                            <dx:ListEditItem Text="12:30PM" Value="12:30PM"/>
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" onblur="Change(this, event)"
                                                    OnClick="btnSave_Click" onfocus="Change(this, event)" Text="Save" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" onblur="Change(this, event)"
                                                    OnClick="btnCancel_Click" onfocus="Change(this, event)" Text="Cancel" />
                                                <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink1"
                                                    runat="server">dummy</a>
                                            </div>
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
                                <asp:GridView ID="GridShift" runat="server" CssClass="table table-striped table-bordered table-hover table-condensed cf"
                                    HorizontalAlign="Center" OnRowEditing="GridShift_RowEditing" AutoGenerateColumns="False"
                                    EmptyDataText="No Record exist" OnPageIndexChanging="Grid_users_PageIndexChanging" PageSize="8"
                                    AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChbIsAssigned" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>

                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SHIFT_ID" HeaderText="SHIFT_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_FROM" HeaderText="Time From" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="SHIFT_TO" HeaderText="Time To" ReadOnly="true"></asp:BoundField>
                                        <asp:BoundField DataField="LOCATION_ID" ReadOnly="true">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" ReadOnly="true">
                                            <ItemStyle Width="8%" />

                                        </asp:BoundField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" class="fa fa-pencil">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
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
