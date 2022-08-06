using ColourSchemeBoostFix.Configuration;
using HarmonyLib;

namespace ColourSchemeBoostFix.HarmonyPatches
{ 
	[HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.LoadFromCurrentVersion))] 
	internal class LoadPatch
	{
		public static bool Loaded;
		
		private static void Postfix(ref PlayerData __result)
		{
			if (PluginConfig.Instance.BoostColoursList.Count == 0)
			{
				Loaded = true;
				return;
			}
			
			for (var i = 0; i < __result.colorSchemesSettings.GetNumberOfColorSchemes() || i <= PluginConfig.Instance.BoostColoursList.Count; i++)
			{
				var colourScheme = __result.colorSchemesSettings.GetColorSchemeForIdx(i);
				if (colourScheme.isEditable)
				{
					__result.colorSchemesSettings.SetColorSchemeForId(new ColorScheme(colourScheme.colorSchemeId,
						colourScheme.colorSchemeNameLocalizationKey, colourScheme.useNonLocalizedName,
						colourScheme.nonLocalizedName, colourScheme.isEditable, colourScheme.saberAColor,
						colourScheme.saberBColor, colourScheme.environmentColor0, colourScheme.environmentColor1, true,
						PluginConfig.Instance.BoostColoursList[i].BoostColour0,
						PluginConfig.Instance.BoostColoursList[i].BoostColour1, colourScheme.obstaclesColor));
				}
			}

			Loaded = true;
		}
	}
	
	[HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.Save))]
	internal class SavePatch
	{
		private static void Postfix(PlayerData playerData)
		{
			// Why is Save invoked before Load? :clueless:
			if (!LoadPatch.Loaded)
			{
				return;
			}
			
			PluginConfig.Instance.BoostColoursList.Clear();
			for (var i = 0; i < playerData.colorSchemesSettings.GetNumberOfColorSchemes(); i++)
			{
				var colourScheme = playerData.colorSchemesSettings.GetColorSchemeForIdx(i);
				if (colourScheme.isEditable)
				{
					PluginConfig.Instance.BoostColoursList.Add(new BoostColours
					{
						BoostColour0 = colourScheme.environmentColor0Boost,
						BoostColour1 = colourScheme.environmentColor1Boost
					});
				}
			}
		}
	}
}