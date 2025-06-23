using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseDamageResistance : GemEffect
{
	private const float BaseEffect = 0.05f;
	
	public static float GetEffectStrength(Player player, float strength) {
		return BaseEffect * strength;
	}
	
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.endurance += GetEffectStrength(player, data.Strength);
	}

	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
