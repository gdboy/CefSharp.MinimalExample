using CefSharp.WinForms;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    partial class BrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            
            toolStrip = new ToolStrip();

            backButton = new ToolStripButton();
            forwardButton = new ToolStripButton();
            reloadButton = new ToolStripButton();
            stopButton = new ToolStripButton();
            homeButton = new ToolStripButton();

            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();

            toolStrip.Items.AddRange(new ToolStripItem[] { backButton, forwardButton, reloadButton, stopButton, homeButton });

            backButton.Text = "后退";
            backButton.Image = Properties.Resources.nav_left_green;
            forwardButton.Text = "前进";
            forwardButton.Image = Properties.Resources.nav_right_green;
            reloadButton.Text = "刷新";
            reloadButton.Image = Properties.Resources.nav_plain_green;
            stopButton.Text = "停止";
            stopButton.Image = Properties.Resources.nav_plain_red;
            homeButton.Text = "主页";

            backButton.Enabled = false;
            forwardButton.Enabled = false;

            backButton.Click += new System.EventHandler(BackButton_Click);
            forwardButton.Click += new System.EventHandler(ForwardButton_Click);
            reloadButton.Click += new System.EventHandler(ReloadButton_Click);
            stopButton.Click += new System.EventHandler(StopButton_Click);
            homeButton.Click += new System.EventHandler(HomeButton_Click);

            statusStrip.Items.Add(toolStripStatusLabel);

            Controls.AddRange(new Control[] { toolStrip, statusStrip });
        }

        #endregion
        
        private ToolStrip toolStrip;

        private ToolStripButton backButton, forwardButton, reloadButton, stopButton, homeButton;

        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
    }
}