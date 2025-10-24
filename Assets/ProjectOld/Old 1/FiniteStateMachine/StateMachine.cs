using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Project.Scripts.FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode current;
        private Dictionary<Type, StateNode> nodes = new();
        private HashSet<Transition> allTransitions = new();

        public void Update()
        {
            if (!current.State.CanStopAnimation())
            {
                current.State?.Update();
                return;
            }
            
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }
            current.State?.Update();
        }

        public void FixedUpdate()
        {
            current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }

        void ChangeState(IState state)
        {
            if(state == current.State) return;

            var previousState = current.State;
            var nextState = nodes[state.GetType()];
            
            previousState?.OnExit();
            nextState.State?.OnEnter();
            current = nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (var transition in allTransitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            foreach (var transition in current.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }
        
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            allTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<Transition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<Transition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}