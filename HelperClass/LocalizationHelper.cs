using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PlakietkUJ.HelperClass
{
    public class LocalizationHelper
    {
        private static ResourceManager resourceManager;


        static LocalizationHelper()
        {
            
            resourceManager = new ResourceManager("PlakietkUJ.Resources.Resources", typeof(LocalizationHelper).Assembly);

            

        }


        public static void ChangeCulture(string culture)
        {
            Properties.Settings.Default.LanguageCode = culture;
            Properties.Settings.Default.Save();
        }

        public static string GetLocalizedString(string key)
        {

            string localizedString = resourceManager.GetString(key);
            
            return localizedString ?? key;
        }
    }
}
