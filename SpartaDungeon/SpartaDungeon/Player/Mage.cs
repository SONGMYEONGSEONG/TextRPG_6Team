namespace SpartaDungeon
{
    internal class Mage : Character
    {
        public float Intelligence { get; set; }

        public Mage(string name, JobType jobType, string job, float hp, float mp,
                    float atk, float def, float agl, float acc, float luc, int gold, float intelligence)
                    : base(name, jobType, job, hp, mp, atk, def, agl, acc, luc, gold)
        {
            
        }
    }
}
