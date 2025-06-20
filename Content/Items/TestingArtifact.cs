using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items;
public class TestingArtifact : ArtifactItem {
	public override string Texture => $"Terraria/Images/Item_{ItemID.CrossNecklace}";
	public override int GemCount => 3;
	public override void SetArtifactDefaults() {
		Gems[0].Data = new GemData(ItemID.Sapphire, 2f, [GemTypeLoader.GemEffectLookup["TestEffect"]], Color.White);
	}
}
