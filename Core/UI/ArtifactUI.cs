using System.Linq;
using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Crystallography.Core.UI;
public class ArtifactUI : UIState {
	ArtifactSlotRing Area;
	ArtifactItem _artifact = null;
	public ArtifactItem TheArtifact { 
		get { return _artifact; } 
		set {
			_artifact = value;
			Area.ReinitializeSlots();
			var mousestate = Mouse.GetState(); //Main.MouseScreen gets unreliable with zoom/etc.
			Area.Top.Set(mousestate.Y - Area.Width.Pixels/2, 0); 
			Area.Left.Set(mousestate.X - Area.Height.Pixels/2, 0);
			Area.OverflowHidden = false;
		} 
	}	
	public override void OnInitialize() {
		Area = new ArtifactSlotRing();
		Area.Width.Set(300 * Main.inventoryScale, 0);
		Area.Height.Set(300 * Main.inventoryScale, 0);
		Area.Activate();
		Append(Area);
	}
	public override void Update(GameTime gameTime) {
		Area.Update(gameTime);
	}
}
public class ArtifactSlotRing : UIElement
{
	private bool _centerOnMouse = false;
	public override void OnInitialize() {
		ReinitializeSlots();
	}
	public override void OnActivate() {
		base.OnActivate();
		_centerOnMouse = true;
	}
	public override void Update(GameTime gameTime) {		
		base.Update(gameTime);
		if (_centerOnMouse) {
			_centerOnMouse = false;
			Left.Set(Main.mouseX - Width.Pixels / 2f, 0f);
			Top.Set(Main.mouseY - Height.Pixels / 2f, 0f);
			Recalculate();
		}
                                                    
		var hovering = false;
		foreach (var child in Children) {
			child.Update(gameTime);
			if (child.IsMouseHovering)
				hovering = true;
		}
		if (!hovering && Main.mouseLeft && IsMouseHovering) {
			HAlign = 0f;
			VAlign = 0f;
			Top.Set(Main.mouseY - Height.Pixels/2, 0);
			Left.Set(Main.mouseX - Width.Pixels/2, 0);
		}
	}
	protected override void DrawChildren(SpriteBatch spriteBatch) {
		foreach(var child in Children) {
			child.Draw(spriteBatch);
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		var style = GetDimensions();
		//var pos = style.Position();
		var mouse = Mouse.GetState();
		//Vector2 pos = new Vector2(mouse.X, mouse.Y);
		//ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, pos.ToString(), pos, Color.White, 0, Vector2.Zero, Vector2.One);
		//spriteBatch.Draw(TextureAssets.MagicPixel.Value, pos, new Rectangle((int)pos.X,(int)pos.Y, (int)style.Width, (int)style.Height), Color.White);
	}
	public void ReinitializeSlots() {
		if (Parent != null) {
			var artifact = ((ArtifactUI)Parent).TheArtifact;
			this.RemoveAllChildren();
			for (int i = 0; i < artifact.GemCount; i++) {
				Vector2 desiredPos = new Vector2(0, Height.Pixels / 2.5f).RotatedBy((MathHelper.TwoPi / artifact.GemCount) * i, Vector2.Zero);
				Vector2 center = Vector2.Zero;
				ArtifactGemSlotUI slot = new();
				slot.HAlign = 0.5f;
				slot.VAlign = 0.5f;
				slot.Left.Set(center.X, 0);
				slot.Top.Set(center.Y, 0);
				slot.Width.Set(60 * Main.inventoryScale, 0);
				slot.Height.Set(60 * Main.inventoryScale, 0);
				slot.Scale = 0.5f;
				slot.DesiredLocation = desiredPos;
				slot.Gem = artifact.Gems[i];
				this.Append(slot);
				slot.whoAmI = i;
			}
		}
	}
}
public class ArtifactGemSlotUI : UIPanel {
	public Item Gem { get; set; }
	public Vector2 DesiredLocation;
	internal float _scale;
	public int whoAmI;
	public float Scale { get { return _scale; }
		set {
			_scale = value;
			Width.Set(60 * Main.inventoryScale * _scale*1.1f, 0);
			Height.Set(60 * Main.inventoryScale * _scale*1.1f, 0);
		}
	}
	public override void OnInitialize() {
		Width.Set(52 * Main.inventoryScale, 0);
		Height.Set(52*Main.inventoryScale, 0);
		//OnLeftMouseUp += TryClickItem;
	}
	public override void LeftClick(UIMouseEvent evt) {
		var parent = ((ArtifactUI)Parent.Parent);
		if (IsMouseHovering) {
			if (!Main.mouseItem.IsAir && Main.mouseItem.ModItem is Gem gem) {
				if (Gem is null || Gem.IsAir) {
					Gem = Main.mouseItem.Clone();
					Main.mouseItem.TurnToAir(true);
					Main.LocalPlayer.inventory[58].TurnToAir(true);
					parent.TheArtifact.Gems[whoAmI] = Gem.Clone();
					Main.NewText(parent.TheArtifact.Gems[whoAmI]);
				}
				else if (!Gem.IsAir) {
					var gemLocal = Gem.Clone();
					var gemMouse = Main.mouseItem.Clone();
					Gem = gemMouse;
					Main.mouseItem.TurnToAir();
					Main.LocalPlayer.inventory[58].TurnToAir();
					Main.mouseItem = gemLocal;
					Main.LocalPlayer.inventory[58] = gemLocal;
					parent.TheArtifact.Gems[whoAmI] = Gem.Clone();
					Main.NewText(parent.TheArtifact.Gems[whoAmI]);
				}
			}
			else if (Main.mouseItem.IsAir && Gem is not null && !Gem.IsAir) {
				var item = Gem.Clone();
				if (!Main.playerInventory)
					Main.playerInventory = true;
				Main.mouseItem = item;
				Main.LocalPlayer.inventory[58] = item;
				Gem.TurnToAir(false);
				parent.TheArtifact.Gems[whoAmI] = Gem.Clone();
				Main.NewText(parent.TheArtifact.Gems[whoAmI]);
			}
		}
	}

