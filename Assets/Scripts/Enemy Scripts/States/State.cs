using UnityEngine;

namespace Enemy_Scripts.States
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine stateMachine;

        public void SetStateMachine(StateMachine machine)
        {
            stateMachine = machine;
        }

        public virtual void Setup()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}