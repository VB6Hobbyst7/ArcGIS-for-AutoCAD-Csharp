using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AFA
{
	[Serializable]
	public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, INotifyCollectionChanged, INotifyPropertyChanged
	{
		protected class KeyedDictionaryEntryCollection<TKey> : KeyedCollection<TKey, DictionaryEntry>
		{
			public KeyedDictionaryEntryCollection()
			{
			}

			public KeyedDictionaryEntryCollection(IEqualityComparer<TKey> comparer) : base(comparer)
			{
			}

			protected override TKey GetKeyForItem(DictionaryEntry entry)
			{
				return (TKey)((object)entry.Key);
			}
		}

		[Serializable]
		public struct Enumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IDictionaryEnumerator, IEnumerator
		{
			private ObservableDictionary<TKey, TValue> _dictionary;

			private int _version;

			private int _index;

			private KeyValuePair<TKey, TValue> _current;

			private bool _isDictionaryEntryEnumerator;

			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					this.ValidateCurrent();
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					this.ValidateCurrent();
					if (this._isDictionaryEntryEnumerator)
					{
						return new DictionaryEntry(this._current.Key, this._current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					this.ValidateCurrent();
					return new DictionaryEntry(this._current.Key, this._current.Value);
				}
			}

			object IDictionaryEnumerator.Key
			{
				get
				{
					this.ValidateCurrent();
					return this._current.Key;
				}
			}

			object IDictionaryEnumerator.Value
			{
				get
				{
					this.ValidateCurrent();
					return this._current.Value;
				}
			}

			internal Enumerator(ObservableDictionary<TKey, TValue> dictionary, bool isDictionaryEntryEnumerator)
			{
				this._dictionary = dictionary;
				this._version = dictionary._version;
				this._index = -1;
				this._isDictionaryEntryEnumerator = isDictionaryEntryEnumerator;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				this.ValidateVersion();
				this._index++;
				if (this._index < this._dictionary._keyedEntryCollection.Count)
				{
					this._current = new KeyValuePair<TKey, TValue>((TKey)((object)this._dictionary._keyedEntryCollection[this._index].Key), (TValue)((object)this._dictionary._keyedEntryCollection[this._index].Value));
					return true;
				}
				this._index = -2;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			private void ValidateCurrent()
			{
				if (this._index == -1)
				{
					throw new InvalidOperationException("The enumerator has not been started.");
				}
				if (this._index == -2)
				{
					throw new InvalidOperationException("The enumerator has reached the end of the collection.");
				}
			}

			private void ValidateVersion()
			{
				if (this._version != this._dictionary._version)
				{
					throw new InvalidOperationException("The enumerator is not valid because the dictionary changed.");
				}
			}

			void IEnumerator.Reset()
			{
				this.ValidateVersion();
				this._index = -1;
				this._current = default(KeyValuePair<TKey, TValue>);
			}
		}

		protected ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection<TKey> _keyedEntryCollection;

		private int _countCache;

		private Dictionary<TKey, TValue> _dictionaryCache = new Dictionary<TKey, TValue>();

		private int _dictionaryCacheVersion;

		private int _version;

		[NonSerialized]
		private SerializationInfo _siInfo;

		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		protected virtual event PropertyChangedEventHandler PropertyChanged;

		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this._keyedEntryCollection.Comparer;
			}
		}

		public int Count
		{
			get
			{
				return this._keyedEntryCollection.Count;
			}
		}

		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return this.TrueDictionary.Keys;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				return (TValue)((object)this._keyedEntryCollection[key].Value);
			}
			set
			{
				this.DoSetEntry(key, value);
			}
		}

		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return this.TrueDictionary.Values;
			}
		}

		private Dictionary<TKey, TValue> TrueDictionary
		{
			get
			{
				if (this._dictionaryCacheVersion != this._version)
				{
					this._dictionaryCache.Clear();
					foreach (DictionaryEntry current in this._keyedEntryCollection)
					{
						this._dictionaryCache.Add((TKey)((object)current.Key), (TValue)((object)current.Value));
					}
					this._dictionaryCacheVersion = this._version;
				}
				return this._dictionaryCache;
			}
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return (TValue)((object)this._keyedEntryCollection[key].Value);
			}
			set
			{
				this.DoSetEntry(key, value);
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				return this._keyedEntryCollection[(TKey)((object)key)].Value;
			}
			set
			{
				this.DoSetEntry((TKey)((object)key), (TValue)((object)value));
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		int ICollection<KeyValuePair<TKey, TValue>>.Count
		{
			get
			{
				return this._keyedEntryCollection.Count;
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		int ICollection.Count
		{
			get
			{
				return this._keyedEntryCollection.Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._keyedEntryCollection).IsSynchronized;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._keyedEntryCollection).SyncRoot;
			}
		}

		public ObservableDictionary()
		{
			this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection<TKey>();
		}

		public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection<TKey>();
			foreach (KeyValuePair<TKey, TValue> current in dictionary)
			{
				this.DoAddEntry(current.Key, current.Value);
			}
		}

		public ObservableDictionary(IEqualityComparer<TKey> comparer)
		{
			this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection<TKey>(comparer);
		}

		public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			this._keyedEntryCollection = new ObservableDictionary<TKey, TValue>.KeyedDictionaryEntryCollection<TKey>(comparer);
			foreach (KeyValuePair<TKey, TValue> current in dictionary)
			{
				this.DoAddEntry(current.Key, current.Value);
			}
		}

		protected ObservableDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		public void Add(TKey key, TValue value)
		{
			this.DoAddEntry(key, value);
		}

		public void Clear()
		{
			this.DoClearEntries();
		}

		public bool ContainsKey(TKey key)
		{
			return this._keyedEntryCollection.Contains(key);
		}

		public bool ContainsValue(TValue value)
		{
			return this.TrueDictionary.ContainsValue(value);
		}

		public IEnumerator GetEnumerator()
		{
			return new ObservableDictionary<TKey, TValue>.Enumerator<TKey, TValue>(this, false);
		}

		public bool Remove(TKey key)
		{
			return this.DoRemoveEntry(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			bool flag = this._keyedEntryCollection.Contains(key);
			value = (flag ? ((TValue)((object)this._keyedEntryCollection[key].Value)) : default(TValue));
			return flag;
		}

		protected virtual bool AddEntry(TKey key, TValue value)
		{
			this._keyedEntryCollection.Add(new DictionaryEntry(key, value));
			return true;
		}

		protected virtual bool ClearEntries()
		{
			bool flag = this.Count > 0;
			if (flag)
			{
				this._keyedEntryCollection.Clear();
			}
			return flag;
		}

		protected int GetIndexAndEntryForKey(TKey key, out DictionaryEntry entry)
		{
			entry = default(DictionaryEntry);
			int result = -1;
			if (this._keyedEntryCollection.Contains(key))
			{
				entry = this._keyedEntryCollection[key];
				result = this._keyedEntryCollection.IndexOf(entry);
			}
			return result;
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		protected virtual void OnPropertyChanged(string name)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		protected virtual bool RemoveEntry(TKey key)
		{
			return this._keyedEntryCollection.Remove(key);
		}

		protected virtual bool SetEntry(TKey key, TValue value)
		{
			bool flag = this._keyedEntryCollection.Contains(key);
			if (flag && value.Equals((TValue)((object)this._keyedEntryCollection[key].Value)))
			{
				return false;
			}
			if (flag)
			{
				this._keyedEntryCollection.Remove(key);
			}
			this._keyedEntryCollection.Add(new DictionaryEntry(key, value));
			return true;
		}

		private void DoAddEntry(TKey key, TValue value)
		{
			if (this.AddEntry(key, value))
			{
				this._version++;
				DictionaryEntry entry;
				int indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
				this.FireEntryAddedNotifications(entry, indexAndEntryForKey);
			}
		}

		private void DoClearEntries()
		{
			if (this.ClearEntries())
			{
				this._version++;
				this.FireResetNotifications();
			}
		}

		private bool DoRemoveEntry(TKey key)
		{
			DictionaryEntry entry;
			int indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
			bool flag = this.RemoveEntry(key);
			if (flag)
			{
				this._version++;
				if (indexAndEntryForKey > -1)
				{
					this.FireEntryRemovedNotifications(entry, indexAndEntryForKey);
				}
			}
			return flag;
		}

		private void DoSetEntry(TKey key, TValue value)
		{
			DictionaryEntry entry;
			int indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
			if (this.SetEntry(key, value))
			{
				this._version++;
				if (indexAndEntryForKey > -1)
				{
					this.FireEntryRemovedNotifications(entry, indexAndEntryForKey);
					this._countCache--;
				}
				indexAndEntryForKey = this.GetIndexAndEntryForKey(key, out entry);
				this.FireEntryAddedNotifications(entry, indexAndEntryForKey);
			}
		}

		private void FireEntryAddedNotifications(DictionaryEntry entry, int index)
		{
			this.FirePropertyChangedNotifications();
			if (index > -1)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value)), index));
				return;
			}
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void FireEntryRemovedNotifications(DictionaryEntry entry, int index)
		{
			this.FirePropertyChangedNotifications();
			if (index > -1)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value)), index));
				return;
			}
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void FirePropertyChangedNotifications()
		{
			if (this.Count != this._countCache)
			{
				this._countCache = this.Count;
				this.OnPropertyChanged("Count");
				this.OnPropertyChanged("Item[]");
				this.OnPropertyChanged("Keys");
				this.OnPropertyChanged("Values");
			}
		}

		private void FireResetNotifications()
		{
			this.FirePropertyChangedNotifications();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			this.DoAddEntry(key, value);
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			return this.DoRemoveEntry(key);
		}

		bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
		{
			return this._keyedEntryCollection.Contains(key);
		}

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
		{
			return this.TryGetValue(key, out value);
		}

		void IDictionary.Add(object key, object value)
		{
			this.DoAddEntry((TKey)((object)key), (TValue)((object)value));
		}

		void IDictionary.Clear()
		{
			this.DoClearEntries();
		}

		bool IDictionary.Contains(object key)
		{
			return this._keyedEntryCollection.Contains((TKey)((object)key));
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ObservableDictionary<TKey, TValue>.Enumerator<TKey, TValue>(this, true);
		}

		void IDictionary.Remove(object key)
		{
			this.DoRemoveEntry((TKey)((object)key));
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> kvp)
		{
			this.DoAddEntry(kvp.Key, kvp.Value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			this.DoClearEntries();
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> kvp)
		{
			return this._keyedEntryCollection.Contains(kvp.Key);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("CopyTo() failed:  array parameter was null");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("CopyTo() failed:  index parameter was outside the bounds of the supplied array");
			}
			if (array.Length - index < this._keyedEntryCollection.Count)
			{
				throw new ArgumentException("CopyTo() failed:  supplied array was too small");
			}
			foreach (DictionaryEntry current in this._keyedEntryCollection)
			{
				array[index++] = new KeyValuePair<TKey, TValue>((TKey)((object)current.Key), (TValue)((object)current.Value));
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> kvp)
		{
			return this.DoRemoveEntry(kvp.Key);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._keyedEntryCollection).CopyTo(array, index);
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new ObservableDictionary<TKey, TValue>.Enumerator<TKey, TValue>(this, false);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Collection<DictionaryEntry> collection = new Collection<DictionaryEntry>();
			foreach (DictionaryEntry current in this._keyedEntryCollection)
			{
				collection.Add(current);
			}
			info.AddValue("entries", collection);
		}

		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo != null)
			{
				Collection<DictionaryEntry> collection = (Collection<DictionaryEntry>)this._siInfo.GetValue("entries", typeof(Collection<DictionaryEntry>));
				foreach (DictionaryEntry current in collection)
				{
					this.AddEntry((TKey)((object)current.Key), (TValue)((object)current.Value));
				}
			}
		}
	}
}
