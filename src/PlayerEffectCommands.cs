using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class PlayerEffectCommands
{
	private static string GetEmoteAnimationName(int emotionId)
	{
		return emotionId switch
		{
			0 => "A_Scout_Emote_Salute", 
			1 => "A_Scout_Emote_ThumbsUp", 
			2 => "A_Scout_Emote_Think", 
			3 => "A_Scout_Emote_Nono", 
			4 => "A_Scout_Emote_Flex", 
			5 => "A_Scout_Emote_Shrug", 
			6 => "A_Scout_Emote_CrossedArms", 
			7 => "A_Scout_Emote_Dance1", 
			_ => "A_Scout_Emote_Salute", 
		};
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetPlayerEmotion(PhotonPlayer target, int emotionId)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"setplayeremotion: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("setplayeremotion: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			string emoteAnimationName = GetEmoteAnimationName(emotionId);
			((MonoBehaviourPun)val).photonView.RPC("RPCA_PlayRemove", (RpcTarget)0, new object[2] { emoteAnimationName, true });
			Debug.Log((object)$"setplayeremotion: 已为玩家 '{target.NickName}' 设置表情 {emotionId}");
			Plugin.Log.LogInfo((object)$"已为玩家设置表情 {emotionId}: {target.NickName}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setplayeremotion: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void RestorePlayerStamina(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"restoreplayerstamina: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("restoreplayerstamina: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			((MonoBehaviourPun)val).photonView.RPC("MoraleBoost", (RpcTarget)0, new object[2] { 1f, 0 });
			Debug.Log((object)("restoreplayerstamina: 已恢复玩家 '" + target.NickName + "'"));
			Plugin.Log.LogInfo((object)("已恢复玩家的耐力: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("restoreplayerstamina: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void HealPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"healplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("healplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			CharacterAfflictions afflictions = val.refs.afflictions;
			afflictions.SetStatus((CharacterAfflictions.STATUSTYPE)2, 0f);
			afflictions.SetStatus((CharacterAfflictions.STATUSTYPE)1, 0f);
			afflictions.SetStatus((CharacterAfflictions.STATUSTYPE)0, 0f);
			afflictions.SetStatus((CharacterAfflictions.STATUSTYPE)3, 0f);
			afflictions.SetStatus((CharacterAfflictions.STATUSTYPE)6, 0f);
			try
			{
				Component[] components = ((Component)val).GetComponents<Component>();
				Component[] array = components;
				foreach (Component val2 in array)
				{
					FieldInfo fieldInfo = ((object)val2).GetType().GetField("currentHealth") ?? ((object)val2).GetType().GetField("health") ?? ((object)val2).GetType().GetField("hp");
					if (fieldInfo != null)
					{
						fieldInfo.SetValue(val2, 100f);
						break;
					}
				}
				val.data.currentStamina = val.data.TotalStamina;
			}
			catch (Exception ex)
			{
				Debug.LogWarning((object)("healplayer: 无法恢复生命/耐力 - " + ex.Message));
			}
			if (val.data.dead)
			{
				val.data.dead = false;
				try
				{
					MethodInfo methodInfo = ((object)val).GetType().GetMethod("CallRevive") ?? ((object)val).GetType().GetMethod("Revive") ?? ((object)val).GetType().GetMethod("Resurrect");
					if (methodInfo != null)
					{
						methodInfo.Invoke(val, null);
					}
					else
					{
						Debug.LogWarning((object)"healplayer: 未找到复活方法, 角色已标记为存活但可能需要手动复活");
					}
				}
				catch (Exception ex2)
				{
					Debug.LogWarning((object)("healplayer: 无法调用复活方法 - " + ex2.Message));
				}
			}
			Debug.Log((object)("healplayer: 已治疗玩家 '" + target.NickName + "'"));
			Plugin.Log.LogInfo((object)("已治疗玩家: " + target.NickName));
		}
		catch (Exception ex3)
		{
			Debug.LogError((object)("healplayer: 错误 - " + ex3.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void KillPlayer(PhotonPlayer target)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"killplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("killplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			if (val.data.dead)
			{
				Debug.LogWarning((object)("killplayer: 玩家 '" + target.NickName + "' 已经死亡!"));
				return;
			}
			((MonoBehaviourPun)val).photonView.RPC("RPCA_Die", (RpcTarget)0, Array.Empty<object>());
			Debug.Log((object)("killplayer: 已击杀玩家 '" + target.NickName + "'"));
			Plugin.Log.LogInfo((object)("已击杀玩家: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("killplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void RevivePlayer(PhotonPlayer target)
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"reviveplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character character = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)character == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("reviveplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			if (!character.data.dead)
			{
				Debug.LogWarning((object)("reviveplayer: 玩家 '" + target.NickName + "' 已经存活!"));
				return;
			}
			((MonoBehaviourPun)character).photonView.RPC("RPCA_Revive", (RpcTarget)0, new object[1] { false });
			List<Character> list = Character.AllCharacters.Where((Character c) => (UnityEngine.Object)(object)c != (UnityEngine.Object)(object)character && !c.data.dead && ((MonoBehaviourPun)c).photonView.Owner != null).ToList();
			Vector3 val2 = default(Vector3);
			if (list.Count > 0)
			{
				Character val = list[UnityEngine.Random.Range(0, list.Count)];
				val2 = val.Center + Vector3.up * 2f;
				Debug.Log((object)("reviveplayer: Reviving '" + target.NickName + "' 复活在存活玩家 '" + ((MonoBehaviourPun)val).photonView.Owner.NickName + "'"));
			}
			else
			{
				SpawnPoint[] array = UnityEngine.Object.FindObjectsByType<SpawnPoint>((FindObjectsInactive)1, (FindObjectsSortMode)0);
				if (array.Length != 0)
				{
					SpawnPoint val3 = array[UnityEngine.Random.Range(0, array.Length)];
					val2 = ((Component)val3).transform.position + Vector3.up * 1f;
					Debug.Log((object)("reviveplayer: Reviving '" + target.NickName + "' 复活在出生点"));
				}
				else
				{
					val2 = new Vector3(0f, 10f, 0f);
					Debug.Log((object)("reviveplayer: Reviving '" + target.NickName + "' 复活在默认位置 (未找到出生点)"));
				}
			}
			((MonoBehaviourPun)character).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, true });
			Debug.Log((object)("reviveplayer: 已复活并传送玩家 '" + target.NickName + "'"));
			Plugin.Log.LogInfo((object)("已复活并传送玩家: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("reviveplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void FreezePlayer(PhotonPlayer target, float intensity = 1f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"freezeplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("freezeplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			ApplyPlayerStatus(val, (CharacterAfflictions.STATUSTYPE)2, intensity);
			Debug.Log((object)$"freezeplayer: 已对玩家 '{target.NickName}' 施加寒冷 ({intensity:F2})");
			Plugin.Log.LogInfo((object)("已对玩家施加寒冷效果: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("freezeplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void StarvePlayer(PhotonPlayer target, float intensity = 1f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"starveplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("starveplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			ApplyPlayerStatus(val, (CharacterAfflictions.STATUSTYPE)1, intensity);
			Debug.Log((object)$"starveplayer: 已对玩家 '{target.NickName}' 施加饥饿 ({intensity:F2})");
			Plugin.Log.LogInfo((object)("已对玩家施加饥饿效果: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("starveplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PoisonPlayer(PhotonPlayer target, float intensity = 1f)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"poisonplayer: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("poisonplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			ApplyPlayerStatus(val, (CharacterAfflictions.STATUSTYPE)3, intensity);
			Debug.Log((object)$"poisonplayer: 已对玩家 '{target.NickName}' 施加中毒 ({intensity:F2})");
			Plugin.Log.LogInfo((object)("已对玩家施加中毒效果: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("poisonplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ClearPlayerEffects(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"clearplayereffects: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("clearplayereffects: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			float[] array = new float[9];
			((MonoBehaviourPun)val).photonView.RPC("RPC_ApplyStatusesFromFloatArray", (RpcTarget)0, new object[1] { array });
			Debug.Log((object)("clearplayereffects: 已清除玩家 '" + target.NickName + "'"));
			Plugin.Log.LogInfo((object)("已清除玩家的所有效果: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("clearplayereffects: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TeleportPlayerToPlayer(PhotonPlayer target, PhotonPlayer destination)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"teleportplayertoplayer: 未找到目标玩家!");
			return;
		}
		if (destination == null)
		{
			Debug.LogWarning((object)"teleportplayertoplayer: 未找到目标位置的玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			Character val2 = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)destination) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("teleportplayertoplayer: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			if ((UnityEngine.Object)(object)val2 == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("teleportplayertoplayer: 未找到玩家 '" + destination.NickName + "'"));
				return;
			}
			Vector3 val3 = val2.Center + Vector3.forward * 2f;
			((MonoBehaviourPun)val).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val3, true });
			Debug.Log((object)("teleportplayertoplayer: 已将 '" + target.NickName + "' 传送到 '" + destination.NickName + "'"));
			Plugin.Log.LogInfo((object)("已将玩家 '" + target.NickName + "' 传送到 '" + destination.NickName + "'"));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("teleportplayertoplayer: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetPlayerInvisible(PhotonPlayer target, bool invisible = true)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"setplayerinvisible: 未找到玩家!");
			return;
		}
		try
		{
			Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("setplayerinvisible: 未找到玩家 '" + target.NickName + "'"));
				return;
			}
			try
			{
				SkinnedMeshRenderer mainRenderer = val.refs.mainRenderer;
				if ((UnityEngine.Object)(object)mainRenderer != (UnityEngine.Object)null)
				{
					((Renderer)mainRenderer).enabled = !invisible;
					Debug.Log((object)$"setplayerinvisible: 已为玩家 '{target.NickName}' 设置隐身状态为 {invisible}");
					Plugin.Log.LogInfo((object)$"已为玩家设置隐身状态 {invisible}: {target.NickName}");
				}
				else
				{
					Debug.LogWarning((object)("setplayerinvisible: 未找到玩家 '" + target.NickName + "'"));
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning((object)("setplayerinvisible: 无法设置隐身状态 - " + ex.Message));
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("setplayerinvisible: 错误 - " + ex2.Message));
		}
	}

	private static void ApplyPlayerStatus(Character character, CharacterAfflictions.STATUSTYPE statusType, float intensity)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		float[] currentStatuses = character.refs.afflictions.currentStatuses;
		float[] array = new float[15];
		for (int i = 0; i < Math.Min(currentStatuses.Length, 15); i++)
		{
			array[i] = currentStatuses[i];
		}
		array[(int)statusType] = Mathf.Clamp01(intensity);
		((MonoBehaviourPun)character).photonView.RPC("RPC_ApplyStatusesFromFloatArray", (RpcTarget)0, new object[1] { array });
	}
}
