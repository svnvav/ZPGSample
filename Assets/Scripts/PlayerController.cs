using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

namespace Svnvav.Samples
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private LayerMask _hitLayer;
        private Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);
        private Vector2 TouchPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(TouchPos,Vector2.zero, Mathf.Infinity, _hitLayer);
                var cell = _tilemap.WorldToCell(hit.point);
                transform.position = cell + _tilemap.cellSize * 0.5f;
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
    }
}
