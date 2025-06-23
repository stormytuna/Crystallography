using Crystallography.Content.Items;
using Crystallography.Core.UI;
using Terraria.ObjectData;

namespace Crystallography.Content.Tiles;
public class JewelryStationTile : ModTile {
	public override void SetStaticDefaults() {
		Main.tileFrameImportant[Type] = true;
		Main.tileWaterDeath[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.CoordinatePadding = 0;
		TileObjectData.addTile(Type);
	}
	public override void MouseOver(int i, int j) {
		Main.LocalPlayer.cursorItemIconEnabled = true;
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<JewelryStation>();
	}
	public override bool RightClick(int i, int j) {
		UIManagerSystem.ToggleJewelryUI(new Point(i, j));
		//Main.NewText(UIManagerSystem.JewelryStationUI.CurrentState);
		return true;
	}
	public override void AnimateTile(ref int frame, ref int frameCounter) {
		if (++frameCounter >= 7) {
			frameCounter = 0;
			frame = ++frame % 8;
		}
	}
	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) {
		Tile t = Framing.GetTileSafely(i, j);
		frameYOffset = Main.tileFrame[type] * 48;
	}
}
