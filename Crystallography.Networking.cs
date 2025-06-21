using System.IO;
using Crystallography.Content.GemEffects.Sapphire;

namespace Crystallography;

public partial class Crystallography : Mod
{
	public enum MessageType : byte
	{
		SyncNoHitReduceManaCostTimerReset = 0,
	}
	
	public override void HandlePacket(BinaryReader reader, int whoAmI) {
		var packet = (MessageType)reader.ReadByte();

		switch (packet) {
			case MessageType.SyncNoHitReduceManaCostTimerReset:
				NoHitReduceManaCostPlayer.HandleNoHitTimerReset(reader, whoAmI);
				break;
			default:
				Logger.Error("Unknown packet: " + packet);
				return;
		}
	}
}
