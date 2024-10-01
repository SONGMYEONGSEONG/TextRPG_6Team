using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Creature
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; } // EXP(경험치) 작업하면서 변수 추가 - 20241001송명성

        public float MaxHp { get; set; }
        public float? CurrentHp { get; set; }

        public float MaxMp { get; set; }
        public float? CurrentMp { get; set; }

        public float Attack { get; set; }       // Atk          : 기본 공격 능력치

        public float Defense { get; set; }      // Def

        public float Agility { get; set; }      // Agl          : 민첩. 내 회피 확률에 영향

        public float Accuracy { get; set; }     // Acc          : 명중. 상대의 회피 확률과 내 공격 크리티컬 확률에 영향

        public float Luck { get; set; }         // Luc of Luk   : 행운. 공격 크리티컬 확률, 보상에 영향

        public int Gold { get; set; }

        public string GainItem { get; set; }
    }
}
