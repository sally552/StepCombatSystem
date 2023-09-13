using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Attributes
{
    [Serializable]
    public class StatContainer
    {
        [SerializeField]
        private Stat _stat;
        public float CurrentValue => _stat.CurrentValue;

        public readonly List<TimeLimitedModifier> Modifiers = new ();
        public event Action BuffsChanged;

        public void Initialize()
        {
            _stat.Initialize();
        }

        public void AddModifier(TimeLimitedModifier timeLimitedModifier)
        {
            Modifiers.Add(timeLimitedModifier);
            _stat.AddModifier(timeLimitedModifier.Modifier);
            BuffsChanged?.Invoke();
        }

        public virtual void UpdateDuration()
        {
            for (int i = 0; i < Modifiers.Count; i++)
            {
                var timeLimitedModifier = Modifiers[i];
                timeLimitedModifier.Duration -= 1;
                
                if (timeLimitedModifier.Duration <= 0)
                {
                    _stat.RemoveModifier(timeLimitedModifier.Modifier);
                    Modifiers.RemoveAt(i);
                    BuffsChanged?.Invoke();
                    i--;
                }
            }
        }
    }
}