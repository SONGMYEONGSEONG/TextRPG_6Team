namespace SpartaDungeon
{
    internal class Mage : Character
    {
        public float Intelligence { get; set; }

        public Mage(string name, JobType jobType, string job, float hp, float mp,
                    float atk, float def, float agl, float acc, float luc, int gold, float intelligence)
                    : base(name, jobType, job, hp, mp, atk, def, agl, acc, luc, gold)
        {
            Intelligence = intelligence;
        }

        public override void SetStat()
        {
            base.SetStat();
            // Intelligence = 
        }
        public override void DisplayStatus()
        {
            // Intelligence 변수가 추가된 상태 보기 씬
        }
    }
}
