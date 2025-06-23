using Crystallography.Core.Artifacts;
using Terraria.Enums;

namespace Crystallography.Content.Items.Artifacts;

public class UnholyRing : ArtifactItem
{
	public override int GemCount { get => 1; }
	
	public override void SetArtifactDefaults() {
		Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(gold: 1));
	}

	public override GemData ModifyGemData(Player player, GemEffect effect, GemData data) {
		if (effect.Type == GemEffect.EffectType.Major) {
			return data with { Strength = data.Strength * 1.2f };
		}
		
		return base.ModifyGemData(player, effect, data);
	}

	public override void AddRecipes() {
		CreateRecipe()
			.AddIngredient(ItemID.ShadowScale, 3)
			.AddIngredient(ItemID.DemoniteBar, 5)
			.AddTile(TileID.Anvils)
			.Register();
		
		CreateRecipe()
			.AddIngredient(ItemID.TissueSample, 3)
			.AddIngredient(ItemID.CrimtaneBar, 5)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
