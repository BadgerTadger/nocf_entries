using nocf_entries.cls;
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
                    int.TryParse(Request.QueryString["showclassid"], out showClassID);

                    PopulateEditFields(showClassID);
                    phEdit.Visible = true;
                }
            }
        }

        private void PopulateEditFields(int showClassID)
        {
            DataTable dt = clsShowClass.GetShowClassByID(showClassID);
            DataRow row = dt.Rows[0];
            hdnClassNameID.Value = row["Class_Name_ID"].ToString();
            lblShowClassName.Text = row["Class_Name_Description"].ToString();
            lblClassNo.Text = row["ClassNo"].ToString();
            txtJudges.Text = row["Judges"].ToString();
            int classCap = 0;
            int.TryParse(row["ClassCap"].ToString(), out classCap);
            txtClassCap.Text = classCap == 0 ? "No Limit" : classCap.ToString();
            int dogsPerClassPart = 0;
            int.TryParse(row["DogsPerClassPart"].ToString(), out dogsPerClassPart);
            txtDogsPerClassPart.Text = dogsPerClassPart == 0 ? "Not Set" : dogsPerClassPart.ToString();
            txtClassCost.Text = row["ClassCost"].ToString();
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
                int.TryParse(Request.QueryString["showclassid"], out showClassID);
                clsShowClass sc = new clsShowClass(showClassID);
                sc.ShowID = showID;
                sc.ClassNameID = int.Parse(hdnClassNameID.Value);
                sc.ClassNo = int.Parse(lblClassNo.Text);
                int classCap = 0;
                int.TryParse(txtClassCap.Text, out classCap);
                sc.ClassCap = classCap;
                int dogsPerClassPart = 0;
                int.TryParse(txtDogsPerClassPart.Text, out dogsPerClassPart);
                sc.DogsPerClassPart = dogsPerClassPart;
                sc.Judges = txtJudges.Text;
                decimal classCost = 0;
                decimal.TryParse(txtClassCost.Text, out classCost);
                sc.ClassCost = classCost;
                sc.Save();

                int eventID = 0;
                int.TryParse(Request.QueryString["eventid"], out eventID);
                Response.Redirect("~/Admin/Shows?eventid=" + eventID + "&showid=" + showID, true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            Response.Redirect("~/Admin/Shows?eventid=" + eventID + "&showid=" + showID, true);
        }

        protected void ClassCostValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool argsValid = true;
            if (!string.IsNullOrEmpty(args.Value))
            {
                decimal amount = 0;
                if (!decimal.TryParse(args.Value, out amount))
                {
                    argsValid = false;
                }
            }
            args.IsValid = argsValid;
        }

        protected void DogsPerClassPartValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool argsValid = true;
            if (!string.IsNullOrEmpty(args.Value))
            {
                int amount = 0;
                if (!int.TryParse(args.Value, out amount))
                {
                    argsValid = false;
                }
                else if(amount < 1)
                {
                    argsValid = false;
                }
            }
            args.IsValid = argsValid;
        }
    }
}