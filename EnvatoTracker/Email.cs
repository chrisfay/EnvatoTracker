using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace EnvatoTracker
{
    class Email
    {

        public bool sendEmail(string username, string password, string host, 
            string from, string to, string subject, string body, bool ssl)
        {
            bool success = false;

            try
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential =
                    new NetworkCredential(username, password);
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(from);

                smtpClient.Host = host;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                if (ssl)
                    smtpClient.EnableSsl = true;

                message.From = fromAddress;
                message.Subject = subject;
                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;
                message.Body = body;
                message.To.Add(to);
                
                smtpClient.Send(message);
                success = true;                
            }
            catch (Exception ex)
            {
                //Error, could not send the message
                //MessageBox.Show("Could not send email. Error was:\n" + ex);
                success = false;
            }

            return success;
        }
    }
}
