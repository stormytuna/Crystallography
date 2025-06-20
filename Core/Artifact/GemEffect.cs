namespace Crystallography.Core.Artifacts;
// this sucks. please learn EC 
public abstract class GemEffect : ModType{
	public enum EffectType : byte {
		Major = 0 << 1,
		Minor = 1 << 1,
		Generic = 2 << 1
	}
	public abstract GemEffect.EffectType Type { get; }
	public abstract int GemType { get; }
	protected override void Register() {
		ModTypeLookup<GemEffect>.Register(this);
		switch (Type) {
			case EffectType.Major:
				GemTypeLoader.MajorEffects[GemType].Add(this);
				break;
			case EffectType.Minor:
				GemTypeLoader.MinorEffects[GemType].Add(this);
				break;
			case EffectType.Generic:
				GemTypeLoader.GenericEffects.Add(this);
				break;
		}
	}
	public abstract void Apply(Player player);
}
