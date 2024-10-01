using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpartaDungeon;

namespace SpartaDungeon
{
    internal enum EnemyType
    {
        None = 0,
        Skeleton = 1,
        Goblin,
        Orc,
        Crab,
        Turtle,
        End,
    }

    internal class Enemy : Creature
    {
        EnemyType _enemyType;
        SummonArea _summonArea;
        bool _isDead;
        public bool IsDead { get { return _isDead; } set { _isDead = value; } }

        public Enemy(Enemy _enemyData)
        {
            _summonArea = _enemyData._summonArea;
            _enemyType = _enemyData._enemyType;
            Name = _enemyData.Name;
            Level = _enemyData.Level;
            Attack = _enemyData.Attack;
            Defense = _enemyData.Defense;
            Agility = _enemyData.Agility;
            Accuracy = _enemyData.Accuracy;
            Luck = _enemyData.Luck;
            MaxHp = _enemyData.MaxHp;
            MaxMp = _enemyData.MaxMp;
            CurrentHp = MaxHp;
            CurrentMp = MaxMp;
            Gold = _enemyData.Gold;
            GainItem = _enemyData.GainItem;
        }

        public Enemy(SummonArea area, EnemyType enemyType, string name, int level, float hp, float mp,
                         float atk, float def, float agl, float acc, float luc, int gold, string gainItem)
        {
            _summonArea = area;
            _enemyType = enemyType;
            Name = name;
            Level = level;
            Attack = atk;
            Defense = def;
            Agility = agl;
            Accuracy = acc;
            Luck = luc;
            MaxHp = hp;
            MaxMp = mp;
            CurrentHp = MaxHp;
            CurrentMp = MaxMp;
            Gold = gold;
            GainItem = gainItem;
            _isDead = false;
        }

        // 몬스터들의 수치를 조금 씩 변경 시켜 다른 몬스터라는것을 인지
        public void SetStatRandom()
        {
            Random random = new Random();
            int randomvalue = random.Next(1, 5);

            Level += randomvalue;
            Attack += randomvalue * 2;
            Defense += randomvalue * 2;
            Agility += randomvalue;
            Accuracy += randomvalue;
            Luck += randomvalue;
            MaxHp += randomvalue;
            MaxMp += randomvalue;
            CurrentHp = MaxHp;
            CurrentMp = MaxMp;
            Gold += randomvalue * 5;
        }
    }
}
