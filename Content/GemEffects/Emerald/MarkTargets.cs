using Crystallography.Core.Artifacts;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Crystallography.Content.GemEffects.Emerald;

public class MarkTargets : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Emerald;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<MarkTargetsPlayer>().Active = true;
		player.GetModPlayer<MarkTargetsPlayer>().Strength += data.Strength;
	}
}

public class MarkTargetsPlayer : ModPlayer
{
	private const int MarkInactivityTimeMax = 8 * 60;
	private const float MaxMarkRange = 100f * 16f;
	private const float ExtraDamageAgainstMark = 0.2f;
	
	public bool Active = false;
	public float Strength = 0f;
	public int CurrentMarkWhoAmI = Main.maxNPCs;
	
	private int _markInactivityTimer = 0;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		if (!Active) {
			return;
		}
		
		_markInactivityTimer--;
		var mark = Main.npc[CurrentMarkWhoAmI];
		if ((!mark.active || _markInactivityTimer <= 0) && Player.whoAmI == Main.myPlayer) {
			ChooseMark();	
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active || target.whoAmI != CurrentMarkWhoAmI || !modifiers.DamageType.CountsAsClass(DamageClass.Ranged)) {
			return;
		}

		modifiers.FinalDamage += ExtraDamageAgainstMark * Strength;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active || target.whoAmI != CurrentMarkWhoAmI) {
			return;
		}
		
		_markInactivityTimer = MarkInactivityTimeMax;
	}

	private void ChooseMark() {
		if (!Active) {
			return;
		}
		
		var newMark = NPCHelpers.FindRandomNearbyNPC(MaxMarkRange, Player.Center, false, [CurrentMarkWhoAmI]);
		if (newMark is null) {
			return;
		}
		
		CurrentMarkWhoAmI = newMark.whoAmI;
		_markInactivityTimer = MarkInactivityTimeMax;
	}
}

public class DrawMark : GlobalNPC
{
	public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
		var player = Main.LocalPlayer;
		var markPlayer = player.GetModPlayer<MarkTargetsPlayer>();
		if (!markPlayer.Active || markPlayer.CurrentMarkWhoAmI != npc.whoAmI) {
			return;
		}

		Main.instance.LoadItem(ItemID.MagicMirror);
		var texture = TextureAssets.Item[ItemID.MagicMirror];
		var drawData = new DrawData {
			texture = texture.Value,
			position = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY),
			sourceRect = texture.Frame(),
			origin = texture.Size() / 2f,
			color = Color.White,
			rotation = ((float)Main.timeForVisualEffects * 0.1f) % Consts.TwoPi,
			scale = new Vector2(1f),
		};
		Main.EntitySpriteDraw(drawData);
	}
}
