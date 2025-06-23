using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Amethyst;

public class UniqueMinionsIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Strength += data.Strength;
	}
}

public class UniqueMinionsIncreaseDamagePlayer : ModPlayer
{
	private const float BaseEffect = 0.05f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || !modifiers.DamageType.CountsAsClass(DamageClass.Summon)) {
			return;
		}
		
		int numUniqueMinions = Player.GetNumUniqueMinions();
		modifiers.FinalDamage += BaseEffect * Strength * numUniqueMinions;
	}
}
