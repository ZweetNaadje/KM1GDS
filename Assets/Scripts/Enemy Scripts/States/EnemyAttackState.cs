using System.Security.Cryptography.X509Certificates;
using Player_Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy_Scripts.States
{
    public class EnemyAttackState : EnemyState
    {
        [SerializeField] private float _attackRange;
        
        public override void OnEnter()
        {
            Debug.Log("EnemyAttackState: OnEnter called!");
            
            if (_owner.Player == null)
            {
                Debug.LogWarning("_owner.Player is null!");
            }
        }

        public override void OnUpdate()
        {
            Debug.Log("EnemyAttackState: OnUpdate called!");
            
            if (_owner.Player.IsBurrowed)
            {
                stateMachine.SwitchState(typeof(EnemyPatrolState));
                return;
            }
            
            EngagePlayer();

            /*if (distanceToPlayer < _attackRange * 1.5f)
            {
                //Fire a flare in Y-direction of the enemy
                //Play a ship horn
                /*transform.position = Vector3.MoveTowards(transform.position, _owner.Player.transform.position,
                    _owner.MoveSpeed * Time.deltaTime);#1#
                
                //Look at player
                _owner.LookAtPlayer();
            }*/
        }

        private void EngagePlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);


            if (distanceToPlayer < 100f)
            {
                transform.position = transform.position;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _owner.Player.transform.position, _owner.MoveSpeed * Time.deltaTime);
            }
            
            //Move towards player
            //When in attack range - offset (so its not constantly on the edge of the attackrange)
            //Start circling around the player, 
            //whilst shooting
            
            if (distanceToPlayer < _attackRange)
            {
                //Shoot at player
                _owner.LookAtPlayer();
                _owner.Shooting();

                if (_owner.Player.Health <= 0)
                {
                    stateMachine.SwitchState(typeof(EnemyPatrolState));
                }
            }
        }
    }
}