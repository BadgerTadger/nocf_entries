<%@ Page Title="Show Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shows.aspx.cs" Inherits="nocf_entries.Admin.Shows" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-5">
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <h4>Class List</h4>
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
                                <tr>
                                    <td bgcolor="#CCFFCC">
                                        <asp:Label runat="server" ID="lblClassNameDescription" text='<%# Eval("Class_Name_Description") %>' />
                                    </td>
                                    <td bgcolor="#CCFFCC">
                                        <asp:Label runat="server" ID="lblClassNo" text='<%# Eval("ClassNo") %>' />
                                    </td>
                                    <td bgcolor="#CCFFCC">
                                        <asp:Label runat="server" ID="lblClassCap" text='<%# Eval("ClassCap") %>' />
                                    </td>
                                    <td bgcolor="#CCFFCC">
                                        <asp:Label runat="server" ID="lblJudges" text='<%# Eval("Judges") %>' />
                                    </td>
                                    <td bgcolor="#CCFFCC">
                                        <asp:Button runat="server" ID="btnEditClass" Text="Edit Show Class" UseSubmitBehavior="false" CommandName='<%# Eval("ShowClassID") %>' />
                                    </td>
                                </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                <tr>
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
                                        <asp:Button runat="server" ID="btnEditClass" Text="Edit Show Class" UseSubmitBehavior="false" CommandName='<%# Eval("ShowClassID") %>' />
                                    </td>
                                </tr>
                                </AlternatingItemTemplate>

                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="center-div">
                            <asp:Button runat="server" ID="btnSelectClasses" CssClass="btn btn-default" Text="Select Classes" OnClick="btnSelectClasses_Click" />
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="center-div">
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-default" Text="Edit This Show" OnClick="btnEdit_Click" />
                    <asp:Button runat="server" CausesValidation="false" ID="Button1" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h4>Show Details</h4>
                    <hr />
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtShowName" CssClass="col-md-2 control-label">Show Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtShowName" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtShowName"
                                CssClass="text-danger" ErrorMessage="The Show Name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlShowTypes" CssClass="col-md-2 control-label">* Show Type</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlShowTypes" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="" runat="server" ControlToValidate="ddlShowTypes"
                                CssClass="text-danger" ErrorMessage="The Show Type field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtShowOpens" CssClass="col-md-2 control-label">* Show Opens</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtShowOpens" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtShowOpens"
                                CssClass="text-danger" ErrorMessage="The Show Opens field is required." />
                            <asp:CustomValidator ID="ShowOpensDateFormatValidator" runat="server" ControlToValidate="txtShowOpens" ErrorMessage="Date was in incorrect format" OnServerValidate="DateTimeFormatValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtJudgingCommences" CssClass="col-md-2 control-label">* Judging Commences</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtJudgingCommences" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtJudgingCommences"
                                CssClass="text-danger" ErrorMessage="The Judging Commences field is required." />
                            <asp:CustomValidator ID="JudgingCommencesDateFormatValidator" runat="server" ControlToValidate="txtJudgingCommences" ErrorMessage="Date was in incorrect format" OnServerValidate="DateTimeFormatValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtClosingDate" CssClass="col-md-2 control-label">* Closing Date</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtClosingDate" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtClosingDate"
                                CssClass="text-danger" ErrorMessage="The Closing Date field is required." />
                            <asp:CustomValidator ID="ClosingDateFormatValidator" runat="server" ControlToValidate="txtClosingDate" ErrorMessage="Date was in incorrect format" OnServerValidate="DateFormatValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtMaxClassesPerDog" CssClass="col-md-2 control-label">Max Classes Per Dog</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtMaxClassesPerDog" CssClass="form-control" TextMode="SingleLine" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default" Text="Save" OnClick="btnSave_Click" />&nbsp;
                            <asp:Button runat="server" CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
