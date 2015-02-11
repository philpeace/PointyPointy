using System.Collections.Generic;

namespace CodePeace.Common.Configuration
{
    public interface IAppSettingsConfigurationManager : IDependency
    {
        T Setting<T>(string key);

        string Setting(string key);

        IEnumerable<T> AllSettings<T>(string stub);
    }
}