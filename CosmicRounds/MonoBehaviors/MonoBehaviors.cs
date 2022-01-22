using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnityEngine.UI.ProceduralImage;
using HarmonyLib;
using Photon.Pun;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Networking;
using UnboundLib.Utils;
using System.Collections.Specialized;
using System.ComponentModel;
using ModdingUtils.Extensions;
using ModdingUtils.RoundsEffects;
using ModdingUtils.Utils;
using System.Collections;


namespace CR.MonoBehaviors
{
	public class ShockMono : RayHitEffect
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00017E5B File Offset: 0x0001605B
		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00017E6C File Offset: 0x0001606C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			ShockMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<ShockMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{
				component.TakeDamageOverTime(componentInParent.damage * this.transform.forward, this.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}


		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		[Header("Settings")]
		public float time = 1f;

		public float interval = 0.2f;

		public Color color = Color.yellow;
	}



	public class StunMono : RayHitEffect
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00017B19 File Offset: 0x00015D19
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.multiplier = this.transform.localScale.x;
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.effectCooldown = 4f;
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.canTrigger == false)
				{
					
					this.canTrigger = true; 

				}
			}

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00017B40 File Offset: 0x00015D40
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			StunHandler component = hit.transform.GetComponent<StunHandler>();
			if (component)
			{
				ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
				float num = 30f;
				if (componentInParent)
				{
					num = componentInParent.damage;
				}
				float num2 = this.triggerChancePerTenDamage * num * 0.3f;
				num2 += this.baseTriggerChance;
				if (UnityEngine.Random.value < num2)
				{
					float num3 = this.baseStunTime + this.stunTimePerTenDamage * num * 0.3f;
					this.SetMultiplier();
					num3 *= this.stunMultiplier;
					num3 = Mathf.Pow(num3, this.stunTimeExponent);
					num3 *= this.multiplier;
					if (this.cannotPermaStun)
					{
						num3 = Mathf.Clamp(num3, 0f, this.GetComponentInParent<SpawnedAttack>().spawner.data.weaponHandler.gun.attackSpeed * this.GetComponentInParent<SpawnedAttack>().spawner.data.stats.attackSpeedMultiplier + 0.3f);
					}
					if (num3 > this.stunTimeThreshold && this.canTrigger == true)
					{
						component.AddStun(num3 * 0.3f);
						this.canTrigger = false;
						this.ResetEffectTimer();
					}
				}
			}
			return HasToReturn.canContinue;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00017C60 File Offset: 0x00015E60
		private void SetMultiplier()
		{
			float distanceTravelled = this.move.distanceTravelled;
			if (this.multiplierPerTenMeterTravelled != 0f)
			{
				this.stunMultiplier = distanceTravelled * this.multiplierPerTenMeterTravelled * 0.1f;
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0400053C RID: 1340
		public float triggerChancePerTenDamage = 0.2f;

		// Token: 0x0400053D RID: 1341
		public float baseTriggerChance = 0.4f;

		// Token: 0x0400053E RID: 1342
		[Space(15f)]
		public float stunMultiplier = 0.9f;

		// Token: 0x0400053F RID: 1343
		public float stunTimePerTenDamage = 0.1f;

		// Token: 0x04000540 RID: 1344
		public float baseStunTime = 1f;

		// Token: 0x04000541 RID: 1345
		public bool cannotPermaStun;

		// Token: 0x04000542 RID: 1346
		[Space(15f)]
		public float stunTimeThreshold = 0.1f;

		// Token: 0x04000543 RID: 1347
		public float stunTimeExponent = 1f;

		// Token: 0x04000544 RID: 1348
		public float multiplierPerTenMeterTravelled;

		// Token: 0x04000545 RID: 1349
		private MoveTransform move;

		// Token: 0x04000546 RID: 1350
		private float multiplier = 1f;

		private readonly float updateDelay = 0.1f;

		private float effectCooldown = 4f;

		private float startTime;

		private float timeOfLastEffect;

		private bool canTrigger;

	}

	public class MosquitoMono : RayHitEffect
	{

		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			MosquitoMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<MosquitoMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{

				component.TakeDamageOverTime(componentInParent.damage * this.transform.forward * 1.1f, this.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}


		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		[Header("Settings")]
		public float time = 2f;

		public float interval = .5f;

		public Color color = Color.red;

	}

	public class BeetleMono : MonoBehaviour
	{
		private void Start()
		{
			this.data = this.GetComponentInParent<CharacterData>();
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		private void OnDestroy()
		{
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void Awake()
		{
			this.player = this.gameObject.GetComponent<Player>();
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.data = this.player.GetComponent<CharacterData>();
			this.healthHandler = this.player.GetComponent<HealthHandler>();
			this.gravity = this.player.GetComponent<Gravity>();
			this.block = this.player.GetComponent<Block>();
			this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (player.data.health < player.data.maxHealth && this.active == false)
                {
					this.active = true;
					this.player.transform.gameObject.AddComponent<BeetleRegenMono>();
				}
			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		internal Player player;

		internal Gun gun;

		internal GunAmmo gunAmmo;

		internal Gravity gravity;

		internal HealthHandler healthHandler;

		internal CharacterData data;

		internal Block block;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		public Boolean active;


	}

	public class BeetleRegenMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
		}

		public override void OnStart()
		{
			this.player.data.healthHandler.regeneration += 10f;
			this.effect = this.player.GetComponent<BeetleMono>();
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.player.data.health >= this.player.data.maxHealth)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
		}

		public override void OnOnDestroy()
		{
			this.effect.active = false;
			if (this.player.data.healthHandler.regeneration >= 10f)
			{
				this.player.data.healthHandler.regeneration -= 10f;
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = Color.blue;

		private ReversibleColorEffect colorEffect = null;

		private BeetleMono effect;

		private readonly float updateDelay = 0.1f;

		private float startTime;


	}

	public class FearFactorMono : MonoBehaviour
	{
		private Player player;
		private static GameObject lineEffect;
		public LineEffect componentInChildren;
		public GameObject fearEffect = null;
		public GameObject factorObject = null;

		private readonly float updateDelay = 0.1f; // update every 0.1s instead of every frame
		private readonly float effectCooldown = 2f; // fearfactor 4s cooldown
		private readonly float maxDistance = 7.2f; // max distance the bullets can be to trigger

		private float startTime;
		private float timeOfLastEffect;
		private int numcheck = 0;

		private void Awake()
		{
			this.player = this.GetComponent<Player>();
		}

		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
			this.factorObject = new GameObject();
			this.factorObject.transform.SetParent(this.player.transform);
			this.factorObject.transform.position = this.player.transform.position;
			bool flag = FearFactorMono.lineEffect == null;
			if (flag)
			{
				this.GetLineEffect();
			}
			this.fearEffect = UnityEngine.Object.Instantiate<GameObject>(FearFactorMono.lineEffect, this.factorObject.transform);
			this.componentInChildren = this.fearEffect.GetComponentInChildren<LineEffect>();
			componentInChildren.colorOverTime = new Gradient
			{
				alphaKeys = new GradientAlphaKey[]
				{
					new GradientAlphaKey(0.7f, 0f)
				},
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(Color.red, 0f)
				},
				mode = GradientMode.Fixed
			};
			componentInChildren.widthMultiplier = 1f;
			componentInChildren.radius = 3f;
			componentInChildren.raycastCollision = true;
			componentInChildren.useColorOverTime = true;
		}

		private void GetLineEffect()
		{
			CardInfo cardInfo = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence"));
			CharacterStatModifiers componentInChildren = cardInfo.gameObject.GetComponentInChildren<CharacterStatModifiers>();
			FearFactorMono.lineEffect = componentInChildren.AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		public void Destroy()
		{
			this.componentInChildren.Stop();
			UnityEngine.Object.Destroy(this.factorObject);
			UnityEngine.Object.Destroy(this.fearEffect);
			UnityEngine.Object.Destroy(this.componentInChildren);
			UnityEngine.Object.Destroy(this);
		}


		private void Update()
		{
			// if its time to update, then update
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Fear Factor")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck > 0)
				{
					this.ResetTimer();

					// if the effect is cooled down, continue

					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						int layer = LayerMask.GetMask("Projectile");

						foreach (Collider2D buwwet in Physics2D.OverlapCircleAll(this.transform.position, 7.2f, layer).Where((uwu) => uwu.gameObject.GetComponentInParent<ProjectileHit>() != null && uwu.gameObject.GetComponentInParent<ProjectileHit>().ownPlayer != this.player && PlayerManager.instance.CanSeePlayer(uwu.gameObject.transform.position, this.player).canSee))
						{
								// if anything met our criteria, do the thing and reset the timer

								this.ResetEffectTimer();
								this.player.data.weaponHandler.gun.Attack(2f, true, 0.5f, 1f, false);

						}
					}
				}

				else
                {
					this.Destroy();
                }
			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}


	}

	public class CriticalHitMono : RayHitEffect
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002D0B File Offset: 0x00000F0B
		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D1C File Offset: 0x00000F1C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			CriticalHitMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<CriticalHitMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			this.rand = new System.Random();
			this.chance = this.rand.Next(1, 101);
			if (component && this.chance <= 50)
			{
				crit = hit.transform.gameObject.AddComponent<CriticalMono>(); 
				component.TakeDamageOverTime(componentInParent.damage * 2f * base.transform.forward, base.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			else if (component)
			{
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward, base.transform.position, this.time, this.interval, new Color(1f, 1f, 1f, 1f), this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return HasToReturn.canContinue;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E91 File Offset: 0x00001091
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04000030 RID: 48
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000031 RID: 49
		[Header("Settings")]
		public float time = 0.1f;

		// Token: 0x04000032 RID: 50
		public float interval = 0.1f;

		// Token: 0x04000033 RID: 51
		public Color color = new Color(1f, 1f, 0f, 1f);

		// Token: 0x04000034 RID: 52
		private System.Random rand;

		// Token: 0x04000035 RID: 53
		private int chance;

		public CriticalMono crit;

	}

	public class FlamethrowerMono : RayHitEffect
	{

		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			FlamethrowerMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<FlamethrowerMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{
				component.TakeDamageOverTime(componentInParent.damage * this.transform.forward, this.transform.position, this.time, this.interval, new Color(1f, 0.3f, 0f, 1f), this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}


		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		[Header("Settings")]
		public float time = 2f;

		public float interval = 0.1f;

	}

	public class SyphonMono : RayHitEffect
	{

		private void Start()
		{
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			SilenceHandler component = hit.transform.GetComponent<SilenceHandler>();
			this.rand = new System.Random();
			this.chance = rand.Next(1, 101);
			if (componentInParent.damage >= 25f)
			{
				if (component && chance <= 50)
				{
					component.RPCA_AddSilence(1.3f);
				}
			}
			else
			{
				if (component && chance <= 50f)
				{
					component.RPCA_AddSilence(1f);
				}
			}
			return HasToReturn.canContinue;
		}


		private System.Random rand;

		private int chance;
	}

	public class TaserMono : MonoBehaviour
	{
		private readonly float maxDistance = 8.5f;
		public Block block;
		public Player player;
		public CharacterData data;
		private Action<BlockTrigger.BlockTriggerType> taser;
		private Action<BlockTrigger.BlockTriggerType> basic;
		private static GameObject taserVisu = null;
		private readonly float updateDelay = 0.1f;
		private readonly float effectCooldown = 2.5f;
		private float startTime;
		private float timeOfLastEffect;
		private bool canTrigger;
		private bool hasTriggered;
		public int numcheck;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;
			this.numcheck = 0;

			bool flag = this.block;
			if (flag)
			{
				this.taser = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.taser);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Taser")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck > 0)
				{
					this.ResetTimer();

					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!block.objectsToSpawn.Contains(TaserMono.taserVisual))
						{
							block.objectsToSpawn.Add(TaserMono.taserVisual);
						}
						this.canTrigger = true;
					}

					else
					{
						if (block.objectsToSpawn.Contains(TaserMono.taserVisual))
						{
							block.objectsToSpawn.Remove(TaserMono.taserVisual);
						}

					}
				}

				else
                {
					UnityEngine.Object.Destroy(this);
                }
			}

		}


		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					Vector2 a = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = array[i].playerID == player.playerID;
						if (!flag2)
						{
							bool flag3 = Vector2.Distance(a, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee;
							if (flag3)
							{
								StunHandler component = array[i].transform.GetComponent<StunHandler>();
								DamageOverTime shockComponent = array[i].transform.GetComponent<DamageOverTime>();
								if (this.canTrigger)
								{
									component.AddStun(0.7f);
									this.hasTriggered = true;
								}
							}
						}
					}
					if (this.hasTriggered)
					{
						this.hasTriggered = false;
						this.canTrigger = false;
						this.ResetEffectTimer();
					}
				}
			};
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		public static GameObject taserVisual
		{
			get
			{
				bool flag = TaserMono.taserVisu != null;
				GameObject result;
				if (flag)
				{
					result = TaserMono.taserVisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					TaserMono.taserVisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					TaserMono.taserVisu.name = "E_Taser";
					UnityEngine.Object.DontDestroyOnLoad(TaserMono.taserVisu);
					foreach (ParticleSystem particleSystem in TaserMono.taserVisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = Color.yellow;
					}
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					UnityEngine.Object.Destroy(TaserMono.taserVisu.transform.GetChild(2).gameObject);
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(TaserMono.taserVisu.GetComponent<FollowPlayer>());
					TaserMono.taserVisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(TaserMono.taserVisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(TaserMono.taserVisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(TaserMono.taserVisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(TaserMono.taserVisu.GetComponent<RemoveAfterSeconds>());
					TaserMono.taserVisu.AddComponent<TaserMono.TaserSpawner>();
					result = TaserMono.taserVisu;
				}
				return result;
			}
			set
			{
			}
		}
		private class TaserSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 6f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}
	}

	public class HolsterMono : MonoBehaviour
	{
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private Action<BlockTrigger.BlockTriggerType> handgun;
		private Action<BlockTrigger.BlockTriggerType> basic;

		private void Awake()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.basic = this.block.BlockAction;


			bool flag = this.block;
			if (flag)
			{
				this.handgun = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block, this.data).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.handgun);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					this.player.data.weaponHandler.gun.Attack(0.00001f, true, this.player.data.weaponHandler.gun.damage * 1.5f, 1f, false);
				}
			};
		}

	}

	public class FlexMono : DealtDamageEffect
	{
		private Player player;
		private CharacterData data;
		private Block block;
		private readonly float updateDelay = 0.1f;
		private readonly float effectCooldown = 3f;
		private float startTime;
		private float timeOfLastEffect;
		private bool canTrigger;

		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					if (this.canTrigger == false)
					{
						this.canTrigger = true;
						player.data.block.RPCA_DoBlock(false, true);
						this.ResetEffectTimer();
					}
					this.canTrigger = true;
				}
				else
                {
					this.canTrigger = true;
				}
			}

		}
		public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer = null)
		{
			this.player = this.gameObject.GetComponent<Player>();
			this.data = this.player.GetComponent<CharacterData>();
			this.block = this.player.GetComponent<Block>();

			if (this.canTrigger)
			{
				this.canTrigger = false;
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

	}


	public class DroneMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.flicks = this.GetComponentsInChildren<FlickerEvent>();
			this.view = this.GetComponentInParent<PhotonView>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Homing")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Homing>().soundHomingFound;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (this.gameObject.transform.parent != null)
			{
				Player closestPlayer = PlayerManager.instance.GetClosestPlayer(this.transform.position, true);
				if (!closestPlayer)
				{
					if (this.isOn)
					{
						this.move.simulateGravity--;
						this.soundHomingCanPlay = true;
					}
					this.isOn = false;
					for (int i = 0; i < this.flicks.Length; i++)
					{
						this.flicks[i].isOn = false;
					}
					this.rot1.target = 30f;
					this.rot2.target = -30f;
					return;
				}
				Vector3 a = closestPlayer.transform.position + this.transform.right * this.move.selectedSpread * Vector3.Distance(this.transform.position, closestPlayer.transform.position) * this.spread;
				float num = Vector3.Angle(this.transform.root.forward, a - this.transform.position);
				if (num < 90f)
				{
					this.move.velocity -= this.move.velocity * num * TimeHandler.deltaTime * this.scalingDrag * 1.2f;
					this.move.velocity -= this.move.velocity * TimeHandler.deltaTime * this.drag * 1.2f;
					this.move.velocity += Vector3.ClampMagnitude(a - this.transform.position, 4f) * TimeHandler.deltaTime * this.move.localForce.magnitude * 2f * this.amount;
					this.move.velocity.z = 0f;
					this.move.velocity += Vector3.up * TimeHandler.deltaTime * this.move.gravity * this.move.multiplier * 2f;
					if (!this.isOn)
					{
						this.move.simulateGravity++;
						if (this.soundHomingCanPlay)
						{
							this.soundHomingCanPlay = false;
							SoundManager.Instance.PlayAtPosition(this.soundHomingFound, SoundManager.Instance.GetTransform(), this.transform);
						}
					}
					this.isOn = true;
					for (int j = 0; j < this.flicks.Length; j++)
					{
						this.flicks[j].isOn = true;
					}
					this.rot1.target = 45f;
					this.rot2.target = -45f;
					return;
				}
				if (this.isOn)
				{
					this.move.simulateGravity--;
					this.soundHomingCanPlay = true;
				}
				this.isOn = false;
				for (int k = 0; k < this.flicks.Length; k++)
				{
					this.flicks[k].isOn = false;
				}
				this.rot1.target = 30f;
				this.rot2.target = -30f;
			}
		}

		[Header("Sound")]
		public SoundEvent soundHomingFound;

		private bool soundHomingCanPlay = true;

		[Header("Settings")]
		public float amount = 1f;

		public float scalingDrag = 1f;

		public float drag = 1f;

		public float spread = 1f;

		private MoveTransform move;

		private bool isOn;

		public RotSpring rot1;

		public RotSpring rot2;

		private FlickerEvent[] flicks;

		private PhotonView view;

		private SoundEvent soundSpawn;

	}

	public class GoldenGlazeMono : RayHitEffect
	{

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			};

			CharacterStatModifiers component = hit.transform.GetComponent<CharacterStatModifiers>();
			GoldHealthMono componentHealth = hit.transform.GetComponent<GoldHealthMono>();

			if (component)
			{
				component.RPCA_AddSlow(2.2f, true);
			};

			if (!componentHealth)
			{
				hit.transform.gameObject.AddComponent<GoldHealthMono>();
			};

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

	}


	public class GoldHealthMono : ReversibleEffect
	{
		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterDataModifier.health_mult *= 0.8f;
			this.characterStatModifiersModifier.movementSpeed_mult /= 2.3f;
			this.characterStatModifiersModifier.jump_mult /= 2.3f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{

					this.Destroy();
					this.colorEffect.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}
		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		private readonly Color color = Color.yellow;

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private readonly float effectCooldown = 3f;

		private float startTime;

		private float timeOfLastEffect;
	}

	public class SugarGlazeMono : RayHitEffect
	{
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			};

			PlayerJump component = hit.transform.GetComponent<PlayerJump>();
			SugarMoveMono componentHealth = hit.transform.GetComponent<SugarMoveMono>();

			if (component)
			{
				component.Jump(false, 2f);
			};

			if (!componentHealth)
			{
				hit.transform.gameObject.AddComponent<SugarMoveMono>();
			};

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

	}

	public class SugarMoveMono : ReversibleEffect
	{
		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 3.5f;
			this.characterStatModifiersModifier.gravity_mult *= 3.5f;
			this.characterStatModifiersModifier.jump_mult *= 3f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{

					this.Destroy();
					this.colorEffect.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}
		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		private readonly Color color = Color.magenta;

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private readonly float effectCooldown = 2f;

		private float startTime;

		private float timeOfLastEffect;
	}

	public class MitosisMono : MonoBehaviour
	{

		public void Start()
		{
			this.data = this.GetComponentInParent<CharacterData>();
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			this.soundShoot = addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
			GameObject gameObject = addObjectToPlayer.transform.GetChild(0).gameObject;
			this.particleTransform = UnityEngine.Object.Instantiate<GameObject>(gameObject, this.transform).transform;
			this.parts = this.GetComponentsInChildren<ParticleSystem>();
			this.gun = this.data.weaponHandler.gun;
			this.basic = this.block.BlockAction;
			this.goon = gun.ShootPojectileAction;
			this.mitosis = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block, this.data).Invoke);
			this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.mitosis);
			gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
			this.Failsafe = false;
			this.Active = false;
			this.rain = false;
		}

		private void Attack(GameObject projectile)
		{
			if (!rain)
			{
				MitosisBlockMono[] mitoBlock = this.GetComponents<MitosisBlockMono>();
				if (this.Active)
				{
					SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), this.transform);
					this.Failsafe = false;
					this.Active = false;
				}
			}
		}

		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				if (!rain)
				{
					bool flag = trigger != BlockTrigger.BlockTriggerType.None;
					if (flag && !this.Active)
					{
						this.player.transform.gameObject.AddComponent<MitosisBlockMono>();
						this.Active = true;
						this.Failsafe = true;
					}
				}
			};
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
			this.gun.ShootPojectileAction = this.goon;
			UnityEngine.Object.Destroy(this.soundShoot);
			UnityEngine.Object.Destroy(this.soundSpawn);
			this.Failsafe = false;
			this.Active = false;
		}

		public void Update()
		{
			this.rain = this.player.GetComponent<RainMaker>() != null;
			if (!rain)
			{
				if (this.Active)
				{
					Transform transform = this.data.weaponHandler.gun.transform;
					this.particleTransform.position = transform.position;
					this.particleTransform.rotation = transform.rotation;
					ParticleSystem[] array = this.parts;
					if (!this.alreadyActivated)
					{
						SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), this.transform);
						array = this.parts;
						for (int i = 0; i < array.Length; i++)
						{
							array[i].Play();
						}
						this.alreadyActivated = true;
						return;
					}
				}
				else if (this.alreadyActivated)
				{
					ParticleSystem[] array = this.parts;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Stop();
					}
					this.alreadyActivated = false;
				}
			}
		}

		private CharacterData data;

		private Block block;

		private Player player;

		private Gun gun;

		private Action<BlockTrigger.BlockTriggerType> mitosis;

		private Action<BlockTrigger.BlockTriggerType> basic;

		private Action<GameObject> goon;

		private Transform particleTransform;

		private SoundEvent soundSpawn;

		private SoundEvent soundShoot;

		private bool alreadyActivated;

		private ParticleSystem[] parts;

		public bool Active;

		public bool Failsafe;

		public bool rain;

	}

	public class MitosisBlockMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.gunStatModifier.numberOfProjectiles_add += 1;
			this.gunStatModifier.spread_add += 0.05f;
			this.gunAmmoStatModifier.currentAmmo_mult *= 2;
			this.gunStatModifier.projectileSpeed_mult *= 1.25f;
			this.gunStatModifier.projectileColor += new Color(0f, 1f, 0f, 1f);
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.effect = this.player.GetComponent<MitosisMono>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (!this.effect.Failsafe)
				{
					this.Destroy();
					this.colorEffect.Destroy();
					this.effect.Failsafe = true;
					this.effect.Active = false;
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				this.effect.Active = false;
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = Color.green;

		private ReversibleColorEffect colorEffect = null;

		private MitosisMono effect;

		private readonly float updateDelay = 0.1f;

		private float startTime;


	}

	public class MeiosisMono : MonoBehaviour
	{
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = true;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					this.player.transform.gameObject.AddComponent<MeiosisReloadMono>();
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private CharacterData data;

		private Block block;

		public Player player;

		private Gun gun;

		public bool able;

		private readonly float updateDelay = 0.1f;

		private readonly float effectCooldown = 1f;

		private float timeOfLastEffect;

		private float startTime;
	}

	public class MeiosisReloadMono : ReversibleEffect
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000045BA File Offset: 0x000027BA
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000045D8 File Offset: 0x000027D8
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 1.3f;
			this.characterStatModifiersModifier.jump_mult *= 1.3f;
			this.characterDataModifier.health_mult *= 1.5f;
			this.characterDataModifier.maxHealth_mult *= 1.5f;
			this.blockModifier.cdMultiplier_mult *= 0.7f;
			this.blockModifier.additionalBlocks_add += 2;
			this.effect = this.player.GetComponent<MeiosisMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.effect.able = false;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), this.player.transform);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004714 File Offset: 0x00002914
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.player.data.weaponHandler.gun.isReloading)
				{
					base.Destroy();
					this.effect.able = false;
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000476E File Offset: 0x0000296E
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004789 File Offset: 0x00002989
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.effect.able = true;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047B0 File Offset: 0x000029B0
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400008D RID: 141
		private readonly Color color = new Color(0f, 1f, 1f, 1f);

		// Token: 0x0400008E RID: 142
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400008F RID: 143
		private readonly float updateDelay = 0.001f;

		// Token: 0x04000090 RID: 144
		private float startTime;

		// Token: 0x04000091 RID: 145
		private MeiosisMono effect;

		// Token: 0x04000092 RID: 146
		private SoundEvent soundSpawn;
	}

	public class PogoMono : MonoBehaviour
	{
		private Player player;
		private readonly float updateDelay = 0.1f;
		private readonly float effectCooldown = 1f;
		private float startTime;
		private float timeOfLastEffect;

		private void Awake()
		{
			this.player = this.GetComponent<Player>();
		}

		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void Update()
		{
			// if its time to update, then update
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					this.ResetEffectTimer();
					this.player.data.jump.Jump(true, 1f);
				}
			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}


	}

	public class CloudMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				this.move.velocity.x *= 0.8f;
				this.move.velocity.y *= 0.7f;
				this.move.velocity.z = 0f;

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.1f;

		private float startTime;
	}

	public class PulseMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.state = true;
			this.ResetTimer();


		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (state)
				{
					this.move.velocity.x *= 0.6f;
					this.move.velocity.y *= 0.6f;
					this.move.velocity.z = 0f;
					this.state = false;
				}

				else
                {
					this.move.velocity.x *= 1.8f;
					this.move.velocity.y *= 1.8f;
					this.move.velocity.z = 0f;
					this.state = true;
				}

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.2f;

		private float startTime;

		public bool state;
	}

	public class DriveMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.flicks = this.GetComponentsInChildren<FlickerEvent>();
			this.view = this.GetComponentInParent<PhotonView>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.state = 0;
			this.x = this.move.velocity.x;
			this.y = this.move.velocity.y;
			this.ResetTimer();
			this.detected = false;

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (state == 0)
				{
					this.state = 1;
					this.move.velocity.z = 0f;
					this.move.velocity.x *= 1f;
					this.move.velocity.y *= 1f;
				}

				else if(state == 1)
				{
					this.move.simulateGravity--; 
					this.move.velocity.x *= 0.01f;
					this.move.velocity.y *= 0.01f;
					this.move.velocity.z = 0f;
					this.state = 2;
					Player closestPlayer = PlayerManager.instance.GetClosestPlayer(this.transform.position, true);
					if (closestPlayer)
                    {
						this.detected = true;
                    };
					Vector3 a = closestPlayer.transform.position + this.transform.right * this.move.selectedSpread * Vector3.Distance(this.transform.position, closestPlayer.transform.position);
					float num = Vector3.Angle(this.transform.root.forward, a - this.transform.position);
					this.move.velocity -= this.move.velocity * num;
					this.move.velocity -= this.move.velocity;
					this.move.velocity += Vector3.ClampMagnitude(a - this.transform.position, 10f) * TimeHandler.deltaTime * this.move.localForce.magnitude;
					this.move.velocity.z = 0f;
						for (int j = 0; j < this.flicks.Length; j++)
						{
							this.flicks[j].isOn = true;
						}
				}

				else if (state == 2)
				{
 
					this.move.velocity.x *= 20f;
					this.move.velocity.y *= 20f;
					this.move.velocity.z = 0f;
					if (this.detected)
                    {
						this.move.simulateGravity++;
					}
					this.state = 3;
				}

				else if (state == 3)
				{
					this.move.velocity.z = 0f;
					this.state = 3;
				}

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.2f;

		private float startTime;

		public int state;

		public bool detected;

		private FlickerEvent[] flicks;

		private PhotonView view;

		public RotSpring rot1;

		public RotSpring rot2;

		public float x;

		public float y;
	}

	public class CriticalMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}

			if (this.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				this.Destroy();
			}

		}

		public override void OnStart()
		{
			this.effect = this.player.GetComponent<CriticalHitMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundShoot = addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
			SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), this.transform);
			if (this.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				this.Destroy();
			}
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.Destroy();

			}

			if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
			{
				this.ResetTimer();
				this.Destroy();
			}
		}
		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}

			if (this.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				this.Destroy();
			}

		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
				UnityEngine.Object.Destroy(this);
				UnityEngine.Object.Destroy(this.soundShoot);
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0.8f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.2f;

		private float startTime;

		private CriticalHitMono effect;

		private SoundEvent soundShoot;


	}

	public class DropMono : HitSurfaceEffect
	{
		private void Awake()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
		}

		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			UnityEngine.Object.Instantiate<GameObject>(DropMono.dropVisual, position, Quaternion.identity);
		}

		public static GameObject dropVisual
		{
			get
			{
				bool flag = DropMono.dropVisu != null;
				GameObject result;
				if (flag)
				{
					result = DropMono.dropVisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					DropMono.dropVisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					DropMono.dropVisu.name = "E_Drop";
					UnityEngine.Object.DontDestroyOnLoad(DropMono.dropVisu);
					foreach (ParticleSystem particleSystem in DropMono.dropVisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = Color.yellow;
					}
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					UnityEngine.Object.Destroy(DropMono.dropVisu.transform.GetChild(2).gameObject);
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(DropMono.dropVisu.GetComponent<FollowPlayer>());
					DropMono.dropVisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(DropMono.dropVisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(DropMono.dropVisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(DropMono.dropVisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(DropMono.dropVisu.GetComponent<RemoveAfterSeconds>());
					DropSpawner drop = DropMono.dropVisu.AddComponent<DropMono.DropSpawner>();
					result = DropMono.dropVisu;
				}
				return result;
			}
			set
			{
			}
		}
		private class DropSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 0.2f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.1f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private static GameObject dropVisu = null;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		public Color col;
	}

	public class SunMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();
			this.loop = 1f;

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				this.move.simulateGravity--;
				Vector3 a = this.transform.right * this.move.selectedSpread * 15f;
				float num = Vector3.Angle(this.transform.root.forward, a - this.transform.position);
				this.move.velocity -= this.move.velocity * num * TimeHandler.deltaTime;
				this.move.velocity -= this.move.velocity * TimeHandler.deltaTime;
				this.move.velocity += Vector3.ClampMagnitude(a - this.transform.position, 1f) * TimeHandler.deltaTime * this.move.localForce.magnitude * this.loop;
				if (this.loop < 10f)
				{
					this.loop += 1f;
				}
				this.move.velocity.z = 0f;
				this.move.velocity += Vector3.up * TimeHandler.deltaTime * this.move.gravity * this.move.multiplier * 2f;

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.15f;

		private float startTime;

		private float loop = 1f;
	}

	public class SpawnBulletsEffect : MonoBehaviour
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00005555 File Offset: 0x00003755
		private void Awake()
		{
			this.player = base.gameObject.GetComponent<Player>();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005568 File Offset: 0x00003768
		private void Start()
		{
			this.ResetTimer();
			this.timeSinceLastShot += this.initialDelay;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005583 File Offset: 0x00003783
		private void Update()
		{
			if (this.numShot >= this.numBullets || this.gunToShootFrom == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (Time.time >= this.timeSinceLastShot + this.timeBetweenShots)
			{
				this.Shoot();
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000055C2 File Offset: 0x000037C2
		private void OnDisable()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000055CA File Offset: 0x000037CA
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.newWeaponsBase);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000055D8 File Offset: 0x000037D8
		private void Shoot()
		{
			int num = this.gunToShootFrom.lockGunToDefault ? 1 : (this.gunToShootFrom.numberOfProjectiles + Mathf.RoundToInt(this.gunToShootFrom.chargeNumberOfProjectilesTo * 0f));
			for (int i = 0; i < this.gunToShootFrom.projectiles.Length; i++)
			{
				for (int j = 0; j < num; j++)
				{
					Vector3 vector;
					if (this.directionsToShoot.Count == 0)
					{
						vector = Vector3.down;
					}
					else
					{
						vector = this.directionsToShoot[this.numShot % this.directionsToShoot.Count];
					}
					if (this.gunToShootFrom.spread != 0f)
					{
						float multiplySpread = this.gunToShootFrom.multiplySpread;
						float num2 = UnityEngine.Random.Range(-this.gunToShootFrom.spread, this.gunToShootFrom.spread);
						num2 /= (1f + this.gunToShootFrom.projectileSpeed * 0.5f) * 0.5f;
						vector += Vector3.Cross(vector, Vector3.forward) * num2 * multiplySpread;
					}
					if ((bool)typeof(Gun).InvokeMember("CheckIsMine", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gunToShootFrom, new object[0]))
					{
						Vector3 position;
						if (this.positionsToShootFrom.Count == 0)
						{
							position = Vector3.zero;
						}
						else
						{
							position = this.positionsToShootFrom[this.numShot % this.positionsToShootFrom.Count];
						}
						GameObject gameObject = PhotonNetwork.Instantiate(this.gunToShootFrom.projectiles[i].objectToSpawn.gameObject.name, position, Quaternion.LookRotation(vector), 0, null);
						if (PhotonNetwork.OfflineMode)
						{
							this.RPCA_Shoot(gameObject.GetComponent<PhotonView>().ViewID, num, 1f, UnityEngine.Random.Range(0f, 1f));
						}
						else
						{
							base.gameObject.GetComponent<PhotonView>().RPC("RPCA_Shoot", RpcTarget.All, new object[]
							{
								gameObject.GetComponent<PhotonView>().ViewID,
								num,
								1f,
								UnityEngine.Random.Range(0f, 1f)
							});
						}
					}
				}
			}
			this.ResetTimer();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005820 File Offset: 0x00003A20
		[PunRPC]
		private void RPCA_Shoot(int bulletViewID, int numProj, float dmgM, float seed)
		{
			GameObject gameObject = PhotonView.Find(bulletViewID).gameObject;
			this.gunToShootFrom.BulletInit(gameObject, numProj, dmgM, seed, true);
			this.numShot++;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005858 File Offset: 0x00003A58
		public void SetGun(Gun gun)
		{
			this.newWeaponsBase = UnityEngine.Object.Instantiate<GameObject>(this.player.GetComponent<Holding>().holdable.GetComponent<Gun>().gameObject, new Vector3(500f, 500f, -100f), Quaternion.identity);
			UnityEngine.Object.DontDestroyOnLoad(this.newWeaponsBase);
			foreach (object obj in this.newWeaponsBase.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponentInChildren<Renderer>() != null)
				{
					Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].enabled = false;
					}
				}
			}
			this.gunToShootFrom = this.newWeaponsBase.GetComponent<Gun>();
			SpawnBulletsEffect.CopyGunStats(gun, this.gunToShootFrom);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005944 File Offset: 0x00003B44
		public void SetNumBullets(int num)
		{
			this.numBullets = num;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000594D File Offset: 0x00003B4D
		public void SetPosition(Vector3 pos)
		{
			this.positionsToShootFrom = new List<Vector3>
			{
				pos
			};
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005961 File Offset: 0x00003B61
		public void SetDirection(Vector3 dir)
		{
			this.directionsToShoot = new List<Vector3>
			{
				dir
			};
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005975 File Offset: 0x00003B75
		public void SetPositions(List<Vector3> pos)
		{
			this.positionsToShootFrom = pos;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000597E File Offset: 0x00003B7E
		public void SetDirections(List<Vector3> dir)
		{
			this.directionsToShoot = dir;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005987 File Offset: 0x00003B87
		public void SetTimeBetweenShots(float delay)
		{
			this.timeBetweenShots = delay;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005990 File Offset: 0x00003B90
		public void SetInitialDelay(float delay)
		{
			this.initialDelay = delay;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005999 File Offset: 0x00003B99
		private void ResetTimer()
		{
			this.timeSinceLastShot = Time.time;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000059A8 File Offset: 0x00003BA8
		public static void CopyGunStats(Gun copyFromGun, Gun copyToGun)
		{
			copyToGun.ammo = copyFromGun.ammo;
			copyToGun.ammoReg = copyFromGun.ammoReg;
			copyToGun.attackID = copyFromGun.attackID;
			copyToGun.attackSpeed = copyFromGun.attackSpeed;
			copyToGun.attackSpeedMultiplier = copyFromGun.attackSpeedMultiplier;
			copyToGun.bodyRecoil = copyFromGun.bodyRecoil;
			copyToGun.bulletDamageMultiplier = copyFromGun.bulletDamageMultiplier;
			copyToGun.bulletPortal = copyFromGun.bulletPortal;
			copyToGun.bursts = copyFromGun.bursts;
			copyToGun.chargeDamageMultiplier = copyFromGun.chargeDamageMultiplier;
			copyToGun.chargeEvenSpreadTo = copyFromGun.chargeEvenSpreadTo;
			copyToGun.chargeNumberOfProjectilesTo = copyFromGun.chargeNumberOfProjectilesTo;
			copyToGun.chargeRecoilTo = copyFromGun.chargeRecoilTo;
			copyToGun.chargeSpeedTo = copyFromGun.chargeSpeedTo;
			copyToGun.chargeSpreadTo = copyFromGun.chargeSpreadTo;
			copyToGun.cos = copyFromGun.cos;
			copyToGun.currentCharge = copyFromGun.currentCharge;
			copyToGun.damage = copyFromGun.damage;
			copyToGun.damageAfterDistanceMultiplier = copyFromGun.damageAfterDistanceMultiplier;
			copyToGun.defaultCooldown = copyFromGun.defaultCooldown;
			copyToGun.dmgMOnBounce = copyFromGun.dmgMOnBounce;
			copyToGun.dontAllowAutoFire = copyFromGun.dontAllowAutoFire;
			copyToGun.drag = copyFromGun.drag;
			copyToGun.dragMinSpeed = copyFromGun.dragMinSpeed;
			copyToGun.evenSpread = copyFromGun.evenSpread;
			copyToGun.explodeNearEnemyDamage = copyFromGun.explodeNearEnemyDamage;
			copyToGun.explodeNearEnemyRange = copyFromGun.explodeNearEnemyRange;
			copyToGun.forceSpecificAttackSpeed = copyFromGun.forceSpecificAttackSpeed;
			copyToGun.forceSpecificShake = copyFromGun.forceSpecificShake;
			copyToGun.gravity = copyFromGun.gravity;
			copyToGun.hitMovementMultiplier = copyFromGun.hitMovementMultiplier;
			copyToGun.ignoreWalls = copyFromGun.ignoreWalls;
			copyToGun.isProjectileGun = copyFromGun.isProjectileGun;
			copyToGun.isReloading = copyFromGun.isReloading;
			copyToGun.knockback = copyFromGun.knockback;
			copyToGun.lockGunToDefault = copyFromGun.lockGunToDefault;
			copyToGun.multiplySpread = copyFromGun.multiplySpread;
			copyToGun.numberOfProjectiles = copyFromGun.numberOfProjectiles;
			copyToGun.objectsToSpawn = copyFromGun.objectsToSpawn;
			copyToGun.overheatMultiplier = copyFromGun.overheatMultiplier;
			copyToGun.percentageDamage = copyFromGun.percentageDamage;
			copyToGun.player = copyFromGun.player;
			copyToGun.projectielSimulatonSpeed = copyFromGun.projectielSimulatonSpeed;
			copyToGun.projectileColor = copyFromGun.projectileColor;
			copyToGun.projectiles = copyFromGun.projectiles;
			copyToGun.projectileSize = copyFromGun.projectileSize;
			copyToGun.projectileSpeed = copyFromGun.projectileSpeed;
			copyToGun.randomBounces = copyFromGun.randomBounces;
			copyToGun.recoil = copyFromGun.recoil;
			copyToGun.recoilMuiltiplier = copyFromGun.recoilMuiltiplier;
			copyToGun.reflects = copyFromGun.reflects;
			copyToGun.reloadTime = copyFromGun.reloadTime;
			copyToGun.reloadTimeAdd = copyFromGun.reloadTimeAdd;
			copyToGun.shake = copyFromGun.shake;
			copyToGun.shakeM = copyFromGun.shakeM;
			copyToGun.ShootPojectileAction = copyFromGun.ShootPojectileAction;
			copyToGun.sinceAttack = copyFromGun.sinceAttack;
			copyToGun.size = copyFromGun.size;
			copyToGun.slow = copyFromGun.slow;
			copyToGun.smartBounce = copyFromGun.smartBounce;
			copyToGun.spawnSkelletonSquare = copyFromGun.spawnSkelletonSquare;
			copyToGun.speedMOnBounce = copyFromGun.speedMOnBounce;
			copyToGun.spread = copyFromGun.spread;
			copyToGun.teleport = copyFromGun.teleport;
			copyToGun.timeBetweenBullets = copyFromGun.timeBetweenBullets;
			copyToGun.timeToReachFullMovementMultiplier = copyFromGun.timeToReachFullMovementMultiplier;
			copyToGun.unblockable = copyFromGun.unblockable;
			copyToGun.useCharge = copyFromGun.useCharge;
			copyToGun.waveMovement = copyFromGun.waveMovement;
			Traverse.Create(copyToGun).Field("attackAction").SetValue((Action)Traverse.Create(copyFromGun).Field("attackAction").GetValue());
			Traverse.Create(copyToGun).Field("gunID").SetValue((int)Traverse.Create(copyFromGun).Field("gunID").GetValue());
			Traverse.Create(copyToGun).Field("spreadOfLastBullet").SetValue((float)Traverse.Create(copyFromGun).Field("spreadOfLastBullet").GetValue());
			Traverse.Create(copyToGun).Field("forceShootDir").SetValue((Vector3)Traverse.Create(copyFromGun).Field("forceShootDir").GetValue());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000055C2 File Offset: 0x000037C2
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x04000075 RID: 117
		private float initialDelay = 1f;

		// Token: 0x04000076 RID: 118
		private int numBullets = 1;

		// Token: 0x04000077 RID: 119
		private int numShot;

		// Token: 0x04000078 RID: 120
		private Gun gunToShootFrom;

		// Token: 0x04000079 RID: 121
		private List<Vector3> directionsToShoot = new List<Vector3>();

		// Token: 0x0400007A RID: 122
		private List<Vector3> positionsToShootFrom = new List<Vector3>();

		// Token: 0x0400007B RID: 123
		private float timeBetweenShots;

		// Token: 0x0400007C RID: 124
		private float timeSinceLastShot;

		// Token: 0x0400007D RID: 125
		private GameObject newWeaponsBase;

		// Token: 0x0400007E RID: 126
		private Player player;
	}

	public class FakeGun : Gun
	{
	}

	[RequireComponent(typeof(PhotonView))]
	public class RainMono : MonoBehaviour, IPunInstantiateMagicCallback
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004B3C File Offset: 0x00002D3C
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004B8A File Offset: 0x00002D8A
		private float delay
		{
			get
			{
				bool flag = this.numPops > 0;
				float result;
				if (flag)
				{
					result = this.defaultDelay;
				}
				else
				{
					result = this.defaultDelay;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004B90 File Offset: 0x00002D90
		public void OnPhotonInstantiate(PhotonMessageInfo info)
		{
			object[] instantiationData = info.photonView.InstantiationData;
			GameObject gameObject = PhotonView.Find((int)instantiationData[0]).gameObject;
			this.gameObject.transform.SetParent(gameObject.transform);
			this.player = gameObject.GetComponent<ProjectileHit>().ownPlayer;
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.ResetTimer();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004C00 File Offset: 0x00002E00
		private void ResetTimer()
		{
			this.time = Time.time;
			this.startTime = Time.time;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004C0E File Offset: 0x00002E0E
		private void Awake()
		{
			this.ResetTimer();
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004C14 File Offset: 0x00002E14
		private void Start()
		{
			if (this.gameObject.transform.parent != null)
			{
				this.projectile = this.gameObject.transform.parent.GetComponent<ProjectileHit>();
				this.view = this.gameObject.GetComponent<PhotonView>();
				this.rainMaker = this.player.GetComponent<RainMaker>();
				this.numPops = 10;
				this.ResetTimer();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004CAC File Offset: 0x00002EAC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				if (this.gameObject.transform.parent != null)
				{
					bool flag2 = this.pops >= this.numPops;
					if (flag2)
					{
						UnityEngine.Object.Destroy(this);
					}
					else
					{
						bool flag3 = Time.time < this.delay + this.time;
						if (!flag3)
						{
							this.ResetTimer();
							this.Pop();
						}
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D84 File Offset: 0x00002F84
		private void Pop()
		{
			if (this.gameObject.transform.parent != null)
			{
				this.pops++;

				this.view.RPC("RPCA_ShootWaterWorks", RpcTarget.All, new object[]
				{
					this.gameObject.transform.position,
					this.bullets
				});
			};
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004E14 File Offset: 0x00003014
		private void OnDisable()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E18 File Offset: 0x00003018
		[PunRPC]
		private void RPCA_ShootWaterWorks(Vector3 position, int numBullets)
		{
			if (this.gameObject.transform.parent != null)
			{
			Gun newGun = this.player.gameObject.AddComponent<FakeGun>();
			SpawnBulletsEffect spawnBulletsEffect = this.player.gameObject.AddComponent<SpawnBulletsEffect>();
			List<Vector3> positions = this.GetPositions(position, this.radius, numBullets);
			spawnBulletsEffect.SetPositions(positions);
			spawnBulletsEffect.SetDirections(this.GetDirections(position, positions));
			spawnBulletsEffect.SetNumBullets(numBullets);
			spawnBulletsEffect.SetTimeBetweenShots(0f);
			spawnBulletsEffect.SetInitialDelay(0.1f);
			SpawnBulletsEffect.CopyGunStats(this.gun, newGun);
			newGun.spread = 1f;
			newGun.numberOfProjectiles = 1;
			newGun.projectiles = (from e in Enumerable.Range(0, newGun.numberOfProjectiles)
									  from x in newGun.projectiles
									  select x).ToList<ProjectilesToSpawn>().Take(newGun.numberOfProjectiles).ToArray<ProjectilesToSpawn>();
			newGun.damage = Mathf.Clamp(newGun.damage, 0.2f, float.MaxValue);
			newGun.projectileSpeed = 0.15f;
			newGun.projectielSimulatonSpeed = 1.2f;
			newGun.dragMinSpeed = 0f;
			newGun.gravity = 1.1f;
			newGun.reflects = 0;
			newGun.GetAdditionalData().inactiveDelay = 0.1f;
			newGun.damageAfterDistanceMultiplier = 1f;
			this.transform.parent.Find("Collider").gameObject.SetActive(false);
			List<ObjectsToSpawn> list = new List<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
				{
					AddToProjectile = new GameObject("A_Recurse", new Type[]
					{
					typeof(Recursiont)
					})
				});

			list.Add(new ObjectsToSpawn
				{
					AddToProjectile = new GameObject("A_Colli", new Type[]
						{
					typeof(RainCollider)
						})
				});

			newGun.objectsToSpawn = list.ToArray();
			spawnBulletsEffect.SetGun(newGun);
		}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005024 File Offset: 0x00003224
		private Vector2 CosSin(float angle)
		{
			return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005048 File Offset: 0x00003248
		private List<Vector3> GetPositions(Vector2 center, float radius, int bullets)
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(center);
			}
			return list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000509C File Offset: 0x0000329C
		private List<Vector3> GetDirections(Vector2 center, List<Vector3> shootPos)
		{
			List<Vector3> list = new List<Vector3>();
			foreach (Vector3 v in shootPos)
			{
				list.Add((v - (Vector3)center).normalized);
			}
			return list;
		}

		// Token: 0x0400003E RID: 62
		private static readonly System.Random rng = new System.Random();

		// Token: 0x0400003F RID: 63
		private readonly float defaultDelay = 0.25f;

		// Token: 0x04000040 RID: 64
		private readonly int bullets = 1;

		// Token: 0x04000041 RID: 65
		private readonly float radius = 1f;

		// Token: 0x04000042 RID: 66
		private int numPops;

		// Token: 0x04000043 RID: 67
		private float time;

		// Token: 0x04000044 RID: 68
		private int pops = 0;

		// Token: 0x04000046 RID: 70
		private PhotonView view;

		// Token: 0x04000047 RID: 71
		private Transform parent;

		// Token: 0x04000048 RID: 72
		private Player player;

		// Token: 0x04000049 RID: 73
		private Gun gun;

		private RainMaker rainMaker;

		// Token: 0x0400004A RID: 74
		private ProjectileHit projectile;

		private readonly float updateDelay = 0.1f;

		private float startTime;

	}

	public class RainAssets
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000049D0 File Offset: 0x00002BD0
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004A3A File Offset: 0x00002C3A
		internal static GameObject rain
		{
			get
			{
				bool flag = RainAssets._rain != null;
				GameObject rain;
				if (flag)
				{
					rain = RainAssets._rain;
				}
				else
				{
					RainAssets._rain = new GameObject("Waterworks", new Type[]
					{
						typeof(RainMono),
						typeof(PhotonView)
					});
					UnityEngine.Object.DontDestroyOnLoad(RainAssets._rain);
					rain = RainAssets._rain;
				}
				return rain;
			}
			set
			{
			}
		}

		// Token: 0x0400003C RID: 60
		private static GameObject _rain;
	}

	public class RainSpawner : MonoBehaviour
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004A48 File Offset: 0x00002C48
		private void Awake()
		{
			bool flag = !RainSpawner.Initialized;
			if (flag)
			{
				PhotonNetwork.PrefabPool.RegisterPrefab(RainAssets.rain.name, RainAssets.rain);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004A80 File Offset: 0x00002C80
		private void Start()
		{
			bool flag = !RainSpawner.Initialized;
			if (flag)
			{
				RainSpawner.Initialized = true;
			}
			else
			{
				bool flag2 = !PhotonNetwork.OfflineMode && !base.gameObject.transform.parent.GetComponent<ProjectileHit>().ownPlayer.data.view.IsMine;
				if (!flag2)
				{
					PhotonNetwork.Instantiate(RainAssets.rain.name, base.transform.position, base.transform.rotation, 0, new object[]
					{
						base.gameObject.transform.parent.GetComponent<PhotonView>().ViewID
					});
				}
			}
		}

		// Token: 0x0400003D RID: 61
		private static bool Initialized;
	}

	public class Recursiont
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000253E File Offset: 0x0000073E
		internal static GameObject stopRecursion
		{
			get
			{
				bool flag = Recursiont._stopRecursion != null;
				GameObject stopRecursion;
				if (flag)
				{
					stopRecursion = Recursiont._stopRecursion;
				}
				else
				{
					Recursiont._stopRecursion = new GameObject("Recursion't", new Type[]
					{
						typeof(StopRecursion),
						typeof(DestroyOnUnparentPostInitialization)
					});
					UnityEngine.Object.DontDestroyOnLoad(Recursiont._stopRecursion);
					stopRecursion = Recursiont._stopRecursion;
				}
				return stopRecursion;
			}
			set
			{
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002568 File Offset: 0x00000768
		internal static ObjectsToSpawn stopRecursionObjectToSpawn
		{
			get
			{
				return new ObjectsToSpawn
				{
					AddToProjectile = Recursiont.stopRecursion
				};
			}
			set
			{
			}
		}

		// Token: 0x04000005 RID: 5
		private static GameObject _stopRecursion;
	}

	public class DestroyOnUnparentPostInitialization : MonoBehaviour
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002574 File Offset: 0x00000774
		private void Start()
		{
			bool flag = !DestroyOnUnparentPostInitialization.initialized;
			if (flag)
			{
				this.isOriginal = true;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002598 File Offset: 0x00000798
		private void LateUpdate()
		{
			bool flag = this.isOriginal;
			if (!flag)
			{
				bool flag2 = base.gameObject.transform.parent == null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private static bool initialized;

		// Token: 0x04000007 RID: 7
		private bool isOriginal = false;
	}

	public class MeteorMono : RayHitEffect
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();

		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			MeteorMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<MeteorMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{

				component.TakeDamageOverTime(componentInParent.damage *  1.25f * this.transform.forward, this.transform.position, 1f, 0.25f, new Color(0.1f, 1f, 0.3f, 1f), this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.y < 0f)
				{
					this.move.velocity.y *= 1.5f;
				}
				this.move.velocity.z = 0f;

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

	}

	public class RainCollider : MonoBehaviour
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0000605A File Offset: 0x0000425A
		private void Awake()
		{
			if (this.transform.parent)
			{
				this.transform.parent.Find("Collider").gameObject.SetActive(false);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public class RainMaker : MonoBehaviour
	{
		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void Awake()
		{
			this.player = this.gameObject.GetComponent<Player>();
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.data = this.player.GetComponent<CharacterData>();
			this.healthHandler = this.player.GetComponent<HealthHandler>();
			this.gravity = this.player.GetComponent<Gravity>();
			this.block = this.player.GetComponent<Block>();
			this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
			this.gun.numberOfProjectiles = 1;
			this.gun.bursts = 1;
			this.gun.attackSpeed = 0.35f;
			this.gun.spread = 0f;
			this.gun.evenSpread = 0f;
		}

		private void Update()
		{
			// if its time to update, then update
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					this.ResetEffectTimer();
					this.gun.numberOfProjectiles = 1;
					this.gun.bursts = 1;
					this.gun.attackSpeed = 0.2f;
					this.gun.spread = 0f;
					this.gun.evenSpread = 0f;

				}
			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		internal Player player;

		internal Gun gun;

		internal GunAmmo gunAmmo;

		internal Gravity gravity;

		internal HealthHandler healthHandler;

		internal CharacterData data;

		internal Block block;

		public int projectiles;

		public int burst;

		private readonly float updateDelay = 0.1f;

		private readonly float effectCooldown = 1f;

		private float startTime;

		private float timeOfLastEffect;

	}

	public class CometMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.x != 0f)
				{
					this.move.velocity.x *= 1.1f;
				}
				this.move.velocity.z = 0f;

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.1f;

		private float startTime;
	}

	public class UnicornMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.player = this.gameObject.GetComponent<Player>();
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.data = this.player.GetComponent<CharacterData>();
			this.healthHandler = this.player.GetComponent<HealthHandler>();
			this.gravity = this.player.GetComponent<Gravity>();
			this.block = this.player.GetComponent<Block>();
			this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
			this.mode = 0;
			this.realmode = 0;

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.realmode == 0)
                {
					this.player.gameObject.AddComponent<RedMono>();
					this.mode = 0;
					this.realmode += 1;
				}
				else if (this.realmode == 1)
				{
					this.player.gameObject.AddComponent<OrangeMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 2)
				{
					this.player.gameObject.AddComponent<YellowMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 3)
				{
					this.player.gameObject.AddComponent<GreenMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 4)
				{
					this.player.gameObject.AddComponent<CyanMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 5)
				{
					this.player.gameObject.AddComponent<BlueMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 6)
				{
					this.player.gameObject.AddComponent<PurpleMono>();
					this.mode += 1;
					this.realmode += 1;
				}
				else if (this.realmode == 7)
				{
					this.player.gameObject.AddComponent<PinkMono>();
					this.mode += 1;
					this.realmode = 0;
				}

			}

		}


		private readonly float updateDelay = 5f;

		private float startTime;

		public int mode;

		public int realmode;

		internal Player player;

		internal Gun gun;

		internal GunAmmo gunAmmo;

		internal Gravity gravity;

		internal HealthHandler healthHandler;

		internal CharacterData data;

		internal Block block;
	}

	public class RedMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.gunStatModifier.damage_mult *= 2;
			this.gunStatModifier.damageAfterDistanceMultiplier_mult *= 1.2f;
			this.gunStatModifier.projectileColor += new Color(1f, 0f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 0)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class OrangeMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.gunAmmoStatModifier.reloadTimeMultiplier_mult *= 0.1f;
			this.gunStatModifier.attackSpeedMultiplier_mult *= 0.001f;
			this.gunStatModifier.damage_mult *= 0.75f;
			this.gunStatModifier.projectileColor += new Color(1f, 0.5f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 1)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0.5f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class YellowMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.gunStatModifier.projectileSize_mult *= 1.5f;
			this.gunStatModifier.projectileSpeed_mult *= 2;
			this.gunStatModifier.projectileColor += new Color(1f, 1f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 2)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 1f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class GreenMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterDataModifier.health_mult *= 2f;
			this.characterDataModifier.maxHealth_mult *= 2f;
			this.gunStatModifier.projectileColor += new Color(0f, 1f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 3)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(0f, 1f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class CyanMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 2f;
			this.characterStatModifiersModifier.jump_mult *= 2f;
			this.gunStatModifier.slow_add += 0.1f; 
			this.gunStatModifier.projectileColor += new Color(0f, 1f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 4)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(0f, 1f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class BlueMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 0.75f;
			this.blockModifier.cdMultiplier_mult *= 0.5f;
			this.blockModifier.additionalBlocks_add += 2;
			this.gunStatModifier.projectileColor += new Color(0f, 0f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 5)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(0f, 0f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class PurpleMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.secondsToTakeDamageOver_add += 4f;
			this.characterDataModifier.health_mult *= 1.3f;
			this.characterStatModifiersModifier.lifeSteal_mult += 0.5f;
			this.gunStatModifier.damage_mult *= 1.2f;
			this.gunStatModifier.projectileColor += new Color(1f, 0f, 1f, 1f);
			List<ObjectsToSpawn> list = this.gunStatModifier.objectsToSpawn_add;
			list.Add
			(
				new ObjectsToSpawn()
				{
					AddToProjectile = new GameObject("Buwwet UwU", new Type[]
					{
						typeof(PurpleBulletMono)
					})
				}
			);
			this.gunStatModifier.objectsToSpawn_add = list;
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 6)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;

		public PurpleBulletMono buwwet;


	}

	public class PurpleBulletMono : RayHitEffect
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00017E5B File Offset: 0x0001605B
		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00017E6C File Offset: 0x0001606C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			ShockMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<ShockMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{
				component.TakeDamageOverTime(componentInParent.damage * this.transform.forward * 1.1f, this.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}


		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		[Header("Settings")]
		public float time = 1f;

		public float interval = 0.2f;

		public Color color = Color.magenta;
	}

	public class PinkMono : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 0.6f;
			this.blockModifier.cdMultiplier_mult *= 0.5f;
			this.blockModifier.additionalBlocks_add += 1;
			List<ObjectsToSpawn> list = this.gunStatModifier.objectsToSpawn_add;
			ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add
			(
				objectsToSpawn
			);
			this.gunStatModifier.reflects_add += 6;
			this.gunStatModifier.objectsToSpawn_add = list;
			this.gunStatModifier.projectileColor += new Color(1f, 0.7f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();

				if (this.effect.mode != 7)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0.7f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private UnicornMono effect;


	}

	public class GravityMono : MonoBehaviour
	{
		private readonly float maxDistance = 8f;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private Action<BlockTrigger.BlockTriggerType> gravy;
		private Action<BlockTrigger.BlockTriggerType> basic;
		private static GameObject gravityvisual = null;
		private readonly float updateDelay = 0.1f;
		private readonly float effectCooldown = 0.1f;
		private float startTime;
		private float timeOfLastEffect;
		private bool canTrigger;
		private bool hasTriggered;
		public int numcheck = 0;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;

			bool flag = this.block;
			if (flag)
			{
				this.gravy = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.gravy);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Gravity")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck > 0)
				{
					this.ResetTimer();

					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!block.objectsToSpawn.Contains(GravityMono.gravityVisual))
						{
							block.objectsToSpawn.Add(GravityMono.gravityVisual);
						}
						this.canTrigger = true;
					}

					else
					{
						if (block.objectsToSpawn.Contains(GravityMono.gravityVisual))
						{
							block.objectsToSpawn.Remove(GravityMono.gravityVisual);
						}

					}
				}

				else
                {
					UnityEngine.Object.Destroy(this);
                }
			}

		}


		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					Vector2 a = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = array[i].playerID == player.playerID;
						if (!flag2)
						{
							bool flag3 = Vector2.Distance(a, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee;
							if (flag3)
							{
								HealthHandler component = array[i].transform.GetComponent<HealthHandler>();
								if (this.canTrigger)
								{
									component.TakeDamage(15f * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
									component.TakeForce(new Vector2(52f * component.transform.position.x, 52f * component.transform.position.y) * 12f, ForceMode2D.Impulse, false, false, 0.5f);
									this.hasTriggered = true;
								}
							}
						}
					}
					if (this.hasTriggered)
					{
						this.hasTriggered = false;
						this.canTrigger = false;
						this.ResetEffectTimer();
					}
				}
			};
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			numcheck = 0;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		public static GameObject gravityVisual
		{
			get
			{
				bool flag = GravityMono.gravityvisual != null;
				GameObject result;
				if (flag)
				{
					result = GravityMono.gravityvisual;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					GravityMono.gravityvisual = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					GravityMono.gravityvisual.name = "E_Gravity";
					UnityEngine.Object.DontDestroyOnLoad(GravityMono.gravityvisual);
					foreach (ParticleSystem particleSystem in GravityMono.gravityvisual.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = Color.magenta;
					}
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.magenta, 0f)
					};
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.transform.GetChild(2).gameObject);
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.GetComponent<FollowPlayer>());
					GravityMono.gravityvisual.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(GravityMono.gravityvisual.GetComponent<RemoveAfterSeconds>());
					GravitySpawner grav = GravityMono.gravityvisual.AddComponent<GravityMono.GravitySpawner>();
					result = GravityMono.gravityvisual;
				}
				return result;
			}
			set
			{
			}
		}
		private class GravitySpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 6f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}
	}

	public class IgniteMono : MonoBehaviour
	{
		private readonly float maxDistance = 8f;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private Action<BlockTrigger.BlockTriggerType> igi;
		private Action<BlockTrigger.BlockTriggerType> basic;
		private static GameObject ignitevisual = null;
		private readonly float updateDelay = 0.1f;
		private readonly float effectCooldown = 0.5f;
		private float startTime;
		private float timeOfLastEffect;
		private bool canTrigger;
		private bool hasTriggered;
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;
		public int numcheck = 0;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;

			bool flag = this.block;
			if (flag)
			{
				this.igi = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.igi);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Ignite")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!block.objectsToSpawn.Contains(IgniteMono.ignitevisual))
						{
							block.objectsToSpawn.Add(IgniteMono.ignitevisual);
						}
						this.canTrigger = true;
					}

					else
					{
						if (block.objectsToSpawn.Contains(IgniteMono.ignitevisual))
						{
							block.objectsToSpawn.Remove(IgniteMono.ignitevisual);
						}

					}
				}

                else
                {
					UnityEngine.Object.Destroy(this);
                }

			}

		}


		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					Vector2 a = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						bool flag2 = array[i].playerID == player.playerID;
						if (!flag2)
						{
							bool flag3 = Vector2.Distance(a, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee;
							if (flag3)
							{
								CharacterData componentdata = array[i].gameObject.GetComponent<CharacterData>();
								Player component = array[i].gameObject.GetComponent<Player>();
								HealthHandler health = array[i].gameObject.GetComponent<HealthHandler>();
								if (this.canTrigger)
								{
									component.gameObject.AddComponent<IgniteEffect>();
									this.hasTriggered = true;
								}
							}
						}
					}
					if (this.hasTriggered)
					{
						this.hasTriggered = false;
						this.canTrigger = false;
						this.ResetEffectTimer();
					}
				}
			};
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			numcheck = 0;
		}
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		public static GameObject igniteVisual
		{
			get
			{
				bool flag = IgniteMono.ignitevisual != null;
				GameObject result;
				if (flag)
				{
					result = IgniteMono.ignitevisual;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					IgniteMono.ignitevisual = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					IgniteMono.ignitevisual.name = "E_Ignite";
					UnityEngine.Object.DontDestroyOnLoad(IgniteMono.ignitevisual);
					foreach (ParticleSystem particleSystem in IgniteMono.ignitevisual.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.4f, 0f, 1f);
					}
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.transform.GetChild(2).gameObject);
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.GetComponent<FollowPlayer>());
					IgniteMono.ignitevisual.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(IgniteMono.ignitevisual.GetComponent<RemoveAfterSeconds>());
					IgniteSpawner ig = IgniteMono.ignitevisual.AddComponent<IgniteMono.IgniteSpawner>();
					result = IgniteMono.ignitevisual;
				}
				return result;
			}
			set
			{
			}
		}
		private class IgniteSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 6f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}
	}

	public class IgniteEffect : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 0.65f;
			this.heat = this.player.transform.GetComponent<HealthHandler>();
			this.dat = this.player.transform.GetComponent<CharacterData>();
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.heat.DoDamage(dat.maxHealth/5 * 0.05f * Vector2.down, player.transform.position, this.color, this.player.data.weaponHandler.gameObject, this.player, false, true, true);
				this.ResetTimer();
                this.count += 0.1f;
				if (count >= 2f)
                {
					this.Destroy();
                }

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

        }

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0.3f, 0f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private HealthHandler heat;

		private CharacterData dat;

		private float count;


	}

	public class FaeEmbersMono : MonoBehaviour
	{
		private readonly float maxDistance = 8f;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private readonly float updateDelay = 0.1f;
		private float effectCooldown = 0.5f;
		private float startTime;
		private float timeOfLastEffect;
		private static GameObject faeVisu = null;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				if (this.player.data.weaponHandler.gun.isReloading)
				{

					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
                    {
						UnityEngine.Object.Instantiate<GameObject>(FaeEmbersMono.faeVisual, player.gameObject.transform.position, Quaternion.identity);
						if (this.effectCooldown > 0.3f)
                        {
							this.effectCooldown -= 0.05f;
                        };
						Vector2 a = player.transform.position;
						Player[] array = PlayerManager.instance.players.ToArray();
						for (int i = 0; i < array.Length; i++)
						{
							bool flag2 = array[i].playerID == player.playerID;
							if (!flag2)
							{
								bool flag3 = Vector2.Distance(a, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee;
								bool flag4 = Vector2.Distance(a, array[i].transform.position) < this.maxDistance * 0.5f && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee;
								if (flag4)
								{
									HealthHandler component = array[i].transform.GetComponent<HealthHandler>();
									component.DoDamage(6f * Vector2.down, array[i].transform.position, new Color(1f, 0.5f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
								}
								else if (flag3)
								{
									HealthHandler component = array[i].transform.GetComponent<HealthHandler>();
									component.DoDamage(3f * Vector2.down, array[i].transform.position, new Color(1f, 0.5f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
								}
							}
						}
						this.ResetEffectTimer();
					}
				}
                else
                {
					this.effectCooldown = 0.5f;
				}
				this.ResetTimer();
			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		public static GameObject faeVisual
		{
			get
			{
				bool flag = FaeEmbersMono.faeVisu != null;
				GameObject result;
				if (flag)
				{
					result = FaeEmbersMono.faeVisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					FaeEmbersMono.faeVisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					FaeEmbersMono.faeVisu.name = "E_Fae";
					UnityEngine.Object.DontDestroyOnLoad(FaeEmbersMono.faeVisu);
					foreach (ParticleSystem particleSystem in FaeEmbersMono.faeVisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.5f, 1f, 1f);
					}
					FaeEmbersMono.faeVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.5f, 1f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.transform.GetChild(2).gameObject);
					FaeEmbersMono.faeVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					FaeEmbersMono.faeVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<FollowPlayer>());
					FaeEmbersMono.faeVisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<RemoveAfterSeconds>());
					FaeSpawner fs = FaeEmbersMono.faeVisu.AddComponent<FaeEmbersMono.FaeSpawner>();
					result = FaeEmbersMono.faeVisu;
				}
				return result;
			}
			set
			{
			}
		}

		private class FaeSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 3f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 10f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 1f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}


	}

	public class CareenMono : BounceTrigger
	{
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();
			this.bounceEffects = base.GetComponents<BounceEffect>();
			RayHitReflect componentInParent = base.GetComponentInParent<RayHitReflect>();
			componentInParent.reflectAction = (Action<HitInfo>)Delegate.Combine(componentInParent.reflectAction, new Action<HitInfo>(this.Reflect));

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public new void Reflect(HitInfo hit)
		{
			for (int i = 0; i < this.bounceEffects.Length; i++)
			{
				this.bounceEffects[i].DoBounce(hit);
			}

			this.caree++;
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				if (this.move.velocity.y < 0f && this.caree == 1)
				{
						this.move.velocity.y = 0f;
				}
				this.move.velocity.z = 0f;
				this.ResetTimer();
			}
		}

		public int caree = 1;

		public Player player;

		private MoveTransform move;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private BounceEffect[] bounceEffects;

	}

	public class AsteroidMono : MonoBehaviour
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		private void Start()
		{
			this.move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<SyncProjectile>().active = true;
			this.ResetTimer();

		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && this.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.x != 0f)
				{
					this.move.velocity.x *= 1.1f;
				};

				if (this.move.velocity.y != 0f)
				{
					this.move.velocity.y *= 1.1f;
				}

				this.move.velocity.z = 0f;

			}

		}


		private MoveTransform move;

		private readonly float updateDelay = 0.1f;

		private float startTime;
	}

	public class PulsarMono : HitSurfaceEffect
	{
		private void Awake()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
		}

		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			UnityEngine.Object.Instantiate<GameObject>(PulsarMono.pulsarVisual, position, Quaternion.identity);
			Vector2 a = position;
			Player[] array = PlayerManager.instance.players.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
					bool flag = Vector2.Distance(a, array[i].transform.position) < 7f;
					if (flag)
					{
					CharacterData componentdata = array[i].gameObject.GetComponent<CharacterData>();
					Player component = array[i].gameObject.GetComponent<Player>();
					HealthHandler health = array[i].gameObject.GetComponent<HealthHandler>();
					component.gameObject.AddComponent<PulsarEffect>();
					Vector3 norm = position.normalized;
					component.data.healthHandler.DoDamage(10f * Vector2.down, player.transform.position, new Color(1f, 0.6f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
					array[i].transform.position += Vector3.ClampMagnitude(norm - array[i].transform.position, 5f);

					}
			}
		}

		public static GameObject pulsarVisual
		{
			get
			{
				bool flag = PulsarMono.pulseVisu != null;
				GameObject result;
				if (flag)
				{
					result = PulsarMono.pulseVisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					PulsarMono.pulseVisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					PulsarMono.pulseVisu.name = "E_Pulsar";
					UnityEngine.Object.DontDestroyOnLoad(PulsarMono.pulseVisu);
					foreach (ParticleSystem particleSystem in PulsarMono.pulseVisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.6f, 1f, 1f);
					}
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.6f, 1f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.transform.GetChild(2).gameObject);
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.GetComponent<FollowPlayer>());
					PulsarMono.pulseVisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(PulsarMono.pulseVisu.GetComponent<RemoveAfterSeconds>());
					PulsarSpawner ps = PulsarMono.pulseVisu.AddComponent<PulsarMono.PulsarSpawner>();
					result = PulsarMono.pulseVisu;
				}
				return result;
			}
			set
			{
			}
		}
		private class PulsarSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 1f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.1f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private static GameObject pulseVisu = null;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		public Color col;
	}

	public class PulsarEffect : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 0.5f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (count >= 1.5f)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 0.6f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private float count;


	}

	public class GlueMono : HitSurfaceEffect
	{
		private void Awake()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
		}

		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			UnityEngine.Object.Instantiate<GameObject>(GlueMono.glueVisual, position, Quaternion.identity);
			Vector2 a = position;
			Player[] array = PlayerManager.instance.players.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = Vector2.Distance(a, array[i].transform.position) < 7f;
				if (flag)
				{
					CharacterData componentdata = array[i].gameObject.GetComponent<CharacterData>();
					Player component = array[i].gameObject.GetComponent<Player>();
					HealthHandler health = array[i].gameObject.GetComponent<HealthHandler>();
					component.gameObject.AddComponent<GlueEffect>();

				}
			}
		}

		public static GameObject glueVisual
		{
			get
			{
				bool flag = GlueMono.glueVisu != null;
				GameObject result;
				if (flag)
				{
					result = GlueMono.glueVisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					GlueMono.glueVisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					GlueMono.glueVisu.name = "E_Pulsar";
					UnityEngine.Object.DontDestroyOnLoad(GlueMono.glueVisu);
					foreach (ParticleSystem particleSystem in GlueMono.glueVisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 1f, 1f, 1f);
					}
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 1f, 1f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(GlueMono.glueVisu.transform.GetChild(2).gameObject);
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(GlueMono.glueVisu.GetComponent<FollowPlayer>());
					GlueMono.glueVisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(GlueMono.glueVisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(GlueMono.glueVisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(GlueMono.glueVisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(GlueMono.glueVisu.GetComponent<RemoveAfterSeconds>());
					GlueSpawner gs = GlueMono.glueVisu.AddComponent<GlueMono.GlueSpawner>();
					result = GlueMono.glueVisu;
				}
				return result;
			}
			set
			{
			}
		}
		private class GlueSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 1f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.1f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private static GameObject glueVisu = null;
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		public Color col;
	}

	public class GlueEffect : ReversibleEffect
	{


		public override void OnOnEnable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult *= 0.45f;
			this.characterStatModifiersModifier.jump_mult *= 0.6f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (count >= 1f)
				{
					this.Destroy();
				}

				if (this.GetComponent<Player>().data.dead == true | this.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.Destroy();
				}

			}

		}

		public override void OnOnDisable()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}

		public override void OnOnDestroy()
		{
			bool flag = this.colorEffect != null;
			if (flag)
			{
				this.colorEffect.Destroy();
			}
		}
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		private readonly Color color = new Color(1f, 1f, 1f, 1f);

		private ReversibleColorEffect colorEffect = null;

		private readonly float updateDelay = 0.1f;

		private float startTime;

		private float count;


	}

	public class AquaRingMono : MonoBehaviour
	{
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private Action<BlockTrigger.BlockTriggerType> aqua;
		private Action<BlockTrigger.BlockTriggerType> basic;
		private static GameObject aquavisual = null;
		private readonly float updateDelay = 0.1f;
		private float startTime;
		public int numcheck = 0;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.basic = this.block.BlockAction;

			bool flag = this.block;
			if (flag)
			{
				this.aqua = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.aqua);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					GameObject gameObject = new GameObject();
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 3f;
					AquaRing aquaRing = gameObject.AddComponent<AquaRing>();
					if (AquaRingMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(AquaRingMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
					new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
					new GradientColorKey(new Color(0.1f, 0.9f, 1f, 1f), 0f)
						},
						mode = GradientMode.Fixed
					};
					componentInChildren.widthMultiplier = 2f;
					componentInChildren.radius = 5.5f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 5f;
					main.startSpeed = 10f;
					main.startLifetime = 0.5f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = ParticleSystemShapeType.Circle;
					shape.radius = 0.5f;
					shape.radiusThickness = 1f;
					ParticleSystemRenderer componentInChildren2 = gameObject2.GetComponentInChildren<ParticleSystemRenderer>();
					componentInChildren2.material.color = new Color(0.1f, 0.9f, 1f, 1f);
					foreach (ParticleSystem particleSystem1 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0.1f, 0.9f, 1f, 1f);
					}
				}
			};
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Aqua Ring")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck < 1)
				{
					UnityEngine.Object.Destroy(this);

				}

			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			numcheck = 0;
		}

		private void FindLineEffect()
		{
			UnityEngine.Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			AquaRingMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		public static GameObject aquaVisual
		{
			get
			{
				bool flag = AquaRingMono.aquavisual != null;
				GameObject result;
				if (flag)
				{
					result = AquaRingMono.aquavisual;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					AquaRingMono.aquavisual = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					AquaRingMono.aquavisual.name = "E_Aqua";
					UnityEngine.Object.DontDestroyOnLoad(AquaRingMono.aquavisual);
					foreach (ParticleSystem particleSystem in AquaRingMono.aquavisual.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0.1f, 0.9f, 1f, 1f);
					}
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.1f, 0.9f, 1f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.transform.GetChild(2).gameObject);
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.GetComponent<FollowPlayer>());
					AquaRingMono.aquavisual.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(AquaRingMono.aquavisual.GetComponent<RemoveAfterSeconds>());
					AquaSpawner aq = AquaRingMono.aquavisual.AddComponent<AquaRingMono.AquaSpawner>();
					result = AquaRingMono.aquavisual;
				}
				return result;
			}
			set
			{
			}
		}
		private class AquaSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private static GameObject lineEffect = null;

	}

	public class AquaRing : MonoBehaviour
	{
		private readonly float updateDelay = 0.2f;
		private float startTime = 0;
		private float counter = 0;

		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Vector2 a = this.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					bool flag = Vector2.Distance(a, array[i].transform.position) <= 6f;
					if (flag)
					{
						CharacterData dat = array[i].gameObject.GetComponent<CharacterData>();
						float dam = dat.maxHealth * 0.015f;
						HealthHandler health = array[i].gameObject.GetComponent<HealthHandler>();
						health.Heal(dam);
						
					}
				}

				this.ResetTimer();
				counter += 0.1f;
				if (counter >= 2f)
				{
					UnityEngine.Object.Instantiate<GameObject>(AquaRingMono.aquaVisual, this.gameObject.transform.position, Quaternion.identity);
				}
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

	}

	public class QuasarMono : MonoBehaviour
	{
		public Block block;
		public Player player;
		public CharacterData data;
		public Gun gun;
		private Action<BlockTrigger.BlockTriggerType> quasa;
		private Action<BlockTrigger.BlockTriggerType> basic;
		private static GameObject quasarvisual = null;
		private static GameObject blackholevisu = null;
		private static GameObject blackholevisu2 = null;
		private readonly float updateDelay = 0.1f;
		private float startTime;
		public int numcheck = 0;

		private void Start()
		{
			this.player = this.GetComponent<Player>();
			this.block = this.GetComponent<Block>();
			this.data = this.GetComponent<CharacterData>();
			this.gun = this.GetComponent<Gun>();
			this.basic = this.block.BlockAction;

			bool flag = this.block;
			if (flag)
			{
				this.quasa = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.quasa);
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate (BlockTrigger.BlockTriggerType trigger)
			{
				bool flag = trigger != BlockTrigger.BlockTriggerType.None;
				if (flag)
				{
					GameObject gameObject = new GameObject();
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 3f;
					Quasar quasar = gameObject.AddComponent<Quasar>();
					quasar.player = this.player;
					if (QuasarMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(QuasarMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
					new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
					new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
						},
						mode = GradientMode.Fixed
					};
					componentInChildren.widthMultiplier = 2f;
					componentInChildren.radius = 6f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 5f;
					main.startSpeed = 10f;
					main.startLifetime = 0.5f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = ParticleSystemShapeType.Circle;
					shape.radius = 0.5f;
					shape.radiusThickness = 1f;
					ParticleSystemRenderer componentInChildren2 = gameObject2.GetComponentInChildren<ParticleSystemRenderer>();
					componentInChildren2.material.color = new Color(1f, 0.4f, 0f, 1f);
					foreach (ParticleSystem particleSystem1 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.4f, 0f, 1f);
					}
				}
			};
		}

		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Quasar")
					{
						this.numcheck += 1;
					}

					i++;
				}

				if (numcheck < 1)
				{
					UnityEngine.Object.Destroy(this);

				}

			}

		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
			numcheck = 0;
		}

		private void FindLineEffect()
		{
			UnityEngine.Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			QuasarMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		public static GameObject quasarVisual
		{
			get
			{
				bool flag = QuasarMono.quasarvisual != null;
				GameObject result;
				if (flag)
				{
					result = QuasarMono.quasarvisual;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					QuasarMono.quasarvisual = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.quasarvisual.name = "E_Quasar";
					UnityEngine.Object.DontDestroyOnLoad(QuasarMono.quasarvisual);
					foreach (ParticleSystem particleSystem in QuasarMono.quasarvisual.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.4f, 0f, 1f);
					}
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.transform.GetChild(2).gameObject);
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.GetComponent<FollowPlayer>());
					QuasarMono.quasarvisual.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(QuasarMono.quasarvisual.GetComponent<RemoveAfterSeconds>());
					QuasarSpawner qs = QuasarMono.quasarvisual.AddComponent<QuasarMono.QuasarSpawner>();
					result = QuasarMono.quasarvisual;
				}
				return result;
			}
			set
			{
			}
		}
		public static GameObject blackholeVisual
		{
			get
			{
				bool flag = QuasarMono.blackholevisu != null;
				GameObject result;
				if (flag)
				{
					result = QuasarMono.blackholevisu;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					QuasarMono.blackholevisu = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.blackholevisu.name = "E_Blackhole";
					UnityEngine.Object.DontDestroyOnLoad(QuasarMono.blackholevisu);
					foreach (ParticleSystem particleSystem in QuasarMono.blackholevisu.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0f, 0f, 0f, 1f);
					}
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0f, 0f, 0f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.transform.GetChild(2).gameObject);
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.GetComponent<FollowPlayer>());
					QuasarMono.blackholevisu.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu.GetComponent<RemoveAfterSeconds>());
					BlackHoleSpawner bh = QuasarMono.blackholevisu.AddComponent<QuasarMono.BlackHoleSpawner>();
					result = QuasarMono.blackholevisu;
				}
				return result;
			}
			set
			{
			}
		}

		public static GameObject blackholeVisual2
		{
			get
			{
				bool flag = QuasarMono.blackholevisu2 != null;
				GameObject result;
				if (flag)
				{
					result = QuasarMono.blackholevisu2;
				}
				else
				{
					List<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					List<CardInfo> source = first.Concat(second).ToList<CardInfo>();
					GameObject original = (from card in source
										   where card.cardName.ToLower() == "overpower"
										   select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0];
					QuasarMono.blackholevisu2 = UnityEngine.Object.Instantiate<GameObject>(original, new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.blackholevisu2.name = "E_Blackhole2";
					UnityEngine.Object.DontDestroyOnLoad(QuasarMono.blackholevisu2);
					foreach (ParticleSystem particleSystem in QuasarMono.blackholevisu2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0f, 0f, 0f, 1f);
					}
					QuasarMono.blackholevisu2.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0f, 0f, 0f, 1f), 0f)
					};
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.transform.GetChild(1).gameObject);
					QuasarMono.blackholevisu2.transform.GetChild(2).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.blackholevisu2.transform.GetChild(2).GetComponent<LineEffect>().playOnAwake = true;
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.GetComponent<FollowPlayer>());
					QuasarMono.blackholevisu2.GetComponent<DelayEvent>().time = 0f;
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.GetComponent<SoundUnityEventPlayer>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.GetComponent<Explosion>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.GetComponent<Explosion_Overpower>());
					UnityEngine.Object.Destroy(QuasarMono.blackholevisu2.GetComponent<RemoveAfterSeconds>());
					BlackHoleSpawner bh2 = QuasarMono.blackholevisu2.AddComponent<QuasarMono.BlackHoleSpawner>();
					result = QuasarMono.blackholevisu2;
				}
				return result;
			}
			set
			{
			}
		}

		private class QuasarSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.7f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private class BlackHoleSpawner : MonoBehaviour
		{
			private void Start()
			{
				bool flag = !(this.gameObject.GetComponent<SpawnedAttack>().spawner != null);
				if (!flag)
				{
					this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("inited", false);
					typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4f;
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().SetFieldValue("startWidth", 0.5f);
					this.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
				}
			}
		}

		private static GameObject lineEffect = null;

	}

	public class Quasar : MonoBehaviour
	{
		private readonly float updateDelay = 0.3f;
		private float startTime = 0;
		private float counter = 0;
		public Player player;
		private int chance;
		private System.Random rand;

		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Vector2 a = this.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					bool flag = Vector2.Distance(a, array[i].transform.position) <= 6f;
					if (flag)
					{
						bool flag2 = array[i].playerID == player.playerID;
						if (!flag2)
						{
							HealthHandler health = array[i].gameObject.GetComponent<HealthHandler>();
							CharacterData dat = array[i].gameObject.GetComponent<CharacterData>();
							StunHandler stun = array[i].gameObject.GetComponent<StunHandler>();
							array[i].transform.position += Vector3.ClampMagnitude(this.transform.position - array[i].transform.position, 0.5f) * 1.15f;
							float dam = dat.maxHealth * 0.03f;
							health.TakeDamage(dam * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
							health.TakeForce(Vector2.MoveTowards(array[i].transform.position, a, 30f), ForceMode2D.Impulse, false, true, 0.5f);
							UnityEngine.Object.Instantiate<GameObject>(QuasarMono.blackholeVisual, this.gameObject.transform.position, Quaternion.identity);
							stun.AddStun(0.01f);

						}
					}
				}
				this.counter += 0.1f;
				if (counter >= 0.5)
                {
					UnityEngine.Object.Instantiate<GameObject>(QuasarMono.quasarVisual, this.gameObject.transform.position, Quaternion.identity);
				}
				UnityEngine.Object.Instantiate<GameObject>(QuasarMono.blackholeVisual2, this.gameObject.transform.position, Quaternion.identity);
				this.ResetTimer();
			}
		}

		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

	}

} 