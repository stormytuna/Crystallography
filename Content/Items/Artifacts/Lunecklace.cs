using Crystallography.Core.Artifacts;
using Terraria.Enums;

namespace Crystallography.Content.Items.Artifacts;

public class Lunecklace : ArtifactItem
{
	public override int GemCount { get => 3; }
	
	public override void SetArtifactDefaults() {
		Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(gold: 5));
	}
	
	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		return data with { Strength = data.Strength * 1.3f };
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.FragmentNebula, 2)
			.AddIngredient(ItemID.FragmentSolar, 2)
			.AddIngredient(ItemID.FragmentStardust, 2)
			.AddIngredient(ItemID.FragmentVortex, 2)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}
