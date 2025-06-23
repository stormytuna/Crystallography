using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using System.Collections.Generic;
using System;
using Terraria.ObjectData;
using Terraria.Map;
using System.Reflection;
using Terraria.Localization;
using Crystallography.Core.Utilities;

namespace Crystallography.Content.Tiles;

public class StandardGeodes : ModTile {
	internal static readonly int[] StyleToGemID = [ItemID.Emerald, ItemID.Amethyst, ItemID.Ruby, ItemID.Sapphire, ItemID.Topaz, ItemID.Diamond];
	internal static readonly Color[] StyleToGlowColor = [Color.LimeGreen, Color.PaleVioletRed, Color.OrangeRed, Color.Cyan, Color.Goldenrod, Color.LightSkyBlue];
	public override void SetStaticDefaults() {
		//Main.tileNoFail[Type] = true;
		Main.tileFrameImportant[Type] = true;
		DustType = DustID.Stone;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.Width = 3;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.AnchorBottom = new Terraria.DataStructures.AnchorData(Terraria.Enums.AnchorType.SolidTile, 3, 0);
		TileObjectData.newTile.AnchorValidTiles = [TileID.Stone, TileID.Dirt, TileID.Marble, TileID.Granite, TileID.JungleGrass, TileID.Mud, TileID.MushroomGrass];
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.newTile.DrawYOffset = 8;
		TileObjectData.addTile(Type);
		HitSound = SoundID.Dig;
		Main.tileLighted[Type] = true;
		TileID.Sets.Ore[Type] = true;
		Main.tileOreFinderPriority[Type] = 551;
		Main.tileShine2[Type] = true;
		Main.tileShine[Type] = 975;
		MinPick = 55; // Gold
		AddMapEntry(CrystallographyUtils.GetTileMapColor(TileID.Stone), Language.GetText("Mods.Crystallography.Tiles.Geode.MapEntry"));

	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
		int style = Main.tile[i, j].TileFrameY / 48;
		var color = StyleToGlowColor[style].ToVector3() * (float)(0.9+MathF.Sin(Main.GlobalTimeWrappedHourly+i+j)*0.2+0.2) *0.3f;
		r = color.X;
		g = color.Y;
		b = color.Z;
	}
	public override IEnumerable<Item> GetItemDrops(int i, int j) {
		int count = WorldGen.genRand.Next(2, 5);
		int type = StyleToGemID[Main.tile[i,j].TileFrameY/48];
		Item[] items = new Item[count+1];
		for (int index = 0; index < count; index++) {
			items[index] = new Item(ModContent.ItemType<Gem>());
			if (items[index].ModItem is Gem gem) {
				gem.Data = new(type, (float)Math.Round((float)WorldGen.genRand.NextFloat(0.2f, 1.5f), 2), [], GemTypeLoader.GetRandomColor(type));
				gem.SetDefaults();
			}
		}
		items[items.Length - 1] = new Item(type, WorldGen.genRand.Next(2, 6));
		return items;
	}
}
