namespace Svnvav.Samples
{
    public class Dead : StateComponent
    {
        public override void Enter(Creature creature)
        {
            Destroy(gameObject);
        }
        
        public override void GameUpdate(Creature creature)
        {
        }

        public override void Exit(Creature creature)
        {
        }
    }
}