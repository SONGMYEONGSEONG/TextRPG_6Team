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
        public string Label { get; set; }
        public string Detail { get; set; }
        public bool IsFinish { get; set; }
        //map 쓸지 고민해볼것
        public string RewardType { get; set; }
        public string RewardValue { get; set; }
        public string RewardGold { get; set; }


        //public Quest()
        //{
        //    Label = "Test Quest";
        //    Detail = "Test 123 \n Test 456";
        //    IsFinish = false;
        //    RewardType = new List<string>();
        //    RewardValue = new List<string>();
        //}

        public Quest(string _Label, string _Detail, bool _IsFinished = false)
        {
            Label = _Label;
            Detail = _Detail;
            IsFinish = _IsFinished;
        }
    }
}
