using System.Collections.Generic;
using Crystallography.Core.Artifacts;

namespace Crystallography.Content.GemEffects.Diamond;

public class CumulativeCritChance : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Diamond;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<CumulativeCritChancePlayer>().Active = true;
		player.GetModPlayer<CumulativeCritChancePlayer>().Strength += data.Strength;
	}
}

public class CumulativeCritChancePlayer : ModPlayer
{
	private const float BaseEffect = 0.5f;
	
	public bool Active = false;
	public float Strength = 0f;

	private static Dictionary<int, float> _cumulativeCritChanceMap;

	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}

	public override void PostUpdateMiscEffects() {
		if (Main.myPlayer != Player.whoAmI) {
			return;
		}
		
		if (_cumulativeCritChanceMap is null) {
			_cumulativeCritChanceMap = new Dictionary<int, float>(Main.maxNPCs);
			for (int i = 0; i < Main.maxNPCs; i++) {
				_cumulativeCritChanceMap[i] = 0f;
			}
		}

		foreach (var npc in Main.ActiveNPCs) {
			if (!Active || !npc.active) {
				_cumulativeCritChanceMap[npc.whoAmI] = 0f;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
		if (!Active) {
			return;
		}
		
		float cumulativeCritChance = _cumulativeCritChanceMap[target.whoAmI];
		if (Main.rand.NextFloat(100) < cumulativeCritChance) {
			modifiers.SetCrit();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
		if (!Active) {
			return;
		}

		if (hit.Crit) {
			_cumulativeCritChanceMap[target.whoAmI] = 0f;
			return;
		} 
		
		_cumulativeCritChanceMap[target.whoAmI] += BaseEffect * Strength;
	}
}
