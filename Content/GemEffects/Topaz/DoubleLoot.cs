using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Topaz;

public class DoubleLoot : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Topaz;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<DoubleLootPlayer>().Active = true;
		player.GetModPlayer<DoubleLootPlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(DoubleLootPlayer.BaseEffect * strength);
	}
}

public class DoubleLootPlayer : ModPlayer
{
	public const float BaseEffect = 0.1f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void Load() {
		On_NPC.NPCLoot_DropItems += static (orig, self, closestPlayer) => {
			orig(self, closestPlayer);

			float bonusLoot = 0f;
			foreach (var player in Main.ActivePlayers) {
				var modPlayer = player.GetModPlayer<DoubleLootPlayer>();
				if (modPlayer.Active) {
					bonusLoot += modPlayer.Strength * BaseEffect;
				}
			}
			
			while (bonusLoot > 0f) {
				float rollChance = float.Clamp(bonusLoot, 0f, 1f);
				if (Main.rand.NextFloat() < rollChance) {
					orig(self, closestPlayer);
				}
				
				bonusLoot -= 1f;
			}
		};
	}
}
