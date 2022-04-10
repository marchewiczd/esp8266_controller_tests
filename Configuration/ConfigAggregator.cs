using ESP8266_Controller_tests.Configuration.Model;
using ESP8266_Controller_tests.Configuration.Model.Test;
using Newtonsoft.Json;
using System.IO;
using System;

namespace ESP8266_Controller_tests.Configuration
{
    public static class ConfigAggregator
    {
        private static AppSettings _appSettings;

        public static AppSettings GetConfig()
        {
            LoadConfig();
            return _appSettings;
        }

        public static T Get<T>()
        {
            object obj = null;

            LoadConfig();

            switch (typeof(T).Name)
            {
                case "AppSettings":
                    obj = _appSettings;
                    break;
                case "TestSettings":
                    obj = _appSettings.TestSettings;
                    break;
                case "WebServerSettings":
                    obj = _appSettings.TestSettings.WebServer;
                    break;
                case "SerialPortSettings":
                    obj = _appSettings.TestSettings.SerialPort;
                    break;
                default:
                    throw new ArgumentException($"{nameof(T)} is not a correct type.");
            }
            
            return (T)obj;
        }

        private static void LoadConfig()
        {
            if (_appSettings == null)
            {
                string jsonText = File.ReadAllText("appsettings.json");
                _appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonText);
            }
        }
    }
}
