using Crystallography.Content.GemEffects.Diamond;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Emerald;

public class FarTargetsIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<FarTargetsIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<FarTargetsIncreaseDamagePlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(FarTargetsIncreaseDamagePlayer.MaxDamageIncrease * strength);
	}
}

public class FarTargetsIncreaseDamagePlayer : ModPlayer
{
	private const float MinDistanceToIncreaseDamage = 25f * 16f;
	private const float MaxDistanceToIncreaseDamage = 32f * 16f;
	private const float MinDistanceToDropOffDamage = 43f * 16f;
	private const float MaxDistanceToDropOffDamage = 50f * 16f;
	private const float MinDamageIncrease = 0f;
	public const float MaxDamageIncrease = 0.2f;
	private const float MinDamageDropOff = 0f;
	private const float MaxDamageDropOff = -0.2f;
	
	public bool Active;
	public float Strength;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || !modifiers.DamageType.CountsAsClass(DamageClass.Ranged)) {
			return;
		}
		
		float distance = Player.Distance(target.Center);
		float damageIncrease = distance switch {
			> MinDistanceToIncreaseDamage and < MinDistanceToDropOffDamage => Utils.Remap(distance, MinDistanceToIncreaseDamage, MaxDistanceToIncreaseDamage, MinDamageIncrease, MaxDamageIncrease),
			> MinDistanceToDropOffDamage => Utils.Remap(distance, MinDistanceToDropOffDamage, MaxDistanceToDropOffDamage, MinDamageDropOff, MaxDamageDropOff),
			_ => 0f,
		};
		
		modifiers.FinalDamage += damageIncrease * Strength;
	}
}
