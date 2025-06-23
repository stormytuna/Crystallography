using Crystallography.Core.Artifacts;
using Terraria.GameContent.ItemDropRules;

namespace Crystallography.Content.Items.Artifacts;

public class SpiderlegCharm : ArtifactItem
{
	public override int GemCount { get => 2; }

	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		if (data.Type == ItemID.Amethyst) {
			return data with { Strength = data.Strength * 1.5f };
		}
		
		return base.ModifyGemData(player, effect, data);
	}
}

public class SpiderlegCharmDrop : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type is NPCID.BlackRecluse or NPCID.BlackRecluseWall;
	}

	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SpiderlegCharm>(), 50));
	}
}
