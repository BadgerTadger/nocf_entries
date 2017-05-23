using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Manage
{
    public partial class Owner : System.Web.UI.Page
    {
        string mode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();

                if (mode == "e")
                {
                    phView.Visible = false;
                    phEdit.Visible = true;
                }
                else
                {
                    phEdit.Visible = false;
                    phView.Visible = true;
                }
                    }
        }
    }
}