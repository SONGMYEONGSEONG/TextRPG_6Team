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
        public int TargetCount { get; protected set; }
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