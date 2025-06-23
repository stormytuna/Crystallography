using System;
using ReLogic.Content;

namespace Crystallography.Core;
public static class Assets {
	public static class Textures {
		public static string Empty = "Crystallography/Assets/Textures/Empty";
		public static Asset<Texture2D> GemSlotPanel;
		public static Asset<Texture2D> JewelrySlotOutline;
		public static Asset<Texture2D> JewelrySlotBack;
		static Textures() {
			GemSlotPanel = ModContent.Request<Texture2D>("Crystallography/Assets/Textures/GemSlotPanel");
			JewelrySlotOutline = ModContent.Request<Texture2D>("Crystallography/Assets/Textures/JewelryUISlotGreyscale");
			JewelrySlotBack = ModContent.Request<Texture2D>("Crystallography/Assets/Textures/JewelryUISlotBack");
		}
	}
}
