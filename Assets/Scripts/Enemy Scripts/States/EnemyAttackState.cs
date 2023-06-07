using System.Security.Cryptography.X509Certificates;
using Player_Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy_Scripts.States
{
    public class EnemyAttackState : EnemyState
    {
        private bool _isPlayingAudio;
        
        public override void OnEnter()
        {
            Debug.Log("EnemyAttackState: OnEnter called!");

            if (_owner.Player == null)
            {
                Debug.LogWarning("_owner.Player is null!");
            }
        }

        public override void OnExit()
        {
            _isPlayingAudio = false;
        }

        public override void OnUpdate()
        {
            Debug.Log("EnemyAttackState: OnUpdate called!");

            if (_owner.Player.IsBurrowed)
            {
                stateMachine.SwitchState(typeof(EnemyPatrolState));
                return;
            }
            
            FOOEngage();

            //EngagePlayer();

            /*if (distanceToPlayer < _owner.AttackRange * 1.5f)
            {
                //Fire a flare in Y-direction of the enemy
                //Play a ship horn
                /*transform.position = Vector3.MoveTowards(transform.position, _owner.Player.transform.position,
                    _owner.MoveSpeed * Time.deltaTime);#1#
                
                //Look at player
                _owner.LookAtPlayer();
            }*/
        }

        private void FOOEngage()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);

            if (!_isPlayingAudio)
            {
                _owner.AudioSource.PlayOneShot(_owner.ShipHornAudioClip);
                _isPlayingAudio = true;
            }

            if (distanceToPlayer > _owner.AttackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _owner.Player.transform.position,
                    _owner.MoveSpeed * Time.deltaTime);
            }

            if (_owner.ShouldRotateToPlayer)
            {
                transform.LookAt(_owner.Player.transform);
            }
            
            if (distanceToPlayer < _owner.AttackRange)
            {
                //Shoot at player
                _owner.LookAtPlayer();
                _owner.ShootDeckCannon();

                if (_owner.Player.Health <= 0)
                {
                    stateMachine.SwitchState(typeof(EnemyPatrolState));
                }
            }
        }

        private void EngagePlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);
    
            //When in player is in firing range, or whatever range you define, 
            //Stop with moving towards player,
            //Rotate the side of the ship towards the player,
            //Start circling around the player,
            //whilst shooting
            if (distanceToPlayer < 200f)
            {
                /*var rotateToThis = Vector3.RotateTowards(transform.right, _owner.Player.transform.position - transform.position, 10f * Time.deltaTime, 10f);
                transform.rotation = Quaternion.LookRotation(rotateToThis);
                transform.position = transform.position;*/
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _owner.Player.transform.position,
                    _owner.MoveSpeed * Time.deltaTime);
                
                transform.LookAt(_owner.Player.transform);
            }

            //Move towards player
            //When in attack range - offset (so its not constantly on the edge of the attackrange)
            //Start circling around the player, 
            //whilst shooting

            if (distanceToPlayer > _owner.AttackRange)
            {
                return;
            }
            
            if (distanceToPlayer < _owner.AttackRange)
            {
                //Shoot at player
                _owner.LookAtPlayer();
                _owner.ShootDeckCannon();

                if (_owner.Player.Health <= 0)
                {
                    stateMachine.SwitchState(typeof(EnemyPatrolState));
                }
            }
        }
    }
}