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
        Archer,
        Rogue,
        Mage
    }

    internal class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public JobType JobType { get; set; }
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
            Gold = 500;
        }

        public Character(string name, JobType jobType, string job, float hp, float mp,
                         float atk, float def, float agl, float acc, float luc, int gold)
        {
            Name = name;
            Level = 1;
            JobType = jobType;
            Job = job;
            MaxHp = hp;
            MaxMp = mp;
            Attack = atk;
            Defense = def;
            Agility = agl;
            Accuracy = acc;
            Luck = luc;
            Gold = gold;
        }

        // 플레이어 스탯 갱신
        public virtual void SetStat()
        {

        }

        // 플레이어 상태 보기
        public virtual void DisplayStatus()
        { 

        }
    }
}
