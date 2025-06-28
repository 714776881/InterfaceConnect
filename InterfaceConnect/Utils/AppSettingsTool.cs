using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InterfaceConnect
{
    public class AppSettingsTool
    {
        public static Dictionary<string, string> ReadAllSettings()
        {
            var dic = new Dictionary<string, string>();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
               
                if (appSettings.Count == 0)
                {
                    Logger.LogInfo("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        dic.Add(key, appSettings[key]);
                        Logger.LogInfo(string.Format("Key: {0} Value: {1}", key, appSettings[key]));
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Logger.LogError("Error reading app settings");
            }
            return dic;
        }
        public static string ReadSetting(string key,string defaultValue = "")
        {
            string result = defaultValue;
            try
            {
                if(ConfigurationManager.AppSettings[key] != null)
                {
                    result = ConfigurationManager.AppSettings[key].ToString();
                }
            }
            catch (ConfigurationErrorsException)
            {
                Logger.LogError("Error reading app settings");
            }
            return result;
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Logger.LogError("Error writing app settings");
            }
        }
    }
}
