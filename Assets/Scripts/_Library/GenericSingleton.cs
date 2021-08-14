using UnityEngine;

namespace N_Library
{
	[DisallowMultipleComponent]
	public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance { get; private set; }

		protected virtual void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
			}

			// Instance = (T)System.Convert.ChangeType(this,typeof(T));
			Instance = this as T;
		}

		protected virtual void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}
