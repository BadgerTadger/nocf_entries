<%@ Page Title="Class Names" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassNames.aspx.cs" Inherits="nocf_entries.Admin.ClassNames" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Current Classes</h4>
                    <asp:Repeater ID="rptrClassNames" runat="server" OnItemCommand="rptrClassNames_ItemCommand" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                                <th>Class Name</th>
                                <th>Weighting</th>
                                <th>Gender</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrTemplate" : "rptrAltTemplate" %>'>
                                <td>
                                <asp:Label runat="server" ID="lblClassName" 
                                    text='<%# Eval("Class_Name_Description") %>' />
                                </td>
                                <td>
                                <asp:Label runat="server" ID="lblWeighting" 
                                    text='<%# Eval("Weighting") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblGender" text='<%# Eval("GenderDescr") %>' />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnEditClass" Text="View/Edit Class" UseSubmitBehavior="false" CommandName='<%# Eval("Class_Name_ID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div>
                    <asp:Button runat="server" ID="btnAddClassName" CssClass="btn btn-default" Text="Add a Class Name" OnClick="btnAddClassName_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="form-horizontal">
            <h4>Class Details</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtClassName" CssClass="col-md-2 control-label">* Class name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtClassName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtClassName"
                        CssClass="text-danger" ErrorMessage="The Class Name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlGender" CssClass="col-md-2 control-label">* Gender</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator Display="Dynamic" InitialValue="Please select..." runat="server" ControlToValidate="ddlGender"
                        CssClass="text-danger" ErrorMessage="The Gender field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtWeighting" CssClass="col-md-2 control-label">Weighting</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtWeighting" CssClass="form-control" TextMode="SingleLine" />
                    <asp:CustomValidator ID="ClassWeightingFormatValidator" runat="server" ControlToValidate="txtWeighting" ErrorMessage="Weighting must be empty or a number" OnServerValidate="ClassWeightingFormatValidator_ServerValidate" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default" Text="Save" OnClick="btnSave_Click" />&nbsp;
                    <asp:Button runat="server" CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phSelect" Visible="false">
        <div class="row">
            <div class="col-md-8">
                <div class="form-horizontal">
                    <h4>Select Show Classes</h4>
                    <asp:Repeater ID="rptrClasses" runat="server" OnItemDataBound="rptrClasses_ItemDataBound">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <th>Class Name</th>
                                <th>Class Number</th>
                                <th></th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkClassName" Text='<%# Eval("Class_Name_Description") %>' Checked='<%# !string.IsNullOrEmpty(Eval("ShowClassID").ToString()) %>' runat="server" />
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnShowClassID" Value='<%# Eval("ShowClassID") %>' runat="server" />
                                    <asp:HiddenField ID="hdnClassNameID" Value='<%# Eval("Class_Name_ID") %>' runat="server" />
                                    <asp:HiddenField ID="hdnDefaultClassCost" Value='<%# Eval("DefaultClassCost") %>' runat="server" />
                                    <asp:HiddenField ID="hdnDefaultClassCap" Value='<%# Eval("DefaultClassCap") %>' runat="server" />
                                    <asp:HiddenField ID="hdnDefaultDogsPerClassPart" Value='<%# Eval("DefaultDogsPerClassPart") %>' runat="server" />
                                    <asp:TextBox ID="txtClassNo" Text='<%# Eval("ClassNo") %>' runat="server" /><br />
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div>
                    <asp:Button runat="server" ID="btnSaveSelection" CssClass="btn btn-default" Text="Save Class Selection to Show" OnClick="btnSaveSelection_Click" />
                    <asp:Button runat="server" ID="btnCancelSelection" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancelSelection_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>
</asp:Content>
