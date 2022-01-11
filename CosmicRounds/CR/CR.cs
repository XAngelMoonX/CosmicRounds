using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using Jotunn.Utils;
using ModdingUtils.Extensions;
using ModdingUtils.Utils;
using CR.Cards;
using CR.MonoBehaviors;
using CR.Patches;
using HarmonyLib;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using UnboundLib.Utils;
using UnityEngine;




namespace CR
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("com.XAngelMoonX.rounds.CosmicRounds", "Cosmic Rounds", "1.7.4")]
    [BepInProcess("Rounds.exe")]


    public class CR : BaseUnityPlugin
    {

#if DEBUG
        public static readonly bool DEBUG = true;
#else
        public static readonly bool DEBUG = false;
#endif

        static void Main()
        {

        }

        private void Awake()
        {
           

        }

        private IEnumerator ResetEffects(IGameModeHandler gm)
        {
            this.DestroyAll<ShockMono>();
            this.DestroyAll<StunMono>();
            this.DestroyAll<MosquitoMono>();
            this.DestroyAll<BeetleMono>();
            this.DestroyAll<FearFactorMono>();
            this.DestroyAll<CriticalHitMono>();
            this.DestroyAll<FlamethrowerMono>();
            this.DestroyAll<SyphonMono>();
            this.DestroyAll<TaserMono>();
            this.DestroyAll<HolsterMono>();
            this.DestroyAll<FlexMono>();
            this.DestroyAll<DroneMono>();
            this.DestroyAll<GoldenGlazeMono>();
            this.DestroyAll<GoldHealthMono>();
            this.DestroyAll<SugarGlazeMono>();
            this.DestroyAll<SugarMoveMono>();
            this.DestroyAll<MitosisMono>();
            this.DestroyAll<MitosisBlockMono>();
            this.DestroyAll<MeiosisMono>();
            this.DestroyAll<MeiosisReloadMono>();
            this.DestroyAll<PogoMono>();
            this.DestroyAll<CloudMono>();
            this.DestroyAll<PulseMono>();
            this.DestroyAll<DriveMono>();
            this.DestroyAll<CriticalMono>();
            this.DestroyAll<DropMono>();
            this.DestroyAll<SunMono>();
            this.DestroyAll<RainMono>();
            this.DestroyAll<RainSpawner>();
            this.DestroyAll<MeteorMono>();
            this.DestroyAll<RainCollider>();
            this.DestroyAll<RainMaker>();
            this.DestroyAll<CometMono>();
            this.DestroyAll<UnicornMono>();
            this.DestroyAll<RedMono>();
            this.DestroyAll<OrangeMono>();
            this.DestroyAll<YellowMono>();
            this.DestroyAll<GreenMono>();
            this.DestroyAll<CyanMono>();
            this.DestroyAll<BlueMono>();
            this.DestroyAll<PurpleMono>();
            this.DestroyAll<PurpleBulletMono>();
            this.DestroyAll<PinkMono>();
            this.DestroyAll<GravityMono>();
            this.DestroyAll<IgniteMono>();
            this.DestroyAll<IgniteEffect>();
            this.DestroyAll<FaeEmbersMono>();
            this.DestroyAll<CareenMono>();
            this.DestroyAll<AsteroidMono>();
            this.DestroyAll<PulsarMono>();
            this.DestroyAll<GlueMono>();
            this.DestroyAll<AquaRingMono>();
            this.DestroyAll<QuasarMono>();
            yield break;
        }

        private void DestroyAll<T>() where T : UnityEngine.Object
        {
            T[] array = UnityEngine.Object.FindObjectsOfType<T>();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(array[i]);
            }
        }

        private void Start()
        {
            CR.ArtAsset = AssetUtils.LoadAssetBundleFromResources("cr_assets", typeof(CR).Assembly);
            if (CR.ArtAsset == null)
            {
                UnityEngine.Debug.Log("Failed to load CR art asset bundle");
            }

            GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
            GameModeManager.AddHook("GameStart", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
            GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
            GameModeManager.AddHook("GameStart", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));

            CustomCard.BuildCard<BeetleCard>();
            CustomCard.BuildCard<CrowCard>();
            CustomCard.BuildCard<HawkCard>();
            CustomCard.BuildCard<SpeedUpCard>();
            CustomCard.BuildCard<MosquitoCard>();
            CustomCard.BuildCard<SuperSonicCard>();
            CustomCard.BuildCard<StasisCard>();
            CustomCard.BuildCard<OnesKingCard>();
            CustomCard.BuildCard<BulletTimeCard>();
            CustomCard.BuildCard<StunCard>();
            CustomCard.BuildCard<FearFactorCard>();
            CustomCard.BuildCard<StarCard>();
            CustomCard.BuildCard<CriticalHitCard>();
            CustomCard.BuildCard<FlamethrowerCard>();
            CustomCard.BuildCard<SyphonCard>();
            CustomCard.BuildCard<DropshotCard>();
            CustomCard.BuildCard<ReconCard>();
            CustomCard.BuildCard<TaserCard>();
            CustomCard.BuildCard<HolsterCard>();
            CustomCard.BuildCard<FlexCard>();
            CustomCard.BuildCard<DroneCard>();
            CustomCard.BuildCard<SparkCard>();
            CustomCard.BuildCard<GoldenGlazeCard>();
            CustomCard.BuildCard<FocusCard>();
            CustomCard.BuildCard<SugarGlazeCard>();
            CustomCard.BuildCard<MitosisCard>();
            CustomCard.BuildCard<MeiosisCard>();
            CustomCard.BuildCard<PogoCard>();
            CustomCard.BuildCard<AllCard>();
            CustomCard.BuildCard<CloudCard>();
            CustomCard.BuildCard<PulseCard>();
            CustomCard.BuildCard<DriveCard>();
            CustomCard.BuildCard<SunCard>();
            CustomCard.BuildCard<CometCard>();
            CustomCard.BuildCard<MeteorCard>();
            CustomCard.BuildCard<UnicornCard>();
            CustomCard.BuildCard<GravityCard>();
            CustomCard.BuildCard<IgniteCard>();
            CustomCard.BuildCard<FaeEmbersCard>();
            CustomCard.BuildCard<CareenCard>();
            CustomCard.BuildCard<AsteroidCard>();
            CustomCard.BuildCard<PulsarCard>();
            CustomCard.BuildCard<GlueCard>();
            CustomCard.BuildCard<AquaRingCard>();
            CustomCard.BuildCard<QuasarCard>();
        }


        private const string ModId = "com.XAngelMoonX.rounds.CosmicRounds";
        private const string ModName = "CR";
        public const string Version = "1.7.4";
        internal static AssetBundle ArtAsset;

    }
}
