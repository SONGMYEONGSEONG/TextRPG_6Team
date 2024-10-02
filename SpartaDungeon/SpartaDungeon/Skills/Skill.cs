namespace SpartaDungeon
{

    internal class Skill
    {
        internal enum SkillTargetType
        {
            OneTarget = 0,
            RandomTarget = 1,
            AllTarget = 2,
            End
        }

        /*Debug*/
        public int ID { get; set; }
        public SkillTargetType TargetType { get; set; }
        public int TargetCount { get; set; } //TargetCount : 스킬을 사용하는 횟수 , //Skill.SkillTargetType.RandomTarget 일때만 사용되는 변수 그 외는 0으로 설정하면됨
        public float SkillValue { get; set; } //스킬 계수
        /*Debug*/
        public string Name { get; set; }
        public float MpCost { get; set; }
        public string Description { get; set; }

        /*Debug*/

        public Skill(int id, string name, float mpCost, string description, SkillTargetType targetType, int targetCount, float skillValue)
        {
            ID = id;
            Name = name;
            MpCost = mpCost;
            Description = description;
            TargetType = targetType;
            TargetCount = targetCount;
            SkillValue = skillValue;
        }

        public virtual void AllPrint()
        {

        }
        /*Debug*/
    }
}