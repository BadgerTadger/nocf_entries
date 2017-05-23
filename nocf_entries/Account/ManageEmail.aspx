<%@ Page Title="Manage Email" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="ManageEmail.aspx.cs" Inherits="nocf_entries.Account.ManageEmail" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Update your email</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Current email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="NewEmail" CssClass="col-md-2 control-label">New email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="NewEmail" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewEmail"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmEmail" CssClass="col-md-2 control-label">Confirm new email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmEmail" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmEmail"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm email field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="NewEmail" ControlToValidate="ConfirmEmail"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The email and confirmation email do not match." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="UpdateEmail_Click" Text="Update Email" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
