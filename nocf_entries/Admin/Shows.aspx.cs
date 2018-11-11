using nocf_entries.App_Code;
using System;
using System.Collections.Generic;
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
                    int _showID = 0;
                    int.TryParse(Request.QueryString["id"], out _showID);

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
                            PopulateEditFields(_showID);
                            phView.Visible = false;
                            phEdit.Visible = true;
                            break;
                        default:
                            PopulateViewFields(_showID);
                            phEdit.Visible = false;
                            phView.Visible = true;
                            break;
                    }
                }
            }
        }

        private void PopulateViewFields(int showID)
        {
            throw new NotImplementedException();
        }

        private void PopulateEditFields(int showID)
        {
            throw new NotImplementedException();
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

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void DateFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}