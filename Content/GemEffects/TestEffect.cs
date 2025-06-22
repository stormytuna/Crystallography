using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content;
/// <summary>
/// Example of a Gem Effect.
/// </summary>
public class TestEffect : GemEffect {
	public override int GemType => ItemID.Sapphire;
	public override EffectType Type => EffectType.Generic;
	public override void Apply(Player player, GemData data) {
		player.GetDamage(DamageClass.Melee) += 0.05f;
	}
}
