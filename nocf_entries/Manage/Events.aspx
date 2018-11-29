<%@ Page Title="Manage My Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="nocf_entries.Manage.Events" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-10">
                <div class="form-horizontal">
                    <h4>My Events</h4>
                    <asp:Repeater ID="rptrMyEvents" runat="server" OnItemDataBound="rptrMyEvents_ItemDataBound" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                            </tr>
                            <tr>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblEventName" 
                                    text='<%# Eval("EventName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
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
                                                <asp:Label runat="server" ID="lblShowOpens" text='<%# DateTime.Parse(Eval("ShowOpens").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                            </td>
                                            <td bgcolor="#CCFFCC">
                                                <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                <asp:Button runat="server" ID="btnEditShow" Text="View/Enter Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
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
                                                <asp:Label runat="server" ID="lblShowOpens" text='<%# DateTime.Parse(Eval("ShowOpens").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                <asp:Button runat="server" ID="btnEditEvent" Text="View/Enter Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                                            </td>
                                        </tr>
                                        </AlternatingItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div>
<%--                    <asp:Button runat="server" ID="btnAddDog" CssClass="btn btn-default" Text="Add a dog" OnClick="btnAddDog_Click" />--%>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">
                <div class="form-horizontal">
                    <h4>Upcoming Events</h4>
                    <asp:Repeater ID="rptrUpcomingEvents" runat="server" OnItemDataBound="rptrUpcomingEvents_ItemDataBound" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                            </tr>
                            <tr>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblEventName" 
                                    text='<%# Eval("EventName") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
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
                                                <asp:Label runat="server" ID="lblShowOpens" text='<%# DateTime.Parse(Eval("ShowOpens").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                            </td>
                                            <td bgcolor="#CCFFCC">
                                                <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                <asp:Button runat="server" ID="btnEditShow" Text="View/Enter Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
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
                                                <asp:Label runat="server" ID="lblShowOpens" text='<%# DateTime.Parse(Eval("ShowOpens").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                <asp:Button runat="server" ID="btnEditEvent" Text="View/Enter Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                                            </td>
                                        </tr>
                                        </AlternatingItemTemplate>

                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div>
<%--                    <asp:Button runat="server" ID="btnAddDog" CssClass="btn btn-default" Text="Add a dog" OnClick="btnAddDog_Click" />--%>
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>
</asp:Content>
