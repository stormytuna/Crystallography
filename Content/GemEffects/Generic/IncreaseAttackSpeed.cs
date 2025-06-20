using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseAttackSpeed : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		player.GetAttackSpeed(DamageClass.Generic) += 0.05f * data.Strength;
	}
}
