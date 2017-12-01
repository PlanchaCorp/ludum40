using System;
using UnityEngine;

namespace bTools
{
	[Serializable]
	public abstract class ToolsSettingsBase : ScriptableObject
	{
		// The name of the module this settings wrapper describes. Used by the settings window as tab name.
		[SerializeField] public string moduleName = "Unconfigured";
		// List of methods that draw subgroups of the settings. The settings window uses the name of the method as the tab name.
		[SerializeField] public Action[] subCategories;

		// Event callbacks.
		public delegate void SettingsChangedCallback();
		public SettingsChangedCallback OnSettingsChanged;

		public ToolsSettingsBase()
		{
		}
	}
}