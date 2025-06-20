using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Diamond;

public class IncreaseCritDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<CritDamagePlayer>().BonusCritDamage += 0.1f * data.Strength;
	}
}

public class CritDamagePlayer : ModPlayer
{
	public float BonusCritDamage = 0f;
	
	public override void ResetEffects() {
		BonusCritDamage = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (BonusCritDamage > 0f) 
		{
			modifiers.CritDamage += BonusCritDamage;
		}
	}
}
