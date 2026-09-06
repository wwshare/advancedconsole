using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class ModDetection
{
	public class PlayerModInfo
	{
		public bool HasAtlas { get; set; }

		public bool IsAtlasOwner { get; set; }

		public bool HasCherry { get; set; }

		public bool IsCherryOwner { get; set; }

		public bool HasAdvancedConsole { get; set; }

		public int Ping { get; set; }

		public bool IsCheater
		{
			get
			{
				if (!HasAtlas && !IsAtlasOwner && !HasCherry)
				{
					return IsCherryOwner;
				}
				return true;
			}
		}

		public string ModLoader
		{
			get
			{
				if (!HasAtlas && !IsAtlasOwner)
				{
					if (!HasCherry && !IsCherryOwner)
					{
						return "";
					}
					return "CHERRY";
				}
				return "ATLAS";
			}
		}

		public List<string> ModList
		{
			get
			{
				List<string> list = new List<string>();
				if (HasAtlas || IsAtlasOwner)
				{
					list.Add(IsAtlasOwner ? "ATLAS OWNER" : "ATLAS");
				}
				if (HasCherry || IsCherryOwner)
				{
					list.Add(IsCherryOwner ? "CHERRY OWNER" : "CHERRY");
				}
				if (HasAdvancedConsole)
				{
					list.Add("CONSOLE");
				}
				return list;
			}
		}

		public string ModHash => string.Join(",", ModList);
	}

	private static readonly Dictionary<int, PlayerModInfo> _playerModInfos = new Dictionary<int, PlayerModInfo>();

	public static void BroadcastModInfo()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		if (!PhotonNetwork.IsConnected)
		{
			return;
		}
		try
		{
			ExitGames.Client.Photon.Hashtable val = new ExitGames.Client.Photon.Hashtable();
			val[(object)"AdvancedConsoleUser"] = PhotonNetwork.GetPing();
			PhotonNetwork.LocalPlayer.SetCustomProperties(val, (ExitGames.Client.Photon.Hashtable)null, (WebFlags)null);
			Plugin.Log.LogInfo((object)"Broadcasted Advanced Console mod information");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to broadcast mod info: " + ex.Message));
		}
	}

	public static void UpdatePlayerModInfo(PhotonPlayer player)
	{
		if (((player != null) ? player.CustomProperties : null) != null)
		{
			PlayerModInfo playerModInfo = new PlayerModInfo();
			playerModInfo.HasAtlas = ((Dictionary<object, object>)(object)player.CustomProperties).ContainsKey((object)"AtlUser");
			playerModInfo.IsAtlasOwner = ((Dictionary<object, object>)(object)player.CustomProperties).ContainsKey((object)"AtlOwner");
			playerModInfo.HasCherry = ((Dictionary<object, object>)(object)player.CustomProperties).ContainsKey((object)"CherryUser");
			playerModInfo.IsCherryOwner = ((Dictionary<object, object>)(object)player.CustomProperties).ContainsKey((object)"CherryOwner");
			playerModInfo.HasAdvancedConsole = ((Dictionary<object, object>)(object)player.CustomProperties).ContainsKey((object)"AdvancedConsoleUser");
			if (((Dictionary<object, object>)(object)player.CustomProperties).TryGetValue((object)"AdvancedConsoleUser", out object value) && value is int ping)
			{
				playerModInfo.Ping = ping;
			}
			_playerModInfos[player.ActorNumber] = playerModInfo;
		}
	}

	public static PlayerModInfo? GetPlayerModInfo(PhotonPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		_playerModInfos.TryGetValue(player.ActorNumber, out PlayerModInfo value);
		return value;
	}

	public static void ClearPlayerModInfo(PhotonPlayer player)
	{
		if (player != null)
		{
			_playerModInfos.Remove(player.ActorNumber);
		}
	}

	public static string GetModStatusString(PhotonPlayer player)
	{
		PlayerModInfo playerModInfo = GetPlayerModInfo(player);
		if (playerModInfo == null)
		{
			return "";
		}
		List<string> list = new List<string>();
		if (playerModInfo.HasAtlas || playerModInfo.IsAtlasOwner)
		{
			list.Add(playerModInfo.IsAtlasOwner ? "ATLAS OWNER" : "ATLAS");
		}
		if (playerModInfo.HasCherry || playerModInfo.IsCherryOwner)
		{
			list.Add(playerModInfo.IsCherryOwner ? "CHERRY OWNER" : "CHERRY");
		}
		if (playerModInfo.HasAdvancedConsole)
		{
			list.Add("CONSOLE");
		}
		return string.Join(", ", list);
	}

	public static bool IsPlayerCheater(PhotonPlayer player)
	{
		PlayerModInfo playerModInfo = GetPlayerModInfo(player);
		if (playerModInfo == null)
		{
			return false;
		}
		if (!playerModInfo.HasAtlas && !playerModInfo.IsAtlasOwner && !playerModInfo.HasCherry)
		{
			return playerModInfo.IsCherryOwner;
		}
		return true;
	}

	public static Color GetModStatusColor(PhotonPlayer player)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		PlayerModInfo playerModInfo = GetPlayerModInfo(player);
		if (playerModInfo == null)
		{
			return Color.white;
		}
		if (playerModInfo.HasAtlas || playerModInfo.IsAtlasOwner || playerModInfo.HasCherry || playerModInfo.IsCherryOwner)
		{
			return new Color(1f, 0.3f, 0.3f);
		}
		if (playerModInfo.HasAdvancedConsole)
		{
			return new Color(0.3f, 1f, 0.3f);
		}
		return Color.white;
	}

	public static void KickPlayer(PhotonPlayer player, string reason = "Kicked by host")
	{
		if (!MasterClientUtils.IsMasterClient())
		{
			Plugin.Log.LogWarning((object)"Only Master Client can kick players!");
			return;
		}
		if (player == null || player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
		{
			Plugin.Log.LogWarning((object)"Cannot kick local player or null player!");
			return;
		}
		Plugin.Log.LogInfo((object)("Kicking player " + player.NickName + " - Reason: " + reason));
		try
		{
			PhotonNetwork.CloseConnection(player);
			Plugin.Log.LogInfo((object)("Kicked " + player.NickName + " via Photon disconnect"));
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to kick player " + player.NickName + ": " + ex.Message));
		}
	}

	private static bool TryKickViaAirportKiosk(PhotonPlayer player, string reason)
	{
		try
		{
			AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					component.RPC("BeginIslandLoadRPC", (RpcTarget)1, new object[2] { "Airport", 7 });
					Plugin.Log.LogInfo((object)("Kicked " + player.NickName + " via Airport Kiosk - " + reason));
					if ((UnityEngine.Object)(object)Plugin.Instance != (UnityEngine.Object)null)
					{
						((MonoBehaviour)Plugin.Instance).StartCoroutine(ReclaimMasterClientDelayed());
					}
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Airport Kiosk kick failed: " + ex.Message));
		}
		return false;
	}

	private static bool TryKickViaCharacterWarp(PhotonPlayer player, string reason)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val = null;
			Character[] array2 = array;
			foreach (Character val2 in array2)
			{
				PhotonView component = ((Component)val2).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && component.Owner != null && component.Owner.ActorNumber == player.ActorNumber)
				{
					val = val2;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component2 = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component2 != (UnityEngine.Object)null)
				{
					Vector3 val3 = default(Vector3);
					val3 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
					component2.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val3, true });
					Plugin.Log.LogInfo((object)("Black screened " + player.NickName + " - " + reason));
					component2.RequestOwnership();
					if ((UnityEngine.Object)(object)Plugin.Instance != (UnityEngine.Object)null)
					{
						((MonoBehaviour)Plugin.Instance).StartCoroutine(DestroyCharacterDelayed(component2));
					}
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Character warp kick failed: " + ex.Message));
		}
		return false;
	}

	private static IEnumerator DestroyCharacterDelayed(PhotonView characterPhotonView)
	{
		yield return (object)new WaitForSeconds(0.1f);
		try
		{
			if ((UnityEngine.Object)(object)characterPhotonView != (UnityEngine.Object)null && characterPhotonView.IsMine)
			{
				PhotonNetwork.Destroy(characterPhotonView);
				Plugin.Log.LogInfo((object)"Successfully destroyed kicked player's character");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error destroying character: " + ex.Message));
		}
	}

	private static IEnumerator ReclaimMasterClientDelayed()
	{
		yield return (object)new WaitForSeconds(1f);
		if (!PhotonNetwork.LocalPlayer.IsMasterClient)
		{
			PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
			Plugin.Log.LogInfo((object)"Reclaimed master client after kicking player");
		}
	}
}
