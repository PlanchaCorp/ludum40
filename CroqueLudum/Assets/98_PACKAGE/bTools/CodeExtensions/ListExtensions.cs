using System.Collections.Generic;

namespace bTools.CodeExtensions
{
	public static class ListExtensions
	{
		/// <summary>
		/// Moves element at oldIndex to newIndex
		/// </summary>
		public static void Move<T>( this List<T> list, int oldIndex, int newIndex )
		{
			if ( newIndex < 0 || oldIndex > list.Count || oldIndex < 0 || newIndex > list.Count )
			{
				throw new System.IndexOutOfRangeException();
			}

			var item = list[oldIndex];

			list.RemoveAt( oldIndex );

			// the actual index could have shifted due to the removal
			if ( newIndex > oldIndex ) newIndex--;

			list.Insert( newIndex, item );
		}
	}
}