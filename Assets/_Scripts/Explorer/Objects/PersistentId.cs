using System;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.Character
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-10000)]
    public sealed class PersistentId : MonoBehaviour
    {
        public Guid ID { get; private set; }

        private void Awake()
        {
            ID = Guid.NewGuid();
        }
    }
}