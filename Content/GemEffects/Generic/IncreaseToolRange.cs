using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Generic;

public class IncreaseToolRange : GemEffect
{
	public override EffectType Type => EffectType.Generic;
	public override int GemType => ItemID.WhitePearl;
	public override void Apply(Player player, GemData data) {
		Player.tileRangeX++;
		Player.tileRangeY++;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip;
	}
}
