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
            
            _owner.Move();
            FOOScanning();
        }

        private void FOOScanning()
        {
            var distToPlayer = Vector3.Distance(transform.position, _owner.Player.transform.position);

            Debug.Log(distToPlayer);
            
            if (distToPlayer < _detectionRange && !_owner.Player.IsBurrowed)
            {
                stateMachine.SwitchState(typeof(EnemyAttackState));
            }
        }

        // private void ScanForPlayer()
        // {
        //     //Check for transitions
        //     //Attackable layer can be used to simulate AI attacking AI maybe? For now ignore that
        //     
        //     //WIP Workaround
        //     int maxColliders = 10;
        //     Collider[] colls = Physics.OverlapSphere(transform.position, _detectionRange, _attackableLayer);
        //     
        //
        //     
        //     
        //     Debug.Log(colls.Length);
        //     //TODO: Colls length is not getting cleared thus causing enemy to constantly switch between states since colls is never 0 again
        //     if (colls.Length != 0)
        //     {
        //         //TODO: This line needs attention. Im kinda lost as to how to do this.
        //         stateMachine.SwitchState(typeof(EnemyAttackState));
        //         
        //     }
        // }

       
        

        
    }
}