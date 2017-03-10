using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCommonLisa
{
    public static class Tools
    {
        public static string getConfig(string key)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
        }
    }
}
