using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace ColourSchemeBoostFix.Configuration
{
	internal class PluginConfig
	{
		public static PluginConfig Instance { get; set; }
		[NonNullable]
		[UseConverter(typeof(ListConverter<BoostColours>))]
		public virtual List<BoostColours> BoostColoursList { get; set; } = new List<BoostColours>();
	}

	internal class BoostColours
	{
		internal Color BoostColour0;

		internal Color BoostColour1;
	}
}