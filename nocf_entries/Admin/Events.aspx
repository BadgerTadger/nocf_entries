﻿<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="nocf_entries.Admin.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phList" Visible="false">
        <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Event List</h4>
                    <asp:Repeater ID="rptrEvents" runat="server" OnItemCommand="rptrEvents_ItemCommand" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                                <th>Event Name</th>
                                <th>Event Active?</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                        <tr>
                            <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblEventName" text='<%# Eval("EventName") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblEventActive" text='<%# (Eval("EventActive").ToString() == "True" ? "Yes" : "No") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Button runat="server" ID="btnEditEvent" Text="View/Edit Event" UseSubmitBehavior="false" CommandName='<%# Eval("EventID") %>' />
                            </td>
                        </tr>
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblEventName" text='<%# Eval("EventName") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblEventActive" text='<%# (Eval("EventActive").ToString() == "true" ? "Yes" : "No") %>' />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnEditEvent" Text="View/Edit Event" UseSubmitBehavior="false" CommandName='<%# Eval("EventID") %>' />
                            </td>
                        </tr>
                        </AlternatingItemTemplate>

                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="center-div">
                    <asp:Button runat="server" ID="btnAddEvent" CssClass="btn btn-default" Text="Add an Event" OnClick="btnAddEvent_Click" />
                </div>
            </div>
            <hr />
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="form-horizontal">
            <h4>Event details</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEventName" CssClass="col-md-2 control-label">* Event Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtEventName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtEventName"
                        CssClass="text-danger" ErrorMessage="The First name field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" ID="chkEventActive" />
                        <asp:Label runat="server" AssociatedControlID="chkEventActive">Event Active?</asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-horizontal">
                <h4>Show List</h4>
                <asp:Repeater ID="rptrShows" runat="server" OnItemCommand="rptrShows_ItemCommand" >
                    <HeaderTemplate>
                        <table>
                        <tr>
                            <th>Show Name</th>
                            <th>Show Type</th>
                            <th>Show Opens</th>
                            <th></th>
                        </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                    <tr>
                        <td bgcolor="#CCFFCC">
                            <asp:Label runat="server" ID="lblShowName" text='<%# Eval("ShowName") %>' />
                        </td>
                        <td bgcolor="#CCFFCC">
                            <asp:Label runat="server" ID="lblShowType" text='<%# Eval("ShowTypeDescription") %>' />
                        </td>
                        <td bgcolor="#CCFFCC">
                            <asp:Label runat="server" ID="lblShowOpens" text='<%# Eval("ShowOpens") %>' />
                        </td>
                        <td bgcolor="#CCFFCC">
                            <asp:Button runat="server" ID="btnEditShow" Text="View/Edit Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                        </td>
                    </tr>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblShowName" text='<%# Eval("ShowName") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblShowType" text='<%# Eval("ShowTypeDescription") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblShowOpens" text='<%# Eval("ShowOpens") %>' />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnEditEvent" Text="View/Edit Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                        </td>
                    </tr>
                    </AlternatingItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="center-div">
                <asp:Button runat="server" ID="btnAddShow" CssClass="btn btn-default" Text="Add a Show" OnClick="btnAddShow_Click" />
            </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default" Text="Save" OnClick="btnSave_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
    </asp:PlaceHolder>
</asp:Content>