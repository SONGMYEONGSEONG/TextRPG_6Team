using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class RecoverySystem
    {
        public float? CurrentHp { get; private set; }
        public float MaxHp { get; private set; }
        public float? CurrentMp { get; private set; }
        public float MaxMp { get; private set; }

        // 회복량을 저장하는 변수
        private float recoveryAmount;

        // 체력 초기화
        public void InitializeHp(float maxHp, float? currentHp)
        {
            MaxHp = maxHp;
            CurrentHp = currentHp;
        }

        // 마나 초기화
        public void InitializeMp(float maxMp, float? currentMp)
        {
            MaxMp = maxMp;
            CurrentMp = currentMp;
        }

        // 체력 회복 기능
        public float? HpRecover(string itemNum)
        {
            // 포션 종류에 따른 회복량
            if (itemNum == "501") { recoveryAmount = 100f; }

            // 현재 체력에 회복량 추가, 최대 체력을 넘지 않도록 조정
            CurrentHp += recoveryAmount;
            if (CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
            Console.WriteLine("--------------");
            Console.WriteLine(CurrentHp);
            Console.WriteLine("--------------");

            return CurrentHp;
        }

        // 마나 회복 기능 추가
        public float? MpRecover(string itemNum)
        {
            // 포션 종류에 따른 회복량 (예시)
            if (itemNum == "005") { recoveryAmount = 50f; }

            // 현재 마나에 회복량 추가, 최대 마나를 넘지 않도록 조정
            CurrentMp += recoveryAmount;
            if (CurrentMp > MaxMp)
            {
                CurrentMp = MaxMp;
            }

            return CurrentMp;
        }
    }
}
