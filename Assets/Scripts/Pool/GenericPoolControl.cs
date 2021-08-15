using System.Collections.Generic;
using N_Library;
using UnityEngine;

namespace N_Pool
{
	public class GenericPoolControl<T, TKey> : GenericSingleton<GenericPoolControl<T, TKey>> where T : Component where TKey : System.Enum
	{
		public Prefab[] Prefabs => m_prefabs;

		[SerializeField]
		private Prefab[] m_prefabs;
		
		private readonly Dictionary<TKey, T> m_prefabDictionary = new Dictionary<TKey, T>();
		private readonly Dictionary<TKey, Queue<T>> m_queueDictionary = new Dictionary<TKey, Queue<T>>();

		[System.Serializable]
		public class Prefab
		{
			public T Item { get => m_item; set => m_item = value; }
			public int PoolStartSize { get => m_poolStartSize; set => m_poolStartSize = value; }
			
			[SerializeField]
			private T m_item;
			[SerializeField]
			private int m_poolStartSize;
		}

		protected override void Awake()
		{
			base.Awake();
			
			for (int i = 0; i < Prefabs.Length; i++)
			{
				TKey key = (TKey)(i as object);

				m_prefabDictionary[key] = Prefabs[i].Item;
				m_queueDictionary[key] = new Queue<T>();

				InstantiateItem(key, Prefabs[i].PoolStartSize);
			}
		}

		private void InstantiateItem(TKey _key, int _poolStartSize)
		{
			for (int i = 0; i < _poolStartSize; i++)
			{
				T item = Instantiate(m_prefabDictionary[_key], transform);
				m_queueDictionary[_key].Enqueue(item);
				// item.gameObject.SetActive(false);
			}
		}

		public T GetItem(TKey _key)
		{
			if (m_queueDictionary[_key].Count > 0)
			{
				T item = m_queueDictionary[_key].Dequeue();
				item.gameObject.SetActive(true);
				return item;
			}
			else
			{
				T item = Instantiate(m_prefabDictionary[_key], transform);
				item.gameObject.SetActive(true);
				return item;
			}
		}

		public void ReturnItem(TKey _key, T _item)
		{
			m_queueDictionary[_key].Enqueue(_item);
			// _item.gameObject.SetActive(false);
		}
	}
}
