using System;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using CR.MonoBehaviors;
using CR.Extensions;

namespace CR.Patches
{

	[HarmonyPatch(typeof(Player), "FullReset")]
	[Serializable]
	internal class PlayerPatchFullReset
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
			__instance.data.currentCards = new List<CardInfo>();
		}
	}


	[HarmonyPatch(typeof(Gun), "ResetStats")]
	[Serializable]
	internal class GunPatchResetStats
	{
		private static void Prefix(Gun __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
			__instance.player.data.currentCards = new List<CardInfo>();
		}
	}


	[HarmonyPatch(typeof(Block), "ResetStats")]
	[Serializable]
	internal class BlockPatchResetStats
	{
		private static void Prefix(Block __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);

		}
	}


	[HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
	[Serializable]
	internal class CharacterStatModifiersPatchResetStats
	{
		private static void Prefix(CharacterStatModifiers __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(CustomCard), "OnRemoveCard")]
	[Serializable]
	internal class CustomCardOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}
}