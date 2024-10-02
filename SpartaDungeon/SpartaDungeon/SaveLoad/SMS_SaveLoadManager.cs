using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace SpartaDungeon
{
    internal class SMS_SaveLoadManager
    {
        string _path;

        public void SMS_Load(ref Character _curPlayer)
        {
            string jsonFilePath = Path.GetFullPath(@"..\..\..\Data\SaveLoadData.json");

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                //Character _loadData = JsonConvert.DeserializeObject<Character>(jsonContent, settings);
                _curPlayer =  JsonConvert.DeserializeObject<Character>(jsonContent, settings);
                _curPlayer.LoadData();
            }
        }

        public void SMS_Save(Character _curPlayer)
        {
            string jsonFilePath = Path.GetFullPath(@"..\..\..\Data\SaveLoadData.json");

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                //Character _loadData = JsonConvert.DeserializeObject<Character>(jsonContent, settings);
                string _saveDataStr = JsonConvert.SerializeObject(_curPlayer, Formatting.Indented);

                File.WriteAllText(jsonFilePath, _saveDataStr);
            }

        }
    }
}
