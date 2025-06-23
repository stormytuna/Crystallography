using Crystallography.Content.Items;
using Crystallography.Core.Artifacts;

namespace Crystallography;

public partial class Crystallography : Mod
{
		
}

public class TestCommand : ModCommand
{
	public override void Action(CommandCaller caller, string input, string[] args) {
		var gem = new Item(ModContent.ItemType<Gem>());
		var gemgem = gem.ModItem as Gem;

		GemEffect[] effects = [
			GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Major),
			GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Minor),
			GemTypeLoader.GetRandomEffect(ItemID.Sapphire, GemEffect.EffectType.Generic),
		];
		gemgem.Data = new GemData(ItemID.Sapphire, Main.rand.NextFloat(0.8f, 1.5f), effects, Color.White);
		
		Main.LocalPlayer.QuickSpawnItem(null, gem);
	}

	public override string Command => "spawngem";
	public override CommandType Type { get => CommandType.Chat; }
}
