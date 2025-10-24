using Explorer._Project.Scripts.EventBus.Events;

namespace Explorer._Project.Scripts.Player.Events
{
    public struct AnimationEvent : IEvent
    {
        public AnimationEventPhase Phase;
        public string AnimationClipName;
        public AnimationTag Tag;
    }

    public enum AnimationEventPhase
    {
        Start,
        End,
    }

    public enum AnimationTag
    {
        WeaponAttack
    } 
}