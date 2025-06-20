namespace Crystallography.Core.Artifacts;

public record class Gem(int Type, int Size, float Purity, GemEffect[] Effects, Color Color) {
}
