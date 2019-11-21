using UnityEngine;

namespace Svnvav.Samples
{
    public class CameraFlying : MonoBehaviour
    {
        [SerializeField] private float _edgeWidth;
        [SerializeField] private float _speed;

        private void Update()
        {
            if (Input.mousePosition.x > Screen.width - _edgeWidth)
            {
                transform.Translate(_speed  * Time.deltaTime * Vector3.right);
            }
            else if (Input.mousePosition.x < _edgeWidth)
            {
                transform.Translate(_speed  * Time.deltaTime * Vector3.left);
            }
            
            if (Input.mousePosition.y > Screen.height - _edgeWidth)
            {
                transform.Translate(_speed  * Time.deltaTime * Vector3.up);
            } 
            else if (Input.mousePosition.y < _edgeWidth)
            {
                transform.Translate(_speed  * Time.deltaTime * Vector3.down);
            }
        }
    }
}