using System.Collections.Generic;


namespace Svnvav.Samples
{
    public static class AnimalBehaviourPool<T> where T : AnimalBehaviour, new()
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