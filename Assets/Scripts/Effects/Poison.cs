using UnityEngine;

namespace Svnvav.Samples
{
    public class Poison : Effect
    {
        [SerializeField]
        private float _value;

        public override float Value => _value;
    }
}