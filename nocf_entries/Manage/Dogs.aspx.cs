using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using nocf_entries.App_Code;

namespace nocf_entries.Manage
{
    public partial class Dogs : System.Web.UI.Page
    {
        string _mode = "";
        Owner _owner = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userid = User.Identity.GetUserId();
            var user = userManager.FindById(userid);
            _owner = new Owner(userid, user.UserName, user.Email);
            _owner.Load();
            if (!Page.IsPostBack)
            {
                int _dogID = 0;
                int.TryParse(Request.QueryString["id"], out _dogID);

                _mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();
                switch (_mode)
                {
                    case "a":
                        LoadBreedList();
                        LoadGenderList();
                        phView.Visible = false;
                        phEdit.Visible = true;
                        break;
                    case "e":
                        LoadBreedList();
                        LoadGenderList();
                        PopulateEditFields(_dogID);
                        phView.Visible = false;
                        phEdit.Visible = true;
                        break;
                    default:
                        PopulateViewFields(_dogID);
                        phEdit.Visible = false;
                        phView.Visible = true;
                        break;
                }
            }
        }

        private void PopulateEditFields(int dogID)
        {
            Dog _dog = new Dog(_owner.ID);
            _dog.Load(dogID);
            txtPetName.Text = _dog.PetName;
            txtKCName.Text = _dog.KCName;
            txtRegNo.Text = _dog.RegNo;
            txtATCNo.Text = _dog.ATCNo;
            ddlBreeds.SelectedValue = _dog.BreedID.ToString();
            ddlGender.SelectedValue = _dog.GenderID.ToString();
            txtDOB.Text = _dog.DateOfBirth.ToString("dd/MM/yyyy");
            txtDescr.Text = _dog.Descr;
        }

        private void LoadBreedList()
        {
            List<Breed> breedList = Dog.GetBreedList();

            foreach (Breed breed in breedList)
            {
                ddlBreeds.Items.Add(new ListItem(breed.Descr, breed.BreedID));
            }
        }

        private void LoadGenderList()
        {
            ddlGender.Items.Add(new ListItem("Please select...", "0"));
            ddlGender.Items.Add(new ListItem("Dog", "1"));
            ddlGender.Items.Add(new ListItem("Bitch", "2"));
        }

        private void PopulateViewFields(int dogID)
        {
            Dog _dog = new Dog(_owner.ID);
            _dog.Load(dogID);
            lblPetName.Text = _dog.PetName;
            lblKCName.Text = _dog.KCName;
            lblRegNo.Text = _dog.RegNo;
            lblATCNo.Text = _dog.ATCNo;
            lblBreed.Text = _dog.BreedDescr;
            lblGender.Text = _dog.GenderDescr;
            lblDateOfBirth.Text = _dog.DateOfBirth.ToString("dd/MM/yyyy");
            lblDescr.Text = _dog.Descr;
        }

        protected void RegNumberCustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.txtRegNo.Text) && string.IsNullOrEmpty(this.txtATCNo.Text))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int _dogID = 0;
            if (int.TryParse(Request.QueryString["id"], out _dogID))
            {
                Response.Redirect("~/Manage/Dogs?mode=e&id=" + _dogID.ToString(), true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                int _dogID = 0;
                int.TryParse(Request.QueryString["id"], out _dogID);
                Dog _dog = new Dog(_owner.ID);
                _dog.DogID = _dogID;
                _dog.OwnerID = _owner.ID;
                _dog.KCName = txtKCName.Text;
                _dog.PetName = txtPetName.Text;
                _dog.BreedID = int.Parse(ddlBreeds.SelectedValue);
                _dog.GenderID = int.Parse(ddlGender.SelectedValue);
                _dog.RegNo = txtRegNo.Text;
                _dog.ATCNo = txtATCNo.Text;
                _dog.DateOfBirth = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", null);
                _dog.Descr = txtDescr.Text;
                _dog.NLWU = false;
                _dog.Save();
                Response.Redirect("~/Manage/PersonalInfo");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manage/PersonalInfo", true);
        }

        protected void DOBFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime d;
            args.IsValid = DateTime.TryParseExact(args.Value, new[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }
    }
}