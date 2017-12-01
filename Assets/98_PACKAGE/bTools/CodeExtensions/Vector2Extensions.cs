using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bTools.CodeExtensions
{
	public static class Vector2Extensions
	{
		/// <summary>
		/// Returns this vector with the specified x component
		/// </summary>
		public static Vector2 WithX( this Vector2 original, float x )
		{
			original.x = x;
			return original;
		}

		/// <summary>
		/// Returns this vector with the specified y component
		/// </summary>
		public static Vector2 WithY( this Vector2 original, float y )
		{
			original.y = y;
			return original;
		}
	}
}