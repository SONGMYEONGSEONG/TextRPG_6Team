using System.Text;

namespace SpartaDungeon
{
    internal class ArcherSkill : SkillDeck
    {
        public ArcherSkill()
        {
            _skillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "헤드샷", 15, "하나의 적에게 강력한 화살을 쏜다.  데미지 3배", Skill.SkillTargetType.OneTarget, 1, 3.0f);
            Skill _skill2 = new Skill(1, "멀티샷", 30, "모든 적에게 화살을 발사하여 공격한다. 데미지 2배", Skill.SkillTargetType.AllTarget, 4, 2.0f);
            Skill _skill3 = new Skill(2, "래피드샷", 45, "랜덤의 적에게 화살 6벌을 공격한다. 데미지 1.5배", Skill.SkillTargetType.RandomTarget, 6, 1.5f);

            _skillList.Add(_skill1);
            _skillList.Add(_skill2);
            _skillList.Add(_skill3);
        }

    }
}