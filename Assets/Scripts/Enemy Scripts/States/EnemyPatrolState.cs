using Unity.Collections;
using UnityEngine;

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

        [SerializeField] private float _minimumDistanceToNewWaypoint;
        [SerializeField] private float _detectionRange;
        [SerializeField] private LayerMask _attackableLayer;
        
        
        public override void Setup()
        {
            base.Setup();
        }

        public override void OnEnter()
        {
            //FindNewWanderPosition();
        }

        public override void OnUpdate()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, _owner.transform.position);

            if (distanceToWaypoint > 0.1f)
            {
                //Set destination to a (new) waypoint
            }
            else
            {
                FindNewPatrolPosition();
            }
            
            //Check for transitions
            //Attackable layer can be used to simulate AI attacking AI maybe? For now ignore that
            Collider[] colls = Physics.OverlapSphere(transform.position, _detectionRange, _attackableLayer);
            if (colls.Length != 0)
            {
                //TODO: This line needs attention. Im kinda lost as to how to do this.
                _owner.Player.transform.position = colls[0].gameObject.transform.position;
                stateMachine.SwitchState(typeof(EnemyAttackState));
            }
        }

        private void FindNewPatrolPosition()
        {
            //Make the ai select a new random point atleast x units away from them 
        }

        /*public override void OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(transform.position, _owner.TargetPosition);
            if(distanceToTarget > 0.1f)
            {
                _owner.animator.SetFloat("MoveSpeed", 1f);
                _owner.navMeshAgent.SetDestination(_owner.TargetPosition);
            }
            else
            {
                FindNewWanderPosition();
            }

            //Check for transitions
            Collider[] cols = Physics.OverlapSphere(transform.position, senseRadius, foodLayer);
            if(cols.Length != 0)
            {
                _owner.TargetFood = cols[0].gameObject;
                stateMachine.SwitchState(typeof(AntState_GoToFood));
            }
        }

        private void FindNewWanderPosition()
        {
            _owner.TargetPosition = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
        }*/
    }
}