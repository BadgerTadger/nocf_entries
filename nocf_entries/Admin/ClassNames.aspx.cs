using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nocf_entries.App_Code;

namespace nocf_entries.Admin
{
    public partial class ClassNames : System.Web.UI.Page
    {
        string _mode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!User.IsInRole("Admin"))
            {
                Response.Redirect("~/Default");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    int _classNameID = 0;
                    int.TryParse(Request.QueryString["id"], out _classNameID);

                    _mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();
                    switch (_mode)
                    {
                        case "a":
                            phView.Visible = false;
                            phEdit.Visible = true;
                            break;
                        case "e":
                            PopulateEditFields(_classNameID);
                            phView.Visible = false;
                            phEdit.Visible = true;
                            break;
                        default:
                            phEdit.Visible = false;                            
                            PopulateViewFields(_classNameID);
                            phView.Visible = true;
                            break;
                    }
                }
            }
        }

        private void PopulateViewFields(int classNameID)
        {
            rptrClassNames.DataSource = ClassName.GetClassNameList();
            rptrClassNames.DataBind();
        }

        private void PopulateEditFields(int classNameID)
        {
            if(classNameID > 0)
            {
                ClassName className = new ClassName();
                className.Load(classNameID);
                txtClassName.Text = className.Class_Name_Description;
            }
        }

        protected void btnAddClassName_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames?mode=a");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int classNameID = 0;
                int.TryParse(Request.QueryString["id"], out classNameID);
                ClassName className = new ClassName();
                className.Class_Name_ID = classNameID;
                className.Class_Name_Description = txtClassName.Text;
                className.Save();
                Response.Redirect("~/Admin/ClassNames");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames");
        }

        protected void rptrClassNames_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames?mode=e&id=" + e.CommandName);
        }
    }
}