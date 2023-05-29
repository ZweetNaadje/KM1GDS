using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    

    private float _nextFireTime;
    
    public void Shooting()
    {
        if (Time.time >= _nextFireTime)
        {
            foreach (var cannon in _bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(_bulletPrefab, cannon.position, cannon.rotation);
                bullet.GetComponent<Rigidbody>().velocity = cannon.forward * _bulletSpeed;
                
                _nextFireTime = Time.time + (1f / _fireRate);
            }
        }
        
        
    }
}

