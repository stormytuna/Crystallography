using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Amber;

public class DefenseIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		int damageIncrease = player.statDefense / 5;
		player.GetDamage(DamageClass.Generic).Flat += damageIncrease * data.Strength;
	}
}
