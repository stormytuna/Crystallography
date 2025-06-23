using Crystallography.Core.Artifacts;
using Terraria.DataStructures;

namespace Crystallography.Content.GemEffects.Amethyst;

public class SentryFreshSpawnDamageIncrease : GemEffect
{
	public override EffectType Type => EffectType.Major;
	public override int GemType => ItemID.Amethyst;
	public override void Apply(Player player, GemData data) {
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Active = true;
		player.GetModPlayer<UniqueMinionsIncreaseDamagePlayer>().Strength += data.Strength;
	}
}

public class SentryFreshSpawnDamageIncreasePlayer : ModPlayer
{
	public bool Active = false;
	public float Strength = 0f;
	
	public override void ResetEffects() {
		Active = false;
		Strength = 0f;
	}
}

public class SentryFreshSpawnDamageIncreaseProjectile : GlobalProjectile
{
	private const float BaseEffect = 0.2f;
	
	private int _sentryLifeTimer = 0;
	private bool _firstFrame = true;
	
	public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) {
		return lateInstantiation && entity.sentry;
	}

	public override bool InstancePerEntity {
		get => true;
	}

	public override void PostAI(Projectile projectile) {
		if (_firstFrame) {
			_firstFrame = false;
			_sentryLifeTimer = 30 * 60;
		}
		
		_sentryLifeTimer--;
		if (_sentryLifeTimer < 0) {
			return;
		}
		
		var owner = Main.player[projectile.owner];
		var modPlayer = owner.GetModPlayer<SentryFreshSpawnDamageIncreasePlayer>();
		if (!modPlayer.Active) {
			return;
		}

		int damage = (int)owner.GetTotalDamage(projectile.DamageType).ApplyTo(projectile.originalDamage);
		float strength = BaseEffect * modPlayer.Strength;
		projectile.damage = (int)(damage * (1f + strength));
	}
}
