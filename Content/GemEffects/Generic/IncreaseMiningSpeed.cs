using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseMiningSpeed : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		player.pickSpeed -= GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.1f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
