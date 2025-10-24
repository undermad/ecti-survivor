using UnityEngine;

namespace Explorer._Project.Scripts.Player.Hand
{
    [CreateAssetMenu(fileName = "WeaponAnimationData", menuName = "Weapon/Animation/Weapon Animation Data")]
    public class WeaponAnimationDataSO : ScriptableObject
    {
        public AnimationClip AnimationClip;
    }
}