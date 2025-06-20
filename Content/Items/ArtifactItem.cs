
using Crystallography.Core;
using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items;

public class ArtifactItem : ModItem {
	public override string Texture => Assets.Textures.Empty;
	public ArtifactMaterial Material;
	public readonly Gem[] Gems;
	public override void SetDefaults() {
		Item.accessory = true;
		//Item.SetNameOverride($"{Material?.Name} ");
	}
	public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
		return equippedItem.ModItem is not ArtifactItem;
	}
}
