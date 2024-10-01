namespace SpartaDungeon
{
    internal class ArcherSkill : Skill
    {
        public void HeadShot()
        {
            Name = "헤드샷";
            MpCost = 10f;
            Description = "강력한 한 발을 적의 급소에 명중시킨다. 데미지 2배";
            // damage = player.atk * 2
        }

        public void QuickShot()
        {
            // 3발 속사
        }

        public void TrackingArrow()
        {
            // 추적화살
            // 회피 불가
        }

        public override void AllPrint()
        {

        }
    }
}