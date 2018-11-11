<%@ Page Title="Admin Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="nocf_entries.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="row">
        <div class="col-md-5">
            <ul>
                <li><a href="ClassNames.aspx">Class Names</a></li>
                <li><a href="Events.aspx">Events</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
