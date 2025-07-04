using Crystallography.Content.GemEffects.Emerald;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class NearbyIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<NearbyIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<NearbyIncreaseDamagePlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(NearbyIncreaseDamagePlayer.BaseEffect * strength);
	}
}

public class NearbyIncreaseDamagePlayer : ModPlayer
{
	private const float MaxRange = 12f * 16f;
	public const float BaseEffect = 0.2f;
	public bool Active;
	public float Strength;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || !modifiers.DamageType.CountsAsClass(DamageClass.Melee) || !Player.WithinRange(target.Center, MaxRange)) {
			return;
		}

		modifiers.FinalDamage += BaseEffect * Strength;
	}
}
