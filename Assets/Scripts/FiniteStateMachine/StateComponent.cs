using UnityEngine;

namespace Svnvav.Samples
{
    public abstract class StateComponent : MonoBehaviour
    {
        public abstract void Enter(AnimalWithFsm animal);
        public abstract void GameUpdate(AnimalWithFsm animal);
        
        public abstract void Exit(AnimalWithFsm animal);
    }
}