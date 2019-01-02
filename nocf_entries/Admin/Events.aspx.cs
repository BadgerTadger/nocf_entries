﻿using nocf_entries.cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Admin
{
    public partial class Events : System.Web.UI.Page
    {
        string mode = "";

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


                    mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();

                    switch (mode)
                    {
                        case "a":
                            phList.Visible = false;
                            phEdit.Visible = true;
                            break;
                        case "e":
                            PopulateEditFields(eventID);
                            PopulateShowList(eventID);
                            phList.Visible = false;
                            phEdit.Visible = true;
                            break;
                        default:
                            if (eventID > 0) PopulateEditFields(eventID);
                            PopulateEventList();
                            phList.Visible = true;
                            phEdit.Visible = false;
                            break;
                    }
                }
            }
        }

        private void PopulateViewFields(int eventID)
        {
            throw new NotImplementedException();
        }

        private void PopulateEventList()
        {
            rptrEvents.DataSource = clsEvent.GetUpcomingEventList();
            rptrEvents.DataBind();
        }

        private void PopulateShowList(int eventID)
        {
            clsShow show = new clsShow(eventID);
            rptrShows.DataSource = show.GetShowList();
            rptrShows.DataBind();
        }

        private void PopulateEditFields(int eventID)
        {
            clsEvent showEvent = new clsEvent();
            showEvent.Load(eventID);
            txtEventName.Text = showEvent.EventName;
            chkEventActive.Checked = showEvent.EventActive;
            txtCatalogueCost.Text = showEvent.CatalogueCost.ToString();
            txtPostage.Text = showEvent.Postage.ToString();
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Events?mode=a");
        }

        protected void rptrEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Admin/Events?mode=e&eventid=" + e.CommandName);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int eventID = SaveEvent();
                if (ErrorMessage.Text.Length == 0)
                {
                    Response.Redirect("~/Admin/Events?eventid=" + eventID);
                }
            }
        }

        private int SaveEvent()
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            clsEvent showEvent = new clsEvent();
            showEvent.EventID = eventID;
            showEvent.EventName = txtEventName.Text;
            showEvent.EventActive = chkEventActive.Checked;
            decimal catalogueCost = 0;
            decimal.TryParse(txtCatalogueCost.Text, out catalogueCost);
            showEvent.CatalogueCost = catalogueCost;
            decimal postage = 0;
            decimal.TryParse(txtPostage.Text, out postage);
            showEvent.Postage = postage;
            ErrorMessage.Text = showEvent.Save();
            return showEvent.EventID;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Events");
        }

        protected void rptrShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hdnEventID = e.Item.FindControl("hdnEventID") as HiddenField;
            int eventID = 0;
            int.TryParse(hdnEventID.Value, out eventID);
            HiddenField hdnIsEdit = e.Item.FindControl("hdnIsEdit") as HiddenField;
            if (hdnIsEdit.Value == "true")
            {
                eventID = SaveEvent();
            }
            if (ErrorMessage.Text.Length == 0)
            {
                Response.Redirect("~/Admin/Shows.aspx?eventid=" + eventID + "&showid=" + e.CommandName);
            }
        }

        protected void btnAddShow_Click(object sender, EventArgs e)
        {
            int eventID = SaveEvent();
            if (ErrorMessage.Text.Length == 0)
            {
                Response.Redirect("~/Admin/Shows?eventid=" + eventID + "&mode=a", true);
            }
        }

        protected void rptrEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptrShows = (Repeater)e.Item.FindControl("rptrShows");
                int eventID = 0;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "EventID").ToString(), out eventID);
                PopulateShowList(eventID, rptrShows);
            }
        }

        private void PopulateShowList(int eventID, Repeater rptrShows)
        {
            clsShow show = new clsShow(eventID);
            rptrShows.DataSource = show.GetShowList();
            rptrShows.DataBind();
        }

        protected void rptrShows_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptrClasses = (Repeater)e.Item.FindControl("rptrClasses");
                int showID = 0;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "ShowID").ToString(), out showID);
                PopulateShowClassList(showID, rptrClasses);
            }
        }

        private void PopulateShowClassList(int showID, Repeater rptrClasses)
        {
            rptrClasses.DataSource = clsShowClass.GetSelectedClassesForShow(showID, false);
            rptrClasses.DataBind();
        }

        protected void rptrClasses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hdnEventID = e.Item.FindControl("hdnEventID") as HiddenField;
            int eventID = 0;
            int.TryParse(hdnEventID.Value, out eventID);
            HiddenField hdnShowID = e.Item.FindControl("hdnShowID") as HiddenField;
            int showID = 0;
            int.TryParse(hdnShowID.Value, out showID);
            Response.Redirect("~/Admin/ShowClasses?eventid=" + eventID + "&showid=" + showID + "&showclassid=" + e.CommandName);
        }
    }
}