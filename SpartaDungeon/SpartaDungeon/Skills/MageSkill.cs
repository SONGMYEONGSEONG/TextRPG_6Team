using System.Text;
using System.Xml.Linq;

namespace SpartaDungeon
{
    internal class MageSkill : SkillDeck
    { 
        public MageSkill()
        {
            SkillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "파이어 볼", 15, "하나의 적에게 마법으로 만든 화염구를 발사한다.  데미지 3배",Skill.SkillTargetType.OneTarget,1,3.0f);
            Skill _skill2 = new Skill(1, "아이스 스트라이크", 30, "모든 적에게 얼음덩어리를 여러개 만들어 공격한다. 데미지 2배", Skill.SkillTargetType.AllTarget,4,2.0f);
            Skill _skill3 = new Skill(2, "썬더 볼트", 45, "2명의 랜덤의 적에게 번개를 날려 공격한다. 데미지 4배", Skill.SkillTargetType.RandomTarget,2,4.0f);

            SkillList.Add(_skill1);
            SkillList.Add(_skill2);
            SkillList.Add(_skill3);
        }
 
    }
}