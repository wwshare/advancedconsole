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

public static class EnvironmentCommands
{
	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetTime(float time)
	{
		try
		{
			MonoBehaviour obj = UnityEngine.Object.FindFirstObjectByType<MonoBehaviour>();
			MonoBehaviour val = ((obj != null) ? ((Component)obj).GetComponent<MonoBehaviour>() : null);
			Debug.Log((object)$"settime: 正在尝试将时间设置为 {time}");
			Plugin.Log.LogInfo((object)$"已尝试将时间设置为: {time}");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("settime: 设置时间失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetGravity(float gravity = -9.81f)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		Physics.gravity = new Vector3(0f, gravity, 0f);
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter != (UnityEngine.Object)null)
			{
				((MonoBehaviourPun)localCharacter).photonView.RPC("SyncGravityRPC", (RpcTarget)1, new object[1] { gravity });
			}
		}
		catch (Exception)
		{
		}
		Debug.Log((object)$"setgravity: 重力已设置为 {gravity}");
		Plugin.Log.LogInfo((object)$"重力已设置为: {gravity}");
	}

	[ConsoleCommand]
	public static void GetGravity()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 gravity = Physics.gravity;
		float num = Mathf.Abs(gravity.y);
		Debug.Log((object)$"getgravity: 当前重力 Y: {gravity.y:F2} (大小: {num:F2})");
		Plugin.Log.LogInfo((object)$"当前重力: {gravity}");
	}

	[ConsoleCommand]
	public static void ToggleFog()
	{
		RenderSettings.fog = !RenderSettings.fog;
		Debug.Log((object)("togglefog: 雾现已" + (RenderSettings.fog ? "开启" : "关闭")));
		Plugin.Log.LogInfo((object)("雾已切换: " + (RenderSettings.fog ? "开启" : "关闭")));
	}

	[ConsoleCommand]
	public static void SetFogDensity(float density = 0.01f)
	{
		RenderSettings.fogDensity = density;
		Debug.Log((object)$"setfogdensity: 雾密度已设置为 {density}");
		Plugin.Log.LogInfo((object)$"雾密度已设置为: {density}");
	}

	[ConsoleCommand]
	public static void SetFogColor(float r = 0.5f, float g = 0.5f, float b = 0.5f)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		RenderSettings.fogColor = new Color(r, g, b, 1f);
		Debug.Log((object)$"setfogcolor: 雾颜色已设置为 R:{r} G:{g} B:{b}");
		Plugin.Log.LogInfo((object)$"雾颜色已设置为 R:{r} G:{g} B:{b}");
	}

	[ConsoleCommand]
	public static void GodModeEnvironment()
	{
		try
		{
			IEnumerable<MonoBehaviour> enumerable = from c in UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0)
				where ((object)c).GetType().Name.Contains("Damage") || ((object)c).GetType().Name.Contains("Harm") || ((object)c).GetType().Name.Contains("Kill")
				select c;
			int num = 0;
			foreach (MonoBehaviour item in enumerable)
			{
				((Behaviour)item).enabled = false;
				num++;
			}
			Debug.Log((object)$"godmodeenv: 已禁用 {num} 个伤害组件");
			Plugin.Log.LogInfo((object)$"已禁用 {num} 个环境伤害组件");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("godmodeenv: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ExplodeAt(PhotonPlayer target, float force = 1000f)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"explode: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("explode: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Vector3 center = val.Center;
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter != (UnityEngine.Object)null)
			{
				((MonoBehaviourPun)localCharacter).photonView.RPC("RPCA_Explosion", (RpcTarget)0, new object[4] { center.x, center.y, center.z, force });
			}
		}
		catch
		{
			CreateExplosionEffect(center, force);
		}
		Debug.Log((object)$"explode: 在 '{target.NickName}' 的位置生成了爆炸, 力度为 {force}");
		Plugin.Log.LogInfo((object)$"在玩家 {target.NickName} 的位置生成爆炸, 力度为 {force}");
	}

	private static void CreateExplosionEffect(Vector3 explosionPos, float force)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Rigidbody val = default(Rigidbody);
		foreach (Character allCharacter in Character.AllCharacters)
		{
			float num = Vector3.Distance(allCharacter.Center, explosionPos);
			if (num < 10f && ((Component)allCharacter).TryGetComponent<Rigidbody>(out val))
			{
				Vector3 val2 = allCharacter.Center - explosionPos;
				Vector3 normalized = val2.normalized;
				float num2 = force / (num + 1f);
				val.AddForce(normalized * num2, (ForceMode)1);
			}
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void Lightning()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter != (UnityEngine.Object)null)
			{
				Vector3 val = localCharacter.Center + Vector3.up * 50f;
				Debug.Log((object)"lightning: 已尝试创建闪电效果");
				Plugin.Log.LogInfo((object)"已尝试创建闪电效果");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("lightning: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ClearMobs()
	{
		try
		{
			IEnumerable<MonoBehaviour> enumerable = from c in UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0)
				where ((object)c).GetType().Name.Contains("Monster") || ((object)c).GetType().Name.Contains("Enemy") || ((object)c).GetType().Name.Contains("Mob") || ((object)c).GetType().Name.Contains("AI")
				select c;
			int num = 0;
			foreach (MonoBehaviour item in enumerable)
			{
				if ((UnityEngine.Object)(object)((Component)item).gameObject != (UnityEngine.Object)null)
				{
					PhotonNetwork.Destroy(((Component)item).gameObject);
					num++;
				}
			}
			Debug.Log((object)$"clearmobs: 已移除 {num} 个怪物");
			Plugin.Log.LogInfo((object)$"已从地图移除 {num} 个怪物");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("clearmobs: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SetGameSpeed(float speed = 1f)
	{
		Time.timeScale = speed;
		Debug.Log((object)$"setgamespeed: 游戏速度已设置为 {speed}x");
		Plugin.Log.LogInfo((object)$"游戏速度已设置为: {speed}x");
	}

	[ConsoleCommand]
	public static void PauseGame()
	{
		Time.timeScale = 0f;
		Debug.Log((object)"pausegame: 游戏已暂停");
		Plugin.Log.LogInfo((object)"游戏已暂停");
	}

	[ConsoleCommand]
	public static void ResumeGame()
	{
		Time.timeScale = 1f;
		Debug.Log((object)"resumegame: 游戏已恢复");
		Plugin.Log.LogInfo((object)"游戏已恢复");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void LightNearestCampfire()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"lightnearestcampfire: 未找到本地角色!");
			return;
		}
		try
		{
			IOrderedEnumerable<MonoBehaviour> source = (from c in UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0)
				where ((object)c).GetType().Name.Contains("Campfire") || ((object)c).GetType().Name.Contains("Fire") || ((object)c).GetType().Name.Contains("Bonfire")
				select c).Where(delegate(MonoBehaviour c)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				return Vector3.Distance(((Component)c).transform.position, localCharacter.Center) <= 10f;
			}).OrderBy(delegate(MonoBehaviour c)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				return Vector3.Distance(((Component)c).transform.position, localCharacter.Center);
			});
			MonoBehaviour val = source.FirstOrDefault();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"lightnearestcampfire: 10 米内未找到篝火");
				return;
			}
			float num = Vector3.Distance(((Component)val).transform.position, localCharacter.Center);
			MethodInfo methodInfo = ((object)val).GetType().GetMethod("Light") ?? ((object)val).GetType().GetMethod("LightFire") ?? ((object)val).GetType().GetMethod("StartFire");
			if (methodInfo != null)
			{
				methodInfo.Invoke(val, null);
				Debug.Log((object)$"lightnearestcampfire: 已点燃距离 {num:F1}m 的篝火");
				Plugin.Log.LogInfo((object)$"已点燃最近的篝火, 距离 {num:F1}m");
				return;
			}
			ParticleSystem val2 = default(ParticleSystem);
			if (((Component)val).TryGetComponent<ParticleSystem>(out val2))
			{
				val2.Play();
			}
			List<Transform> list = (from t in ((Component)val).GetComponentsInChildren<Transform>()
				where ((UnityEngine.Object)t).name.Contains("Fire") || ((UnityEngine.Object)t).name.Contains("Flame")
				select t).ToList();
			ParticleSystem val3 = default(ParticleSystem);
			foreach (Transform item in list)
			{
				((Component)item).gameObject.SetActive(true);
				if (((Component)item).TryGetComponent<ParticleSystem>(out val3))
				{
					val3.Play();
				}
			}
			Debug.Log((object)$"lightnearestcampfire: 已尝试点燃距离 {num:F1}m 的篝火");
			Plugin.Log.LogInfo((object)$"已尝试点燃最近的篝火, 距离 {num:F1}m");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("lightnearestcampfire: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ExtinguishNearbyFires(float radius = 20f)
	{
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"extinguishnearbyfires: 未找到本地角色!");
			return;
		}
		try
		{
			IEnumerable<MonoBehaviour> enumerable = (from c in UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0)
				where ((object)c).GetType().Name.Contains("Campfire") || ((object)c).GetType().Name.Contains("Fire") || ((object)c).GetType().Name.Contains("Bonfire")
				select c).Where(delegate(MonoBehaviour c)
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				return Vector3.Distance(((Component)c).transform.position, localCharacter.Center) <= radius;
			});
			int num = 0;
			ParticleSystem val = default(ParticleSystem);
			ParticleSystem val2 = default(ParticleSystem);
			foreach (MonoBehaviour item in enumerable)
			{
				try
				{
					MethodInfo methodInfo = ((object)item).GetType().GetMethod("Extinguish") ?? ((object)item).GetType().GetMethod("ExtinguishFire") ?? ((object)item).GetType().GetMethod("StopFire");
					if (methodInfo != null)
					{
						methodInfo.Invoke(item, null);
					}
					else
					{
						if (((Component)item).TryGetComponent<ParticleSystem>(out val))
						{
							val.Stop();
						}
						IEnumerable<Transform> enumerable2 = from t in ((Component)item).GetComponentsInChildren<Transform>()
							where ((UnityEngine.Object)t).name.Contains("Fire") || ((UnityEngine.Object)t).name.Contains("Flame")
							select t;
						foreach (Transform item2 in enumerable2)
						{
							((Component)item2).gameObject.SetActive(false);
							if (((Component)item2).TryGetComponent<ParticleSystem>(out val2))
							{
								val2.Stop();
							}
						}
					}
					num++;
				}
				catch
				{
				}
			}
			Debug.Log((object)$"extinguishnearbyfires: 已熄灭 {radius}m 范围内的 {num} 处火焰");
			Plugin.Log.LogInfo((object)$"已熄灭 {radius}m 范围内的 {num} 处火焰");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("extinguishnearbyfires: 失败 - " + ex.Message));
		}
	}
}
