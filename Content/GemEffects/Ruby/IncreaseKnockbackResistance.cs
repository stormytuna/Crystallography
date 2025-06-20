using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseKnockbackResistance : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<KnockbackResistancePlayer>().KnockbackResistance += 0.3f * data.Strength;
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
