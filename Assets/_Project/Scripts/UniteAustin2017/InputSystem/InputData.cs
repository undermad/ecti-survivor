using System;
using UnityEngine;

namespace Explorer._Project.Scripts.UniteAustin2017.InputSystem
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Input/InputData")]
    public class InputData : ScriptableObject
    {
        [NonSerialized] public Vector2 Direction;
    }
}