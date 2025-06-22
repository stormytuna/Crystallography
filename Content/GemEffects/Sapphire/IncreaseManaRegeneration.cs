using Crystallography.Core.Artifacts;
using Crystallography.Content.Items;
namespace Crystallography.Content.GemEffects.Sapphire;

public class IncreaseManaRegeneration : GemEffect
{
	public override EffectType Type => EffectType.Minor;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<ManaRegenerationPlayer>().ManaRegenBoost += 0.2f * data.Strength;
	}
}

public class ManaRegenerationPlayer : ModPlayer
{
	public float ManaRegenBoost = 0f;
	
	public override void ResetEffects() {
		ManaRegenBoost = 0f;
	}

	public override void Load() {
		On_Player.UpdateManaRegen += static (orig, self) => {
			orig(self);

			float manaRegenBoost = self.GetModPlayer<ManaRegenerationPlayer>().ManaRegenBoost;
			if (manaRegenBoost > 0f) {
				self.manaRegen += (int)(self.manaRegen * manaRegenBoost);
			}
		};
	}
}
