<%@ Page Title="CORN :: Employee Salary" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmEmployeeSalary.aspx.cs" Inherits="Forms_frmEmployeeSalary"  %>
   
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script language="javascript" type="text/javascript">

  
    function ValidateForm() {
        var str;

        str = document.getElementById('<%=txtBasicSalary.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Basic Salary Amount');
            return false;
        }
     
        return true;
    }
</script>
 
 <div class="main-contents">
      <div class="container employee-infomation">
        <asp:UpdatePanel ID="pnl_head" runat="server">
            <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                        <label ID="lbldesignationID"><span class="fa fa-caret-right rgt_cart"></span>Location</label>
                        <asp:DropDownList ID="DrpDistributor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                                </div>
                <div class="row">
                            <div class="col-md-4">
                        <label ><span class="fa fa-caret-right rgt_cart"></span>Employee Name</label>
                        <asp:DropDownList ID="DrpEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpEmployee_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                            </div>
                    <div class="col-md-4" Style="margin-top:30px; margin-left:-20px">
                        <asp:Label ID="lbl_Designation" runat="server" Font-Bold="False" Font-Size="17px" ></asp:Label>
                            <asp:HiddenField ID="hdnDesignationID" runat="server" Visible="false" />
                             <asp:HiddenField ID="hdnSalaryID" runat="server" Visible="false" />
                    </div>
                                </div>
                <div class="row">
                    <div class="col-md-4">
                     <label><span class="fa fa-caret-right rgt_cart"></span>Basic Salary</label>
                    <asp:TextBox ID="txtBasicSalary" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
                    </div>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="pnl_Allowances" runat="server" Height="200px">
                                <fieldset>
                                    <legend align ="left" >Allowances</legend>
                                    <table>
                                        <tr class="tblhead" >
                                            <td style="padding:2px;">
                                                <asp:Label ID="lblAllowanceDesc" runat="server" Text="Allowance Description" ForeColor="White"
                                                    CssClass="lblbox" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td  style="padding:2px;">
                                                <asp:Label ID="Label2" runat="server" Text="Amount" ForeColor="White" CssClass="lblbox" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DrpAllowance" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpAllowance_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAllowanceAmount" runat="server" Text="" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd_Allowance" runat="server" Text="Add" CssClass="Button" Width="80px"
                                                    OnClick="btnAdd_Allowance_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="Grid_Allowances" runat="server" Width="100%" ForeColor="SteelBlue"
                                                    CssClass="gridRow2" BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                    BackColor="White" OnRowEditing="Grid_Allowances_RowEditing" OnRowDeleting="Grid_Allowances_RowDeleting"
                                                    PageSize="15" 
                                                    onselectedindexchanged="Grid_Allowances_SelectedIndexChanged">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous"></PagerSettings>
                                                    <Columns>
                                                        <asp:BoundField DataField="ALLOWANCE_ID" HeaderText="ALLOWANCE ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ALLOWANCE_DESC" HeaderText="ALLOWANCE DESC">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="46%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ALLOWANCE_AMOUNT" HeaderText="ALLOWANCE AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="35%" ></ItemStyle>
                                                        </asp:BoundField>
                                                    
                                                        <asp:TemplateField >
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit"
                                                                CommandName="Edit" ToolTip="Edit" CssClass="grdButton"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="8%" 
                                                            HorizontalAlign="Center" CssClass="grdButton">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    CommandName="Delete" ToolTip="Delete" CssClass="grdButton"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%"
                                                             HorizontalAlign="Center" CssClass="grdButton">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="pnl_Deductions" runat="server" Height="200px">
                                <fieldset>
                                    <legend align ="left">Deductions</legend>
                                    <table>
                                        <tr class="tblhead">
                                            <td  style="padding:2px;">
                                                <asp:Label ID="lblDeductionDesc" runat="server" Text="Deduction Description" ForeColor="White"
                                                    CssClass="lblbox" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td  style="padding:2px;">
                                                <asp:Label ID="lblDeductionAmount" runat="server" Text="Amount" ForeColor="White"
                                                    CssClass="lblbox" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DrpDeduction" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDeduction_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDeductionAmount" runat="server" Text="" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd_Deduction" runat="server" Text="Add" CssClass="Button" Width="80px"
                                                    OnClick="btnAdd_Deduction_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="Grid_Deductions" runat="server"
                                                    CssClass="gridRow2" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                    OnRowEditing="Grid_Deductions_RowEditing" OnRowDeleting="Grid_Deductions_RowDeleting"
                                                    AllowPaging="false">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous"></PagerSettings>
                                                    <Columns>
                                                        <asp:BoundField DataField="DEDUCTION_ID" HeaderText="DEDUCTION ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DEDUCTION_DESC" HeaderText="DEDUCTION DESC">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="46%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DEDUCTION_AMOUNT" HeaderText="DEDUCTION AMOUNT">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="35%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField >
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit2" runat="server" Text="Edit"
                                                                CommandName="Edit" ToolTip="Edit" CssClass="grdButton"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="8%" HorizontalAlign="Center"
                                                            CssClass="grdButton">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete2" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    CommandName="Delete" ToolTip="Delete" CssClass="grdButton"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="15%" HorizontalAlign="Center"
                                                            CssClass="grdButton">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" Width="90px"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     </div>
</asp:Content>
