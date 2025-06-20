using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseDamageResistance : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.endurance += 0.05f * data.Strength;
	}
}
