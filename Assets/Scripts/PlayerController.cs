
using System.Linq;
using UnityEngine;

namespace Svnvav.Samples
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NavTileAgent _navAgent;
        [SerializeField] private LayerMask _hitLayer;
        private Vector2 TouchPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private Vector3[] _path;
        private Vector3 _prevPoint, _nextPoint;
        private int _pathStep;
        private float _pathPointsTransition = 0f;
        private bool _moving;
        private Rigidbody2D _rigidbody;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(TouchPos, Vector2.zero, Mathf.Infinity, _hitLayer);
                _navAgent.SetDestination(hit.point);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * Time.deltaTime);
            }
        }

//        private void OnDrawGizmos()
//        {
//            if(_path == null) return;
//            
//            Gizmos.color = Color.red;
//            var prev = new Vector3(_path[0].x, _path[0].y, 0) + _tilemap.cellSize * 0.5f;
//            for (int i = 1; i < _path.Length; i++)
//            {
//                var current = new Vector3(_path[i].x, _path[i].y, 0) + _tilemap.cellSize * 0.5f;
//                Gizmos.DrawLine(prev, current);
//                Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)i / _path.Length);
//                prev = current;
//            }
//        }
    }
}