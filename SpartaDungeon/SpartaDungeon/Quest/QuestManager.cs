using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpartaDungeon
{
    internal class QuestManager
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        Dictionary<int, Quest> _quests;
        public Dictionary<int, Quest> Quests { get { return _quests; } }

        //Dictionary<int, Quest> _acceptedQuest; //플레이어가 수락한 퀘스트 
        //public Dictionary<int, Quest> AcceptedQuest {  get { return _acceptedQuest; } }

        public QuestManager()
        {
            string csvFilePath = @"..\..\..\Data\TextRPG_Quest.csv";

            // 1. CSV 파일을 UTF-8 인코딩으로 읽기
            Dictionary<int, Quest> csvData = new Dictionary<int, Quest>();
            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            {
                string headerLine = reader.ReadLine(); //카테고리
                string[] headers = headerLine.Split(','); //문자열 카테고리 분류

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

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

                    _strbuilder.Clear();
                    
                    Quest quest = new Quest();
                    quest.Label = values[1];
                    _strbuilder.Append(ApplyEscapeCharacters(values[2]));
                    quest.Detail = _strbuilder.ToString();
                    quest.Purpose = values[3];
                    quest.CurProgressRequired = int.Parse(values[4]);
                    quest.EndProgressRequired = int.Parse(values[5]);
                    quest.IsFinish = bool.Parse(values[6]);
                    quest.RewardItemName = values[7];
                    quest.RewardValue = values[8];
                    quest.RewardGold = int.Parse(values[9]);
                    quest.Type = (QuestType)Enum.Parse(typeof(QuestType), values[10]);

                    csvData.Add(int.Parse(values[0]), quest);
                }

                if (csvData != null)
                {
                    _quests = csvData;
                }

                ////플레이어가 수락한 퀘스트 리스트 생성 
                //if(_acceptedQuest == null)
                //{
                //    _acceptedQuest = new Dictionary<int, Quest>();
                //}
                //

            }

        }

        private string ApplyEscapeCharacters(string input)
        {
            // 예시로, 줄바꿈 문자를 이스케이프 화시킴
            input = input.Replace("\\n", "\n");
            input = input.Replace("\\t", "\t");

            // 다른 이스케이프 문자도 필요에 따라 처리할 수 있다.
            return input;
        }

        public void Print(int _questID)
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine($"{_quests[_questID].Label}");
            _strbuilder.AppendLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine($"{_quests[_questID].Detail}");
            _strbuilder.AppendLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine($"- {_quests[_questID].Purpose} ({_quests[_questID].CurProgressRequired}/{_quests[_questID].EndProgressRequired})");
            _strbuilder.AppendLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine("-보상-");

            if (_quests[_questID].RewardItemName != "")
            {
                _strbuilder.AppendLine($"{_quests[_questID].RewardItemName} x {_quests[_questID].RewardValue}");
            }
            if (_quests[_questID].RewardGold != 0)
            {
                _strbuilder.AppendLine($"{_quests[_questID].RewardGold}G");
            }
                _strbuilder.AppendLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(_strbuilder.ToString());
        }

        //public void QuestAccept(int _questID)
        //{
        //    if (!_acceptedQuest.ContainsKey(_questID))
        //    {
        //        _acceptedQuest.Add(_questID, _quests[_questID]);
        //        _strbuilder.Clear();
        //        _strbuilder.AppendLine("해당 퀘스트를 수락 하였습니다.");
        //        Console.Write(_strbuilder.ToString());
        //    }
        //    else
        //    {
        //        _strbuilder.Clear();
        //        _strbuilder.AppendLine("해당 퀘스트는 이미 수락 하였습니다.");
        //        Console.Write(_strbuilder.ToString());
        //    }
        //}

    }
}
