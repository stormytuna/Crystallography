using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Crystallography.Core;
using Crystallography.Core.Artifacts;
using Crystallography.Core.Utilities;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Crystallography.Content.Items;
public class Gem : ModItem {
	/// <summary>
	///		Contains all the custom properties for this Gem.
	/// </summary>
	public GemData Data;
	/// <summary>
	///		The artifact item instance this gem is slotted in,
	///		<see langword="null"/> if the gem is not slotted in any artifact.
	/// </summary>
	public ArtifactItem? Artifact;
	public override string Texture => Assets.Textures.Empty;
	public override void SetDefaults() {
		//Data = new GemData(ItemID.Sapphire, 2, [], Color.White);
		var item = ContentSamples.ItemsByType[Data.Type];
		Item.width = item.width;
		Item.height = item.height;
		Item.value = item.value + item.value * (int)Data.Strength;
		Item.SetNameOverride(item.Name);
	}
	// this will also need a draw layer, unless the items never have a usestyle, then not :steamhappy:
	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
		CrystallographyUtils.DrawItemFromType(Data.Type, null, position, drawColor.MultiplyRGB(Data.Color), 0, origin, scale, SpriteEffects.None);
		return false;
	}
	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
		CrystallographyUtils.DrawItemFromType(Data.Type, null, Item.Center-Main.screenPosition, alphaColor.MultiplyRGB(Data.Color), rotation, Vector2.Zero, scale, SpriteEffects.None);
		return false;
	}
	public override void ModifyTooltips(List<TooltipLine> tooltips) {
		TooltipLine top = new TooltipLine(Mod, "GemStrength", Language.GetText("Mods.Crystallography.GemData.Strength").WithFormatArgs(Data.Strength).Value);
		top.OverrideColor = Color.Lerp(Color.DarkGray, new Color(185,255,246), Data.Strength / 1.5f);
		tooltips.Add(top);
		if (Data.Effects is not null) {
			TooltipLine header = new TooltipLine(Mod, "EffectsHeader", Language.GetText("Mods.Crystallography.GemEffects.Header").Value);
			tooltips.Add(header);
			TooltipLine major = new TooltipLine(Mod, "MajorEffect", $"[i:{ItemID.GoldCoin}]" + Data.Effects[0].GetFormattedTooltip(Data.Strength));
			tooltips.Add(major);
			TooltipLine minor = new TooltipLine(Mod, "MinorEffect", $"[i:{ItemID.SilverCoin}]" + Data.Effects[1].GetFormattedTooltip(Data.Strength));
			tooltips.Add(minor);
			TooltipLine generic = new TooltipLine(Mod, "GenericEffect", $"[i:{ItemID.CopperCoin}]" + Data.Effects[2].GetFormattedTooltip(Data.Strength));
			tooltips.Add(generic);
		}
	}
	public override void SaveData(TagCompound tag) {
		tag["gemID"] = Data.Type;
		tag["gemStrength"] = Data.Strength;
		tag["gemColorOverride"] = Data.Color;
		tag["gemEffectCount"] = Data.Effects.Length;
		for (int i = 0; i < Data.Effects.Length; i++) {
			tag[$"gemEffect{i}"] = Data.Effects[i].Name;
		}
	}
	public override void LoadData(TagCompound tag) {
		int id = tag.Get<int>("gemID");
		float strength = tag.Get<float>("gemStrength");
		Color gemColor = tag.Get<Color>("gemColorOverride");
		GemEffect[] effects = new GemEffect[tag.Get<int>("gemEffectCount")];
		for (int i = 0; i < effects.Length; i++) {
			effects[i] = GemTypeLoader.GemEffectLookup[(string)tag[$"gemEffect{i}"]];
		}
		Data = new GemData(id, strength, effects, gemColor);
		SetDefaults();
	}
	public override ModItem Clone(Item newEntity) {
		var clone = (Gem)base.Clone(newEntity);
		clone.Data = Data;
		return clone;
	}
}
