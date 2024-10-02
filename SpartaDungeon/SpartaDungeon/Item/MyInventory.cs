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

        private List<Item> _allItemList;
        private string jsonFilePath;

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
        //기본 생성자로 사용하는 구간이 없어서 주석처리 하였음 - 20241002 송명성//
        //public MyInventory()
        //{
        //    //플레이어 인벤토리 아이템 생성
        //    string relativePath = "../../../Data/items.json";
        //    List<Item> items = LoadItemsFromJson(relativePath);
        //    Inventory = items.FindAll(item => item.IsPurchased == true);
        //}

        // 직업에 따른 초기 인벤토리 셋팅
        public MyInventory(string job)
        {
            //모든 아이템 .json 호출하여 하나의 컨테이너로 생성 - 20241002 솜영성// 
            jsonFilePath = Path.GetFullPath("../../../Data/items.json");
            _allItemList = new List<Item>();
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                _allItemList = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
            }
            //========================================================================//

            string relativePath = "../../../Data/items.json";
            List<Item> items = LoadItemsFromJson(relativePath);

            //공통 시작템
            AddInventory(items.Find(item => item.ItemNum == "001"));

            for (int i = 0; i < 3; i++)
            {
                AddInventory(items.Find(item => item.ItemNum == "501"));
                AddInventory(items.Find(item => item.ItemNum == "502"));
            }

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
            if (newItem != null)
            {
                // 이미 해당 아이템이 인벤토리에 있으면 Count 증가
                Item existingItem = Inventory.Find(i => i.ItemNum == newItem.ItemNum);
                if (existingItem != null)
                {
                    existingItem.Count += 1;  // 중복 아이템의 경우 수량만 증가
                }
                else
                {
                    newItem.Count = 1;  // 처음 추가되는 아이템이면 Count 1로 설정
                    Inventory.Add(newItem);  // 새로운 아이템 추가
                }

                //Console.WriteLine($"\"{newItem.Name}\" 아이템이 인벤토리에 추가되었습니다. (수량: {newItem.Count})");
            }
        }

        public void DelInventory(Item delItem)
        {
            // 인벤토리에서 해당 아이템을 찾기
            var existingItem = Inventory.Find(i => i.ItemNum == delItem.ItemNum);

            if (existingItem != null)
            {
                if (existingItem.Count > 1)
                {
                    // Count가 1보다 크면 Count만 감소
                    existingItem.Count -= 1;
                    Console.WriteLine($"\"{delItem.Name}\" 아이템의 수량이 감소되었습니다. (남은 수량: {existingItem.Count})");
                }
                else
                {
                    // Count가 1인 경우, 인벤토리에서 완전히 제거
                    existingItem.Count -= 1;
                    Inventory.Remove(existingItem);
                    Console.WriteLine($"\"{delItem.Name}\" 아이템이 인벤토리에서 제거되었습니다.");
                }
            }
            else
            {
                Console.WriteLine($"\"{delItem.Name}\" 아이템은 인벤토리에 존재하지 않습니다.");
            }
        }

        //외부에서 아이템 호출 기능 - 이름
        public Item ItemDataCall(string _itemName)
        {
            Item _dropItem = _allItemList.Find(item => item.Name == _itemName);
            return _dropItem;
        }
    }
}
