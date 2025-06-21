using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Sapphire;

public class LowManaIncreaseDamage : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<LowManaIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<LowManaIncreaseDamagePlayer>().Strength = data.Strength;
	}
}

public class LowManaIncreaseDamagePlayer : ModPlayer
{
	private const float ManaPercentForMinEffect = 0.4f;
	private const float ManaPercentForMaxEffect = 0.1f;
	private const float MinEffect = 0f;
	private const float MaxEffect = 0.2f;

	public bool Active;
	public float Strength;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void ModifyWeaponDamage(Item item, ref StatModifier damage) {
		if (!Active) {
			return;
		}

		float manaPercent = Main.LocalPlayer.statMana / (float)Main.LocalPlayer.statManaMax2;
		float extraDamage = Utils.Remap(manaPercent, ManaPercentForMinEffect, ManaPercentForMaxEffect, MinEffect, MaxEffect);
		damage += extraDamage * Strength;
	}
}
