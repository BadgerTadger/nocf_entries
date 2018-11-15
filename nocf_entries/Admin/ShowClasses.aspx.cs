using nocf_entries.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nocf_entries.Admin
{
    public partial class ShowClasses : System.Web.UI.Page
    {
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
                    int showClassID = 0;
                    int.TryParse(Request.QueryString["id"], out showClassID);

                    PopulateEditFields(showClassID);
                    phEdit.Visible = true;
                }
            }
        }

        private void PopulateEditFields(int showClassID)
        {
            DataTable dt = ClassName.GetShowClassByID(showClassID);
            DataRow row = dt.Rows[0];
            hdnClassNameID.Value = row["Class_Name_ID"].ToString();
            lblShowClassName.Text = row["Class_Name_Description"].ToString();
            hdnGender.Value = row["Gender"].ToString();
            lblGenderDescr.Text = row["GenderDescr"].ToString();
            lblClassNo.Text = row["ClassNo"].ToString();
            txtJudges.Text = row["Judges"].ToString();
            int classCap = 0;
            int.TryParse(row["ClassCap"].ToString(), out classCap);
            txtClassCap.Text = classCap == 0 ? "No Limit" : classCap.ToString();
        }

        protected void ClassCapFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool argsValid = true;
            if(!string.IsNullOrEmpty(args.Value) && args.Value != "No Limit")
            {
                int classCap = 0;
                if (int.TryParse(args.Value, out classCap))
                {
                    argsValid = classCap >= 35;
                }
            }
            args.IsValid = argsValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int showID = 0;
                int.TryParse(Request.QueryString["showid"], out showID);
                int showClassID = 0;
                int.TryParse(Request.QueryString["id"], out showClassID);
                ShowClass sc = new ShowClass(showClassID);
                sc.ShowID = showID;
                sc.ClassNameID = int.Parse(hdnClassNameID.Value);
                sc.ClassNo = int.Parse(lblClassNo.Text);
                sc.GenderID = int.Parse(hdnGender.Value);
                int classCap = 0;
                int.TryParse(txtClassCap.Text, out classCap);
                sc.ClassCap = classCap;
                sc.Judges = txtJudges.Text;
                sc.Save();

                int eventID = 0;
                int.TryParse(Request.QueryString["eventid"], out eventID);
                Response.Redirect("~/Admin/Shows?eventid=" + eventID + "&id=" + showID, true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            Response.Redirect("~/Admin/Shows?eventid=" + eventID + "&id=" + showID, true);
        }
    }
}