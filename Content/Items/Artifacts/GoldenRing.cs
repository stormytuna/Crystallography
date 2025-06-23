using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items.Artifacts;

public class GoldenRing : ArtifactItem
{
	public override int GemCount { get => 1; }

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.GoldBar, 8)
			.AddTile(TileID.Tables)
			.AddTile(TileID.Chairs)
			.Register();
	}
}
