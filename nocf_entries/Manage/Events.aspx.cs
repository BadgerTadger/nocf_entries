using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using nocf_entries.cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Manage
{
    public partial class Events : System.Web.UI.Page
    {
        string mode = "";
        clsOwner _owner = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userid = User.Identity.GetUserId();
            var user = userManager.FindById(userid);
            _owner = new clsOwner(userid, user.UserName, user.Email);
            _owner.Load();

            if (!Page.IsPostBack)
            {
                mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();

                if (mode == "e")
                {
                    //LoadCountryList();
                    //PopulateEditFields();
                    //phView.Visible = false;
                    //phEdit.Visible = true;
                }
                else
                {
                    PopulateViewFields(userid);
                    //phEdit.Visible = false;
                    phView.Visible = true;
                }

            }
        }

        private void PopulateViewFields(string userid)
        {
            rptrMyEvents.DataSource = clsEvent.GetMyEventList(_owner.OwnerID);
            rptrMyEvents.DataBind();
            rptrUpcomingEvents.DataSource = clsEvent.GetUpcomingEventList(true);
            rptrUpcomingEvents.DataBind();
        }

        protected void rptrUpcomingEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptrShows =  (Repeater)e.Item.FindControl("rptrShows");
                int eventID = 0;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "EventID").ToString(), out eventID);
                PopulateShowList(eventID, rptrShows);
            }
        }

        private void PopulateMyShowList(int eventID, Repeater rptrMyShows)
        {
            clsShow show = new clsShow(eventID);
            rptrMyShows.DataSource = show.GetMyShowList(_owner.OwnerID);
            rptrMyShows.DataBind();
        }

        private void PopulateShowList(int eventID, Repeater rptrShows)
        {
            clsShow show = new clsShow(eventID);
            rptrShows.DataSource = show.GetShowList();
            rptrShows.DataBind();
        }

        protected void rptrShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hdnEventID = e.Item.FindControl("hdnEventID") as HiddenField;
            int eventID = 0;
            int.TryParse(hdnEventID.Value, out eventID);
            clsEventEntry eventEntry = new clsEventEntry(_owner.OwnerID);
            int eventEntryID = 0;
            if (eventEntry.LoadByEventID(int.Parse(e.CommandName)))
            {
                eventEntryID = eventEntry.EventEntryID;
            }
            Response.Redirect("~/Manage/Show?eventid=" + eventID + "&evententryid=" + eventEntryID + "&showid=" + e.CommandName);
        }

        protected void rptrMyEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptrMyShows = (Repeater)e.Item.FindControl("rptrMyShows");
                int eventID = 0;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "EventID").ToString(), out eventID);
                PopulateMyShowList(eventID, rptrMyShows);
            }
        }

        protected void rptrMyShows_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptrMyClasses = (Repeater)e.Item.FindControl("rptrMyClasses");
                int showID = 0;
                int.TryParse(DataBinder.Eval(e.Item.DataItem, "ShowID").ToString(), out showID);
                PopulateMyClassList(showID, rptrMyClasses);
            }
        }

        private void PopulateMyClassList(int showID, Repeater rptrMyClasses)
        {
            int eventEntryID = 0;
            clsEventEntry eventEntry = new clsEventEntry(_owner.OwnerID);
            if (eventEntry.LoadByShowID(showID))
            {
                eventEntryID = eventEntry.EventEntryID;
            }
            clsDogClass dogClass = new clsDogClass(eventEntryID);
            rptrMyClasses.DataSource = dogClass.GetEnteredClasses();
            rptrMyClasses.DataBind();
        }

        protected void rptrMyClasses_ItemCommand(object source, RepeaterCommandEventArgs e)
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
            Response.Redirect("~/Manage/Show?mode=e&eventid=" + eventID + "&showid=" + showID + "&entryid=" + entryID + "&classid=" + e.CommandName);
        }
    }
}