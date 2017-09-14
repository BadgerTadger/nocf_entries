using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace nocf_entries.Manage
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        string mode = "";
        Owner owner = null;        

        protected void Page_Load(object sender, EventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userid = User.Identity.GetUserId();
            var user = userManager.FindById(userid);
            owner = new Owner(userid, user.UserName, user.Email);
            owner.Load();

            if (!Page.IsPostBack)
            {
                mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();

                if (mode == "e")
                {
                    LoadCountryList();
                    PopulateEditFields();
                    phView.Visible = false;
                    phEdit.Visible = true;
                }
                else
                {
                    PopulateViewFields();
                    phEdit.Visible = false;
                    phView.Visible = true;
                }

            }
        }

        private void PopulateViewFields()
        {
            lblName.Text = BuildName(owner);
            lblKCRegName.Text = owner.KCName;
            lblUsername.Text = owner.UserName;
            lblAddress.Text = BuildAddress(owner);
            lblEmail.Text = owner.Email;
            lblPhone.Text = owner.Phone;
            lblMobile.Text = owner.Mobile;
        }

        private string BuildAddress(Owner owner)
        {
            string retVal = string.Format(@"{0}{1}<br />{2}<br />{3}<br />{4}<br />{5}",
                owner.Address1,
                string.IsNullOrWhiteSpace(owner.Address2) ? "" : "<br />" + owner.Address2,
                owner.Town,
                owner.County,
                owner.Postcode,
                owner.Country);

            return retVal;
        }

        private string BuildName(Owner owner)
        {
            string retVal = string.Format(@"{0}{1} {2}",
                string.IsNullOrWhiteSpace(owner.Title) ? "" : owner.Title + " ", 
                owner.FirstName, 
                owner.LastName);

            return retVal;
        }

        private void PopulateEditFields()
        {
            txtTitle.Text = owner.Title;
            txtFirstName.Text = owner.FirstName;
            txtLastName.Text = owner.LastName;
            txtKCName.Text = owner.KCName;
            txtAddress1.Text = owner.Address1;
            txtAddress2.Text = owner.Address2;
            txtTown.Text = owner.Town;
            txtCounty.Text = owner.County;
            txtPostcode.Text = owner.Postcode;
            ddlCountry.SelectedValue = owner.Country;
            txtPhone.Text = owner.Phone;
            txtMobile.Text = owner.Mobile;
        }

        private void LoadCountryList()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/App_Data/countries.xml"));

            foreach (XmlNode node in doc.SelectNodes("//country"))
            {
                ddlCountry.Items.Add(new ListItem(node.InnerText, node.InnerText));
            }
        }

        public void PhoneValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (txtPhone.Text == "" && txtMobile.Text == "")
            {
                PhoneValidator.ErrorMessage = "At least one phone number is required";
                args.IsValid = false;

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manage/PersonalInfo?mode=e", true);
        }

        protected void btnAddDog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manage/Dogs?mode=a", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manage/PersonalInfo", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                owner.Title = txtTitle.Text;
                owner.FirstName = txtFirstName.Text;
                owner.LastName = txtLastName.Text;
                owner.KCName = txtKCName.Text;
                owner.Address1 = txtAddress1.Text;
                owner.Address2 = txtAddress2.Text;
                owner.Town = txtTown.Text;
                owner.County = txtCounty.Text;
                owner.Postcode = txtPostcode.Text;
                owner.Country = ddlCountry.SelectedValue;
                owner.Phone = txtPhone.Text;
                owner.Mobile = txtMobile.Text;
                owner.Save();
                Response.Redirect("~/Manage/PersonalInfo", true);
            }
        }
    }
}