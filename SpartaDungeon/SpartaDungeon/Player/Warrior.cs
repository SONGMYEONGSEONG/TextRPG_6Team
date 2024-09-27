using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Warrior : Character
    {
        public Warrior(string name, JobType jobType, string job, float hp, float mp,
                       float atk, float def, float agl, float acc, float luc, int gold)
                       : base (name, jobType, job, hp, mp, atk, def, agl, acc, luc, gold)
        {

        }
    }
}
