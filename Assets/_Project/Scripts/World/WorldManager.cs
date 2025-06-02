using System;
using KBCore.Refs;

namespace Explorer._Project.Scripts.World
{
    public class WorldManager : ValidatedMonoBehaviour
    {
        private void Update()
        {
            CooldownManager.TickAllTimers();
        }
    }
}