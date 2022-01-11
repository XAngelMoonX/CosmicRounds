using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CR.Cards;
using CR.MonoBehaviors;


namespace CR.Extensions
{
    public static class CustomEffects
    {
        public static void DestroyAllEffects(GameObject gameObject)
        {
            CustomEffects.DestroyAllAppliedEffects(gameObject);
        }
        public static void DestroyAllAppliedEffects(GameObject gameObject)
        {
			FearFactorMono[] fear = gameObject.GetComponents<FearFactorMono>();
			ShockMono[] shock = gameObject.GetComponents<ShockMono>();
			StunMono[] stun = gameObject.GetComponents<StunMono>();
			MosquitoMono[] mosquito = gameObject.GetComponents<MosquitoMono>();
			BeetleMono[] beetle = gameObject.GetComponents<BeetleMono>();
			CriticalHitMono[] critical = gameObject.GetComponents<CriticalHitMono>();
			FlamethrowerMono[] flamethrower = gameObject.GetComponents<FlamethrowerMono>();
			SyphonMono[] syphon = gameObject.GetComponents<SyphonMono>();
			TaserMono[] tase = gameObject.GetComponents<TaserMono>();
			HolsterMono[] holster = gameObject.GetComponents<HolsterMono>();
			FlexMono[] flex = gameObject.GetComponents<FlexMono>();
			DroneMono[] drone = gameObject.GetComponents<DroneMono>();
			GoldenGlazeMono[] gold = gameObject.GetComponents<GoldenGlazeMono>();
			GoldHealthMono[] gh = gameObject.GetComponents<GoldHealthMono>();
			SugarGlazeMono[] sugar = gameObject.GetComponents<SugarGlazeMono>();
			SugarMoveMono[] sm = gameObject.GetComponents<SugarMoveMono>();
			MitosisMono[] mm = gameObject.GetComponents<MitosisMono>();
			MitosisBlockMono[] mb = gameObject.GetComponents<MitosisBlockMono>();
			MeiosisMono[] meme = gameObject.GetComponents<MeiosisMono>();
			MeiosisReloadMono[] mere = gameObject.GetComponents<MeiosisReloadMono>();
			PogoMono[] pm = gameObject.GetComponents<PogoMono>();
			CloudMono[] cloud = gameObject.GetComponents<CloudMono>();
			PulseMono[] pulse = gameObject.GetComponents<PulseMono>();
			DriveMono[] drive = gameObject.GetComponents<DriveMono>();
			CriticalMono[] crit = gameObject.GetComponents<CriticalMono>();
			DropMono[] drop = gameObject.GetComponents<DropMono>();
			SunMono[] sun = gameObject.GetComponents<SunMono>();
			RainMono[] rain = gameObject.GetComponents<RainMono>();
			RainSpawner[] rainSpawners = gameObject.GetComponents<RainSpawner>();
			MeteorMono[] meteor = gameObject.GetComponents<MeteorMono>();
			RainCollider[] rc = gameObject.GetComponents<RainCollider>();
			RainMaker[] rm = gameObject.GetComponents<RainMaker>();
			UnicornMono[] um = gameObject.GetComponents<UnicornMono>();
			RedMono[] red = gameObject.GetComponents<RedMono>();
			OrangeMono[] orange = gameObject.GetComponents<OrangeMono>();
			YellowMono[] yellow = gameObject.GetComponents<YellowMono>();
			GreenMono[] green = gameObject.GetComponents<GreenMono>();
			CyanMono[] cyan = gameObject.GetComponents<CyanMono>();
			BlueMono[] blue = gameObject.GetComponents<BlueMono>();
			PurpleMono[] purple = gameObject.GetComponents<PurpleMono>();
			PurpleBulletMono[] purpleBullets = gameObject.GetComponents<PurpleBulletMono>();
			PinkMono[] pink = gameObject.GetComponents<PinkMono>();
			GravityMono[] gravity = gameObject.GetComponents<GravityMono>();
			IgniteMono[] ignite = gameObject.GetComponents<IgniteMono>();
			IgniteEffect[] ie = gameObject.GetComponents<IgniteEffect>();
			FaeEmbersMono[] fae = gameObject.GetComponents<FaeEmbersMono>();
			CareenMono[] careen = gameObject.GetComponents<CareenMono>();
			CometMono[] cm = gameObject.GetComponents<CometMono>();
			AsteroidMono[] aster = gameObject.GetComponents<AsteroidMono>();
			PulsarMono[] pul = gameObject.GetComponents<PulsarMono>();
			GlueMono[] glue = gameObject.GetComponents<GlueMono>();
			AquaRingMono[] aqu = gameObject.GetComponents<AquaRingMono>();
			QuasarMono[] quas = gameObject.GetComponents<QuasarMono>();

			foreach (FearFactorMono fearmono in fear)
			{
				bool flag = fearmono != null;
				if (flag)
				{
					fearmono.componentInChildren.Stop();
					UnityEngine.Object.Destroy(fearmono.factorObject);
					UnityEngine.Object.Destroy(fearmono.fearEffect);
					UnityEngine.Object.Destroy(fearmono.componentInChildren);
					fearmono.Destroy();
				}
			}

			foreach (ShockMono shockmono in shock)
			{
				bool flag = shockmono != null;
				if (flag)
				{
					shockmono.Destroy();
				}
			}

			foreach (StunMono stunmono in stun)
			{
				bool flag = stunmono != null;
				if (flag)
				{
					stunmono.Destroy();
				}
			}

			foreach (MosquitoMono mosquitomono in mosquito)
			{
				bool flag = mosquitomono != null;
				if (flag)
				{
					mosquitomono.Destroy();
				}
			}

			foreach (BeetleMono beetleMono in beetle)
			{
				bool flag = beetleMono != null;
				if (flag)
				{
					beetleMono.Destroy();
				}
			}

			foreach (CriticalHitMono criticalHit in critical)
			{
				bool flag = criticalHit != null;
				if (flag)
				{
					criticalHit.Destroy();
				}
			}

			foreach (FlamethrowerMono flamethrowerMono in flamethrower)
			{
				bool flag = flamethrowerMono != null;
				if (flag)
				{
					flamethrowerMono.Destroy();
				}
			}

			foreach (SyphonMono syphonMono in syphon)
			{
				bool flag = syphonMono != null;
				if (flag)
				{
					syphonMono.Destroy();
				}
			}

			foreach (TaserMono taserMono in tase)
			{
				bool flag = taserMono != null;
				if (flag)
				{
					taserMono.Destroy();
				}
			}

			foreach (HolsterMono holsterMono in holster)
			{
				bool flag = holsterMono != null;
				if (flag)
				{
					holsterMono.Destroy();
				}
			}

			foreach (FlexMono flexMono in flex)
			{
				bool flag = flexMono != null;
				if (flag)
				{
					flexMono.Destroy();
				}
			}

			foreach (DroneMono droneMono in drone)
			{
				bool flag = droneMono != null;
				if (flag)
				{
					droneMono.Destroy();
				}
			}

			foreach (GoldenGlazeMono goldMono in gold)
			{
				bool flag = goldMono != null;
				if (flag)
				{
					goldMono.Destroy();
				}
			}

			foreach (GoldHealthMono goldHealth in gh)
			{
				bool flag = goldHealth != null;
				if (flag)
				{
					goldHealth.Destroy();
				}
			}

			foreach (SugarGlazeMono sugarGlaze in sugar)
			{
				bool flag = sugarGlaze != null;
				if (flag)
				{
					sugarGlaze.Destroy();
				}
			}

			foreach (SugarMoveMono sugarMove in sm)
			{
				bool flag = sugarMove != null;
				if (flag)
				{
					sugarMove.Destroy();
				}
			}

			foreach (MitosisMono mitosisMono in mm)
			{
				bool flag = mitosisMono != null;
				if (flag)
				{
					mitosisMono.Destroy();
				}
			}

			foreach (MitosisBlockMono mitosisBlock in mb)
			{
				bool flag = mitosisBlock != null;
				if (flag)
				{
					mitosisBlock.Destroy();
				}
			}

			foreach (MeiosisMono meiosisMono in meme)
			{
				bool flag = meiosisMono != null;
				if (flag)
				{
					meiosisMono.Destroy();
				}
			}

			foreach (MeiosisReloadMono meiosisReload in mere)
			{
				bool flag = meiosisReload != null;
				if (flag)
				{
					meiosisReload.Destroy();
				}
			}

			foreach (PogoMono pogoMono in pm)
			{
				bool flag = pogoMono != null;
				if (flag)
				{
					pogoMono.Destroy();
				}
			}

			foreach (CloudMono cloudMono in cloud)
			{
				bool flag = cloudMono != null;
				if (flag)
				{
					cloudMono.Destroy();
				}
			}

			foreach (PulseMono pulseMono in pulse)
			{
				bool flag = pulseMono != null;
				if (flag)
				{
					pulseMono.Destroy();
				}
			}

			foreach (DriveMono driveMono in drive)
			{
				bool flag = driveMono != null;
				if (flag)
				{
					driveMono.Destroy();
				}
			}

			foreach (CriticalMono cri in crit)
			{
				bool flag = cri != null;
				if (flag)
				{
					cri.Destroy();
				}
			}

			foreach (DropMono dro in drop)
			{
				bool flag = dro != null;
				if (flag)
				{
					dro.Destroy();
				}
			}

			foreach (SunMono tsun in sun)
			{
				bool flag = tsun != null;
				if (flag)
				{
					tsun.Destroy();
				}
			}

			foreach (RainMono rai in rain)
			{
				bool flag = rai != null;
				if (flag)
				{
					rai.Destroy();
				}
			}

			foreach (RainSpawner rainSpawner in rainSpawners)
			{
				bool flag = rainSpawner != null;
				if (flag)
				{
					rainSpawner.Destroy();
				}
			}

			foreach (MeteorMono meteorMono in meteor)
			{
				bool flag = meteorMono != null;
				if (flag)
				{
					meteorMono.Destroy();
				}
			}

			foreach (RainCollider rainCollider in rc)
			{
				bool flag = rainCollider != null;
				if (flag)
				{
					rainCollider.Destroy();
				}
			}

			foreach (RainMaker rainMaker in rm)
			{
				bool flag = rainMaker != null;
				if (flag)
				{
					rainMaker.Destroy();
				}
			}

			foreach (CometMono cometMono in cm)
			{
				bool flag = cometMono != null;
				if (flag)
				{
					cometMono.Destroy();
				}
			}

			foreach (UnicornMono unicornMono in um)
			{
				bool flag = unicornMono != null;
				if (flag)
				{
					unicornMono.Destroy();
				}
			}

			foreach (RedMono redMono in red)
			{
				bool flag = redMono != null;
				if (flag)
				{
					redMono.Destroy();
				}
			}

			foreach (OrangeMono orangeMono in orange)
			{
				bool flag = orangeMono != null;
				if (flag)
				{
					orangeMono.Destroy();
				}
			}

			foreach (YellowMono yellowMono in yellow)
			{
				bool flag = yellowMono != null;
				if (flag)
				{
					yellowMono.Destroy();
				}
			}

			foreach (GreenMono greenMono in green)
			{
				bool flag = greenMono != null;
				if (flag)
				{
					greenMono.Destroy();
				}
			}

			foreach (CyanMono cyanMono in cyan)
			{
				bool flag = cyanMono != null;
				if (flag)
				{
					cyanMono.Destroy();
				}
			}

			foreach (BlueMono blueMono in blue)
			{
				bool flag = blueMono != null;
				if (flag)
				{
					blueMono.Destroy();
				}
			}

			foreach (PurpleMono purpleMono in purple)
			{
				bool flag = purpleMono != null;
				if (flag)
				{
					purpleMono.Destroy();
				}
			}

			foreach (PurpleBulletMono purpleBulletMono in purpleBullets)
			{
				bool flag = purpleBulletMono != null;
				if (flag)
				{
					purpleBulletMono.Destroy();
				}
			}

			foreach (PinkMono pinkMono in pink)
			{
				bool flag = pinkMono != null;
				if (flag)
				{
					pinkMono.Destroy();
				}
			}

			foreach (GravityMono gravityMono in gravity)
			{
				bool flag = gravityMono != null;
				if (flag)
				{
					gravityMono.Destroy();
				}
			}

			foreach (IgniteMono igniteMono in ignite)
			{
				bool flag = igniteMono != null;
				if (flag)
				{
					igniteMono.Destroy();
				}
			}

			foreach (IgniteEffect igniteEffect in ie)
			{
				bool flag = igniteEffect != null;
				if (flag)
				{
					igniteEffect.Destroy();
				}
			}

			foreach (FaeEmbersMono faeEmbersMono in fae)
			{
				bool flag = faeEmbersMono != null;
				if (flag)
				{
					faeEmbersMono.Destroy();
				}
			}

			foreach (CareenMono careenMono in careen)
			{
				bool flag = careenMono != null;
				if (flag)
				{
					careenMono.Destroy();
				}
			}

			foreach (AsteroidMono asteroidMono in aster)
			{
				bool flag = asteroidMono != null;
				if (flag)
				{
					asteroidMono.Destroy();
				}
			}

			foreach (PulsarMono pulsarMono in pul)
			{
				bool flag = pulsarMono != null;
				if (flag)
				{
					pulsarMono.Destroy();
				}
			}

			foreach (GlueMono glueMono in glue)
			{
				bool flag = glueMono != null;
				if (flag)
				{
					glueMono.Destroy();
				}
			}

			foreach (AquaRingMono aquaRing in aqu)
			{
				bool flag = aquaRing != null;
				if (flag)
				{
					aquaRing.Destroy();
				}
			}

			foreach (QuasarMono quasarMono in quas)
			{
				bool flag = quasarMono != null;
				if (flag)
				{
					quasarMono.Destroy();
				}
			}

		}

    }
}
