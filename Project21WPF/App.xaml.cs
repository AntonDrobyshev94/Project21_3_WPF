﻿using Project21WPF.UI_Interface.Authenticate;
using Project21WPF_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project21WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ContactDataApi contactDataApi = new ContactDataApi(); 
            AuthenticateWindow authenticateWindow = new AuthenticateWindow(contactDataApi); 
            authenticateWindow.Show(); 
        }
    }
}
