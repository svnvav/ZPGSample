using System;
using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> _items;
        public List<Item> Items => _items;

        private void Awake()
        {
            _items = new List<Item>();
        }

        public void Put(Item item)
        {
            item.transform.parent = transform;
            _items.Add(item);
        }
        
        public void Grab(Item item)
        {
            _items.Remove(item);
        }
    }
}