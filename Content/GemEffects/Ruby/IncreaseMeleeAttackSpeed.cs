using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseMeleeAttackSpeed : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetAttackSpeed(DamageClass.Melee) += 0.05f * data.Strength;
	}
}
