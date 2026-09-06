using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class ScoutMasterCommands
{
	[ConsoleCommand]
	[MasterClientOnly("CallScoutmaster")]
	public static void CallScoutmaster(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"callscoutmaster: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("callscoutmaster: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"callscoutmaster: 地图上未找到 ScoutMaster!");
			return;
		}
		val2.SetCurrentTarget(val, 30f);
		Debug.Log((object)("callscoutmaster: ScoutMaster 正在追捕 '" + target.NickName + "' 30 秒"));
		Plugin.Log.LogInfo((object)("已设置 ScoutMaster 追捕玩家: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly("CallScoutmasterTime")]
	public static void CallScoutmasterTime(PhotonPlayer target, float forceTime = 30f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"callscoutmastertime: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("callscoutmastertime: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"callscoutmastertime: 地图上未找到 ScoutMaster!");
			return;
		}
		val2.SetCurrentTarget(val, forceTime);
		Debug.Log((object)$"callscoutmastertime: ScoutMaster 正在追捕 '{target.NickName}' {forceTime} 秒");
		Plugin.Log.LogInfo((object)$"已设置 ScoutMaster 追捕玩家: {target.NickName}, 时长 {forceTime} 秒");
	}

	[ConsoleCommand]
	[MasterClientOnly("StopScoutmaster")]
	public static void StopScoutmaster()
	{
		Scoutmaster val = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)"stopscoutmaster: 地图上未找到 ScoutMaster!");
			return;
		}
		val.SetCurrentTarget((Character)null, 0f);
		Debug.Log((object)"stopscoutmaster: 已清除 ScoutMaster 的目标");
		Plugin.Log.LogInfo((object)"已清除 ScoutMaster 的目标");
	}

	[ConsoleCommand]
	[MasterClientOnly("CallScoutmasterRandom")]
	public static void CallScoutmasterRandom(float forceTime = 30f)
	{
		List<Character> list = Character.AllCharacters.Where((Character c) => ((MonoBehaviourPun)c).photonView.Owner != null).ToList();
		if (list.Count == 0)
		{
			Debug.LogWarning((object)"callscoutmasterrandom: 未找到玩家!");
			return;
		}
		Character val = list[Random.Range(0, list.Count)];
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"callscoutmasterrandom: 地图上未找到 ScoutMaster!");
			return;
		}
		val2.SetCurrentTarget(val, forceTime);
		PhotonPlayer owner = ((MonoBehaviourPun)val).photonView.Owner;
		string arg = ((owner != null) ? owner.NickName : null) ?? "Unknown";
		Debug.Log((object)$"callscoutmasterrandom: ScoutMaster 正在追捕 '{arg}' {forceTime} 秒");
		Plugin.Log.LogInfo((object)$"已设置 ScoutMaster 追捕随机玩家: {arg}, 时长 {forceTime} 秒");
	}

	[ConsoleCommand]
	public static void ScoutmasterStatus()
	{
		Scoutmaster val = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)"scoutmasterstatus: 地图上未找到 ScoutMaster!");
			return;
		}
		Character currentTarget = val.currentTarget;
		if ((Object)(object)currentTarget == (Object)null)
		{
			Debug.Log((object)"scoutmasterstatus: ScoutMaster 当前没有目标");
			return;
		}
		PhotonPlayer owner = ((MonoBehaviourPun)currentTarget).photonView.Owner;
		string text = ((owner != null) ? owner.NickName : null) ?? "Unknown";
		Debug.Log((object)("scoutmasterstatus: ScoutMaster 当前正在追捕 '" + text + "'"));
	}

	[ConsoleCommand]
	[MasterClientOnly("TeleportScoutmaster")]
	public static void TeleportScoutmaster(PhotonPlayer target)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"teleportscoutmaster: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("teleportscoutmaster: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"teleportscoutmaster: 地图上未找到 ScoutMaster!");
			return;
		}
		Vector3 val3 = ((Component)val).transform.position + Vector3.up * 2f;
		PhotonView component = ((Component)val2).GetComponent<PhotonView>();
		if ((Object)(object)component != (Object)null)
		{
			component.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val3, false });
		}
		else
		{
			((Component)val2).transform.position = val3;
		}
		Debug.Log((object)("teleportscoutmaster: 已将 ScoutMaster 传送到 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已将 ScoutMaster 传送到玩家: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly("SpawnScoutmaster")]
	public static void SpawnScoutmaster()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Scoutmaster val = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val != (Object)null)
		{
			Debug.LogWarning((object)"spawnscoutmaster: 地图上已存在 ScoutMaster!");
			return;
		}
		ScoutmasterSpawner val2 = Object.FindFirstObjectByType<ScoutmasterSpawner>();
		if ((Object)(object)val2 != (Object)null)
		{
			Vector3 position = ((Component)val2).transform.position;
			Quaternion rotation = ((Component)val2).transform.rotation;
			GameObject val3 = PhotonNetwork.InstantiateRoomObject("Character_Scoutmaster", position, rotation, (byte)0, (object[])null);
			if ((Object)(object)val3 != (Object)null)
			{
				Character component = val3.GetComponent<Character>();
				if ((Object)(object)component != (Object)null)
				{
					component.data.spawnPoint = ((Component)val2).transform;
				}
				Debug.Log((object)"spawnscoutmaster: ScoutMaster 已成功生成!");
				Plugin.Log.LogInfo((object)"ScoutMaster 已成功生成");
			}
			else
			{
				Debug.LogError((object)"spawnscoutmaster: 生成 ScoutMaster 失败!");
			}
			return;
		}
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)localCharacter != (Object)null)
		{
			Vector3 val4 = ((Component)localCharacter).transform.position + ((Component)localCharacter).transform.forward * 5f;
			Quaternion identity = Quaternion.identity;
			GameObject val5 = PhotonNetwork.InstantiateRoomObject("Character_Scoutmaster", val4, identity, (byte)0, (object[])null);
			if ((Object)(object)val5 != (Object)null)
			{
				Debug.Log((object)"spawnscoutmaster: ScoutMaster 已在玩家附近生成!");
				Plugin.Log.LogInfo((object)"ScoutMaster 已在玩家附近生成");
			}
			else
			{
				Debug.LogError((object)"spawnscoutmaster: 生成 ScoutMaster 失败!");
			}
		}
		else
		{
			Debug.LogError((object)"spawnscoutmaster: 未找到用于生成参考的本地角色!");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly("BugleCall")]
	public static void BugleCall(float forceTime = 30f)
	{
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)localCharacter == (Object)null)
		{
			Debug.LogWarning((object)"buglecall: 未找到本地角色!");
			return;
		}
		Scoutmaster val = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)"buglecall: 地图上未找到 ScoutMaster!");
			return;
		}
		val.SetCurrentTarget(localCharacter, forceTime);
		Debug.Log((object)$"buglecall: 你吹响了号角, ScoutMaster 将追捕你 {forceTime} 秒 (号角效果)");
		Plugin.Log.LogInfo((object)$"号角呼叫: 已设置 ScoutMaster 追捕本地玩家, 时长 {forceTime} 秒");
	}

	[ConsoleCommand]
	[MasterClientOnly("BugleCallOnPlayer")]
	public static void BugleCallOnPlayer(PhotonPlayer target, float forceTime = 30f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"buglecallonplayer: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("buglecallonplayer: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"buglecallonplayer: 地图上未找到 ScoutMaster!");
			return;
		}
		val2.SetCurrentTarget(val, forceTime);
		Debug.Log((object)$"buglecallonplayer: ScoutMaster 已锁定 '{target.NickName}' {forceTime} 秒 (如同其吹响了号角)");
		Plugin.Log.LogInfo((object)$"已对玩家施加号角效果: {target.NickName}, 时长 {forceTime} 秒");
	}

	[ConsoleCommand]
	[MasterClientOnly("CursePlayer")]
	public static void CursePlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"curseplayer: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("curseplayer: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Scoutmaster val2 = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)"curseplayer: 地图上未找到 ScoutMaster!");
			return;
		}
		val2.SetCurrentTarget(val, 600f);
		Debug.Log((object)("curseplayer: '" + target.NickName + "' 已被诅咒 - ScoutMaster 将追捕其 10 分钟!"));
		Plugin.Log.LogInfo((object)("玩家被诅咒: " + target.NickName + " - ScoutMaster 将追捕 10 分钟"));
	}

	[ConsoleCommand]
	[MasterClientOnly("RemoveScoutmaster")]
	public static void RemoveScoutmaster()
	{
		Scoutmaster val = Object.FindFirstObjectByType<Scoutmaster>();
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)"removescoutmaster: 地图上未找到 ScoutMaster!");
			return;
		}
		PhotonView component = ((Component)val).GetComponent<PhotonView>();
		if ((Object)(object)component != (Object)null)
		{
			PhotonNetwork.Destroy(component);
			Debug.Log((object)"removescoutmaster: 已从地图移除 ScoutMaster!");
			Plugin.Log.LogInfo((object)"ScoutMaster 已成功移除");
		}
		else
		{
			Debug.LogError((object)"removescoutmaster: 未能在 ScoutMaster 上找到 PhotonView!");
		}
	}

	[ConsoleCommand]
	public static void Bugle()
	{
		BugleCall();
	}
}
