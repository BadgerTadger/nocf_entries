<%@ Page Title="Manage My Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="nocf_entries.Manage.Events" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h3>My Events</h3>
                    <asp:Repeater ID="rptrMyEvents" runat="server" OnItemDataBound="rptrMyEvents_ItemDataBound" >
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-12 eventName">
                                    <h4><%# Eval("EventName") %></h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-11">
                                    <asp:Repeater ID="rptrMyShows" runat="server" OnItemCommand="rptrShows_ItemCommand" OnItemDataBound="rptrMyShows_ItemDataBound" >
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="row showName">
                                                <div class="col-md-4"><strong>Show Name</strong></div>
                                                <div class="col-md-2"><strong>Show Type</strong></div>
                                                <div class="col-md-2"><strong>Show Opens</strong></div>
                                                <div class="col-md-4"></div>
                                            </div>
                                            <div class="row showName">
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" ID="Label1" text='<%# Eval("ShowName") %>' />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label runat="server" ID="lblShowType" text='<%# Eval("ShowTypeDescription") %>' />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label runat="server" ID="lblShowOpens" text='<%# DateTime.Parse(Eval("ShowOpens").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                    <asp:Button runat="server" ID="btnEditShow" Text="View/Edit Entries" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-11">
                                                    <asp:Repeater ID="rptrMyClasses" runat="server" OnItemCommand="rptrMyClasses_ItemCommand" >
                                                        <HeaderTemplate>
                                                            <table>
                                                            <tr>
                                                                <th>Class Name</th>
                                                                <th>Class No</th>
                                                                <th>Dog Name</th>
                                                                <th>Preferred Part</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrTemplate" : "rptrAltTemplate" %>'>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassNameDescription" text='<%# Eval("Class_Name_Description") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassNo" text='<%# Eval("ClassNo") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblKCName" text='<%# Eval("KCName") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblPreferredPart" text='<%# Eval("PreferredPart") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                            <hr />
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
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
            <div class="col-md-12">
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h3>Available Events</h3>
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
                            <tr class="eventName">
                                <td>
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
                                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrTemplate" : "rptrAltTemplate" %>'>
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
                                                <asp:Button runat="server" ID="btnEditShow" Text="View/Enter Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                                            </td>
                                        </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                            <hr />
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
