using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Topaz;

public class IncreaseLuck : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Topaz;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<LuckPlayer>().BonusLuck += GetEffectStrength(player, data.Strength);
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 0.15f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
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
