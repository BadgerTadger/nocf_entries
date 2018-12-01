<%@ Page Title="Show Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="nocf_entries.Manage.Show" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-6">
                <div class="form-horizontal">
                    <h4>Show Details</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Show Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblShowName" />
                        </dd>
                        <dt>Show Type:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblShowType" />
                        </dd>
                        <dt>Show Opens:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblShowOpens" />
                        </dd>
                        <dt>Judging Commences:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblJudgingCommences" />
                        </dd>
                        <dt>Closing Date:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblClosingDate" />
                        </dd>
                        <dt>Max Classes Per Dog:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblMaxClassesPerDog" />
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-horizontal">
                    <h4>Entry Details</h4>
                    <hr />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="chkCatalogue" CssClass="col-md-6 control-label">Catalogue Required?</asp:Label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkCatalogue" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="chkOvernightCamping" CssClass="col-md-6 control-label">Overnight Camping Required?</asp:Label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkOvernightCamping" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="chkOfferOfHelp" CssClass="col-md-6 control-label">Can you help out at the Show?</asp:Label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkOfferOfHelp" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtHelpDetails" CssClass="col-md-6 control-label">Details of help offered</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtHelpDetails" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="chkWitholdAddress" CssClass="col-md-6 control-label">Withold Address?</asp:Label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkWitholdAddress" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-horizontal">
                    <h4>Entered Classes</h4>
                    <hr />
                    <asp:Repeater ID="rptrEnteredClasses" runat="server" OnItemCommand="rptrEnteredClasses_ItemCommand" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                                <th>Class No</th>
                                <th>Class Name</th>
                                <th>Dog Name</th>
                                <th>Preferred Part</th>
                                <th>Special Request</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrTemplate" : "rptrAltTemplate" %>'>
                                <td>
                                    <asp:Label runat="server" ID="lblClassNo" text='<%# Eval("ClassNo") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblClassNameDescription" text='<%# Eval("Class_Name_Description") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblDogName" text='<%# Eval("KCName") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblPreferredPart" text='<%# Eval("PreferredPart") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblSpecialRequest" text='<%# Eval("SpecialRequest") %>' />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnEditEntry" Text="Edit This Entry" UseSubmitBehavior="false" CommandName='<%# Eval("ShowClassID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-horizontal">
                    <h4>Available Classes</h4>
                    <hr />
                    <asp:Repeater ID="rptrClasses" runat="server" OnItemCommand="rptrClasses_ItemCommand" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                                <th>Class Name</th>
                                <th>Class No</th>
                                <th>Class Cap</th>
                                <th>Judges</th>
                                <th></th>
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
                                    <asp:Label runat="server" ID="lblClassCap" text='<%# Eval("ClassCap") %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblJudges" text='<%# Eval("Judges") %>' />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnEnterClass" Text="Enter This Class" UseSubmitBehavior="false" CommandName='<%# Eval("ShowClassID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="center-div">
                <asp:Button runat="server" ID="Button2" CssClass="btn btn-default" Text="Save" OnClick="btnSave_Click" />&nbsp;
                <asp:Button runat="server" CausesValidation="false" ID="btnBack" CssClass="btn btn-default" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phEnter" Visible="false">
        <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Class Details</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Class Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblClassNameDescription" />
                        </dd>
                        <dt>Class No:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblClassNo" />
                        </dd>
                        <dt>Judges:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblJudges" text='<%# Eval("Judges") %>' />
                        </dd>
                    </dl>
                </div>                
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <h4>Select Dog(s) for this Class</h4>
                            <asp:Repeater ID="rptrDogs" runat="server" OnItemCommand="rptrDogs_ItemCommand" OnItemDataBound="rptrDogs_ItemDataBound" >
                                <HeaderTemplate>
                                    <table>
                                    <tr>
                                        <th>Enter This Dog</th>
                                        <th>Dog Name</th>
                                        <th>Preferred Part</th>
                                        <th>Special Request</th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "rptrTemplate" : "rptrAltTemplate" %>'>
                                        <td>
                                            <asp:CheckBox ID="chkEnterDog" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblKCName" text='<%# Eval("KCName") %>' />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnDogID" runat="server" Value='<%# Eval("DogID") %>' />
                                            <asp:TextBox ID="txtPreferredPart" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSpecialRequest" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="center-div">
                <div class="center-div">
                    <asp:Button runat="server" ID="btnEnterClass" CssClass="btn btn-default" Text="Save" OnClick="btnEnterClass_Click" />&nbsp;
                    <asp:Button runat="server" CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>
</asp:Content>
