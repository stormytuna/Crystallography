
using System.Collections.Generic;
using Crystallography.Content.Items;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Crystallography.Core.Artifacts;

public abstract class ArtifactItem : ModItem {
	public override string Texture => Assets.Textures.Empty;
	public ArtifactMaterial Material;
	public Gem[] Gems { get; private set; }
	public ArtifactItem() {
		Gems = new Gem[GemCount];
	}
	public abstract int GemCount { get; }
	public sealed override void SetDefaults() {
		for (int i = 0; i < GemCount; i++) {
			Gems[i] = new Item(ModContent.ItemType<Gem>()).ModItem as Gem;
			Gems[i].SetDefaults();
		}
		Item.accessory = true;
		Item.SetNameOverride($"{Material?.Name} {Language.GetOrRegister($"{this.GetType().Name}")}");
		SetArtifactDefaults();
	}
	public sealed override void UpdateAccessory(Player player, bool hideVisual) {
		if (Gems is not null) {
			foreach (var gem in Gems) {
				Main.NewText(gem.Data.Type);
				if (gem is not null)
				foreach (var effect in gem.Data.Effects) {
					effect.Apply(player, gem.Data);
				}
			}
		}
		UpdateAccessoryEquip(player, hideVisual);
	}
	public override void SaveData(TagCompound tag) {
		tag["gemCount"] = Gems.Length;
		for (int i = 0; i < Gems.Length; i++) {
			tag["gem" + i] = ItemIO.Save(Gems[i].Item);
		}
	}
	public override void LoadData(TagCompound tag) {
		var count = tag.Get<int>("gemCount");
		Gems = new Gem[count];
		for (int i = 0; i < count; i++) {
			Gems[i] = ItemIO.Load((TagCompound)tag["gem" + i]).ModItem as Gem;
		}
	}
	public virtual void UpdateAccessoryEquip(Player player, bool hideVisual) { }
	public virtual void SetArtifactDefaults() { }
	public sealed override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
		return incomingItem.ModItem is not ArtifactItem || equippedItem.ModItem is not ArtifactItem;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips) { 
		base.ModifyTooltips(tooltips); // list material modifier, each gem, if shift is held down also list each gems effects
	}
}
