using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public class GameplayTagContainer
    {
        [SerializeField] private List<string> tags = new();
        public IEnumerable<GameplayTag> Tags {
            get { foreach (var t in tags) yield return new GameplayTag(t); }
        }
        public bool HasTag(GameplayTag tag)
        {
            foreach (var t in Tags)
                if (t.Matches(tag)) return true;
            return false;
        }
        public void AddTag(GameplayTag tag)
        {
            if (!HasExact(tag)) tags.Add(tag.Path);
        }
        public void RemoveTag(GameplayTag tag)
        {
            tags.RemoveAll(t => string.Equals(t, tag.Path, StringComparison.OrdinalIgnoreCase));
        }
        private bool HasExact(GameplayTag tag)
        {
            foreach (var t in tags)
                if (string.Equals(t, tag.Path, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}