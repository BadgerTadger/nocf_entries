using nocf_entries.App_Code;
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
                    int.TryParse(Request.QueryString["id"], out eventID);


                    mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();

                    switch (mode)
                    {
                        case "a":
                            phList.Visible = false;
                            phEdit.Visible = true;
                            break;
                        case "e":
                            PopulateEditFields(eventID);
                            phList.Visible = false;
                            phEdit.Visible = true;
                            break;
                        default:
                            PopulateEventList();
                            phList.Visible = true;
                            phEdit.Visible = false;
                            break;
                    }
                }
            }
        }

        private void PopulateEventList()
        {
            rptrEvents.DataSource = Event.GetEventList();
            rptrEvents.DataBind();
        }

        private void PopulateEditFields(int eventID)
        {
            Event showEvent = new Event();
            showEvent.Load(eventID);
            txtEventName.Text = showEvent.EventName;
            chkEventActive.Checked = showEvent.EventActive;
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Events?mode=a");
        }

        protected void rptrEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Admin/Events?mode=e&id=" + e.CommandName);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int eventID = 0;
                int.TryParse(Request.QueryString["id"], out eventID);
                Event showEvent = new Event();
                showEvent.EventID = eventID;
                showEvent.EventName = txtEventName.Text;
                showEvent.EventActive = chkEventActive.Checked;
                showEvent.Save();
                Response.Redirect("~/Admin/Events");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Events");
        }

        protected void rptrShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Admin/Shows.aspx?id=" + e.CommandName);
        }

        protected void btnAddShow_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Shows?mode=a", true);
        }
    }
}