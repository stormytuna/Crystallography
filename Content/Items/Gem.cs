using System.Collections.Generic;
using Crystallography.Core;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;

namespace Crystallography.Content.Items;
public class Gem : ModItem {
	public GemData Data;
	public override string Texture => Assets.Textures.Empty;
	public override void SetDefaults() {
		Data = new GemData(ItemID.Sapphire, 2, [], Color.White);
		var item = ContentSamples.ItemsByType[Data.Type];
		Item.width = item.width;
		Item.height = item.height;
		Item.SetNameOverride(item.Name);
	}
	// this will also need a draw layer, unless the items never have a usestyle, then not :steamhappy:
	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
		CrystallographyUtils.DrawItemFromType(Data.Type, null, position, drawColor.MultiplyRGB(Data.Color), 0, origin, scale, SpriteEffects.None);
		return false;
	}
	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		CrystallographyUtils.DrawItemFromType(Data.Type, null, Item.Center-Main.screenPosition, alphaColor.MultiplyRGB(Data.Color), rotation, Vector2.Zero, scale, SpriteEffects.None);
		return false;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips) {
		base.ModifyTooltips(tooltips); // add gem effect listing here idk
	}
}
