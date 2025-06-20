
using System.Collections.Generic;
using Crystallography.Content.Items;
using Terraria.Localization;

namespace Crystallography.Core.Artifacts;

public abstract class ArtifactItem : ModItem {
	public override string Texture => Assets.Textures.Empty;
	public ArtifactMaterial Material;
	public readonly Gem[] Gems;
	public ArtifactItem() {
		Gems = new Gem[GemCount];
	}
	public abstract int GemCount { get; }
	public sealed override void SetDefaults() {
		Item.accessory = true;
		for(int i = 0; i < GemCount; i++) {
			Gems[i] = new Item(ModContent.ItemType<Gem>()).ModItem as Gem;
		}
		Item.SetNameOverride($"{Material?.Name} {Language.GetOrRegister($"{this.GetType().Name}")}");
		SetArtifactDefaults();
	}
	public sealed override void UpdateAccessory(Player player, bool hideVisual) {
		foreach (var gem in Gems) {
			foreach(var effect in gem.Data.Effects) {
				effect.Apply(player, gem.Data);
			}
		}
		UpdateAccessoryEquip(player, hideVisual);
	}
	public virtual void UpdateAccessoryEquip(Player player, bool hideVisual) { }
	public virtual void SetArtifactDefaults() { }
	public sealed override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
		return equippedItem.ModItem is not ArtifactItem;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips) { 
		base.ModifyTooltips(tooltips); // list material modifier, each gem, if shift is held down also list each gems effects
	}
}
