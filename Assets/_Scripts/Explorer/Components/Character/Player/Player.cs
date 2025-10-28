using System;
using Explorer._Scripts.Explorer.Objects;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Character.Player
{
    public class Player : ValidatedMonoBehaviour, IGetObjectId
    {
        [SerializeField, Parent] private PersistentId persistentId;
        
        public Guid GetObjectId()
        {
            return persistentId.ID;
        }
    }
}