	public override void Update(GameTime gameTime) {
		Scale = MathHelper.SmoothStep(Scale, 1f, 0.14f);
		Vector2 pos = new Vector2(Left.Pixels, Top.Pixels);
		Vector2 nextPos = Vector2.SmoothStep(pos, DesiredLocation, 0.1f);
		// cant get ts to work, maybe tuna can try fixing!!
		//Vector2 nextPos = CrystallographyUtils.CubicLerp(Vector2.Zero, pos, DesiredLocation, Vector2.SmoothStep(Vector2.Zero, DesiredLocation, 0.2f), 0.01f);
		Top.Set(nextPos.Y, 0);
		Left.Set(nextPos.X, 0);
		if (IsMouseHovering) {
			Main.LocalPlayer.mouseInterface = true;
		}
	}
	protected override void DrawSelf(SpriteBatch spriteBatch) {
		var style = GetDimensions();
		var pos = style.Center();
		var panelTexture = Assets.Textures.GemSlotPanel;
		spriteBatch.End();
		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		spriteBatch.Draw(panelTexture.Value, pos, null, Color.White, 0, panelTexture.Size()/2, Scale* 0.6f *1.2f, SpriteEffects.None, 0);
		if (Gem is not null && Gem.ModItem is Gem) {
			var data = ((Gem)Gem.ModItem).Data;
			CrystallographyUtils.DrawItemFromType(data.Type, null, pos, data.Color, 0, Vector2.Zero, Vector2.One * 0.6f * 1.5f, SpriteEffects.None);
		}
		if (IsMouseHovering) {
			if (Gem is null || Gem.IsAir) {
				UICommon.TooltipMouseText(Language.GetText("Mods.Crystallography.ArtifactUI.HintSlotItem").Value);
			}
			else {
				Main.HoverItem = Gem;
				Main.instance.MouseText("");
			}
		}
	}
}
