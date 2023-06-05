using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Interfaces;
using Sketches;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeAlive = 10f;
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private CinemachineImpulseSource _impulse;
    
    
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

        //TODO: VFX effects dont destroy themselves after being instantiated
        //VFX effect
        GameObject explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        
        //Camerashake on collision
        _impulse.GenerateImpulse();
        
        //TODO: Soundeffect is fucking ass to add
        //Sound effect
        
        
        //Destroy bullet
        Destroy(gameObject);
        Destroy(explosion.gameObject, 5f);
    }
}