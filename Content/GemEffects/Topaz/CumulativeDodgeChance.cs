using Crystallography.Content.GemEffects.Sapphire;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Topaz;

public class CumulativeDodgeChance : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Topaz;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<CumulativeDodgeChancePlayer>().Active = true;
		player.GetModPlayer<CumulativeDodgeChancePlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(CumulativeDodgeChancePlayer.BaseEffect * strength);
	}
}

public class CumulativeDodgeChancePlayer : ModPlayer
{
	public const float BaseEffect = 0.05f;
	
	public bool Active = false;
	public float Strength = 0f;

	private float _dodgeChance = 0f;
	
	public override void ResetEffects() {
		if (!Active) {
			_dodgeChance = 0f;
		}
		
		Active = false;
		Strength = 0f;
	}

	public override bool ConsumableDodge(Player.HurtInfo info) {
		if (!Active || Player.whoAmI != Main.myPlayer) {
			return base.ConsumableDodge(info);
		}

		if (Main.rand.NextFloat() < _dodgeChance) {
			_dodgeChance = 0f;
			Player.ApplyStandardImmuneTime();
			return true;
		}
		
		_dodgeChance += BaseEffect * Strength;
		return false;
	}
}
