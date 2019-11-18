using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class StateComponent : MonoBehaviour
    {
        private void OnEnable()
        {
        }
        
        public abstract void Enter(Creature creature);
        public abstract void GameUpdate(Creature creature);
        
        public abstract void Exit(Creature creature);
        
        private void OnDisable()
        {
        }
    }
}