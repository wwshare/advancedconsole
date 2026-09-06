using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BepInEx.Logging;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedConsole;

[HarmonyPatch]
public static class MapControlPatches
{
	private static readonly Dictionary<string, string> ValidMaps = new Dictionary<string, string>
	{
		{ "level0", "Level0" },
		{ "level1", "Level1" },
		{ "level2", "Level2" },
		{ "level3", "Level3" },
		{ "level4", "Level4" },
		{ "level5", "Level5" },
		{ "level6", "Level6" },
		{ "level7", "Level7" },
		{ "level8", "Level8" },
		{ "level9", "Level9" },
		{ "level10", "Level10" },
		{ "level11", "Level11" },
		{ "level12", "Level12" },
		{ "level13", "Level13" },
		{ "level14", "Level14" }
	};

	private static ManualLogSource Logger => Plugin.Log;

	public static string ForcedMapName { get; set; } = "";

	public static bool DisableForcedMap { get; set; } = false;

	public static bool SetForcedMap(string mapName)
	{
		if (string.IsNullOrEmpty(mapName))
		{
			ForcedMapName = "";
			Logger.LogInfo((object)"Forced map cleared");
			return true;
		}
		string key = mapName.ToLower().Replace(" ", "_");
		if (ValidMaps.TryGetValue(key, out string value))
		{
			ForcedMapName = value;
			Logger.LogInfo((object)("Forced map set to: " + ForcedMapName));
			return true;
		}
		Logger.LogWarning((object)("Unknown map name: " + mapName));
		return false;
	}

	public static List<string> GetAvailableMaps()
	{
		return new List<string>(ValidMaps.Values);
	}

	[HarmonyPatch(typeof(MapBaker), "GetLevel")]
	[HarmonyPrefix]
	public static bool MapBaker_GetLevel_Prefix(MapBaker __instance, int levelIndex, ref string __result)
	{
		try
		{
			if (DisableForcedMap)
			{
				return true;
			}
			if (PhotonNetwork.InRoom && !MasterClientUtils.IsMasterClient())
			{
				Logger.LogDebug((object)"[CLIENT] Non-master client called GetLevel - using original method");
				return true;
			}
			if (!string.IsNullOrEmpty(ForcedMapName))
			{
				__result = ForcedMapName;
				string text = (PhotonNetwork.InRoom ? "[MASTER]" : "[OFFLINE]");
				Logger.LogInfo((object)(text + " Forced map selected: " + ForcedMapName));
				ForcedMapName = "";
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Logger.LogError((object)("Error in MapBaker.GetLevel patch: " + ex.Message));
			return true;
		}
	}

	[HarmonyPatch(typeof(AirportCheckInKiosk), "BeginIslandLoadRPC")]
	[HarmonyPrefix]
	public static bool AirportCheckInKiosk_BeginIslandLoadRPC_Prefix(ref string sceneName, int ascent)
	{
		try
		{
			if (DisableForcedMap)
			{
				return true;
			}
			if (!string.IsNullOrEmpty(ForcedMapName))
			{
				string text = sceneName;
				sceneName = ForcedMapName;
				string text2 = (PhotonNetwork.IsMasterClient ? "[MASTER]" : "[CLIENT]");
				Logger.LogInfo((object)(text2 + " Forcing map change: " + text + " -> " + ForcedMapName));
				ForcedMapName = "";
			}
			return true;
		}
		catch (Exception ex)
		{
			Logger.LogError((object)("Error in BeginIslandLoadRPC patch: " + ex.Message));
			return true;
		}
	}

	[MasterClientOnly(null)]
	public static bool ForceLoadMap(string mapName)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		if (!SetForcedMap(mapName))
		{
			return false;
		}
		try
		{
			try
			{
				AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
				if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
				{
					PhotonView component = ((Component)val).GetComponent<PhotonView>();
					if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
					{
						Logger.LogInfo((object)("Method 1: Triggering map load via AirportCheckInKiosk for map: " + ForcedMapName));
						component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[2] { "Level0", 7 });
						return true;
					}
					Logger.LogWarning((object)"AirportCheckInKiosk found but no PhotonView component!");
				}
				else
				{
					Logger.LogWarning((object)"AirportCheckInKiosk not found in scene!");
				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning((object)("Method 1 failed: " + ex.Message + ". Trying method 2..."));
			}
			try
			{
				if (PhotonNetwork.InRoom)
				{
					Logger.LogInfo((object)("Method 2: Setting room properties and loading map: " + ForcedMapName));
					Hashtable val2 = new Hashtable();
					val2[(object)"scene"] = ForcedMapName;
					PhotonNetwork.CurrentRoom.SetCustomProperties(val2, (Hashtable)null, (WebFlags)null);
					PhotonNetwork.LoadLevel(ForcedMapName);
					string forcedMapName = ForcedMapName;
					ForcedMapName = "";
					Logger.LogInfo((object)("Method 2: Successfully initiated Photon map load: " + forcedMapName));
					return true;
				}
				Logger.LogWarning((object)"Method 2 failed: Not in a Photon room");
			}
			catch (Exception ex2)
			{
				Logger.LogWarning((object)("Method 2 failed: " + ex2.Message + ". Trying method 3..."));
			}
			try
			{
				string forcedMapName2 = ForcedMapName;
				ForcedMapName = "";
				Logger.LogInfo((object)("Method 3: Using Unity SceneManager to load map: " + forcedMapName2));
				SceneManager.LoadScene(forcedMapName2);
				Logger.LogInfo((object)("Method 3: Successfully initiated Unity direct map load: " + forcedMapName2));
				return true;
			}
			catch (Exception ex3)
			{
				Logger.LogError((object)("Method 3 failed: " + ex3.Message));
				ForcedMapName = "";
			}
		}
		catch (Exception ex4)
		{
			Logger.LogError((object)("Error forcing map load: " + ex4.Message));
			ForcedMapName = "";
		}
		return false;
	}

