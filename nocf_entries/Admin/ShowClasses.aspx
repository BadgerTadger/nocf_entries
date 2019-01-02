<%@ Page Title="Show Classes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowClasses.aspx.cs" Inherits="nocf_entries.Admin.ShowClasses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h4>Show Details</h4>
                    <hr />
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <div class="form-group">
                        <asp:HiddenField ID="hdnClassNameID" runat="server" />
                        <asp:Label runat="server" AssociatedControlID="lblShowClassName" CssClass="col-md-2 control-label">Show Name</asp:Label>
                        <div class="col-md-10">
                            <asp:Label runat="server" ID="lblShowClassName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="lblClassNo" CssClass="col-md-2 control-label">Class No.</asp:Label>
                        <div class="col-md-10">
                            <asp:Label runat="server" ID="lblClassNo" CssClass="form-control"  />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtJudges" CssClass="col-md-2 control-label">Judges</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtJudges" CssClass="form-control" TextMode="SingleLine" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtClassCap" CssClass="col-md-2 control-label">Class Cap</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtClassCap" CssClass="form-control" TextMode="SingleLine" />
                            <asp:CustomValidator ID="ClassCapFormatValidator" runat="server" ControlToValidate="txtClassCap" ErrorMessage="Class Cap must be empty or a number greater than 34" OnServerValidate="ClassCapFormatValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtDogsPerClassPart" CssClass="col-md-2 control-label">Dogs Per Class Part</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDogsPerClassPart" CssClass="form-control" TextMode="SingleLine" />
                            <asp:CustomValidator ID="DogsPerClassPartValidator" runat="server" ControlToValidate="txtDogsPerClassPart" ErrorMessage="Dogs Per Class Part must be empty or a number greater than 0" OnServerValidate="DogsPerClassPartValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtClassCost" CssClass="col-md-2 control-label">Class Cost</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtClassCost" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtClassCost"
                                CssClass="text-danger" ErrorMessage="The Class Cost field is required."
                                ValidationGroup="ChangePassword" />
                            <asp:CustomValidator ID="ClassCostValidator" runat="server" ControlToValidate="txtClassCost" ErrorMessage="Class Cost must be a valid amount" OnServerValidate="ClassCostValidator_ServerValidate" />
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
