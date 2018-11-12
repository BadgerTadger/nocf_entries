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
                                <th></th>
                            </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                        <tr>
                            <td bgcolor="#CCFFCC">
                            <asp:Label runat="server" ID="lblClassName" 
                                text='<%# Eval("Class_Name_Description") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Button runat="server" ID="btnEditClass" Text="View/Edit Class" UseSubmitBehavior="false" CommandName='<%# Eval("Class_Name_ID") %>' />
                            </td>
                        </tr>
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                        <tr>
                            <td>
                            <asp:Label runat="server" ID="lblClassName" 
                                text='<%# Eval("Class_Name_Description") %>' />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnEditClass" Text="View/Edit Class" UseSubmitBehavior="false" CommandName='<%# Eval("Class_Name_ID") %>' />
                            </td>
                        </tr>
                        </AlternatingItemTemplate>

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
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default" Text="Save" OnClick="btnSave_Click" />&nbsp;
                    <asp:Button runat="server" CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
    </asp:PlaceHolder>
</asp:Content>
