using System;
using UnityEngine;

namespace Svnvav.Samples
{
    public class SearchKeyPoint : MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor;
        
        public Vector3 Position => transform.position;
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
    }
}