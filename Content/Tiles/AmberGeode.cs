using System;
using System.Collections.Generic;
using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Crystallography.Content.Tiles;
public class AmberGeode : ModTile {
	public override void SetStaticDefaults() {
		//Main.tileNoFail[Type] = true;
		Main.tileFrameImportant[Type] = true;
		DustType = DustID.Stone;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Width = 3;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.AnchorBottom = new Terraria.DataStructures.AnchorData(Terraria.Enums.AnchorType.SolidTile, 3, 0);
		TileObjectData.newTile.AnchorValidTiles = [TileID.Sandstone, TileID.Sand, TileID.HardenedSand];
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.newTile.DrawYOffset = 8;
		TileObjectData.addTile(Type);
		HitSound = SoundID.Dig;
		Main.tileLighted[Type] = true;
		TileID.Sets.Ore[Type] = true;
		Main.tileOreFinderPriority[Type] = 551;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 975;
		AddMapEntry(CrystallographyUtils.GetTileMapColor(TileID.Sand), Language.GetText("Mods.Crystallography.Tiles.Geode.MapEntry"));
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
		var color = Color.PaleGoldenrod.ToVector3() * (float)(0.9 + MathF.Sin(Main.GlobalTimeWrappedHourly + i + j) * 0.2 + 0.2)*0.3f;
		r = color.X;
		g = color.Y;
		b = color.Z;
	}
	public override IEnumerable<Item> GetItemDrops(int i, int j) {
		int count = WorldGen.genRand.Next(2, 5);
		Item[] items = new Item[count+1];
		for (int index = 0; index <count; index++) {
			items[index] = new Item(ModContent.ItemType<Gem>());
			if (items[index].ModItem is Gem gem) {
				gem.Data = new(ItemID.Amber,(float)Math.Round((float)WorldGen.genRand.NextFloat(0.2f, 1.5f), 2), [], GemTypeLoader.GetRandomColor(ItemID.Amber));
				gem.SetDefaults();
			}
		}
		items[items.Length - 1] = new Item(ItemID.Amber, WorldGen.genRand.Next(2, 6));
		return items;
	}
}
