using UnityEngine;
using UnityEditor;

namespace bTools.CodeExtensions
{
	public static class EditorGameObjectExtensions
	{
		/// <summary>
		/// Changes the visibility of this GameObject and all of it's children and sub-children
		/// </summary>
		public static void SetVisibleRecursively( this GameObject GO, bool state )
		{
			Undo.RecordObject( GO, "Visibility Change" );
			GO.SetActive( state );

			if ( GO.transform.childCount > 0 )
			{
				foreach ( UnityEngine.Transform child in GO.transform )
				{
					Undo.RecordObject( GO, "Visibility Change" );
					child.gameObject.SetVisibleRecursively( state );
				}
			}
		}

		/// <summary>
		/// Changes the lock of this GameObject and all of it's children and sub-children
		/// </summary>
		public static void SetLockedRecursively( this GameObject GO, bool state )
		{
			GO.SetLocked( state );

			if ( GO.transform.childCount > 0 )
			{
				foreach ( UnityEngine.Transform child in GO.transform )
				{
					child.gameObject.SetLockedRecursively( state );
				}
			}
		}

		/// <summary>
		/// Changes the static flags of this GameObject and all of it's children and sub-children
		/// </summary>
		public static void SetStaticFlagsRecursively( this GameObject GO, StaticEditorFlags flags )
		{
			Undo.RecordObject( GO, "Changed static flags" );
			GameObjectUtility.SetStaticEditorFlags( GO, flags );

			if ( GO.transform.childCount > 0 )
			{
				foreach ( UnityEngine.Transform child in GO.transform )
				{
					Undo.RecordObject( child.gameObject, "Changed static flags" );
					child.gameObject.SetStaticFlagsRecursively( flags );
				}
			}
		}

		/// <summary>
		/// Changes the tag of this GameObject and all of it's children and sub-children
		/// </summary>
		public static void SetTagRecursively( this GameObject GO, string tag )
		{
			Undo.RecordObject( GO, "Changed tag" );
			GO.tag = tag;

			if ( GO.transform.childCount > 0 )
			{
				foreach ( UnityEngine.Transform child in GO.transform )
				{
					Undo.RecordObject( child.gameObject, "Changed tag" );
					child.gameObject.SetTagRecursively( tag );
				}
			}
		}

		/// <summary>
		/// Changes the layer of this GameObject and all of it's children and sub-children
		/// </summary>
		public static void SetLayerRecursively( this GameObject GO, int layer )
		{
			Undo.RecordObject( GO, "Changed layer" );
			GO.layer = layer;

			if ( GO.transform.childCount > 0 )
			{
				foreach ( UnityEngine.Transform child in GO.transform )
				{
					Undo.RecordObject( child.gameObject, "Changed layer" );
					child.gameObject.SetLayerRecursively( layer );
				}
			}
		}

		/// <summary>
		/// Returns true if the object has the HideFlag NotEditable set
		/// </summary>
		public static bool GetLocked( this GameObject GO )
		{
			return ( GO.hideFlags & HideFlags.NotEditable ) != 0;
		}

		/// <summary>
		/// Sets the NotEditable flag on the object while preserving other HideFlags
		/// </summary>
		public static void SetLocked( this GameObject GO, bool status )
		{
			if ( status == true )
			{
				GO.hideFlags |= HideFlags.NotEditable;
				SceneView.RepaintAll();
			}
			else if ( status == false )
			{
				GO.hideFlags &= ~HideFlags.NotEditable;
				SceneView.RepaintAll();
			}
		}
	}
}