using Crystallography.Core.Artifacts;
using Terraria.Enums;
using Terraria.GameContent.ItemDropRules;

namespace Crystallography.Content.Items.Artifacts;

public class LostBand : ArtifactItem
{
	public override int GemCount { get => 1; }

	public override void SetArtifactDefaults() {
		Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(gold: 2));
	}
	
	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		if (effect.GemType == ItemID.WhitePearl) {
			return data with { Strength = data.Strength * 1.3f };
		}
		
		return base.ModifyGemData(player, effect, data);
	}
}

public class LostBandDropChance : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type is NPCID.AngryBones or NPCID.AngryBonesBig or NPCID.AngryBonesBigHelmet or NPCID.AngryBonesBigMuscle;
	}

	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LostBand>(), 200));
	}
}
