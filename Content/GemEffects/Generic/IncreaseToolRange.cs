using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseToolRange : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		Player.tileRangeX++;
		Player.tileRangeY++;
	}
}
