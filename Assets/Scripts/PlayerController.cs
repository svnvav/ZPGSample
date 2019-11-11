using UnityEngine;
using UnityEngine.AI;

namespace Svnvav.Samples
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = TouchRay;
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
    }
}
