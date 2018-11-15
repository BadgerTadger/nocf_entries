using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using nocf_entries.App_Code;
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
            rptrEvents.DataSource = Event.GetEventList(true);
            rptrEvents.DataBind();
        }

        protected void rptrEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptrEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        private void PopulateShowList(int eventID, Repeater rptrShows)
        {
            Show show = new Show(eventID);
            rptrShows.DataSource = show.GetShowList();
            rptrShows.DataBind();
        }

        protected void rptrShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}