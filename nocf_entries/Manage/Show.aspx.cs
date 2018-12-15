using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using nocf_entries.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Manage
{
    public partial class Show : System.Web.UI.Page
    {
        string _mode = "";
        clsOwner _owner = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userid = User.Identity.GetUserId();
            var user = userManager.FindById(userid);
            _owner = new clsOwner(userid, user.UserName, user.Email);
            _owner.Load();
            if (!Page.IsPostBack)
            {
                int eventID = 0;
                int.TryParse(Request.QueryString["eventid"], out eventID);
                int showID = 0;
                int.TryParse(Request.QueryString["showid"], out showID);
                int classID = 0;
                int.TryParse(Request.QueryString["classid"], out classID);

                _mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();
                switch (_mode)
                {
                    case "e":
                        PopulateEnterFields(eventID, showID, classID);
                        phView.Visible = false;
                        phEnter.Visible = true;
                        break;
                    default:
                        PopulateViewFields(eventID, showID);
                        phEnter.Visible = false;
                        phView.Visible = true;
                        break;
                }
            }
        }

        private void PopulateEnterFields(int eventID, int showID, int classID)
        {
            DataTable dt = clsClassName.GetShowClassByID(classID);
            DataRow row = dt.Rows[0];
            lblClassNameDescription.Text = row["Class_Name_Description"].ToString();
            lblClassNo.Text = row["ClassNo"].ToString();
            lblJudges.Text = row["Judges"].ToString();
            clsDog dog = new clsDog(_owner.OwnerID);
            rptrDogs.DataSource = dog.GetDogList();
            rptrDogs.DataBind();
        }

        private void PopulateViewFields(int eventID, int showID)
        {
            clsShow show = new clsShow(eventID);
            show.Load(showID);
            lblShowName.Text = show.ShowName;
            lblShowType.Text = show.ShowTypeDescription;
            lblShowOpens.Text = show.ShowOpens.ToString("dd/MM/yyyy HH:mm");
            lblJudgingCommences.Text = show.JudgingCommences.ToString("dd/MM/yyyy HH:mm");
            lblClosingDate.Text = show.ClosingDate.ToString("dd/MM/yyyy");
            lblMaxClassesPerDog.Text = show.MaxClassesPerDog.ToString();
            LoadShowEntryDetails(showID);
            LoadSelectedClasses();
            LoadEnteredClasses();
        }

        private void LoadEnteredClasses()
        {
            int entryID = 0;
            int.TryParse(Request.QueryString["entryid"], out entryID);
            clsDogClass dogClass = new clsDogClass(entryID);
            rptrEnteredClasses.DataSource = dogClass.GetEnteredClasses();
            rptrEnteredClasses.DataBind();
        }

        private void LoadShowEntryDetails(int showID)
        {
            int eventEntryID = 0;
            int.TryParse(Request.QueryString["evententryid"], out eventEntryID);
            clsEventEntry eventEntry = new clsEventEntry(_owner.OwnerID);
            if (eventEntry.LoadByShowID(showID))
            {
                chkCatalogue.Checked = eventEntry.Catalogue;
                chkOvernightCamping.Checked = eventEntry.OvernightCamping;
                chkOfferOfHelp.Checked = eventEntry.OfferOfHelp;
                txtHelpDetails.Text = eventEntry.VehicleReg;
                chkWitholdAddress.Checked = eventEntry.WitholdAddress;
            }
        }

        private void LoadSelectedClasses()
        {
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            rptrClasses.DataSource = clsClassName.GetSelectedClassesForShow(showID);
            rptrClasses.DataBind();
        }

        protected void rptrClasses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            clsEventEntry entry = new clsEventEntry(_owner.OwnerID);
            int entryID = 0;
            if (entry.LoadByShowID(showID))
            {
                entryID = entry.EventEntryID;
            }
            else
            {
                entryID = SaveEntry(showID);
            }
            Response.Redirect("~/Manage/Show?mode=e&eventid=" + eventID + "&showid=" + showID + "&entryid=" + entryID + "&classid=" + e.CommandName);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manage/Events", true);
        }

        protected void rptrDogs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        private int SaveEntry(int showID)
        {
            int entryID = 0;
            int.TryParse(Request.QueryString["entryid"], out entryID);
            clsEventEntry entry = new clsEventEntry(_owner.OwnerID);
            entry.EventEntryID = entryID;
            entry.ShowID = showID;
            entry.Catalogue = chkCatalogue.Checked;
            entry.OvernightCamping = chkOvernightCamping.Checked;
            entry.OfferOfHelp = chkOfferOfHelp.Checked;
            entry.VehicleReg = txtHelpDetails.Text;
            entry.WitholdAddress = chkWitholdAddress.Checked;
            entry.SendRunningOrder = false;
            entry.EntryDate = DateTime.Now;
            entryID = entry.Save();

            return entryID;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            SaveEntry(showID);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            int entryID = 0;
            int.TryParse(Request.QueryString["entryid"], out entryID);
            Response.Redirect("~/Manage/Show?eventid=" + eventID + "&entryid=" + entryID + "&showid=" + showID);
        }

        private bool PreferredPartValid(TextBox txtPreferredPart)
        {
            bool argsValid = true;
            if (!string.IsNullOrEmpty(txtPreferredPart.Text))
            {
                int preferredPart = 0;
                if (!int.TryParse(txtPreferredPart.Text, out preferredPart))
                {
                    argsValid = false;
                }
            }
            return argsValid;
        }

        protected void btnEnterClass_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            int classID = 0;
            int.TryParse(Request.QueryString["classid"], out classID);
            int entryID = 0;
            int.TryParse(Request.QueryString["entryid"], out entryID);

            clsDogClass.DeleteDogEntriesForClass(entryID, classID);

            foreach (RepeaterItem ri in rptrDogs.Items)
            {
                CheckBox chk = ri.FindControl("chkEnterDog") as CheckBox;
                if (chk.Checked)
                {
                    TextBox txtPreferredPart = ri.FindControl("txtPreferredPart") as TextBox;
                    if (!PreferredPartValid(txtPreferredPart))
                    {
                        ErrorMessage.Text = "Preferred Part must be empty or a valid number";
                        return;
                    }
                    int preferredPart = 0;
                    int.TryParse(txtPreferredPart.Text, out preferredPart);
                    HiddenField hdnDogID = ri.FindControl("hdnDogID") as HiddenField;
                    int dogID = 0;
                    int.TryParse(hdnDogID.Value, out dogID);
                    if (dogID > 0 && eventID > 0 && showID > 0 && classID > 0)
                    {
                        TextBox txtSpecialRequest = ri.FindControl("txtSpecialRequest") as TextBox;
                        clsDogClass dogClass = new clsDogClass(entryID);
                        dogClass.DogID = dogID;
                        dogClass.ShowClassID = classID;
                        dogClass.PreferredPart = preferredPart;
                        dogClass.SpecialRequest = txtSpecialRequest.Text;
                        dogClass.Save();
                    }
                }
            }
            Response.Redirect("~/Manage/Show?eventid=" + eventID + "&entryid=" + entryID + "&showid=" + showID);
        }

        protected void rptrDogs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int classID = 0;
                int.TryParse(Request.QueryString["classid"], out classID);
                int entryID = 0;
                int.TryParse(Request.QueryString["entryid"], out entryID);
                int dogID = 0;
                int.TryParse(((HiddenField)e.Item.FindControl("hdnDogID")).Value, out dogID);

                clsDogClass dogClass = new clsDogClass(entryID);
                dogClass.ShowClassID = classID;
                dogClass.DogID = dogID;
                dogClass.Load();

                if (dogClass.DogClassID > 0)
                {
                    ((CheckBox)e.Item.FindControl("chkEnterDog")).Checked = true;
                    ((TextBox)e.Item.FindControl("txtPreferredPart")).Text = dogClass.PreferredPart.ToString();
                    ((TextBox)e.Item.FindControl("txtSpecialRequest")).Text = dogClass.SpecialRequest;
                }
            }
        }

        protected void rptrEnteredClasses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            clsEventEntry entry = new clsEventEntry(_owner.OwnerID);
            int entryID = 0;
            if (entry.LoadByShowID(showID))
            {
                entryID = entry.EventEntryID;
            }
            else
            {
                entryID = SaveEntry(showID);
            }
            Response.Redirect("~/Manage/Show?mode=e&eventid=" + eventID + "&showid=" + showID + "&entryid=" + entryID + "&classid=" + e.CommandName);
        }
    }
}