	[MasterClientOnly(null)]
	public static bool ForceLoadMapDirect(string mapName)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		string key = mapName.ToLower().Replace(" ", "_");
		if (!ValidMaps.TryGetValue(key, out string value))
		{
			Logger.LogWarning((object)("Unknown map name: " + mapName));
			ShowAvailableMaps();
			return false;
		}
		try
		{
			Logger.LogInfo((object)("Force loading map directly: " + value));
			try
			{
				Logger.LogInfo((object)("Method 1: Using PhotonNetwork.LoadLevel for " + value));
				if (PhotonNetwork.InRoom)
				{
					Hashtable val = new Hashtable();
					val[(object)"scene"] = value;
					PhotonNetwork.CurrentRoom.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
				}
				DisableForcedMap = true;
				PhotonNetwork.LoadLevel(value);
				Logger.LogInfo((object)("Successfully initiated Photon map load: " + value));
				Task.Delay(1000).ContinueWith(delegate
				{
					DisableForcedMap = false;
				});
				return true;
			}
			catch (Exception ex)
			{
				Logger.LogWarning((object)("Method 1 failed: " + ex.Message + ". Trying Method 2..."));
			}
			try
			{
				LoadingScreenHandler val2 = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
				if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
				{
					MethodInfo method = typeof(LoadingScreenHandler).GetMethod("LoadSceneProcess", BindingFlags.Instance | BindingFlags.NonPublic);
					if (method != null)
					{
						method.Invoke(val2, new object[1] { value });
						Logger.LogInfo((object)("Method 2: Successfully triggered LoadingScreenHandler map load: " + value));
						return true;
					}
					Logger.LogWarning((object)"Method 2 failed: LoadSceneProcess method not found");
				}
				else
				{
					Logger.LogWarning((object)"Method 2 failed: LoadingScreenHandler not found in scene");
				}
			}
			catch (Exception ex2)
			{
				Logger.LogWarning((object)("Method 2 failed: " + ex2.Message + ". Trying Method 3..."));
			}
			try
			{
				Logger.LogInfo((object)("Method 3: Using UnityEngine.SceneManagement.SceneManager.LoadScene for " + value));
				SceneManager.LoadScene(value);
				Logger.LogInfo((object)("Method 3: Successfully initiated Unity direct map load: " + value));
				return true;
			}
			catch (Exception ex3)
			{
				Logger.LogError((object)("Method 3 failed: " + ex3.Message));
			}
		}
		catch (Exception ex4)
		{
			Logger.LogError((object)("Error in direct map load: " + ex4.Message));
		}
		return false;
	}

	public static void ShowAvailableMaps()
	{
		Logger.LogInfo((object)"Available maps:");
		foreach (KeyValuePair<string, string> validMap in ValidMaps)
		{
			Logger.LogInfo((object)("  " + validMap.Key + " -> " + validMap.Value));
		}
	}
}
