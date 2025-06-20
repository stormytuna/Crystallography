namespace Crystallography.Core.Artifacts;
/// <summary>
///  A data structure containing common gem properties.
/// </summary>
/// <param name="Type"> The ItemID of the gem.</param>
/// <param name="Strength"> Multiplier to any numeric value based effects.</param>
/// <param name="Effects"> Array containing this gems effects.</param>
/// <param name="Color"> Color tint for this gem.</param>
public readonly record struct GemData(int Type, float Strength, GemEffect[] Effects, Color Color) { }
