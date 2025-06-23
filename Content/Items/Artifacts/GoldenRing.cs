using Crystallography.Core.Artifacts;
using Terraria.Enums;

namespace Crystallography.Content.Items.Artifacts;

public class GoldenRing : ArtifactItem
{
	public override int GemCount { get => 1; }
	
	public override void SetArtifactDefaults() {
		Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 50));
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.GoldBar, 8)
			.AddTile(TileID.Tables)
			.AddTile(TileID.Chairs)
			.Register();
	}
}
