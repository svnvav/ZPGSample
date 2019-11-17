
using System;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Plant : MonoBehaviour, Eatable
    {
        private Effect[] _effects;

        [SerializeField] private float _health = 10f;

        public bool IsAlive => _health > 0f;

        private void Awake()
        {
            _effects = GetComponents<Effect>();
        }

        private void FixedUpdate()
        {
            if (_health <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public Effect[] ToBeEaten(Eater eater)
        {
            _health -= 1f;
            return _effects;
        }
    }
}