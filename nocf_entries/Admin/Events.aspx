<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="nocf_entries.Admin.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phList" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h4>Event List</h4>
                    <asp:Repeater ID="rptrEvents" runat="server" OnItemCommand="rptrEvents_ItemCommand" OnItemDataBound="rptrEvents_ItemDataBound" >
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th>Event Name</th>
                                <th>Event Active?</th>
                                <th>Catalogue Cost</th>
                                <th>Postage</th>
                                <th></th>
                            </tr>
                            <tr class="eventName">
                                <td>
                                    <asp:Label runat="server" ID="lblEventName" text='<%# Eval("EventName") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblEventActive" text='<%# (Eval("EventActive").ToString() == "True" ? "Yes" : "No") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblCatalogueCost" text='<%# (Eval("CatalogueCost").ToString()) %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblPostage" text='<%# (Eval("Postage").ToString()) %>' />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnEditEvent" Text="View/Edit Event" UseSubmitBehavior="false" CommandName='<%# Eval("EventID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="5">
                                    <asp:Repeater ID="rptrShows" runat="server" OnItemCommand="rptrShows_ItemCommand" OnItemDataBound="rptrShows_ItemDataBound" >
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
                                                    <asp:HiddenField ID="hdnIsEdit" runat="server" Value="false" />
                                                    <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                    <asp:Button runat="server" ID="btnEditShow" Text="View/Edit Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="4">
                                                    <asp:Repeater ID="rptrClasses" runat="server" OnItemCommand="rptrClasses_ItemCommand" >
                                                        <HeaderTemplate>
                                                            <table>
                                                            <tr>
                                                                <th>Class Name</th>
                                                                <th>Class No</th>
                                                                <th>Class Cap</th>
                                                                <th>Dogs Per Class Part</th>
                                                                <th>Judges</th>
                                                                <th>Class Cost</th>
                                                                <th></th>
                                                            </tr>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrLevel2Template" : "rptrLevel2AltTemplate" %>'>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassNameDescription" text='<%# Eval("Class_Name_Description") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassNo" text='<%# Eval("ClassNo") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassCap" text='<%# Eval("ClassCap") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblDogsPerClassPart" text='<%# Eval("DogsPerClassPart") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblJudges" text='<%# Eval("Judges") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblClassCost" text='<%# Eval("ClassCost") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                                                    <asp:HiddenField ID="hdnShowID" runat="server" Value='<%# Eval("ShowID") %>' />
                                                                    <asp:Button runat="server" ID="btnEditClass" Text="Edit Show Class" UseSubmitBehavior="false" CommandName='<%# Eval("ShowClassID") %>' />
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
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="center-div">
                    <br />
                    <asp:Button runat="server" ID="btnAddEvent" CssClass="btn btn-default" Text="Add an Event" OnClick="btnAddEvent_Click" />
                </div>
            </div>
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
                        CssClass="text-danger" ErrorMessage="The Event Name field is required." />
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
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtCatalogueCost" CssClass="col-md-2 control-label">* Catalogue Cost</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtCatalogueCost" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtCatalogueCost"
                        CssClass="text-danger" ErrorMessage="The Catalogue Cost field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPostage" CssClass="col-md-2 control-label">* Postage</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtPostage" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtPostage"
                        CssClass="text-danger" ErrorMessage="The Postage field is required." />
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
                                <asp:HiddenField ID="hdnIsEdit" runat="server" Value="true" />
                                <asp:HiddenField ID="hdnEventID" runat="server" Value='<%# Eval("EventID") %>' />
                                <asp:Button runat="server" ID="btnEditShow" Text="View/Edit Show" UseSubmitBehavior="false" CommandName='<%# Eval("ShowID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
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
                    <asp:Button runat="server" CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
    </asp:PlaceHolder>
</asp:Content>
