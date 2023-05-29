using System;
using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public StateMachine()
        {
            _currentState = State.Unburrowed;
        }

        private void Start()
        {
        }

        private void Update()
        {
            CheckState();

            if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionTo(State.Burrowed);
            }
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TransitionTo(State.Unburrowed);
            }
        }

        public void TransitionTo(State nextState)
        {
            _currentState = nextState;
        }

        private void CheckState()
        {
            switch (_currentState)
            {
                case State.Burrowed:
                    BurrowedBehaviour();
                    break;

                case State.Unburrowed:
                    UnburrowedBehaviour();
                    break;
            }
        }

        private void UnburrowedBehaviour()
        {
        }

        private void BurrowedBehaviour()
        {
        }
    }
}