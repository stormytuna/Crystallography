using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Sapphire;

public class IncreaseMaxMana : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.statManaMax2 += GetEffectStrength(player, data.Strength);
	}
	
	public static int GetEffectStrength(Player player, float strength) {
		return (int)(20 * strength);	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
