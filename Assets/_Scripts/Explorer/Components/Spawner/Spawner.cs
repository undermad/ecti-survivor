using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.Spawner
{
    public class Spawner : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private GameObject prefab;

        public void Spawn()
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}