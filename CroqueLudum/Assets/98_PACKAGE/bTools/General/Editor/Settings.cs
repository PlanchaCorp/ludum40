using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using bTools.CodeExtensions;

namespace bTools
{
	[InitializeOnLoad]
	public static class Settings
	{
		private static List<ToolsSettingsBase> m_loadedModules;
		public static List<ToolsSettingsBase> LoadedModules
		{
			get
			{
				if ( m_loadedModules == null )
				{
					m_loadedModules = EditorGUIExtensions.FindAssetsByType<ToolsSettingsBase>();

					if ( m_loadedModules == null )
					{
						Debug.LogError( "[bTools]Could not load any settings file! The bSettings window will not work properly until some are generated !" );
					}
				}

				return m_loadedModules;
			}
		}

		static Settings()
		{
		}

		private static T GenerateSettingsFile<T>() where T : ToolsSettingsBase
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var path = AssetDatabase.GenerateUniqueAssetPath( Ressources.PathTo_bSettings + typeof( T ).ToString() + ".asset" );

			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( path, typeof( T ) ) as T;
			m_loadedModules.Add( final );
			return final;
		}

		/// <summary>
		/// Returns the settings file for the provided bToolsSettings derived type.
		/// Generates one if none was found.
		/// </summary>
		public static T Get<T>() where T : ToolsSettingsBase
		{
			var isItLoaded = LoadedModules.Where( m => m is T );

			if ( isItLoaded.Count() > 1 ) MultipleSettingsFileWarning( isItLoaded.First() );

			if ( isItLoaded.Count() > 0 )
			{
				return isItLoaded.First() as T;
			}

			var notLoadedYet = EditorGUIExtensions.FindAssetsByType<T>();
			if ( notLoadedYet.Count() > 0 )
			{
				m_loadedModules.Add( notLoadedYet[0] );
				return notLoadedYet[0];
			}
			else
			{
				return GenerateSettingsFile<T>();
			}
		}

		static void MultipleSettingsFileWarning( ToolsSettingsBase module )
		{
			Debug.Log( "[bTools] Multiple instances of a settings file for " + module.moduleName + " was found. Only the first one will be used" );
		}
	}
}