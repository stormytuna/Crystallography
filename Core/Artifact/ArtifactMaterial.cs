using System.Collections.Generic;
using Crystallography.Content.Items;
using ReLogic.Content;

namespace Crystallography.Core.Artifacts;

public class ArtifactMaterial {
	public static readonly Dictionary<int[], ArtifactMaterial> IngredientToMaterial = new Dictionary<int[], ArtifactMaterial>();
	public ArtifactMaterial(string name, int[] ingredients, int type, float amplifier, ArtifactMaterial.EffectCallback? callback, string[] texturePaths, string? callbackDescriptionKey) {
		Name = name;
		Type = type;
		Amplifier = amplifier;
		CallbackDescription = callback is null ? "" : callbackDescriptionKey;
		TexturePaths = new string[2] { texturePaths[0], texturePaths[1] };
		Callback = callback is null ? DefaultCallback : callback;
		IngredientToMaterial[ingredients] = this;
	}
	/// <summary>
	///		The localization key for the name of this material.
	/// </summary>
	public readonly new string Name;
	/// <summary>
	///		The ItemID of the material itself.
	/// </summary>
	public readonly int Type;
	/// <summary>
	///		Multiplier for effect strength.
	/// </summary>
	public float Amplifier;
	/// <summary>
	///		The localization key for the materials effect description.
	/// </summary>
	public string CallbackDescription;
	/// <summary>
	///		Index 0 is the item texture, index 1 is the equip texture.
	/// </summary>
	public readonly string[] TexturePaths;
	public readonly EffectCallback Callback;
	/// <summary>
	///		Allows for extra effects based on the gem type.
	///		Invoked during the gem effect activation.
	/// </summary>
	public delegate void EffectCallback(ref GemData data, Player player);
	private static void DefaultCallback(ref GemData data, Player player) { }
}
