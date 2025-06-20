namespace Crystallography.Core.Artifacts;

public class ArtifactItem : ModItem {
	public readonly ArtifactMaterial Material;
	public readonly Gem[] Gems;
	private ArtifactItem() { }
	public ArtifactItem(ArtifactMaterial material, Gem[] gems) {
		Material = material;
		Gems = gems;
	}
	public sealed override void UpdateAccessory(Player player, bool hideVisual) {
		UpdateAccessoryEquip(player, hideVisual);
	}
	public virtual void UpdateAccessoryEquip(Player player, bool hideVisual) { }
}
