using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Crystallography.Core.UI;
public class GemballUI : UIState {
	internal Point _useless;
	public Point TilePosition { get { return _useless; } set {
		_useless = value;
			var mousestate = Mouse.GetState();
			slot.Top.Set(0, 0.25f);
			slot.Left.Set(0, 0f);
			button.Top.Set(0, 0.5f);
			button.Left.Set(0, 0.6f);
		} }
	internal UIElement element;
	public GemballUISlot slot;
	public GemballUIButton button;
	public void TryReroll() {
		if (!slot.Slot.IsAir && slot.Slot.ModItem is Gem gem) {
			foreach(var item in Main.LocalPlayer.inventory) {
				if (item.type == ItemID.FallenStar && item.stack >= 3) {
					for (int i = 0; i < gem.Data.Effects.Length; i++) {
						gem.Data.Effects[i] = GemTypeLoader.GetRandomEffect(gem.Data.Type, gem.Data.Effects[i].Type);
					}
					item.stack -= 3;
					if (item.stack <= 0) {
						item.TurnToAir();
					}
					SoundEngine.PlaySound(SoundID.ResearchComplete);
				}
			}
		}
	}
	public override void OnInitialize() {
		
		var mousestate = Mouse.GetState();
		element = new UIElement();
		element.Width.Set(100, 0);
		element.Height.Set(60, 0);
		element.Top.Set(mousestate.Y - 50,0);
		element.Left.Set(mousestate.X - 50, 0);
		slot = new GemballUISlot();
		slot.Width.Set(52, 0);
		slot.Height.Set(52, 0);
		slot.Top.Set(0, 0.25f);
		slot.Left.Set(0, 0f);
		slot.Activate();
		slot.Slot = new Item();
		slot.Slot.TurnToAir();
		button = new GemballUIButton();
		button.Width.Set(30, 0);
		button.Height.Set(30, 0);
		button.Top.Set(0, 0.5f);
		button.Left.Set(0, 0.6f);
		button.Activate();
		Append(element);
		element.Append(button);
		element.Append(slot);
	}
	public override void Update(GameTime gameTime) {
		slot.Update(gameTime);
		button.Update(gameTime);
		element.Update(gameTime);
		bool hovered = false;
		foreach(var child in element.Children) {
			if (child.IsMouseHovering) {
				hovered = true;
			}
		}
		if (element.IsMouseHovering) {
			Main.LocalPlayer.mouseInterface = true;
		}
		if (!hovered && Main.mouseLeft && element.IsMouseHovering) {
			element.HAlign = 0f;
			element.VAlign = 0f;
			element.Top.Set(Main.mouseY-element.Height.Pixels / 2, 0);
			element.Left.Set(Main.mouseX - element.Width.Pixels / 4, 0);
		}
		if (Main.LocalPlayer.Distance(TilePosition.ToWorldCoordinates()) > 120 && slot.Slot is not null) {
			var item = slot.Slot.Clone();
			Item.NewItem(item.GetSource_DropAsItem(), TilePosition.ToWorldCoordinates(), item);
			slot.Slot.TurnToAir();
			
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		Vector2 drawPos = element.GetDimensions().Position();
		Rectangle pos = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)element.Width.Pixels,(int)element.Height.Pixels);
		CrystallographyUtils.Draw9SliceInventoryPanel(spriteBatch, TextureAssets.InventoryBack9.Value, pos, Color.White with { A = 220});
	}
}
public class GemballUIButton : UIElement {
	public override void LeftClick(UIMouseEvent evt) {
		var parent = (GemballUI)(Parent.Parent);
		parent.TryReroll();
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		var tex = IsMouseHovering ? TextureAssets.Reforge[1].Value : TextureAssets.Reforge[0].Value;
		spriteBatch.Draw(tex, GetDimensions().Position(), Color.White);
		CrystallographyUtils.DrawItemFromType(ItemID.FallenStar, null, GetDimensions().Position() + new Vector2(16, 30), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None);
		ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, "3", GetDimensions().Position() + new Vector2(24, 20), Color.White, 0, Vector2.Zero, Vector2.One);
	}
}
public class GemballUISlot : UIElement {
	public Item Slot;
	public override void OnInitialize() {
		Width.Set(52 * Main.inventoryScale, 0);
		Height.Set(52 * Main.inventoryScale, 0);
	}
	public override void LeftClick(UIMouseEvent evt) {
		var parent = (GemballUI)(Parent.Parent);
		if (IsMouseHovering) {
			if (!Main.mouseItem.IsAir && Main.mouseItem.ModItem is Gem gem) {
				if (Slot is null || Slot.IsAir) {
					Slot = Main.mouseItem.Clone();
					Main.mouseItem.TurnToAir(true);
					Main.LocalPlayer.inventory[58].TurnToAir(true);
				}
				else if (!Slot.IsAir) {
					var gemLocal = Slot.Clone();
					var gemMouse = Main.mouseItem.Clone();
					Slot = gemMouse;
					Main.mouseItem.TurnToAir();
					Main.LocalPlayer.inventory[58].TurnToAir();
					Main.mouseItem = gemLocal;
					Main.LocalPlayer.inventory[58] = gemLocal;
				}
			}
			else if (Main.mouseItem.IsAir && Slot is not null && !Slot.IsAir) {
				var item = Slot.Clone();
				if (!Main.playerInventory)
					Main.playerInventory = true;
				Main.mouseItem = item;
				Main.LocalPlayer.inventory[58] = item;
				Slot.TurnToAir(false);
			}
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		Vector2 drawPos = GetDimensions().Position();
		Rectangle pos = new Rectangle((int)drawPos.X, (int)drawPos.Y, 40, 40);
		CrystallographyUtils.Draw9SliceInventoryPanel(spriteBatch, TextureAssets.InventoryBack9.Value, pos, Color.White);
		if (Slot is not null && Slot.ModItem is Gem) {
			var data = ((Gem)Slot.ModItem).Data;
			CrystallographyUtils.DrawItemFromType(data.Type, null, GetDimensions().Center(), data.Color, 0, Vector2.Zero, Vector2.One * 0.6f * 1.5f, SpriteEffects.None);
		}
		if (IsMouseHovering) {
			if (Slot is null || Slot.IsAir) {
				UICommon.TooltipMouseText(Language.GetText("Mods.Crystallography.ArtifactUI.HintSlotItem").Value);
			}
			else {
				Main.HoverItem = Slot;
				Main.instance.MouseText("");
			}
		}
	}
}
