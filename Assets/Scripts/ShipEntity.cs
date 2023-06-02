using Interfaces;
using UnityEngine;

public abstract class ShipEntity : MonoBehaviour, IDamageable
{
    public virtual Transform[] BulletSpawnPoints { get; }
    public virtual GameObject BulletPrefab { get; }
    public virtual GameObject SmokeVFX { get; }
    public virtual GameObject[] Cannons { get; }
    
    public virtual GameObject[] Barrels { get; }
    
    public virtual float FireRate => 1f;
    public virtual float BulletSpeed => 50f;
    public virtual float AttackRange => 500f;
    public virtual int Health => 10;
    public virtual int MaxHealth => 10;

    public virtual bool CanAttack()
    {
        throw new System.NotImplementedException();
    }

    public virtual void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Shooting()
    {
        throw new System.NotImplementedException();
    }

    public virtual void LookAtPlayer()
    {
        throw new System.NotImplementedException();
    }
}