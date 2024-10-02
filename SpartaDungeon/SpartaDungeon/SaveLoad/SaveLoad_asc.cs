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
            string jsonStr = JsonConvert.SerializeObject(t,Formatting.Indented);
            File.WriteAllText(relativePath, jsonStr);
        }

        public T? LoadData<T>(string name) where T : class
        {
            string relativePath = @$"../../../Data/{name}.json";

            if (!File.Exists(relativePath))
            {
                return null;
            }
            else
            {
                string jsonStr = File.ReadAllText(relativePath);
                T t = JsonConvert.DeserializeObject<T>(jsonStr);
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
