using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppFMD
{
    class Settings
    {
        private ApplicationDataContainer Parameters;

        // Clé des paramètres
        private const String IpComputerKeyName = "IPComputer";
        private const String PathComputerKeyName = "PathComputer";

        // Valeurs par défaut
        private String IpComputerDefault = "";
        private String PathComputerDefault = "";

        private static Settings instance;

        private Settings()
        {
            this.Parameters = ApplicationData.Current.LocalSettings;
        }

        public static Settings Instance
        {
            get{
                if(instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }

        private void SaveOrAddParameters(string name, string value)
        {
            if (this.Parameters.Values[name] == null)
            {
                this.Parameters.Values.Add(name, value);
            }
            else
            {
                this.Parameters.Values[name] = value;
            }
        }

        public T GetValueOrDefault<T>(String name, T defaultValue)
        {
            T value;

            if (this.Parameters.Values[name] == null)
            {
                value = defaultValue;
            }
            else
            {
                value = (T) this.Parameters.Values[name];
            }

            return value;
        }

        public String IpComputer
        {
            get
            {
                return GetValueOrDefault<String>(IpComputerKeyName, IpComputerDefault) + ":51589";
            }
            set
            {
                SaveOrAddParameters(IpComputerKeyName, value);
            }
        }

        public String PathComputer
        {
            get
            {
                return GetValueOrDefault<String>(PathComputerKeyName, PathComputerDefault);
            }
            set
            {
                SaveOrAddParameters(PathComputerKeyName, value);
            }
        }
    }
}
