using System;
using Units.Attributes;

namespace Units.Components
{
    [Serializable]
    public class HealthComponent : IBuffUseComponent
    {
        public StatContainer MaxHealth;
        public StatContainer Armor;
        
        private float _lastHandledMaxHealth;
        
        private float _currentHealth;
        public float CurrentHealthRatio => _currentHealth / MaxHealth.CurrentValue;
        
        public bool IsAlive => _currentHealth > 0f;

        public int BuffCount => MaxHealth.Modifiers.Count + Armor.Modifiers.Count;

        public event Action Died;

        public void Initialize()
        {
            Armor.Initialize();
            MaxHealth.Initialize();

            MaxHealth.BuffsChanged += HandleMaxHealthBuffsChanged;
            
            _currentHealth = MaxHealth.CurrentValue;
        }
        
        public float ReceiverDamage(float damage)
        {
            var receivedDamage = damage * (1f - Armor.CurrentValue / 100f);

            ChangeHealth(-receivedDamage);
            
            return receivedDamage;
        }

        public void ChangeHealth(float delta)
        {
            _currentHealth += delta;

            if (!IsAlive)
            {
                Died?.Invoke();
            }
        }

        private void HandleMaxHealthBuffsChanged()
        {
            var newMaxHealth = MaxHealth.CurrentValue;
            
            _currentHealth *= newMaxHealth / _lastHandledMaxHealth;

            _lastHandledMaxHealth = newMaxHealth;
        }

        public void UpdateDurationBuff()
        {
            MaxHealth.UpdateDuration();
            Armor.UpdateDuration();
        }
    }
}
