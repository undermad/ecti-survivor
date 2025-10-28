using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.Spawner
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