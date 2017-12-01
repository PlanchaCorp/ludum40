using UnityEngine;

namespace bTools.CodeExtensions
{
	public static class RectExtensions
	{
		public static Rect WithPadding( this Rect rect, float padding )
		{
			rect.x += padding;
			rect.xMax -= padding * 2;
			rect.y += padding;
			rect.yMax -= padding * 2;

			return rect;
		}
	}
}