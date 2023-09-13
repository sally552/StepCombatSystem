using System;
using Units.Attributes;

namespace Units.Components
{
    [Serializable]
    public class DamageComponent : IBuffUseComponent
    { 
        public StatContainer Damage { get; set; }

        public int BuffCount => Damage.Modifiers.Count;

        public event Action<float> DamageDealt;

        public float DealDamage(BattleUnit receiver)
        {
            var damage = Damage.CurrentValue;

            var dealtDamage = receiver.Health.ReceiverDamage(damage);
            DamageDealt?.Invoke(dealtDamage);

            return dealtDamage;
        }

        public void UpdateDurationBuff()
        {
            Damage.UpdateDuration();
        }
    }

    public interface IBuffUseComponent
    {
        public void UpdateDurationBuff();
        public int BuffCount { get; }
    }
}
