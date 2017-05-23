<%@ Page Title="Owner Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Owner.aspx.cs" Inherits="nocf_entries.Manage.Owner" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
            <div class="col-md-12">
                <div class="form-horizontal">
                    <h4>View your details</h4>
                    <hr />
                    <dl class="dl-horizontal">
                        <dt>Name:</dt>
                        <dd>
                            <asp:Label runat="server" ID="lblName" />
                        </dd>
                        <dt>KC Registration:</dt>
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
                            <asp:Label runat="server" ID="lblEmail" />
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
            </div>
        </div>
    </asp:PlaceHolder>


    <asp:PlaceHolder runat="server" ID="phEdit" Visible="false">
        <div class="form-horizontal">
            <h4>Update your details</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlTitle" CssClass="col-md-2 control-label">Title</asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList runat="server" ID="ddlTitle">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTitle" InitialValue="Please Select..."
                        CssClass="text-danger" ErrorMessage="The title field is required." />
                </div>
            </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 control-label">First name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
                <asp:RegularExpressionValidator
            </div>
        </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
