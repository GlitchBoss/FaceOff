using UnityEngine;
using System.Collections;
using System;

namespace GLITCH.Helpers
{
	public class Helpers
	{

	}

	[Serializable]
	public class RangeInt
	{
		public int max;
		public int min = 0;
		public int num;
	}

	[Serializable]
	public class RangeFloat
	{

		public float max;
		public float min = 0;
		public float num;
	}

	[Serializable]
	public class RandomInt
	{
		public int min, max;
		public int random
		{
			get { return UnityEngine.Random.Range(min, max); }
		}
	}

	[Serializable]
	public class RandomFloat
	{
		public float min, max;
		public float random
		{
			get { return UnityEngine.Random.Range(min, max); }
		}
	}
}
