using Cinemachine;
using Enemy_Scripts;
using StylizedWater2;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Player_Scripts
{
    public class Player : ShipEntity
    {
        //Player logic, not input!

        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed = 10f;
        
        [SerializeField] private Volume _volumeProfile;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AudioSource _audioSource;

        
        private Vignette _vignette;
        private bool _isBurrowed;
        private bool _isUsingPeriscope;
        
        public CinemachineVirtualCamera PeriscopeCamera;

    
        //When you want to do something with these variables, but only as read-only.
        public override int Health => _health;
        public override int MaxHealth => _maxHealth;
        public bool IsBurrowed => _isBurrowed;


        private void Start()
        {
            _health = _maxHealth;
            _canvas.enabled = false;

            _volumeProfile.profile.TryGet(typeof(Vignette), out Vignette vignette);
    
            if (vignette != null)
            {
                _vignette = vignette;
            }
        }

        public override void TakeDamage(int damage)
        {
            _health -= damage;
            
            _audioSource.PlayOneShot(_audioSource.clip);

            if (_health <= 0)
            {
                SceneManager.LoadScene("Game");
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
                _canvas.enabled = true;
                _volumeProfile.enabled = true;
                PeriscopeCamera.m_Lens.FieldOfView = 40;
                PeriscopeCamera.Priority = 11;
                
                transform.localPosition = new Vector3(xVector, yVector - 7.07f, zVector);
                _moveSpeed = 15f;
            }
            else
            {
                _canvas.enabled = false;
                _volumeProfile.enabled = false;
                PeriscopeCamera.Priority = 9;
                
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
        
        

        //For enabling periscopetoggle aside from being burrowed or not
        public void TogglePeriscope()
        {
            _isUsingPeriscope = !_isUsingPeriscope;

            if (_isUsingPeriscope)
            {
                _canvas.enabled = true;
                _volumeProfile.enabled = true;
                PeriscopeCamera.m_Lens.FieldOfView = 40;
                PeriscopeCamera.Priority = 11;
            }
            else
            {
                _canvas.enabled = false;
                _volumeProfile.enabled = false;
                PeriscopeCamera.Priority = 9;
            }
        }
    }
}

