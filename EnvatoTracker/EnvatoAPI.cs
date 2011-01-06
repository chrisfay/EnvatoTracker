/****************************************************/
// Filename: EnvatoAPI.cs
// Created: Chris Fay
// Change history:
// 5.1.2009 / Chris Fay
// 5.5.2010 / Chris Fay (cleanup)
/****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections;

namespace EnvatoTracker
{
    class EnvatoAPI
    {
        //get the total number of sales from the given username
        public int getTotalSales(string username)
        {
            if (String.IsNullOrEmpty(username))
                return 0;

            WebClient client = new WebClient();
            string urlFormat = "http://marketplace.envato.com/api/v1/user:" + username + ".json";
            Uri url = new Uri(string.Format(urlFormat));
            string data = client.DownloadString(url);
            int totalSales = 0;
                        
            using (JsonReader reader = new JsonReader(new StringReader(data)))
            {
                while (reader.Read())
                {
                    //users++;
                    if ((string)reader.Value == "sales")
                    {
                        reader.Read();
                        totalSales = int.Parse((string)reader.Value);
                    }                        
                }
                reader.Close(); //close the reader object
            }            

            return totalSales;
        }

        //return all sales information within a 2d array of parameter/values
        //Index 0 = "item name"
        //Index 1 = "sold at"
        // array[0][0] = "orangeDream"
        // array[0][1] = "Sun Mar 29 08:09:17 +1100 2009"
        // array[1][0] = "orangeDream"
        // array[1][1] = "Sun Mar 29 08:09:17 +1100 2009"
        public ArrayList getNewSalesItemName(string username, string apiKey, int max, ref NotifyIcon notifyIcon)
        {
            ArrayList twod_salesItems = new ArrayList(); //contains only the newest items (equal to max) after sorting by date

            try
            {

                if (String.IsNullOrEmpty(username))
                    return new ArrayList();


                ArrayList tempTwod_salesItems = new ArrayList(); //stores all itemname/sold at pairs before sorting and stripping out max


                WebClient client = new WebClient();
                string urlFormat = "http://marketplace.envato.com/api/v1/" + username + "/" + apiKey + "/recent-sales.json";
                Uri url = new Uri(string.Format(urlFormat));
                string data = client.DownloadString(url);

                //when looping through each item we should compare the dates against the ones in the array
                //if a newer sale is found we replace it 
                using (JsonReader reader = new JsonReader(new StringReader(data)))
                {
                    string itemName = "";
                    string soldAt = "";

                    while (reader.Read())
                    {
                        if ((string)reader.Value == "item")
                        {
                            reader.Read();
                            itemName = (string)reader.Value;
                        }

                        if ((string)reader.Value == "sold_at")
                        {
                            reader.Read();
                            soldAt = (string)reader.Value;

                            //we have both item name and sold time so add to array list
                            ArrayList tempArr = new ArrayList(); //stores the item name/sold at pairs
                            tempArr.Add(itemName);
                            tempArr.Add(soldAt);
                            tempTwod_salesItems.Add(tempArr);
                        }
                    }
                    reader.Close(); //close the reader object
                }

                //lets process through the temp 2d array and extrac the newest items up to max            
                ArrayList dates = new ArrayList();
                int loop = 0;
                foreach (ArrayList arr in tempTwod_salesItems)
                {
                    if (loop < max)
                        twod_salesItems.Add(arr);
                    else
                        break;

                    loop++;
                }
            }
            catch (WebException webex)
            {                
                notifyIcon.ShowBalloonTip(180000000, "EnvatoTracker error!", "Error querying the api - invalid api key maybe?\nError:" + webex, ToolTipIcon.None);
            }
            
            return twod_salesItems;
        }

        //get the current account balance for a given user
        public string getAccountBalance(string username, string apiKey, ref NotifyIcon notifyIcon)
        {
            string accountBalance = "0.0";

            try
            {
                if (String.IsNullOrEmpty(username))
                    return "";

                WebClient client = new WebClient();
                string urlFormat = "http://marketplace.envato.com/api/v1/" + username + "/" + apiKey + "/account.json";
                Uri url = new Uri(string.Format(urlFormat));
                string data = client.DownloadString(url);               

                using (JsonReader reader = new JsonReader(new StringReader(data)))
                {
                    while (reader.Read())
                    {
                        //users++;
                        if ((string)reader.Value == "balance")
                        {
                            reader.Read();
                            accountBalance = (string)reader.Value;
                        }
                    }
                    reader.Close(); //close the reader object
                }
            }
            catch (WebException webex)
            {                
                notifyIcon.ShowBalloonTip(180000000, "EnvatoTracker error!", "Error querying the api - invalid api key maybe?\n\nError:"+webex, ToolTipIcon.None);
            }

            return accountBalance;
        }


        //validates the username/key combo
        //on failure we get a WebException and return false, otherwise return true
        public bool isValidApiKey(string username, string apiKey)
        {
            bool isValid = false;

            try
            {
                if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(apiKey))
                    return false;

                WebClient client = new WebClient();
                string urlFormat = "http://marketplace.envato.com/api/v1/" + username + "/" + apiKey + "/account.json";
                Uri url = new Uri(string.Format(urlFormat));
                string data = client.DownloadString(url);

                using (JsonReader reader = new JsonReader(new StringReader(data)))
                {
                    while (reader.Read())
                    {
                        //do nothing with the data
                    }
                    reader.Close(); //close the reader object
                }
                isValid = true;
            }
            catch (WebException webex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
