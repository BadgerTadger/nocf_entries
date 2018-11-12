<%@ Page Title="Dog Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dogs.aspx.cs" Inherits="nocf_entries.Manage.Dogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function CheckRegNumbers(sender, args) {
            if ($('#txtRegNo').val() == ""
                && $('#txtATCNo').val() == "") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
    </script>
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Dog Details</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Pet Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblPetName" />
                        </dd>
                        <dt>Kennel Club Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblKCName" />
                        </dd>
                        <dt>KC Reg. Number:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblRegNo" />
                        </dd>
                        <dt>KC ATC Number:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblATCNo" />
                        </dd>
                        <dt>Breed:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblBreed" />
                        </dd>
                        <dt>Gender:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblGender" />
                        </dd>
                        <dt>Date of Birth:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblDateOfBirth" />
                        </dd>
                        <dt>Note:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblDescr" />
                        </dd>
                    </dl>
                </div>
                <div class="center-div">
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-default" Text="Edit This Dog" OnClick="btnEdit_Click" />
                    <asp:Button runat="server" CausesValidation="false" ID="Button1" CssClass="btn btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="row">
            <div class="col-md-10">
                <div class="form-horizontal">
                    <h4>Dog Details</h4>
                    <hr />
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtPetName" CssClass="col-md-2 control-label">Pet Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtPetName" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtPetName"
                                CssClass="text-danger" ErrorMessage="The Pet Name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtKCName" CssClass="col-md-2 control-label">* Dog Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtKCName" CssClass="form-control" TextMode="SingleLine" />
                            <asp:Label runat="server">The official registration name of your dog.<br /></asp:Label>
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtKCName"
                                CssClass="text-danger" ErrorMessage="The Kennel Club Registered Name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtRegNo" CssClass="col-md-2 control-label">* KC Registration Number</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtRegNo" CssClass="form-control" TextMode="SingleLine" />
                            <asp:Label runat="server">Compulsory: The Kennel Club requires that your dog is registered to enter a show. UK residents please enter your KC Registration Number above. If you are a non-UK resident, please enter your ATC Number below.<br /></asp:Label>
                            <asp:CustomValidator ID="RegNumberCustomValidator1" runat="server" Display="None" ValidateEmptyText="true"
                                ClientValidationFunction="CheckRegNumbers" ErrorMessage="Either KC Registration Number or ATC Number must be provided" ControlToValidate="txtRegNo"
                                OnServerValidate="RegNumberCustomValidator2_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtATCNo" CssClass="col-md-2 control-label">* ATC Number</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtATCNo" CssClass="form-control" TextMode="SingleLine" />
                            <asp:CustomValidator ID="RegNumberCustomValidator2" runat="server" Display="None" ValidateEmptyText="true"
                                ClientValidationFunction="CheckRegNumbers" ErrorMessage="" ControlToValidate="txtATCNo"
                                OnServerValidate="RegNumberCustomValidator2_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlBreeds" CssClass="col-md-2 control-label">* Breed</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlBreeds" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="Please select..." runat="server" ControlToValidate="ddlBreeds"
                                CssClass="text-danger" ErrorMessage="The Breed field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlGender" CssClass="col-md-2 control-label">* Gender</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="Please select..." runat="server" ControlToValidate="ddlGender"
                                CssClass="text-danger" ErrorMessage="The Gender field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtDOB" CssClass="col-md-2 control-label">* Date of Birth</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDOB" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtDOB"
                                CssClass="text-danger" ErrorMessage="The Date of Birth field is required." />
                            <asp:CustomValidator ID="DOBFormatValidator" runat="server" ControlToValidate="txtDOB" ErrorMessage="Date was in incorrect format" OnServerValidate="DOBFormatValidator_ServerValidate" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtDescr" CssClass="col-md-2 control-label">Notes</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDescr" CssClass="form-control" TextMode="MultiLine" Rows="2" />
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
