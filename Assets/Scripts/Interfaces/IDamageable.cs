
namespace Interfaces
{
    public interface IDamageable
    {
        /// <summary>
        /// This property determines the health of the unit.
        /// </summary>
        public int Health { get; }    
        
        /// <summary>
        /// This property determines the maximum health of the unit.
        /// </summary>
        public int MaxHealth { get; }
        
        /// <summary>
        /// This method will cause a unit to take damage with the damage value represented by an int.
        /// </summary>
        /// <param name="damage">Damage is the value of the damage being done to the receiver</param>
        /// <returns></returns>
        public void TakeDamage(int damage);
    }
}