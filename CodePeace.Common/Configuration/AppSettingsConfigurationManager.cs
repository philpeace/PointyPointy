using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CodePeace.Common.Configuration
{
    public class AppSettingsConfigurationManager : IAppSettingsConfigurationManager
    {
        public T Setting<T>(string key)
        {
            object value = ConfigurationManager.AppSettings[key];
            return (value != null) ? (T)Convert.ChangeType(value, typeof(T)) : default(T);
        }

        public string Setting(string key)
        {
            return Setting<string>(key);
        }

        public IEnumerable<T> AllSettings<T>(string stub)
        {
            return ConfigurationManager.AppSettings.Cast<KeyValuePair<string, string>>()
                .Where(c => c.Key.StartsWith(stub))
                .Select(c => (T)Convert.ChangeType(c.Value, typeof(T)));
        }
    }
}