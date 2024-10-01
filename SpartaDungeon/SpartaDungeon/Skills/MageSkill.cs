using System.Text;
using System.Xml.Linq;

namespace SpartaDungeon
{
    internal class MageSkill
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        List<Skill> _skillList;
        public List<Skill> SkillList { get { return _skillList; } }

        public MageSkill()
        {
            _skillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "파이어 볼", 15, "하나의 적에게 마법으로 만든 화염구를 발사한다.  데미지 2배",Skill.SkillTargetType.OneTarget,1,2.0f);
            Skill _skill2 = new Skill(1, "아이스 스트라이크", 30, "모든 적에게 얼음덩어리를 여러개 만들어 공격한다. 데미지 1.5배", Skill.SkillTargetType.AllTarget,4,1.5f);
            Skill _skill3 = new Skill(2, "썬더 볼트", 45, "2명의 랜덤의 적에게 번개를 날려 공격한다. 데미지 3배", Skill.SkillTargetType.RandomTarget,2,3.0f);

            _skillList.Add(_skill1);
            _skillList.Add(_skill2);
            _skillList.Add(_skill3);
        }

        
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
    }
}