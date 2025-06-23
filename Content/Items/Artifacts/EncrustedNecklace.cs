using Crystallography.Core.Artifacts;
using Terraria.DataStructures;

namespace Crystallography.Content.Items.Artifacts;

public class EncrustedNecklace : ArtifactItem
{
	public override int GemCount { get => 2; }
	
	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		if (data.Type == ItemID.Emerald) {
			return data with { Strength = data.Strength * 1.3f };
		}
		
		return base.ModifyGemData(player, effect, data);
	}
}

public class EncrustedNecklaceFish : ModPlayer
{
	public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition) {
		bool inWater = !attempt.inLava && !attempt.inHoney;
		if (Player.ZoneBeach && inWater && attempt is { veryrare: true, legendary: false } && Main.rand.NextBool(3)) {
			itemDrop = ModContent.ItemType<EncrustedNecklace>();
		}
	}
}
