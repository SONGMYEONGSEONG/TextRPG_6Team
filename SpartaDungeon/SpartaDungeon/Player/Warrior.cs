using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Warrior : Character
    {
        public Warrior(string _name, JobType _jobType, string _job, float _hp, float _mp,
                       float _atk, float _def, float _agl, float _acc, float _luc, int _gold, float? _int)
                       : base(_name, _jobType, _job, _hp, _mp, _atk, _def, _agl, _acc, _luc, _gold, _int)
        {

        }
    }
}
