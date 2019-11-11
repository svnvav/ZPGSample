using System.Collections.Generic;
using UnityEngine;

namespace Svnvav.Samples
{
    public class CreatureBehaviourPool
    {
        public static class ShapeBehaviourPool<T> where T : CreatureBehaviour, new()
        {
            private static Stack<T> _stack = new Stack<T>();

            public static T Get()
            {
                if (_stack.Count > 0)
                {
                    var instance = _stack.Pop();
                    return instance;
                }

                return new T();
            }

            public static void Reclaim(T instance)
            {
                _stack.Push(instance);
            }
        }
    }
}