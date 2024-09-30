using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpartaDungeon;

namespace SpartaDungeon.Heal
{
    internal class Inventory
    {

        public Inventory() 
        {
        }

        public void ShowInventory(Player player)
        {
            while (true)
            {
                Console.WriteLine("[테스트]"+player.Name + "의 인벤토리");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < player.inventory.Count; i++)
                {
                    string itemWearState = "";
                    if (player.inventory[i] == player.wearMainWeapon || player.inventory[i] == player.wearArmor || player.inventory[i] == player.wearSubWeapon)
                    {
                        itemWearState = "[E]";
                    }
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($" [{i + 1}] {itemWearState}{player.inventory[i].Name}({player.inventory[i].ItemTypeKorean})" +
                                      $" | {player.inventory[i].Description}" +
                                      $" | 공격력 +{player.inventory[i].Atk}" +
                                      $" 방어력 +{player.inventory[i].Def}" +
                                      $" 추가체력 +{player.inventory[i].AdditionalHP}" +
                                      $" | {player.inventory[i].Price}G |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("아이템 번호를 입력하면 해당 아이템을 장착하거나 해제할 수 있습니다.");
                Console.WriteLine("주무기, 보조무기, 갑옷을 한 개씩 장착할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.Write(">> ");

                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0) break;
                    else if (select > 0 && select <= player.inventory.Count && player.inventory[select - 1].ItemType == ITEMTYPE.HealingItem)
                    {
                        Console.Clear();
                        if (player.CurrentHP == player.TotalMaxHP)
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("이미 최대 체력 상태입니다.");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                        }
                        else 
                        {
                            RecoverySystem recovery = new RecoverySystem(player.TotalMaxHP, player.CurrentHP);
                            player.CurrentHP = recovery.HpRecover(player.inventory[select - 1].ItemNum);
                            Console.WriteLine($"현재 체력: {player.CurrentHP}/{player.TotalMaxHP}");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                        }
                        
                    }
                    else if (select > 0 && select <= player.inventory.Count)
                    {
                        player.ManageItemWear(player.inventory[select - 1]);
                        Console.Clear();
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
            Console.Clear();
        }
    }
}
