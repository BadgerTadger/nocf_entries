using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;

namespace nocf_entries.Account
{
    public partial class ManageEmail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UpdateEmail_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                RegisterAsyncTask(new PageAsyncTask(UpdateEmail));
            }
        }

        private async Task UpdateEmail()
        {
            var userManager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            string userid = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userid);
            if (user.Email.ToLowerInvariant() != Email.Text.ToLowerInvariant())
            {
                ErrorMessage.Text = "Your current password is incorrect";
            }
            else
            {
                user.Email = NewEmail.Text;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    string code = userManager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    SendEmail(user.Email, "Confirm your updated email", "Please confirm your updated email by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                    //                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                }
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress("noreply@nocf.co.uk");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                //SmtpClient mailClient = new SmtpClient("relay-hosting.secureserver.net");  //On Godaddy Server
                SmtpClient mailClient = new SmtpClient("smtpout.europe.secureserver.net", 80); //Local Server

                mailClient.Credentials = new System.Net.NetworkCredential("entries@nocf.co.uk", "Dazzer67");
                mailClient.Send(message);

            }
            catch (System.Web.HttpException ehttp)
            {
                Console.WriteLine("{0}", ehttp.Message);
                Console.WriteLine("Here is the full error message output");
                Console.Write("{0}", ehttp.ToString());
            }
        }
    }
}