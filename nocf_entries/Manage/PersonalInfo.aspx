<%@ Page Title="Personal Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="nocf_entries.Manage.PersonalInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function PhoneValidator_ClientValidate(source, args) {
            if (document.getElementById("<%= txtPhone.ClientID %>").value == "" &&
            document.getElementById("<%= txtMobile.ClientID %>").value == "") {
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
    <p class="text-warning">
        <asp:Literal runat="server">
            PLEASE NOTE<br />
            We use the information that you enter to produce numerous reports and documents (including the show catalogue) and also, on occasion, to contact you.<br />
            Please provide as much information as possible and ensure that the details are entered EXACTLY as you would like them to appear in printed form.<br />
            Pay particular attention to spelling and capitalisation as we cannot accept changes after the closing date for a show when document production has begun.<br />
            Thank you very much.
        </asp:Literal>
    </p>
    <asp:PlaceHolder runat="server" ID="phView" Visible="false">
        <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Your details</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblName" />
                        </dd>
                        <dt>Kennel Club name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblKCRegName" />
                        </dd>
                        <dt>Username:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblUsername" />
                        </dd>
                        <dt>Address:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblAddress" />
                        </dd>
                        <dt>Email:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblEmail" />&nbsp;
                            <asp:HyperLink NavigateUrl="/Account/ManageEmail" Text="[Change]" Visible="true" ID="ChangeEmail" runat="server" />
                        </dd>
                        <dt>Telephone:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblPhone" />
                        </dd>
                        <dt>Mobile:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblMobile" />
                        </dd>
                    </dl>
                </div>
                <div class="center-div">
                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-default" Text="Edit Your Details" OnClick="btnEdit_Click" />
                </div>
            </div>
            <hr />
            <div class="col-md-2">
            </div>
            <div class="col-md-5">
                <div class="form-horizontal">
                    <h4>Your dogs</h4>
                    <asp:Repeater ID="rptrDogs" runat="server" OnItemCommand="rptrDogs_ItemCommand" >
                        <HeaderTemplate>
                            <table>
                            <tr>
                                <th>Pet Name</th>
                                <th>Breed</th>
                                <th>Gender</th>
                                <th>Date of Birth</th>
                                <th></th>
                            </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                        <tr>
                            <td bgcolor="#CCFFCC">
                            <asp:Label runat="server" ID="lblPetName" 
                                text='<%# Eval("PetName") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblBreed" 
                                    text='<%# Eval("Breed") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblGender" 
                                    text='<%# Eval("Gender") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Label runat="server" ID="lblDOB" 
                                    text='<%# Eval("DateOfBirth") %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <asp:Button runat="server" ID="btnEditDog" Text="View/Edit Dog" UseSubmitBehavior="false" CommandName='<%# Eval("DogID") %>' />
                            </td>
                        </tr>
                        </ItemTemplate>

                        <AlternatingItemTemplate>
                        <tr>
                            <td>
                            <asp:Label runat="server" ID="lblPetName" 
                                text='<%# Eval("PetName") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblBreed" 
                                    text='<%# Eval("Breed") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGender" 
                                    text='<%# Eval("Gender") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDOB" 
                                    text='<%# Eval("DateOfBirth") %>' />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnEditDog" Text="View/Edit Dog" UseSubmitBehavior="false" CommandName='<%# Eval("DogID") %>' />
                            </td>
                        </tr>
                        </AlternatingItemTemplate>

                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div>
                    <asp:Button runat="server" ID="btnAddDog" CssClass="btn btn-default" Text="Add a dog" OnClick="btnAddDog_Click" />
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
    </asp:PlaceHolder>


    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="form-horizontal">
            <h4>Update your details</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtTitle" CssClass="col-md-2 control-label">Title</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" TextMode="SingleLine" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 control-label">* First name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                        CssClass="text-danger" ErrorMessage="The First name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 control-label">* Last name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtLastName"
                        CssClass="text-danger" ErrorMessage="The Last name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtKCName" CssClass="col-md-2 control-label">* Kennel Club name</asp:Label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" ID="txtKCName" CssClass="form-control" TextMode="SingleLine" />
                    <asp:Label runat="server">Note: Please enter your name exactly as your registration with the Kennel Club.  This informtaion will be included on the entry form supplied to the Show secretary.<br /></asp:Label>
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtKCName"
                        CssClass="text-danger" ErrorMessage="The Kennel Club name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtAddress1" CssClass="col-md-2 control-label">* Address line 1</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtAddress1" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtAddress1"
                        CssClass="text-danger" ErrorMessage="The Address Line 1 field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtAddress2" CssClass="col-md-2 control-label">Address line 2</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtAddress2" CssClass="form-control" TextMode="SingleLine" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtTown" CssClass="col-md-2 control-label">* Town</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtTown" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtTown"
                        CssClass="text-danger" ErrorMessage="The Town field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtCounty" CssClass="col-md-2 control-label">* County</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtCounty" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtCounty"
                        CssClass="text-danger" ErrorMessage="The County field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPostcode" CssClass="col-md-2 control-label">* Postcode</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtPostcode" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="txtPostcode"
                        CssClass="text-danger" ErrorMessage="The Postcode field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlCountry" CssClass="col-md-2 control-label">* Country</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control" Width="280" />
                    <asp:RequiredFieldValidator Display="Dynamic" InitialValue="Please select..." runat="server" ControlToValidate="ddlCountry"
                        CssClass="text-danger" ErrorMessage="The Country field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPhone" CssClass="col-md-2 control-label">** Phone</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" TextMode="SingleLine" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 control-label">** Mobile</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" TextMode="SingleLine" />
                    <asp:CustomValidator ID="PhoneValidator" Display="Dynamic" runat="server" ClientValidationFunction="PhoneValidator_ClientValidate" OnServerValidate="PhoneValidator_ServerValidate"
                        CssClass="text-danger" ErrorMessage="At least one phone number is required." />
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
