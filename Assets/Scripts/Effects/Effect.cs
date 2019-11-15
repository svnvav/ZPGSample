
using UnityEngine;

namespace Svnvav.Samples
{
    [System.Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public abstract bool Apply(Animal animal);
    }
}