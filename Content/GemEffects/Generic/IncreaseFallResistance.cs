using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseFallResistance : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		player.extraFall += (int)(20 * data.Strength);
	}
}
