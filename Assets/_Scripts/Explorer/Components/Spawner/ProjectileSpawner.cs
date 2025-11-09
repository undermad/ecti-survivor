using System;
using System.Collections.Generic;
using Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus;
using Explorer._Scripts.Explorer.Components.Abilities;
using Explorer._Scripts.Explorer.Objects;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Pool;

namespace Explorer._Scripts.Explorer.Components.Spawner
{
    public class ProjectileSpawner : ValidatedMonoBehaviour
    {
        [SerializeField, Parent] private PersistentId persistentId;
        [SerializeField, Anywhere]private Projectile projectilePrefab;
        private EventBinding<ProjectileAbilityActivatedEvent> projectileAbilityActivatedEventBinding;
        private IObjectPool<Projectile> projectilePool;

        private void Start()
        {
            projectilePool = new ObjectPool<Projectile>(
                () =>
                {
                    var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    projectile.Init(ReleaseAction);
                    return projectile;
                },
                projectile =>
                {
                    projectile.transform.position = transform.position;
                    projectile.gameObject.SetActive(true);
                },
                projectile =>
                {
                    projectile.gameObject.SetActive(false);
                },
                projectile => Destroy(projectile.gameObject),
                collectionCheck: true);
        }

        private void OnEnable()
        {
            projectileAbilityActivatedEventBinding =
                new EventBinding<ProjectileAbilityActivatedEvent>(HandleProjectileAbilityActivatedEvent);
            EventBus<ProjectileAbilityActivatedEvent>.Subscribe(persistentId.ID, projectileAbilityActivatedEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ProjectileAbilityActivatedEvent>.UnSubscribe(persistentId.ID, projectileAbilityActivatedEventBinding);
        }

        private void HandleProjectileAbilityActivatedEvent(ProjectileAbilityActivatedEvent e)
        {
            var ownerAsc = e.ownerAsc;
            projectilePool.Get(out var projectile);
            projectile.SetOwner(ownerAsc);
        }

        private void ReleaseAction(Projectile projectile)
        {
            projectilePool.Release(projectile);
        }
    }
}