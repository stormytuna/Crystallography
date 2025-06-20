using System.Collections.Generic;

namespace Crystallography.Core.Artifacts;

public static class GemTypeLoader {
	public static readonly Dictionary<int, List<GemEffect>> MajorEffects = new Dictionary<int, List<GemEffect>>();
	public static readonly Dictionary<int, List<GemEffect>> MinorEffects = new Dictionary<int, List<GemEffect>>();
	public static readonly List<GemEffect> GenericEffects = new List<GemEffect>();
	public static readonly List<int> GemTypes = new List<int>();
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
				effect = Main.rand.NextFromList(MajorEffects[itemType].ToArray());
				break;
			case GemEffect.EffectType.Minor:
				effect = Main.rand.NextFromList(MinorEffects[itemType].ToArray());
				break;
			default:
				// idea: maybe make this return minor effects from a random type??
				effect = Main.rand.NextFromList(GenericEffects.ToArray());
				break;
		}
		return effect;
	}
}
