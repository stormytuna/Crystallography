using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Amber;

public class NoHitIncreaseDamageReduction : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amber;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<NoHitIncreaseDamageReductionPlayer>().Active = true;
		player.GetModPlayer<NoHitIncreaseDamageReductionPlayer>().Strength += data.Strength;
	}
	
	public static int GetEffectStrength(Player player, float strength) {
		return (int)(NoHitIncreaseDamageReductionPlayer.NoHitMaxEffect * strength);	
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(GetEffectStrength(Main.LocalPlayer, strength));
	}
}

public class NoHitIncreaseDamageReductionPlayer : ModPlayer
{
	private const float NoHitCounterForMinEffect = 10 * 60;
	private const float NoHitCounterForMaxEffect = 20 * 60;
	private const float NoHitMinEffect = 0f;
	
	public const float NoHitMaxEffect = 0.5f;
	
	public bool Active = false;
	public float Strength = 0f;
	
	private int _noHitCounter = 0;
	
	public override void ResetEffects() {
		if (!Active) {
			_noHitCounter = 0;
		}
		
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		if (!Active) {
			return;
		}
		
		_noHitCounter++;
	}

	public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
		if (!Active || _noHitCounter < NoHitCounterForMinEffect) {
			return;
		}
		
		float damageReduction = Utils.Remap(_noHitCounter, NoHitCounterForMinEffect, NoHitCounterForMaxEffect, NoHitMinEffect, NoHitMaxEffect);
		Player.endurance += damageReduction * Strength;
	}

	public override void OnHurt(Player.HurtInfo info) {
		if (!Active) {
			return;
		}
		
		_noHitCounter = 0;
	}
}
