using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseMeleeDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Melee) += 0.05f * data.Strength;
	}
}
