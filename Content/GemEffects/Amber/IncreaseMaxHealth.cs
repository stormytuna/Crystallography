using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Amber;

public class IncreaseMaxHealth : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.statLifeMax2 += (int)(50 * data.Strength);
	}
}
