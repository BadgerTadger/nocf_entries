using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace nocf_entries.Manage
{
    public partial class Dogs : System.Web.UI.Page
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
                    phView.Visible = false;
                    phEdit.Visible = true;
                }
                else
                {
                    PopulateViewFields();
                    phEdit.Visible = false;
                    phView.Visible = true;
                }
            }
        }

        private void PopulateViewFields()
        {
            //lblName.Text = BuildName(owner);
            lblKCRegName.Text = owner.KCName;
            lblUsername.Text = owner.UserName;
            //lblAddress.Text = BuildAddress(owner);
            lblEmail.Text = owner.Email;
            lblPhone.Text = owner.Phone;
            lblMobile.Text = owner.Mobile;
        }
    }
}