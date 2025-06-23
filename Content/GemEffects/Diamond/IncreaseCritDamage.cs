using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Diamond;

public class IncreaseCritDamage : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<CritDamagePlayer>().BonusCritDamage += GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.1f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
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
