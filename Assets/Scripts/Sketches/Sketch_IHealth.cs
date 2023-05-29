namespace Sketches
{
    public interface Sketch_IHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        void TakeDamage(int damageAmount);
        void Die();
    }
}