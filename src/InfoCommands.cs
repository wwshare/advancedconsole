using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class InfoCommands
{
	[ConsoleCommand]
	public static void ListPlayers()
	{
		Debug.Log((object)"=== 玩家列表 ===");
		PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
		foreach (PhotonPlayer player in playerList)
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player) ?? false);
			string text = ((val != null && val.data.dead) ? "DEAD" : "ALIVE");
			string text2 = "N/A";
			Debug.Log((object)string.Format("玩家: {0} | 状态: {1} | 生命: {2} | ID: {3}", new object[4] { player.NickName, text, text2, player.ActorNumber }));
		}
		Debug.Log((object)$"玩家总数: {PhotonNetwork.PlayerList.Length}");
		Plugin.Log.LogInfo((object)$"已列出 {PhotonNetwork.PlayerList.Length} 名玩家");
	}

	[ConsoleCommand]
	public static void PlayerInfo(PhotonPlayer target)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"playerinfo: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		Debug.Log((object)("=== 玩家信息: " + target.NickName + " ==="));
		Debug.Log((object)$"Actor 编号: {target.ActorNumber}");
		Debug.Log((object)$"是否为房主: {target.IsMasterClient}");
		Debug.Log((object)$"是否为本机: {target.IsLocal}");
		if ((Object)(object)val != (Object)null)
		{
			Debug.Log((object)"生命: N/A");
			Debug.Log((object)$"死亡: {val.data.dead}");
			Debug.Log((object)$"位置: {val.Center}");
			Debug.Log((object)("角色名称: " + val.characterName));
			CharacterAfflictions afflictions = val.refs.afflictions;
			Debug.Log((object)$"寒冷: {afflictions.GetCurrentStatus((CharacterAfflictions.STATUSTYPE)2):F2}");
			Debug.Log((object)$"饥饿: {afflictions.GetCurrentStatus((CharacterAfflictions.STATUSTYPE)1):F2}");
			Debug.Log((object)$"受伤: {afflictions.GetCurrentStatus((CharacterAfflictions.STATUSTYPE)0):F2}");
			Debug.Log((object)$"中毒: {afflictions.GetCurrentStatus((CharacterAfflictions.STATUSTYPE)3):F2}");
		}
		else
		{
			Debug.Log((object)"没有可用的角色数据");
		}
		Plugin.Log.LogInfo((object)("已显示玩家信息: " + target.NickName));
	}

	[ConsoleCommand]
	public static void ServerInfo()
	{
		Debug.Log((object)"=== 服务器信息 ===");
		Room currentRoom = PhotonNetwork.CurrentRoom;
		Debug.Log((object)("房间名称: " + (((currentRoom != null) ? currentRoom.Name : null) ?? "N/A")));
		Room currentRoom2 = PhotonNetwork.CurrentRoom;
		Debug.Log((object)$"玩家数量: {((currentRoom2 != null) ? currentRoom2.PlayerCount : 0)}");
		Room currentRoom3 = PhotonNetwork.CurrentRoom;
		Debug.Log((object)$"最大玩家数: {((currentRoom3 != null) ? currentRoom3.MaxPlayers : 0)}");
		PhotonPlayer masterClient = PhotonNetwork.MasterClient;
		Debug.Log((object)("房主: " + (((masterClient != null) ? masterClient.NickName : null) ?? "N/A")));
		PhotonPlayer localPlayer = PhotonNetwork.LocalPlayer;
		Debug.Log((object)("本地玩家: " + (((localPlayer != null) ? localPlayer.NickName : null) ?? "N/A")));
		Debug.Log((object)$"是否已连接: {PhotonNetwork.IsConnected}");
		Debug.Log((object)$"是否为房主: {PhotonNetwork.IsMasterClient}");
		Debug.Log((object)$"网络时间: {PhotonNetwork.Time:F2}");
		Debug.Log((object)$"延迟: {PhotonNetwork.GetPing()} ms");
		Plugin.Log.LogInfo((object)"已显示服务器信息");
	}

	[ConsoleCommand]
	public static void GetPos(PhotonPlayer? target = null)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		Character val;
		if (target == null)
		{
			val = Character.localCharacter;
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogWarning((object)"getpos: 没有本地角色!");
				return;
			}
		}
		else
		{
			val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogWarning((object)("getpos: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
		}
		Vector3 center = val.Center;
		PhotonPlayer? obj = target;
		string text = ((obj != null) ? obj.NickName : null) ?? "Local Player";
		Debug.Log((object)string.Format("{0} 的位置: X:{1:F2} Y:{2:F2} Z:{3:F2}", new object[4] { text, center.x, center.y, center.z }));
		Plugin.Log.LogInfo((object)$"{text} 的位置: {center}");
	}

	[ConsoleCommand]
	public static void PerfStats()
	{
		Debug.Log((object)"=== 性能统计 ===");
		Debug.Log((object)$"FPS: {1f / Time.deltaTime:F1}");
		Debug.Log((object)$"帧间隔时间: {Time.deltaTime * 1000f:F1}ms");
		Debug.Log((object)$"时间缩放: {Time.timeScale}");
		Debug.Log((object)$"帧数: {Time.frameCount}");
		Debug.Log((object)$"运行时长: {Time.realtimeSinceStartup:F1}s");
		Debug.Log((object)$"角色总数: {Character.AllCharacters.Count}");
		Debug.Log((object)$"游戏对象总数: {Object.FindObjectsByType<GameObject>((FindObjectsSortMode)0).Length}");
		Plugin.Log.LogInfo((object)"已显示性能统计");
	}

	[ConsoleCommand]
	public static void Distance(PhotonPlayer player1, PhotonPlayer player2)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (player1 == null || player2 == null)
		{
			Debug.LogWarning((object)"distance: 未找到一名或两名玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player1) ?? false);
		Character val2 = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)player2) ?? false);
		if ((Object)(object)val == (Object)null || (Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"distance: 找不到一名或两名玩家的角色数据!");
			return;
		}
		float num = Vector3.Distance(val.Center, val2.Center);
		Debug.Log((object)$"{player1.NickName} 与 {player2.NickName} 之间的距离: {num:F2} 单位");
		Plugin.Log.LogInfo((object)$"{player1.NickName} 与 {player2.NickName} 之间的距离: {num:F2}");
	}

	[ConsoleCommand]
	public static void FindNearest(PhotonPlayer? target = null)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Character val;
		if (target == null)
		{
			val = Character.localCharacter;
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogWarning((object)"findnearest: 没有本地角色!");
				return;
			}
		}
		else
		{
			val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogWarning((object)("findnearest: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
		}
		Character val2 = null;
		float num = float.MaxValue;
		foreach (Character allCharacter in Character.AllCharacters)
		{
			if ((Object)(object)allCharacter != (Object)(object)val && !allCharacter.data.dead)
			{
				float num2 = Vector3.Distance(val.Center, allCharacter.Center);
				if (num2 < num)
				{
					num = num2;
					val2 = allCharacter;
				}
			}
		}
		if ((Object)(object)val2 != (Object)null)
		{
			PhotonPlayer? obj = target;
			string arg = ((obj != null) ? obj.NickName : null) ?? "Local Player";
			PhotonPlayer owner = ((MonoBehaviourPun)val2).photonView.Owner;
			string arg2 = ((owner != null) ? owner.NickName : null) ?? val2.characterName;
			Debug.Log((object)$"距离 {arg} 最近的玩家: {arg2} (距离: {num:F2})");
			Plugin.Log.LogInfo((object)$"距离 {arg} 最近的玩家: {arg2}, 距离 {num:F2}");
		}
		else
		{
			Debug.Log((object)"未找到其他玩家");
		}
	}
}
