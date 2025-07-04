using System.Collections.Generic;

namespace Crystallography.Core.Artifacts;

public static class GemTypeLoader {
	public static readonly Dictionary<int, List<GemEffect>> MajorEffects = new Dictionary<int, List<GemEffect>>();
	public static readonly Dictionary<int, List<GemEffect>> MinorEffects = new Dictionary<int, List<GemEffect>>();
	public static readonly List<GemEffect> GenericEffects = new List<GemEffect>();
	public static readonly List<int> GemTypes = new List<int>();
	public static readonly Dictionary<int, Color[]> GemTypeToColorPool = new Dictionary<int, Color[]>();
	static GemTypeLoader() {
		GemTypeToColorPool[ItemID.Amber] = [Color.LightGoldenrodYellow, Color.PaleGoldenrod, Color.LightYellow, Color.White];
		GemTypeToColorPool[ItemID.Topaz] = [Color.LightGoldenrodYellow, Color.PaleGoldenrod, Color.Goldenrod, Color.LightYellow, Color.White];
		GemTypeToColorPool[ItemID.Ruby] = [Color.IndianRed, Color.OrangeRed, Color.Pink, Color.HotPink, Color.White];
		GemTypeToColorPool[ItemID.Emerald] = [Color.Lime, Color.LimeGreen, Color.LightSeaGreen, Color.MediumSpringGreen, Color.MintCream, Color.White];
		GemTypeToColorPool[ItemID.Sapphire] = [Color.AliceBlue, Color.Cyan, Color.LightSkyBlue, Color.LightCyan, Color.BlueViolet, Color.White];
		GemTypeToColorPool[ItemID.Amethyst] = [Color.Purple, Color.Violet, Color.White, Color.LightPink, Color.MediumPurple, Color.PaleVioletRed];
		GemTypeToColorPool[ItemID.Diamond] = [Color.LightSkyBlue, Color.LightPink, Color.White, Color.WhiteSmoke, Color.NavajoWhite];
	}
	/// <summary>
	///		Returns a <see cref="GemEffect"/> instance based on its class name.
	/// </summary>
	public static readonly Dictionary<string, GemEffect> GemEffectLookup = new Dictionary<string, GemEffect>();
	/// <summary>
	///		Returns a random effect based on the <paramref name="itemType"/> and <paramref name="effectType"/>
	///		
	/// </summary>
	/// <param name="itemType"></param>
	/// <param name="effectType"></param>
	/// <returns></returns>
	public static GemEffect GetRandomEffect(int itemType, GemEffect.EffectType effectType) 
	{
		GemEffect effect;
		switch (effectType) {
			case GemEffect.EffectType.Major:
				if (MajorEffects.TryGetValue(itemType, out var majors)) {
					effect = Main.rand.NextFromCollection(majors);
				}
				else {
					effect = Main.rand.NextFromCollection(GenericEffects);
				}
				break;
			case GemEffect.EffectType.Minor:
				if (MinorEffects.TryGetValue(itemType, out var minors)) {
					effect = Main.rand.NextFromCollection(minors);
				}
				else {
					effect = Main.rand.NextFromCollection(GenericEffects);
				}
				break;
			default:
				// idea: maybe make this return minor effects from a random type??
				effect = Main.rand.NextFromList(GenericEffects.ToArray());
				break;
		}
		return effect;
	}
	public static Color GetRandomColor(int itemType) {
		if (GemTypeToColorPool.TryGetValue(itemType, out Color[] colors)) {
			return Main.rand.NextFromList(colors);
		}
		return Color.White;
	}
}
