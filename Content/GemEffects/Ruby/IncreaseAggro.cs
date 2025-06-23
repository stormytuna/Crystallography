using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Ruby;

public class IncreaseAggro : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Ruby;
	public override void Apply(Player player, GemData data) {
		player.aggro += (int)(400 * data.Strength);
	}

	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip;
	}
}
