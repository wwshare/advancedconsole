using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class RPCCommands
{
	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void JumpPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"jump: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("jump: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		CharacterMovement component = ((Component)val).GetComponent<CharacterMovement>();
		if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
		{
			((MonoBehaviourPun)val).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
			Debug.Log((object)("jump: 已让玩家 '" + target.NickName + "' 跳跃"));
			Plugin.Log.LogInfo((object)("已让玩家跳跃: " + target.NickName));
		}
		else
		{
			Debug.LogWarning((object)("jump: 未找到玩家 '" + target.NickName + "'"));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetDuskTime()
	{
		TimeOfDaySync.SyncTimeOfDay(0.75f);
		Debug.Log((object)"已将所有玩家的时间设置为黄昏");
		Plugin.Log.LogInfo((object)"已将所有玩家的时间设置为黄昏");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void JumpAllPlayers()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			CharacterMovement component = ((Component)allCharacter).GetComponent<CharacterMovement>();
			if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
			{
				((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
			}
		}
		Debug.Log((object)"jumpall: 已让所有玩家跳跃");
		Plugin.Log.LogInfo((object)"已让所有玩家跳跃");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void FallPlayer(PhotonPlayer target, float force = 7f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"fall: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("fall: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { force });
		Debug.Log((object)$"fall: 已让玩家 '{target.NickName}' 以力度 {force} 摔倒");
		Plugin.Log.LogInfo((object)$"已让玩家摔倒: {target.NickName}, 力度 {force}");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PassOutPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"passout: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("passout: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("RPCA_PassOut", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("passout: 已让玩家 '" + target.NickName + "' 晕厥"));
		Plugin.Log.LogInfo((object)("已让玩家晕厥: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void UnPassOutPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"unpassout: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("unpassout: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("RPCA_UnPassOut", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("unpassout: 已使玩家 '" + target.NickName + "' 恢复清醒"));
		Plugin.Log.LogInfo((object)("已使玩家恢复清醒: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void RevivePlayerRPC(PhotonPlayer target, bool applyStatus = true)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"revive: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("revive: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("RPCA_Revive", (RpcTarget)0, new object[1] { applyStatus });
		Debug.Log((object)$"revive: 已复活玩家 '{target.NickName}' (已应用状态: {applyStatus})");
		Plugin.Log.LogInfo((object)$"已复活玩家: {target.NickName} (已应用状态: {applyStatus})");
	}

	[ConsoleCommand]
	public static void StopClimbingPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"stopclimbing: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("stopclimbing: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("StopClimbingRpc", (RpcTarget)0, new object[1] { true });
		((MonoBehaviourPun)val).photonView.RPC("StopRopeClimbingRpc", (RpcTarget)0, Array.Empty<object>());
		((MonoBehaviourPun)val).photonView.RPC("StopVineClimbingRpc", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("stopclimbing: 已停止玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已停止玩家攀爬: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void StartCarryPlayer(PhotonPlayer carrier, PhotonPlayer target)
	{
		if (carrier == null || target == null)
		{
			Debug.LogWarning((object)"carry: 未找到一名或两名玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)carrier) ?? false);
		Character val2 = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null || (UnityEngine.Object)(object)val2 == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"carry: 未找到一名或两名玩家的角色!");
			return;
		}
		((MonoBehaviourPun)val).photonView.RPC("RPCA_StartCarry", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)val2).photonView });
		Debug.Log((object)("carry: '" + carrier.NickName + "' 正在背负 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("玩家 " + carrier.NickName + " 正在背负 " + target.NickName));
	}

	[ConsoleCommand]
	public static void RenderPlayerDead(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"renderdead: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("renderdead: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((Component)val.refs.customization).GetComponent<PhotonView>().RPC("CharacterDied", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("renderdead: 已将玩家 '" + target.NickName + "' 渲染为死亡状态"));
		Plugin.Log.LogInfo((object)("已将玩家渲染为死亡状态: " + target.NickName));
	}

	[ConsoleCommand]
	public static void RenderPlayerPassedOut(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"renderpassedout: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("renderpassedout: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((Component)val.refs.customization).GetComponent<PhotonView>().RPC("CharacterPassedOut", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("renderpassedout: 已将玩家 '" + target.NickName + "' 渲染为晕厥状态"));
		Plugin.Log.LogInfo((object)("已将玩家渲染为晕厥状态: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void DestroyHeldItem(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"destroyhelditem: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("destroyhelditem: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		val.refs.items.photonView.RPC("DestroyHeldItemRpc", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("destroyhelditem: 已销毁玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已销毁玩家手持物品: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TriggerHelicopter()
	{
		Flare[] array = UnityEngine.Object.FindObjectsByType<Flare>((FindObjectsSortMode)0);
		if (array.Length != 0)
		{
			Flare val = array[0];
			((Component)val).GetComponent<PhotonView>().RPC("TriggerHelicopter", (RpcTarget)3, Array.Empty<object>());
			Debug.Log((object)"triggerhelicopter: 已召唤直升机");
			Plugin.Log.LogInfo((object)"已召唤直升机");
		}
		else
		{
			Debug.LogWarning((object)"triggerhelicopter: 场景中未找到信号弹");
		}
	}

	[ConsoleCommand]
	public static void LightAllFlares()
	{
		Flare[] array = UnityEngine.Object.FindObjectsByType<Flare>((FindObjectsSortMode)0);
		Flare[] array2 = array;
		foreach (Flare val in array2)
		{
			((Component)val).GetComponent<PhotonView>().RPC("SetFlareLitRPC", (RpcTarget)3, Array.Empty<object>());
		}
		Debug.Log((object)$"lightflares: 已点燃 {array.Length} 个信号弹");
		Plugin.Log.LogInfo((object)$"已点燃 {array.Length} 个信号弹");
	}

	[ConsoleCommand]
	public static void LightNearestCampfire()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"lightcampfire: 未找到本地角色!");
			return;
		}
		Campfire[] array = UnityEngine.Object.FindObjectsByType<Campfire>((FindObjectsSortMode)0);
		Campfire val = null;
		float num = 10f;
		Campfire[] array2 = array;
		foreach (Campfire val2 in array2)
		{
			float num2 = Vector3.Distance(localCharacter.Center, ((Component)val2).transform.position);
			if (num2 < num)
			{
				val = val2;
				num = num2;
			}
		}
		if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
		{
			((Component)val).GetComponent<PhotonView>().RPC("Light_Rpc", (RpcTarget)0, Array.Empty<object>());
			Debug.Log((object)$"lightcampfire: 已点燃最近的篝火, 距离 {num:F1}m");
			Plugin.Log.LogInfo((object)$"已点燃最近的篝火, 距离 {num:F1}m");
		}
		else
		{
			Debug.LogWarning((object)"lightcampfire: 10 米内未找到篝火!");
		}
	}

	[ConsoleCommand]
	public static void ExtinguishNearestCampfire()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"extinguishcampfire: 未找到本地角色!");
			return;
		}
		Campfire[] array = UnityEngine.Object.FindObjectsByType<Campfire>((FindObjectsSortMode)0);
		Campfire val = null;
		float num = 10f;
		Campfire[] array2 = array;
		foreach (Campfire val2 in array2)
		{
			float num2 = Vector3.Distance(localCharacter.Center, ((Component)val2).transform.position);
			if (num2 < num)
			{
				val = val2;
				num = num2;
			}
		}
		if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
		{
			((Component)val).GetComponent<PhotonView>().RPC("Extinguish_Rpc", (RpcTarget)0, Array.Empty<object>());
			Debug.Log((object)$"extinguishcampfire: 已熄灭最近的篝火, 距离 {num:F1}m");
			Plugin.Log.LogInfo((object)$"已熄灭最近的篝火, 距离 {num:F1}m");
		}
		else
		{
			Debug.LogWarning((object)"extinguishcampfire: 10 米内未找到篝火!");
		}
	}

	[ConsoleCommand]
	public static void ShakeAllIcicles()
	{
		ShakyIcicleIce2[] array = UnityEngine.Object.FindObjectsByType<ShakyIcicleIce2>((FindObjectsSortMode)0);
		ShakyIcicleIce2[] array2 = array;
		foreach (ShakyIcicleIce2 val in array2)
		{
			((Component)val).GetComponent<PhotonView>().RPC("ShakeRock_Rpc", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)$"shakeicicles: 已摇晃 {array.Length} 个冰柱");
		Plugin.Log.LogInfo((object)$"已摇晃 {array.Length} 个冰柱");
	}

	[ConsoleCommand]
	public static void ShakeAllBridges()
	{
		BreakableBridge[] array = UnityEngine.Object.FindObjectsByType<BreakableBridge>((FindObjectsSortMode)0);
		BreakableBridge[] array2 = array;
		foreach (BreakableBridge val in array2)
		{
			val.photonView.RPC("ShakeBridge_Rpc", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)$"shakebridges: 已摇晃 {array.Length} 座桥");
		Plugin.Log.LogInfo((object)$"已摇晃 {array.Length} 座桥");
	}

	[ConsoleCommand]
	public static void FireAllArrows()
	{
		ArrowShooter[] array = UnityEngine.Object.FindObjectsByType<ArrowShooter>((FindObjectsSortMode)0);
		ArrowShooter[] array2 = array;
		foreach (ArrowShooter val in array2)
		{
			((MonoBehaviourPun)val).photonView.RPC("FireArrow_RPC", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)$"firearrows: 已触发 {array.Length} 个箭矢发射器");
		Plugin.Log.LogInfo((object)$"已触发 {array.Length} 个箭矢发射器");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnEruption(PhotonPlayer target)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"eruption: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("eruption: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		EruptionSpawner[] array = UnityEngine.Object.FindObjectsByType<EruptionSpawner>((FindObjectsSortMode)0);
		EruptionSpawner[] array2 = array;
		foreach (EruptionSpawner val2 in array2)
		{
			((Component)val2).GetComponent<PhotonView>().RPC("RPCA_SpawnEruption", (RpcTarget)0, new object[1] { val.Head });
		}
		Debug.Log((object)("eruption: 已在玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已在玩家身上生成喷发: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void CookAllItems(int cookLevel = 100)
	{
		Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
		Item[] array2 = array;
		foreach (Item val in array2)
		{
			((MonoBehaviourPun)val).photonView.RPC("SetCookedAmountRPC", (RpcTarget)0, new object[1] { cookLevel });
		}
		Debug.Log((object)$"cookitems: 已将 {array.Length} 个物品烹饪到等级 {cookLevel}");
		Plugin.Log.LogInfo((object)$"已将 {array.Length} 个物品烹饪到等级 {cookLevel}");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void FinishCookingAllItems()
	{
		Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
		Item[] array2 = array;
		foreach (Item val in array2)
		{
			if ((UnityEngine.Object)(object)((MonoBehaviourPun)val).photonView != (UnityEngine.Object)null)
			{
				((MonoBehaviourPun)val).photonView.RPC("FinishCookingRPC", (RpcTarget)0, Array.Empty<object>());
			}
		}
		Debug.Log((object)$"finishcooking: 已完成烹饪 {array.Length} 个物品");
		Plugin.Log.LogInfo((object)$"已完成烹饪 {array.Length} 个物品");
	}

	[ConsoleCommand]
	public static void ToggleCookingSmoke(bool enabled = true)
	{
		ItemCooking[] array = UnityEngine.Object.FindObjectsByType<ItemCooking>((FindObjectsSortMode)0);
		ItemCooking[] array2 = array;
		foreach (ItemCooking val in array2)
		{
			((ItemComponent)val).photonView.RPC("EnableCookingSmokeRPC", (RpcTarget)0, new object[1] { enabled });
		}
		string arg = (enabled ? "enabled" : "disabled");
		Debug.Log((object)$"cookingsmoke: {arg} 烹饪烟雾, 作用于 {array.Length} 个物品");
		Plugin.Log.LogInfo((object)$"{arg} 烹饪烟雾, 作用于 {array.Length} 个物品");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void DenyPickupAllItems()
	{
		Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
		Item[] array2 = array;
		foreach (Item val in array2)
		{
			if ((UnityEngine.Object)(object)((MonoBehaviourPun)val).photonView != (UnityEngine.Object)null)
			{
				((MonoBehaviourPun)val).photonView.RPC("DenyPickupRPC", (RpcTarget)0, Array.Empty<object>());
			}
		}
		Debug.Log((object)$"denypickup: 已禁止拾取 {array.Length} 个物品");
		Plugin.Log.LogInfo((object)$"已禁止拾取 {array.Length} 个物品");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ForceWin()
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter != (UnityEngine.Object)null)
		{
			((MonoBehaviourPun)localCharacter).photonView.RPC("RPCEndGame_ForceWin", (RpcTarget)0, Array.Empty<object>());
			Debug.Log((object)"forcewin: 已强制游戏胜利");
			Plugin.Log.LogInfo((object)"已强制游戏胜利");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void UpdatePeakTimer(int timerTime = 10)
	{
		PeakSequence[] array = UnityEngine.Object.FindObjectsByType<PeakSequence>((FindObjectsSortMode)0);
		PeakSequence[] array2 = array;
		foreach (PeakSequence val in array2)
		{
			((Component)val).GetComponent<PhotonView>().RPC("RPCUpdateTimer", (RpcTarget)0, new object[1] { timerTime });
		}
		Debug.Log((object)$"peaktimer: 已更新计时器为 {timerTime} 秒");
		Plugin.Log.LogInfo((object)$"已更新山顶计时器为 {timerTime} 秒");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void BeginIslandLoad(string sceneName = "Title", int loadType = 2)
	{
		AirportCheckInKiosk val = UnityEngine.Object.FindFirstObjectByType<AirportCheckInKiosk>();
		if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
		{
			((MonoBehaviourPun)val).photonView.RPC("BeginIslandLoadRPC", (RpcTarget)0, new object[2] { sceneName, loadType });
			Debug.Log((object)$"islandload: 已开始加载场景 '{sceneName}', 类型为 {loadType}");
			Plugin.Log.LogInfo((object)$"已开始加载场景 '{sceneName}', 类型为 {loadType}");
		}
		else
		{
			Debug.LogWarning((object)"islandload: 未找到 AirportCheckInKiosk");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TriggerBananaPeel(PhotonPlayer target)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"banana: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("banana: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		BananaPeel val2 = UnityEngine.Object.FindFirstObjectByType<BananaPeel>();
		if ((UnityEngine.Object)(object)val2 == (UnityEngine.Object)null)
		{
			val2 = PhotonNetwork.Instantiate("0_Items/Berrynana Peel Pink Variant", val.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BananaPeel>();
		}
		((Component)val2).GetComponent<PhotonView>().RPC("RPCA_TriggerBanana", (RpcTarget)0, new object[1] { val.refs.view.ViewID });
		Debug.Log((object)("banana: 已在玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已在玩家身上触发香蕉皮效果: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void FallAllPlayers(float force = 7f)
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { force });
		}
		Debug.Log((object)$"fallall: 已让所有玩家以力度 {force} 摔倒");
		Plugin.Log.LogInfo((object)$"已让所有玩家以力度 {force} 摔倒");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ReviveAllPlayers(bool applyStatus = true)
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_Revive", (RpcTarget)0, new object[1] { applyStatus });
		}
		Debug.Log((object)$"reviveall: 已复活所有玩家 (已应用状态: {applyStatus})");
		Plugin.Log.LogInfo((object)$"已复活所有玩家 (已应用状态: {applyStatus})");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PassOutAllPlayers()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_PassOut", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)"passoutall: 已让所有玩家晕厥");
		Plugin.Log.LogInfo((object)"已让所有玩家晕厥");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void UnPassOutAllPlayers()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_UnPassOut", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)"unpassoutall: 已使所有玩家恢复清醒");
		Plugin.Log.LogInfo((object)"已使所有玩家恢复清醒");
	}

	[ConsoleCommand]
	public static void StopClimbingAllPlayers()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((MonoBehaviourPun)allCharacter).photonView.RPC("StopClimbingRpc", (RpcTarget)0, new object[1] { true });
			((MonoBehaviourPun)allCharacter).photonView.RPC("StopRopeClimbingRpc", (RpcTarget)0, Array.Empty<object>());
			((MonoBehaviourPun)allCharacter).photonView.RPC("StopVineClimbingRpc", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)"stopclimbingall: 已停止所有玩家的攀爬");
		Plugin.Log.LogInfo((object)"已停止所有玩家的攀爬");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void DestroyAllHeldItems()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			allCharacter.refs.items.photonView.RPC("DestroyHeldItemRpc", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)"destroyallhelditems: 已销毁所有手持物品");
		Plugin.Log.LogInfo((object)"已销毁所有手持物品");
	}

	[ConsoleCommand]
	public static void RenderAllPlayersDead()
	{
		foreach (Character allCharacter in Character.AllCharacters)
		{
			((Component)allCharacter.refs.customization).GetComponent<PhotonView>().RPC("CharacterDied", (RpcTarget)0, Array.Empty<object>());
		}
		Debug.Log((object)"renderalldead: 已将所有玩家渲染为死亡状态");
		Plugin.Log.LogInfo((object)"已将所有玩家渲染为死亡状态");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToBeach()
	{
		SegmentSync.SyncJumpToSegment((Segment)0);
		Debug.Log((object)"已将所有玩家同步到 Beach 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 Beach 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToTropics()
	{
		SegmentSync.SyncJumpToSegment((Segment)1);
		Debug.Log((object)"已将所有玩家同步到 Tropics 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 Tropics 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToAlpine()
	{
		SegmentSync.SyncJumpToSegment((Segment)2);
		Debug.Log((object)"已将所有玩家同步到 Alpine 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 Alpine 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToCaldera()
	{
		SegmentSync.SyncJumpToSegment((Segment)3);
		Debug.Log((object)"已将所有玩家同步到 Caldera 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 Caldera 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToKiln()
	{
		SegmentSync.SyncJumpToSegment((Segment)4);
		Debug.Log((object)"已将所有玩家同步到 TheKiln 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 TheKiln 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncGoToPeak()
	{
		SegmentSync.SyncJumpToSegment((Segment)5);
		Debug.Log((object)"已将所有玩家同步到 Peak 区域");
		Plugin.Log.LogInfo((object)"已将所有玩家同步到 Peak 区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncJumpToSegment(string segmentName)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Segment result2;
			if (byte.TryParse(segmentName, out var result))
			{
				if (result > 5)
				{
					Debug.LogWarning((object)$"syncjumptosegment: 无效的区域编号 {result}。有效范围: 0-5");
					return;
				}
				result2 = (Segment)result;
			}
			else if (!Enum.TryParse<Segment>(segmentName, true, out result2))
			{
				Debug.LogWarning((object)("syncjumptosegment: 未知区域 '" + segmentName + "'。请使用 ListSegments 查看可用区域"));
				return;
			}
			SegmentSync.SyncJumpToSegment(result2);
			Debug.Log((object)$"已将所有玩家同步到 {result2} 区域");
			Plugin.Log.LogInfo((object)$"已将所有玩家同步到 {result2} 区域");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("syncjumptosegment: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncTimeOfDay(float timeValue = 0.5f)
	{
		TimeOfDaySync.SyncTimeOfDay(timeValue);
		Debug.Log((object)$"已将所有玩家的时间同步为 {timeValue:F2}");
		Plugin.Log.LogInfo((object)$"已将时间同步为 {timeValue:F2}");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetDayTime()
	{
		TimeOfDaySync.SyncTimeOfDay(0.5f);
		Debug.Log((object)"已将所有玩家的时间设置为白天 (正午)");
		Plugin.Log.LogInfo((object)"已将所有玩家的时间设置为白天");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetNightTime()
	{
		TimeOfDaySync.SyncTimeOfDay(0f);
		Debug.Log((object)"已将所有玩家的时间设置为夜晚 (午夜)");
		Plugin.Log.LogInfo((object)"已将所有玩家的时间设置为夜晚");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetDawnTime()
	{
		TimeOfDaySync.SyncTimeOfDay(0.25f);
		Debug.Log((object)"已将所有玩家的时间设置为黎明");
		Plugin.Log.LogInfo((object)"已将所有玩家的时间设置为黎明");
	}
}
