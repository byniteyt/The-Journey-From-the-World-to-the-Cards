using UnityEngine;

namespace Auxiliares
{
    public class DamagePlayerCommand : ICommand
    {
        private int damageAmount;
        private LifeManager target;

        public DamagePlayerCommand(LifeManager target, int damageAmount)
        {
            this.damageAmount = damageAmount;
            this.target = target;
        }

        public void Execute()
        {
            Debug.LogWarning($"DamagePlayerCommand created to deal {damageAmount} damage to {target.gameObject.name}");

            target.ChangeLife(this, -damageAmount);
        }
    }
}
