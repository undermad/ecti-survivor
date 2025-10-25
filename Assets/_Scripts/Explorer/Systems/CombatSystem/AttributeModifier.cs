using System;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public class AttributeModifier
    {
        public string AttributeName;
        public ModifierOp Operation = ModifierOp.Add;
        public ScalableFloat Magnitude = ScalableFloat.Constant(0);
    }
}