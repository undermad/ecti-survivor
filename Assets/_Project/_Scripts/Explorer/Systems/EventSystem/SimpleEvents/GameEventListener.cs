using UnityEngine;
using UnityEngine.Events;

namespace Explorer._Project.Scripts.UniteAustin2017.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;
        
        public void OnEventRaised()
        {
            Response.Invoke();
        }
        
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
    }
}