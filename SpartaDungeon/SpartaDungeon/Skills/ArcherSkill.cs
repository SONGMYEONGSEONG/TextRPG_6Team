using System.Text;

namespace SpartaDungeon
{
    internal class ArcherSkill : SkillDeck
    {
        public ArcherSkill()
        {
            SkillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "멀티샷", 15, "화살 2발을 동시에 쏴 랜덤으로 2명을 공격한다. 데미지 1.5배", Skill.SkillTargetType.RandomTarget, 2, 1.5f);
            Skill _skill2 = new Skill(1, "에로우 레인", 60, "화살로 된 비를 내려 모든 적을 공격한다. 데미지 3배", Skill.SkillTargetType.AllTarget, 4, 3.0f);
            Skill _skill3 = new Skill(2, "비장의 한 발", 80, "정신을 집중하고 공을 들여 만든 완벽한 한 발로 적을 공격한다. 데미지 6배", Skill.SkillTargetType.OneTarget, 1, 6.0f);

            SkillList.Add(_skill1);
            SkillList.Add(_skill2);
            SkillList.Add(_skill3);
        }

        //public override void SkillSet()
        //{
        //    Skill _skill1 = new Skill(0, "멀티샷", 15, "화살 2발을 동시에 쏴 랜덤으로 2명을 공격한다. 데미지 1.5배", Skill.SkillTargetType.RandomTarget, 2, 1.5f);
        //    Skill _skill2 = new Skill(1, "에로우 레인", 60, "화살로 된 비를 내려 모든 적을 공격한다. 데미지 3배", Skill.SkillTargetType.AllTarget, 4, 3.0f);
        //    Skill _skill3 = new Skill(2, "비장의 한 발", 80, "정신을 집중하고 공을 들여 만든 완벽한 한 발로 적을 공격한다. 데미지 6배", Skill.SkillTargetType.OneTarget, 1, 6.0f);

        //    _skillList.Add(_skill1);
        //    _skillList.Add(_skill2);
        //    _skillList.Add(_skill3);
        //}
    }
}