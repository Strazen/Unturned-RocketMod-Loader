using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace RocketModLoader
{
    public class Configuration : IRocketPluginConfiguration
    {
        public List<string> Lisanslar = new List<string>();

        public void LoadDefaults()
        {
            Lisanslar = new List<string>
            {
                "0000-0000-0000"
            };
        }
    }
}
