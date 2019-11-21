
using System.Linq;
using UnityEngine;

namespace Svnvav.Samples
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
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
            var tilemap = Game.Instance.Board.Tilemap;
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(TouchPos, Vector2.zero, Mathf.Infinity, _hitLayer);
                var cell = tilemap.WorldToCell(hit.point);
                var playerCell = tilemap.WorldToCell(transform.position);
                _path = AStar.FindPath(playerCell, cell, tilemap)
                    .Select(p => new Vector3(p.x + tilemap.cellSize.x * 0.5f, p.y + tilemap.cellSize.y * 0.5f, 0))
                    .ToArray();
                _pathStep = 0;
                _moving = true;
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

            Move();
        }

        private void Move()
        {
            if (!_moving) return;

            _pathPointsTransition += _speed * Time.deltaTime;
            while (_pathPointsTransition > 1f)
            {
                _pathPointsTransition -= 1f;
                _pathStep++;
                if (_pathStep == _path.Length - 1)
                {
                    transform.position = _path[_pathStep];
                    _pathPointsTransition = 0f;
                    _moving = false;
                    return;
                }
                //_destination = _path[_pathStep];
            }
            transform.position = Vector3.Lerp(_path[_pathStep], _path[_pathStep + 1], _pathPointsTransition);
            //transform.Translate((_destination - transform.position).normalized * Time.deltaTime);
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