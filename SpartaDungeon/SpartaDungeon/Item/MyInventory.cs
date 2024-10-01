using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace SpartaDungeon
{
    internal class MyInventory
    {
        public List<Item> Inventory = new List<Item>();

        public MyInventory() 
        {
            //json데이터 넣어주기
            string relativePath = "../../../Data/items.json";
            string jsonFilePath = Path.GetFullPath(relativePath);
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                Inventory = items.FindAll(item => item.IsPurchased == true);
            }
            

        }

        public void AddInventory(Item newItem)
        {
            if (newItem.IsPurchased)
            {
                Inventory.Add(newItem);
                Console.WriteLine($"\"{newItem.Name}\" 아이템이 인벤토리에 추가되었습니다.");
            }

        }
    }
}
