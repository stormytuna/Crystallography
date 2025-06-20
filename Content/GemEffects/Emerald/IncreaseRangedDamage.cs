using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Emerald;

public class IncreaseRangedDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Ranged) += 0.05f * data.Strength;
	}
}
