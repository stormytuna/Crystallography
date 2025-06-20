using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Topaz;

public class IncreaseLuck : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Topaz;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<LuckPlayer>().BonusLuck += 0.15f * data.Strength;
	}
}

public class LuckPlayer : ModPlayer
{
	public float BonusLuck = 0f;
	
	public override void ResetEffects() {
		BonusLuck = 0f;
	}

	public override void ModifyLuck(ref float luck) {
		if (BonusLuck > 0f) 
		{
			luck += BonusLuck;
		}
	}
}
