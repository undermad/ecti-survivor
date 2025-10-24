using System;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Input/InputData")]
    public class InputData : ScriptableObject
    {
        [NonSerialized] public Vector2 Direction = Vector2.zero;
        [NonSerialized] public bool IsFiring = false;
        [NonSerialized] public Vector2 Look = Vector2.zero;
        
        [NonSerialized] public Vector2 PointerScreen = Vector2.zero;
        [NonSerialized] public Vector2 PointerWorld = Vector2.zero;
    }
}