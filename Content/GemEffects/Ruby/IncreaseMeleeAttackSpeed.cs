using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseMeleeAttackSpeed : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetAttackSpeed(DamageClass.Melee) += GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.05f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
