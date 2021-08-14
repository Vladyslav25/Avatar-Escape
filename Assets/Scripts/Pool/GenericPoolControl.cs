using System.Collections.Generic;
using N_Library;
using UnityEngine;

namespace N_Pool
{
	public class GenericPoolControl<T, TKey> : GenericSingleton<GenericPoolControl<T, TKey>> where T : Component where TKey : System.Enum
	{
		[SerializeField]
		private T[] m_prefabArray;
		[SerializeField]
		private int[] m_poolStartSizes;

		// A list of prefabs
		private readonly Dictionary<TKey, T> m_prefabs = new Dictionary<TKey, T>();

		// A list of pools(Queue)
		private readonly Dictionary<TKey, Queue<T>> m_pools = new Dictionary<TKey, Queue<T>>();

		protected override void Awake()
		{
			base.Awake();
			
			for (int i = 0; i < m_prefabArray.Length; i++)
			{
				TKey key = (TKey)(i as object);

				m_prefabs[key] = m_prefabArray[i];
				m_pools[key] = new Queue<T>();

				InstantiateItem(key, m_poolStartSizes[i]);
			}
		}

		private void InstantiateItem(TKey _key, int _poolStartSize)
		{
			for (int i = 0; i < _poolStartSize; i++)
			{
				T item = Instantiate(m_prefabs[_key], transform);
				m_pools[_key].Enqueue(item);
				item.gameObject.SetActive(false);
			}
		}

		public T GetItem(TKey _key)
		{
			if (m_pools[_key].Count > 0)
			{
				T item = m_pools[_key].Dequeue();
				item.gameObject.SetActive(true);
				return item;
			}
			else
			{
				T item = Instantiate(m_prefabs[_key], transform);
				return item;
			}
		}

		public void ReturnItem(TKey _key, T _item)
		{
			m_pools[_key].Enqueue(_item);
			_item.gameObject.SetActive(false);
		}
	}
}
