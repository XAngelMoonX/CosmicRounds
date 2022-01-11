using System;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using CR.MonoBehaviors;
using CR.Extensions;

namespace CR.Patches
{

	// Token: 0x02000015 RID: 21
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


	[HarmonyPatch(typeof(Gun), "FullReset")]
	internal class GunPatchFullReset
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000A258 File Offset: 0x00008458
		private static void Prefix(Gun __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
			__instance.player.data.currentCards = new List<CardInfo>();
		}
	}


	[HarmonyPatch(typeof(Block), "FullReset")]
	internal class BlockPatchFullReset
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00009C40 File Offset: 0x00007E40
		private static void Prefix(Block __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);

		}
	}


	[HarmonyPatch(typeof(CharacterStatModifiers), "FullReset")]
	internal class CharacterStatModifiersPatchFullReset
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void Prefix(CharacterStatModifiers __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(GunAmmo), "FullReset")]
	internal class GunAmmoPatchFullReset
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void Prefix(GunAmmo __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(HealthHandler), "FullReset")]
	internal class HealthHandlerPatchFullReset
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void Prefix(HealthHandler __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(Gravity), "FullReset")]
	internal class GravityPatchFullReset
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void Prefix(Gravity __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(CharacterData), "FullReset")]
	internal class CharacterDataPatchFullReset
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void Prefix(CharacterData __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}


	[HarmonyPatch(typeof(Player), "OnRemoveCard")]
	[Serializable]
	internal class PlayerPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(Gun), "OnRemoveCard")]
	[Serializable]
	internal class GunPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(Block), "OnRemoveCard")]
	[Serializable]
	internal class BlockPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(CharacterStatModifiers), "OnRemoveCard")]
	[Serializable]
	internal class CharacterStatModifiersPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(GunAmmo), "OnRemoveCard")]
	[Serializable]
	internal class GunAmmoPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(HealthHandler), "OnRemoveCard")]
	[Serializable]
	internal class HealthHandlerPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(Gravity), "OnRemoveCard")]
	[Serializable]
	internal class GravityPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}

	[HarmonyPatch(typeof(CharacterData), "OnRemoveCard")]
	[Serializable]
	internal class CharacterDataPatchOnRemoveCard
	{
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}
}
