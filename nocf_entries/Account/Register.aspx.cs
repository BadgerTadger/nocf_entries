﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using nocf_entries.Models;
using System.Net.Mail;

namespace nocf_entries.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                SendEmail(user.Email, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

//                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(toEmail));
            message.From = new MailAddress("noreply@nocf.co.uk");
            message.Subject = subject;
            message.Body = body;

            try
            {
                SmtpClient mailClient = new SmtpClient("relay-hosting.secureserver.net");
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