using Interfaces;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public virtual int Health => 10;
    public virtual int MaxHealth => 10;
    
    public virtual void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}