using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Explorer._Project.Scripts.Enemy
{
    public class Enemy : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private Transform target;
        [SerializeField, Self] private NavMeshAgent navMeshAgent;
        [SerializeField, Child] private Attributes attributes;
        [SerializeField, Child] private HealthBar HealthBar;

        private void Start()
        {
            HealthBar.UpdateHealthBar(attributes.MaxHealth, attributes.CurrentHealth);
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }
        
        private void Update()
        {
            Debug.Log(navMeshAgent.isOnNavMesh);
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.SetDestination(target.position);
            }
        }


    }
}