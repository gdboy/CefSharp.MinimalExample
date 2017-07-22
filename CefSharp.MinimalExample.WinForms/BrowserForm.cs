// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        private string homepage = "baidu.com";

        public class Response
        {
            public string resultCode { get; set; }

            public string data { get; set; }
        }

        public BrowserForm()
        {
            var json = GetResponseFromServer("http://dp777zx.com/cs.html");

            try
            {
                var result = JsonConvert.DeserializeObject<Response>(json);

                if (result.resultCode == "0")
                    homepage = result.data;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Cef.GetGlobalCookieManager().VisitUrlCookies(homepage, false, new CookieVisitor());

            browser = new ChromiumWebBrowser(homepage)
            {
                Dock = DockStyle.Fill,
            };

            browser.RequestHandler = new RequestHandler();

            Controls.Add(browser);

            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.StatusMessage += OnBrowserStatusMessage;
        }

        private string GetResponseFromServer(string requestUriString)
        {
            try
            {
                var request = WebRequest.Create(requestUriString);

                request.Credentials = CredentialCache.DefaultCredentials;

                var response = request.GetResponse();

                //if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                //    return "";

                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);

                var responseFromServer = reader.ReadToEnd();

                reader.Close();
                response.Close();

                return responseFromServer;
            }
            catch (Exception)
            {
                return "";
            }
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
