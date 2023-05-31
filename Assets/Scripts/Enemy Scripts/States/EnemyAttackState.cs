using Player_Scripts;
using UnityEngine;

namespace Enemy_Scripts.States
{
    public class EnemyAttackState : EnemyState
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private Player _player;
        
        public override void OnEnter()
        {
            //TODO: NullreferenceException
            _owner.Agent.SetDestination(_owner.Player.transform.position);
        }

        public override void OnUpdate()
        {
            //TODO: NullreferenceException
            float distanceToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);

            if (_player.IsBurrowed)
            {
                return;
            }
            
            if (distanceToPlayer < _attackRange * 1.5f)
            {
                //Fire a flare in Y-direction of the enemy
                //Play a ship horn
                
                //Look at player
                _owner.LookAtPlayer();
            }

            if (distanceToPlayer < _attackRange)
            {
                //Shoot at player
                _owner.Shooting();

                if (_player.Health <= 0)
                {
                    stateMachine.SwitchState(typeof(EnemyPatrolState));
                }
            }
        }
    }
}