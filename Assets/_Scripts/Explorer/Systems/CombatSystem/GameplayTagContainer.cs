using System;
using System.Collections.Generic;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Systems.CombatSystem.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [Serializable]
    public class GameplayTagContainer
    {
        [SerializeField] private List<string> tags = new();

        public IEnumerable<GameplayTag> Tags
        {
            get
            {
                foreach (var t in tags) yield return new GameplayTag(t);
            }
        }

        public bool HasTag(GameplayTag tag)
        {
            foreach (var t in Tags)
                if (t.Matches(tag))
                    return true;
            return false;
        }

        public void AddTag(GameplayTag tag, Guid ownerId)
        {
            if (!HasExact(tag))
            {
                EventBus<TagAddedEvent>.Publish(ownerId, new TagAddedEvent { Owner = ownerId, TagName = tag.Path });
                tags.Add(tag.Path);
            }
        }

        public void RemoveTag(GameplayTag tag, Guid ownerId)
        {
            Debug.unityLogger.Log($"Raising RemoveTag event, Tag: {tag.Path}");
            EventBus<TagRemovedEvent>.Publish(ownerId, new TagRemovedEvent { Owner = ownerId, TagName = tag.Path });
            tags.RemoveAll(t => string.Equals(t, tag.Path, StringComparison.OrdinalIgnoreCase));
        }

        private bool HasExact(GameplayTag tag)
        {
            foreach (var t in tags)
                if (string.Equals(t, tag.Path, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}