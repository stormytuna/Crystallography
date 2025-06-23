using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseDefense : GemEffect
{
	public static int GetEffectStrength(Player player, float strength) {
		return (int)(8 * strength);
	}
	
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.statDefense += GetEffectStrength(player, data.Strength);
	}

	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
