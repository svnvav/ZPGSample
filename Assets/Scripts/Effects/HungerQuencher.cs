using UnityEngine;

namespace Svnvav.Samples
{
    public class HungerQuencher : Effect
    {
        [SerializeField]
        private float _value;

        public override float Value => _value;
    }
}