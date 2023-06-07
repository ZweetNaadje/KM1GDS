using Enemy_Scripts.States;
using Player_Scripts;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Enemy_Scripts
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(EnemyAttackState))]
    [RequireComponent(typeof(EnemyPatrolState))]
    public class Enemy : ShipEntity
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Transform[] _bulletSpawnPoints;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _smokeVFX;
        [SerializeField] private GameObject[] _cannons;
        [SerializeField] private GameObject[] _barrels;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _patrolArea;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shipHornAudioClip;
        [SerializeField] private bool _shouldRotateToPlayer;

        private Vector3 _targetPoint;
        private float _nextFireTime;
        
        public Player Player;
        public override int Health => _health;
        public override int MaxHealth => _maxHealth;
        public override Transform[] BulletSpawnPoints => _bulletSpawnPoints;
        public override GameObject BulletPrefab => _bulletPrefab;
        public override GameObject SmokeVFX => _smokeVFX;
        public override GameObject[] Cannons => _cannons;
        public override GameObject[] Barrels => _barrels;
        public override float FireRate => _fireRate;
        public override float BulletSpeed => _bulletSpeed;
        public override float AttackRange => _attackRange;
        public float MoveSpeed => _moveSpeed;
        public AudioSource AudioSource => _audioSource;
        public AudioClip ShipHornAudioClip => _shipHornAudioClip;
        public bool ShouldRotateToPlayer => _shouldRotateToPlayer;


        private void Start()
        {
            _health = _maxHealth;
        }

        public void Move()
        {
            float distanceTargetPoint = Vector3.Distance(transform.position, _targetPoint);
            var rotateToThis = Vector3.RotateTowards(transform.forward, _targetPoint - transform.position, 10f * Time.deltaTime, 10f);

            if (distanceTargetPoint > 0.1f)
            {
                // Move towards the target point
                transform.position =
                    Vector3.MoveTowards(transform.position, _targetPoint, MoveSpeed * Time.deltaTime);

                transform.rotation = Quaternion.LookRotation(rotateToThis);
            } 
            // Check if the target point has been reached
            else if (Vector3.Distance(transform.position,_targetPoint) <= 1f)
            {
                // Pick a new random point within the defined area
                _targetPoint = GetRandomPointInArea();
            }
        }

        //TODO: Fix all enemies moving towards (0, 0, 0) initially instead of instantly getting a random point
        public Vector3 GetRandomPointInArea()
        {
            float randomX = Random.Range(_patrolArea.position.x - 350f, _patrolArea.position.x + 300f);
            float randomZ = Random.Range(_patrolArea.position.z - 400f, _patrolArea.position.z + 500f);

            return new Vector3(randomX, transform.position.y, randomZ);
        }
        
        public Vector3 RandomPointFromShipPos()
        {
            var shipPos = transform.position;

            // Generate random coordinates within the defined area
            float randomX = Random.Range(shipPos.x - 100, shipPos.x + 100);
            float randomZ = Random.Range(shipPos.z - 100, shipPos.z + 100);
            
            Debug.Log("I got used");

            // Return the random point within the defined area
            return new Vector3(randomX, transform.position.y, randomZ);
        }

        public override void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                GameManager.Enemies.Remove(this);
                Destroy(gameObject, 1.0f);
            }
        }

        public override void ShootDeckCannon()
        {
            if (!CanAttack())
            {
                return;
            }

            foreach (var cannon in _bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(_bulletPrefab, cannon.position, cannon.rotation);
                GameObject smokeVfx = Instantiate(_smokeVFX, cannon.position, cannon.rotation);
                
                var personalCollider = GetComponent<Collider>();
                var bulletCollider = bullet.GetComponent<Collider>();
                
                Physics.IgnoreCollision(personalCollider, bulletCollider);

                bullet.GetComponent<Rigidbody>().velocity = cannon.forward * _bulletSpeed; 
            
                smokeVfx.GetComponent<ParticleSystem>().Play();
            }
        }

        public override bool CanAttack()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            
            if (Time.time >= _nextFireTime && distanceToPlayer < AttackRange)
            {
                _nextFireTime = Time.time + (1f / _fireRate);
                return true;
            }

            return false;
        }

        public override void LookAtPlayer()
        {
            var point = Player.transform.position;

            foreach (var barrel in _barrels)
            {
                barrel.transform.LookAt(point);
                barrel.transform.localRotation = new Quaternion(barrel.transform.localRotation.x, 0, 0, barrel.transform.localRotation.w);
            }

            foreach (var cannon in _cannons)
            {
                cannon.transform.LookAt(new Vector3(point.x, cannon.transform.position.y, point.z));
            }
        }
    }
}