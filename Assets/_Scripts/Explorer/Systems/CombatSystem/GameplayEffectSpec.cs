using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    public class GameplayEffectSpec
    {
        public GameplayEffectDef Def;
        public float Level;
        public GameObject Instigator;
        public Dictionary<string, float> ResolvedMagnitudes = new(StringComparer.OrdinalIgnoreCase);
    }
}