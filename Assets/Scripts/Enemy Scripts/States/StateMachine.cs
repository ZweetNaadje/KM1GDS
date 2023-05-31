using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Scripts.States
{
    /// <summary>
    /// This Class, StateMachine, has the Monobehaviour Update, FixedUpdate loops.
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<System.Type, State> _states = new Dictionary<System.Type, State>();

        public State StartState;
        public State CurrentState;

        public void Start()
        {
            State[] allStates = GetComponents<State>();
            
            //Each state has a statemachine
            foreach (State state in allStates)
            {
                state.SetStateMachine(this);
                state.Setup();
                _states.Add(state.GetType(), state);
            }

            SwitchState(StartState.GetType());
        }

        public void Update()
        {
            CurrentState?.OnUpdate();
        }

        public void FixedUpdate()
        {
            CurrentState?.OnFixedUpdate();
        }

        public void SwitchState(System.Type stateType)
        {
            if (_states.ContainsKey(stateType))
            {
                CurrentState?.OnExit();
                CurrentState = _states[stateType];
                CurrentState?.OnEnter();
            }
            else
            {
                Debug.LogWarning("State not found in dictionary!");
            }
        }
    }
}