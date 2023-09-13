namespace Units.Attributes
{
    public class Modifier
    {
        public float Delta;
        public ModifierType Type;
    }

    public enum ModifierType
    {
        Multiply = 1,
        Plain = 2,
    }
}
