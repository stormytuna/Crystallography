using System;
using System.Collections.Generic;
using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Crystallography.Core.UI;
public class JewelryUI : UIState {
	public Point JewelryStationPosition;
	JewelryCrafter Area;
	public override void OnInitialize() {
		Area = new JewelryCrafter();
		Area.Top.Set(Main.screenHeight/2, 0);
		Area.Left.Set(Main.screenWidth / 2, 0);
		Area.Width.Set(300*Main.inventoryScale,0);
		Area.Height.Set(150 * Main.inventoryScale, 0);
		Area.Activate();
		Append(Area);
	}
	public override void OnActivate() {
		Area?.CreateSlots();
	}
	public override void Update(GameTime gameTime) {
		if (Main.LocalPlayer.Center.Distance(JewelryStationPosition.ToWorldCoordinates()) > 80) {
			Area.DropAllItems();
			UIManagerSystem.ToggleJewelryUI(Point.Zero);
			Area.HAlign = 0;
			Area.VAlign = 0;
			Area.Top.Set(Main.screenHeight / 2, 0);
			Area.Left.Set(Main.screenWidth / 2, 0);
			Area.Width.Set(300 * Main.inventoryScale, 0);
			Area.Height.Set(150 * Main.inventoryScale, 0);
		}
		Area.Update(gameTime);
	}
}
public class JewelryCrafter : UIElement {
	public override void OnInitialize() {
		CreateSlots();
	}
	internal Item[] Slots = new Item[5];
	public bool CraftingReady;
	public override void Update(GameTime gameTime) {
		if (IsMouseHovering) {
			Main.LocalPlayer.mouseInterface = true;
		}
		bool hoveringOverSlot = false;
		foreach(var child in Children) {
			child.Update(gameTime);
			if (child is JewelrySlot slot) {
				Slots[slot.whoAmI] = slot.SlottedItem;
			}
			if (child.IsMouseHovering)
				hoveringOverSlot = true;
		}
		if ((!hoveringOverSlot || (Main.mouseItem.IsAir && !Main.playerInventory)) && Main.mouseLeft && IsMouseHovering) {
			HAlign = 0f;
			VAlign = 0f;
			Top.Set(Main.mouseY - Height.Pixels / 2, 0);
			Left.Set(Main.mouseX - Width.Pixels / 2, 0);
		}
	}
	public void CreateSlots() {
		RemoveAllChildren();
		for (int i = 0; i < 5; i++) {
			JewelrySlot slot = new();
			slot.HAlign = 0f;
			slot.VAlign = 0.5f;
			slot.Left.Set(0, ((float)(i + 1) / 5)-0.2f);
			float y = (i == 0 || i == 4) ? 0.2f : 0;
			float extraScale = i > 0 ? 0.1f : 0;
			slot.Top.Set(0, y);
			slot.Width.Set(52 * (Main.inventoryScale+extraScale), 0);
			slot.Height.Set(52 * (Main.inventoryScale+extraScale), 0);
			slot.Scale = Main.inventoryScale + extraScale;
			Main.NewText("reset");
			Append(slot);
			slot.whoAmI = i;
			if (i == 4)
				slot.IsOutputSlot = true;
		}
	}
	public void DropAllItems() {
		foreach(var child in Children) {
			if (child is JewelrySlot slot) {
				slot.DropItem();
			}
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		// draw back panel here
		string text = Language.GetText("Mods.Crystallography.JewelryUI.InputsText").Value;
		Vector2 drawPos = GetDimensions().Center();
		Vector2 size = FontAssets.MouseText.Value.MeasureString(text);
		drawPos.X -= size.X / 2;
		drawPos.Y += size.Y;
		ChatManager.DrawColorCodedStringShadow(spriteBatch, FontAssets.MouseText.Value, text, drawPos, Color.Black, 0, Vector2.Zero, Vector2.One);
		ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value,text, drawPos, Color.White, 0, Vector2.Zero, Vector2.One);
	}
}
public class JewelrySlot : UIPanel {
	public Item SlottedItem;
	public int whoAmI;
	public bool IsOutputSlot;
	public float Scale;
	internal static List<Item> ConsumedItems = new List<Item>();
	public override void LeftClick(UIMouseEvent evt) {
		if (IsMouseHovering) {
			if(Main.mouseItem.IsAir && IsOutputSlot && SlottedItem != null && !SlottedItem.IsAir && ((JewelryCrafter)Parent).CraftingReady) {
				TakeItemOut();
			}
			else if (Main.mouseItem.IsAir && SlottedItem != null && !SlottedItem.IsAir) {
				TakeItemOut();
			}
			else if(!Main.mouseItem.IsAir) {
				if (SlottedItem != null) {
					if (CanBeSlotted(whoAmI)) {
						var localclone = SlottedItem.Clone();
						var mouse = Main.mouseItem.Clone();
						if (!Main.playerInventory)
							Main.playerInventory = true;
						SlottedItem.TurnToAir();
						Main.mouseItem.TurnToAir();
						Main.mouseItem = localclone;
						Main.LocalPlayer.inventory[58] = localclone;
						SlottedItem = mouse;
					}

				}
				else {
					var mouse = Main.mouseItem.Clone();
					SlottedItem = mouse;
					Main.mouseItem.TurnToAir();
					Main.LocalPlayer.inventory[58].TurnToAir();
				}
			}
			if (IsOutputSlot && Main.mouseItem.IsAir && SlottedItem.IsAir) {
				foreach(var value in ArtifactMaterial.ArtifactMaterialLookup.Values) {
					foreach(var ingredient in value.Ingredients) {
						var required = ingredient.Clone();
						for (int i = 0; i < 3; i++) {
							var item = ((JewelryCrafter)Parent).Slots[i + 1];
							var consumed = item.Clone();
							if (required.netID != consumed.netID) {
								continue;
							}
							consumed.stack = Math.Min(item.stack, required.stack);
							if (consumed.stack >= 0) {
								ConsumedItems.Add(consumed);
							}
							if (consumed.stack == 0) {
								item.TurnToAir();	
							}
						}
					}
				}
			}
		}
		void TakeItemOut() {
			var item = SlottedItem.Clone();
			if (!Main.playerInventory)
				Main.playerInventory = true;
			Main.mouseItem = item;
			Main.LocalPlayer.inventory[58] = item;
			SlottedItem.TurnToAir(false);
		}
		bool CanBeSlotted(int whoAmI) {
			if (whoAmI == 4) {
				return false;
			}
			if (whoAmI == 0) {
				return Main.mouseItem.ModItem is ArfitactMold;
			}
			else return true;
		}
	}
	public void DropItem() {
		if (SlottedItem is not null && !SlottedItem.IsAir) {
			Item item = SlottedItem.Clone();
			Item.NewItem(Main.LocalPlayer.GetSource_ItemUse(item), UIManagerSystem.JewelryInterface.JewelryStationPosition.ToWorldCoordinates(), item);
			SlottedItem.TurnToAir();
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		Vector2 drawPos = GetDimensions().Center();
		spriteBatch.Draw(Assets.Textures.JewelrySlotBack.Value, GetDimensions().Position(), null, GetPanelColor(whoAmI), 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
		spriteBatch.Draw(Assets.Textures.JewelrySlotOutline.Value, GetDimensions().Position(), null, GetPanelColor(whoAmI), 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
		string key = GetKey(whoAmI);
		if (!string.IsNullOrEmpty(key)) {
			string text = Language.GetText(key).Value;
			drawPos.Y += 20;
			drawPos.X -= 20;
			Color drawColor = whoAmI == 0 ? Color.White : Color.Orange;
			ChatManager.DrawColorCodedStringShadow(spriteBatch, FontAssets.MouseText.Value, text, new Vector2(drawPos.X, drawPos.Y), Color.Black, 0, Vector2.Zero, new Vector2(Scale));
			if (whoAmI == 4) {
				spriteBatch.End();
				spriteBatch.Begin();
			}
			ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, text, new Vector2(drawPos.X, drawPos.Y), drawColor, 0, Vector2.Zero, new Vector2(Scale));
			if (whoAmI == 4) {
				spriteBatch.End();
				spriteBatch.Begin();
			}
		}
		if (SlottedItem != null && !SlottedItem.IsAir) {
			CrystallographyUtils.DrawItemFromType(SlottedItem.type, null, GetDimensions().Center(), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, SlottedItem.stack.ToString(), drawPos + new Vector2(-12,0), Color.White, 0, Vector2.Zero, Vector2.One);
			if (IsMouseHovering) {
				if (IsOutputSlot && Main.mouseItem.IsAir && SlottedItem.IsAir) {
					Main.instance.MouseText("Click to try creating artifact");
				}
				else if (!SlottedItem.IsAir) {
					Main.HoverItem = SlottedItem;
					Main.instance.MouseText("");
				}
			}
		}
	}
	private static string GetKey(int whoAmI) {
		if (whoAmI == 0) {
			return "Mods.Crystallography.JewelryUI.Mold";
		}
		if (whoAmI == 4) {
			return "Mods.Crystallography.JewelryUI.Output";
		}
		else return string.Empty;
	}
	private static Color GetPanelColor(int whoAmI) {
		return whoAmI == 4 ? new Color(249, 199, 47) : Color.White;
	}
}
