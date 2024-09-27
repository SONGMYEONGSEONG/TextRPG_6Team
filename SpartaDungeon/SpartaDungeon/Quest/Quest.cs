using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.Emit;

namespace SpartaDungeon.Quest
{
    internal class Quest
    {
        string _questLabel; //퀘스트 제목
        string _questDetail; //퀘스트 내용
        bool _isFinish; //퀘스트 성공 여부

        public string QuestLabel { get { return _questLabel; } }
        public string QuestDetail { get { return _questDetail; }}
        public bool IsFinish { get { return _isFinish; } }

        public Quest()
        {
            _questLabel = "Test Quest";
            _questDetail = "Test 123 \n Test 456";
            _isFinish = false;
        }

        public Quest(string _Label)
        {
            _questLabel = _Label;
            _questDetail = "Test 123 \n Test 456";
            _isFinish = false;
        }
    }
}
