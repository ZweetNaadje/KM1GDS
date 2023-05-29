using UnityEngine;

namespace Sketches
{
    public class Sketch_Enemy : MonoBehaviour, Sketch_IHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }


        private void Start()
        {
            MaxHealth = 50;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}