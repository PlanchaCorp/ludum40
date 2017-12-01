using UnityEngine;

namespace bTools.CodeExtensions
{
	public static class ColorExtensions
	{
		/// <summary>
		/// Returns this color with the specified red component
		/// </summary>
		public static Color WithRed( this Color c, float red )
		{
			return new Color( red, c.g, c.b, c.a );
		}

		/// <summary>
		/// Returns this color with the specified green component
		/// </summary>
		public static Color WithGreen( this Color c, float green )
		{
			return new Color( c.r, green, c.b, c.a );
		}

		/// <summary>
		/// Returns this color with the specified blue component
		/// </summary>
		public static Color WithBlue( this Color c, float blue )
		{
			return new Color( c.r, c.g, blue, c.a );
		}

		/// <summary>
		/// Returns this color with the specified alpha component
		/// </summary>
		public static Color WithAlpha( this Color c, float alpha )
		{
			return new Color( c.r, c.g, c.b, alpha );
		}

		/// <summary>
		/// Returns this color with the specified red component
		/// </summary>
		public static Color32 WithRed( this Color32 c, byte red )
		{
			return new Color32( red, c.g, c.b, c.a );
		}

		/// <summary>
		/// Returns this color with the specified green component
		/// </summary>
		public static Color32 WithGreen( this Color32 c, byte green )
		{
			return new Color32( c.r, green, c.b, c.a );
		}

		/// <summary>
		/// Returns this color with the specified blue component
		/// </summary>
		public static Color32 WithBlue( this Color32 c, byte blue )
		{
			return new Color32( c.r, c.g, blue, c.a );
		}

		/// <summary>
		/// Returns this color with the specified alpha component
		/// </summary>
		public static Color32 WithAlpha( this Color32 c, byte alpha )
		{
			return new Color32( c.r, c.g, c.b, alpha );
		}
	}
}