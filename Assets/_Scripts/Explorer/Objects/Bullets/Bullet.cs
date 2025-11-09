using Explorer._Project.Scripts.UniteAustin2017.Bullets;
using Explorer._Scripts.Explorer.Systems.Core.InputSystem;
using Explorer._Scripts.Explorer.Systems.LifecycleManager;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Objects.Bullets
{
    public class Bullet : ValidatedMonoBehaviour, IUpdateObserver
    {
        [SerializeField, Anywhere] private BulletData attributes;
        
        private Vector2 direction;
        
        private void Start()
        {
            var from = (Vector2) transform.position;
            var to = InputData.PointerWorld;
            direction = -(from - to).normalized;
        }

        private void OnEnable()
        {
            UpdateManager.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.Unregister(this);
        }

        public void CustomUpdate()
        {
            transform.Translate(direction * (attributes.speed * Time.deltaTime));
        }
    }
}