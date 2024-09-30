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
        BattleScene _battleScene = new BattleScene();
        QuestScene _questScene = new QuestScene();
        EnemyManager _enemyManager = new EnemyManager();
        StageSelectScene _stageSelectScene = new StageSelectScene();
        SummonArea _curSelectArea = SummonArea.Forest;

        Store _store = new Store();

        public void VillageScene(Character character)
        {
            _enemyManager.Initialize();

            while (true)
            {
                Console.Clear();
                // 메인 화면 올때마다 플레이어 스텟 최신화
                character.UpdateStat();

                Console.WriteLine("마을에서 다음 활동을 선택할 수 있습니다.\n");
                Console.WriteLine("[1] 상태 보기");
                Console.WriteLine("[2] 인벤토리 보기");
                Console.WriteLine("[3] 던전 입장");
                Console.WriteLine("[4] 던전 스테이지 선택");
                Console.WriteLine("[5] 퀘스트 보기");
                Console.WriteLine("[6] 상점 보기");
                Console.WriteLine();
                Console.WriteLine("[0] 저장하기");
                Console.WriteLine("[9] 게임종료");
                Console.WriteLine();
                Console.WriteLine("원하는 활동을 입력해주세요.");
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
                            break;
                        case ((int)MainSceneChoice.Status):
                            Console.Clear();
                            character.DisplayStatus();
                            break;
                        case ((int)MainSceneChoice.Inventory):
                            Console.Clear();
                            character.DisplayInventory();
                            break;
                        case ((int)MainSceneChoice.EnterDungeon):
                            Console.Clear();
                            _battleScene.Initialize(character, _enemyManager.GetEnemies(SummonArea.Forest));
                            _battleScene.SceneStart();
                            _battleScene.SceneExit(ref character);
                            break;
                        case ((int)MainSceneChoice.DungeonStage):
                            Console.Clear();
                            _stageSelectScene.Initialize(_curSelectArea);
                            _stageSelectScene.SceneStart();
                            _stageSelectScene.SceneExit(ref _curSelectArea);
                            break;
                        case ((int)MainSceneChoice.Quest):
                            Console.Clear();
                            _questScene.Initialize(character);
                            _questScene.SceneStart();
                            _questScene.SceneExit(ref character);
                            break;
                        case ((int)MainSceneChoice.Store):
                            Console.Clear();
                            _store.EnterStore(character);
                            break;
                        case ((int)MainSceneChoice.Exit):
                            // 종료 호출
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.WriteLine("선택지의 숫자 중 하나를 입력해주세요.\n");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n");
                }
            }
        }
    }
}
