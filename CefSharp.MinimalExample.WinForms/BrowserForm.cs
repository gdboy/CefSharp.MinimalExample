// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.WinForms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        private string homepage = "baidu.com";

        public BrowserForm()
        {
            browser = new ChromiumWebBrowser(homepage)
            {
                Dock = DockStyle.Fill,
            };

            Controls.Add(browser);

            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.StatusMessage += OnBrowserStatusMessage;
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = args.CanGoBack);
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => reloadButton.Visible = args.CanReload);
            this.InvokeOnUiThreadIfRequired(() => stopButton.Visible = !args.CanReload);
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => toolStripStatusLabel.Text = args.Value);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            browser.Stop();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            LoadUrl(homepage);
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }
    }
}
