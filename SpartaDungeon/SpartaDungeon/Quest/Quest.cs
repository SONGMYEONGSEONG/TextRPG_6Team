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
        public string RewardType { get; set; }
        public string RewardValue { get; set; }
        public string RewardGold { get; set; }

        public Quest(string _Label, string _Detail, bool _IsFinished = false)
        {
            Label = _Label;
            Detail = _Detail;
            IsFinish = _IsFinished;
        }
    }
}
