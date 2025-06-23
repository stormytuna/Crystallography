using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amethyst;

public class WhipSweetSpot : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<WhipSweetSpotPlayer>().Active = true;
		player.GetModPlayer<WhipSweetSpotPlayer>().Strength += data.Strength;
	}

	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(WhipSweetSpotPlayer.BaseEffect * strength);
	}
}

public class WhipSweetSpotPlayer : ModPlayer
{
	public const float BaseEffect = 0.2f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || !ProjectileID.Sets.IsAWhip[proj.type]) {
			return;
		}
		
		// Whips at 1f RangeMultiplier are around 16 tiles wide, so we check if within 2 tiles of 15 tiles
		float distance = Player.Distance(target.Center);
		float sweetSpotMiddle = (15f * 16f) * proj.WhipSettings.RangeMultiplier;
		if (float.Abs(distance - sweetSpotMiddle) > 16f) {
			return;
		}
		
		modifiers.FinalDamage += BaseEffect * Strength;
	}
}
