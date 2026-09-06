using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class StatusCommands
{
	[ConsoleCommand]
	public static void ClearHunger(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Hunger, 0f, "清除饥饿");
	}

	[ConsoleCommand]
	public static void AddHunger(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Hunger, amount, "添加饥饿");
	}

	[ConsoleCommand]
	public static void ClearCold(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Cold, 0f, "清除寒冷");
	}

	[ConsoleCommand]
	public static void AddCold(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Cold, amount, "添加寒冷");
	}

	[ConsoleCommand]
	public static void ClearPoison(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Poison, 0f, "清除中毒");
	}

	[ConsoleCommand]
	public static void AddPoison(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Poison, amount, "添加中毒");
	}

	[ConsoleCommand]
	public static void ClearInjury(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Injury, 0f, "清除受伤");
	}

	[ConsoleCommand]
	public static void AddInjury(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Injury, amount, "添加受伤");
	}

	[ConsoleCommand]
	public static void ClearCurse(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Curse, 0f, "清除诅咒");
	}

	[ConsoleCommand]
	public static void AddCurse(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Curse, amount, "添加诅咒");
	}

	[ConsoleCommand]
	public static void ClearDrowsy(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Drowsy, 0f, "清除困倦");
	}

	[ConsoleCommand]
	public static void AddDrowsy(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Drowsy, amount, "添加困倦");
	}

	[ConsoleCommand]
	public static void ClearHot(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Hot, 0f, "清除炎热");
	}

	[ConsoleCommand]
	public static void AddHot(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Hot, amount, "添加炎热");
	}

	[ConsoleCommand]
	public static void ClearThorns(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Thorns, 0f, "清除荆棘");
	}

	[ConsoleCommand]
	public static void AddThorns(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Thorns, amount, "添加荆棘");
	}

	[ConsoleCommand]
	public static void ClearSpores(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Spores, 0f, "清除孢子");
	}

	[ConsoleCommand]
	public static void AddSpores(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Spores, amount, "添加孢子");
	}

	[ConsoleCommand]
	public static void ClearWeb(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Web, 0f, "清除蛛网");
	}

	[ConsoleCommand]
	public static void AddWeb(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Web, amount, "添加蛛网");
	}

	[ConsoleCommand]
	public static void ClearArrow(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Arrow, 0f, "清除箭矢");
	}

	[ConsoleCommand]
	public static void AddArrow(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Arrow, amount, "添加箭矢");
	}

	[ConsoleCommand]
	public static void ClearPetrify(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Petrify, 0f, "清除石化");
	}

	[ConsoleCommand]
	public static void AddPetrify(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Petrify, amount, "添加石化");
	}

	[ConsoleCommand]
	public static void ClearCrab(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Crab, 0f, "清除螃蟹");
	}

	[ConsoleCommand]
	public static void AddCrab(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Crab, amount, "添加螃蟹");
	}

	[ConsoleCommand]
	public static void ClearWeight(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Weight, 0f, "清除负重");
	}

	[ConsoleCommand]
	public static void AddWeight(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.Weight, amount, "添加负重");
	}

	[ConsoleCommand]
	public static void ClearFlyTrap(PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.FlyTrap, 0f, "清除捕蝇草");
	}

	[ConsoleCommand]
	public static void AddFlyTrap(float amount = 1f, PhotonPlayer? target = null)
	{
		SetStatus(target, CharacterAfflictions.STATUSTYPE.FlyTrap, amount, "添加捕蝇草");
	}

	[ConsoleCommand]
	public static void ShowStatus(PhotonPlayer? target = null)
	{
		Character targetCharacter = GetTargetCharacter(target);
		if ((UnityEngine.Object)(object)targetCharacter == (UnityEngine.Object)null)
		{
			return;
		}
		CharacterAfflictions afflictions = targetCharacter.refs.afflictions;
		if ((UnityEngine.Object)(object)afflictions == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("showstatus: 未找到 " + targetCharacter.characterName + " 的状态组件"));
			return;
		}
		Debug.Log((object)$"=== {targetCharacter.characterName} 的状态 ===");
		foreach (CharacterAfflictions.STATUSTYPE value in Enum.GetValues(typeof(CharacterAfflictions.STATUSTYPE)))
		{
			float currentStatus = afflictions.GetCurrentStatus(value);
			if (currentStatus > 0.01f)
			{
				Debug.Log((object)$"  {value}: {currentStatus:F2}");
			}
		}
		Debug.Log((object)"==================");
	}

	[ConsoleCommand]
	public static void ClearAllStatuses(PhotonPlayer? target = null)
	{
		Character targetCharacter = GetTargetCharacter(target);
		if ((UnityEngine.Object)(object)targetCharacter == (UnityEngine.Object)null)
		{
			return;
		}
		ApplyClearAll(targetCharacter);
		Debug.Log((object)("已清除 " + targetCharacter.characterName + " 的所有状态"));
		Plugin.Log.LogInfo((object)("已清除所有状态: " + targetCharacter.characterName));
	}

	private static void SetStatus(PhotonPlayer? target, CharacterAfflictions.STATUSTYPE type, float amount, string actionName)
	{
		Character targetCharacter = GetTargetCharacter(target);
		if ((UnityEngine.Object)(object)targetCharacter == (UnityEngine.Object)null)
		{
			return;
		}
		CharacterAfflictions afflictions = targetCharacter.refs.afflictions;
		if ((UnityEngine.Object)(object)afflictions == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)$"未找到 {targetCharacter.characterName} 的状态组件");
			return;
		}
		float targetValue = Mathf.Clamp01(amount);
		PhotonView photonView = ((MonoBehaviourPun)targetCharacter).photonView;
		bool isLocal = (UnityEngine.Object)(object)photonView != (UnityEngine.Object)null && photonView.IsMine;
		if (isLocal)
		{
			afflictions.SetStatus(type, targetValue);
		}
		else if ((UnityEngine.Object)(object)photonView != (UnityEngine.Object)null)
		{
			float[] currentStatuses = afflictions.currentStatuses ?? new float[15];
			float currentValue = ((int)type < currentStatuses.Length) ? currentStatuses[(int)type] : 0f;
			float[] deltas = new float[15];
			deltas[(int)type] = targetValue - currentValue;
			photonView.RPC("RPC_ApplyStatusesFromFloatArray", (RpcTarget)0, new object[1] { deltas });
		}
		Debug.Log((object)$"{actionName}: 已对 '{targetCharacter.characterName}' 设置 {type} = {targetValue:F2}");
		Plugin.Log.LogInfo((object)$"{actionName}: {targetCharacter.characterName}, {type}={targetValue:F2}");
	}

	private static void ApplyClearAll(Character character)
	{
		if ((UnityEngine.Object)(object)character == (UnityEngine.Object)null)
		{
			return;
		}
		CharacterAfflictions afflictions = character.refs.afflictions;
		if ((UnityEngine.Object)(object)afflictions == (UnityEngine.Object)null)
		{
			return;
		}
		PhotonView photonView = ((MonoBehaviourPun)character).photonView;
		if ((UnityEngine.Object)(object)photonView != (UnityEngine.Object)null && photonView.IsMine)
		{
			afflictions.ClearAllStatus(excludeCurse: false, excludePetrify: false);
			return;
		}
		if ((UnityEngine.Object)(object)photonView != (UnityEngine.Object)null)
		{
			float[] currentStatuses = afflictions.currentStatuses ?? new float[15];
			float[] deltas = new float[15];
			for (int i = 0; i < Mathf.Min(currentStatuses.Length, deltas.Length); i++)
			{
				deltas[i] = 0f - currentStatuses[i];
			}
			photonView.RPC("RPC_ApplyStatusesFromFloatArray", (RpcTarget)0, new object[1] { deltas });
		}
	}

	private static Character? GetTargetCharacter(PhotonPlayer? target)
	{
		if (target != null)
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				PhotonPlayer? obj = target;
				Debug.LogWarning((object)("未找到玩家 '" + (((obj != null) ? obj.NickName : null) ?? "unknown") + "' 的角色"));
				return null;
			}
			return val;
		}
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"未找到本地角色!");
		}
		return localCharacter;
	}
}
