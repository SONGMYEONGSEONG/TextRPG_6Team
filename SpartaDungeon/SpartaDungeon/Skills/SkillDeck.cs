using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class SkillDeck
    {
        public StringBuilder Strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        public List<Skill> SkillList { get; set; }

        public virtual void Print()
        {
            Strbuilder.Clear();
            Strbuilder.AppendLine();

            foreach (Skill skill in SkillList)
            {
                Strbuilder.AppendLine($"{skill.ID + 1}. {skill.Name}({skill.MpCost}) : {skill.Description} ");
            }

            Console.Write(Strbuilder.ToString());
        }
    }
}
