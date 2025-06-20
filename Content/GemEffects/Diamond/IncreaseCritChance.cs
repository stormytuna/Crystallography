using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Diamond;

public class IncreaseCritChance : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetCritChance(DamageClass.Generic) += 5f * data.Strength;
	}
}
