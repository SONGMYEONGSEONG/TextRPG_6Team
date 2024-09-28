using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpartaDungeon.Quest
{
    internal class QuestManager
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        Dictionary<int, Quest> _quests;
        public Dictionary<int, Quest> Quests { get { return _quests; } }

        public QuestManager()
        {
            string csvFilePath = @"..\..\..\TextRPG_Quest.csv";

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

                    //Dictionary<int, Quest> row = new Dictionary<int, Quest>();

                    _strbuilder.Clear();
                    _strbuilder.Append(ApplyEscapeCharacters(values[2]));

                    Quest quest = new Quest(values[1], _strbuilder.ToString(), bool.Parse(values[3]));
                    quest.RewardType = values[4];
                    quest.RewardValue = values[5];
                    quest.RewardGold = values[6];

                    //row[int.Parse(values[0])] = quest;

                    csvData.Add(int.Parse(values[0]), quest);
                }

                if (csvData != null)
                {
                    _quests = csvData;
                }
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
            _strbuilder.AppendLine($"{_quests[_questID].Detail}");
            _strbuilder.AppendLine();
            _strbuilder.AppendLine("- 퀘스트 목표 (0/1)");
            _strbuilder.AppendLine();
            _strbuilder.AppendLine("-보상-");
            //아이템 보상 받는거 형식화 생각해볼것 
            _strbuilder.AppendLine($"쓸만한 방패 x 1");
            _strbuilder.AppendLine($"5G");
            _strbuilder.AppendLine();
            //-퀘스트 목표 선언


            Console.Write(_strbuilder.ToString());
        }


    }
}
