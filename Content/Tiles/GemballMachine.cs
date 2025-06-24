using Crystallography.Core.UI;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ObjectData;

namespace Crystallography.Content.Tiles;

public class GemballMachineItem : ModItem
{
	public override void SetDefaults() {
		Item.DefaultToPlaceableTile(ModContent.TileType<GemballMachine>());
		Item.width = 28;
		Item.height = 34;
		Item.value = Item.sellPrice(gold: 15);
		Item.rare = ItemRarityID.Blue;
	}
}

public class GemballMachineSell : GlobalNPC
{
	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type is NPCID.PartyGirl;
	}

	public override void ModifyShop(NPCShop shop) {
		shop.InsertAfter(ItemID.BubbleMachine, ModContent.ItemType<GemballMachineItem>());
	}
}

public class GemballMachine : ModTile
{
	public override void SetStaticDefaults() {
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.InteractibleByNPCs[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
		TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
		TileObjectData.addTile(Type);
		
		AddMapEntry(new Color(133, 13, 27));
	}
	

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
		return true;
	}

	public override void MouseOver(int i, int j) {
		var player = Main.LocalPlayer;
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		player.cursorItemIconText = $"[i:{ItemID.Star}] [i:{ItemID.Star}]";
	}

	public override bool RightClick(int i, int j) {
		// TODO
		UIManagerSystem.ToggleGemballUI(new Point(i, j));
		return true;
	}
}
