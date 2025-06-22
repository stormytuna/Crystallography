using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Diamond;

public class SpeedBoostOnCrit : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<SpeedBoostOnCritPlayer>().Active = true;
		player.GetModPlayer<SpeedBoostOnCritPlayer>().Strength += data.Strength;
	}
}

public class SpeedBoostOnCritPlayer : ModPlayer
{
	private const int SpeedBoostTimeMax = 4 * 60;
	private const float BaseEffect = 0.5f;
	
	public bool Active = false;
	public float Strength = 0f;

	private int _speedBoostTimer = 0;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		if (!Active || _speedBoostTimer <= 0) {
			return;
		}
		
		_speedBoostTimer--;
		Player.moveSpeed += BaseEffect * Strength;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active || !hit.Crit) {
			return;
		}
		
		_speedBoostTimer = SpeedBoostTimeMax;
	}
}
