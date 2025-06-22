using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Emerald;

public class IncreaseArmorPenetration : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetArmorPenetration(DamageClass.Generic) += 10f * data.Strength;
	}
}
