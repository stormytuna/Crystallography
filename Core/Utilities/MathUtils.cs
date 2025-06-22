using System;
using System.Runtime.CompilerServices;

namespace Crystallography.Core.Utilities;
public partial class CrystallographyUtils {
	public static Vector2 CubicLerp(Vector2 value0, Vector2 value1, Vector2 value2, Vector2 value3, float interpolant) {
		return new Vector2(CubicLerp(value0.X, value1.X, value2.X, value3.X, interpolant), CubicLerp(value0.X, value1.X, value2.X, value3.X, interpolant));
	}
	public static float CubicLerp(float v0, float v1, float v2, float v3, float t) {
		float p = (v3 - v2) - (v0 - v1);
		return t * t * t * p + t * t * ((v0 - v1) - p) + t * (v2 - v0) + v1;
	}
}
