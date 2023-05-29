using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject[] _smokeVFX;

    private float _nextFireTime;
    
    public void Shooting()
    {
        if (!CanAttack())
        {
            return;
        }

        /*foreach (var smokeVFX in _smokeVFX)
        {
            smokeVFX.SetActive(true);
        }*/
        
        foreach (var cannon in _bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(_bulletPrefab, cannon.position, cannon.rotation);
            bullet.GetComponent<Rigidbody>().velocity = cannon.forward * _bulletSpeed;
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

