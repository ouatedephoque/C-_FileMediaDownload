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

        // Valeurs par défaut
        private String IpComputerDefault = "";

        public Settings()
        {
            this.Parameters = ApplicationData.Current.LocalSettings;
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
    }
}
