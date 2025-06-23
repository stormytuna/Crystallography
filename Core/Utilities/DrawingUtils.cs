using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.Map;

namespace Crystallography.Core.Utilities;
public partial class CrystallographyUtils {
	internal static FieldInfo tileLookup;
	internal static FieldInfo mapColorLookup;
	public static void DrawItemFromType(int type, Rectangle? frame, Vector2 position, Color drawColor, float rotation, Vector2 origin, float scale, SpriteEffects effects) => DrawItemFromType(type,frame,position,drawColor,rotation,origin, new Vector2(scale), effects);
	public static void DrawItemFromType(int type, Rectangle? frame, Vector2 position, Color drawColor, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects) {
		Main.instance.LoadItem(type);
		GetItemDrawData(out var sourceFrame, out var tex);
		if (origin == Vector2.Zero)
			origin = sourceFrame.Size() / 2;
		if (frame is null) 
			frame = sourceFrame;
		Main.spriteBatch.Draw(tex, position, frame, drawColor, rotation, origin, scale, SpriteEffects.None, 0);
		void GetItemDrawData(out Rectangle frame, out Texture2D texture) {
			// Get the texture
			texture = TextureAssets.Item[type].Value;
			// Get the source frame
			frame = Main.itemAnimations[type] != null
				? Main.itemAnimations[type].GetFrame(texture, Main.itemAnimations[type].FrameCounter)
				: texture.Frame();
		}
	}
	public static void Draw9SliceInventoryPanel(SpriteBatch spriteBatch, Texture2D texture, Rectangle drawPosition, Color drawColor) {
		drawPosition.Width = (int)MathHelper.Clamp(drawPosition.Width, 20, 2000);
		drawPosition.Height = (int)MathHelper.Clamp(drawPosition.Height, 20, 2000);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X, drawPosition.Y, 10, 10), new Rectangle(0, 0, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + 10, drawPosition.Y, drawPosition.Width - 20, 10), new Rectangle(10, 0, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + drawPosition.Width - 10, drawPosition.Y, 10, 10), new Rectangle(texture.Width - 10, 0, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X, drawPosition.Y + 10, 10, drawPosition.Height - 20), new Rectangle(0, 10, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + 10, drawPosition.Y + 10, drawPosition.Width - 20, drawPosition.Height - 20), new Rectangle(10, 10, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + drawPosition.Width - 10, drawPosition.Y + 10, 10, drawPosition.Height - 20), new Rectangle(texture.Width - 10, 10, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X, drawPosition.Y + drawPosition.Height - 10, 10, 10), new Rectangle(0, texture.Height - 10, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + 10, drawPosition.Y + drawPosition.Height - 10, drawPosition.Width - 20, 10), new Rectangle(10, texture.Height - 10, 10, 10), drawColor);
		spriteBatch.Draw(texture, new Rectangle(drawPosition.X + drawPosition.Width - 10, drawPosition.Y + drawPosition.Height - 10, 10, 10), new Rectangle(texture.Width - 10, texture.Height - 10, 10, 10), drawColor);
	}
	public static Color GetTileMapColor(int type) {
		tileLookup ??= typeof(MapHelper).GetField("tileLookup", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public);
		mapColorLookup ??= typeof(MapHelper).GetField("colorLookup", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public);
		var map = (Color[])mapColorLookup.GetValue(null);
		var tiles = (ushort[])tileLookup.GetValue(null);
		return map[tiles[type]];
	}
}
