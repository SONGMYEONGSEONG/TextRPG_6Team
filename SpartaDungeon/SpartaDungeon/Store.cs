using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Store
    {
        public List<Item> WarriorItemList;
        public List<Item> MageItemList;
        public List<Item> StoreItemList = new List<Item>();

        Item warriorLegendMainWeapon = new Item(ITEMTYPE.MainWeapon, "전설 소드", 3000, 30, 5, 15, "사용자를 전설로 만들어주는 검",   false);
        Item mageLegendMainWeapon    = new Item(ITEMTYPE.MainWeapon, "전설 완드", 3000, 30, 5, 15, "사용자를 전설로 만들어주는 완드", false);
        Item legendSubWeapon         = new Item(ITEMTYPE.SubWeapon,  "전설 단검", 2500, 20, 3, 5,  "모든 것을 꿰뚫는 전설적인 단검",  false);
        Item legendArmor             = new Item(ITEMTYPE.Armor,      "전설 아머", 3000, 5, 15, 50, "사용자를 지키는 전설적인 아머",   false);
        Item warriorRareMainWeapon   = new Item(ITEMTYPE.MainWeapon, "희귀 소드", 1000, 8, 1, 5,   "전설에 가까워지게 하는 검",      false);
        Item mageRareMainWeapon      = new Item(ITEMTYPE.MainWeapon, "희귀 완드", 1000, 8, 1, 5,   "전설에 가까워지게 하는 완드",     false);
        Item rareSubWeapon           = new Item(ITEMTYPE.SubWeapon,  "희귀 단검", 700,  5, 1, 0,   "근접한 적을 찌르는 숨겨진 비수",  false);
        Item rareArmor               = new Item(ITEMTYPE.Armor,      "희귀 갑옷", 1000, 0, 8, 20,  "쉽게 부서지지 않는 단단한 갑옷",  false);


        // 직업클래스마다 다른 리스트 생성
        public Store()
        {
            WarriorItemList = new List<Item>();
            MageItemList = new List<Item>();

            WarriorItemList.Add(warriorLegendMainWeapon);
            WarriorItemList.Add(legendSubWeapon);
            WarriorItemList.Add(legendArmor);
            WarriorItemList.Add(warriorRareMainWeapon);
            WarriorItemList.Add(rareSubWeapon);
            WarriorItemList.Add(rareArmor);

            MageItemList.Add(mageLegendMainWeapon);
            MageItemList.Add(legendSubWeapon);
            MageItemList.Add(legendArmor);
            MageItemList.Add(mageRareMainWeapon);
            MageItemList.Add(rareSubWeapon);
            MageItemList.Add(rareArmor);
        }

        void ShowStoreItemList()
        {
            Console.WriteLine();
            Console.WriteLine("[상점 아이템 목록]");

            for (int i = 0; i < StoreItemList.Count; i++)
            {
                string completedPurchase = "";
                if (StoreItemList[i].isPurchased == true)
                {
                    completedPurchase = "(구매완료)";
                }

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($" {completedPurchase}[{i + 1}] {StoreItemList[i].Name}({StoreItemList[i].ItemTypeKorean})" +
                                  $" | {StoreItemList[i].Description}" +
                                  $" | 공격력 +{StoreItemList[i].Atk}" +
                                  $" 방어력 +{StoreItemList[i].Def}" +
                                  $" 추가체력 +{StoreItemList[i].AdditionalHP}" +
                                  $" | {StoreItemList[i].Price}G |");
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void EnterStore(Player player)
        {
            if (player.ClassType == CLASSTYPE.Warrior)
            {
                StoreItemList = WarriorItemList;
            }
            else if (player.ClassType == CLASSTYPE.Mage)
            {
                StoreItemList = MageItemList;
            }

            while (true)
            {

                ShowStoreItemList();

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
                    PurchaseItem(player);
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

        public void PurchaseItem(Player player)
        {
            while (true)
            {
                Console.WriteLine("아이템 리스트에서 아이템을 확인하고 구매하고자 하는 아이템의 번호를 입력해주세요.");
                Console.WriteLine($"현재 소지금: {player.Gold}G");

                ShowStoreItemList();

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
                    else if (select > 0 && select <= StoreItemList.Count)
                    {
                        if (player.Gold >= StoreItemList[select - 1].Price && StoreItemList[select - 1].isPurchased == false)
                        {
                            StoreItemList[select - 1].isPurchased = true;
                            player.Gold -= StoreItemList[select - 1].Price;
                            player.inventory.Add(StoreItemList[select - 1]);
                            Console.Clear();
                            Console.WriteLine($"\"{StoreItemList[select - 1].Name}\" 을 구매했습니다. 인벤토리를 확인해보세요.");
                            Console.WriteLine();
                        }
                        else if (StoreItemList[select - 1].isPurchased == true)
                        {
                            Console.Clear();
                            Console.WriteLine("이미 구매된 아이템입니다.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("소지금이 부족합니다.");
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

        public void SellItem(Player player)
        {
            while (true)
            {
                Console.WriteLine("소유한 아이템을 정가의 85%로 판매할 수 있습니다.");
                Console.WriteLine("현재 소지금: {0}G", player.Gold);
                Console.WriteLine();
                Console.WriteLine("내 인벤토리");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < player.inventory.Count; i++)
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($" [{i + 1}] {player.inventory[i].Name}({player.inventory[i].ItemTypeKorean})" +
                                      $" | {player.inventory[i].Description}" +
                                      $" | 공격력 +{player.inventory[i].Atk}" +
                                      $" 방어력 +{player.inventory[i].Def}" +
                                      $" 추가체력 +{player.inventory[i].AdditionalHP}" +
                                      $" | 판매 금액: {(int)(player.inventory[i].Price * 0.85f)}G |");
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
                    else if (select > 0 && select <= player.inventory.Count)
                    {
                        player.inventory[select - 1].isPurchased = false;
                        player.Gold += (int)(player.inventory[select - 1].Price * 0.85f);
                        AddSellItemToStoreItemList(player.inventory[select - 1]);
                        UnWearItemSell(player, player.inventory[select - 1]);
                        Console.Clear();
                        Console.WriteLine($"\"{player.inventory[select - 1].Name}\" 을" +
                                          $"{(int)(player.inventory[select - 1].Price * 0.85f)}G 에 판매하셨습니다.");
                        Console.WriteLine();
                        player.inventory.RemoveAt(select - 1);                        
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

        void UnWearItemSell(Player player, Item sellItem)
        {
            if (sellItem == player.wearMainWeapon)
            {
                player.CurrentHP -= sellItem.AdditionalHP;
                player.wearMainWeapon = new Item();
            }
            else if (sellItem == player.wearSubWeapon)
            {
                player.CurrentHP -= sellItem.AdditionalHP;
                player.wearSubWeapon = new Item();
            }
            else if (sellItem == player.wearArmor)
            {
                player.CurrentHP -= sellItem.AdditionalHP;
                player.wearArmor = new Item();
            }
        }
    }
}
