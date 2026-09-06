using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedConsole;

public static class MapSyncPatch
{
	private static readonly string[] SyncIgnoreScenes = new string[4] { "Pretitle", "Title", "MainMenu", "LoadingScreen" };

	public static void OnEvent_Prefix(ref EventData photonEvent)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (photonEvent.Code != 253)
			{
				return;
			}
			object customData = photonEvent.CustomData;
			Hashtable val = (Hashtable)((customData is Hashtable) ? customData : null);
			if (val == null || !val.ContainsKey((byte)251))
			{
				return;
			}
			object obj = val[(byte)251];
			Hashtable val2 = (Hashtable)((obj is Hashtable) ? obj : null);
			if (val2 == null || !((Dictionary<object, object>)(object)val2).ContainsKey((object)"scene") || !(val2[(object)"scene"] is string text) || MasterClientUtils.IsMasterClient())
			{
				return;
			}
			Plugin.Log.LogInfo((object)("MapSyncPatch: Received room scene change: " + text));
			Scene activeScene = SceneManager.GetActiveScene();
			string currentScene = activeScene.name;
			if (Array.Exists(SyncIgnoreScenes, (string s) => s.Equals(currentScene)) || !(currentScene != text))
			{
				return;
			}
			Plugin.Log.LogInfo((object)("MapSyncPatch: Detected scene mismatch. Room scene: " + text + ", Current: " + currentScene));
			LoadingScreenHandler val3 = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
			if (!((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null))
			{
				return;
			}
			Plugin.Log.LogInfo((object)("MapSyncPatch: Showing loading screen for " + text));
			try
			{
				MethodInfo method = typeof(LoadingScreenHandler).GetMethod("ShowLoadingScreen", BindingFlags.Instance | BindingFlags.Public);
				if (method != null)
				{
					method.Invoke(val3, null);
				}
			}
			catch (Exception ex)
			{
				Plugin.Log.LogError((object)("Error showing loading screen: " + ex.Message));
			}
		}
		catch (Exception ex2)
		{
			Plugin.Log.LogError((object)("Error in PhotonNetwork.OnEvent patch: " + ex2.Message));
		}
	}

	public static void OnFinishedLoadingScene_Postfix()
	{
		try
		{
			if (MasterClientUtils.IsMasterClient() && PhotonNetwork.InRoom)
			{
				Plugin.Log.LogInfo((object)"MapSyncPatch: Scene loaded, checking sync with non-mod players...");
				Task.Delay(2000).ContinueWith(delegate
				{
					MapLoadingFix.SyncMapToNonModPlayers();
				});
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in LoadingScreenHandler.OnFinishedLoadingScene patch: " + ex.Message));
		}
	}

	[PunRPC]
	public static void SyncMapRPC(string mapName)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (string.IsNullOrEmpty(mapName) || MasterClientUtils.IsMasterClient())
			{
				return;
			}
			Scene activeScene = SceneManager.GetActiveScene();
			string currentScene = activeScene.name;
			if (Array.Exists(SyncIgnoreScenes, (string s) => s.Equals(currentScene)) || !(currentScene != mapName))
			{
				return;
			}
			Plugin.Log.LogInfo((object)("SyncMapRPC: Received map change request to " + mapName));
			LoadingScreenHandler val = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Plugin.Log.LogInfo((object)("SyncMapRPC: Showing loading screen for " + mapName));
				MethodInfo method = typeof(LoadingScreenHandler).GetMethod("ShowLoadingScreen", BindingFlags.Instance | BindingFlags.Public);
				if (method != null)
				{
					method.Invoke(val, null);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in SyncMapRPC: " + ex.Message));
		}
	}
}
