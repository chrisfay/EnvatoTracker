using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace EnvatoTracker
{
    class User
    {
        public bool IsValidEmailAddress(string emailAddress)
        {

            //                string patternLenient = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            //                Regex reLenient = new Regex(patternLenient);

            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                  + @"[a-zA-Z]{2,}))$";

            Regex reStrict = new Regex(patternStrict);

            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;
        }

        //check if the user has new sales by
        //comparing known sales against results from api
        //returns true on success or false on failure
        public bool hasNewSales(string username)
        {
            //get current known sales            
            int currentSaleTotal = 0, newSaleTotal = 0;
            string value = "";
            
            if (!File.Exists("sales.txt"))
                return false;
            
            StreamReader sr = new StreamReader("sales.txt");
            value = sr.ReadLine();
            sr.Close();
            string[] split =  value.Split(':');
            currentSaleTotal = int.Parse(split[1].ToString());
            
            //get new total sales from api
            EnvatoAPI api = new EnvatoAPI();
            newSaleTotal = api.getTotalSales(username);

            if (newSaleTotal > currentSaleTotal)
                return true;
            else
                return false;
        }

        //updates the total sales in the log file used by the app
        public void setSalesTotal(int totalSales)
        {            
            StreamWriter sr = new StreamWriter("sales.txt");
            sr.WriteLine("TotalSales:" + totalSales.ToString());
            sr.Close();
        }

        //get the total sales stored in sales.txt
        //return number on success or -1 on failure
        public int getCurrentKnownSales()
        {
            if (!File.Exists("sales.txt"))
                return -1;
                        
            StreamReader sr = new StreamReader("sales.txt");
            int currentSaleTotal = 0;
            string value = sr.ReadLine();
            sr.Close();

            string[] split = value.Split(':');
            currentSaleTotal = int.Parse(split[1].ToString());

            return currentSaleTotal;
        }

        //validates the user/key combo
        public bool isValidApiKey(string username, string apiKey)
        {
            EnvatoAPI api = new EnvatoAPI();
            return api.isValidApiKey(username, apiKey);
        }
    }
}
