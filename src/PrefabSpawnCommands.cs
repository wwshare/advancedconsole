using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using Zorro.Core;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class PrefabSpawnCommands
{
	private static readonly string[] SceneProps = new string[10]
	{
		"Campfire", "AirportGateKiosk", "Kiosk", "BingBong", "Lantern", "Torch",
		"Candle", "Passport", "Snowball", "Airplane Food"
	};

	[ConsoleCommand]
	public static void ListPrefabs()
	{
		try
		{
			Debug.Log((object)"=== 可生成预制件 ===");
			List<string> list = GetAllPrefabNames();
			foreach (string item in list)
			{
				string normalized = NormalizeName(item);
				if (normalized != item.ToLowerInvariant())
				{
					Debug.Log((object)$"  {item}  (输入: {normalized})");
				}
				else
				{
					Debug.Log((object)$"  {item}");
				}
			}
			Debug.Log((object)$"共 {list.Count} 个可用预制件");
			Debug.Log((object)"使用 PrefabSpawnCommands.SpawnPrefab <名称> [数量] 生成");
			Debug.Log((object)"带空格的名称可用别名(去掉空格/符号)输入, 如 anti-ropespool");
			Debug.Log((object)"=========================");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("listprefabs: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnPrefab(string prefabName, int count = 1)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			Debug.LogWarning((object)"spawnprefab: 请指定预制件名称");
			ListPrefabs();
			return;
		}
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"spawnprefab: 未找到本地角色!");
				return;
			}
			string resolvedName = ResolvePrefabName(prefabName);
			if (!string.Equals(resolvedName, prefabName, StringComparison.OrdinalIgnoreCase))
			{
				Debug.Log((object)$"spawnprefab: '{prefabName}' 解析为 '{resolvedName}'");
			}
			count = Mathf.Clamp(count, 1, 30);
			Vector3 val = GetCrosshairPosition();
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				Vector3 val2 = val + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(-1f, 1f));
				if (TrySpawnPrefab(resolvedName, val2))
				{
					num++;
				}
			}
			Debug.Log((object)$"spawnprefab: 已生成 {num}/{count} 个 '{resolvedName}'");
			Plugin.Log.LogInfo((object)$"已生成预制件 {resolvedName} x{num}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnprefab: 错误 - " + ex.Message));
		}
	}

	public static List<string> GetAllPrefabNames()
	{
		HashSet<string> hashSet = new HashSet<string>();
		try
		{
			GameObject[] array = Resources.LoadAll<GameObject>("0_Items");
			GameObject[] array2 = array;
			foreach (GameObject val in array2)
			{
				if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
				{
					hashSet.Add(((UnityEngine.Object)val).name);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("枚举 0_Items 资源失败: " + ex.Message));
		}
		try
		{
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup != null)
			{
				foreach (KeyValuePair<ushort, Item> item in instance.itemLookup)
				{
					hashSet.Add(((UnityEngine.Object)item.Value).name);
				}
			}
		}
		catch (Exception ex2)
		{
			Plugin.Log.LogDebug((object)("枚举物品数据库失败: " + ex2.Message));
		}
		string[] sceneProps = SceneProps;
		foreach (string text in sceneProps)
		{
			hashSet.Add(text);
		}
		return hashSet.OrderBy((string x) => x).ToList();
	}

	private static string NormalizeName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return "";
		}
		return new string((from c in name
			where char.IsLetterOrDigit(c)
			select char.ToLowerInvariant(c)).ToArray());
	}

	private static string ResolvePrefabName(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		List<string> all = GetAllPrefabNames();
		foreach (string name in all)
		{
			if (string.Equals(name, input, StringComparison.OrdinalIgnoreCase))
			{
				return name;
			}
		}
		string normalizedInput = NormalizeName(input);
		if (!string.IsNullOrEmpty(normalizedInput))
		{
			foreach (string name2 in all)
			{
				if (NormalizeName(name2) == normalizedInput)
				{
					return name2;
				}
			}
			foreach (string name3 in all)
			{
				if (NormalizeName(name3).Contains(normalizedInput))
				{
					return name3;
				}
			}
		}
		foreach (string name4 in all)
		{
			if (name4.Contains(input, StringComparison.OrdinalIgnoreCase))
			{
				return name4;
			}
		}
		return input;
	}

	private static bool TrySpawnPrefab(string prefabName, Vector3 position)
	{
		if (PhotonNetwork.IsConnected)
		{
			try
			{
				GameObject val = PhotonNetwork.Instantiate("0_Items/" + prefabName, position, Quaternion.identity);
				if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				Plugin.Log.LogDebug((object)$"Photon 生成 0_Items/{prefabName} 失败: {ex.Message}");
			}
		}
		GameObject val2 = LoadPrefabAsset(prefabName);
		if ((UnityEngine.Object)(object)val2 != (UnityEngine.Object)null)
		{
			GameObject val3 = UnityEngine.Object.Instantiate<GameObject>(val2, position, Quaternion.identity);
			if (PhotonNetwork.IsConnected && (UnityEngine.Object)(object)val3.GetComponent<PhotonView>() == (UnityEngine.Object)null)
			{
				val3.AddComponent<PhotonView>();
			}
			return true;
		}
		GameObject val4 = FindSceneObjectByName(prefabName);
		if ((UnityEngine.Object)(object)val4 != (UnityEngine.Object)null)
		{
			GameObject val5 = UnityEngine.Object.Instantiate<GameObject>(val4, position, Quaternion.identity);
			if (PhotonNetwork.IsConnected && (UnityEngine.Object)(object)val5.GetComponent<PhotonView>() == (UnityEngine.Object)null)
			{
				val5.AddComponent<PhotonView>();
			}
			return true;
		}
		Debug.LogWarning((object)("spawnprefab: 未找到预制件 '" + prefabName + "'"));
		return false;
	}

	private static GameObject LoadPrefabAsset(string prefabName)
	{
		string[] array = new string[4] { "0_Items/" + prefabName, "Items/" + prefabName, "0_Items/" + prefabName + "_Prop", prefabName };
		foreach (string text in array)
		{
			GameObject val = Resources.Load<GameObject>(text);
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				return val;
			}
		}
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup != null)
		{
			foreach (KeyValuePair<ushort, Item> item in instance.itemLookup)
			{
				if (string.Equals(((UnityEngine.Object)item.Value).name, prefabName, StringComparison.OrdinalIgnoreCase))
				{
					return ((Component)item.Value).gameObject;
				}
			}
		}
		return null;
	}

	private static Vector3 GetCrosshairPosition()
	{
		if ((UnityEngine.Object)(object)MainCamera.instance != (UnityEngine.Object)null)
		{
			Transform transform = ((Component)MainCamera.instance).transform;
			float maxRaycastDistance = PluginConfig.MaxRaycastDistance;
			RaycastHit val = default(RaycastHit);
			if (Physics.Raycast(transform.position, transform.forward, out val, maxRaycastDistance))
			{
				return val.point + val.normal * 0.1f;
			}
			return transform.position + transform.forward * PluginConfig.FallbackDistance;
		}
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter != (UnityEngine.Object)null)
		{
			return ((Component)localCharacter).transform.position + ((Component)localCharacter).transform.forward * 2f;
		}
		return Vector3.zero;
	}

	private static GameObject FindSceneObjectByName(string prefabName)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsByType<GameObject>((FindObjectsInactive)1, (FindObjectsSortMode)0);
		GameObject[] array2 = array;
		foreach (GameObject val in array2)
		{
			if (string.Equals(((UnityEngine.Object)val).name, prefabName, StringComparison.OrdinalIgnoreCase) && val.activeInHierarchy)
			{
				return val;
			}
		}
		foreach (GameObject val2 in array2)
		{
			if (val2.activeInHierarchy && ((UnityEngine.Object)val2).name.Contains(prefabName, StringComparison.OrdinalIgnoreCase))
			{
				return val2;
			}
		}
		foreach (GameObject val3 in array2)
		{
			if (string.Equals(((UnityEngine.Object)val3).name, prefabName, StringComparison.OrdinalIgnoreCase))
			{
				return val3;
			}
		}
		foreach (GameObject val4 in array2)
		{
			if (((UnityEngine.Object)val4).name.Contains(prefabName, StringComparison.OrdinalIgnoreCase))
			{
				return val4;
			}
		}
		return null;
	}
}
