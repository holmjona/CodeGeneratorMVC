using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Net.Mail;

namespace Security {
    public class Mailer {
        private string server = "localhost";
        /// <summary>
		/// 		''' Send Email notifying user that Account has been set up.
		/// 		''' </summary>
		/// 		''' <param name="myUser"></param>
		/// 		''' <param name="newpassword"></param>
		/// 		''' <param name="isNewUser"></param>
		/// 		''' <returns></returns>
		/// 		''' <remarks></remarks>
        public int sendNewPassword(Security.UserBase myUser, string newpassword, bool isNewUser, Security.UserBase userWhoAdded, SiteConfigBase sConfig, string customMessage = null) {


            // Dim myToAddress As New MailAddress(myUser.UserName, myUser.FullName)
            // Dim myFromAddress As New MailAddress(sConfig.AdministratorEmailAddress, sConfig.Title & " Administrator")
            // Dim myMessage As New MailMessage
            string subject, body;

            if (isNewUser) {
                subject = "New Account Request";
                body = myUser.FirstName + "," + Constants.vbCrLf
     + Constants.vbCrLf
     + "You have been added to the " + sConfig.Title + "." + Constants.vbCrLf
     + "Your login username is:" + Constants.vbCrLf
     + myUser.UserName + Constants.vbCrLf
     + "Your new password is:" + Constants.vbCrLf
     + newpassword + Constants.vbCrLf
     + Constants.vbCrLf
     + "You may log in and change you password at "
     + sConfig.SiteAddress + "/";

                if (customMessage != null)
                    body += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Constants.vbCrLf
+ "Message From: " + userWhoAdded.OfficialName + Constants.vbCrLf
+ "------------------------------------------------------------" + Constants.vbCrLf
+ customMessage + Constants.vbCrLf
+ "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
                body += Constants.vbCrLf
     + Constants.vbCrLf
     + "Sincerely," + Constants.vbCrLf
      + sConfig.Title + " Administrator" + Constants.vbCrLf
      + sConfig.AdministratorEmailAddress;
            } else {
                subject = "Password Change Request";
                body = "A password change for the " + sConfig.Title + " has been requested";

                if (userWhoAdded != null && myUser.ID != userWhoAdded.ID)
                    body += " by " + userWhoAdded.OfficialName;

                body += "." + Constants.vbCrLf
     + "Your new password is:" + Constants.vbCrLf
     + newpassword + Constants.vbCrLf
     + Constants.vbCrLf
     + "You may log in and change you password at "
     + sConfig.SiteAddress + "/";

                // If Not myUser.Role.HasCreatorRights Then myMessage.Body &= "Public"

                body += "Settings.aspx" + Constants.vbCrLf
     + Constants.vbCrLf
     + "Thank you," + Constants.vbCrLf
     + sConfig.Title + " Administrator" + Constants.vbCrLf
     + sConfig.AdministratorEmailAddress;
            }
            // Try
            // smtpC.Send(myMessage)
            // successAnswer = 1
            // Catch e As Exception
            // successAnswer = -1
            // End Try
            return sendEmail(myUser, subject, body, sConfig); // successAnswer
        }

        private int sendEmail(Security.UserBase toUser, string subject, string message, SiteConfigBase sConfig) {
            int successAnswer = 0;
            // Dim smtpC As New Net.Mail.SmtpClient
            // smtpC.Host = server
            // smtpC.Port = 25
            string toaddress = toUser.Email;
            if (SiteConfigBase.VersionClass == SiteConfigBase.verClass.dev)
                toaddress = sConfig.AdministratorEmailAddress;
            System.Net.Mail.MailAddress myToAddress = new System.Net.Mail.MailAddress(toUser.Email, toUser.OfficialName);
            System.Net.Mail.MailAddress myFromAddress = new System.Net.Mail.MailAddress(sConfig.AdministratorEmailAddress, sConfig.Title + " Administrator");
            System.Net.Mail.MailMessage myMessage = new System.Net.Mail.MailMessage();
            myMessage.To.Add(myToAddress);
            myMessage.From = myFromAddress;
            // myMessage.IsBodyHtml = True
            myMessage.Subject = subject;
            myMessage.Body = message + Constants.vbCrLf
              + Constants.vbCrLf
              + adminSig(sConfig);

            try {
                if (Security.SiteConfigBase.TestingSite)
                    throw new Exception("The site is being tested.");
                System.Net.NetworkCredential netCredentials = new System.Net.NetworkCredential("", "6Ab=?revap$2QE!"); // eEncryptor.Decrypt(sConfig.EmailServerPassword))

                // smtpC.UseDefaultCredentials = False
                // smtpC.Credentials = CType(netCredentials.GetCredential(sConfig.EmailServerAddress, sConfig.EmailServerSMTPPort, "NTLM"), System.Net.ICredentialsByHost)
                System.Net.Mail.SmtpClient mynewsmtp = new System.Net.Mail.SmtpClient(sConfig.Email_ServerAddress);
                mynewsmtp.UseDefaultCredentials = false;
                mynewsmtp.Port = 25;
                mynewsmtp.Credentials = new System.Net.NetworkCredential("", "6Ab=?revap$2QE!");
                // mynewsmtp.Credentials = CType(netCredentials.GetCredential(sConfig.EmailServerAddress, sConfig.EmailServerSMTPPort, "Basic"), System.Net.ICredentialsByHost)
                mynewsmtp.Send(myMessage);
                // smtpC.Send(myMessage)

                successAnswer = 1;
            } catch (Exception e) {
                successAnswer = -1;
            }
            return successAnswer;
        }
        private string adminSig(SiteConfigBase sConfig) {
            return "Sincerely," + Constants.vbCrLf
             + sConfig.Title + " Administrator " + Constants.vbCrLf
             + sConfig.AdministratorEmailAddress;
        }
    }
}
