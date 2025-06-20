using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Amethyst;

public class IncreaseTurretSlots : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.maxTurrets++;
		player.maxMinions--;
	}
}
