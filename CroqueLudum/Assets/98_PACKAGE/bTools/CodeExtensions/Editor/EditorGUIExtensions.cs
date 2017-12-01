using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace bTools.CodeExtensions
{
	public static class EditorGUIExtensions
	{
		public static bool CtrlLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == true
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool AltLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == true
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool ShiftLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == true
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool RightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool LeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool CtrlRightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == true
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool CtrlShiftRightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == true
				&& Event.current.shift == true
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		public static bool ScrollWheelClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 2
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Searches the whole project and attempts to load the first asset matching name (excluding extension)
		/// </summary>
		/// <param name="name">name of the file without extension</param>
		public static T LoadAssetWithName<T>( string name ) where T : UnityEngine.Object
		{
			T asset = null;

			try
			{
				var assetPath = AssetDatabase.GUIDToAssetPath( AssetDatabase.FindAssets( name )[0] );
				asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
			}
			catch ( Exception ex )
			{
				Debug.LogError( "Could not load asset with name " + name + " | Error: " + ex.Message );
			}

			return asset;
		}

		public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
		{
			List<T> assets = new List<T>();
			string[] guids = AssetDatabase.FindAssets( string.Format( "t:{0}", typeof( T ).ToString().Replace( "UnityEngine.", string.Empty ) ) );
			for ( int i = 0 ; i < guids.Length ; i++ )
			{
				string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
				T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
				if ( asset != null )
				{
					assets.Add( asset );
				}
			}
			return assets;
		}

		public static bool LayoutToggleSwitch( GUIContent[] labels, bool value, params GUILayoutOption[] options )
		{
			int i;
			if ( value == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, options );
			}

			return i == 0;
		}

		public static bool LayoutToggleSwitch( string[] labels, bool value, params GUILayoutOption[] options )
		{
			int i;
			if ( value == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, options );
			}

			return i == 0;
		}

		public static bool LayoutToggleSwitch( string[] labels, bool value, GUIStyle style, params GUILayoutOption[] options )
		{
			int i;
			if ( value == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, style, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, style, options );
			}

			return i == 0;
		}

		public static bool LayoutToggleSwitch( GUIContent[] labels, bool value, GUIStyle style, params GUILayoutOption[] options )
		{
			int i;
			if ( value == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, style, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, style, options );
			}

			return i == 0;
		}

		public static T InstanciateScriptableObject<T>() where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var path = AssetDatabase.GenerateUniqueAssetPath( "Assets/" + typeof( T ).ToString() + ".asset" );
			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( path, typeof( T ) ) as T;
			return final;
		}

		public static T InstanciateScriptableObject<T>( string newPath ) where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var path = AssetDatabase.GenerateUniqueAssetPath( newPath + typeof( T ).ToString() + ".asset" );
			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( path, typeof( T ) ) as T;
			return final;
		}

		public static UnityEngine.Object ObjectFromGUID( string guid )
		{
			return AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( guid ), typeof( UnityEngine.Object ) ) as UnityEngine.Object;
		}
	}
}