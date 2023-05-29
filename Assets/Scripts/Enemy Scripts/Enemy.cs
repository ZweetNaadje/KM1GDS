using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_Scripts
{
    public class Enemy : Entity
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private NavMeshAgent _agent;
        
        public Transform coneApex;
        public Vector3 coneDirection;
        public float coneAngle;
        public float maxDistance;
        public GameObject Prefab;

        public override int Health => _health;
        public override int MaxHealth => _maxHealth;

        private void Start()
        {
            _health = _maxHealth;

            Vector3 randomPoint = GetRandomPointInCone();
            Debug.Log("Random Point: " + randomPoint);

            Instantiate(Prefab, randomPoint, Quaternion.identity);
        }

        public override void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Destroy(gameObject, 1.0f);
            }
        }

        private void Move()
        {
            
        }
        
        private Vector3 GetRandomPointInCone()
        {
            float cosHalfAngle = Mathf.Cos(coneAngle * 0.5f * Mathf.Deg2Rad);
            float maxAngle = Mathf.Acos(cosHalfAngle);

            float randomAngle = UnityEngine.Random.Range(-maxAngle, maxAngle);
            Vector3 rotationAxis = UnityEngine.Random.onUnitSphere;
            Quaternion randomRotation = Quaternion.AngleAxis(randomAngle * Mathf.Rad2Deg, rotationAxis);
            Vector3 randomDirection = randomRotation * coneDirection;

            float randomDistance = UnityEngine.Random.Range(0f, maxDistance);
            Vector3 randomPoint = coneApex.position + randomDirection * randomDistance;

            return randomPoint;
        }
    }
}