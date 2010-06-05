/****************************************************/
// Filename: Notify.cs
// Created: Chris Fay
// Change history:
// 5.1.2009 / Chris Fay
// 5.5.2010 / Chris Fay (cleanup)
/****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace EnvatoTracker
{
    class Notify
    {
        String newLine = System.Environment.NewLine.ToString();

        public bool sendEmailNotify(int newSaleItemCount, ref NotifyIcon notifyIcon)
        {
            //get any recent sales (item name and sold date)
            EnvatoAPI api = new EnvatoAPI();
            ArrayList salesData = api.getNewSalesItemName(EnvatoTrackerFrame.username, EnvatoTrackerFrame.apiKey, newSaleItemCount, ref notifyIcon);
            if (salesData.Count <= 0)
                return false;

            //start the email format
            string body = "Hello "+EnvatoTrackerFrame.username+",<br>" +
                          "Below are your new sales received:<br><br>";

            //loop through each new item and build the email body            
            foreach (ArrayList arr in salesData)
            {
                body += "Item name: " + arr[0].ToString() + "<br>" +
                        "Sold at: " + arr[1].ToString() + "<br><br>";
            }

            body += "Thank-you,<br>EnvatoTracker";

            //send the email
            Email email = new Email();

            if (!email.sendEmail(EnvatoTrackerFrame.emailUsername, EnvatoTrackerFrame.emailPassword, EnvatoTrackerFrame.emailHost,
                EnvatoTrackerFrame.emailFrom, EnvatoTrackerFrame.emailTo, "EnvatoTracker - You have new sales!", body, EnvatoTrackerFrame.emailEnableSSL))
                return false;
            else
                return true;
            
        }

        public bool sendEmailAndPopupNotify()
        {
            return true;
        }                
    }
}
