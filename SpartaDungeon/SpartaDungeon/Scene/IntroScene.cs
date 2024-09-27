using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Scene
{
    internal class IntroScene
    {
        Character player = new Character();

        public bool isContinue;
        string name;
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

            // [1] 저장된 게임
            // [2] 새 게임
        }

        void SetPlayerName()
        {
            Console.WriteLine("스파르타 마을에 오신 당신을 환영합니다.");
            while (true)
            {
                Console.WriteLine("원하는 이름을 설정해주세요.");
                Console.Write(">> ");
                name = Console.ReadLine();

                Console.WriteLine();

                CheckPlayerName();

                if (isContinue) break;
            }
            Console.Clear();
            ChooseJob();
        }

        void CheckPlayerName()
        {
            while (true)
            {
                Console.WriteLine(name + " 으로 하시겠습니까?");
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
                    Console.WriteLine("이름 설정을 취소했습니다. 이름을 다시 설정합니다.\n");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("1이나 2를 입력해주세요.\n");
                }
            }
        }

        void ChooseJob()
        {
            Console.WriteLine($"{name}님 환영합니다. 직업을 선택해주세요.");

            while (true)
            {
                Console.WriteLine("원하는 직업의 숫자를 입력하세요.\n");
                Console.WriteLine("[1] 전사");
                Console.WriteLine("[2] 도적");
                Console.WriteLine("[3] 궁수");
                Console.WriteLine("[4] 마법사");
                Console.WriteLine();
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "1" || input == "2" || input == "3" || input == "4")
                {
                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("전사를 선택하셨습니다.");

                            player = new Warrior(name, JobType.Warrior, "전사", 150, 100, 15, 15, 10, 10, 10, 500);
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("도적을 선택하셨습니다.");
                            player = new Rogue(name, JobType.Rogue, "도적", 100, 100, 12, 5, 10, 10, 20, 500); ;
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("궁수를 선택하셨습니다.");
                            player = new Archer(name, JobType.Warrior, "전사", 100, 150, 15, 10, 10, 10, 10, 500); ;
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("마법사를 선택하셨습니다.");
                            player = new Mage(name, JobType.Warrior, "전사", 70, 250, 15, 15, 10, 10, 10, 500, 20); ;
                            break;
                    }

                    player.SetStat();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            // MainScene.VillageScene(player);
        }
    }
}
