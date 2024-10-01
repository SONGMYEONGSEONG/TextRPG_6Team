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
        public int ID { get; protected set; }
        public SkillTargetType TargetType { get; protected set; }
        public int TargetCount { get; protected set; } //TargetCount : 스킬을 사용하는 횟수 , //Skill.SkillTargetType.RandomTarget 일때만 사용되는 변수 그 외는 0으로 설정하면됨
        public float SkillValue { get; protected set; } //스킬 계수
        /*Debug*/
        public string Name { get; protected set; }
        public float MpCost { get; protected set; }
        public string Description { get; protected set; }

        /*Debug*/
        public Skill()
        {

        }

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