using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Svnvav.Samples
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> _items;
        public int ItemsCount => _items.Count;

        public void Put(Item item)
        {
            item.transform.parent = transform;
            _items.Add(item);
        }
        
        public Item Grab(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);
            return item;
        }

        public void Throw(int index)
        {
            var item = Grab(index);
            item.transform.parent = null;
        }

        public void ThrowAll()
        {
            foreach (var item in _items)
            {
                item.transform.parent = null;
            }
            _items.Clear();
        }
    }
}