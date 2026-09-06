using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public class PhotonCallbacks : MonoBehaviourPunCallbacks
{
	private void Start()
	{
		// MonoBehaviourPunCallbacks.OnEnable 已自动注册回调, 此处不再重复注册
	}

	private void OnDestroy()
	{
		PhotonNetwork.RemoveCallbackTarget((object)this);
	}

	public override void OnJoinedRoom()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		Hashtable val = new Hashtable();
		val[(object)"AdvancedConsoleUser"] = PhotonNetwork.GetPing();
		PhotonNetwork.LocalPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
		Plugin.Log.LogInfo((object)"AdvancedConsole user properties set!");
		PhotonPlayer[] playerListOthers = PhotonNetwork.PlayerListOthers;
		PhotonPlayer[] array = playerListOthers;
		foreach (PhotonPlayer val2 in array)
		{
			if (((Dictionary<object, object>)(object)val2.CustomProperties).ContainsKey((object)"AtlUser"))
			{
				Plugin.Log.LogInfo((object)("Atlas user detected: " + val2.NickName));
			}
			if (((Dictionary<object, object>)(object)val2.CustomProperties).ContainsKey((object)"CherryUser"))
			{
				Plugin.Log.LogInfo((object)("Cherry user detected: " + val2.NickName));
			}
			if (((Dictionary<object, object>)(object)val2.CustomProperties).ContainsKey((object)"AdvancedConsoleUser"))
			{
				Plugin.Log.LogInfo((object)("AdvancedConsole user detected: " + val2.NickName));
			}
			ModDetection.UpdatePlayerModInfo(val2);
		}
	}

	public override void OnPlayerEnteredRoom(PhotonPlayer newPlayer)
	{
		Plugin.Log.LogInfo((object)("Player entered room: " + newPlayer.NickName));
		if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"AtlUser"))
		{
			Plugin.Log.LogInfo((object)("Atlas user joined: " + newPlayer.NickName));
		}
		if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"CherryUser"))
		{
			Plugin.Log.LogInfo((object)("Cherry user joined: " + newPlayer.NickName));
		}
		if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"AdvancedConsoleUser"))
		{
			Plugin.Log.LogInfo((object)("AdvancedConsole user joined: " + newPlayer.NickName));
		}
		ModDetection.UpdatePlayerModInfo(newPlayer);
	}

	public override void OnPlayerLeftRoom(PhotonPlayer otherPlayer)
	{
		Plugin.Log.LogInfo((object)("Player left room: " + otherPlayer.NickName));
		if (((Dictionary<object, object>)(object)otherPlayer.CustomProperties).ContainsKey((object)"AtlUser"))
		{
			Plugin.Log.LogInfo((object)("Atlas user left: " + otherPlayer.NickName));
		}
		if (((Dictionary<object, object>)(object)otherPlayer.CustomProperties).ContainsKey((object)"CherryUser"))
		{
			Plugin.Log.LogInfo((object)("Cherry user left: " + otherPlayer.NickName));
		}
		if (((Dictionary<object, object>)(object)otherPlayer.CustomProperties).ContainsKey((object)"AdvancedConsoleUser"))
		{
			Plugin.Log.LogInfo((object)("AdvancedConsole user left: " + otherPlayer.NickName));
		}
	}

	public override void OnPlayerPropertiesUpdate(PhotonPlayer targetPlayer, Hashtable changedProps)
	{
		Plugin.Log.LogInfo((object)("Player properties updated for: " + targetPlayer.NickName));
		ModDetection.UpdatePlayerModInfo(targetPlayer);
	}

	public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		Plugin.Log.LogInfo((object)("Master client switched to: " + newMasterClient.NickName));
		Hashtable val = new Hashtable();
		val[(object)"AdvancedConsoleUser"] = PhotonNetwork.GetPing();
		val[(object)"ConsoleVersion"] = "1.1.4";
		PhotonNetwork.LocalPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
	}

	private float _lastBroadcastTime;

	private void Update()
	{
		if (PhotonNetwork.InRoom && Time.time - _lastBroadcastTime >= 10f)
		{
			_lastBroadcastTime = Time.time;
			Hashtable val = new Hashtable();
			val[(object)"AdvancedConsoleUser"] = PhotonNetwork.GetPing();
			PhotonNetwork.LocalPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
		}
	}

	public static void RegisterMapSyncCallbacks()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		try
		{
			GameObject val = new GameObject("MapSyncCallbacks");
			UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)(object)val);
			MapSyncCallbacks mapSyncCallbacks = val.AddComponent<MapSyncCallbacks>();
			Plugin.Log.LogInfo((object)"Map sync callbacks registered successfully");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to register map sync callbacks: " + ex.Message));
		}
	}

	public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		((MonoBehaviourPunCallbacks)this).OnRoomPropertiesUpdate(propertiesThatChanged);
		try
		{
			if (!((Dictionary<object, object>)(object)propertiesThatChanged).ContainsKey((object)"scene"))
			{
				return;
			}
			object obj = propertiesThatChanged[(object)"scene"];
			string sceneName = obj as string;
			if (sceneName == null)
			{
				return;
			}
			Scene activeScene = SceneManager.GetActiveScene();
			string name = activeScene.name;
			Plugin.Log.LogInfo((object)("Room property 'scene' updated to: " + sceneName + ", current scene: " + name));
			if (MasterClientUtils.IsMasterClient() || !(name != sceneName) || !(name != "LoadingScreen") || string.IsNullOrEmpty(sceneName))
			{
				return;
			}
			Plugin.Log.LogInfo((object)("Detected scene mismatch, preparing to sync to: " + sceneName));
			LoadingScreenHandler val = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Task.Delay(500).ContinueWith(delegate
				{
					SceneManager.LoadScene(sceneName);
				});
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in OnRoomPropertiesUpdate: " + ex.Message));
		}
	}
}
