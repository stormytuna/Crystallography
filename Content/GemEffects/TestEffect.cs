using Crystallography.Core.Artifacts;

namespace Crystallography.Content;
public class TestEffect : GemEffect {
	public override int GemType => ItemID.Sapphire;
	public override EffectType Type => EffectType.Generic;
	public override void Apply(Player player) {
		throw new System.NotImplementedException();
	}
}
public class TestEffect2 : GemEffect
{
	public override int GemType => ItemID.Ruby;
	public override EffectType Type => EffectType.Generic;
	public override void Apply(Player player) {
		throw new System.NotImplementedException();
	}
}
public class TestEffect3 : GemEffect
{
	public override int GemType => ItemID.Ruby;
	public override EffectType Type => EffectType.Major;
	public override void Apply(Player player) {
		throw new System.NotImplementedException();
	}
}
