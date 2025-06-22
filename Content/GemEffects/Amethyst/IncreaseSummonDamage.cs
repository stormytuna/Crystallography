using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Amethyst;

public class IncreaseSummonDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Summon) += 0.05f * data.Strength;
	}
}
