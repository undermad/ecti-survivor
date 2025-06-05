using UnityEngine;

namespace Explorer._Project.Scripts.World
{
    public class WorldManager : MonoBehaviour
    {
        private void Update()
        {
            CooldownManager.TickAllTimers();
        }
    }
}