using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Diamond;

public class IncreaseCritDamageDecreaseNonCritDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<IncreaseCritDamageDecreaseNonCritDamagePlayer>().Active = true;
		player.GetModPlayer<IncreaseCritDamageDecreaseNonCritDamagePlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(IncreaseCritDamageDecreaseNonCritDamagePlayer.ExtraDamageOnCrit * strength, IncreaseCritDamageDecreaseNonCritDamagePlayer.LostDamageOnNonCrit * strength);
	}
}

public class IncreaseCritDamageDecreaseNonCritDamagePlayer : ModPlayer
{
	public const float ExtraDamageOnCrit = 0.25f;
	public const float LostDamageOnNonCrit = 0.2f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active) {
			return;
		}
		
		modifiers.CritDamage += ExtraDamageOnCrit * Strength;
		modifiers.NonCritDamage -= LostDamageOnNonCrit * Strength;
	}
}
