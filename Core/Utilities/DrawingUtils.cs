using Terraria;
using Terraria.GameContent;

namespace Crystallography.Core.Utilities;
partial class CrystallographyUtils {
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
}
