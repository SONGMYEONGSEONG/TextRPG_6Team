using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using SpartaDungeon;

namespace SpartaDungeon
{
    internal class MyInventory
    {
        public List<Item> Inventory = new List<Item>();

        public MyInventory() 
        {
            //json데이터 넣어주기
            string relativePath = "../../../Data/inventory.json";
            string jsonFilePath = Path.GetFullPath(relativePath);
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                Inventory = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
            }
        }

        public void AddInventory()
        { 
            
        }

       

        //public void ShowInventory(Character player)
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("[테스트]" + player.Name + "의 인벤토리");
        //        Console.WriteLine();
        //        Console.WriteLine("[아이템 목록]");
        //        for (int i = 0; i < Inventory.Count; i++)
        //        {
        //            string itemEquipState = (Inventory[i] == player.EquipWeapon || Inventory[i] == player.EquipArmor) ? "[E]" : "";

        //            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        //            Console.WriteLine($" [{i + 1}] {itemEquipState}{Inventory[i].Name}({Inventory[i].ItemTypeKorean})" +
        //                              $" | {Inventory[i].Description}" +
        //                              $" | 공격력 +{Inventory[i].Atk}" +
        //                              $" 방어력 +{Inventory[i].Def}" +
        //                              $" 추가체력 +{Inventory[i].AdditionalHP}" +
        //                              $" | {Inventory[i].Price}G |");
        //        }
        //        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        //        Console.WriteLine();
        //        Console.WriteLine("아이템 번호를 입력하면 해당 아이템을 장착하거나 해제할 수 있습니다.");
        //        Console.WriteLine("주무기, 보조무기, 갑옷을 한 개씩 장착할 수 있습니다.");
        //        Console.WriteLine();
        //        Console.WriteLine("[0] 나가기");
        //        Console.WriteLine();
        //        Console.Write(">> ");

        //        string input = Console.ReadLine();

        //        int select;
        //        bool isNum = int.TryParse(input, out select);

        //        if (isNum)
        //        {
        //            if (select == 0) break;
        //            else if (select > 0 && select <= Inventory.Count && Inventory[select - 1].ItemType == ITEMTYPE.HealingItem)
        //            {
        //                Console.Clear();
        //                if (player.CurrentHp == player.TotalMaxHp)
        //                {
        //                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        //                    Console.WriteLine("이미 최대 체력 상태입니다.");
        //                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        //                }
        //                else
        //                {
        //                    RecoverySystem recovery = new RecoverySystem(player.TotalMaxHp, player.CurrentHp);
        //                    player.CurrentHp = recovery.HpRecover(Inventory[select - 1].ItemNum);
        //                    Console.WriteLine($"현재 체력: {player.CurrentHp}/{player.TotalMaxHp}");
        //                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        //                }

        //            }
        //            else if (select > 0 && select <= Inventory.Count)
        //            {
        //                player.ManageItemEquip(Inventory[select - 1]);
        //                Console.Clear();
        //            }
        //            else
        //            {
        //                Console.Clear();
        //                Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
        //                Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
        //                Console.WriteLine();
        //            }
        //        }
        //        else
        //        {
        //            Console.Clear();
        //            Console.WriteLine("잘못된 입력입니다.");
        //            Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
        //            Console.WriteLine();
        //        }
        //    }
        //    Console.Clear();
        //}
    }
}
