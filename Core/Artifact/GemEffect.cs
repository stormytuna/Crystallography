using System.Collections.Generic;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Core.Artifacts;
// this sucks. please learn EC 
public abstract class GemEffect : ModType{
	public enum EffectType : byte {
		Major = 0 << 1,
		Minor = 1 << 1,
		Generic = 2 << 1
	}

	public static Dictionary<int, Color> GemItemToColor = new() {
		{ ItemID.Sapphire, Color.Blue with { A = 180 } },
		{ ItemID.Ruby, Color.Red with { A = 180 } },
		{ ItemID.Emerald, Color.Green with { A = 180 } },
		{ ItemID.Amethyst, Color.Purple with { A = 180 } },
		{ ItemID.Topaz, Color.Orange with { A = 180 } },
		{ ItemID.Amber, Color.Yellow with { A = 180 } },
		{ ItemID.Diamond, Color.White with { A = 180 } },
	};

	public Color Color {
		get {
			if (GemItemToColor.TryGetValue(GemType, out var color)) {
				return color;
			}

			return Color.LightGray;
		}
	}
	
	public LocalizedText Tooltip => Mod.GetLocalization("GemEffects." + Name);
	public abstract GemEffect.EffectType Type { get; }
	public abstract int GemType { get; }
	protected override void Register() {
		ModTypeLookup<GemEffect>.Register(this);
		switch (Type) {
			case EffectType.Major:
				if (!GemTypeLoader.MajorEffects.ContainsKey(GemType))
					GemTypeLoader.MajorEffects[GemType] = new List<GemEffect>();
				GemTypeLoader.MajorEffects[GemType].Add(this);
				break;
			case EffectType.Minor:
				if (!GemTypeLoader.MinorEffects.ContainsKey(GemType))
					GemTypeLoader.MinorEffects[GemType] = new List<GemEffect>();
				GemTypeLoader.MinorEffects[GemType].Add(this);
				break;
			case EffectType.Generic:
				GemTypeLoader.GenericEffects.Add(this);
				break;
		}
		GemTypeLoader.GemEffectLookup[this.Name] = this;
	}
	public abstract void Apply(Player player, GemData data);
	public abstract LocalizedText GetFormattedTooltip(float strength);
}
