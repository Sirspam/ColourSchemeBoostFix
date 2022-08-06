using System.Reflection;
using ColourSchemeBoostFix.Configuration;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPALogger = IPA.Logging.Logger;

namespace ColourSchemeBoostFix
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		private static readonly Harmony Harmony = new Harmony("com.Sirspam.BeatSaber.ColourSchemeBoostFix");

		[Init]
		public void Init(Config config) => PluginConfig.Instance = config.Generated<PluginConfig>();

		[OnEnable]
		public void OnEnable() => Harmony.PatchAll(Assembly.GetExecutingAssembly());

		[OnDisable]
		public void OnDisable() => Harmony.UnpatchSelf();
	}
}