using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaDungeon
{
    enum MainSceneChoice
    {
        Save,
        Status,
        Inventory,
        EnterDungeon,
        DungeonStage,
        Quest,
        Store,
        Exit = 9
    }

    internal class MainScene
    {
        SaveLoad _saveLoad = new SaveLoad();
        BattleScene _battleScene = new BattleScene();
        QuestScene _questScene = new QuestScene();
        EnemyManager _enemyManager = new EnemyManager();
        StageSelectScene _stageSelectScene = new StageSelectScene();
        SummonArea _curSelectArea = SummonArea.Forest;

        Store _store = new Store();

        public void VillageScene(Character _player)
        {
            _enemyManager.Initialize();

            while (true)
            {
                Console.Clear();
                // 메인 화면 올때마다 플레이어 스텟 최신화
                _player.UpdateStat();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("마을에서 다음 활동을 선택할 수 있습니다.\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[1] 상태 보기");
                Console.WriteLine("[2] 인벤토리 보기");
                Console.Write("[3] 던전 입장");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"(던전 지역 : {_curSelectArea})");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[4] 던전 스테이지 선택");
                Console.WriteLine("[5] 퀘스트 보기");
                Console.WriteLine("[6] 상점 보기");
                Console.WriteLine();

                Console.WriteLine("[0] 저장하기");
                Console.WriteLine("[9] 게임종료");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("원하는 활동을 입력해주세요.");
                Console.ResetColor();

                Console.Write(">> ");

                string input = Console.ReadLine();

                int chooceScene;
                bool isNum = int.TryParse(input, out chooceScene);

                if (isNum)
                {
                    switch (chooceScene)
                    {
                        case ((int)MainSceneChoice.Save):
                            Console.Clear();
                            SaveGame(_player);
                            Console.ReadLine();
                            break;
                        case ((int)MainSceneChoice.Status):
                            Console.Clear();
                            _player.DisplayStatus();
                            break;
                        case ((int)MainSceneChoice.Inventory):
                            Console.Clear();
                            _player.DisplayInventory();
                            break;
                        case ((int)MainSceneChoice.EnterDungeon):
                            Console.Clear();
                            _battleScene.Initialize(_player, _enemyManager.GetEnemies(_curSelectArea));
                            _battleScene.SceneStart();
                            _battleScene.SceneExit(ref _player);
                            break;
                        case ((int)MainSceneChoice.DungeonStage):
                            Console.Clear();
                            _stageSelectScene.Initialize(_curSelectArea);
                            _stageSelectScene.SceneStart();
                            _stageSelectScene.SceneExit(ref _curSelectArea);
                            break;
                        case ((int)MainSceneChoice.Quest):
                            Console.Clear();
                            _questScene.Initialize(_player);
                            _questScene.SceneStart();
                            _questScene.SceneExit(ref _player);
                            break;
                        case ((int)MainSceneChoice.Store):
                            Console.Clear();
                            _store.EnterStore(_player);
                            break;
                        case ((int)MainSceneChoice.Exit):
                            ExitGame(_player);
                            break;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.WriteLine("선택지의 숫자 중 하나를 입력해주세요.\n");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ResetColor();
                }
            }
        }

        public void SaveGame(Character _player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("플레이어 데이터를 저장합니다.");
            Console.ResetColor();
            _saveLoad.SaveData(_player, "player");
        }

        public Character? LoadGame()
        {
            if (_saveLoad.LoadData<Character>("player") == null)
            {
                return null;
            }
            else
            {
                Character player = _saveLoad.LoadData<Character>("player");
                return player;
            }
        }

        void ExitGame(Character _player)
        {
            SaveGame(_player);
            Console.WriteLine("게임이 종료됩니다.");
            Environment.Exit(0);
        }
    }
}
