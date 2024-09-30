using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Heal
{
    internal class RecoverySystem
    {
        public int? CurrentHP { get; private set; }
        public int MaxHP { get; private set; }

        // 회복량을 저장하는 변수
        private int recoveryAmount;

        // 생성자: 초기 체력과 최대 체력을 설정
        public RecoverySystem(int maxHP, int? currentHP)
        {
            MaxHP = maxHP;
            CurrentHP = currentHP;
        }

        // 회복 기능
        public int? HpRecover(string itemNum)
        {
            //포션종류에 따른 회복량
            if (itemNum == "007") { recoveryAmount = 100; }
            else if (itemNum == "008") { recoveryAmount = 80; }

            // 현재 체력에 회복량 추가, 최대 체력을 넘지 않도록 조정
            CurrentHP += recoveryAmount;
            if (CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"플레이어가 {recoveryAmount}만큼 회복했습니다.");

            return CurrentHP;  
        }
    }
}
