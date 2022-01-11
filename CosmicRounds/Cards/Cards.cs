using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CR.MonoBehaviors;


namespace CR.Cards
{
	public class BeetleCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			data.maxHealth *= 1.30f;
			characterStats.movementSpeed *= 0.70f;
			BeetleMono beetleMono = player.gameObject.AddComponent<BeetleMono>();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Beetle";
		}

		protected override string GetDescription()
		{
			return "Gain a beetle shell that regenerates your health until you reach max.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Beetle");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Regen",
					amount = "+10%",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}


		public AssetBundle Asset;

	};

	public class CrowCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity -= (gun.gravity * 0.7f);
			gun.projectileSpeed *= 0.65f;
			if (gun.size <= 0f)
			{
				gun.size += 0.30f;
			}
			else
			{
				gun.size *= 1.30f;
			}
			gun.numberOfProjectiles += 6;
			gun.spread += 0.15f;
			gun.damage *= 0.3f;
			gun.projectileColor += Color.magenta;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Crow";
		}

		protected override string GetDescription()
		{
			return "Launch a 'murder' of bullets to pelt targets.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Crow");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-70%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Size",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+6",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bullet Speed",
					amount = "-35%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-70%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bullet Spread",
					amount = "+15%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		public override string GetModName()
		{
			return "CR";
		}

		public AssetBundle Asset;
	};

	public class HawkCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileSpeed *= 3f;
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.numberOfProjectiles = 0;
			gun.bursts = 0;
			gun.timeBetweenBullets = 0;
			gun.damage *= 1.25f;
			gun.reloadTime *= 1.25f;
			gun.attackSpeed *= 1.75f;
			gun.projectileColor += Color.yellow;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Hawk";
		}

		protected override string GetDescription()
		{
			return "Fire a high velocity bullet!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Hawk");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+200%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Shots",
					amount = "Focused",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "ATK Speed",
					amount = "-75%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class SpeedUpCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTime *= 0.75f;
			gun.attackSpeed *= 0.75f;
			characterStats.movementSpeed *= 1.25f;
			block.cooldown *= 0.75f;
			characterStats.health *= 0.75f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Speed Up";
		}

		protected override string GetDescription()
		{
			return "Come on, step it up!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SpeedUp");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Health",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class MosquitoCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += Color.red;
			gun.damage *= 1.20f;
			gun.projectielSimulatonSpeed *= 1.5f;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.25f;
			}
			else
			{
				characterStats.lifeSteal *= 1.25f;
			}
			gunAmmo.reloadTime *= 1.35f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Mosquito", new Type[]
					{
						typeof(MosquitoMono)
					})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Mosquito";
		}

		protected override string GetDescription()
		{
			return "Bullets deal damage over 4 bursts.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Mosquito");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Steal",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+35%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}

	}

	public class SuperSonicCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTime /= 3f;
			gun.attackSpeed /= 3f;
			block.cooldown /= 3f;
			characterStats.movementSpeed *= 1.66f;
			gun.projectielSimulatonSpeed *= 1.66f;
			characterStats.health *= 0.1f;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Super Sonic";
		}

		protected override string GetDescription()
		{
			return "You're too slow!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SuperSonic");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-66%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+66%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+66%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+66%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-66%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Health",
					amount = "-90%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class StasisCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity = 0f;
			gun.projectielSimulatonSpeed *= 0.70f;
			gun.damage *= 1.30f;
			gun.projectileColor += Color.cyan;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Stasis";
		}

		protected override string GetDescription()
		{
			return "Bullets float through the air!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Stasis");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-100%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}


	}

	public class OnesKingCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity *= 1.50f;
			gun.projectielSimulatonSpeed *= 0.5f;
			data.maxHealth *= 1.50f;
			gravity.gravityForce /= 2f;
			characterStats.jump *= 1.5f;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.30f;
			}
			else
			{
				characterStats.lifeSteal *= 1.3f;
			}
			gun.damage *= 1.50f;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Ones King";
		}

		protected override string GetDescription()
		{
			return "Shoutout to Wunzking!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_OnesKing");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Gravity",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Steal",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	};

	public class BulletTimeCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 0.70f;
			gunAmmo.reloadTime *= 0.7f;
			gun.damageAfterDistanceMultiplier *= 1.5f;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Bullet Time";
		}

		protected override string GetDescription()
		{
			return "Bullets move slower!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_BulletTIme");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}


	}

	public class StunCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.maxAmmo -= 2;
			gun.projectileColor += Color.yellow;
			gun.reloadTime *= 0.65f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Shock", new Type[]
				{
					typeof(ShockMono)
				})
			});

			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Stun", new Type[]
				{
					typeof(StunMono)
				})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Stun";
		}

		protected override string GetDescription()
		{
			return "Bullets cause devastating stun and shock effects!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Stun");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-35%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},

				new CardInfoStat
				{
					positive = false,
					stat = "Ammo",
					amount = "-2",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},

				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "4s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}

			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class FearFactorCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			FearFactorMono fearMono = player.gameObject.AddComponent<FearFactorMono>();
			block.cooldown *= 0.80f;
			data.maxHealth *= 1.30f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Fear Factor";
		}

		protected override string GetDescription()
		{
			return "Sporadically shoot bullets when an enemy bullet is near.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_FearFactor");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-20%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "2s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}

	}

	public class StarCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.numberOfProjectiles += 4;
			gun.bursts = 0;
			gun.timeBetweenBullets = 0;
			gunAmmo.maxAmmo += 7;
			gun.damage *= 0.25f;
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.damageAfterDistanceMultiplier *= 1.2f;
			gunAmmo.reloadTime += 0.25f;
			if (gun.size <= 0f)
			{
				gun.size += 0.30f;
			}
			else
			{
				gun.size *= 1.30f;
			}

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Star";
		}

		protected override string GetDescription()
		{
			return "Overlaps multiple bullets into one super bullet!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Star");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+4",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+7",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-75%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}

	}
	public class CriticalHitCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
	
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTime *= 0.75f;
			gun.damage *= 0.75f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Critical", new Type[]
					{
						typeof(CriticalHitMono)
					})
			});
			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Critical Hit";
		}

		protected override string GetDescription()
		{
			return "Bullets have a 50% chance to deal double damage, targets glow yellow when this happens.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_CriticalHit");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class FlamethrowerCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.numberOfProjectiles += 1;
			gun.spread += 0.1f;
			gun.knockback = 0f;
			gun.damage *= 0.3f;
			gun.projectileSpeed *= 0.75f;
			gun.gravity /= 1.5f;
			gun.projectielSimulatonSpeed *= 0.75f;
			gunAmmo.reloadTime *= 0.7f;
			gunAmmo.maxAmmo += 10;
			gun.attackSpeed *= 0.25f;
			gun.projectileColor = new Color(1f, 0.3f, 0f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Flamethrower", new Type[]
					{
						typeof(FlamethrowerMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Flamethrower";
		}

		protected override string GetDescription()
		{
			return "Absolutely incinerate anyone in front of you in a blaze!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Flamethrower");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet",
					amount = "+1",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+75%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+10",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-70%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}



	}

	public class SyphonCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTime += 0.25f;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.25f;
			}
			else
			{
				characterStats.lifeSteal *= 1.25f;
			}
			gun.projectileColor = new Color(1f, 0f, 0.4f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Syphon", new Type[]
					{
						typeof(SyphonMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();


		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Syphon";
		}

		protected override string GetDescription()
		{
			return "Bullets have a 50% chance to Silence targets.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Syphon");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Life Steal",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class DropshotCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(1f, 0.7f, 0f, 1f);
			gun.gravity *= 1.35f;
			gun.projectielSimulatonSpeed *= 1.5f;
			gun.damage *= 1.30f;
			gun.reflects += 3;
			DropMono dropmono = player.gameObject.AddComponent<DropMono>();
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(
				objectsToSpawn
			);
			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Dropshot";
		}

		protected override string GetDescription()
		{
			return "Bullets collide with players and ground more powerfully!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Dropshot");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+3",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bullet Gravity",
					amount = "+35%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.NatureBrown;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class ReconCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileSpeed *= 5f;
			gun.damageAfterDistanceMultiplier *= 1.5f;
			gun.attackSpeed *= 2f;
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.numberOfProjectiles = 0;
			gun.bursts = 0;
			gun.timeBetweenBullets = 0;
			gun.projectileColor = new Color(1f, 1f, 0.1f, 1f);
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Recon";
		}

		protected override string GetDescription()
		{
			return "Fire a REALLY high velocity bullet!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Recon");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+400%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Shots",
					amount = "Focused",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "ATK SPD",
					amount = "-200%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class TaserCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			TaserMono taserMono = player.gameObject.AddComponent<TaserMono>();
			block.objectsToSpawn.Add(TaserMono.taserVisual);
			characterStats.movementSpeed *= 1.20f;
			block.cooldown *= 1.2f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Taser";
		}

		protected override string GetDescription()
		{
			return "Blocking Stuns nearby targets, immobilizing them.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Taser");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "2.5s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class HolsterCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			HolsterMono holsterMono = player.gameObject.AddComponent<HolsterMono>();
			block.cooldown *= 0.8f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Holster";
		}

		protected override string GetDescription()
		{
			return "Blocking fires your gun with +50% damage!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Holster");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-20%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class FlexCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			FlexMono flexMono = player.gameObject.AddComponent<FlexMono>();
			block.cooldown *= 0.7f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Flex";
		}

		protected override string GetDescription()
		{
			return "Damaging enemies makes you do an extra block.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Flex");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "3s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class DroneCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity *= 0.5f;
			gun.projectileColor += new Color(0.5f, 0.5f, 0.5f, 1f);
			gun.damage *= 0.35f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Drone", new Type[]
					{
						typeof(DroneMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Drone";
		}

		protected override string GetDescription()
		{
			return "Infusing high intellect AI into your bullets allows them to adjust trajectory to nearby targets on the fly!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Drone");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-65%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class SparkCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed += 5f;
			gun.spread += 0.2f;
			gun.attackSpeed *= 0.25f;
			gunAmmo.maxAmmo += 10;
			gun.damage *= 0.30f;
			gun.knockback *= 0.1f;
			gunAmmo.reloadTime *= 0.8f;
			gun.projectileColor = Color.yellow;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Spark", new Type[]
					{
						typeof(StunMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Spark";
		}

		protected override string GetDescription()
		{
			return "Bullets become fast lightning sparks that can stun targets for a bit!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Spark");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+75%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},

				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+10",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},

				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-20%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},

				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-70%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},

				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "4s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}

			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class GoldenGlazeCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			
			gun.projectileColor = new Color(0.8f, 1f, 0f, 1f);
			gun.attackSpeed *= 0.5f;
			gun.spread = 0f;
			gun.damage *= 0.75f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Golden", new Type[]
					{
						typeof(GoldenGlazeMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Golden Glaze";
		}

		protected override string GetDescription()
		{
			return "Bullets slow targets and reduce current hp by 20% for 2 seconds.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_GoldenGlaze");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+100%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Shots",
					amount = "Focused",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class FocusCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		
			gun.spread = 0f;
			data.maxHealth *= 1.30f;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Focus";
		}

		protected override string GetDescription()
		{
			return "Focused Shots reset your bullet spread to keep you precise.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Focus");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Shots",
					amount = "Focused",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.NatureBrown;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class SugarGlazeCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
	
			gun.projectileColor += new Color(0.8f, 0f, 1f, 1f);
			gun.attackSpeed *= 0.5f;
			gun.spread = 0f;
			gun.damage *= 1.3f;
			gunAmmo.reloadTime *= 1.25f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Sugar", new Type[]
					{
						typeof(SugarGlazeMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Sugar Glaze";
		}

		protected override string GetDescription()
		{
			return "Bullets make targets jump and makes them faster for 3 seconds.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SugarGlaze");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+100%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Shots",
					amount = "Focused",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class MitosisCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			MitosisMono mitosisMono = player.gameObject.AddComponent<MitosisMono>();
			block.cooldown *= 1.2f;
			gunAmmo.maxAmmo += 1;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
	
		}

		protected override string GetTitle()
		{
			return "Mitosis";
		}

		protected override string GetDescription()
		{
			return "Blocking adds +1 Bullet and +25% Bullet Speed next shot.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Mitosis");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+1"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.PoisonGreen;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class MeiosisCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			MeiosisMono mitosisMono = player.gameObject.AddComponent<MeiosisMono>();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Meiosis";
		}

		protected override string GetDescription()
		{
			return "While reloading you gain:";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Meiosis");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Blocks",
					amount = "+2",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DefensiveBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class PogoCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			PogoMono pogoMono = player.gameObject.AddComponent<PogoMono>();
			data.maxHealth *= 1.8f;
			block.cooldown *= 0.5f;
			gunAmmo.reloadTime *= 0.5f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Pogo";
		}

		protected override string GetDescription()
		{
			return "You jump every second, even if you are in mid-air! (Weeeee!)";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pogo");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+80%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class AllCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			data.maxHealth *= 1.07f;
			block.cooldown *= 0.93f;
			block.additionalBlocks += 1;
			gunAmmo.reloadTime *= 0.93f;
			characterStats.movementSpeed *= 1.07f;
			gun.gravity *= 0.93f;
			gun.projectileSpeed *= 1.07f;
			gun.projectielSimulatonSpeed *= 1.07f;
			if (gun.size <= 0f)
			{
				gun.size += 0.07f;
			}
			else
			{
				gun.size *= 1.07f;
			}
			gun.numberOfProjectiles += 1;
			gunAmmo.maxAmmo += 1;
			gun.spread *= 0.9f;
			gun.evenSpread *= 0.9f;
			gun.damage *= 1.07f;
			gun.damageAfterDistanceMultiplier *= 1.07f;
			gun.dmgMOnBounce *= 1.07f;
			if (gun.slow <= 0f)
			{
				gun.slow += 0.10f;
			}
			else
			{
				gun.slow *= 1.07f;
			}
			gun.attackSpeed *= 0.93f;
			gun.knockback *= 1.07f;
			gun.destroyBulletAfter *= 1.1f;
			gravity.gravityForce *= 0.9f;
			gun.reflects += 1;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.07f;
			}
			else
			{
				characterStats.lifeSteal *= 1.07f;
			};
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(
				objectsToSpawn
			);
			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "ALL";
		}

		protected override string GetDescription()
		{
			return "Ups stats that benefit from being higher, lowers stats the benefit from being lower.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_All");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Positive Stats",
					amount = "+7%",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Negative Stats",
					amount = "-7%",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet",
					amount = "+1",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+1",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounce",
					amount = "+1",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Blocks",
					amount = "+1",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class CloudCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.7f, 0.8f, 1f, 1f);
			gun.damage *= 1.30f;
			gun.gravity *= 0.7f;
			gunAmmo.reloadTime *= 0.3f;
			gun.projectileSpeed *= 1.3f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Cloud", new Type[]
					{
						typeof(CloudMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
	
		}

		protected override string GetTitle()
		{
			return "Cloud";
		}

		protected override string GetDescription()
		{
			return "Bullets slow down and drift downwards slowly.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Cloud");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class PulseCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(1f, 0.8f, 0.7f, 1f);
			gun.damage *= 1.3f;
			gunAmmo.reloadTime *= 1.25f;
			gun.reflects += 2;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Pulse", new Type[]
					{
						typeof(PulseMono)
					})
			});
			ObjectsToSpawn objectsToSpawn2 = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(
				objectsToSpawn2
			);
			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Pulse";
		}

		protected override string GetDescription()
		{
			return "Bullets periodically slow down and speed up.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pulse");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class DriveCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(0.7f, 1f, 0.7f, 1f);
			gun.damage *= 0.50f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Drive", new Type[]
					{
						typeof(DriveMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Drive";
		}

		protected override string GetDescription()
		{
			return "Bullets try to redirect to where a target is once after you shoot them.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Drive");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class SunCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.8f, 0.1f, 1f);
			gun.damage *= 1.2f;
			gun.projectileSpeed *= 1.5f;
			gun.reflects += 5;
			gunAmmo.reloadTime *= 1.25f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Sun", new Type[]
				{
					typeof(SunMono)
				})
			});
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Explosive bullet")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = new Vector3(1000f, 0f, 0f);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.name = "Explosion";
			UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<RemoveAfterSeconds>());
			gameObject.GetComponent<Explosion>().force = 5000f;
			list.Add
			(
				new ObjectsToSpawn
				{
					effect = gameObject,
					normalOffset = 0.1f,
					numberOfSpawns = 1,
					scaleFromDamage = 0.5f,
					scaleStackM = 0.7f,
					scaleStacks = true
				}
			);
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add
			(
				objectsToSpawn
			);
			gun.objectsToSpawn = list.ToArray();

			if (gun.size <= 0f)
			{
				gun.size += 0.50f;
			}
			else
			{
				gun.size *= 1.50f;
			}

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Sun";
		}

		protected override string GetDescription()
		{
			return "Bullets become explosive sun bullets and are redirected towards the center of the screen.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Sun");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Size",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+50%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+5"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.NatureBrown;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class CometCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0f, 0f, 1f, 1f);
			gun.damage *= 1.3f;
			if (gun.slow > 0)
            {
				gun.slow *= 1.1f;
            }

			else
            {
				gun.slow += 1f;
			};

			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Comet", new Type[]
					{
						typeof(CometMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTime += 0.5f;

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Comet";
		}

		protected override string GetDescription()
		{
			return "Bullets accelerate horizontally and slow targets when hitting.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Comet");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+10%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.5s"
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.ColdBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class MeteorCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.1f, 1f, 0.3f, 1f);
			gun.damage *= 1.3f;
			gun.reloadTime *= 1.25f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Meteor", new Type[]
					{
						typeof(MeteorMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Meteor";
		}

		protected override string GetDescription()
		{
			return "Bullets accelerate when falling and deal 25% additional damage after hitting.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Meteor");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.PoisonGreen;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class UnicornCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			UnicornMono pogoMono = player.gameObject.AddComponent<UnicornMono>();
			gun.projectielSimulatonSpeed *= 0.7f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Unicorn";
		}

		protected override string GetDescription()
		{
			return "Rainbows and Unicorns! Change colors every 5s, gain unique effects with each color!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Unicorn");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class GravityCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			GravityMono gravityMono = player.gameObject.AddComponent<GravityMono>();
			block.objectsToSpawn.Add(GravityMono.gravityVisual);
			characterStats.movementSpeed *= 1.20f;
			block.cooldown += 0.5f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Gravity";
		}

		protected override string GetDescription()
		{
			return "Blocking slams nearby targets towards the closest barriers, and damages them!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Gravity");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.5s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "0.1s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class IgniteCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
	
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			IgniteMono igniteMono = player.gameObject.AddComponent<IgniteMono>();
			block.objectsToSpawn.Add(IgniteMono.igniteVisual);
			characterStats.movementSpeed *= 0.85f;
			gun.damage *= 1.3f;
			gun.projectileColor = new Color(1f, 0.3f, 0f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Flamethrower", new Type[]
					{
						typeof(FlamethrowerMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Ignite";
		}

		protected override string GetDescription()
		{
			return "Bullets and Blocking ignite targets for 2 seconds, igniting through blocking deals 20% of targets' max hp as damage.";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Ignite");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-25%",
					simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "0.5s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class FaeEmbersCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			FaeEmbersMono faeMono = player.gameObject.AddComponent<FaeEmbersMono>();
			characterStats.movementSpeed *= 1.2f;
			gunAmmo.reloadTime *= 1.5f;
			data.maxHealth *= 1.3f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Fae Embers";
		}

		protected override string GetDescription()
		{
			return "Emit small, scorching fae embers while reloading!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_FaeEmbers");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+50%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class CareenCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
	
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.2f, 0.7f, 1f, 1f);
			gun.damage *= 1.40f;
			gun.gravity *= 0.7f;
			gunAmmo.reloadTime += 0.25f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Careen", new Type[]
					{
						typeof(CareenMono)
					})
			});

			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Careen";
		}

		protected override string GetDescription()
		{
			return "Your Bullets hardly fall!";
		}

		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Careen");
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+40%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s"
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.ColdBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class AsteroidCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.3f, 1f, 1f);
			gun.damage *= 1.3f;
			gun.reloadTime *= 1.3f;
			gun.reflects += 2;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Asteroid", new Type[]
					{
						typeof(AsteroidMono)
					})
			});
			list.Add(
				objectsToSpawn
			);
			gun.objectsToSpawn = list.ToArray();
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Asteroid";
		}

		protected override string GetDescription()
		{
			return "Bullets accelerate both horizontally and vertically.";
		}

		protected override GameObject GetCardArt()
		{
			return null;
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Uncommon;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.EvilPurple;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class PulsarCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.6f, 1f, 1f);
			gun.gravity *= 1.25f;
			gun.damage *= 1.30f;
			gun.reflects += 2;
			PulsarMono pulsarmono = player.gameObject.AddComponent<PulsarMono>();
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
			list.Add(
				objectsToSpawn
			);
			gun.objectsToSpawn = list.ToArray();

		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Pulsar";
		}

		protected override string GetDescription()
		{
			return "Bullets create a pulsar on bounce that damages, relocates and slows targets.";
		}

		protected override GameObject GetCardArt()
		{
			return null;
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bullet Gravity",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.MagicPink;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class GlueCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{

		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 1f, 1f, 1f);
			gun.projectileSpeed *= 0.7f;
			gun.reflects = 0;
			GlueMono glueMono = player.gameObject.AddComponent<GlueMono>();
			if (gun.slow > 0)
			{
				gun.slow *= 1.1f;
			}

			else
			{
				gun.slow += 1f;
			};
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		protected override string GetTitle()
		{
			return "Glue";
		}

		protected override string GetDescription()
		{
			return "Bullets splat when hitting to slow targets.";
		}

		protected override GameObject GetCardArt()
		{
			return null;
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+10%"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bullet Speed",
					amount = "-30%",
					simepleAmount = CardInfoStat.SimpleAmount.lower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bounces",
					amount = "Reset",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.TechWhite;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class AquaRingCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			AquaRingMono aquaRingMono = player.gameObject.AddComponent<AquaRingMono>();
			block.objectsToSpawn.Add(AquaRingMono.aquaVisual);
			characterStats.movementSpeed *= 0.8f;
			characterStats.health *= 1.2f;
			block.cooldown += 0.5f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Aqua Ring";
		}

		protected override string GetDescription()
		{
			return "Blocking conjures a pool of water that regenerates life.";
		}

		protected override GameObject GetCardArt()
		{
			return null;
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.5s",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.ColdBlue;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

	public class QuasarCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			QuasarMono quasarMono = player.gameObject.AddComponent<QuasarMono>();
			block.objectsToSpawn.Add(QuasarMono.quasarVisual);
			block.objectsToSpawn.Add(QuasarMono.blackholeVisual);
			characterStats.movementSpeed *= 0.8f;
			block.cooldown *= 1.25f;
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{

		}

		protected override string GetTitle()
		{
			return "Quasar";
		}

		protected override string GetDescription()
		{
			return "Blocking conjures a black hole that sucks in and rips opponents apart!";
		}

		protected override GameObject GetCardArt()
		{
			return null;
		}

		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-20%",
					simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+25%",
					simepleAmount = CardInfoStat.SimpleAmount.Some
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.DestructiveRed;
		}

		public override string GetModName()
		{
			return "CR";
		}
	}

}
