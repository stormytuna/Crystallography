using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Diamond;

public class IncreaseCritChance : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetCritChance(DamageClass.Generic) += GetEffectStrength(player, data.Strength);
	}

	public static float GetEffectStrength(Player player, float strength) {
		return 5f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
