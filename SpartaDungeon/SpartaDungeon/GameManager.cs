using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaDungeon
{
    internal class GameManager
    {
        Player player = new Player();
        Store store = new Store();
        Dungeon dungeon = new Dungeon();

        public bool isContinue;

        void GameStart()
        {
            Console.WriteLine("Sparta Dungeon");
            Console.WriteLine("게임 시작");
            Console.WriteLine("Press Enter to Start.");
            Console.Write(">> ");
            string? input = Console.ReadLine();

            if (input != null)
            {
                Console.Clear();
                SetPlayerName();
            }
        }

        void SetPlayerName()
        {
            Console.WriteLine("스파르타 마을에 오신 당신을 환영합니다.");
            while (true)
            {
                Console.WriteLine("원하는 이름을 설정해주세요.");
                Console.Write(">> ");
                player.Name = Console.ReadLine();

                Console.WriteLine();

                CheckPlayerName();

                if (isContinue)
                {
                    Console.Clear();
                    player.ChooseClass();
                    break;
                }
            }
            Console.Clear();
            VillageScene();
        }

        void CheckPlayerName()
        {
            while (true)
            {
                Console.WriteLine(player.Name + " -> 이 이름으로 하시겠습니까?");
                Console.Write("[1] 네\t[2] 아니요\n>> ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    isContinue = true;
                    break;
                }
                else if (input == "2")
                {
                    isContinue = false;
                    Console.Clear();
                    Console.WriteLine("이름 설정을 취소했습니다. 이름을 다시 설정합니다.");
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("1이나 2를 입력해주세요.");
                    Console.WriteLine();
                }
            }
        }


        // 메인 화면 무한 루프
        // 호출된 메서드가 종료되면 돌아와서 반복
        public void VillageScene()
        {
            while (true)
            {
                // 메인 화면 올때마다 플레이어 스텟 최신화
                player.SetStat();

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
                        player.ShowStatus();
                        break;
                    case "2":
                        Console.Clear();
                        player.ShowInventory();
                        break;
                    case "3":
                        Console.Clear();
                        store.EnterStore(player);
                        break;
                    case "4":
                        Console.Clear();
                        player.Rest();
                        break;
                    case "5":
                        Console.Clear();
                        dungeon.SelectDungeon(player);
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

        public void RunGame()
        {
            GameStart();
        }
    }
}
