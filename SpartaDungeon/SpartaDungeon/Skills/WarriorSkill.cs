namespace SpartaDungeon
{
    internal class WarriorSkill : SkillDeck
    {
        public WarriorSkill()
        {
            SkillList = new List<Skill>();

            Skill _skill1 = new Skill(0, "스파르타의 검", 15, "스파르타의 검을 소환해 적을 공격한다. 데미지 3배", Skill.SkillTargetType.OneTarget, 1, 3.0f);
            Skill _skill2 = new Skill(1, "회전 베기", 25, "검을 들고 회전하며 모든 적을 베어낸다. 데미지 2배", Skill.SkillTargetType.AllTarget, 4, 2.0f);
            Skill _skill3 = new Skill(2, "일격 필살", 35, "전장을 순식간에 가로질러 3명의 랜덤한 적에게 피해를 입힌다. 데미지 3배", Skill.SkillTargetType.RandomTarget, 3, 3.0f);

            SkillList.Add(_skill1);
            SkillList.Add(_skill2);
            SkillList.Add(_skill3);
        }
    }
}