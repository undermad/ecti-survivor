using System;
using KBCore.Refs;
using UnityEngine;
using UnityHFSM;

namespace Explorer._Project.Scripts.Enemy.fsm
{
    public class Test : MonoBehaviour
    {
        
        private StateMachine fsm;

        public float playerScanningRange = 4f;
        public float ownScanningRange = 6f;
        [SerializeField, Anywhere] private Transform player;
        
        Vector2 PlayerPosition => player.transform.position;
        float DistanceToPlayer => Vector2.Distance(PlayerPosition, transform.position);
        
        void MoveTowardsPlayer(float speed) {
            transform.position = Vector2.MoveTowards(
                transform.position,
                PlayerPosition,
                speed * Time.deltaTime
            );
        }
        
        void RotateAtSpeed(float speed)
            => transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
        
        private void Awake()
        {
            fsm = new StateMachine();

            fsm.AddState("ExtractIntel");
            fsm.AddState("FollowPlayer", onLogic: state => MoveTowardsPlayer(1));
            fsm.AddState("FleeFromPlayer", onLogic: state => MoveTowardsPlayer(-1));

            fsm.SetStartState("FollowPlayer");

            fsm.AddTwoWayTransition("ExtractIntel", "FollowPlayer",
                transition => DistanceToPlayer > ownScanningRange);

            fsm.AddTwoWayTransition("ExtractIntel", "FleeFromPlayer",
                transition => DistanceToPlayer < playerScanningRange);
            
            var extractIntel = new StateMachine(true);
            fsm.AddState("ExtractIntel", extractIntel);
            extractIntel.AddState("SendData",
                onLogic: state => RotateAtSpeed(100f),
                canExit: state => state.timer.Elapsed > 5,
                needsExitTime: true
            );
            
            extractIntel.AddState("CollectData"
                // The canExit function is another way to define when the state is
                // allowed to exit (it calls `fsm.StateCanExit()` internally).
                // canExit: state => state.timer.Elapsed > 5,
                // needsExitTime: true
            );

            extractIntel.AddTransition("SendData", "CollectData");

            // Exit transition without a condition.
            extractIntel.AddExitTransition("CollectData");

            extractIntel.AddTransition(new TransitionAfter("CollectData", "SendData", 5));
            
            fsm.Init();
        }
        
        private void Update()
        {
            fsm.OnLogic();
        }

        void DoSomething()
        {
            
        }
        
    }
}