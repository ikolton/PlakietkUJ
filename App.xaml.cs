using System.Globalization;
using System.Windows;

namespace PlakietkUJ
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Language Setup
            var langCode = PlakietkUJ.Properties.Settings.Default.LanguageCode;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(langCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);
        }

    }

}
