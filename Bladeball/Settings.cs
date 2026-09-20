using System;
using System.IO;
using System.Web.Script.Serialization;

namespace Bladeball
{
    public class Settings
    {
        public static readonly string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bladeball");
        public static readonly string file = Path.Combine(folder, "settings.json");

        public static SettingsData data = new SettingsData();


        public static void SaveSettings()
        {
            Directory.CreateDirectory(folder);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string js = serializer.Serialize(data);

            File.WriteAllText(file, js);
        }

        public static void LoadSettings()
        {
            Directory.CreateDirectory(folder);

            if (!File.Exists(file))
            {
                SaveSettings();
                return;
            }

            try
            {
                string js = File.ReadAllText(file);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                var read = serializer.Deserialize<SettingsData>(js);

                if (read != null)
                    data = read;

                else
                {
                    data = new SettingsData();
                }
            }
            catch
            {
                data = new SettingsData();
                SaveSettings();
            }
        }

        public class SettingsData
        {
            public int CPS { get; set; } = 25;
            public byte BlockKey1 { get; set; } = 0;
            public byte BlockKey2 { get; set; } = 0;
            public int StartKey { get; set; } = 70;
            public string BlockKey1Text { get; set; } = "None";
            public string BlockKey2Text { get; set; } = "None";
            public string StartKeyText { get; set; } = "F";
        }
    }
}
