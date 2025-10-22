using System;

namespace Explorer._Project.Scripts.UniteAustin2017.Values
{
    [Serializable]
    public class FloatReference
    {
        public bool useConstant = true;
        public float constantValue;
        public FloatVariable variable;

        public float GetValue()
        {
            return useConstant ? constantValue : variable.value;
        }
    }
}