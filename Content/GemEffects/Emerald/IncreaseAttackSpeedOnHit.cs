using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Emerald;

public class IncreaseAttackSpeedOnHit : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<IncreaseAttackSpeedOnHitPlayer>().Active = true;
		player.GetModPlayer<IncreaseAttackSpeedOnHitPlayer>().Strength += data.Strength;
	}
}

public class IncreaseAttackSpeedOnHitPlayer : ModPlayer
{
	private const float MaxAttackSpeedIncrease = 0.5f;
	private const int MaxAttackSpeedTimer = 3 * 60;
	
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
		
		Player.GetAttackSpeed(DamageClass.Ranged) += MaxAttackSpeedIncrease * Strength;
	}

	public override void OnHurt(Player.HurtInfo info) {
		if (!Active) {
			return;
		}

		_attackSpeedTimer = MaxAttackSpeedTimer;
	}
}
