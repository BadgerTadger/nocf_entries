﻿using nocf_entries.App_Code;
using System;
using System.Collections.Generic;
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
        Event showEvent = null;

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
            Show show = new Show(eventID);
            show.Load(showID);
            lblShowName.Text = show.ShowName;
            lblShowType.Text = show.ShowTypeDescription;
            lblShowOpens.Text = show.ShowOpens.ToString("dd/MM/yyyy");
            lblJudgingCommences.Text = show.JudgingCommences.ToString("dd/MM/yyyy");
            lblClosingDate.Text = show.ClosingDate.ToString("dd/MM/yyyy");
            lblMaxClassesPerDog.Text = show.MaxClassesPerDog.ToString();
        }

        private void PopulateEditFields(int showID)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            Show show = new Show(eventID);
            show.Load(showID);
            txtShowName.Text = show.ShowName;
            ddlShowTypes.SelectedValue = show.ShowTypeID.ToString();
            txtShowOpens.Text = show.ShowOpens.ToString("dd/MM/yyyy");
            txtJudgingCommences.Text = show.JudgingCommences.ToString("dd/MM/yyyy");
            txtClosingDate.Text = show.ClosingDate.ToString("dd/MM/yyyy");
            txtMaxClassesPerDog.Text = show.MaxClassesPerDog.ToString();
        }

        private void LoadShowTypeList()
        {
            List<ShowType> showTypeList = Show.GetShowTypeList();

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

                Show show = new Show(eventID);
                show.ShowID = showID;
                show.ShowName = txtShowName.Text;
                show.ShowTypeID = int.Parse(ddlShowTypes.SelectedValue);
                show.ShowOpens = DateTime.ParseExact(txtShowOpens.Text, "dd/MM/yyyy", null);
                show.JudgingCommences = DateTime.ParseExact(txtJudgingCommences.Text, "dd/MM/yyyy", null);
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
    }
}