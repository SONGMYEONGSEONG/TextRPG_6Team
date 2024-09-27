namespace SpartaDungeon
{
    internal class  Rogue : Character
    {
        public Rogue(string name, JobType jobType, string job, float hp, float mp,
                     float atk, float def, float agl, float acc, float luc, int gold)
                     : base(name, jobType, job, hp, mp, atk, def, agl, acc, luc, gold)
        {

        }
    }
}
