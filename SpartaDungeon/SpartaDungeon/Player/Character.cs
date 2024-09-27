using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    enum JobType
    {
        None,
        Warrior,
        Rogue,
        Archer,
        Mage
    }

    internal class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public JobType CharacterJobType { get; set; }
        public string Job { get; set; }
        public float MaxHp { get; set; }
        public float CurrentHp { get; set; }
        public float MaxMp { get; set; }
        public float CurrentMp { get; set; }
        public float Attack { get; set; }       // Atk          : 기본 공격 능력치
        public float ExtraAttack { get; set; }  // ExtraAtk     : 장착된 장비 아이템 추가 수치
        public float Defense { get; set; }      // Def          : 
        public float ExtraDefense { get; set; } // ExtraDef
        public float Agility { get; set; }      // Agl          : 민첩. 내 회피 확률에 영향
        public float Accuracy { get; set; }     // Acc          : 명중. 상대의 회피 확률과 내 공격 크리티컬 확률에 영향
        public float Luck { get; set; }         // Luc of Luk   : 행운. 공격 크리티컬 확률, 보상에 영향
        public float? Intelligence { get; set; }
        public int Gold { get; set; }

        public List<Item> Inventory = new List<Item>();
        public Item EquipWeapon = new Item();
        public Item EquipArmor = new Item();

        public Character()
        {
            Name = "NoName";
            Level = 1;
            Job = "None";
            MaxHp = 100f;
            MaxMp = 100f;
            Attack = 10f;
            Defense = 10f;
            Agility = 10f;
            Accuracy = 10f;
            Luck = 10f;
            Intelligence = null;
            Gold = 500;
        }

        public Character(string _name, int _jobTypeNum)
        {
            Name = _name;
            Level = 1;
            if (_jobTypeNum == (int)JobType.Warrior)
            {
                // 초기스탯 세팅
            }
            else if (_jobTypeNum == (int)JobType.Rogue)
            {

            }
            else if (_jobTypeNum == (int)JobType.Archer)
            {

            }
            else if (_jobTypeNum == (int)JobType.Mage)
            {
                // 초기스탯 세팅
                Intelligence = 20f;
            }
        }

        public Character(string _name, JobType _jobType, string _job, float _hp, float _mp,
                         float _atk, float _def, float _agl, float _acc, float _luc, int _gold, float? _intelligence)
        {
            Name = _name;
            Level = 1;
            CharacterJobType = _jobType;
            Job = _job;
            MaxHp = _hp;
            MaxMp = _mp;
            Attack = _atk;
            Intelligence = _intelligence;
            Defense = _def;
            Agility = _agl;
            Accuracy = _acc;
            Luck = _luc;
            Gold = _gold;
        }

        // 플레이어 스탯 갱신
        public void SetStat()
        {

        }

        // 플레이어 상태 보기
        public void DisplayStatus()
        {

            // 플레이어 스탯
            if (Intelligence != null)
            {
                Console.WriteLine($"지능: {Intelligence}");
            }
        }
    }
}
