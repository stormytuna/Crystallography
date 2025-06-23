using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class OnKillAttackSpeed : GemEffect
{	
	public override EffectType Type => EffectType.Major;
 	public override int GemType => ItemID.Ruby;
 	public override void Apply(Player player, GemData data) {
 		player.GetModPlayer<OnKillAttackSpeedPlayer>().Active = true;
 		player.GetModPlayer<OnKillAttackSpeedPlayer>().Strength += data.Strength;
 	}
    
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(OnKillAttackSpeedPlayer.BaseEffect * strength);
	}
}

public class OnKillAttackSpeedPlayer : ModPlayer
{
	private const int AttackSpeedTimerMax = 4 * 60;
	public const float BaseEffect = 0.5f;
	
	public bool Active = false;
	public float Strength = 0f;

	private int _attackSpeedTimer = 0;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		if (!Active || _attackSpeedTimer <= 0) {
			return;
		}

		_attackSpeedTimer--;
		
		Player.GetAttackSpeed(DamageClass.Melee) += BaseEffect * Strength;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active || target.active) {
			return;	
		}
		
		_attackSpeedTimer = AttackSpeedTimerMax;
	}
}
