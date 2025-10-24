using System;

namespace Explorer._Project.Scripts.UniteAustin2017.EventSystem.EventBus
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}