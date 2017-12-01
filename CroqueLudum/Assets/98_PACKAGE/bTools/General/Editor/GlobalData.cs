using UnityEditor;
using UnityEngine;

namespace bTools
{
	[InitializeOnLoad]
	public static class GlobalData
	{
		public static Vector3 clipboardPosition;
		public static Vector3 clipboardRotation;
		public static Vector3 clipboardScale;
		public static GameObject clipboardCopyAllComponents;

		static GlobalData()
		{

		}
	}
}
