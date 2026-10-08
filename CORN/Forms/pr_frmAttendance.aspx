<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="pr_frmAttendance.aspx.cs" Inherits="pr_frmAttendance" Title="Attendance" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

    <script type="text/javascript">
        function checkDate(sender, args) {
            var chkDate = new Date();
            var firstDay = new Date(chkDate.getFullYear(), chkDate.getMonth(), 1);
            firstDay.setMonth(firstDay.getMonth() - 1);
            if (sender._selectedDate < firstDay) {
                alert("You can only select current or previous");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                return;
            }
        }
    </script>
    <style>
        .tblheading {
            background: #006699;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

            .tblheading td {
                color: #ffffff;
                padding: 5px 5px 5px 5px;
            }

        ajax__calendar_container {
            visibility: hidden;
            position: absolute;
            left: 404px;
            top: 364px;
            z-index: 999;
            display: none;
        }
    </style>
    <div class="main-contents">
        <div class="container employee-infomation">
            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                <ContentTemplate>

                    <asp:UpdateProgress ID="UpdateProgress" runat="server">
                        <ProgressTemplate>
                            <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                Width="31px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:LinkButton runat="server" ID="hbtn"></asp:LinkButton>
                    <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="hbtn"
                        PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                           
                            <dx:ASPxComboBox ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_Change" 
                                AutoPostBack="true" runat="server"
                                CssClass="form-control"></dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Employee</label>
                           
                            <dx:ASPxComboBox ID="ddlEmployee" runat="server" CssClass="form-control"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">

                            </dx:ASPxComboBox>
                            <asp:HiddenField ID="hfAttendanceID" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Attendance Date</label>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate"
                                Format="dd-MMM-yyyy"
                                OnClientDateSelectionChanged="checkDate">
                            </cc1:CalendarExtender>
                            <asp:TextBox ID="txtDate" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"
                                class="form-control"></asp:TextBox>

                        </div>
                        <div class="col-md-4">

                            <asp:RadioButtonList ID="rblTime" runat="server" RepeatDirection="Horizontal" Width="350px">
                                <asp:ListItem Value="0" Text="Time In (HH:MM)"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Time Out (HH:MM)"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-4">
                            <cc1:TimeSelector ID="tsTime" Width="100%" runat="server" 
                                DisplaySeconds="false" SelectedTimeFormat="TwentyFour" EnableTheming="true">
                            </cc1:TimeSelector>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label><span class="fa fa-caret-right rgt_cart"></span>Search</label>
                            <input type="text" id="text" name="n" placeholder="Enter Text To Search " class="form-control" />

                        </div>
                    </div>

                    <div class="row center">
                        <div class="col-md-12">
                            <div class="emp-table">

                                <asp:Repeater ID="rptEmployee" runat="server"
                                    OnItemCommand="rptEmployee_ItemCommand">
                                    <HeaderTemplate>
                                        <table width="100%" class="table table-striped table-bordered table-hover table-condensed cf">
                                            <thead>
                                                <tr>
                                                    <td width="40%">Employee Name
                                                    </td>
                                                    <td width="15%">Date
                                                    </td>
                                                    <td width="15%">Time Of Date
                                                    </td>
                                                    <td width="15%">Time(In/Out)
                                                    </td>
                                                    <td width="15%" colspan="3" align="center">Action
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="table1">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="<%#SetClass(Convert.ToBoolean(Eval("IsLate")))%>">
                                            <td>
                                                <%# Eval("USER_NAME")%>
                                                <asp:HiddenField ID="hfDayofMonth" runat="server" Value='<%# Eval("DayofMonth")%>' />
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "DayofMonth", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("TimeOfDay")%>
                                            </td>
                                            <td>
                                                <%# Eval("TimeInOut")%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnEdit" CommandName="edit" CommandArgument='<%# Eval("AttendanceID")%>' runat="server" ClientIDMode="AutoID" ToolTip="Edit">
                                    Edit    
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnDelete" CommandName="del" CommandArgument='<%# Eval("AttendanceID")%>' runat="server" ClientIDMode="AutoID" ToolTip="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete?');return false;">
                                    Delete
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnRemoveLate" CommandName="remove" CommandArgument='<%# Eval("AttendanceID")%>' runat="server" ClientIDMode="AutoID" ToolTip="Remove Late" OnClientClick="javascript:return confirm('Are you sure you want to remove Late?');return false;">
                                                  <%#GetAction(Convert.ToBoolean(Eval("IsLate")))%>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-offset-2 col-md-2">
                            <div class="btnlist pull-right">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" OnClick="btnDisCard_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
