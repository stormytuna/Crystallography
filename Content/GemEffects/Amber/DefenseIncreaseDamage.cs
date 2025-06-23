using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amber;

public class DefenseIncreaseDamage : GemEffect
{
	private const int DefenseNeeded = 5;
	private const int DamageIncrease = 2;

	public static int GetEffectStrength(Player player, float strength) {
		int damageIncrease = (player.statDefense / DefenseNeeded) * DamageIncrease;
		return (int)(damageIncrease * strength);
	}
	
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Generic).Flat += GetEffectStrength(player, data.Strength);
	}

	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs((int)(2 * strength), 5, GetEffectStrength(Main.LocalPlayer, strength));
	}
}
