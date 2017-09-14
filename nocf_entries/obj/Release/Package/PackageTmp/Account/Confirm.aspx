<%@ Page Title="Account Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="nocf_entries.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successLoggedOutPanel" ViewStateMode="Disabled" Visible="false">
            <p>
                Thank you for confirming your email. Click <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">here</asp:HyperLink>  to login             
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="successLoggedInPanel" ViewStateMode="Disabled" Visible="false">
            <p>
                Thank you for confirming your email. Click <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/">here</asp:HyperLink>  to continue             
            </p>
        </asp:PlaceHolder>
       <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                An error has occurred.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
