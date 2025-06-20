
using Crystallography.Core.Artifacts;

namespace Crystallography.Content.Items;

public class ArtifactItem : ModItem {
	public ArtifactMaterial Material;
	public readonly Gem[] Gems;
	public override void SetDefaults() {
		Item.SetNameOverride($"{Material.Name} ");
	}
}
