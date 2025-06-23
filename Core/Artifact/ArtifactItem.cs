
using System.Collections.Generic;
using System.Linq;
using Crystallography.Content.Items;
using Crystallography.Core.UI;
using FishUtils.UI;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Crystallography.Core.Artifacts;

public abstract class ArtifactItem : ModItem {
	public override string Texture { 
		get {
			return Material?.TexturePath ?? Assets.Textures.Empty;		
		} 
	} 
	public ArtifactMaterial Material;
	public Item[] Gems { get; set; }
	public ArtifactItem() {
		if (Gems is null || Gems.Length == 0) 
		Gems = new Item[GemCount];
	}
	public abstract int GemCount { get; }
	public sealed override void SetDefaults() {
		// testing
		Material = new ArtifactMaterial("Gold", [new Item(ItemID.GoldBar, 5)],ItemID.GoldBar, 2, null, $"Terraria/Images/Item_{ItemID.CrossNecklace}", null);
		for (int i = 0; i < GemCount; i++) {
			Gems[i] = new Item(ModContent.ItemType<Gem>());
			Gems[i].SetDefaults();
			if (Gems[i].ModItem is Gem gem) {
				gem.Artifact = Item.ModItem as ArtifactItem;
			}
		}
		Item.accessory = true;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.useTime = Item.useAnimation = 2;
		Item.SetNameOverride($"{Material?.Name} {PrettyPrintName()}");
		Item.noUseGraphic = true;
		SetArtifactDefaults();
	}
	public sealed override void UpdateAccessory(Player player, bool hideVisual) {
		foreach (var gem in Gems) {
			var gemItem = gem.ModItem as Gem;
			if (gemItem != null) {
				var data = gemItem.Data;
				Material.Callback(ref data, gemItem, player);
				foreach (var effect in gemItem.Data.Effects) {
					effect.Apply(player, data);
				}
			}
		}
		UpdateAccessoryEquip(player, hideVisual);
	}
	public override bool AltFunctionUse(Player player) {
		return true;
	}
	public override bool? UseItem(Player player) {
		if (player.altFunctionUse == 2) {
			UIManagerSystem.ToggleArtifactUI(this);
			return true;
		}
		else return false;
	}
	public override void SaveData(TagCompound tag) {
		tag["gemCount"] = Gems.Length;
		for (int i = 0; i < Gems.Length; i++) {
			tag["gem" + i] = ItemIO.Save(Gems[i]);
		}
		tag["materialName"] = Material.Name;
	}
	public override void LoadData(TagCompound tag) {
		var count = tag.Get<int>("gemCount");
		Gems = new Item[count];
		for (int i = 0; i < count; i++) {
			Gems[i] = ItemIO.Load((TagCompound)tag["gem" + i]);
		}
		string name = tag.Get<string>("materialName");
		if (ArtifactMaterial.ArtifactMaterialLookup.TryGetValue(name, out var material)) {
			Material = material;
		}
		else {
			Material = ArtifactMaterial.ArtifactMaterialLookup[ArtifactMaterial.ArtifactMaterialLookup.Keys.First()];
		}
	}
	public virtual void UpdateAccessoryEquip(Player player, bool hideVisual) { }
	public virtual void SetArtifactDefaults() { }
	public sealed override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
		return incomingItem.ModItem is not ArtifactItem || equippedItem.ModItem is not ArtifactItem;
	}
	public override ModItem Clone(Item newEntity) {
		var clone = (ArtifactItem)base.Clone(newEntity);
		clone.Gems = (Item[])Gems?.Clone();
		return clone;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips) { 
		var insertIndex = tooltips.FindIndex(x => x.Mod == "Terraria" && x.Name == "Tooltip0");
		tooltips[insertIndex].Hide();
		
		List<TooltipLine> gemTooltips = new();

		foreach (var gemItem in Gems) {
			var gem = (gemItem.ModItem as Gem);
			if (gem is null) {
				// TODO: tell players they can slot more
				continue;
			}
			
			var tooltip = $"[i:{gemItem.type}] {gem.DisplayName}";
			gemTooltips.Add(new TooltipLine(Mod, "Gem", tooltip));

			if (Keyboard.GetState().IsKeyUp(Keys.LeftShift)) {
				continue;
			}

			foreach (var effect in gem.Data.Effects) {
				var effectTip = effect.GetFormattedTooltip(gem.Data.Strength).Value;
				
				var icon = effect.Type == GemEffect.EffectType.Major ? "MajorIcon" : "MinorIcon";
				var textureTag = TextureTagHandler.CreateTag($"{nameof(Crystallography)}/Assets/Textures/{icon}", effect.Color);
				
				gemTooltips.Add(new TooltipLine(Mod, "GemEffect", $"    {textureTag} {effectTip}"));
			}
		}

		if (Keyboard.GetState().IsKeyUp(Keys.LeftShift)) {
			gemTooltips.Add(new TooltipLine(Mod, "ShiftToExpand", Mod.GetLocalization("ShiftToExpand").Value));
		}

		tooltips.InsertRange(insertIndex, gemTooltips);
	}
}
