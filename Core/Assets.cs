using System;
using ReLogic.Content;

namespace Crystallography.Core;
public static class Assets {
	public static class Textures {
		public static string Empty = "Crystallography/Assets/Textures/Empty";
		public static Asset<Texture2D> GemSlotPanel;
		static Textures() {
			GemSlotPanel = ModContent.Request<Texture2D>("Crystallography/Assets/Textures/GemSlotPanel");
		}
	}
}
