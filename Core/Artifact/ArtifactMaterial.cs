using ReLogic.Content;

namespace Crystallography.Core.Artifacts;

public class ArtifactMaterial(string name, int type, float amplifier, ArtifactMaterial.EffectCallback? callback, string equipTexturePath) : ModType
{
	#region Fields
	/// <summary>
	///		Used for determining jewelry name based on material type.
	/// </summary>
	public readonly new string Name = name;
	/// <summary>
	///		The ItemID of the material itself.
	/// </summary>
	public readonly int Type = type;
	/// <summary>
	///		Multiplier for effect strength.
	/// </summary>
	public float Amplifier = amplifier;
	public Asset<Texture2D> equipTexture { get; private set; }
	public readonly EffectCallback Callback = callback;
	#endregion
	protected sealed override void Register() {
		ModTypeLookup<ArtifactMaterial>.Register(this);
		equipTexture = ModContent.Request<Texture2D>(equipTexturePath);
	}
	public sealed override void SetupContent() {
		SetStaticDefaults();
	}
	/// <summary>
	///		Allows for extra effects based on the gem type.
	///		Invoked during the gem effect activation.
	/// </summary>
	/// <param name="type">The gem item ID.</param>
	public delegate void EffectCallback(int type, Player player);
}
