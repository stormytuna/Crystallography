using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Sapphire;

public class IncreaseMagicDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Magic) += 0.05f * data.Strength;
	}
}
