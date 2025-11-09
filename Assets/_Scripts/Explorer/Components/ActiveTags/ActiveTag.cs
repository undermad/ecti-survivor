using System;
using System.Collections.Generic;
using Explorer._Scripts.Explorer.Systems.CombatSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.ActiveTags
{
    [Serializable]
    public class ActiveTag
    {
        public GameplayTag gameplayTag;
        public Sprite icon;
    }
}