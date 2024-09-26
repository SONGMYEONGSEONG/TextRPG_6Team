using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    enum MonsterType
    {
        Skeleton=1,
        Goblin,
        Orc
    }

    internal class Monster : IStatus
    {
        public MonsterType monsterType { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public int CharacterAttack { get; set; }
        public int CharacterDefense { get; set; }
        public int CharacterMaxHP { get; set; }
        public int? CurrentHP { get; set; }
        public int DropGold { get; set; }
        public bool IsDead => CurrentHP <= 0;

        Random random = new Random();

        public Monster(MonsterType monsterType)
        {
            this.monsterType = monsterType;
        }

        public void MonsterSetStat()
        {
            if (monsterType == MonsterType.Skeleton)
            {
                int randomLevel = random.Next(1, 5);
                Name = "스켈레톤";
                Level = randomLevel;
                CharacterAttack = 3 + randomLevel;
                CharacterDefense = 1 + randomLevel / 2;
                CharacterMaxHP = 25 + randomLevel * 2;
                CurrentHP = CharacterMaxHP;
                DropGold = 100 + randomLevel * 10;
            }
            else if (monsterType == MonsterType.Goblin)
            {
                int randomLevel = random.Next(3, 10);
                Name = "고블린";
                Level = randomLevel;
                CharacterAttack = 8 + randomLevel;
                CharacterDefense = 5 + randomLevel / 2;
                CharacterMaxHP = 50 + randomLevel * 2;
                CurrentHP = CharacterMaxHP;
                DropGold = 150 + randomLevel * 10;
            }
            else if (monsterType == MonsterType.Orc)
            {
                int randomLevel = random.Next(10, 21);
                Name = "오크";
                Level = randomLevel;
                CharacterAttack = 15 + randomLevel;
                CharacterDefense = 8 + randomLevel / 2;
                CharacterMaxHP = 80 + randomLevel * 2;
                CurrentHP = CharacterMaxHP;
                DropGold = 250 + randomLevel * 10;
            }
        }

        public void ShowMonsterState()
        {
            if (CurrentHP < 0) CurrentHP = 0;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            string deadState = "";
            if (IsDead) deadState = "[처치!]";
            Console.WriteLine($"{deadState} {Name}(Lv.{Level}) |" +
                              $" 공격력 {CharacterAttack}" +
                              $" 방어력 {CharacterDefense}" +
                              $" 체력 {CurrentHP} / {CharacterMaxHP} |" +
                              $" 처치 시 획득 골드: {DropGold}");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
    }
}
