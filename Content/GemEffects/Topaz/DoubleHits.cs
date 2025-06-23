using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Topaz;

public class DoubleHits : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Topaz;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<DoubleHitsPlayer>().Active = true;
		player.GetModPlayer<DoubleHitsPlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(DoubleHitsPlayer.BaseEffect * strength);
	}
}

public class DoubleHitsPlayer : ModPlayer
{
	public const float BaseEffect = 0.01f;

	private static bool PreventRecursion = false;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active || PreventRecursion) {
			PreventRecursion = false;
			return;
		}

		PreventRecursion = true;
		
		float chance = BaseEffect * Strength;
		if (Main.rand.NextFloat() < chance) {
			Player.StrikeNPCDirect(target, hit);
		}
	}
}
