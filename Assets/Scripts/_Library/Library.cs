using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace N_Library
{
	public static class Library
	{
		// Extensions

		/// <summary>
		/// Get percentage float value based on _inputValue relative to _min and _max.
		/// </summary>
		/// <param name="_inputValue">Value between _min and _max</param>
		/// <param name="_min">0 percent value</param>
		/// <param name="_max">100 percent value</param>
		/// <returns>Returns percentage float value.</returns>
		public static float GetPercentageRelative(this float _inputValue, float _min, float _max)
		{
			float range = _max - _min;

			if (range == 0)
				return 1;

			float position = _inputValue - _min;
			return System.Math.Abs(position / range);
		}

		/// <summary>
		/// Get percentage float value(0-1) based on _inputValue between _min and _max.
		/// </summary>
		/// <param name="_inputValue">Value between _min and _max</param>
		/// <param name="_min">0 percent value</param>
		/// <param name="_max">100 percent value</param>
		/// <returns>Returns percentage float value.</returns>
		public static float GetPercentage(this float _inputValue, float _min, float _max)
		{
			if (_inputValue > _max)
				_inputValue = _max;
			else if (_inputValue < _min)
				_inputValue = _min;

			return GetPercentageRelative(_inputValue, _min, _max);
		}

		/// <summary>
		/// Counts up over time and returns the value.
		/// </summary>
		/// <param name="_countedTime">The variable for counted time</param>
		/// <param name="_countingLimit"></param>
		/// <returns></returns>
		public static void Timer(this ref float _countedTime, float _countingLimit)
		{
			// Timer
			if (_countedTime <= _countingLimit)
			{
				_countedTime += Time.deltaTime;
			}
			else
			{
				_countedTime = _countingLimit;
			}
		}

		/// <summary>
		/// Check if bit is set.
		/// </summary>
		/// <param name="_b"></param>
		/// <param name="_pos"></param>
		/// <returns></returns>
		public static bool IsBitSet(this byte _b, int _pos)
		{
			return (_b & (1 << _pos)) != 0;
		}

		public static float SqrDistance(this Vector3 _a, Vector3 _b)
		{
			float x = _a.x - _b.x;
			float y = _a.y - _b.y;
			float z = _a.z - _b.z;
			return x * x + y * y + z * z;
		}

		public static void DeleteUnusedVertices(this List<Vector3> _vertices, List<int> _tris)
		{
			List<int> trisDistinct = _tris.Distinct().OrderBy(_x => _x).ToList();
			Dictionary<int, int> newValues = new Dictionary<int, int>();
			List<int> keys = new List<int>(trisDistinct);

			// Deletes unused points and set up dictionary.
			for (int adapt = 0, i = 0; i < _vertices.Count;)
			{
				int compareValue = trisDistinct[i] - adapt;

				if (i == compareValue)
				{
					newValues[keys[i]] = i;
					i++;

					if (i == trisDistinct.Count)
					{
						while (_vertices.Count > trisDistinct.Count)
							_vertices.RemoveAt(i);
						break;
					}
				}
				else
				{
					int iter = compareValue - i;
					adapt = trisDistinct[i] - i;

					for (int k = 0; k < iter; k++)
						_vertices.RemoveAt(i);
				}
			}

			// Updates _tris
			foreach (KeyValuePair<int, int> kvp in newValues)
			{
				for (int i = 0; i < _tris.Count; i++)
				{
					if (_tris[i] == kvp.Key)
					{
						_tris[i] = kvp.Value;
					}
				}
			}
		}
	}
}
