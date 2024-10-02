using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.SaveLoad
{
    internal class SaveLoad_asc
    {
        // player data
        // equipitem
        // inventory
        // skilldeck
        // playerquest

        // questscene
        // _curSelectArea
        
        public void SaveData<T>(T t, string name) where T : class
        {
            string relativePath = @$"../../../Data/{name}.json";
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string jsonStr = JsonConvert.SerializeObject(t,Formatting.Indented, settings);
            File.WriteAllText(relativePath, jsonStr);
        }

        public T? LoadData<T>(string name) where T : class
        {
            string relativePath = @$"../../../Data/{name}.json";

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (!File.Exists(relativePath))
            {
                return null;
            }
            else
            {
                string jsonStr = File.ReadAllText(relativePath);
                T t = JsonConvert.DeserializeObject<T>(jsonStr, settings);
                if (t == null)
                {
                    return null;
                }
                else
                {
                    return t;
                }
            }

        }
    }
}
