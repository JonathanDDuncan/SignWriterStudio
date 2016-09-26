// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            //Perform dependency check to make sure all relevant resources are in our output directory.


            var settings = new CefSettings { RemoteDebuggingPort = 8088 };

            Cef.Initialize(settings, shutdownOnProcessExit: false, performDependencyCheck: true);

            var browser = new BrowserForm();
            Application.Run(browser);
        }

        public static BrowserForm GetBrowserForm()
        {
            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            //Perform dependency check to make sure all relevant resources are in our output directory.


            var settings = new CefSettings { RemoteDebuggingPort = 8088 };
            if (!Cef.IsInitialized)
                Cef.Initialize(settings, shutdownOnProcessExit: false, performDependencyCheck: true);

            var browserform = new BrowserForm();
           
            return browserform;
           
        }
    }
}
