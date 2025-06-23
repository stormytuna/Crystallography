using Crystallography.Core.Artifacts;
using MonoMod.Cil;

namespace Crystallography.Content.GemEffects.Amber;

public class HeartsRestoreMore : GemEffect
{
	public override void Load() {
		IL_Player.PickupItem += (il) => {
			var cursor = new ILCursor(il);
			
			cursor.GotoNext(MoveType.Before,
				i => i.MatchLdarg0(),
				i => i.MatchLdcI4(20),
				i => i.MatchCall<Player>(nameof(Player.Heal)));
			
			cursor.Index += 2;
			cursor.EmitPop();
			cursor.EmitLdarg0();
			cursor.EmitDelegate((Player player) => {
				var modPlayer = player.GetModPlayer<HeartsRestoreMorePlayer>();
				if (!modPlayer.Active) {
					return 20;
				}

				float mult = 1f + (modPlayer.Strength * 0.5f);
				return (int)(20 * mult);
			});
		};
	}

	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<HeartsRestoreMorePlayer>().Active = true;
		player.GetModPlayer<HeartsRestoreMorePlayer>().Strength += data.Strength;
	}
}

public class HeartsRestoreMorePlayer : ModPlayer
{
	public bool Active = false;
	public float Strength = 0f;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}
}
