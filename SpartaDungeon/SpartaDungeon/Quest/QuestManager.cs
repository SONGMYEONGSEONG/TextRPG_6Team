using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpartaDungeon.Quest
{
    internal class QuestManager
    {
        Quest[] _quests;
       

        public QuestManager()
        {
            _quests = new Quest[3];

            string fileName = "QuestList.json";
            string jsonString = "";
            var options = new JsonSerializerOptions { WriteIndented = true };

            for (int i = 0; i < _quests.Length; i++)
            {
                _quests[i] = new Quest();

                //파일 json 파일 생성
                jsonString += JsonSerializer.Serialize(_quests[i], options);
            }
            File.WriteAllText(fileName, jsonString);

        
            //파일 json 저장 
            //jsonString += JsonSerializer.Serialize(_quests[i]);

        }



    }
}
