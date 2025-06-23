using System;
using System.Collections.Generic;
using System.Linq;
using Crystallography.Content.Items;
using ReLogic.Content;
using Terraria.Localization;

namespace Crystallography.Core.Artifacts;

public class ArtifactMaterial {
	public static readonly Dictionary<string, ArtifactMaterial> ArtifactMaterialLookup = new Dictionary<string, ArtifactMaterial>();
	//public static readonly List<string> RegisteredNames = new List<string>();
	public ArtifactMaterial(string name, Item[] ingredients, int type, float amplifier, ArtifactMaterial.EffectCallback? callback, string texturePath, GetCallbackDescription? callbackDescription) {
		Name = name;
		Type = type;
		Amplifier = amplifier;
		TexturePath = texturePath;
		Callback = callback is null ? DefaultCallback : callback;
		CallbackDescription = callback is null ? GetDefaultCallbackDescription : (callbackDescription is null ? GetDefaultCallbackDescription : callbackDescription);
		ArtifactMaterialLookup[name] = this;
		Array.Resize(ref ingredients, 3);
		Ingredients = ingredients.ToList();
	}
	/// <summary>
	///		Array containing the items needed to create this artifact.
	/// </summary>
	public readonly List<Item> Ingredients;
	/// <summary>
	///		The localization key for the name of this material.
	/// </summary>
	public readonly string Name;
	/// <summary>
	///		The ItemID of the material itself.
	/// </summary>
	public readonly int Type;
	/// <summary>
	///		Multiplier for effect strength.
	/// </summary>
	public float Amplifier;
	/// <summary>
	///		Index 0 is the item texture, index 1 is the equip texture.
	///		Leads to the root material path texture, an example would look like this: <br></br>
	///		Crystallography/Assets/Textures/Artifacts/Copper <br></br>
	///		In this case, a necklace type item will try to get: <br></br>
	///		Crystallography/Assets/Textures/Artifacts/Copper/Necklace <br></br>
	///		Crystallography/Assets/Textures/Artifacts/Copper/Necklace_Equip	
	/// </summary>
	public readonly string TexturePath;
	public readonly EffectCallback Callback;
	public readonly GetCallbackDescription CallbackDescription;
	/// <summary>
	///		Allows for extra effects based on the gem type.
	///		Invoked during the gem effect activation.
	/// </summary>
	public delegate void EffectCallback(ref GemData data, Gem gem, Player player);
	private static void DefaultCallback(ref GemData data, Gem gem, Player player) { }
	public delegate LocalizedText GetCallbackDescription(ArtifactItem item);
	private static LocalizedText GetDefaultCallbackDescription(ArtifactItem item) {
		return Language.GetText("Mods.Crystallography.ArfitactMaterial.DefaultCallbackDescription").WithFormatArgs(item.Material.Amplifier);
	}
}
