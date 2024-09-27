using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaDungeon.Scene
{
    internal class MainScene
    {
        public void VillageScene(Character character)
        {
            while (true)
            {
                // 메인 화면 올때마다 플레이어 스텟 최신화
                character.SetStat();

                Console.WriteLine("마을에서 다음 활동을 선택할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[1] 상태창 보기");
                Console.WriteLine("[2] 인벤토리 보기");
                Console.WriteLine("[3] 상점 보기");
                Console.WriteLine("[4] 휴식하기");
                Console.WriteLine("[5] 던전 입장");
                Console.WriteLine();
                Console.WriteLine("원하는 활동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        character.ShowStatus();
                        break;
                    case "2":
                        Console.Clear();
                        character.ShowInventory();
                        break;
                    case "3":
                        Console.Clear();
                        character.EnterStore(character);
                        break;
                    case "4":
                        Console.Clear();
                        character.Rest();
                        break;
                    case "5":
                        Console.Clear();
                        character.SelectDungeon(character);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine("1 ~ 5의 숫자 중에서 하나를 입력해주세요.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
