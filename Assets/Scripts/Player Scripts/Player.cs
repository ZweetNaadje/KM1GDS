using UnityEngine;

namespace Player_Scripts
{
    public class Player : Entity
    {
        //Player logic, not input!

        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed = 10f;

        private bool _isBurrowed;
    
        //When you want to do something with these 2 variables, but only as read-only.
        public override int Health => _health;
        public override int MaxHealth => _maxHealth;
        public bool IsBurrowed => _isBurrowed;

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

        public void ToggleBurrowed()
        {
            var xVector = transform.position.x;
            var yVector = transform.position.y;
            var zVector = transform.position.z;
        
            _isBurrowed = !_isBurrowed;

            if (_isBurrowed)
            {
                transform.localPosition = new Vector3(xVector, yVector - 7.07f, zVector);
                _moveSpeed = 15f;
            }
            else
            {
                transform.localPosition = new Vector3(xVector, yVector + 7.07f, zVector);
                _moveSpeed = 7f;
            }
        }

        public void Move(float horizontal, float vertical)
        {
            Vector3 movement = new Vector3(0f, 0f, vertical).normalized;

            //Rotate along the Y-axis cuz Y is up.
            Vector3 rotation = new Vector3(0f, horizontal * _rotationSpeed, 0f);

            transform.Rotate(rotation * Time.deltaTime);
            transform.Translate(movement * _moveSpeed * Time.deltaTime);
        }
    }
}

