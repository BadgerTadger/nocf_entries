<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="nocf_entries._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>National Obedience Class Finals</h1>
        <p class="lead">The Kennel Club Building, Stoneleigh</p>
        <%--        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <h2>Getting started</h2>
            <asp:LoginView runat="server" ViewStateMode="Disabled">
                <AnonymousTemplate>
                    <p>
                        Login to manage your details and to enter the show
                    </p>
                    <p>
                        <a runat="server" class="btn btn-default" href="~/Account/Login">Login</a>
                    </p>
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <ul>
                        <li><a runat="server" href="~/Manage/PersonalInfo" title="Manage your personal information">Manage your personal information</a></li>
                        <li><a runat="server" href="~/Manage/Events" title="Manage your personal information">Manage your Events</a></li>
                        <li><a runat="server" href="~/Account/Manage" title="Manage your account information">Manage your account information</a></li>
                        <li>
                            <asp:LoginStatus runat="server" LogoutAction="Redirect" LogoutText="Log off" LogoutPageUrl="~/" OnLoggingOut="Unnamed_LoggingOut" />
                        </li>
                    </ul>
                </LoggedInTemplate>
            </asp:LoginView>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
