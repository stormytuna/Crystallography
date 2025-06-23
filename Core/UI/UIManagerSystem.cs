using System;
using System.Collections.Generic;
using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Terraria.UI;

namespace Crystallography.Core.UI;

public class UIManagerSystem : ModSystem {
	public static UserInterface GemSlotsMenu { get; private set; }
	internal static ArtifactUI ArtifactInterface;
	public static UserInterface JewelryStationUI { get; private set; }
	public override void Load() {
		if (!Main.dedServ) {
			ArtifactInterface = new();
			GemSlotsMenu = new();
			JewelryStationUI = new();
			//GemSlotsMenu.SetState(ArtifactInterface);
		}
	}
	/// <summary>
	///		Pass <see langword="null"/> as <paramref name="item"/> to disable the UI.
	/// </summary>
	/// <param name="item"></param>
	internal static void ToggleArtifactUI(ArtifactItem item) {
		if (item is null) {
			GemSlotsMenu.SetState(null);
			ArtifactInterface.TheArtifact = null;
		}
		else {
			if (GemSlotsMenu.CurrentState != null) {
				GemSlotsMenu.SetState(null);
			}
			else {
				GemSlotsMenu.SetState(ArtifactInterface);
				var clone = item.Item.Clone().ModItem as ArtifactItem;
				ArtifactInterface.TheArtifact = clone;
			}
		}
	}
	public override void UpdateUI(GameTime gameTime) {
		GemSlotsMenu?.Update(gameTime);
		JewelryStationUI?.Update(gameTime);
	}
	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
		int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Dresser Window")); //Vanilla: Interface Logic 3
		if (index != -1) {
			var p = Main.LocalPlayer; 
			layers.Insert(index, new LegacyGameInterfaceLayer(
					"Crystallography: GemSlotMenu",
					delegate {
						GemSlotsMenu.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			layers.Insert(index, new LegacyGameInterfaceLayer(
					"Crystallography: JewelryStationUI",
					delegate {
						JewelryStationUI.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
		}
	}
}
