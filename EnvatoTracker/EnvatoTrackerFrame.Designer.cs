namespace EnvatoTracker
{
    partial class EnvatoTrackerFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnvatoTrackerFrame));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.userTab = new System.Windows.Forms.TabPage();
            this.totalSalesLabelTextValue = new System.Windows.Forms.Label();
            this.totalSalesLabelText = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.apikeyTextbox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.apiKeyLabel = new System.Windows.Forms.Label();
            this.emailTextbox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.optionsTab = new System.Windows.Forms.TabPage();
            this.updateOptionsButton = new System.Windows.Forms.Button();
            this.notifyIntervalCombo = new System.Windows.Forms.ComboBox();
            this.notifyIntervelLabel = new System.Windows.Forms.Label();
            this.notifyOptionsCombo = new System.Windows.Forms.ComboBox();
            this.notifyOptionsLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.appStatusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schedulerWorker = new System.ComponentModel.BackgroundWorker();
            this.viewSalesPortfolioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainPanel.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.userTab.SuspendLayout();
            this.optionsTab.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.trayIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tabPanel);
            this.mainPanel.Controls.Add(this.statusStrip1);
            this.mainPanel.Location = new System.Drawing.Point(1, 1);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(329, 222);
            this.mainPanel.TabIndex = 0;
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.userTab);
            this.tabPanel.Controls.Add(this.optionsTab);
            this.tabPanel.Location = new System.Drawing.Point(3, 11);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(320, 187);
            this.tabPanel.TabIndex = 8;
            // 
            // userTab
            // 
            this.userTab.Controls.Add(this.totalSalesLabelTextValue);
            this.userTab.Controls.Add(this.totalSalesLabelText);
            this.userTab.Controls.Add(this.updateButton);
            this.userTab.Controls.Add(this.usernameTextbox);
            this.userTab.Controls.Add(this.apikeyTextbox);
            this.userTab.Controls.Add(this.emailLabel);
            this.userTab.Controls.Add(this.apiKeyLabel);
            this.userTab.Controls.Add(this.emailTextbox);
            this.userTab.Controls.Add(this.usernameLabel);
            this.userTab.Location = new System.Drawing.Point(4, 22);
            this.userTab.Name = "userTab";
            this.userTab.Padding = new System.Windows.Forms.Padding(3);
            this.userTab.Size = new System.Drawing.Size(312, 161);
            this.userTab.TabIndex = 0;
            this.userTab.Text = "User Info";
            this.userTab.UseVisualStyleBackColor = true;
            // 
            // totalSalesLabelTextValue
            // 
            this.totalSalesLabelTextValue.AutoSize = true;
            this.totalSalesLabelTextValue.BackColor = System.Drawing.Color.YellowGreen;
            this.totalSalesLabelTextValue.ForeColor = System.Drawing.Color.Linen;
            this.totalSalesLabelTextValue.Location = new System.Drawing.Point(90, 122);
            this.totalSalesLabelTextValue.Name = "totalSalesLabelTextValue";
            this.totalSalesLabelTextValue.Padding = new System.Windows.Forms.Padding(3);
            this.totalSalesLabelTextValue.Size = new System.Drawing.Size(59, 19);
            this.totalSalesLabelTextValue.TabIndex = 8;
            this.totalSalesLabelTextValue.Text = "Unknown";
            // 
            // totalSalesLabelText
            // 
            this.totalSalesLabelText.AutoSize = true;
            this.totalSalesLabelText.Location = new System.Drawing.Point(12, 122);
            this.totalSalesLabelText.Name = "totalSalesLabelText";
            this.totalSalesLabelText.Padding = new System.Windows.Forms.Padding(3);
            this.totalSalesLabelText.Size = new System.Drawing.Size(69, 19);
            this.totalSalesLabelText.TabIndex = 7;
            this.totalSalesLabelText.Text = "Total Sales:";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(213, 119);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click_1);
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(87, 23);
            this.usernameTextbox.Multiline = true;
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(201, 20);
            this.usernameTextbox.TabIndex = 3;
            // 
            // apikeyTextbox
            // 
            this.apikeyTextbox.Location = new System.Drawing.Point(87, 53);
            this.apikeyTextbox.Name = "apikeyTextbox";
            this.apikeyTextbox.Size = new System.Drawing.Size(201, 20);
            this.apikeyTextbox.TabIndex = 4;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(16, 86);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(35, 13);
            this.emailLabel.TabIndex = 2;
            this.emailLabel.Text = "Email:";
            // 
            // apiKeyLabel
            // 
            this.apiKeyLabel.AutoSize = true;
            this.apiKeyLabel.Location = new System.Drawing.Point(16, 56);
            this.apiKeyLabel.Name = "apiKeyLabel";
            this.apiKeyLabel.Size = new System.Drawing.Size(48, 13);
            this.apiKeyLabel.TabIndex = 1;
            this.apiKeyLabel.Text = "API Key:";
            // 
            // emailTextbox
            // 
            this.emailTextbox.Location = new System.Drawing.Point(87, 83);
            this.emailTextbox.Name = "emailTextbox";
            this.emailTextbox.Size = new System.Drawing.Size(201, 20);
            this.emailTextbox.TabIndex = 5;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(15, 26);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // optionsTab
            // 
            this.optionsTab.Controls.Add(this.updateOptionsButton);
            this.optionsTab.Controls.Add(this.notifyIntervalCombo);
            this.optionsTab.Controls.Add(this.notifyIntervelLabel);
            this.optionsTab.Controls.Add(this.notifyOptionsCombo);
            this.optionsTab.Controls.Add(this.notifyOptionsLabel);
            this.optionsTab.Location = new System.Drawing.Point(4, 22);
            this.optionsTab.Name = "optionsTab";
            this.optionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.optionsTab.Size = new System.Drawing.Size(312, 161);
            this.optionsTab.TabIndex = 2;
            this.optionsTab.Text = "Options";
            this.optionsTab.UseVisualStyleBackColor = true;
            // 
            // updateOptionsButton
            // 
            this.updateOptionsButton.Location = new System.Drawing.Point(213, 119);
            this.updateOptionsButton.Name = "updateOptionsButton";
            this.updateOptionsButton.Size = new System.Drawing.Size(75, 23);
            this.updateOptionsButton.TabIndex = 4;
            this.updateOptionsButton.Text = "Update";
            this.updateOptionsButton.UseVisualStyleBackColor = true;
            this.updateOptionsButton.Click += new System.EventHandler(this.updateOptionsButton_Click);
            // 
            // notifyIntervalCombo
            // 
            this.notifyIntervalCombo.FormattingEnabled = true;
            this.notifyIntervalCombo.Items.AddRange(new object[] {
            "5",
            "30",
            "60",
            "360",
            "720",
            "1440"});
            this.notifyIntervalCombo.Location = new System.Drawing.Point(168, 60);
            this.notifyIntervalCombo.Name = "notifyIntervalCombo";
            this.notifyIntervalCombo.Size = new System.Drawing.Size(121, 21);
            this.notifyIntervalCombo.TabIndex = 3;
            // 
            // notifyIntervelLabel
            // 
            this.notifyIntervelLabel.AutoSize = true;
            this.notifyIntervelLabel.Location = new System.Drawing.Point(15, 63);
            this.notifyIntervelLabel.Name = "notifyIntervelLabel";
            this.notifyIntervelLabel.Size = new System.Drawing.Size(146, 13);
            this.notifyIntervelLabel.TabIndex = 2;
            this.notifyIntervelLabel.Text = "Notification Interval (minutes):";
            // 
            // notifyOptionsCombo
            // 
            this.notifyOptionsCombo.FormattingEnabled = true;
            this.notifyOptionsCombo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.notifyOptionsCombo.Items.AddRange(new object[] {
            "Email Only",
            "Popup Only",
            "Email & Popup"});
            this.notifyOptionsCombo.Location = new System.Drawing.Point(168, 23);
            this.notifyOptionsCombo.Name = "notifyOptionsCombo";
            this.notifyOptionsCombo.Size = new System.Drawing.Size(121, 21);
            this.notifyOptionsCombo.TabIndex = 1;
            // 
            // notifyOptionsLabel
            // 
            this.notifyOptionsLabel.AutoSize = true;
            this.notifyOptionsLabel.Location = new System.Drawing.Point(15, 26);
            this.notifyOptionsLabel.Name = "notifyOptionsLabel";
            this.notifyOptionsLabel.Size = new System.Drawing.Size(102, 13);
            this.notifyOptionsLabel.TabIndex = 0;
            this.notifyOptionsLabel.Text = "Notification Options:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.appStatusValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 200);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(329, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // appStatusValue
            // 
            this.appStatusValue.Name = "appStatusValue";
            this.appStatusValue.Size = new System.Drawing.Size(74, 17);
            this.appStatusValue.Text = "Not polling api";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.trayIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "EnvatoTracker";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClosed += new System.EventHandler(this.notifyIcon_BalloonTipClosed);
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // trayIconContextMenu
            // 
            this.trayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.viewSalesPortfolioToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.trayIconContextMenu.Name = "trayIconContextMenu";
            this.trayIconContextMenu.Size = new System.Drawing.Size(180, 76);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreToolStripMenuItem.Image")));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.restoreToolStripMenuItem.Text = "Restore App";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // schedulerWorker
            // 
            this.schedulerWorker.WorkerSupportsCancellation = true;
            this.schedulerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.schedulerWorker_DoWork);
            this.schedulerWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.schedulerWorker_RunWorkerCompleted);
            // 
            // viewSalesPortfolioToolStripMenuItem
            // 
            this.viewSalesPortfolioToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewSalesPortfolioToolStripMenuItem.Image")));
            this.viewSalesPortfolioToolStripMenuItem.Name = "viewSalesPortfolioToolStripMenuItem";
            this.viewSalesPortfolioToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.viewSalesPortfolioToolStripMenuItem.Text = "View Sales/Portfolio";
            this.viewSalesPortfolioToolStripMenuItem.Click += new System.EventHandler(this.viewSalesPortfolioToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // EnvatoTrackerFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 225);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnvatoTrackerFrame";
            this.Text = "EnvatoTracker";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Resize += new System.EventHandler(this.EnvatoTrackerFrame_Resize);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.tabPanel.ResumeLayout(false);
            this.userTab.ResumeLayout(false);
            this.userTab.PerformLayout();
            this.optionsTab.ResumeLayout(false);
            this.optionsTab.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.trayIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox emailTextbox;
        private System.Windows.Forms.TextBox apikeyTextbox;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label apiKeyLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage userTab;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel appStatusValue;
        private System.Windows.Forms.TabPage optionsTab;
        private System.Windows.Forms.ComboBox notifyOptionsCombo;
        private System.Windows.Forms.Label notifyOptionsLabel;
        private System.Windows.Forms.Label notifyIntervelLabel;
        private System.Windows.Forms.ComboBox notifyIntervalCombo;
        private System.Windows.Forms.Button updateOptionsButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.ComponentModel.BackgroundWorker schedulerWorker;
        private System.Windows.Forms.Label totalSalesLabelText;
        private System.Windows.Forms.Label totalSalesLabelTextValue;
        private System.Windows.Forms.ContextMenuStrip trayIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSalesPortfolioToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

