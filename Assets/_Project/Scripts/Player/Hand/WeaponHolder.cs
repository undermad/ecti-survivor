using System;
using Explorer._Project.Scripts.EventBus;
using Explorer._Project.Scripts.Player.Events;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand
{
    public class WeaponHolder : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private GameObject unarmedPrefab;
        [SerializeField, Anywhere] private GameObject axePrefab;

        [SerializeField, Anywhere] private GameObject _currentWeapon;

        [SerializeField, Parent] private Animator animator;

        private EventBinding<ChangeWeaponEvent> _changeWeaponEventBinding;

        private bool _isUnarmed;

        private void OnEnable()
        {
            _changeWeaponEventBinding = new EventBinding<ChangeWeaponEvent>(HandleChangeWeaponEvent);
            EventBus<ChangeWeaponEvent>.Subscribe(_changeWeaponEventBinding);
        }

        private void OnDisable()
        {
            EventBus<ChangeWeaponEvent>.UnSubscribe(_changeWeaponEventBinding);
        }


        private void Equip(GameObject gameObject)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            _currentWeapon = Instantiate(gameObject, transform);
            _currentWeapon.name = gameObject.name;
            animator.Rebind();
            _isUnarmed = !_isUnarmed;
        }

        private void HandleChangeWeaponEvent()
        {
            Equip(_isUnarmed ? axePrefab : unarmedPrefab);
        }
    }
}