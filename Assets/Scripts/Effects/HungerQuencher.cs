using UnityEngine;

namespace Svnvav.Samples
{
    public class HungerQuencher : Effect
    {
        [SerializeField]
        private float _value;
        
        public override bool Apply(Animal animal)
        {
            animal.Hunger -= _value;
            return false;
        }
    }
}