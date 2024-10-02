using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class SkillDeck
    {
        protected StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        //protected List<Skill> _skillList;
        public List<Skill> SkillList { get; set; }

        public virtual void Print()
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine();

            foreach (Skill skill in SkillList)
            {
                _strbuilder.AppendLine($"{skill.ID + 1}. {skill.Name}({skill.MpCost}) : {skill.Description} ");
            }

            Console.Write(_strbuilder.ToString());
        }

        public virtual void SkillSet()
        {

        }
    }
}
