using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class ItemSpawnCommands
{
	private static readonly Dictionary<string, string> CommonItems = new Dictionary<string, string>
	{
		{ "flashlight", "Flashlight" },
		{ "walkie", "WalkieTalkie" },
		{ "medkit", "Medkit" },
		{ "battery", "Battery" },
		{ "lantern", "Lantern" },
		{ "camera", "Camera" },
		{ "phone", "Phone" },
		{ "keys", "Keys" },
		{ "crowbar", "Crowbar" },
		{ "hammer", "Hammer" },
		{ "wrench", "Wrench" },
		{ "screwdriver", "Screwdriver" },
		{ "rope", "Rope" },
		{ "tape", "DuctTape" },
		{ "soda", "SodaCan" },
		{ "water", "WaterBottle" },
		{ "energy", "EnergyDrink" },
		{ "chips", "ChipsBag" },
		{ "apple", "Apple" },
		{ "banana", "Banana" }
	};

	private static List<GameObject> spawnedItems = new List<GameObject>();

	[ConsoleCommand]
	public static void ListItems()
	{
		Debug.Log((object)"=== 可用物品 ===");
		foreach (KeyValuePair<string, string> commonItem in CommonItems)
		{
			Debug.Log((object)("  " + commonItem.Key + " -> " + commonItem.Value));
		}
		Debug.Log((object)"用法: spawnitem <item_name> [count]");
		Debug.Log((object)"====================");
	}

	[ConsoleCommand]
	public static void SpawnItem(string itemName, int count = 1)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(itemName))
		{
			ListItems();
			return;
		}
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"spawnitem: 未找到本地角色!");
			return;
		}
		string key = itemName.ToLower();
		if (!CommonItems.TryGetValue(key, out string value))
		{
			Debug.LogWarning((object)("spawnitem: 未知物品 '" + itemName + "'"));
			ListItems();
			return;
		}
		count = Mathf.Clamp(count, 1, 50);
		try
		{
			Vector3 val = ((Component)localCharacter).transform.position + Vector3.forward * 2f;
			Vector3 val2 = default(Vector3);
			for (int i = 0; i < count; i++)
			{
				val2 = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(-1f, 1f));
				Vector3 val3 = val + val2;
				GameObject val4 = Resources.Load<GameObject>("Items/" + value);
				if ((UnityEngine.Object)(object)val4 == (UnityEngine.Object)null)
				{
					val4 = Resources.Load<GameObject>(value);
				}
				if ((UnityEngine.Object)(object)val4 != (UnityEngine.Object)null)
				{
					GameObject val5 = UnityEngine.Object.Instantiate<GameObject>(val4, val3, Quaternion.identity);
					spawnedItems.Add(val5);
					if ((UnityEngine.Object)(object)val5.GetComponent<PhotonView>() == (UnityEngine.Object)null)
					{
						val5.AddComponent<PhotonView>();
					}
					continue;
				}
				Debug.LogWarning((object)("spawnitem: 找不到物品 '" + value + "'"));
				return;
			}
			Debug.Log((object)$"spawnitem: 已生成 {count}x {value}");
			Plugin.Log.LogInfo((object)$"已生成 {count}x {value}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnitem: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void RemoveAllItems()
	{
		int num = 0;
		for (int num2 = spawnedItems.Count - 1; num2 >= 0; num2--)
		{
			if ((UnityEngine.Object)(object)spawnedItems[num2] != (UnityEngine.Object)null)
			{
				UnityEngine.Object.Destroy((UnityEngine.Object)(object)spawnedItems[num2]);
				num++;
			}
			spawnedItems.RemoveAt(num2);
		}
		Debug.Log((object)$"removeallitems: 已移除 {num} 个已生成物品");
		Plugin.Log.LogInfo((object)$"已移除 {num} 个已生成物品");
	}

	[ConsoleCommand]
	public static void RemoveNearbyItems(float radius = 5f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"removenearby: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		MonoBehaviour[] array = (from mb in UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0)
			where ((object)mb).GetType().Name.Contains("Item") || ((object)mb).GetType().Name.Contains("Pickup")
			select mb).ToArray();
		MonoBehaviour[] array2 = array;
		foreach (MonoBehaviour val in array2)
		{
			if (Vector3.Distance(((Component)val).transform.position, position) <= radius)
			{
				UnityEngine.Object.Destroy((UnityEngine.Object)(object)((Component)val).gameObject);
				num++;
			}
		}
		Debug.Log((object)$"removenearby: 已移除 {radius}m 范围内的 {num} 个物品");
		Plugin.Log.LogInfo((object)$"已移除 {num} 个附近物品");
	}

	[ConsoleCommand]
	public static void FreezeNearbyItems(float radius = 3f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"freezeitems: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		Rigidbody[] array = UnityEngine.Object.FindObjectsByType<Rigidbody>((FindObjectsSortMode)0);
		Rigidbody[] array2 = array;
		foreach (Rigidbody val in array2)
		{
			if (Vector3.Distance(((Component)val).transform.position, position) <= radius)
			{
				val.isKinematic = true;
				val.linearVelocity = Vector3.zero;
				val.angularVelocity = Vector3.zero;
				num++;
			}
		}
		Debug.Log((object)$"freezeitems: 已冻结 {radius}m 范围内的 {num} 个物品");
		Plugin.Log.LogInfo((object)$"已冻结 {num} 个物品");
	}

	[ConsoleCommand]
	public static void UnfreezeNearbyItems(float radius = 3f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"unfreezeitems: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		Rigidbody[] array = UnityEngine.Object.FindObjectsByType<Rigidbody>((FindObjectsSortMode)0);
		Rigidbody[] array2 = array;
		foreach (Rigidbody val in array2)
		{
			if (Vector3.Distance(((Component)val).transform.position, position) <= radius && val.isKinematic)
			{
				val.isKinematic = false;
				num++;
			}
		}
		Debug.Log((object)$"unfreezeitems: 已解冻 {radius}m 范围内的 {num} 个物品");
		Plugin.Log.LogInfo((object)$"已解冻 {num} 个物品");
	}

	[ConsoleCommand]
	public static void SpinNearbyItems(float radius = 3f, float force = 5f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"spinitems: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		Rigidbody[] array = UnityEngine.Object.FindObjectsByType<Rigidbody>((FindObjectsSortMode)0);
		Rigidbody[] array2 = array;
		Vector3 val2 = default(Vector3);
		foreach (Rigidbody val in array2)
		{
			if (Vector3.Distance(((Component)val).transform.position, position) <= radius)
			{
				val.isKinematic = false;
				val2 = new Vector3(UnityEngine.Random.Range(0f - force, force), UnityEngine.Random.Range(0f - force, force), UnityEngine.Random.Range(0f - force, force));
				val.AddTorque(val2, (ForceMode)1);
				num++;
			}
		}
		Debug.Log((object)$"spinitems: 已使 {radius}m 范围内的 {num} 个物品旋转");
		Plugin.Log.LogInfo((object)$"已使 {num} 个物品旋转");
	}

	[ConsoleCommand]
	public static void GravityPull(float radius = 10f, float force = 5f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"gravitypull: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		Rigidbody[] array = UnityEngine.Object.FindObjectsByType<Rigidbody>((FindObjectsSortMode)0);
		Rigidbody[] array2 = array;
		foreach (Rigidbody val in array2)
		{
			float num2 = Vector3.Distance(((Component)val).transform.position, position);
			if (num2 <= radius && num2 > 1f)
			{
				val.isKinematic = false;
				Vector3 val2 = position - ((Component)val).transform.position;
				Vector3 normalized = val2.normalized;
				val.AddForce(normalized * force, (ForceMode)1);
				num++;
			}
		}
		Debug.Log((object)$"gravitypull: 已吸附 {radius}m 范围内的 {num} 个物品");
		Plugin.Log.LogInfo((object)$"已用重力吸附 {num} 个物品");
	}

	[ConsoleCommand]
	public static void GravityPush(float radius = 5f, float force = 10f)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"gravitypush: 未找到本地角色!");
			return;
		}
		int num = 0;
		Vector3 position = ((Component)localCharacter).transform.position;
		Rigidbody[] array = UnityEngine.Object.FindObjectsByType<Rigidbody>((FindObjectsSortMode)0);
		Rigidbody[] array2 = array;
		foreach (Rigidbody val in array2)
		{
			float num2 = Vector3.Distance(((Component)val).transform.position, position);
			if (num2 <= radius)
			{
				val.isKinematic = false;
				Vector3 val2 = ((Component)val).transform.position - position;
				Vector3 normalized = val2.normalized;
				val.AddForce(normalized * force, (ForceMode)1);
				num++;
			}
		}
		Debug.Log((object)$"gravitypush: 已推开 {radius}m 范围内的 {num} 个物品");
		Plugin.Log.LogInfo((object)$"已推开 {num} 个物品");
	}
}
