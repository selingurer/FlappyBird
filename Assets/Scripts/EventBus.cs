namespace DefaultNamespace
{
    using System;
    using System.Collections.Generic;

    public static class EventBus<T>
    {
        private static readonly List<Action<T>> _listeners = new();

        public static void Subscribe(Action<T> listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public static void Unsubscribe(Action<T> listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        public static void Publish(T evt)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]?.Invoke(evt);
            }
        }
    }

}