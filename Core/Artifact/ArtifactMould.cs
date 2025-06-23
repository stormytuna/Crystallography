namespace Crystallography.Core.Artifacts;

public abstract class ArfitactMold : ModItem {
	public abstract int ArtifactType { get; }	
	public override void SetDefaults() {
		Item.width = Item.height = 16;
	}
}
