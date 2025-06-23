using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Sapphire;

public class ManaStarReduceManaCost : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<ManaStarReduceManaCostPlayer>().Active = true;
		player.GetModPlayer<ManaStarReduceManaCostPlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(ManaStarReduceManaCostPlayer.ManaCostReduction * strength);
	}
}

public class ManaStarReduceManaCostPlayer : ModPlayer
{
	private const int ManaCostReductionTimerMax = 2 * 60;
	private const int ManaCostReductionCooldownMax = 5 * 60;
	public const float ManaCostReduction = 0.4f;
	
	public bool Active;
	public float Strength;

	public int _manaCostReductionTimer = 0;
	public int _manaCostCooldownTimer = 0;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		_manaCostReductionTimer--;
		_manaCostCooldownTimer--;

		if (_manaCostReductionTimer == 0) {
			_manaCostCooldownTimer = ManaCostReductionCooldownMax;
		}
	}

	public override bool OnPickup(Item item) {
		if (!Active || !item.IsManaPickup()) {
			return base.OnPickup(item);
		}

		if (_manaCostReductionTimer <= 0 && _manaCostCooldownTimer <= 0) {
			_manaCostReductionTimer = ManaCostReductionTimerMax;
		}
		
		return base.OnPickup(item);
	}

	public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
		if (!Active || _manaCostReductionTimer <= 0) {
			return;
		}
		
		reduce -= ManaCostReduction * Strength;
	}
}
