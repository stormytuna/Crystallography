using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseMaxHealth : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.statLifeMax2 += GetEffectStrength(player, data.Strength);
	}

	public static int GetEffectStrength(Player player, float strength) {
		return (int)(50 * strength);	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
