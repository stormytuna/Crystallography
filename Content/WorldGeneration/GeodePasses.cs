using Crystallography.Content.Tiles;
using Humanizer;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Crystallography.Content.WorldGeneration;
public class StandardGeodePass(float weight) : GenPass("Crystallography:StandardGeodes", weight) {
	private static int GetGeodeCount() {
		int size = WorldGen.GetWorldSize();
		switch (size) {
			case WorldGen.WorldSize.Large:
				return 450;
			case WorldGen.WorldSize.Medium:
				return 300;
			case WorldGen.WorldSize.Small:
				return 160;
			default:
				return 300;
		}
	}
	// Woah! Vanilla Accurate Code!!! :DDDDDDDD
	protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
		progress.Message = PassesSystem.GenMessage;
		int geodeCount = GetGeodeCount();
		if (WorldGen.tenthAnniversaryWorldGen)
			geodeCount = (int)(geodeCount*1.2f);
		for (int i =0; i < geodeCount; i++) {
			double completion = (double)i / (double)(Main.maxTilesX * Main.maxTilesY * geodeCount);
			progress.Set(completion);
			int attempts = 0;
			bool placed = false;
			while (!placed) {
				int x = WorldGen.genRand.Next(Main.offLimitBorderTiles, Main.maxTilesX - Main.offLimitBorderTiles);
				int y = WorldGen.genRand.Next((int)(Main.worldSurface * 2.0 + Main.rockLayer) / 3, Main.maxTilesY - 300);
				if (WorldGen.remixWorldGen) {
					y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 400);
				}
				bool isDesert = x > GenVars.UndergroundDesertLocation.X && x < GenVars.UndergroundDesertLocation.X+GenVars.UndergroundDesertLocation.Width;
				int type = isDesert ? ModContent.TileType<AmberGeode>() : ModContent.TileType<StandardGeodes>();
				int style = isDesert ? 0 : WorldGen.genRand.Next(0, 6);
				if (WorldGen.PlaceObject(x,y, type, mute:true, style: style)) {
					placed = true;
				}
				else {
					attempts++;
					if (attempts >= 5000) {
						placed = true; // give up if you really cant find a spot after 5000 attempts.
					}
				}
			}
		}
	}
}
