using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amethyst;

public class IncreaseMinionSlots : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.maxMinions++;
		player.maxTurrets--;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip;
	}
}
