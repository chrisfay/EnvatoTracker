/****************************************************/
// Filename: Config.cs
// Created: Chris Fay
// Change history:
// 5.1.2009 / Chris Fay
// 5.5.2010 / Chris Fay (cleanup)
/****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EnvatoTracker
{
    class Config
    {
        //get all config data - return the number of parameters retrieved on success or -1 on failure
        String newLine = System.Environment.NewLine.ToString();
        public int initializeConfig()
        {
            //load config data
            int totalParms = 0;
            FileInfo cf = new FileInfo("config.ini");
            if (!cf.Exists)
            {
                String configData = "#############################################################################" + newLine +
                        "##################                                   ########################" + newLine +
                        "################## Config file for EnvatoTracker.exe ########################" + newLine +
                        "##################                                   ########################" + newLine +
                        "#############################################################################" + newLine + newLine +

                        "[user options]" + newLine +
                        ";Define the envato username" + newLine +
                        @"username=" + newLine +
                        ";Define the user specific envato api key" + newLine +
                        @"api.key=" + newLine + newLine +

                        "[notification options]" + newLine +
                        ";notify.type options: 1=email only, 2=popup only, 3=email & popup" + newLine +
                        "notify.type=2" + newLine +
                        ";notify.interval options (in minutes): 5,30,60,360,720,1440" + newLine +
                        "notify.interval=5" + newLine + newLine +

                        "[email config]" + newLine +
                        ";In order to receive email notifications you will need a mail server to send through. I use my gmail account but" + newLine +
                        ";any mail server that you can authenticate with should work fine" + newLine +
                        ";username to auth with server (either user or user@domain depending) For example, my gmail username is chrislfay@gmail.com" + newLine +
                        "mail.username=" + newLine +
                        ";your password to connect to mail server" + newLine +
                        "mail.password=" + newLine +
                        ";your mail server host name (ie smtp.gmail.com)" + newLine +
                        "mail.host=" + newLine +
                        ";the email address you want the header to show from (ie your email address)" + newLine +
                        "mail.from=" + newLine +
                        ";the email address to to send notifications to (ie your email address)" + newLine +
                        "mail.to=" + newLine +
                        ";whether or not to use ssl - some servers like Gmail require ssl enabled (options are true or false)" + newLine +
                        "mail.enable.ssl=false" + newLine + newLine +

                        "[sound file]" + newLine +
                        ";wav file played when a sale is detected" + newLine +
                        ";path should be relative to the .exe file (ie in the same dir just use the file name, other wise full path" + newLine +
                        "wav.file=MONEY95.WAV";


                StreamWriter sw = new StreamWriter("config.ini");
                sw.Write(configData);
                sw.Close();
            }
                        
            StreamReader sr = new StreamReader("config.ini");
            String value = "";
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("username"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.username = split[1].Trim();
                    totalParms++;
                }

                if (value.StartsWith("api.key"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.apiKey = split[1].Trim();
                    totalParms++;
                }                          

                if (value.StartsWith("notify.type"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.notifyType = int.Parse(split[1].Trim());
                    totalParms++;
                }

                if (value.StartsWith("notify.interval"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.notifyInterval = int.Parse(split[1].Trim());
                    totalParms++;
                }
                if (value.StartsWith("mail.username"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.emailUsername = split[1].Trim();
                    totalParms++;
                }
                if (value.StartsWith("mail.password"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.emailPassword = split[1].Trim();
                    totalParms++;
                }
                if (value.StartsWith("mail.host"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.emailHost = split[1].Trim();
                    totalParms++;
                }
                if (value.StartsWith("mail.from"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.emailFrom = split[1].Trim();
                    totalParms++;
                }
                if (value.StartsWith("mail.to"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.emailTo = split[1].Trim();
                    totalParms++;
                }
                if (value.StartsWith("mail.enable.ssl"))
                {
                    String[] split = value.Split('=');
                    if (split[1] == "true")
                        EnvatoTrackerFrame.emailEnableSSL = true;
                    else
                        EnvatoTrackerFrame.emailEnableSSL = false;
                    totalParms++;
                }
                if (value.StartsWith("wav.file"))
                {
                    String[] split = value.Split('=');
                    EnvatoTrackerFrame.wavFile = split[1].Trim();
                    totalParms++;
                }
            }
            sr.Close();
            
            return totalParms;
        }

        public bool setUsername(string username)
        {
            if (!File.Exists("config.ini"))
                return false;

            String result = "", value = "";
            StreamReader sr = new StreamReader("config.ini");
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("username"))
                {
                    result += "username=" + username + newLine;
                }
                else
                    result += value + newLine;
            }
            sr.Close();

            //write new config file
            StreamWriter sw = new StreamWriter("config.ini");
            sw.Write(result);
            sw.Close();

            return true;
        }

        public bool setApiKey(string key)
        {
            if (!File.Exists("config.ini"))
                return false;

            String result = "", value = "";
            StreamReader sr = new StreamReader("config.ini");
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("api.key"))
                {
                    result += "api.key=" + key + newLine;
                }
                else
                    result += value + newLine;
            }
            sr.Close();

            //write new config file
            StreamWriter sw = new StreamWriter("config.ini");
            sw.Write(result);
            sw.Close();

            return true;

        }               

        public bool setNotifyType(int notifyType)
        {
            if (!File.Exists("config.ini"))
                return false;

            String result = "", value = "";
            StreamReader sr = new StreamReader("config.ini");
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("notify.type"))
                {
                    result += "notify.type=" + notifyType + newLine;
                }
                else
                    result += value + newLine;
            }
            sr.Close();

            //write new config file
            StreamWriter sw = new StreamWriter("config.ini");
            sw.Write(result);
            sw.Close();

            return true;
        }

        public bool setNotfyInterval(int interval)
        {
            if (!File.Exists("config.ini"))
                return false;

            String result = "", value = "";
            StreamReader sr = new StreamReader("config.ini");
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("notify.interval"))
                {
                    result += "notify.interval=" + interval + newLine;
                }
                else
                    result += value + newLine;
            }
            sr.Close();

            //write new config file
            StreamWriter sw = new StreamWriter("config.ini");
            sw.Write(result);
            sw.Close();

            return true;
        }

        //update config file with new email to address
        public bool setEmailTo(string email)
        {
            if (!File.Exists("config.ini"))
                return false;
            
            String result = "", value = "";
            StreamReader sr = new StreamReader("config.ini");
            while ((value = sr.ReadLine()) != null)
            {
                if (value.StartsWith("mail.to"))
                {
                    result += "mail.to=" + email + newLine;
                }
                else
                    result += value + newLine;
            }
            sr.Close();

            //write new config file
            StreamWriter sw = new StreamWriter("config.ini");
            sw.Write(result);
            sw.Close();

            return true;
        }                
    }
}