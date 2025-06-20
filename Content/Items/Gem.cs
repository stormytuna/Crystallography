using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items;
public class Gem : ModItem {
	public readonly GemData Data;
	public override void SetDefaults() {
		var item = ContentSamples.ItemsByType[Data.Type];
		Item.SetNameOverride(item.Name);
	}
	// this will also need a draw layer, unless the items never have a usestyle, then not :steamhappy:
	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
		return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
	}
	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
	}
}
