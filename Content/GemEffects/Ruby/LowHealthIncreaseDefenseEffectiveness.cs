using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class LowHealthIncreaseDefenseEffectiveness : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		int startLife = player.statLifeMax2 / 2;
		float extraEffectiveness = Utils.Remap(player.statLife, startLife, 0, 0f, 0.5f);
		player.DefenseEffectiveness *= (1f + (extraEffectiveness * GetEffectStrength(player, data.Strength)));
	}
	
	public static float GetEffectStrength(Player player, float strength) {
		return 1f * strength;	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}
