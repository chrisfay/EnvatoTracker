using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading;
using System.Timers;



namespace EnvatoTracker
{
    public partial class EnvatoTrackerFrame : Form
    {
        //instance variables
        private static System.Timers.Timer aTimer;
        
        //config items
        public static string username = "", apiKey = "", emailUsername="",emailPassword="",emailHost="",emailFrom="",emailTo="",wavFile="MONEY95.WAV",accountBalance = "0.0";
        public static int notifyType = 2, notifyInterval = 5;
        public static bool emailEnableSSL = false;
        private bool closing = false; //track what is initiating the close (x'ing the form or shutdown)

         
        public EnvatoTrackerFrame()
        {            
            InitializeComponent();
            User user = new User();
            EnvatoAPI api = new EnvatoAPI();
            
            //get ini config data, or create default file if not exists
            Config config = new Config();
            if (config.initializeConfig() < 5)            
                MessageBox.Show("Please verify you have set all configuration parameters and relaunch");                
            

            //load all window elements from config data
            usernameTextbox.Text = username;
            apikeyTextbox.Text = apiKey;
            emailTextbox.Text = emailTo;            
            notifyIntervalCombo.Text = notifyInterval.ToString();

            switch (notifyType)
            {
                case 1:
                    notifyOptionsCombo.Text = "Email Only";
                    break;
                case 2:
                    notifyOptionsCombo.Text = "Popup Only";
                    break;
                case 3:
                    notifyOptionsCombo.Text = "Email & Popup";
                    break;
                default:
                    notifyOptionsCombo.Text = "Popup Only";
                    break;
            }

            //remove from task bar on startup (should onlyl show in system tray)
            this.ShowInTaskbar = false;

            //display a popup that shows we're minimized and to make sure things are configured
            notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker", "Remember to set your configuration information\nor you won't get any updates. (you should see your sales total if configured properly)", ToolTipIcon.None);
            
            //lets check if configured user is valid, if not we shouldn't do anything, otherwise lets initialize
            //set status (user is configured so we're polling)only if the username/key are successful            
            if (user.isValidApiKey(username, apiKey))
            {
                //get total sales            
                int totalSales = api.getTotalSales(username);

                //update account balance & show balance if hover over tray icon
                accountBalance = api.getAccountBalance(username, apiKey, ref notifyIcon);
                if (accountBalance == "")
                    accountBalance = "0.0";
                notifyIcon.Text = "EnvatoTracker\nAccount Balance: $" + accountBalance + "\nTotal Sales: " + totalSales;

                //check if sales file exists, if it does we should compare the total to the number returned from the 
                //api            
                if (user.hasNewSales(username))
                {
                    notifyIcon.ShowBalloonTip(18000000, "New Themeforest sales!", "You have received new sales since last time!\nAccount Balance:" + accountBalance, ToolTipIcon.None);

                    //update the sales total in sales.txt
                    user.setSalesTotal(totalSales);                                        
                }                                                

                appStatusValue.Text = "API polling active";
                totalSalesLabelTextValue.Text = totalSales.ToString();
            }
            else
                appStatusValue.Text = "API polling inactive";
            

            //and finally we kick off the scheduler            
            schedulerWorker.RunWorkerAsync();                        
        }
        
        private void updateButton_Click_1(object sender, EventArgs e)
        {
            //write user info to file            
            username = usernameTextbox.Text;
            apiKey = apikeyTextbox.Text;
            emailTo = emailTextbox.Text;
            EnvatoAPI api = new EnvatoAPI();
            User user = new User();

            //validate fields
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(apiKey) || String.IsNullOrEmpty(emailTo))
                MessageBox.Show("Please fill in all details and resubmit");
            else if (!user.IsValidEmailAddress(emailTo))
                MessageBox.Show("Invalid email address");            
            else
            {
                //check if user is valid, if so we notify that we're polling, otherwise display warning
                if (user.isValidApiKey(username, apiKey))
                {
                    //update config 
                    Config config = new Config();
                    config.setUsername(username);
                    config.setApiKey(apiKey);
                    config.setEmailTo(emailTo);

                    //update total sales status label
                    int totalSales = api.getTotalSales(username);
                    totalSalesLabelTextValue.Text = totalSales.ToString();

                    //write sales total to file
                    StreamWriter sr2 = new StreamWriter("sales.txt");
                    sr2.WriteLine("TotalSales:" + totalSales);
                    sr2.Close();

                    //update account balance & show balance if hover over tray icon
                    accountBalance = api.getAccountBalance(username, apiKey, ref notifyIcon);
                    if (accountBalance == "")
                        accountBalance = "0.0";
                    notifyIcon.Text = "EnvatoTracker\nAccount Balance: $" + accountBalance + "\nTotal Sales: " + totalSales;

                    appStatusValue.Text = "API polling active";
                    totalSalesLabelTextValue.Text = totalSales.ToString();
                }
                else
                    appStatusValue.Text = "API polling inactive - invalid key/username combo";          
            }
        }

        private void updateOptionsButton_Click(object sender, EventArgs e)
        {
            //update options to file           
            string type = notifyOptionsCombo.Text;
            notifyInterval = int.Parse(notifyIntervalCombo.Text);

            //map the notify type to the numeric code
            switch (type)
            {
                case "Email Only":
                    notifyType = 1;
                    break;
                case "Popup Only":
                    notifyType = 2;
                    break;
                case "Email & Popup":
                    notifyType = 3;
                    break;
                default:
                    notifyType = 2;
                    break;
            }

            Config config = new Config();
            config.setNotfyInterval(notifyInterval);
            config.setNotifyType(notifyType);

            //stop/start the scheduler thread and timer
            schedulerWorker.CancelAsync();
            schedulerWorker.RunWorkerAsync();
            aTimer.Stop();

            appStatusValue.Text += " - updated";
        }

