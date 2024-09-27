using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpartaDungeon.Quest
{
    internal class QuestManager
    {
        List<Quest> _quests;
        public List<Quest> Quests { get { return _quests; } }

        public QuestManager()
        {
            string csvFilePath = "TextRPG_Quest.csv";

            // 1. CSV 파일을 UTF-8 인코딩으로 읽기
            List<Dictionary<int, Quest>> csvData = new List<Dictionary<int, Quest>>();
            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            {
                string headerLine = reader.ReadLine(); //카테고리
                string[] headers = headerLine.Split(','); //문자열 카테고리 분류

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    Dictionary<int, Quest> row = new Dictionary<int, Quest>();

                    Quest quest = new Quest(values[1], values[2], bool.Parse(values[3]));
                    quest.RewardType = values[4];
                    quest.RewardValue = values[5];
                    quest.RewardGold = values[6];

                    row[int.Parse(values[0])] = quest;
            

                    csvData.Add(row);
                }
            }
        }




    }
}
