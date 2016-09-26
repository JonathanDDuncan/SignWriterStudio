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
        public readonly ChromiumWebBrowser browser;

        public BrowserForm()
        {
            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;



            String page = string.Format(@"{0}\dist\index.html", Application.StartupPath);
            browser = new ChromiumWebBrowser(page)
            {
                Dock = DockStyle.Fill,
            };

            // Allow the use of local resources in the browser
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            browser.BrowserSettings = browserSettings;



            toolStripContainer.ContentPanel.Controls.Add(browser);

            browser.TitleChanged += OnBrowserTitleChanged;


            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion,
                Cef.CefVersion, Cef.CefSharpVersion, bitness);
        }
 
        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }
 
    }
}
