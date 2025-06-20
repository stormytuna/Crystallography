using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseAggro : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.aggro += (int)(400 * data.Strength);
	}
}
