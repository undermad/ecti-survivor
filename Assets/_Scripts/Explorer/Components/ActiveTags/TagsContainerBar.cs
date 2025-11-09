using System;
using System.Collections.Generic;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Objects;
using Explorer._Scripts.Explorer.Systems.CombatSystem.Events;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.ActiveTags
{
    public class TagsContainerBar : ValidatedMonoBehaviour
    {
        [SerializeField, Parent] private PersistentId persistentId;
        [FormerlySerializedAs("tagsData")] public ActiveTagsData activeTagsData;

        private readonly List<ActiveTag> activeTags = new();
        private readonly Dictionary<ActiveTag, GameObject> spawnedObjects = new();

        private EventBinding<TagAddedEvent> tagAddedEventBinding;
        private EventBinding<TagRemovedEvent> tagRemovedEventBinding;

        private void OnEnable()
        {
            tagAddedEventBinding = new EventBinding<TagAddedEvent>(HandleOnTagAdded);
            EventBus<TagAddedEvent>.Subscribe(persistentId.ID, tagAddedEventBinding);
            
            tagRemovedEventBinding = new EventBinding<TagRemovedEvent>(HandleOnTagRemoved);
            EventBus<TagRemovedEvent>.Subscribe(persistentId.ID, tagRemovedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<TagAddedEvent>.UnSubscribe(tagAddedEventBinding);
            EventBus<TagRemovedEvent>.UnSubscribe(tagRemovedEventBinding);
        }

        public void Start()
        {
            Refresh();
        }

        private void Refresh()
        {

            foreach (var spawnedObject in spawnedObjects.Values)
            {
                Destroy(spawnedObject);
            }
            spawnedObjects.Clear();
            
            for(var index = 0; index < activeTags.Count; index++)   
            {
                var activeTag = activeTags[index];
                var newGameObject = new GameObject();
                spawnedObjects.Add(activeTag, newGameObject);
                newGameObject.transform.SetParent(transform);
                
                var ppu = activeTag.icon.pixelsPerUnit;
                var iconHeightPx = activeTag.icon.rect.height;
                var stepY = iconHeightPx / ppu;  
                
                var localPosition = new Vector3(0, index * stepY, 0);
                newGameObject.transform.localPosition = localPosition;
                
                var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = activeTag.icon;
            }
        }

        private void HandleOnTagAdded(TagAddedEvent payload)
        {
            var activeTag = activeTagsData.tags.Find(activeTag => activeTag.gameplayTag.Path.Equals(payload.TagName));
            if (activeTag != null)
            {
                activeTags.Add(activeTag);
                Refresh();
            }
        }

        private void HandleOnTagRemoved(TagRemovedEvent payload)
        {
            var activeTag = activeTags.Find(activeTag => activeTag.gameplayTag.Path.Equals(payload.TagName));
            if (activeTag != null)
            {
                activeTags.Remove(activeTag);
                Refresh();
            }
        }
    }
}