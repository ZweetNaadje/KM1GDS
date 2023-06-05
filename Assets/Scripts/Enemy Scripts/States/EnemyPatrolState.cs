using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine.Utility;
using Player_Scripts;
using Unity.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Enemy_Scripts.States
{
    public class EnemyPatrolState : EnemyState
    {
        /// <summary>
        /// 
        /// Set navmesh destination randomly,
        /// Scans occasionally for a player,
        /// If a player is scanned, go to AttackState
        /// If player is burrowed again keep firing at last known position for x seconds,
        /// Then resume patrol state
        /// </summary>

        [SerializeField] private float _detectionRange;
        [SerializeField] private LayerMask _attackableLayer; //useful for when using overlapsphere

        public override void OnExit()
        {
            Debug.Log("EnemyPatrolState: OnExit called!");
        }

        public override void OnEnter()
        {
            Debug.Log("EnemyPatrolState: OnEnter called!");
        }

        public override void OnUpdate()
        {
            Debug.Log("EnemyPatrolState: OnUpdate called!");
            
            ScanForPlayer();
            _owner.Move();
        }

        private void ScanForPlayer()
        {
            var distToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);

            Debug.Log(distToPlayer);
            
            if (distToPlayer < _detectionRange && !_owner.Player.IsBurrowed)
            {
                stateMachine.SwitchState(typeof(EnemyAttackState));
            }
        }
    }
}