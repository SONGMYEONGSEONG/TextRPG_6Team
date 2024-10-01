using System.Text;
using System.Xml.Linq;

namespace SpartaDungeon
{
    internal class RogueSkill
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        List<Skill> _skillList;
        public List<Skill> SkillList { get { return _skillList; } }

        public RogueSkill()
        {
            _skillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "섀도우 슬래시", 15, "어둠 속에서 적을 기습해 모든적을 공격한다. 데미지 1배",Skill.SkillTargetType.AllTarget, 4,4.0f);
            Skill _skill2 = new Skill(1, "은신 타격", 30, "은신상태에서 하나의 적에게 접근해 심각한 피해를 입힌다. 데미지 3배", Skill.SkillTargetType.OneTarget, 1,3.0f);
            Skill _skill3 = new Skill(2, "연격", 45, "3명 랜덤의 적에게 빠르게 연속으로 공격한다 데미지 2배", Skill.SkillTargetType.RandomTarget,3,2.0f);

            _skillList.Add(_skill1);
            _skillList.Add(_skill2);
            _skillList.Add(_skill3);
        }

        
        public void Print()
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine();
            
            foreach (Skill skill in SkillList)
            {
                _strbuilder.AppendLine($"{skill.ID + 1}. {skill.Name}({skill.MpCost}) : {skill.Description} ");
            }
            Console.Write(_strbuilder.ToString());
        }
    }
}