using GamePlay.Scripts.Character.Stats;

namespace GamePlay.Scripts.Character.AttackingComponent
{
    public interface ICharacterAttacking
    {
        void PlayAttacking(UnitBase unitBase, float attackingDamage);
    }

    public class CharacterAttackingFactory
    {
        public ICharacterAttacking GetAttackingStrategy(TroopBehaviourType troopBehaviourType)
        {
            switch (troopBehaviourType)
            {
                case TroopBehaviourType.Melee:
                    {
                        return new MeleeAttacking();
                    }
                case TroopBehaviourType.Ranger:
                    {
                        return new MeleeAttacking();
                    }
                case TroopBehaviourType.Tower:
                    {
                        return new TowerAttacking();
                    }
                default:
                    {
                        return new MeleeAttacking();
                    }
            }
        }
    }

    internal class MeleeAttacking : ICharacterAttacking
    {
        public void PlayAttacking(UnitBase target, float attackingDamage)
        {
            target.HealthComp().PlayHurting(attackingDamage);
        }
    }

    internal class TowerAttacking : ICharacterAttacking
    {
        public void PlayAttacking(UnitBase target, float attackingDamage)
        {


        }
    }
}
