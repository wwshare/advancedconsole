using System;
using HarmonyLib;
using UnityEngine;

namespace AdvancedConsole;

[HarmonyPatch]
public static class CharacterItemsPatch
{
	[HarmonyPatch(typeof(CharacterItems), "UpdateBalloonCount")]
	[HarmonyPrefix]
	public static bool UpdateBalloonCount_Prefix(CharacterItems __instance, ItemSlot[] slots)
	{
		try
		{
			if ((UnityEngine.Object)(object)__instance?.character?.refs?.items == (UnityEngine.Object)null || (UnityEngine.Object)(object)__instance.character.refs.balloons == (UnityEngine.Object)null)
			{
				return false;
			}
			if (__instance.character.refs.items.currentSelectedSlot.IsSome)
			{
				if ((UnityEngine.Object)(object)__instance.character.player == (UnityEngine.Object)null)
				{
					__instance.character.refs.balloons.heldBalloonCount = 0;
					return false;
				}
				ItemSlot itemSlot = __instance.character.player.GetItemSlot(__instance.character.refs.items.currentSelectedSlot.Value);
				if (itemSlot != null && !itemSlot.IsEmpty() && (UnityEngine.Object)(object)itemSlot.prefab != (UnityEngine.Object)null && __instance.character.refs.items.currentSelectedSlot.IsSome && (UnityEngine.Object)(object)((Component)itemSlot.prefab).GetComponent<Balloon>() != (UnityEngine.Object)null)
				{
					__instance.character.refs.balloons.heldBalloonCount = 1;
				}
				else
				{
					__instance.character.refs.balloons.heldBalloonCount = 0;
				}
			}
			else
			{
				__instance.character.refs.balloons.heldBalloonCount = 0;
			}
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in UpdateBalloonCount patch: " + ex.Message));
			if ((UnityEngine.Object)(object)__instance?.character?.refs?.balloons != (UnityEngine.Object)null)
			{
				__instance.character.refs.balloons.heldBalloonCount = 0;
			}
			return false;
		}
	}
}
