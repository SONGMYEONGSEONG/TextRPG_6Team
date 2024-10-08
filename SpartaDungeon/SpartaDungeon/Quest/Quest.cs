﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.Emit;

namespace SpartaDungeon
{
    internal enum QuestType
    {
        None = 0,
        MonsterKillCount = 1,
        EquipCheck = 2,
        UseSkill = 3,
        End
    }

    internal class Quest
    {

        //0 퀘스트 ID
        //1 퀘스트 이름
        //2 퀘스트 내용
        //3 퀘스트 목적
        //4 퀘스트 현재 진행도 (최초 0)
        //5 퀘스트 목표 진행도
        //6 퀘스트 클리어 유무
        //7 퀘스트 보상 이름
        //8 퀘스트 보상 갯수
        //9 퀘스트 보상 골드
        //10 퀘스트 타입

        public string Label { get; set; }
        public string Detail { get; set; }
        public string Purpose { get; set; }
        public int CurProgressRequired { get; set; }
        public int EndProgressRequired { get; set; }
        public bool IsFinish { get; set; }
        public string RewardItemName { get; set; }
        public string RewardValue { get; set; }
        public int RewardGold { get; set; }
        public QuestType Type;
        //public string Type;

        public Quest() { }

        public bool QuestCheck(Character _curPlayer)
        {
            switch(Type)
            {
                case QuestType.MonsterKillCount:
                    if (CurProgressRequired >= EndProgressRequired)
                    {
                        return true;
                    }
                    break;

                case QuestType.EquipCheck:
                    if(_curPlayer.EquipWeapon.Name != "" || _curPlayer.EquipArmor.Name != "")
                    {
                        return true;
                    }
                    break;

                case QuestType.UseSkill:
                    //캐릭터의 현재 마나가 최대 마나량보다 작으면 스킬 사용한걸로 취급
                    //if(_curPlayer.MaxMp > _curPlayer.CurrentMp)
                    //{
                    //    return true;
                    //}
                    //break;
                    if (CurProgressRequired >= EndProgressRequired)
                    {
                        return true;
                    }
                    break;

            }

            return false;
        }
        
    }
}
