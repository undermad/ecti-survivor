using System;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class HitBoxTrigger : MonoBehaviour
    {
        [SerializeField, Parent] private Attack attack;

        private void OnTriggerEnter2D(Collider2D other)
        {
            attack.ProcessHit(other);
        }
    }
}