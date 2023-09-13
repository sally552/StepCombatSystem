using System.Collections;
using Units.Attributes;
using Units.Components;
using UnityEngine;

namespace Units
{
    public class BattleUnit : Unit
    {
        public readonly float Initiative;

        //можно так же в отдельный компонент вынести
        public StatContainer Vamp;

        public DamageComponent Damage;
        public HealthComponent Health;

        private BattleUnit _targetUnit;

        public void SetTarget(BattleUnit target)
        {
            _targetUnit = target;
        }

        public override void Initialize()
        {
            base.Initialize();
            Damage = new DamageComponent();
            Health = new HealthComponent();

        //    Damage.DamageDealt += 
        }

        //жизненный цикл хода игрока
        public IEnumerator StepAttack()
        {
            // кнопка флаг
            yield return new WaitForSeconds(1);
            Attack();
            // секунда красный.
            yield return new WaitForSeconds(1);


            Damage.UpdateDurationBuff();
            Health.UpdateDurationBuff();
        }

        public void Attack()
        {
            var dealtDamage = Damage.DealDamage(_targetUnit);
            var vampiredHealth = dealtDamage * (Vamp.CurrentValue / 100f);
            Health.ChangeHealth(vampiredHealth);
        }

        public void AddBuff(Buff buff)
        {
            if (GetActiveBuff() >= 2) return;
            
            switch (buff.TargetAttributes)
            {
                case UnitAttributes.Armor:
                    Health.MaxHealth.AddModifier(buff.Modifier);
                    break;
                case UnitAttributes.Damage:
                    Damage.Damage.AddModifier(buff.Modifier);
                    break;
                case UnitAttributes.Vampirism:
                    Vamp.AddModifier(buff.Modifier);
                    Health.Armor.AddModifier(buff.SupportModifier);
                    break;
                case UnitAttributes.ArmorDecline:
                    _targetUnit.Health.Armor.AddModifier(buff.Modifier);
                    break;
                case UnitAttributes.VampirismDecline:
                    _targetUnit.Vamp.AddModifier(buff.Modifier);
                    break;
            }
        }

        private int GetActiveBuff()
        {
            return Vamp.Modifiers.Count + Damage.BuffCount + Health.BuffCount;
        }
    }

}