﻿using UnityEngine;

namespace Explorer._Project.Scripts.Utils.Timer
{
    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
            }
            
            if (IsRunning && Time <= 0) {
                Stop();
            }
        }
        
        public bool IsFinished => Time <= 0;
        
        public void Reset() => Time = InitialTime;
        
        public void Reset(float newTime) {
            Debug.Log($"Resetting timer to.");
            InitialTime = newTime;
            Reset();
        }
    }
}