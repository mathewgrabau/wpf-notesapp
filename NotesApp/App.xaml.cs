using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string UserId = string.Empty;

        // TODO need to read this from environment or something? Perhaps a config file??
        // TODO make sure to remove this before committing and pushing.
        //public static MobileServiceClient MobileServiceClient = new MobileServiceClient();
    }
}
