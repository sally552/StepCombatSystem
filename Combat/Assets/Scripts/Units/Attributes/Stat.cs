using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units.Attributes
{
    [Serializable]
    public class Stat
    {
        [SerializeField]
        private float _minCurrentValue;
        [SerializeField]
        private float _maxCurrentValue;
        
        [SerializeField]
        private float _baseValue;
        public float BaseValue => _baseValue;
        
        public float CurrentValue { get; private set; }
        
        public readonly List<Modifier> Modifiers = new ();

        public void Initialize()
        {
            CalculateCurrentValue();
        }

        public void AddModifier(Modifier modifier)
        {
            Modifiers.Add(modifier);
            CalculateCurrentValue();
        }

        public void RemoveModifier(Modifier modifier)
        {
            Modifiers.Remove(modifier);
            CalculateCurrentValue();
        }

        private void CalculateCurrentValue()
        {
            var result = BaseValue;

            foreach (var modifier in Modifiers.Where(mod => mod.Type == ModifierType.Plain))
            {
                result += modifier.Delta;
            }
            
            foreach (var modifier in Modifiers.Where(mod => mod.Type == ModifierType.Multiply))
            {
                result *= modifier.Delta;
            }

            CurrentValue = Mathf.Clamp(result, _minCurrentValue, _maxCurrentValue);
        }
    }
}
