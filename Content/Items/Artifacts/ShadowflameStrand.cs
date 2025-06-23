using Crystallography.Core.Artifacts;
using Terraria.Enums;
using Terraria.GameContent.ItemDropRules;

namespace Crystallography.Content.Items.Artifacts;

public class ShadowflameStrand : ArtifactItem
{
	public override int GemCount { get => 2; }
	
	public override void SetArtifactDefaults() {
		Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(gold: 3));
	}
	
	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		if (data.Type == ItemID.Sapphire) {
			return data with { Strength = data.Strength * 1.3f };
		}
		
		return base.ModifyGemData(player, effect, data);
	}
}

public class ShadowflameStrandDrop : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type is NPCID.GoblinSorcerer;
	}

	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<ShadowflameStrand>(), 80));
	}
}
