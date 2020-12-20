using Campaignr.Core.Attributes;
using Campaignr.Core.Database.Tables;
using Campaignr.Core.Interfaces;
using Campaignr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Campaignr.Core.Helpers
{
    public class ApiHelper
    {
        private static IApiInterface _currentApi = null;

        public static IApiInterface GetCurrentApi()
        {
            if (_currentApi == null)
            {
                var apiTypes = GetApiTypes();
                var savedApi = GetSavedApi();

                if (!string.IsNullOrWhiteSpace(savedApi))
                {
                    foreach (var apiType in apiTypes)
                    {
                        try
                        {
                            var friendlyname = apiType.Name;
                            var friendlyNameAttribute = (FriendlyNameAttribute)apiType.GetCustomAttributes(typeof(FriendlyNameAttribute), false).FirstOrDefault();
                            if (friendlyNameAttribute != null)
                            {
                                friendlyname = friendlyNameAttribute.Value;
                                if (friendlyname == savedApi)
                                {
                                    _currentApi = (IApiInterface)Activator.CreateInstance(apiType);
                                    return _currentApi;
                                }
                            }

                        }
                        catch { }
                    }
                }
            }

            return _currentApi;
        }

        public static Settings GetSettings()
        {
            Settings response = new Settings();

            response.Name = SettingsTableStore.GetSettingValue("ApiName");
            response.Username = SettingsTableStore.GetSettingValue("ApiUsername");
            response.Password = SettingsTableStore.GetSettingValue("ApiPassword");

            response.Apis = GetApiNames();

            if (!string.IsNullOrWhiteSpace(response.Name) && !string.IsNullOrWhiteSpace(response.Username) && !string.IsNullOrWhiteSpace(response.Password))
            {
                response.ValidSettings = true;
            }

            return response;
        }

        public static bool SaveSettings(Settings settings)
        {
            if (settings != null)
            {
                SettingsTableStore.SaveSetting("ApiName", settings.Name);
                SettingsTableStore.SaveSetting("ApiUsername", settings.Username);
                SettingsTableStore.SaveSetting("ApiPassword", settings.Password);

                _currentApi = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads currently selected api provider name from saved config.
        /// </summary>
        /// <returns></returns>
        public static string GetSavedApi()
        {
            string apiName = SettingsTableStore.GetSettingValue("ApiName");

            if (!string.IsNullOrWhiteSpace(apiName))
            {
                return apiName;
            }

            return "";
        }

        public static List<string> GetApis()
        {
            var apiTypes = GetApiClasses();

            return apiTypes.Keys.ToList();
        }

        public static List<string> GetApiNames()
        {
            var apiTypes = GetApiTypes();
            List<string> names = new List<string>();

            foreach (var apiType in apiTypes)
            {
                try
                {
                    var friendlyname = apiType.Name;
                    var friendlyNameAttribute = (FriendlyNameAttribute)apiType.GetCustomAttributes(typeof(FriendlyNameAttribute), false).FirstOrDefault();
                    if (friendlyNameAttribute != null)
                    {
                        friendlyname = friendlyNameAttribute.Value;
                    }
                    names.Add(friendlyname);

                }
                catch { }
            }

            return names;
        }

        private static List<Type> GetApiTypes()
        {
            var apiTypes = GetApiClasses();
            List<Type> types = new List<Type>();
            bool enabled = false;

            foreach (var apiTypeKey in apiTypes.Keys)
            {
                var friendlyname = apiTypeKey;
                var apiType = apiTypes[apiTypeKey];

                try
                {
                    var friendlyNameAttribute = (FriendlyNameAttribute)apiType.GetCustomAttributes(typeof(FriendlyNameAttribute), false).FirstOrDefault();
                    if (friendlyNameAttribute != null)
                    {
                        friendlyname = friendlyNameAttribute.Value;
                    }

                    var enabledAttribute = (EnabledAttribute)apiType.GetCustomAttributes(typeof(EnabledAttribute), false).FirstOrDefault();
                    if (enabledAttribute != null)
                    {
                        enabled = enabledAttribute.Value;
                    }
                }
                catch { }

                if (enabled)
                {
                    types.Add(apiType);
                }
            }

            return types;
        }

        private static Dictionary<string, Type> GetApiClasses()
        {
            return Assembly.GetAssembly(typeof(IApiInterface))
                .GetTypes()
                .Where(x => typeof(IApiInterface).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToDictionary(x => x.Name, x => x);
        }
    }
}