using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public class NavTileAgent : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private Tilemap _tilemap;
        
        private Vector3[] _path;
        private Vector3 _prevPoint;
        private Vector3 _nextPoint;
        private int _pathStep;
        private float _pathPointsTransition;
        private bool _moving;

        public void Initialize()
        {
            _tilemap = GameController.Instance.Board.Tilemap;
            _prevPoint = _tilemap.WorldToCell(transform.position) + _tilemap.cellSize * 0.5f;
            _nextPoint = _prevPoint;
            transform.position = _prevPoint;
            _moving = false;
        }

        private void Update()
        {
            Move();
        }

        public bool SetDestination(Vector3 destination)
        {
            var destinationCell = _tilemap.WorldToCell(destination);
            var tile = _tilemap.GetTile<CustomTile>(destinationCell);
            if (tile.type != GameTileType.Empty)
                return false;
            
            _path = AStar.FindPath(_tilemap.WorldToCell(transform.position), destinationCell, _tilemap)
                .Select(p => new Vector3(p.x + _tilemap.cellSize.x * 0.5f, p.y + _tilemap.cellSize.y * 0.5f, 0))
                .ToArray();


            _pathStep = 0;
            _nextPoint = _path[_pathStep];
            if (transform.position == _nextPoint)
            {
                _prevPoint = transform.position;
                _pathPointsTransition = 1f;
            }
            else
            {
                var diff = _nextPoint - transform.position;
                _prevPoint = _nextPoint - diff.normalized;
                _pathPointsTransition = 1f - diff.magnitude;
                if (_path.Length > 1 && diff.normalized == (_path[0] - _path[1]).normalized)
                {
                    _pathStep++;
                    _prevPoint = _nextPoint;
                    _nextPoint = _path[_pathStep];
                    _pathPointsTransition = diff.magnitude;
                }
            }

            _moving = true;
            return true;
        }

        public void Stop()
        {
            _moving = false;
        }
        
        public void Continue()
        {
            if(_path == null || _pathStep == _path.Length)
                return;

            _moving = true;
        }

        private void Move()
        {
            if (!_moving) return;

            _pathPointsTransition += _speed * Time.deltaTime;
            
            while (_pathPointsTransition > 1f)
            {
                _pathPointsTransition -= 1f;
                _pathStep++;
                if (_pathStep == _path.Length)
                {
                    transform.position = _path[_pathStep - 1];
                    _pathPointsTransition = 0f;
                    _moving = false;
                    return;
                }

                _prevPoint = _nextPoint;
                _nextPoint = _path[_pathStep];
            }
            transform.position = Vector3.Lerp(_prevPoint, _nextPoint, _pathPointsTransition);
        }
    }
}