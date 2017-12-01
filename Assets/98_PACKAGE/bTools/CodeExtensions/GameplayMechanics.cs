using UnityEngine;

namespace bTools.CodeExtensions
{
	[System.Serializable]
	public class Cooldown
	{
		public float duration { get; private set; }
		public bool isDone
		{
			get
			{
				return timestamp < Time.time;
			}
		}

		private float timestamp;

		/// <summary>
		/// Starts this cooldown with a new duration
		/// </summary>
		public void Start( float duration )
		{
			this.duration = duration;
			timestamp = Time.time + duration;
		}

		/// <summary>
		/// Resets this cooldown using the last specified duration
		/// </summary>
		public void Reset()
		{
			timestamp = Time.time + duration;
		}
	}

	public class UnscaledCooldown
	{
		public float duration { get; private set; }
		public bool isDone
		{
			get
			{
				return timestamp < Time.unscaledTime;
			}
		}

		private float timestamp;

		/// <summary>
		/// Starts this cooldown with a new duration
		/// </summary>
		public void Start( float duration )
		{
			this.duration = duration;
			timestamp = Time.unscaledTime + duration;
		}

		/// <summary>
		/// Resets this cooldown using the last specified duration
		/// </summary>
		public void Reset()
		{
			timestamp = Time.unscaledTime + duration;
		}
	}

	[System.Serializable]
	public struct RandomInt
	{
		public int min;
		public int max;
		public int last { get; private set; }

		public int Next()
		{
			last = Random.Range( min, max + 1 );
			return last;
		}
	}


	[System.Serializable]
	public struct RandomFloat
	{
		public float min;
		public float max;
		public float last { get; private set; }

		public float Next()
		{
			last = Random.Range( min, max );
			return last;
		}
	}
}