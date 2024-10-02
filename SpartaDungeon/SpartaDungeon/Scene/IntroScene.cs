namespace SpartaDungeon
{
    internal class IntroScene
    {
        MainScene mainScene = new MainScene();
        public Character player;

        bool _isContinue;
        string _name;

        void GameStart()
        {
            Console.WriteLine("Sparta Dungeon");
            Console.WriteLine("게임 시작");
            Console.WriteLine("Press Enter to Start.");
            Console.WriteLine("\n[1] 저장된 게임 불러오기");
            Console.WriteLine("[2] 새 게임 시작\n");
            Console.Write(">> ");
            string? input = Console.ReadLine();

            if (input == "1")
            {
                Console.Clear();
                //player = mainScene.LoadGame();
                player = mainScene.LoadGame_Test();

                if (player == null)
                {
                    Console.WriteLine("저장된 게임이 없습니다. 새 게임을 시작합니다.");
                    SetPlayerName();
                }
                else
                {
                    mainScene.VillageScene(player);
                }
            }
            else if (input == "2")
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
                _name = Console.ReadLine();

                Console.WriteLine();

                CheckPlayerName();

                if (_isContinue) break;
            }
            Console.Clear();
            ChooseJob();
        }

        void CheckPlayerName()
        {
            while (true)
            {
                Console.WriteLine($"[{_name}]으로 하시겠습니까?");
                Console.Write("[1] 네\t[2] 아니요\n>> ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    _isContinue = true;
                    break;
                }
                else if (input == "2")
                {
                    _isContinue = false;
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
            Console.WriteLine($"{_name}님 환영합니다. 직업을 선택해주세요.");

            while (true)
            {
                Console.WriteLine("원하는 직업의 숫자를 입력하세요.\n");
                Console.WriteLine("[1] 전사");
                Console.WriteLine("[2] 도적");
                Console.WriteLine("[3] 궁수");
                Console.WriteLine("[4] 마법사");
                Console.Write("\n>> ");
                string? input = Console.ReadLine();

                int jobTypeNum;
                bool isNum = int.TryParse(input, out jobTypeNum);

                if (isNum)
                {
                    if (jobTypeNum >= 1 || jobTypeNum <= 4)
                    {
                        player = new Character(_name, jobTypeNum);
                        Console.Clear();
                        Console.WriteLine($"[{player.Job}] 선택!\n");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            mainScene.VillageScene(player);
        }

        public void RunGame()
        {
            GameStart();
        }
    }
}