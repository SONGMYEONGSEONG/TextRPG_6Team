namespace SpartaDungeon
{
    internal class Archer : Character
    {
        public Archer(string name, JobType jobType, string job, float hp, float mp,
                      float atk, float def, float agl, float acc, float luc, int gold)
                      : base(name, jobType, job, hp, mp, atk, def, agl, acc, luc, gold)
        {

        }
    }
}
