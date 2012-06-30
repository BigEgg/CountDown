using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BigEgg.Framework.Foundation;

namespace BigEgg.Framework.Applications
{
    /// <summary>
    /// Abstract base class for a DataModel implementation.
    /// </summary>
    public abstract class DataModel : Model
    {
        private readonly List<PropertyChangedEventListener> propertyChangedListeners = new List<PropertyChangedEventListener>();
        private readonly List<CollectionChangedEventListener> collectionChangedListeners = new List<CollectionChangedEventListener>();


        /// <summary>
        /// Adds a weak event listener for a PropertyChanged event.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void AddWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }

            PropertyChangedEventListener listener = new PropertyChangedEventListener(source, handler);

            this.propertyChangedListeners.Add(listener);

            PropertyChangedEventManager.AddListener(source, listener, "");
        }

        /// <summary>
        /// Removes the weak event listener for a PropertyChanged event.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void RemoveWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }

            PropertyChangedEventListener listener = this.propertyChangedListeners.LastOrDefault(l =>
                l.Source == source && l.Handler == handler);

            if (listener != null)
            {
                this.propertyChangedListeners.Remove(listener);
                PropertyChangedEventManager.RemoveListener(source, listener, "");
            }
        }

        /// <summary>
        /// Adds a weak event listener for a CollectionChanged event.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void AddWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }

            CollectionChangedEventListener listener = new CollectionChangedEventListener(source, handler);

            this.collectionChangedListeners.Add(listener);

            CollectionChangedEventManager.AddListener(source, listener);
        }

        /// <summary>
        /// Removes the weak event listener for a CollectionChanged event.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        protected void RemoveWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }

            CollectionChangedEventListener listener = this.collectionChangedListeners.LastOrDefault(l =>
                l.Source == source && l.Handler == handler);

            if (listener != null)
            {
                this.collectionChangedListeners.Remove(listener);
                CollectionChangedEventManager.RemoveListener(source, listener);
            }
        }
    }
}
