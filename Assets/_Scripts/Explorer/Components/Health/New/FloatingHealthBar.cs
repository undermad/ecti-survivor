using System;
using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.Health.New
{
    public class FloatingHealthBar : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private SpriteRenderer fill;

        private float maxWidth;

        private void Awake()
        {
            fill.drawMode = SpriteDrawMode.Sliced;
            fill.transform.localScale = Vector3.one;
            
            var ppu = fill.sprite.pixelsPerUnit;
            fill.size = new Vector2(fill.sprite.rect.width / ppu, fill.sprite.rect.height / ppu);
            maxWidth = fill.size.x;
        }

        public void SetHealth(float healthPercents)
        {
            Debug.unityLogger.Log(healthPercents);
            var t = Mathf.Clamp(healthPercents, 0f, 100f) / 100f;
            Debug.unityLogger.Log(t);
            fill.size = new Vector2(maxWidth * t, fill.size.y);
        }

    }
}