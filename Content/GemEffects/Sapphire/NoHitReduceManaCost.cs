using System.IO;
using Crystallography.Core.Artifacts;
using Terraria.Localization;

namespace Crystallography.Content.GemEffects.Sapphire;

public class NoHitReduceManaCost : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Sapphire;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<NoHitReduceManaCostPlayer>().Active = true;
		player.GetModPlayer<NoHitReduceManaCostPlayer>().Strength += data.Strength;
	}
	
	public override LocalizedText GetFormattedTooltip(float strength) {
		return Tooltip.WithFormatArgs(NoHitReduceManaCostPlayer.NoHitMaxEffect * strength);
	}
}

public class NoHitReduceManaCostPlayer : ModPlayer
{
	private const float NoHitCounterForMinEffect = 6 * 60;
	private const float NoHitCounterForMaxEffect = 20 * 60;
	private const float NoHitMinEffect = 0f;
	public const float NoHitMaxEffect = 0.4f;
	
	public bool Active;
	public float Strength;

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

	public override void ModifyManaCost(Item item, ref float reduce, ref float mult) {
		if (!Active || _noHitCounter < NoHitCounterForMinEffect) {
			return;
		}
		
		float manaReduction = Utils.Remap(_noHitCounter, NoHitCounterForMinEffect, NoHitCounterForMaxEffect, NoHitMinEffect, NoHitMaxEffect);
		manaReduction *= Strength;
		reduce -= manaReduction;
	}

	public override void OnHurt(Player.HurtInfo info) {
		if (!Active) {
			return;
		}
		
		_noHitCounter = 0;
		if (Main.netMode != NetmodeID.SinglePlayer) {
			BroadcastNoHitTimerReset(Main.myPlayer, Main.myPlayer);
		}
	}

	public static void HandleNoHitTimerReset(BinaryReader reader, int whoAmI) {
		int player = reader.Read7BitEncodedInt();
		
		Main.player[player].GetModPlayer<NoHitReduceManaCostPlayer>()._noHitCounter = 0;

		if (Main.netMode == NetmodeID.Server) {
			BroadcastNoHitTimerReset(whoAmI, player);	
		}
	}

	public static void BroadcastNoHitTimerReset(int whoAmI, int player) {
		var packet = ModContent.GetInstance<Crystallography>().GetPacket();
		packet.Write((byte)Crystallography.MessageType.SyncNoHitReduceManaCostTimerReset);
		packet.Write7BitEncodedInt(player);
		packet.Send(-1, whoAmI);
	}
}
