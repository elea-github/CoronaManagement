using Newtonsoft.Json;
using System;
using System.IO;

namespace CoronaManagementSystemHMO
{

    public class ConnectionStrings { public string DefaultRDSConn { get; set; } }

    public static class Config
    {
        public static void LoadConfigurations()
        {
        }

        private static void LoadConfigurationsWithSync()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "appsettings.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var content = JsonConvert.DeserializeObject<dynamic>(json);
                var re = content.AppSettings;

                // load each setting and set them
                foreach (var row in re)
                {

                }
            }           
        }
    }
}
