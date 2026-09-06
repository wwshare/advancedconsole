using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class ServerAdmin
{
	public class PlayerCheatInfo
	{
		public PhotonPlayer? PhotonPlayer { get; set; }

		public int ActorNumber { get; set; }

		public string NickName { get; set; } = "";

		public bool HasAtlas { get; set; }

		public bool IsAtlasOwner { get; set; }

		public bool HasCherry { get; set; }

		public bool IsCherryOwner { get; set; }

		public bool HasAdvancedConsole { get; set; }

		public bool IsCheater { get; set; }

		public List<string> CheatFunctions { get; set; } = new List<string>();
	}

	[ConsoleCommand]
	[MasterClientOnly("SoftLockPlayer")]
	public static void SoftLockPlayer(string playerName)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"softlock: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer val = FindPlayerByName(playerName);
		if (val == null)
		{
			Debug.LogWarning((object)("softlock: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		if (val.IsLocal)
		{
			Debug.LogWarning((object)"softlock: 不能软锁定自己!");
			return;
		}
		try
		{
			string kickReason = GetKickReason(val);
			SoftLockPlayerInternal(val, kickReason);
			Debug.Log((object)("softlock: 已软锁定玩家 '" + val.NickName + "' - 原因: " + kickReason));
			Plugin.Log.LogInfo((object)("已通过控制台软锁定玩家: " + val.NickName + " - " + kickReason));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("softlock: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("通过控制台软锁定玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly("KickPlayerReason")]
	public static void KickPlayerReason(string playerName, string reason = "Kicked by admin")
	{
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"kickreason: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer val = FindPlayerByName(playerName);
		if (val == null)
		{
			Debug.LogWarning((object)("kickreason: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		if (val.IsLocal)
		{
			Debug.LogWarning((object)"kickreason: 不能踢出自己!");
			return;
		}
		try
		{
			SoftLockPlayerInternal(val, reason);
			Debug.Log((object)("kickreason: 已软锁定玩家 '" + val.NickName + "' - 原因: " + reason));
			Plugin.Log.LogInfo((object)("已软锁定玩家, 原因: " + val.NickName + " - " + reason));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("kickreason: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("带原因踢出玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ForceKickPlayer(string playerName)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"forcekick: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer val = FindPlayerByName(playerName);
		if (val == null)
		{
			Debug.LogWarning((object)("forcekick: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		if (val.IsLocal)
		{
			Debug.LogWarning((object)"forcekick: 不能踢出自己!");
			return;
		}
		try
		{
			PhotonNetwork.CloseConnection(val);
			Debug.Log((object)("forcekick: 已强制断开玩家 '" + val.NickName + "'"));
			Plugin.Log.LogInfo((object)("已强制踢出玩家: " + val.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("forcekick: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("强制踢出玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SoftKickPlayer(string playerName)
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"softkick: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer val = FindPlayerByName(playerName);
		if (val == null)
		{
			Debug.LogWarning((object)("softkick: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		if (val.IsLocal)
		{
			Debug.LogWarning((object)"softkick: 不能踢出自己!");
			return;
		}
		try
		{
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val2 = null;
			Character[] array2 = array;
			foreach (Character val3 in array2)
			{
				PhotonView component = ((Component)val3).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && component.Owner != null && component.Owner.ActorNumber == val.ActorNumber)
				{
					val2 = val3;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
			{
				Vector3 position = default(Vector3);
				position = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
				((Component)val2).transform.position = position;
				Debug.Log((object)("softkick: 已将玩家 '" + val.NickName + "' 传送到无限远处 (黑屏)"));
				Plugin.Log.LogInfo((object)("已通过传送软踢玩家: " + val.NickName));
			}
			else
			{
				PhotonNetwork.CloseConnection(val);
				Debug.Log((object)("softkick: 对玩家 '" + val.NickName + "' 使用备用踢出 (未找到角色)"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("softkick: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("软踢玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void BlackScreenKick(string playerName)
	{
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"blackscreen: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer val = FindPlayerByName(playerName);
		if (val == null)
		{
			Debug.LogWarning((object)("blackscreen: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		if (val.IsLocal)
		{
			Debug.LogWarning((object)"blackscreen: 不能对自己使用黑屏!");
			return;
		}
		try
		{
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val2 = null;
			Character[] array2 = array;
			foreach (Character val3 in array2)
			{
				PhotonView component = ((Component)val3).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && component.Owner != null && component.Owner.ActorNumber == val.ActorNumber)
				{
					val2 = val3;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
			{
				PhotonView component2 = ((Component)val2).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component2 != (UnityEngine.Object)null)
				{
					Vector3 val4 = default(Vector3);
					val4 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
					try
					{
						component2.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val4, true });
					}
					catch
					{
						((Component)val2).transform.position = val4;
					}
					Debug.Log((object)("blackscreen: 已为玩家 '" + val.NickName + "'"));
					Plugin.Log.LogInfo((object)("已使玩家黑屏: " + val.NickName));
				}
				else
				{
					Debug.LogWarning((object)("blackscreen: 未在角色上找到 PhotonView: " + val.NickName));
					PhotonNetwork.CloseConnection(val);
				}
			}
			else
			{
				Debug.LogWarning((object)("blackscreen: 未找到角色: " + val.NickName + ", 改用强制踢出"));
				PhotonNetwork.CloseConnection(val);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("blackscreen: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("使玩家黑屏失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void KickAllCheaters()
	{
		int num = 0;
		List<PhotonPlayer> list = PhotonNetwork.PlayerList.Where((PhotonPlayer p) => !p.IsLocal && ModDetection.IsPlayerCheater(p)).ToList();
		if (!list.Any())
		{
			Debug.Log((object)"kickcheaters: 大厅中未找到作弊者");
			return;
		}
		foreach (PhotonPlayer item in list)
		{
			try
			{
				string kickReason = GetKickReason(item);
				SoftLockPlayerInternal(item, kickReason);
				num++;
				Debug.Log((object)("kickcheaters: 已软锁定作弊者 '" + item.NickName + "' - " + kickReason));
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("kickcheaters: 踢出 " + item.NickName + " - " + ex.Message));
			}
		}
		Debug.Log((object)$"kickcheaters: 已从大厅软锁定 {num} 名作弊者");
		Plugin.Log.LogInfo((object)$"已从大厅软锁定 {num} 名作弊者");
	}

	[ConsoleCommand]
	public static void ListPlayers()
	{
		if (!PhotonNetwork.IsConnected)
		{
			Debug.Log((object)"listplayers: 未连接到任何房间");
			return;
		}
		if (PhotonNetwork.PlayerList == null || PhotonNetwork.PlayerList.Length == 0)
		{
			Debug.Log((object)"listplayers: 未找到玩家");
			return;
		}
		Debug.Log((object)"=== 玩家列表 ===");
		Room currentRoom = PhotonNetwork.CurrentRoom;
		Debug.Log((object)("房间: " + (((currentRoom != null) ? currentRoom.Name : null) ?? "未知")));
		object arg = PhotonNetwork.PlayerList.Length;
		Room currentRoom2 = PhotonNetwork.CurrentRoom;
		Debug.Log((object)$"玩家: {arg}/{((currentRoom2 != null) ? currentRoom2.MaxPlayers : 0)}");
		PhotonPlayer masterClient = PhotonNetwork.MasterClient;
		Debug.Log((object)("房主: " + (((masterClient != null) ? masterClient.NickName : null) ?? "未知")));
		Debug.Log((object)"-------------------");
		IOrderedEnumerable<PhotonPlayer> orderedEnumerable = from p in PhotonNetwork.PlayerList
			orderby (!p.IsLocal) ? 1 : 0, (!p.IsMasterClient) ? 1 : 0, p.ActorNumber
			select p;
		foreach (PhotonPlayer item in orderedEnumerable)
		{
			try
			{
				PlayerCheatInfo playerCheatInfo = GetPlayerCheatInfo(item);
				string text = "";
				if (item.IsLocal)
				{
					text += "[YOU] ";
				}
				if (item.IsMasterClient)
				{
					text += "[HOST] ";
				}
				string text2 = "";
				if (playerCheatInfo.HasAtlas || playerCheatInfo.IsAtlasOwner)
				{
					text2 += (playerCheatInfo.IsAtlasOwner ? "[ATLAS OWNER] " : "[ATLAS] ");
				}
				if (playerCheatInfo.HasCherry || playerCheatInfo.IsCherryOwner)
				{
					text2 += (playerCheatInfo.IsCherryOwner ? "[CHERRY OWNER] " : "[CHERRY] ");
				}
				if (playerCheatInfo.HasAdvancedConsole)
				{
					text2 += "[CONSOLE] ";
				}
				if (playerCheatInfo.IsCheater)
				{
					text2 += "[CHEATER] ";
				}
				Debug.Log((object)string.Format("  {0}: {1}{2} {3}", new object[4] { item.ActorNumber, text, item.NickName, text2 }));
			}
			catch (Exception ex)
			{
				Debug.Log((object)$"  {item.ActorNumber}: {item.NickName} [错误: {ex.Message}]");
			}
		}
		Debug.Log((object)"===================");
	}

	[ConsoleCommand]
	public static void PlayerInfo(string playerName)
	{
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"playerinfo: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer player = FindPlayerByName(playerName);
		if (player == null)
		{
			Debug.LogWarning((object)("playerinfo: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		try
		{
			PlayerCheatInfo playerCheatInfo = GetPlayerCheatInfo(player);
			Debug.Log((object)("=== 玩家信息: " + player.NickName + " ==="));
			Debug.Log((object)$"Actor ID: {player.ActorNumber}");
			Debug.Log((object)("用户 ID: " + (player.UserId ?? "未知")));
			Debug.Log((object)$"是否为本机: {player.IsLocal}");
			Debug.Log((object)$"是否为房主: {player.IsMasterClient}");
			Debug.Log((object)$"是否在线: {!player.IsInactive}");
			Debug.Log((object)"--- 模组信息 ---");
			Debug.Log((object)$"拥有 ATLAS: {playerCheatInfo.HasAtlas}");
			Debug.Log((object)$"是否为 ATLAS 拥有者: {playerCheatInfo.IsAtlasOwner}");
			Debug.Log((object)$"拥有 Cherry: {playerCheatInfo.HasCherry}");
			Debug.Log((object)$"是否为 Cherry 拥有者: {playerCheatInfo.IsCherryOwner}");
			Debug.Log((object)$"拥有 Advanced Console: {playerCheatInfo.HasAdvancedConsole}");
			Debug.Log((object)$"是否为作弊者: {playerCheatInfo.IsCheater}");
			if (playerCheatInfo.CheatFunctions.Count > 0)
			{
				Debug.Log((object)"--- 作弊功能 ---");
				foreach (string cheatFunction in playerCheatInfo.CheatFunctions)
				{
					Debug.Log((object)("  - " + cheatFunction));
				}
			}
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Debug.Log((object)"--- 角色信息 ---");
				Debug.Log((object)("状态: " + (val.data.dead ? "DEAD" : "ALIVE")));
				Debug.Log((object)$"位置: ({val.Center.x:F1}, {val.Center.y:F1}, {val.Center.z:F1})");
			}
			Debug.Log((object)"========================");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("playerinfo: 获取玩家信息出错 - " + ex.Message));
		}
	}

	private static void SoftLockPlayerInternal(PhotonPlayer player, string reason)
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					try
					{
						component.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[1] { 0 });
						return;
					}
					catch (Exception ex)
					{
						Debug.LogWarning((object)("使用自助终端软锁定失败: " + ex.Message));
					}
				}
			}
			Character[] array = UnityEngine.Object.FindObjectsByType<Character>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			Character val2 = null;
			Character[] array2 = array;
			foreach (Character val3 in array2)
			{
				PhotonView component2 = ((Component)val3).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component2 != (UnityEngine.Object)null && component2.Owner != null && component2.Owner.ActorNumber == player.ActorNumber)
				{
					val2 = val3;
					break;
				}
			}
			if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
			{
				PhotonView component3 = ((Component)val2).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component3 != (UnityEngine.Object)null)
				{
					try
					{
						Vector3 val4 = default(Vector3);
						val4 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
						component3.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val4, true });
						return;
					}
					catch (Exception ex2)
					{
						Debug.LogWarning((object)("使用角色传送失败: " + ex2.Message));
					}
				}
			}
			PhotonNetwork.CloseConnection(player);
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("SoftLockPlayerInternal: 严重错误 - " + ex3.Message));
			PhotonNetwork.CloseConnection(player);
		}
	}

	private static PlayerCheatInfo GetPlayerCheatInfo(PhotonPlayer player)
	{
		PlayerCheatInfo playerCheatInfo = new PlayerCheatInfo
		{
			PhotonPlayer = player,
			ActorNumber = player.ActorNumber,
			NickName = player.NickName
		};
		try
		{
			Hashtable customProperties = player.CustomProperties;
			playerCheatInfo.HasAtlas = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlUser");
			playerCheatInfo.IsAtlasOwner = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AtlOwner");
			playerCheatInfo.HasCherry = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryUser");
			playerCheatInfo.IsCherryOwner = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"CherryOwner");
			playerCheatInfo.HasAdvancedConsole = ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"AdvancedConsole");
			playerCheatInfo.CheatFunctions = DetectCheatFunctions(player);
			playerCheatInfo.IsCheater = playerCheatInfo.HasAtlas || playerCheatInfo.IsAtlasOwner || playerCheatInfo.HasCherry || playerCheatInfo.IsCherryOwner || playerCheatInfo.CheatFunctions.Count > 0;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("获取玩家 " + player.NickName + ": " + ex.Message));
		}
		return playerCheatInfo;
	}

	private static List<string> DetectCheatFunctions(PhotonPlayer player)
	{
		List<string> list = new List<string>();
		try
		{
			Hashtable customProperties = player.CustomProperties;
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"SpeedHack"))
			{
				list.Add("Speed Hack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"FlyHack"))
			{
				list.Add("Fly Hack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"NoClip"))
			{
				list.Add("No Clip");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"Teleport"))
			{
				list.Add("Teleport");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"GodMode"))
			{
				list.Add("God Mode");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"InfiniteStamina"))
			{
				list.Add("Infinite Stamina");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ESPHack"))
			{
				list.Add("ESP/Wallhack");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ItemSpawn"))
			{
				list.Add("Item Spawning");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"WeatherControl"))
			{
				list.Add("Weather Control");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"TimeControl"))
			{
				list.Add("Time Control");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"UnlimitedItems"))
			{
				list.Add("Unlimited Items");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"InstantWin"))
			{
				list.Add("Instant Win");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"KillAllPlayers"))
			{
				list.Add("Kill All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ReviveAllPlayers"))
			{
				list.Add("Revive All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ForceStart"))
			{
				list.Add("Force Start Game");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"TeleportAllPlayers"))
			{
				list.Add("Teleport All Players");
			}
			if (((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"ModMenu"))
			{
				list.Add("Mod Menu");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("检测玩家 " + player.NickName + ": " + ex.Message));
		}
		return list;
	}

	private static PhotonPlayer? FindPlayerByName(string playerName)
	{
		if (PhotonNetwork.PlayerList == null)
		{
			return null;
		}
		PhotonPlayer val = PhotonNetwork.PlayerList.FirstOrDefault((PhotonPlayer p) => string.Equals(p.NickName, playerName, StringComparison.OrdinalIgnoreCase));
		if (val != null)
		{
			return val;
		}
		return PhotonNetwork.PlayerList.FirstOrDefault((PhotonPlayer p) => p.NickName.ToLower().Contains(playerName.ToLower()));
	}

	private static string GetKickReason(PhotonPlayer player)
	{
		ModDetection.PlayerModInfo playerModInfo = ModDetection.GetPlayerModInfo(player);
		if (playerModInfo != null)
		{
			if (playerModInfo.HasAtlas || playerModInfo.IsAtlasOwner)
			{
				if (!playerModInfo.IsAtlasOwner)
				{
					return "ATLAS cheat mod user";
				}
				return "ATLAS cheat mod owner";
			}
			if (playerModInfo.HasCherry || playerModInfo.IsCherryOwner)
			{
				if (!playerModInfo.IsCherryOwner)
				{
					return "Cherry cheat mod user";
				}
				return "Cherry cheat mod owner";
			}
		}
		return "Kicked by host";
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SlowPlayer(string playerName)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"slowplayer: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer player = FindPlayerByName(playerName);
		if (player == null)
		{
			Debug.LogWarning((object)("slowplayer: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				foreach (Bodypart part in val.refs.ragdoll.partList)
				{
					Rigidbody rig = part.rig;
					rig.maxLinearVelocity *= 0.1f;
				}
				Debug.Log((object)("slowplayer: 已减速玩家 '" + player.NickName + "'"));
				Plugin.Log.LogInfo((object)("已减速玩家: " + player.NickName));
			}
			else
			{
				Debug.LogWarning((object)("slowplayer: 未找到角色: " + player.NickName));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("slowplayer: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("减速玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpeedPlayer(string playerName)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			Debug.LogWarning((object)"speedplayer: 请指定玩家名称");
			ListPlayers();
			return;
		}
		PhotonPlayer player = FindPlayerByName(playerName);
		if (player == null)
		{
			Debug.LogWarning((object)("speedplayer: 未找到玩家 '" + playerName + "'"));
			ListPlayers();
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault(delegate(Character c)
			{
				PhotonPlayer owner = ((MonoBehaviourPun)c).photonView.Owner;
				return ((owner != null) ? new int?(owner.ActorNumber) : ((int?)null)) == player.ActorNumber;
			});
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				foreach (Bodypart part in val.refs.ragdoll.partList)
				{
					Rigidbody rig = part.rig;
					rig.maxLinearVelocity *= 10f;
				}
				Debug.Log((object)("speedplayer: 已加速玩家 '" + player.NickName + "'"));
				Plugin.Log.LogInfo((object)("已加速玩家: " + player.NickName));
			}
			else
			{
				Debug.LogWarning((object)("speedplayer: 未找到角色: " + player.NickName));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("speedplayer: 错误 - " + ex.Message));
			Plugin.Log.LogError((object)("加速玩家失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ListPhotonObjects()
	{
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PhotonView[] array = UnityEngine.Object.FindObjectsByType<PhotonView>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			if (array == null || array.Length == 0)
			{
				Debug.Log((object)"listphotonobjects: 未找到 PhotonView 对象");
				return;
			}
			Debug.Log((object)$"=== 带生成路径的 PHOTON 对象 ({array.Length}) ===");
			var list = (from pv in array
				select new
				{
					PhotonView = pv,
					PrefabName = GetPrefabName(pv),
					SpawnPath = GetSpawnPath(pv)
				} into obj
				where !string.IsNullOrEmpty(obj.PrefabName)
				group obj by obj.PrefabName into g
				orderby g.Key
				select g).ToList();
			foreach (var item in list)
			{
				var anon = item.First();
				Debug.Log((object)$"\n--- {item.Key} ({item.Count()}) ---");
				Debug.Log((object)("  生成命令: PhotonNetwork.Instantiate(\"" + anon.SpawnPath + "\", pos, rot)"));
				foreach (var item2 in item.Take(3))
				{
					try
					{
						PhotonView photonView = item2.PhotonView;
						PhotonPlayer owner = photonView.Owner;
						string text = ((owner != null) ? owner.NickName : null) ?? "No Owner";
						Vector3 position = ((Component)photonView).transform.position;
						int viewID = photonView.ViewID;
						Debug.Log((object)string.Format("    ID:{0} | 拥有者:{1} | 位置:({2:F1}, {3:F1}, {4:F1})", new object[5] { viewID, text, position.x, position.y, position.z }));
					}
					catch (Exception ex)
					{
						Debug.Log((object)("    读取对象出错: " + ex.Message));
					}
				}
				if (item.Count() > 3)
				{
					Debug.Log((object)$"    ... 还有 {item.Count() - 3} 个");
				}
			}
			Debug.Log((object)"\n=== 新对象生成 ===");
			foreach (var item3 in list.Take(20))
			{
				var anon2 = item3.First();
				Debug.Log((object)("  ServerAdmin.DebugSpawn " + anon2.SpawnPath));
			}
			Debug.Log((object)"================================");
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("listphotonobjects: 错误 - " + ex2.Message));
		}
	}

	private static string GetPrefabName(PhotonView photonView)
	{
		try
		{
			string text = ((UnityEngine.Object)((Component)photonView).gameObject).name;
			if (text.Contains("(Clone)"))
			{
				text = text.Split('(')[0];
			}
			return text.Trim();
		}
		catch
		{
			return "";
		}
	}

	private static string GetSpawnPath(PhotonView photonView)
	{
		try
		{
			string prefabName = GetPrefabName(photonView);
			GameObject gameObject = ((Component)photonView).gameObject;
			if ((UnityEngine.Object)(object)gameObject.GetComponent<Item>() != (UnityEngine.Object)null)
			{
				return "0_Items/" + prefabName;
			}
			if ((UnityEngine.Object)(object)gameObject.GetComponent<Character>() != (UnityEngine.Object)null)
			{
				return prefabName;
			}
			switch (prefabName)
			{
			case "BugfixOnYou":
			case "Tornado":
			case "PlayerGhost":
			case "Player":
				return prefabName;
			default:
				return "0_Items/" + prefabName;
			}
		}
		catch
		{
			return GetPrefabName(photonView);
		}
	}

	[ConsoleCommand]
	public static void ListSpawnablePrefabs()
	{
		try
		{
			Debug.Log((object)"=== 可生成预制体 ===");
			LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
			if (networkingClient != null)
			{
				Debug.Log((object)"PhotonNetwork 资源:");
				Type typeFromHandle = typeof(PhotonNetwork);
				FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
				FieldInfo[] array = fields;
				foreach (FieldInfo fieldInfo in array)
				{
					if (fieldInfo.Name.Contains("prefab") || fieldInfo.Name.Contains("resource"))
					{
						try
						{
							object value = fieldInfo.GetValue(null);
							Debug.Log((object)$"  {fieldInfo.Name}: {value}");
						}
						catch
						{
						}
					}
				}
			}
			Debug.Log((object)"\n正在搜索 Resources 文件夹:");
			try
			{
				List<GameObject> list = (from go in Resources.FindObjectsOfTypeAll<GameObject>()
					where (UnityEngine.Object)(object)go.GetComponent<PhotonView>() != (UnityEngine.Object)null
					select go).Take(50).ToList();
				foreach (GameObject item in list)
				{
					try
					{
						PhotonView component = item.GetComponent<PhotonView>();
						Debug.Log((object)("  " + ((UnityEngine.Object)item).name + " | ViewID: " + (((UnityEngine.Object)(object)component != (UnityEngine.Object)null) ? component.ViewID.ToString() : "None")));
					}
					catch (Exception ex)
					{
						Debug.Log((object)("  " + ((UnityEngine.Object)item).name + " | 错误: " + ex.Message));
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.Log((object)("搜索资源出错: " + ex2.Message));
			}
			Debug.Log((object)"=========================");
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("listspawnables: 错误 - " + ex3.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void DebugSpawn(string prefabName)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(prefabName))
		{
			Debug.LogWarning((object)"debugspawn: 请指定预制体名称");
			Debug.Log((object)"可尝试的常用预制体:");
			Debug.Log((object)"  - 0_Items/Coconut");
			Debug.Log((object)"  - 0_Items/Stone");
			Debug.Log((object)"  - 0_Items/Torch");
			Debug.Log((object)"  - Characters/Character");
			return;
		}
		try
		{
			Vector3 val = Vector3.zero;
			if ((UnityEngine.Object)(object)Character.localCharacter != (UnityEngine.Object)null)
			{
				val = ((Component)Character.localCharacter).transform.position + Vector3.up * 2f;
			}
			else if ((UnityEngine.Object)(object)MainCamera.instance != (UnityEngine.Object)null)
			{
				val = ((Component)MainCamera.instance).transform.position + ((Component)MainCamera.instance).transform.forward * 3f;
			}
			Debug.Log((object)$"debugspawn: 正在尝试在 {val} 生成 '{prefabName}'");
			GameObject val2 = PhotonNetwork.Instantiate(prefabName, val, Quaternion.identity, (byte)0, (object[])null);
			if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
			{
				Debug.Log((object)("debugspawn: 已成功生成 '" + prefabName + "'"));
				Debug.Log((object)("  游戏对象: " + ((UnityEngine.Object)val2).name));
				Debug.Log((object)$"  位置: {val2.transform.position}");
				IEnumerable<string> values = from c in val2.GetComponents<Component>()
					where (UnityEngine.Object)(object)c != (UnityEngine.Object)null
					select ((object)c).GetType().Name;
				Debug.Log((object)("  组件: " + string.Join(", ", values)));
				Plugin.Log.LogInfo((object)("已调试生成对象: " + prefabName));
			}
			else
			{
				Debug.LogWarning((object)("debugspawn: 生成 '" + prefabName + "' 失败 - 对象为空"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("debugspawn: 生成 '" + prefabName + "' 失败 - " + ex.Message));
			Debug.Log((object)"尝试这些常用路径:");
			Debug.Log((object)"  - 0_Items/[ItemName]");
			Debug.Log((object)"  - Characters/[CharacterName]");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnObject(string prefabPath, PhotonPlayer? target = null)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(prefabPath))
		{
			Debug.LogWarning((object)"spawnobject: 请指定预制体路径");
			Debug.Log((object)"用法: spawnobject \"0_Items/Coconut\" [player]");
			Debug.Log((object)"使用 'listphotonobjects' 查看可用路径");
			return;
		}
		try
		{
			Vector3 spawnPositionForObject = GetSpawnPositionForObject(target);
			Debug.Log((object)$"spawnobject: 正在尝试在 {spawnPositionForObject} 生成 '{prefabPath}'");
			GameObject val = PhotonNetwork.Instantiate(prefabPath, spawnPositionForObject, Quaternion.identity, (byte)0, (object[])null);
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Debug.Log((object)("spawnobject: 已成功生成 '" + prefabPath + "'"));
				Plugin.Log.LogInfo((object)("已生成对象: " + prefabPath));
			}
			else
			{
				Debug.LogWarning((object)("spawnobject: 生成 '" + prefabPath + "' 失败 - 对象为空"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnobject: 生成 '" + prefabPath + "' 失败 - " + ex.Message));
			Debug.Log((object)"请确保预制体路径正确且存在于 Resources 文件夹中");
			Debug.Log((object)"使用 'listphotonobjects' 查看可用路径");
		}
	}

	private static Vector3 GetSpawnPositionForObject(PhotonPlayer? target = null)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Character val = null;
		val = (Character)((target == null) ? ((object)Character.localCharacter) : ((object)Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false)));
		if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
		{
			Vector3 center = val.Center;
			Vector3 lookDirection_Flat = val.data.lookDirection_Flat;
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(center, lookDirection_Flat, out val2, 20f))
			{
				return val2.point + Vector3.up * 0.5f;
			}
			return center + lookDirection_Flat * 5f;
		}
		if ((UnityEngine.Object)(object)MainCamera.instance != (UnityEngine.Object)null)
		{
			return ((Component)MainCamera.instance).transform.position + ((Component)MainCamera.instance).transform.forward * 3f;
		}
		return Vector3.zero;
	}

	[ConsoleCommand]
	public static void ListAllItems()
	{
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"listallitems: 未找到物品数据库!");
				return;
			}
			Debug.Log((object)$"=== 物品数据库 ({instance.itemLookup.Count}) ===");
			foreach (KeyValuePair<ushort, Item> item in instance.itemLookup.OrderBy((KeyValuePair<ushort, Item> x) => ((UnityEngine.Object)x.Value).name))
			{
				try
				{
					Item value = item.Value;
					ushort key = item.Key;
					Debug.Log((object)$"ID:{key} | {((UnityEngine.Object)value).name}");
					Debug.Log((object)("  路径: 0_Items/" + ((UnityEngine.Object)value).name));
					if (value.UIData != null && !string.IsNullOrEmpty(value.UIData.itemName) && value.UIData.itemName != ((UnityEngine.Object)value).name)
					{
						Debug.Log((object)("  显示名: " + value.UIData.itemName));
					}
					if ((int)value.itemTags != 0)
					{
						Debug.Log((object)$"  标签: {value.itemTags}");
					}
				}
				catch (Exception ex)
				{
					Debug.Log((object)$"  读取物品 {item.Key} 出错: {ex.Message}");
				}
			}
			Debug.Log((object)"==============================");
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("listallitems: 错误 - " + ex2.Message));
		}
	}
}
