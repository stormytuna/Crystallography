using System.Collections.Generic;
using Terraria.Localization;
using Terraria.WorldBuilding;

namespace Crystallography.Content.WorldGeneration;
public class PassesSystem : ModSystem {
	public static string GenMessage => Language.GetTextValue("Mods.Crystallography.WorldGenMessage");
	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
		int currentIndex = tasks.FindIndex(p => p.Name == "Shinies");
		currentIndex++;
		tasks.Insert(currentIndex, new StandardGeodePass((int)totalWeight / 100));
		totalWeight += totalWeight / 100;
	}
}