        //start of scheduling thread
        private void schedulerWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            //convert chosen interval to millisecons
            int interval = 300000;
            switch (notifyInterval)
            {
                case 5:
                    interval = 300000;
                    break;
                case 30:
                    interval = 1800000;
                    break;
                case 60:
                    interval = 3600000;
                    break;
                case 360:
                    interval = 21600000;
                    break;
                case 720:
                    interval = 43200000;
                    break;
                case 1440:
                    interval = 86400000;
                    break;
                default:
                    interval = 300000;
                    break;
            }

            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to total milliseconds (5 mins = 300,000)
            aTimer.Interval = interval;
            aTimer.Enabled = true;

        }

        // What to happen when the timer event is raised
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            /*
             * TASKS:
             * 1. Connect to api (if username/apikey are valid)
             * 2. Get total sales count
             * 3. Compare count to known sales count
             * 4. If new count is greater we should notify the user with details of the sales
             *   (ie subtract new sales from known sales and get the last x amount of sales
             */
            User user = new User();
            EnvatoAPI api = new EnvatoAPI();
            Notify notify = new Notify();
            if (user.isValidApiKey(username, apiKey))
            {
                if (user.hasNewSales(username))
                {
                    //check the notify type and send notification with info
                    //we should find out the details of the new sales and either pop'm up or email em
                    int newSalesTotal = api.getTotalSales(username);
                    int currentSalesTotal = user.getCurrentKnownSales();
                    if (currentSalesTotal != -1)
                    {
                        int salesChange = newSalesTotal - currentSalesTotal;

                        //update account balance
                        accountBalance = api.getAccountBalance(username, apiKey, ref notifyIcon);

                        switch (notifyType)
                        {
                            case 2:
                                //popup only
                                notifyIcon.ShowBalloonTip(18000000, "New Themeforest sales!", "You have " + salesChange + " new sale(s)!\nAccount Balance:" + accountBalance, ToolTipIcon.None);
                                break;
                            case 1:
                                //email only
                                if (!notify.sendEmailNotify(salesChange, ref notifyIcon))
                                    notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker Error", "Failed to send email with new sales - verify your email config", ToolTipIcon.None);
                                break;
                            case 3:
                                //popup and email                            
                                if (!notify.sendEmailNotify(salesChange, ref notifyIcon))
                                    notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker Error", "Failed to send email with new sales - verify your email config", ToolTipIcon.None);
                                notifyIcon.ShowBalloonTip(18000000, "New Themeforest sales!", "You have " + salesChange + " new sale(s)!", ToolTipIcon.None);
                                break;
                            default:
                                //default is just a popup
                                notifyIcon.ShowBalloonTip(18000000, "New Themeforest sales!", "You have " + salesChange + " new sale(s)!\nAccount Balance:" + accountBalance, ToolTipIcon.None);
                                break;
                        }

                        //now update known sales total (to keep from sending out notifications over and over)                                    
                        user.setSalesTotal(newSalesTotal);

                        //update the ui with new sales total                
                        totalSalesLabelTextValue.Invoke(new updateSalesTotalUICallback(this.updateSalesTotalUI), new object[] { newSalesTotal });

                        //update balloon hover text
                        notifyIcon.Text = "EnvatoTracker\nAccount Balance: $" + accountBalance + "\nTotal Sales: " + newSalesTotal;                                               

                        //play the kaching sound - cause we're cool like that
                        if (File.Exists(@wavFile))
                        {
                            System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
                            myPlayer.SoundLocation = @wavFile;
                            myPlayer.Play();
                        }
                    }
                    else
                        notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker Error!", "Could not get sales change for some reason.", ToolTipIcon.None);
                }
            }
        }


        //completion of scheduling thread
        private void schedulerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        //used by the worker thread to update sales totals (delegate sends safely)
        public void updateSalesTotalUI(int newSalesTotal)
        {
            totalSalesLabelTextValue.Text = newSalesTotal.ToString();
        }

        public delegate void updateSalesTotalUICallback(int newSalesTotal);

        private void EnvatoTrackerFrame_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;

            //show in task bar on startup (should onlyl show in system tray)
            this.ShowInTaskbar = true;
        }

        //restore app from right click -> restore
        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;

            //show in task bar on startup (should onlyl show in system tray)
            this.ShowInTaskbar = true;
        }

        //exit with right click of icon
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        //override the default close behavior so it minimizes instead
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closing) // not ready to close yet
            {
                //cancels the closing event
                e.Cancel = true;
                //hides the window
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.Hide();

                //hide in task bar on startup (should onlyl show in system tray)
                this.ShowInTaskbar = false;
            }
            //else let the system shutdown and close the form window

        }

        //what to do when balloon tip is clicked
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(username))
                Help.ShowHelp(this, "http://themeforest.net/user/" + username + "/portfolio");
            else
                notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker Error!", " Update with valid username to view portfolio", ToolTipIcon.None);
        }

        //launch sales from context menu
        private void viewSalesPortfolioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(username))
                Help.ShowHelp(this, "http://themeforest.net/user/" + username + "/portfolio");
            else
                notifyIcon.ShowBalloonTip(18000000, "EnvatoTracker Error!", " Update with valid username to view portfolio", ToolTipIcon.None);
        }

        //make sure the default notifyIcon is shown on tip close
        private void notifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {           
            
        }
        
        //overwrite windows shutdown event - fixes issue where app wouldn't allow system to shutdown
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0011) //WM_QUERYENDSESSION
            {
                closing = true;
            }
            base.WndProc(ref m);
        }      
    }
}