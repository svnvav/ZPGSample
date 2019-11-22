using UnityEngine;

namespace Svnvav.Samples
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Collider2D _detectionCollider;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private Inventory _owner;
        public Inventory Owner => _owner;
        
        private int _idInInventory;
        public int IdInInventory => _idInInventory;

        public void SetInventory(Inventory inventory, int idInInventory)
        {
            _owner = inventory;
            _idInInventory = idInInventory;
            DefineState();
        }

        private void DefineState()
        {
            if (_owner == null)
            {
                _animator.Play("OnTheGround");
                _detectionCollider.enabled = true;
            }
            else
            {
                _animator.Play("InTheInventory");
                _detectionCollider.enabled = false;
            }
        }
    }
}