using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSEAktarimConsole.Config;

namespace TSEAktarimConsole.RefreshTime
{
    public class ConfigValues
    {

        public bool SicilRefreshEnabled { get; set; }
        public bool BirimRefreshEnabled { get; set; }
        public bool SicilIzinRefreshEnabled { get; set; }
        public EntegrationTimeFormat BirimRefreshTime { get; set; }
        public EntegrationTimeFormat SicilIzinRefreshTime { get; set; }
        public EntegrationTimeFormat SicilRefreshTime { get; set; }

        public int DayCountToRequestForms { get; set; }
        public ConfigValues()
        {
            //bools
            this.SicilRefreshEnabled = GetBoolVal("SicilRefreshEnabled");
            this.BirimRefreshEnabled = GetBoolVal("BirimRefreshEnabled");
            this.SicilIzinRefreshEnabled = GetBoolVal("SicilIzinRefreshEnabled");

            //bools
            this.SicilRefreshTime = GetEntegrationTimes("SicilRefreshTime");
            this.BirimRefreshTime = GetEntegrationTimes("BirimRefreshTime");
            this.SicilIzinRefreshTime = GetEntegrationTimes("SicilIzinRefreshTime");

  
        }
        private static string GetVal(string val)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return config.AppSettings.Settings[val].Value;


        }
        private EntegrationTimeFormat GetEntegrationTimes(string ValKey)
        {
            EntegrationTimeFormat e = new EntegrationTimeFormat(GetVal(ValKey));
            return e;
        }
        private int GetIntVal(string KeyName)
        {
            return Convert.ToInt32(GetVal(KeyName));
        }
        private bool GetBoolVal(string KeyName)
        {
            return Convert.ToBoolean(Convert.ToInt16(GetVal(KeyName)));
        }
  
        public static string ReCalculate()
        {

            return GetVal("StartReCalculate");
        }
        public static void SetReCalculateStatusAsCompeleted()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["StartReCalculate"].Value = "0";
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}

