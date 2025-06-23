using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items;
public class TestingArtifact : ArtifactItem {
	public override string Texture => $"Terraria/Images/Item_{ItemID.CrossNecklace}";
	public override int GemCount => 4;
	public override void SetArtifactDefaults() {
		Gems[0] = new Item(ModContent.ItemType<Gem>());
		if (Gems[0].ModItem is Gem gem) {
			gem.Data = new GemData(ItemID.Sapphire, 2f, [GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Minor), GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Minor), GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Generic)], Color.White);
		}
	}
}
