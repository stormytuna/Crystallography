using Crystallography.Content.Tiles;
namespace Crystallography.Content.Items;
public class JewelryStation : ModItem {
	public override void SetDefaults() {
		Item.DefaultToPlaceableTile(ModContent.TileType<JewelryStationTile>());
	}
	public override void AddRecipes() {
		var recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Ruby, 2);
		recipe.AddIngredient(ItemID.Sapphire, 2);
		recipe.AddIngredient(ItemID.Emerald, 2);
		recipe.AddIngredient(ItemID.Diamond, 2);
		recipe.AddIngredient(ItemID.Topaz, 2);
		recipe.AddIngredient(ItemID.Amethyst, 2);
		recipe.AddIngredient(ItemID.Amber, 2);
		recipe.AddTile(TileID.Anvils);
		recipe.Register();
	}
}
