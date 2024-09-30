using SpartaDungeon.Quest;
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
        QuestManager _questManager = new QuestManager();

        public void VillageScene(Character character)
        {
            while (true)
            {
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
                            // 던전 호출
                            //_battleScene.Initialize(character);
                            //_battleScene.SceneStart();
                            break;
                        case ((int)MainSceneChoice.DungeonStage):
                            Console.Clear();
                            // 스테이지 선택 호출
                            break;
                        case ((int)MainSceneChoice.Quest):
                            Console.Clear();
                            // 퀘스트 보기 호출
                            break;
                        case ((int)MainSceneChoice.Store):
                            Console.Clear();
                            // 상점 호출
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
