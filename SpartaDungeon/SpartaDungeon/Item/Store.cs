﻿using System;
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
        public List<Item> PlayerInventory = new List<Item>();

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

        void ShowStoreItemList(Character player, ITEMTYPE itemType)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"{itemType}[상점 아이템 목록]");
            Console.ResetColor();

            SelectTypeItemList = StoreItemList.FindAll(item => item.ItemType == itemType && item.IsNotForSale == false);
            PlayerInventory = player.myInventory.Inventory;

            for (int i = 0; i < SelectTypeItemList.Count; i++)
            {
                string completedPurchase = "";
                
                var matchedItem = PlayerInventory.Find(item => item.ItemNum == SelectTypeItemList[i].ItemNum);
                if (matchedItem != null && matchedItem.Count > 0)
                {
                    completedPurchase = $"({matchedItem.Count}개 보유)";
                }
                Console.ResetColor();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" [{i + 1}] {SelectTypeItemList[i].Name}" +
                                  $" | {SelectTypeItemList[i].Description}" +
                                  $" | 공격력 +{SelectTypeItemList[i].Atk}" +
                                  $" 방어력 +{SelectTypeItemList[i].Def}" +
                                  $" 추가체력 +{SelectTypeItemList[i].AdditionalHP}" +
                                  $" | {SelectTypeItemList[i].Price}G |" +
                                  $" \t{completedPurchase}");
            }
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void EnterStore(Character player)
        {
            List<Item>filteredItems = StoreItemList.Where(item => item.ItemNum.StartsWith("5") || item.ItemNum.StartsWith("0")).ToList();

            if (player.Job == "전사")
            {
                filteredItems.AddRange(StoreItemList.Where(item => item.ItemNum.StartsWith("1")));
            }
            else if (player.Job == "궁수")
            {
                filteredItems.AddRange(StoreItemList.Where(item => item.ItemNum.StartsWith("2")));
            }
            else if (player.Job == "도적")
            {
                filteredItems.AddRange(StoreItemList.Where(item => item.ItemNum.StartsWith("3")));
            }
            else if (player.Job == "마법사")
            {
                filteredItems.AddRange(StoreItemList.Where(item => item.ItemNum.StartsWith("4")));
            }

            StoreItemList = filteredItems;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("상점 아이템 리스트에서 구매하거나, 소유한 아이템을 판매할 수 있습니다.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[1] 구매\n");
                Console.WriteLine("[2] 판매\n");
                Console.WriteLine("[0] 나가기\n");

                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }
            }
        }

        //구매선택
        public void ChoosePurchaseType(Character player)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("상점 아이템 리스트에서 구매하거나, 소유한 아이템을 판매할 수 있습니다.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[1] 무기 구매\n");
                Console.WriteLine("[2] 갑옷 구매\n");
                Console.WriteLine("[3] 회복아이템 구매\n");
                Console.WriteLine("[0] 나가기\n");

                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }
            }
        }

        //장비아이템구매
        public void PurchaseItem(Character player, ITEMTYPE itemType)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 리스트에서 아이템을 확인하고 구매하고자 하는 아이템의 번호를 입력해주세요.");
                Console.WriteLine($"현재 소지금: {player.Gold}G");

                Console.ResetColor();
                ShowStoreItemList(player, itemType);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[0] 나가기\n");

                Console.ResetColor();
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
                        if (player.Gold >= SelectTypeItemList[select - 1].Price)
                        {
                            player.Gold -= SelectTypeItemList[select - 1].Price;
                            player.myInventory.AddInventory(SelectTypeItemList[select - 1]);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\"{SelectTypeItemList[select - 1].Name}\" 을 구매했습니다. 인벤토리를 확인해보세요.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Gold가 부족합니다.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("소유한 아이템을 정가의 50%로 판매할 수 있습니다.");

                Console.ResetColor();
                Console.WriteLine("현재 소지금: {0}G", player.Gold);
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("내 인벤토리");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < player.myInventory.Inventory.Count; i++)
                {
                    Console.ResetColor();
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" [{i + 1}] {player.myInventory.Inventory[i].Name}" +
                                      $" | {player.myInventory.Inventory[i].Description}" +
                                      $" | 공격력 +{player.myInventory.Inventory[i].Atk}" +
                                      $" 방어력 +{player.myInventory.Inventory[i].Def}" +
                                      $" 추가체력 +{player.myInventory.Inventory[i].AdditionalHP}" +
                                      $" | 판매 금액: {(int)(player.myInventory.Inventory[i].Price * 0.5f)}G |" +
                                      $"  {player.myInventory.Inventory[i].Count} 개 |");
                }
                Console.ResetColor();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 번호을 입력하면 판매됩니다.");
                Console.WriteLine();

                Console.ResetColor();
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
                        player.Gold += (int)(player.myInventory.Inventory[select - 1].Price * 0.5f);
                        UnWearItemSell(player, player.myInventory.Inventory[select - 1]);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\"{player.myInventory.Inventory[select - 1].Name}\" 을" +
                                          $"{(int)(player.myInventory.Inventory[select - 1].Price * 0.5f)}G 에 판매하셨습니다.");
                        Console.WriteLine();

                        //아이템이 2개 이상인경우 1개만 판매하게 되는 로직
                        if (player.myInventory.Inventory[select - 1].Count > 1)
                        {
                            player.myInventory.Inventory[select - 1].Count--;
                        }
                        else
                        {
                            player.myInventory.Inventory.RemoveAt(select - 1);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.ResetColor();
                    Console.WriteLine();
                }
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
