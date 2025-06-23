using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseKnockbackResistance : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<KnockbackResistancePlayer>().KnockbackResistance += GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.3f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}

public class KnockbackResistancePlayer : ModPlayer
{
	public float KnockbackResistance = 1f;

	public override void ResetEffects() {
		KnockbackResistance = 1f;
	}

	public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers) {
		modifiers.Knockback *= 1f / KnockbackResistance;
	}

	public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers) {
		modifiers.Knockback *= 1f / KnockbackResistance;
	}
}
