using nocf_entries.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Admin
{
    public partial class Shows : System.Web.UI.Page
    {
        string _mode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Admin"))
            {
                Response.Redirect("~/Default");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    int eventID = 0;
                    int.TryParse(Request.QueryString["eventid"], out eventID);
                    int showID = 0;
                    int.TryParse(Request.QueryString["id"], out showID);

                    _mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();
                    switch (_mode)
                    {
                        case "a":
                            LoadShowTypeList();
                            phView.Visible = false;
                            phEdit.Visible = true;
                            break;
                        case "e":
                            LoadShowTypeList();
                            PopulateEditFields(showID);
                            phView.Visible = false;
                            phEdit.Visible = true;
                            break;
                        default:
                            PopulateViewFields(showID);
                            phEdit.Visible = false;
                            phView.Visible = true;
                            break;
                    }
                }
            }
        }

        private void PopulateViewFields(int showID)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            clsShow show = new clsShow(eventID);
            show.Load(showID);
            lblShowName.Text = show.ShowName;
            lblShowType.Text = show.ShowTypeDescription;
            lblShowOpens.Text = show.ShowOpens.ToString("dd/MM/yyyy HH:mm");
            lblJudgingCommences.Text = show.JudgingCommences.ToString("dd/MM/yyyy HH:mm");
            lblClosingDate.Text = show.ClosingDate.ToString("dd/MM/yyyy");
            lblMaxClassesPerDog.Text = show.MaxClassesPerDog.ToString();
            LoadSelectedClasses();
        }

        private void PopulateEditFields(int showID)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            clsShow show = new clsShow(eventID);
            show.Load(showID);
            txtShowName.Text = show.ShowName;
            ddlShowTypes.SelectedValue = show.ShowTypeID.ToString();
            txtShowOpens.Text = show.ShowOpens.ToString("dd/MM/yyyy HH:mm");
            txtJudgingCommences.Text = show.JudgingCommences.ToString("dd/MM/yyyy HH:mm");
            txtClosingDate.Text = show.ClosingDate.ToString("dd/MM/yyyy");
            txtMaxClassesPerDog.Text = show.MaxClassesPerDog.ToString();
        }

        private void LoadShowTypeList()
        {
            List<ShowType> showTypeList = clsShow.GetShowTypeList();

            foreach (ShowType showType in showTypeList)
            {
                ddlShowTypes.Items.Add(new ListItem(showType.ShowTypeDescription, showType.ShowTypeID.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                int eventID = 0;
                int.TryParse(Request.QueryString["eventid"], out eventID);
                int showID = 0;
                int.TryParse(Request.QueryString["id"], out showID);

                clsShow show = new clsShow(eventID);
                show.ShowID = showID;
                show.ShowName = txtShowName.Text;
                show.ShowTypeID = int.Parse(ddlShowTypes.SelectedValue);
                show.ShowOpens = DateTime.ParseExact(txtShowOpens.Text, "dd/MM/yyyy HH:mm", null);
                show.JudgingCommences = DateTime.ParseExact(txtJudgingCommences.Text, "dd/MM/yyyy HH:mm", null);
                show.ClosingDate = DateTime.ParseExact(txtClosingDate.Text, "dd/MM/yyyy", null);
                show.MaxClassesPerDog = int.Parse(txtMaxClassesPerDog.Text);
                show.Save();
                Response.Redirect("~/Admin/Events?mode=e&id=" + eventID);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            Response.Redirect("~/Admin/Events?mode=e&id=" + eventID);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["id"], out showID);
            Response.Redirect("~/Admin/Shows?mode=e&eventid=" + eventID + "&id=" + showID.ToString(), true);
        }

        protected void DateFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime d;
            args.IsValid = DateTime.TryParseExact(args.Value, new[] { "dd/MM/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }

        protected void DateTimeFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime d;
            args.IsValid = DateTime.TryParseExact(args.Value, new[] { "dd/MM/yyyy HH:mm", "yyyy-MM-dd HH:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        }

        private void LoadSelectedClasses()
        {
            int showID = 0;
            int.TryParse(Request.QueryString["id"], out showID);
            rptrClasses.DataSource = clsClassName.GetSelectedClassesForShow(showID);
            rptrClasses.DataBind();
        }

        protected void rptrClasses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["id"], out showID);
            Response.Redirect("~/Admin/ShowClasses?eventid=" + eventID + "&showid=" + showID + "&id=" + e.CommandName);
        }

        protected void btnSelectClasses_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["id"], out showID);
            Response.Redirect("~/Admin/ClassNames?mode=s&eventid=" + eventID + "&showid=" + showID);
        }
    }
}