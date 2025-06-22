using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Diamond;

public class RecoveryItemOnCrit : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<RecoveryItemOnCritPlayer>().Active = true;
		player.GetModPlayer<RecoveryItemOnCritPlayer>().Strength += data.Strength;
	}
}

public class RecoveryItemOnCritPlayer : ModPlayer
{
	private const int RecoveryItemCooldownMax = 5 * 60;
	private const int RecoveryItemCooldownReduction = 30;
	
	public bool Active = false;
	public float Strength = 1f;

	private int _recoveryItemSpawnCooldown = 0;

	public override void ResetEffects() {
		Active = false;
		Strength = 1f;
	}

	public override void PostUpdateMiscEffects() {
		if (!Active) {
			return;
		}

		_recoveryItemSpawnCooldown--;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active || !hit.Crit || _recoveryItemSpawnCooldown > 0) {
			return;
		}

		Item.NewItem(Player.GetSource_OnHit(target), target.position, target.Size, ModContent.ItemType<RecoveryItem>());
		
		int reduction = (int)(RecoveryItemCooldownReduction * Strength);
		Main.NewText(reduction);
		Main.NewText(RecoveryItemCooldownMax);
		_recoveryItemSpawnCooldown = RecoveryItemCooldownMax - reduction;
		Main.NewText(_recoveryItemSpawnCooldown);
	}
}

public class RecoveryItem : ModItem
{
	public override string Texture {
		get => "Terraria/Images/Item_50";
	}

	private const int HealthAmount = 10;
	private const int ManaAmount = 10;
	
	public override void SetStaticDefaults() {
		ItemID.Sets.ItemsThatShouldNotBeInInventory[Type] = true;
		ItemID.Sets.IgnoresEncumberingStone[Type] = true;
		ItemID.Sets.IsAPickup[Type] = true;
		ItemID.Sets.ItemNoGravity[Type] = true;	
	}

	public override void SetDefaults() {
		Item.width = Item.height = 12;
	}

	public override bool OnPickup(Player player) {
		player.Heal(HealthAmount);
		player.statMana += ManaAmount;	
		player.ManaEffect(ManaAmount);
		
		Item.TurnToAir();
		
		return false;
	}

	public override Color? GetAlpha(Color lightColor) {
		return Color.White;
	}
}
