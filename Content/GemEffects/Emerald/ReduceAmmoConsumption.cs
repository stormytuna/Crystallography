using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Emerald;

public class ReduceAmmoConsumption : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<DecreasedAmmoConsumptionPlayer>().NoAmmoConsumptionChance += GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.3f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}

public class DecreasedAmmoConsumptionPlayer : ModPlayer
{
	public float NoAmmoConsumptionChance = 0f;

	public override void ResetEffects() {
		NoAmmoConsumptionChance = 0f;
	}

	public override bool CanConsumeAmmo(Item weapon, Item ammo) {
		if (NoAmmoConsumptionChance > 0f) {
			return Main.rand.NextFloat() < NoAmmoConsumptionChance;
		}
		
		return base.CanConsumeAmmo(weapon, ammo);
	}
}
