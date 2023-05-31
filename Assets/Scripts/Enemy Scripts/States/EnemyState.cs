namespace Enemy_Scripts.States
{
    public class EnemyState : State
    {
        protected Enemy _owner;
        public override void Setup()
        {
            _owner = GetComponent<Enemy>();
        }
    }
}