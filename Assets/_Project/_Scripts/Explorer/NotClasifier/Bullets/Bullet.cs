using System;
using Explorer._Project.Scripts.UniteAustin2017.InputSystem;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.Bullets
{
    public class Bullet : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private BulletData attributes;
        [SerializeField, Anywhere] private InputData inputData;
        
        private Vector2 direction;
        
        private void Start()
        {
            var from = (Vector2) transform.position;
            var to = inputData.PointerWorld;
            direction = -(from - to).normalized;
        }

        private void Update()
        {
            transform.Translate(direction * (attributes.speed * Time.deltaTime));
        }
    }
}