using System;
using Interfaces;
using Player_Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_Scripts
{
    public class Enemy : Entity
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Transform[] _bulletSpawnPoints;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _smokeVFX;
        [SerializeField] private GameObject[] _cannons;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _attackRange;
  

        private float _nextFireTime;
        
        public NavMeshAgent Agent;
        public Player Player;
        
        public override int Health => _health;
        public override int MaxHealth => _maxHealth;
        public override Transform[] BulletSpawnPoints => _bulletSpawnPoints;
        public override GameObject BulletPrefab => _bulletPrefab;
        public override GameObject SmokeVFX => _smokeVFX;
        public override GameObject[] Cannons => _cannons;
        public override float FireRate => _fireRate;
        public override float BulletSpeed => _bulletSpeed;
        public override float AttackRange => _attackRange;
        

        private void Start()
        {
            _health = _maxHealth;
        }

        public override void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Destroy(gameObject, 1.0f);
            }
        }

        public override void Shooting()
        {
            if (!CanAttack())
            {
                return;
            }

            foreach (var cannon in _bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(_bulletPrefab, cannon.position, cannon.rotation);
                GameObject smokeVfx = Instantiate(_smokeVFX, cannon.position, cannon.rotation);
            
                bullet.GetComponent<Rigidbody>().velocity = cannon.forward * _bulletSpeed; 
            
                smokeVfx.GetComponent<ParticleSystem>().Play();
            }
        }

        public override bool CanAttack()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            
            if (Time.time >= _nextFireTime || distanceToPlayer < _attackRange)
            {
                _nextFireTime = Time.time + (1f / _fireRate);
                return true;
            }

            return false;
        }

        public override void LookAtPlayer()
        {
            var point = Player.transform.position;
            point.y = 0f;

            foreach (var cannon in _cannons)
            {
                cannon.transform.LookAt(point);
            }
        }
    }
}