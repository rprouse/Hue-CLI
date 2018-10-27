using System;
using System.Drawing;
using System.IO;
using Console = Colorful.Console;

namespace alteridem.hue.cli
{
    class KeyManager
    {
        string _ip;

        public KeyManager(string ip)
        {
            _ip = ip;
        }

        public string LoadKey()
        {
            string keyfile = KeyFilename;

            if (!File.Exists(keyfile))
                return null;

            try
            {
                using (var reader = new StreamReader(keyfile))
                {
                    var key = reader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(key))
                        return key;
                }
            }
            catch { }

            return null;
        }

        public bool SaveKey(string key)
        {
            try
            {
                using (var writer = new StreamWriter(KeyFilename, false))
                {
                    writer.Write(key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to save bridge registration.", Color.Red);
                Console.WriteLine(e.Message, Color.Red);
                return false;
            }
            return true;
        }

        string KeyFilename =>
            Path.Combine(GetAppDirectory(), $"{_ip}.key");

        string GetAppDirectory()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "alteridem", "hue");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
