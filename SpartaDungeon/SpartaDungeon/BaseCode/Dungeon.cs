using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    enum DungeonDifficulty
    {
        Easy = 1,
        Normal,
        Hard
    }

    internal class Dungeon
    {
        DungeonDifficulty Difficulty { get; set; }
        public int RewardGold { get; set; }


        public void SelectDungeon(Player player)
        {
            while (true)
            {
                player.SetStat();
                Console.WriteLine("이곳은 던전 입구입니다. 던전 난이도를 선택하면 해당 던전으로 이동합니다.");
                Console.WriteLine();
                Console.WriteLine($"플레이어 현재 체력 : {player.CurrentHP} / {player.TotalMaxHP}");
                Console.WriteLine();
                Console.WriteLine("[1] Easy   - 권장 방어력 : 5");
                Console.WriteLine("[2] Normal - 권장 방어력 : 17");
                Console.WriteLine("[3] Hard   - 권장 방어력 : 33");
                Console.WriteLine($"플레이어 현재 방어력 : {player.TotalDefense}");
                Console.WriteLine();
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.WriteLine("원하는 난이도를 선택해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                if (player.CurrentHP > 10)
                {
                    if (input == "0")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (input == "1")
                    {
                        Console.Clear();
                        Difficulty = DungeonDifficulty.Easy;
                        EnterDungeon(player);
                    }
                    else if (input == "2")
                    {
                        Console.Clear();
                        Difficulty = DungeonDifficulty.Normal;
                        EnterDungeon(player);
                    }
                    else if (input == "3")
                    {
                        Console.Clear();
                        Difficulty = DungeonDifficulty.Hard;
                        EnterDungeon(player);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("플레이어가 [빈사] 상태입니다. 휴식을 취해주세요.");
                    Console.WriteLine();
                    break;
                }
            }
        }

        public void EnterDungeon(Player player)
        {
            Console.WriteLine($"{Difficulty} 난이도 던전에 입장했습니다.");

            List<Monster> stageMonsterList = new List<Monster>();

            // 몬스터 생성
            if (Difficulty == DungeonDifficulty.Easy)
            {
                Console.WriteLine("던전에 스켈레톤 5마리 존재합니다.");
                Console.WriteLine();

                RewardGold = 500;

                for (int i = 0; i < 5; i++)
                {
                    Monster skeleton = new Monster(MonsterType.Skeleton);
                    skeleton.MonsterSetStat();
                    skeleton.ShowMonsterState();
                    stageMonsterList.Add(skeleton);
                }
            }
            else if (Difficulty == DungeonDifficulty.Normal)
            {
                Console.WriteLine("던전에 고블린이 5마리 존재합니다.");
                Console.WriteLine();

                RewardGold = 1000;

                for (int i = 0; i < 5; i++)
                {
                    Monster goblin = new Monster(MonsterType.Goblin);
                    goblin.MonsterSetStat();
                    goblin.ShowMonsterState();
                    stageMonsterList.Add(goblin);
                }
            }
            else if (Difficulty == DungeonDifficulty.Hard)
            {
                Console.WriteLine("던전에 오크가 5마리 존재합니다.");
                Console.WriteLine();

                RewardGold = 2000;

                for (int i = 0; i < 5; i++)
                {
                    Monster orc = new Monster(MonsterType.Orc);
                    orc.MonsterSetStat();
                    orc.ShowMonsterState();
                    stageMonsterList.Add(orc);
                }
            }

            Console.WriteLine();
            Console.WriteLine("전투를 시작하려면 Enter를 입력해주세요.");
            Console.Write(">> 전투 시작! ");
            string battle = Console.ReadLine();

            int turn = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("[전투 중 ...]");

                bool isPlayerDead = false;
                int deadCount = 0;
                int obtainGold = 0;

                foreach (Monster monster in stageMonsterList)
                {
                    int playersDamage = player.TotalAttack > monster.CharacterDefense ?
                                        player.TotalAttack - monster.CharacterDefense : 1;
                    int skeletonsDamage = monster.CharacterAttack > player.CharacterDefense ?
                                          monster.CharacterAttack - player.CharacterDefense : 1;
                    if (!monster.IsDead)
                    {
                        monster.CurrentHP -= playersDamage;
                        player.CurrentHP -= skeletonsDamage;
                    }
                    if (monster.IsDead)
                    {
                        deadCount++;
                        obtainGold += monster.DropGold;
                    }

                    monster.ShowMonsterState();

                    if (player.CurrentHP <= 10)
                    {
                        isPlayerDead = true;
                        break;
                    }
                }

                if (deadCount == stageMonsterList.Count)
                {
                    player.Gold += obtainGold + RewardGold;

                    Console.Clear();
                    Console.WriteLine($"승리! {obtainGold + RewardGold}G 를 획득했습니다.");
                    Console.WriteLine($"플레이어 레벨업! Lv.{player.Level} -> Lv.{++player.Level}");
                    Console.WriteLine($"현재 체력은 {player.CurrentHP} / {player.TotalMaxHP} 입니다.");
                    Console.WriteLine();
                    Console.WriteLine("퇴장하려면 Enter를 입력해주세요.");
                    Console.Write(">> ");
                    string? input = Console.ReadLine();

                    if (input != null)
                    {
                        Console.Clear();
                        break;
                    }
                }

                if (!isPlayerDead)
                {
                    ++turn;
                    Console.WriteLine();
                    Console.WriteLine($"플레이어의 현재 체력 : {player.CurrentHP} / {player.TotalMaxHP}");
                    Console.WriteLine();
                    Console.WriteLine($"턴 {turn} 종료!");
                    Console.WriteLine();
                    Console.WriteLine("[0] 도망가기");
                    Console.WriteLine("다음 턴을 진행하려면 0 이외의 문자를 입력해주세요.");
                    Console.WriteLine();
                    Console.Write(">> ");
                    string input = Console.ReadLine();

                    if (input == "0")
                    {
                        Console.Clear();
                        Console.WriteLine("도망쳤습니다.");
                        Console.WriteLine();
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("패배... 플레이어가 빈사상태입니다. 던전 입구로 복귀합니다.");
                    Console.WriteLine();
                    break;
                }
            }
        }
    }
}
