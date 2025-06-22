using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseDefense : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.statDefense += (int)(8 * data.Strength);
	}
}
