using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amethyst;

public class UniqueMinionsIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(UniqueMinionsIncreaseDamagePlayer.BaseEffect * strength);
	}
}

public class UniqueMinionsIncreaseDamagePlayer : ModPlayer
{
	public const float BaseEffect = 0.05f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || !modifiers.DamageType.CountsAsClass(DamageClass.Summon)) {
			return;
		}
		
		int numUniqueMinions = Player.GetNumUniqueMinions();
		modifiers.FinalDamage += BaseEffect * Strength * numUniqueMinions;
	}
}
