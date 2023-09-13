using System;

namespace Units.Attributes
{
    [Serializable]
    public class Buff
    {
        public TimeLimitedModifier Modifier;
        public TimeLimitedModifier SupportModifier;
        public UnitAttributes TargetAttributes;
    }
}
