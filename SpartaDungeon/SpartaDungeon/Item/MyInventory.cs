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

        // json 파일에서 데이터를 불러오는 메소드
        private List<Item> LoadItemsFromJson(string path)
        {
            string jsonFilePath = Path.GetFullPath(path);
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<List<Item>>(jsonContent);
            }
            return new List<Item>();
        }

        // 기본 생성자: 구매된 아이템만 추가
        public MyInventory()
        {
            string relativePath = "../../../Data/items.json";
            List<Item> items = LoadItemsFromJson(relativePath);
            Inventory = items.FindAll(item => item.IsPurchased == true);
        }

        // 직업에 따른 초기 인벤토리 셋팅
        public MyInventory(string job)
        {
            string relativePath = "../../../Data/items.json";
            List<Item> items = LoadItemsFromJson(relativePath);
            Inventory = items.FindAll(item => item.IsPurchased == true);

            if (job == "전사")
            {
                // 전사 시작템
                AddInventory(items.Find(item => item.ItemNum == "101"));
            }
            else if (job == "궁수")
            {
                // 궁수 시작템
                AddInventory(items.Find(item => item.ItemNum == "201"));
            }
            else if (job == "도적")
            {
                // 도적 시작템
                AddInventory(items.Find(item => item.ItemNum == "301"));
            }
            else if (job == "마법사")
            {
                // 마법사 시작템
                AddInventory(items.Find(item => item.ItemNum == "401"));
            }
        }

        public void AddInventory(Item newItem)
        {
            newItem.IsPurchased = true;
            Inventory.Add(newItem);
            Console.WriteLine($"\"{newItem.Name}\" 아이템이 인벤토리에 추가되었습니다.");

        }

        public void DelInventory (Item delItem)
        {
            if (!delItem.IsPurchased)
            {
                if (Inventory.Contains(delItem))
                {
                    Inventory.Remove(delItem);
                    Console.WriteLine($"\"{delItem.Name}\" 아이템이 인벤토리에서 제거되었습니다.");
                }
                else
                {
                    Console.WriteLine($"\"{delItem.Name}\" 아이템은 인벤토리에 존재하지 않습니다.");
                }
            }
        }
    }
}
