using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Interfaces;
using Player_Scripts;
using Sketches;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeAlive = 10f;
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private CinemachineImpulseSource _impulse;
    [SerializeField] private AudioClip _audioClip;
    
    
    private void Awake()
    {
        Destroy(gameObject, _timeAlive);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Deal Damage if possible
        var damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }

        //VFX effect
        GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        
        //Camerashake on collision
        _impulse.GenerateImpulse();
        
        //TODO: Static function will set Spatial blend to 3D (1.0f). Use a different method if you want it to be 2D sound
        //Sound effect
        AudioSource.PlayClipAtPoint(_audioClip, collision.transform.position, 1f);

        //Destroy bullet
        Destroy(gameObject);
    }
}