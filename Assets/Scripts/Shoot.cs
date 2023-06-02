using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _smokeVfx;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;

    private float _nextFireTime;

    public void Shooting()
    {
        if (!CanAttack())
        {
            return;
        }

        foreach (var cannon in _bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(_bulletPrefab, cannon.position, cannon.rotation);
            GameObject smokeVfx = Instantiate(_smokeVfx, cannon.position, cannon.rotation);

            var personalCollider = GetComponent<Collider>();
            var bulletCollider = bullet.GetComponent<Collider>();
            
            Physics.IgnoreCollision(personalCollider, bulletCollider);

            bullet.GetComponent<Rigidbody>().velocity = cannon.forward * _bulletSpeed;

            smokeVfx.GetComponent<ParticleSystem>().Play();
        }
    }

    public bool CanAttack()
    {
        if (Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + (1f / _fireRate);
            return true;
        }

        return false;
    }
}