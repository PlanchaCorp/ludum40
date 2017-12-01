using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace bTools
{
	/// <summary>
	/// Used to manage scene wide and module-independent events such as actions upon selection changes or scene loads.
	/// </summary>
	[InitializeOnLoad]
	public static class GlobalManager
	{
		static GlobalManager()
		{
			EditorApplication.update += EditorUpdate;
		}

		static void EditorUpdate()
		{
			if ( Settings.Get<ToolsSettings_General>().preventLockedFromSelection )
			{
				RemoveLockedFromSelection();
			}
		}

		static void RemoveLockedFromSelection()
		{
			Object[] sel = Selection.objects;
			List<Object> newSel = new List<Object>( sel.Length );

			for ( int i = 0 ; i < sel.Length ; i++ )
			{
				// If it is locked, skip.
				if ( ( (int)sel[i].hideFlags & 8 ) != 0 && sel[i] is GameObject )
				{
					continue;
				}

				// If any of the above conditions are false, keep it selected.
				newSel.Add( sel[i] );
			}

			Selection.objects = newSel.ToArray();
		}
	}
}