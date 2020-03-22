// Adapted from StrangeIOC Signals
// Removed event/delegate architecture and replaced with basic action list in order to provide AOT compatibility in a third party dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WildbotLabs.Signals
{
    /// Base concrete form for a Signal with no parameters
    public class Signal
    {
        private readonly ThreadSafeList<Action> m_oneTimeListeners = new ThreadSafeList<Action>();
        private readonly ThreadSafeList<Action> m_listeners = new ThreadSafeList<Action>();
        private readonly WeakActionList m_weakListeners = new WeakActionList();

        public void AddListener(Action callback)
        {
            if (!m_listeners.Contains(callback))
            {
                m_listeners.Add(callback);
            }
        }

        public void AddWeakListener(Action callback)
        {
            m_weakListeners.Add(callback);
        }

        public void AddOneTimeListener(Action callback)
        {
            m_oneTimeListeners.Add(callback);
        }

        public void RemoveListener(Action callback)
        {
            m_listeners.Remove(callback);
        }

        public void Dispatch()
        {
            foreach (var listener in m_listeners)
            {
                listener();
            }

            for (int i = m_oneTimeListeners.Count - 1; i >= 0; i--)
            {
                var listener = m_oneTimeListeners[i];
                m_oneTimeListeners.RemoveAt(i);
                listener();
            }

            var cleanup = false;
            foreach (var weakListener in m_weakListeners.OfType<WeakActionHandler>())
            {
                cleanup = weakListener.Invoke();
            }

            if (cleanup)
            {
                m_weakListeners.PruneDeadTargets();
            }
        }
    }

    /// Base concrete form for a Signal with one parameter
    public class Signal<T>
    {
        private readonly ThreadSafeList<Action<T>> m_oneTimeListeners = new ThreadSafeList<Action<T>>();
        private readonly ThreadSafeList<Action<T>> m_listeners = new ThreadSafeList<Action<T>>();
        private readonly WeakActionList m_weakListeners = new WeakActionList();

        public void AddListener(Action<T> callback)
        {
            if (!m_listeners.Contains(callback))
            {
                m_listeners.Add(callback);
            }
        }
        public void AddWeakListener(Action<T> callback)
        {
            m_weakListeners.Add(callback);
        }

        public void AddOneTimeListener(Action<T> callback)
        {
            m_oneTimeListeners.Add(callback);
        }

        public void RemoveListener(Action<T> callback)
        {
            m_listeners.Remove(callback);
        }

        public void Dispatch(T data)
        {
            foreach (var listener in m_listeners)
            {
                listener(data);
            }

            foreach(var listener in m_oneTimeListeners)
            {
                // This is only safe because ThreadSafeList copies the enumerator
                m_oneTimeListeners.Remove(listener);
                listener(data);
            }

            var cleanup = false;
            foreach (var weakListener in m_weakListeners.OfType<WeakActionHandler<T>>())
            {
                cleanup = weakListener.Invoke(data);
            }

            if (cleanup)
            {
                m_weakListeners.PruneDeadTargets();
            }
        }
    }

    /// Base concrete form for a Signal with two parameters
    public class Signal<T, U>
    {
        private readonly ThreadSafeList<Action<T, U>> m_oneTimeListeners = new ThreadSafeList<Action<T, U>>();
        private readonly ThreadSafeList<Action<T, U>> m_listeners = new ThreadSafeList<Action<T, U>>();
        private readonly WeakActionList m_weakListeners = new WeakActionList();

        public void AddListener(Action<T, U> callback)
        {
            if (!m_listeners.Contains(callback))
            {
                m_listeners.Add(callback);
            }
        }

        public void AddWeakListener(Action<T, U> callback)
        {
            m_weakListeners.Add(callback);
        }

        public void AddOneTimeListener(Action<T, U> callback)
        {
            m_oneTimeListeners.Add(callback);
        }

        public void RemoveListener(Action<T, U> callback)
        {
            m_listeners.Remove(callback);
        }

        public void Dispatch(T type1, U type2)
        {
            foreach(var listener in m_listeners)
            {
                listener(type1, type2);
            }

            foreach (var listener in m_oneTimeListeners)
            {
                // This is only safe because ThreadSafeList copies the enumerator
                m_oneTimeListeners.Remove(listener);
                listener(type1, type2);
            }

            var cleanup = false;
            foreach (var weakListener in m_weakListeners.OfType<WeakActionHandler<T, U>>())
            {
                cleanup = weakListener.Invoke(type1, type2);
            }

            if (cleanup)
            {
                m_weakListeners.PruneDeadTargets();
            }
        }
    }

    /// Base concrete form for a Signal with three parameters
    public class Signal<T, U, V>
    {
        private readonly ThreadSafeList<Action<T, U, V>> m_listeners = new ThreadSafeList<Action<T, U, V>>();

        public void AddListener(Action<T, U, V> callback)
        {
            if (!m_listeners.Contains(callback))
            {
                m_listeners.Add(callback);
            }
        }

        public void RemoveListener(Action<T, U, V> callback)
        {
            m_listeners.Remove(callback);
        }

        public void Dispatch(T type1, U type2, V type3)
        {
            foreach (var listener in m_listeners)
            {
                listener(type1, type2, type3);
            }
        }
    }

    /// Base concrete form for a Signal with four parameters
    public class Signal<T, U, V, W>
    {
        private readonly ThreadSafeList<Action<T, U, V, W>> m_listeners = new ThreadSafeList<Action<T, U, V, W>>();

        public void AddListener(Action<T, U, V, W> callback)
        {
            if (!m_listeners.Contains(callback))
            {
                m_listeners.Add(callback);
            }
        }

        public void RemoveListener(Action<T, U, V, W> callback)
        {
            m_listeners.Remove(callback);
        }

        public void Dispatch(T type1, U type2, V type3, W type4)
        {
            foreach (var listener in m_listeners)
            {
                listener(type1, type2, type3, type4);
            }
        }
    }

    public interface IWeakActionHandler
    {
        bool IsAlive { get; }
        bool IsSameAction(object otherAction);
    }

    /// <summary>
    /// Maintains a weak reference to an action with a single parameter of type T
    /// </summary>
    public class WeakActionHandler : IWeakActionHandler
    {
        private readonly WeakReference m_targetReference;
        private readonly MethodInfo m_method;

        /// <summary>
        /// Create a WeakActionHandler for the provided action
        /// </summary>
        /// <param name="callback">Action to maintain a weak reference for</param>
        public WeakActionHandler(Action callback)
        {
            m_method = callback.Method;
            m_targetReference = new WeakReference(callback.Target, true);
        }

        /// <summary>
        /// Is the target reference still alive
        /// </summary>
        public bool IsAlive => m_targetReference.IsAlive;

        /// <summary>
        /// Attempt to synchronously invoke the action with the give parameter.  
        /// </summary>
        public bool Invoke()
        {
            var target = m_targetReference.Target;

            Action callback = null;

            // Always check target != null and do not use IsAlive property: see MSDN Remarks - 
            // http://msdn.microsoft.com/en-us/library/system.weakreference.isalive(v=vs.100).aspx
            if (target != null)
            {
                callback = (Action)Delegate.CreateDelegate(typeof(Action), target, m_method, true);
            }
            else if (m_method.IsStatic)
            {
                callback = (Action)Delegate.CreateDelegate(typeof(Action), m_method, true);
            }

            if (callback != null)
            {
                callback();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if the provide action is the same Method and has the same Target as the wrapped action
        /// </summary>
        /// <param name="otherAction">Action to compare with</param>
        /// <returns>True if the Method and Target are the same</returns>
        public bool IsSameAction(object otherAction)
        {
            return IsSameAction(otherAction as Action);
        }

        public bool IsSameAction(Action otherAction)
        {
            return m_method == otherAction.Method && m_targetReference.Target == otherAction.Target;
        }
    }

    public class WeakActionHandler<T> : IWeakActionHandler
    {
        private readonly WeakReference m_targetReference;
        private readonly MethodInfo m_method;

        /// <summary>
        /// Create a WeakActionHandler for the provided action
        /// </summary>
        /// <param name="callback">Action to maintain a weak reference for</param>
        public WeakActionHandler(Action<T> callback)
        {
            m_method = callback.Method;
            m_targetReference = new WeakReference(callback.Target, true);
        }

        /// <summary>
        /// Is the targer reference still alive
        /// </summary>
        public bool IsAlive
        {
            get { return m_targetReference.IsAlive; }
        }

        /// <summary>
        /// Attempt to synchronously invoke the action with the give parameter.  
        /// </summary>
        /// <param name="message">Parameter to invoke the action with</param>
        public bool Invoke(T message)
        {
            var target = m_targetReference.Target;

            Action<T> callback = null;

            // Always check target != null and do not use IsAlive property: see MSDN Remarks - 
            // http://msdn.microsoft.com/en-us/library/system.weakreference.isalive(v=vs.100).aspx
            if (target != null)
            {
                callback = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), target, m_method, true);
            }
            else if (m_method.IsStatic)
            {
                callback = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), m_method, true);
            }

            if (callback != null)
            {
                callback(message);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if the provide action is the same Method and has the same Target as the wrapped action
        /// </summary>
        /// <param name="otherAction">Action to compare with</param>
        /// <returns>True if the Method and Target are the same</returns>
        public bool IsSameAction(object otherAction)
        {
            return IsSameAction(otherAction as Action<T>);
        }

        public bool IsSameAction(Action<T> otherAction)
        {
            return m_method == otherAction.Method && m_targetReference.Target == otherAction.Target;
        }
    }

    public class WeakActionHandler<T, U> : IWeakActionHandler
    {
        private readonly WeakReference m_targetReference;
        private readonly MethodInfo m_method;

        /// <summary>
        /// Create a WeakActionHandler for the provided action
        /// </summary>
        /// <param name="callback">Action to maintain a weak reference for</param>
        public WeakActionHandler(Action<T, U> callback)
        {
            m_method = callback.Method;
            m_targetReference = new WeakReference(callback.Target, true);
        }

        /// <summary>
        /// Is the targer reference still alive
        /// </summary>
        public bool IsAlive
        {
            get { return m_targetReference.IsAlive; }
        }

        /// <summary>
        /// Attempt to synchronously invoke the action with the give parameter.  
        /// </summary>
        /// <param name="message1">Parameter to invoke the action with</param>
        /// <param name="message2">Parameter to invoke the action with</param>
        public bool Invoke(T message1, U message2)
        {
            var target = m_targetReference.Target;

            Action<T, U> callback = null;

            // Always check target != null and do not use IsAlive property: see MSDN Remarks - 
            // http://msdn.microsoft.com/en-us/library/system.weakreference.isalive(v=vs.100).aspx
            if (target != null)
            {
                callback = (Action<T, U>)Delegate.CreateDelegate(typeof(Action<T, U>), target, m_method, true);
            }
            else if (m_method.IsStatic)
            {
                callback = (Action<T, U>)Delegate.CreateDelegate(typeof(Action<T, U>), m_method, true);
            }

            if (callback != null)
            {
                callback(message1, message2);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if the provide action is the same Method and has the same Target as the wrapped action
        /// </summary>
        /// <param name="otherAction">Action to compare with</param>
        /// <returns>True if the Method and Target are the same</returns>
        public bool IsSameAction(object otherAction)
        {
            return IsSameAction(otherAction as Action<T, U>);
        }

        public bool IsSameAction(Action<T, U> otherAction)
        {
            return m_method == otherAction.Method && m_targetReference.Target == otherAction.Target;
        }
    }


    /// <summary>
    /// Thread safe collection of IWeakActionWrapper
    /// </summary>
    public class WeakActionList : IEnumerable<IWeakActionHandler>
    {
        private readonly List<IWeakActionHandler> m_internalList = new List<IWeakActionHandler>();
        private readonly object m_sync = new object();

        /// <summary>
        /// Thread safe enumerator of the WeakActionList based on a snapshot of the list created when GetEnumerator is called.
        /// Changes to the WeakActionList during enumeration will not be reflected in the retured enumerator.
        /// </summary>
        /// <returns>Enumerator of the snapshot</returns>
        public IEnumerator<IWeakActionHandler> GetEnumerator()
        {
            List<IWeakActionHandler> tempList;
            lock (m_sync)
            {
                tempList = m_internalList.ToList();
            }

            return tempList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Add an action/dispatcher pair to the collection
        /// </summary>
        /// <param name="action">Action to add</param>
        public void Add(Action action)
        {
            lock (m_sync)
            {
                if (!m_internalList.Any(t => t.IsSameAction(action)))
                {
                    m_internalList.Add(new WeakActionHandler(action));
                }
            }
        }

        public void Add<T>(Action<T> action)
        {
            lock (m_sync)
            {
                if (!m_internalList.Any(t => t.IsSameAction(action)))
                {
                    m_internalList.Add(new WeakActionHandler<T>(action));
                }
            }
        }

        public void Add<T, U>(Action<T, U> action)
        {
            lock (m_sync)
            {
                if (!m_internalList.Any(t => t.IsSameAction(action)))
                {
                    m_internalList.Add(new WeakActionHandler<T, U>(action));
                }
            }
        }

        /// <summary>
        /// Remove an action/dispatcher pair to the collection
        /// </summary>
        /// <typeparam name="T">Parameter type for the action</typeparam>
        /// <param name="action">Action to add</param>
        public void Remove<T>(Action<T> action)
        {
            lock (m_sync)
            {
                var existing = m_internalList.FirstOrDefault(t => t.IsSameAction(action));
                if (existing != null)
                {
                    m_internalList.Remove(existing);
                }
            }
        }

        /// <summary>
        /// Remove any items for the collection with references to dead targets
        /// </summary>
        public void PruneDeadTargets()
        {
            lock (m_sync)
            {
                m_internalList.RemoveAll(t => !t.IsAlive);
            }
        }
    }


    public class ThreadSafeList<T> : IEnumerable<T>
    {
        private readonly List<T> m_internalList = new List<T>();
        private readonly object m_sync = new object();

        public int Count { get; private set; }

        /// <summary>
        /// Thread safe enumerator of the WeakActionList based on a snapshot of the list created when GetEnumerator is called.
        /// Changes to the WeakActionList during enumeration will not be reflected in the retured enumerator.
        /// </summary>
        /// <returns>Enumerator of the snapshot</returns>
        public IEnumerator<T> GetEnumerator()
        {
            List<T> tempList;
            lock (m_sync)
            {
                tempList = m_internalList.ToList();
            }

            return tempList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Add an action/dispatcher pair to the collection
        /// </summary>
        public void Add(T item)
        {
            lock (m_sync)
            {
                if (!m_internalList.Contains(item))
                {
                    m_internalList.Add(item);
                    Count++;
                }
            }
        }

        /// <summary>
        /// Remove an action/dispatcher pair to the collection
        /// </summary>
        public void Remove(T item)
        {
            lock (m_sync)
            {
                var existing = m_internalList.FirstOrDefault(t => Equals(t, item));
                if (existing != null)
                {
                    m_internalList.Remove(existing);
                    Count--;
                }
            }
        }

        public T this[int i]
        {
            get
            {
                lock (m_sync)
                {
                    return m_internalList[i];
                }
            }
        }

        public void RemoveAt(int i)
        {
            lock (m_sync)
            {
                m_internalList.RemoveAt(i);
            }
        }
    }
}
