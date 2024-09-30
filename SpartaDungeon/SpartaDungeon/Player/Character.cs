﻿using System;
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
        public float ExtraHp { get; set; }
        public float TotalMaxHp { get; set; }
        public float? CurrentHp { get; set; }

        public float MaxMp { get; set; }
        public float ExtraMp { get; set; }
        public float TotalMaxMp { get; set; }
        public float? CurrentMp { get; set; }

        public float Attack { get; set; }       // Atk          : 기본 공격 능력치
        public float ExtraAttack { get; set; }  // ExtraAtk     : 장착된 장비 아이템 추가 수치
        public float TotalAttack { get; set; }


        public float Defense { get; set; }      // Def
        public float ExtraDefense { get; set; } // ExtraDef
        public float TotalDefense { get; set; }


        public float Agility { get; set; }      // Agl          : 민첩. 내 회피 확률에 영향
        // public float ExtraAgility { get; set; }
        // public float TotalAgility { get; set; }

        public float Accuracy { get; set; }     // Acc          : 명중. 상대의 회피 확률과 내 공격 크리티컬 확률에 영향
        // public float ExtraAccuracy { get; set; }
        // public float TotalAccuracy { get; set; }

        public float Luck { get; set; }         // Luc of Luk   : 행운. 공격 크리티컬 확률, 보상에 영향

        public float? Intelligence { get; set; }
        // public float ExtraIntelligence { get; set; }
        // public float TotalIntelligence { get; set; }

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
            // 디폴트 값
            Name = _name;
            Level = 1;
            MaxHp = 100f;
            MaxMp = 100f;
            Attack = 10f;
            Defense = 10f;
            Agility = 10f;
            Accuracy = 10f;
            Luck = 10f;
            Intelligence = null;
            Gold = 500;

            if (_jobTypeNum == (int)JobType.Warrior)
            {
                Job = "전사";
                MaxHp = 150f;
                MaxMp = 50f;
                Attack = 15f;
                Defense = 15f;
            }
            else if (_jobTypeNum == (int)JobType.Rogue)
            {
                Job = "도적";
                Attack = 13f;
                Agility = 15f;
                Luck = 20f;
            }
            else if (_jobTypeNum == (int)JobType.Archer)
            {
                Job = "궁수";
                MaxHp = 80f;
                MaxMp = 120f;
                Attack = 15f;
                Defense = 8f;
                Agility = 20f;
                Accuracy = 15f;
            }
            else if (_jobTypeNum == (int)JobType.Mage)
            {
                Job = "마법사";
                MaxHp = 70f;
                MaxMp = 200f;
                Attack = 5f;
                Defense = 5f;
                Agility = 5f;
                Accuracy = 5f;
                Luck = 15f;
                Intelligence = 20f;
            }
        }

        public Character(string _name, JobType _jobType, string _job, float _hp, float _mp,
                         float _atk, float _def, float _agl, float _acc, float _luc, int _gold, float? _int)
        {
            Name = _name;
            Level = 1;
            CharacterJobType = _jobType;
            Job = _job;
            MaxHp = _hp;
            MaxMp = _mp;
            Attack = _atk;
            Intelligence = _int;
            Defense = _def;
            Agility = _agl;
            Accuracy = _acc;
            Luck = _luc;
            Gold = _gold;
        }

        // 플레이어 스탯 갱신
        public void UpdateStat()
        {
            if (CurrentHp == null)
            {
                CurrentHp = TotalMaxHp;
            }
            if (CurrentMp == null)
            {
                CurrentMp = TotalMaxMp;
            }
            ExtraHp = EquipWeapon.AdditionalHP + EquipArmor.AdditionalHP; //
            // ExtraMp = EquipWeapon.AdditionalMP + EquipArmor.AdditionalMP;
            ExtraAttack = EquipWeapon.Atk + EquipArmor.Atk; // + EquipSubWeapon.Atk
            ExtraDefense = EquipWeapon.Def + EquipArmor.Def; // + wearSubWeapon.Def
            TotalMaxHp = MaxHp + ExtraHp;
            TotalMaxMp = MaxMp + ExtraMp;
            TotalAttack = Attack + ExtraAttack;
            TotalDefense = Defense + ExtraDefense;
        }

        // 플레이어 상태 보기
        public void DisplayStatus()
        {
            while (true)
            {
                Console.WriteLine($"\n[플레이어의 현재 상태]\n");
                Console.WriteLine($"이름: {Name}");
                Console.WriteLine($"레벨: {Level}");
                Console.WriteLine($"직업: {Job}");
                Console.WriteLine($"체력: {CurrentHp} / {TotalMaxHp} ({MaxHp} +{ExtraHp})");
                Console.WriteLine($"마력: {CurrentMp} / {TotalMaxMp} ({MaxMp} +{ExtraMp})");
                Console.WriteLine($"공격력: {TotalAttack}");
                Console.WriteLine($"방어력: {TotalDefense}");
                if (Intelligence != null)
                {
                    Console.WriteLine($"지능: {Intelligence}");
                }
                Console.WriteLine($"민첩: {Agility}");
                Console.WriteLine($"명중: {Accuracy}");
                Console.WriteLine($"행운: {Luck}");
                Console.WriteLine($"소지금: {Gold}G");

                Console.WriteLine("\n[0] 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
    }
}
