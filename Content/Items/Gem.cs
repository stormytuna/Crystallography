using System.Collections.Generic;
using Crystallography.Core;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using Terraria.ModLoader.IO;

namespace Crystallography.Content.Items;
public class Gem : ModItem {
	public GemData Data;
	public override string Texture => Assets.Textures.Empty;
	public override void SetDefaults() {
		Data = new GemData(ItemID.Sapphire, 2, [GemTypeLoader.GemEffectLookup["TestEffect"]], Color.White);
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
	public override void SaveData(TagCompound tag) {
		tag["gemID"] = Data.Type;
		tag["gemStrength"] = Data.Strength;
		tag["gemColorOverride"] = Data.Color;
		tag["gemEffectCount"] = Data.Effects.Length;
		for (int i = 0; i < Data.Effects.Length; i++) {
			tag[$"gemEffect{i}"] = Data.Effects[i].Name;
		}
	}
	public override void LoadData(TagCompound tag) {
		int id = tag.Get<int>("gemID");
		float strength = tag.Get<float>("gemStrength");
		Color gemColor = tag.Get<Color>("gemColorOverride");
		GemEffect[] effects = new GemEffect[tag.Get<int>("gemEffectCount")];
		for (int i = 0; i < effects.Length; i++) {
			effects[i] = GemTypeLoader.GemEffectLookup[(string)tag[$"gemEffect{i}"]];
		}
		Data = new GemData(id, strength, effects, gemColor);
	}
}
