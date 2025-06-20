namespace Crystallography.Core.Artifacts;
/// <summary>
///  A data structure containing common gem properties.
/// </summary>
/// <param name="Type"> The ItemID of the gem. </param>
/// <param name="Strength"></param>
/// <param name="Effects"></param>
/// <param name="Color"></param>
public readonly record struct GemData(int Type, float Strength, GemEffect[] Effects, Color Color) { }
