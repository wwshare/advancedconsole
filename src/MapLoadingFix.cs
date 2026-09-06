using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BepInEx.Logging;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedConsole;

public static class MapLoadingFix
{
	private static ManualLogSource Logger => Plugin.Log;

	public static bool LoadMap(string sceneName)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		if (!MasterClientUtils.IsMasterClient("LoadMap"))
		{
			return false;
		}
		Logger.LogInfo((object)("MapLoadingFix: Starting map load sequence for " + sceneName));
		try
		{
			Logger.LogInfo((object)("MapLoadingFix Method 1: Using optimized PhotonNetwork.LoadLevel for " + sceneName));
			if (PhotonNetwork.InRoom)
			{
				Hashtable val = new Hashtable();
				val[(object)"scene"] = sceneName;
				val[(object)"m_LevelPrefix"] = 0;
				val[(object)"curScn"] = sceneName;
				PhotonNetwork.CurrentRoom.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
				Logger.LogInfo((object)"Room properties updated for new map - all players will sync");
			}
			try
			{
				AirportCheckInKiosk val2 = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
				if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
				{
					PhotonView component = ((Component)val2).GetComponent<PhotonView>();
					if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
					{
						Logger.LogInfo((object)"Notifying all players via AirportCheckInKiosk RPC");
						component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[2] { sceneName, 0 });
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning((object)("RPC notification failed: " + ex.Message + ", continuing with direct load"));
			}
			MapControlPatches.DisableForcedMap = true;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			PhotonNetwork.LoadLevel(sceneName);
			Logger.LogInfo((object)("MapLoadingFix Method 1: Successfully initiated Photon map load: " + sceneName));
			Task.Delay(1000).ContinueWith(delegate
			{
				MapControlPatches.DisableForcedMap = false;
			});
			return true;
		}
		catch (Exception ex2)
		{
			Logger.LogWarning((object)("MapLoadingFix Method 1 failed: " + ex2.Message + ". Trying Method 2..."));
		}
		try
		{
			Logger.LogInfo((object)("MapLoadingFix Method 2: Using LoadingScreenHandler for " + sceneName));
			LoadingScreenHandler val3 = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
			if ((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null)
			{
				MethodInfo method = typeof(LoadingScreenHandler).GetMethod("LoadSceneProcess", BindingFlags.Instance | BindingFlags.NonPublic);
				if (method != null)
				{
					Resources.UnloadUnusedAssets();
					GC.Collect();
					method.Invoke(val3, new object[1] { sceneName });
					Logger.LogInfo((object)("MapLoadingFix Method 2: Successfully triggered LoadingScreenHandler map load: " + sceneName));
					return true;
				}
				Logger.LogWarning((object)"MapLoadingFix Method 2 failed: LoadSceneProcess method not found");
			}
			else
			{
				Logger.LogWarning((object)"MapLoadingFix Method 2 failed: LoadingScreenHandler not found in scene");
			}
		}
		catch (Exception ex3)
		{
			Logger.LogWarning((object)("MapLoadingFix Method 2 failed: " + ex3.Message + ". Trying Method 3..."));
		}
		try
		{
			Logger.LogInfo((object)("MapLoadingFix Method 3: Using SceneManager for " + sceneName));
			Resources.UnloadUnusedAssets();
			GC.Collect();
			SceneManager.LoadScene(sceneName, (LoadSceneMode)0);
			Logger.LogInfo((object)("MapLoadingFix Method 3: Successfully initiated Unity direct map load: " + sceneName));
			return true;
		}
		catch (Exception ex4)
		{
			Logger.LogError((object)("MapLoadingFix Method 3 failed: " + ex4.Message));
		}
		Logger.LogError((object)("MapLoadingFix: All methods failed to load map " + sceneName));
		return false;
	}

	public static bool LoadMapAsync(string sceneName)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		try
		{
			if (!MasterClientUtils.IsMasterClient("LoadMapAsync"))
			{
				return false;
			}
			Logger.LogInfo((object)("MapLoadingFix: Starting async map load for " + sceneName));
			if (PhotonNetwork.InRoom)
			{
				Hashtable val = new Hashtable();
				val[(object)"scene"] = sceneName;
				val[(object)"m_LevelPrefix"] = 0;
				val[(object)"curScn"] = sceneName;
				PhotonNetwork.CurrentRoom.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
				Logger.LogInfo((object)"Room properties set for async load - all players will sync");
			}
			try
			{
				AirportCheckInKiosk val2 = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
				if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
				{
					PhotonView component = ((Component)val2).GetComponent<PhotonView>();
					if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
					{
						Logger.LogInfo((object)"Notifying all players via AirportCheckInKiosk RPC (async)");
						component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[2] { sceneName, 0 });
					}
				}
				PhotonView[] array = UnityEngine.Object.FindObjectsByType<PhotonView>((FindObjectsInactive)1, (FindObjectsSortMode)0);
				PhotonView[] array2 = array;
				foreach (PhotonView val3 in array2)
				{
					if ((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null && val3.IsMine && (UnityEngine.Object)(object)val3 != (UnityEngine.Object)(object)((val2 != null) ? ((Component)val2).GetComponent<PhotonView>() : null))
					{
						try
						{
							val3.RPC("SyncMapRPC", (RpcTarget)0, new object[1] { sceneName });
						}
						catch (Exception)
						{
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.LogWarning((object)("RPC notification failed: " + ex2.Message + ", continuing with direct load"));
			}
			MapControlPatches.DisableForcedMap = true;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			try
			{
				PhotonNetwork.LoadLevel(sceneName);
				Logger.LogInfo((object)("Started Photon async loading for " + sceneName));
			}
			catch
			{
				SceneManager.LoadSceneAsync(sceneName, (LoadSceneMode)0);
				Logger.LogInfo((object)("Started Unity async loading for " + sceneName));
			}
			Task.Delay(1000).ContinueWith(delegate
			{
				MapControlPatches.DisableForcedMap = false;
			});
			return true;
		}
		catch (Exception ex3)
		{
			Logger.LogError((object)("MapLoadingFix: Async loading failed - " + ex3.Message));
			MapControlPatches.DisableForcedMap = false;
			return false;
		}
	}

	public static void SyncMapToNonModPlayers()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (!MasterClientUtils.IsMasterClient() || !PhotonNetwork.InRoom)
		{
			return;
		}
		try
		{
			if (!((Dictionary<object, object>)(object)((RoomInfo)PhotonNetwork.CurrentRoom).CustomProperties).TryGetValue((object)"scene", out object value) || !(value is string text))
			{
				return;
			}
			Scene activeScene = SceneManager.GetActiveScene();
			string name = activeScene.name;
			if (string.IsNullOrEmpty(text) || !(name != text))
			{
				return;
			}
			Logger.LogInfo((object)("Detected scene mismatch. Room scene: " + text + ", Current scene: " + name));
			Logger.LogInfo((object)"Attempting to sync non-mod players to correct map...");
			AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					Logger.LogInfo((object)("Sending BeginIslandLoadRPC to all players for scene: " + text));
					component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[2] { text, 0 });
				}
			}
			PhotonNetwork.LoadLevel(text);
		}
		catch (Exception ex)
		{
			Logger.LogError((object)("Error syncing map to non-mod players: " + ex.Message));
		}
	}
}
