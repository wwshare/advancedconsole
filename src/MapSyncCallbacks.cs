using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedConsole;

public class MapSyncCallbacks : MonoBehaviourPunCallbacks
{
	private void Start()
	{
		// MonoBehaviourPunCallbacks.OnEnable 已自动注册回调, 此处不再重复注册
		((MonoBehaviour)this).StartCoroutine(SetupEventListeners());
	}

	private void OnDestroy()
	{
		PhotonNetwork.RemoveCallbackTarget((object)this);
	}

	private IEnumerator SetupEventListeners()
	{
		yield return (object)new WaitUntil((Func<bool>)(() => PhotonNetwork.IsConnectedAndReady));
		Plugin.Log.LogInfo((object)"Map sync callbacks ready!");
		if (!PhotonNetwork.InRoom || !((Dictionary<object, object>)(object)((RoomInfo)PhotonNetwork.CurrentRoom).CustomProperties).TryGetValue((object)"scene", out object value) || !(value is string text))
		{
			yield break;
		}
		Scene activeScene = SceneManager.GetActiveScene();
		string name = activeScene.name;
		Plugin.Log.LogInfo((object)("Current room scene: " + text + ", loaded scene: " + name));
		if (!(name != text) || !(name != "LoadingScreen") || !(name != "Pretitle") || !(name != "Title") || !(name != "MainMenu"))
		{
			yield break;
		}
		Plugin.Log.LogInfo((object)("Scene mismatch detected on join, syncing to " + text));
		if (MasterClientUtils.IsMasterClient())
		{
			MapLoadingFix.SyncMapToNonModPlayers();
			yield break;
		}
		LoadingScreenHandler val = UnityEngine.Object.FindFirstObjectByType<LoadingScreenHandler>();
		if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
		{
			((MonoBehaviour)this).Invoke("SyncToRoomScene", 0.5f);
		}
	}

	private void SyncToRoomScene()
	{
		try
		{
			if (PhotonNetwork.InRoom && ((Dictionary<object, object>)(object)((RoomInfo)PhotonNetwork.CurrentRoom).CustomProperties).TryGetValue((object)"scene", out object value) && value is string text)
			{
				Plugin.Log.LogInfo((object)("Syncing client to room scene: " + text));
				SceneManager.LoadScene(text);
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in SyncToRoomScene: " + ex.Message));
		}
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		((MonoBehaviourPunCallbacks)this).OnRoomPropertiesUpdate(propertiesThatChanged);
		try
		{
			if (!((Dictionary<object, object>)(object)propertiesThatChanged).ContainsKey((object)"scene") || !(propertiesThatChanged[(object)"scene"] is string text))
			{
				return;
			}
			Scene activeScene = SceneManager.GetActiveScene();
			string name = activeScene.name;
			Plugin.Log.LogInfo((object)("Room property 'scene' updated to: " + text + ", current scene: " + name));
			if (!MasterClientUtils.IsMasterClient() && name != text && name != "LoadingScreen" && !string.IsNullOrEmpty(text) && name != "Pretitle" && name != "Title" && name != "MainMenu")
			{
				Plugin.Log.LogInfo((object)("Detected scene mismatch, syncing to: " + text));
				AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
				if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
				{
					Plugin.Log.LogInfo((object)"Found AirportCheckInKiosk, using standard loading");
				}
				else
				{
					((MonoBehaviour)this).Invoke("SyncToRoomScene", 0.5f);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in OnRoomPropertiesUpdate: " + ex.Message));
		}
	}

	public override void OnJoinedRoom()
	{
		((MonoBehaviourPunCallbacks)this).OnJoinedRoom();
		((MonoBehaviour)this).StartCoroutine(CheckRoomMapOnJoin());
	}

	private IEnumerator CheckRoomMapOnJoin()
	{
		yield return (object)new WaitForSeconds(1f);
		try
		{
			if (!PhotonNetwork.InRoom || !((Dictionary<object, object>)(object)((RoomInfo)PhotonNetwork.CurrentRoom).CustomProperties).TryGetValue((object)"scene", out object value) || !(value is string text))
			{
				yield break;
			}
			Scene activeScene = SceneManager.GetActiveScene();
			string name = activeScene.name;
			Plugin.Log.LogInfo((object)("On join: Room scene is " + text + ", current scene is " + name));
			if (name != text && name != "LoadingScreen" && name != "Pretitle" && name != "Title" && name != "MainMenu")
			{
				if (MasterClientUtils.IsMasterClient())
				{
					Plugin.Log.LogInfo((object)"Master client checking map sync for players...");
					MapLoadingFix.SyncMapToNonModPlayers();
				}
				else
				{
					Plugin.Log.LogInfo((object)("Client syncing to room map: " + text));
					((MonoBehaviour)this).StartCoroutine(LoadSceneCoroutine(text));
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error checking room map on join: " + ex.Message));
		}
	}

	private IEnumerator LoadSceneCoroutine(string sceneName)
	{
		yield return (object)new WaitForSeconds(0.5f);
		try
		{
			SceneManager.LoadScene(sceneName);
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to load scene " + sceneName + ": " + ex.Message));
		}
	}
}
