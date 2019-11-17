using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchStealTarget : StateComponent
    {
        private Skyvan _skyvan;
        public override void Enter(Creature creature)
        {
            _skyvan = (Skyvan) creature;
        }

        public override void GameUpdate(Creature creature)
        {
            
        }

        public override void Exit(Creature creature)
        {
            
        }
    }
}