using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Amethyst;

public class DecreaseAggro : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.aggro -= (int)(400 * data.Strength);
	}
}
