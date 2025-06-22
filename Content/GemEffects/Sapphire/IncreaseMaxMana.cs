using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Sapphire;

public class IncreaseMaxMana : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.statManaMax2 += (int)(20 * data.Strength);
	}
}
