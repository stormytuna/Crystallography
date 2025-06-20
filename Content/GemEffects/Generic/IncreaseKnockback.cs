using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseKnockback : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		player.GetKnockback(DamageClass.Generic) += 2f * data.Strength;
	}
}
