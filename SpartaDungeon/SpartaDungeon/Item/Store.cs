using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpartaDungeon
{

    internal class Store
    {
        public List<Item> StoreItemList = new List<Item>();
        public List<Item> SelectTypeItemList = new List<Item>();

        public Store()
        {

            //WarriorItemList = new List<Item>();
            //MageItemList = new List<Item>();

            //json데이터 넣어주기
            string relativePath = "../../../Data/items.json";
            string jsonFilePath = Path.GetFullPath(relativePath);
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                StoreItemList = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
            }

        }

        void ShowStoreItemList(ITEMTYPE itemType)
        {

            Console.WriteLine();
            Console.WriteLine($"{itemType}[상점 아이템 목록]");


            SelectTypeItemList = StoreItemList.Where(item => item.ItemType == itemType).ToList();

            for (int i = 0; i < SelectTypeItemList.Count; i++)
            {
                string completedPurchase = "";
                if (SelectTypeItemList[i].IsPurchased == true)
                {
                    completedPurchase = "(구매완료)";
                }

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($" {completedPurchase}[{i + 1}] {SelectTypeItemList[i].Name}({SelectTypeItemList[i].ItemTypeKorean})" +
                                  $" | {SelectTypeItemList[i].Description}" +
                                  $" | 공격력 +{SelectTypeItemList[i].Atk}" +
                                  $" 방어력 +{SelectTypeItemList[i].Def}" +
                                  $" 추가체력 +{SelectTypeItemList[i].AdditionalHP}" +
                                  $" | {SelectTypeItemList[i].Price}G |");
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void EnterStore(Character player)
        {
            if (player.Job == "전사")
            {
                StoreItemList = StoreItemList.Where(item => item.ItemNum.StartsWith("1")
                    || item.ItemNum.StartsWith("0")).ToList();

            }
            else if (player.Job == "궁수")
            {
                StoreItemList = StoreItemList.Where(item => item.ItemNum.StartsWith("2")
                    || item.ItemNum.StartsWith("0")).ToList();
            }
            else if (player.Job == "도적")
            {
                StoreItemList = StoreItemList.Where(item => item.ItemNum.StartsWith("3")
                    || item.ItemNum.StartsWith("0")).ToList();
            }
            else if (player.Job == "마법사")
            {
                StoreItemList = StoreItemList.Where(item => item.ItemNum.StartsWith("4")
                    || item.ItemNum.StartsWith("0")).ToList();
            }

            while (true)
            {
                Console.WriteLine("상점 아이템 리스트에서 구매하거나, 소유한 아이템을 판매할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[1] 구매\n");
                Console.WriteLine("[2] 판매\n");
                Console.WriteLine("[0] 나가기\n");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "1")
                {
                    Console.Clear();
                    ChoosePurchaseType(player);
                }
                else if (input == "2")
                {
                    Console.Clear();
                    SellItem(player);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        //구매선택
        public void ChoosePurchaseType(Character player)
        {
            while (true)
            {

                Console.WriteLine("상점 아이템 리스트에서 구매하거나, 소유한 아이템을 판매할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[1] 무기 구매\n");
                Console.WriteLine("[2] 갑옷 구매\n");
                Console.WriteLine("[3] 회복아이템 구매\n");
                Console.WriteLine("[0] 나가기\n");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "1")
                {
                    Console.Clear();
                    PurchaseItem(player, ITEMTYPE.MainWeapon);
                }
                else if (input == "2")
                {
                    Console.Clear();
                    PurchaseItem(player, ITEMTYPE.Armor);
                }
                else if (input == "3")
                {
                    Console.Clear();
                    PurchaseItem(player, ITEMTYPE.HealingItem);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        //장비아이템구매
        public void PurchaseItem(Character player, ITEMTYPE itemType)
        {
            while (true)
            {
                Console.WriteLine("아이템 리스트에서 아이템을 확인하고 구매하고자 하는 아이템의 번호를 입력해주세요.");
                Console.WriteLine($"현재 소지금: {player.Gold}G");

                ShowStoreItemList(itemType);

                Console.WriteLine("[0] 나가기\n");
                Console.Write(">> ");
                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);
                if (isNum)
                {
                    if (select == 0)
                    {
                        Console.Clear();
                        break;
                    }
                    else if (select > 0 && select <= SelectTypeItemList.Count)
                    {
                        if (player.Gold >= SelectTypeItemList[select - 1].Price && SelectTypeItemList[select - 1].IsPurchased == false)
                        {
                            SelectTypeItemList[select - 1].IsPurchased = true;
                            player.Gold -= SelectTypeItemList[select - 1].Price;
                            player.myInventory.Inventory.Add(SelectTypeItemList[select - 1]);
                            Console.Clear();
                            Console.WriteLine($"\"{SelectTypeItemList[select - 1].Name}\" 을 구매했습니다. 인벤토리를 확인해보세요.");
                            Console.WriteLine();
                        }
                        else if (SelectTypeItemList[select - 1].IsPurchased == true)
                        {
                            Console.Clear();
                            Console.WriteLine("이미 구매된 아이템입니다.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.WriteLine();
                }
            }
        }

        public void SellItem(Character player)
        {
            while (true)
            {
                Console.WriteLine("소유한 아이템을 정가의 85%로 판매할 수 있습니다.");
                Console.WriteLine("현재 소지금: {0}G", player.Gold);
                Console.WriteLine();
                Console.WriteLine("내 인벤토리");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < player.myInventory.Inventory.Count; i++)
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($" [{i + 1}] {player.myInventory.Inventory[i].Name}({player.myInventory.Inventory[i].ItemTypeKorean})" +
                                      $" | {player.myInventory.Inventory[i].Description}" +
                                      $" | 공격력 +{player.myInventory.Inventory[i].Atk}" +
                                      $" 방어력 +{player.myInventory.Inventory[i].Def}" +
                                      $" 추가체력 +{player.myInventory.Inventory[i].AdditionalHP}" +
                                      $" | 판매 금액: {(int)(player.myInventory.Inventory[i].Price * 0.85f)}G |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.WriteLine("아이템 번호을 입력하면 판매됩니다.");
                Console.WriteLine();
                Console.Write(">> ");
                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0)
                    {
                        Console.Clear();
                        break;
                    }
                    else if (select > 0 && select <= player.myInventory.Inventory.Count)
                    {
                        player.myInventory.Inventory[select - 1].IsPurchased = false;
                        player.Gold += (int)(player.myInventory.Inventory[select - 1].Price * 0.85f);
                        AddSellItemToStoreItemList(player.myInventory.Inventory[select - 1]);
                        UnWearItemSell(player, player.myInventory.Inventory[select - 1]);
                        Console.Clear();
                        Console.WriteLine($"\"{player.myInventory.Inventory[select - 1].Name}\" 을" +
                                          $"{(int)(player.myInventory.Inventory[select - 1].Price * 0.85f)}G 에 판매하셨습니다.");
                        Console.WriteLine();
                        player.myInventory.Inventory.RemoveAt(select - 1);                        
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.WriteLine();
                }
            }
        }

        // 판매한 아이템이 상점 아이템 리스트에 있는지 체크
        void AddSellItemToStoreItemList(Item sellItem)
        {
            bool isExist = false;
            foreach (Item item in StoreItemList)
            {
                if (item == sellItem)
                {
                    isExist = true;
                }
            }
            if (!isExist)
            {
                StoreItemList.Add(sellItem);
            }
        }

        void UnWearItemSell(Character player, Item sellItem)
        {
            if (sellItem == player.EquipWeapon)
            {
                player.CurrentHp -= sellItem.AdditionalHP;
                player.EquipWeapon = new Item();
            }
            else if (sellItem == player.EquipArmor)
            {
                player.CurrentHp -= sellItem.AdditionalHP;
                player.EquipArmor = new Item();
            }
        }
    }
}
