using UnityEngine;

namespace Enemy_Scripts.States
{
    public abstract class EnemyState : State
    {
        protected Enemy _owner;

        public override void Setup()
        {
            _owner = GetComponent<Enemy>();
        }
    }
}