using GamePlay.Scripts.Unit;

public struct CombatConfigRule
{
    public int MeleeCompetitorCounter;
    public bool IsRuleApproved(UnitBase unit){
        if (unit.unitType == UnitType.ShortRangeTroop && MeleeCompetitorCounter - 1 >= 0)
        {
            MeleeCompetitorCounter--;
            return true;
        }
        return false;
    }
}
public interface ICombatConfigRule
{
    CombatConfigRule ApplyRule(CombatConfigRule combatConfigRule);
}

public class AllySideCombatConfigRule : ICombatConfigRule
{
    public CombatConfigRule ApplyRule(CombatConfigRule combatConfigRule)
    {
        combatConfigRule.MeleeCompetitorCounter = 1;
        return combatConfigRule;
    }
}
public class EnemySideCombatConfigRule : ICombatConfigRule
{
    public CombatConfigRule ApplyRule(CombatConfigRule combatConfigRule)
    {
        combatConfigRule.MeleeCompetitorCounter = 2;
        return combatConfigRule;
    }
}
