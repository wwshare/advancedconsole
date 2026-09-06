using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class SpawnCommands
{
	[ConsoleCommand]
	public static void ListItems()
	{
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup == null)
		{
			Debug.LogWarning((object)"listitems: ItemDatabase not found!");
			return;
		}
		Debug.Log((object)$"listitems: Available items ({instance.itemLookup.Count}):");
		foreach (KeyValuePair<ushort, Item> item in instance.itemLookup.Take(20))
		{
			Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
		}
		if (instance.itemLookup.Count > 20)
		{
			Debug.Log((object)$"  ... and {instance.itemLookup.Count - 20} more items");
		}
		Plugin.Log.LogInfo((object)$"Listed {instance.itemLookup.Count} available items");
	}

	[ConsoleCommand]
	public static void FindItem(string itemName)
	{
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup == null)
		{
			Debug.LogWarning((object)"finditem: ItemDatabase not found!");
			return;
		}
		IEnumerable<KeyValuePair<ushort, Item>> enumerable = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Contains(itemName, StringComparison.OrdinalIgnoreCase)).Take(10);
		if (enumerable.Any())
		{
			Debug.Log((object)("finditem: Found items matching '" + itemName + "':"));
			{
				foreach (KeyValuePair<ushort, Item> item in enumerable)
				{
					Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
				}
				return;
			}
		}
		Debug.LogWarning((object)("finditem: no items found matching '" + itemName + "'"));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnItem(PhotonPlayer target, ConsoleItem item)
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		try
		{
			if (target == null)
			{
				Debug.LogWarning((object)"spawnitem: player not found!");
				return;
			}
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null || !instance.itemLookup.ContainsKey(item.ID))
			{
				Debug.LogWarning((object)$"spawnitem: item '{item.Name}' (ID:{item.ID}) not found in database!");
				return;
			}
			Player player = PlayerHandler.GetPlayer(target);
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("spawnitem: no Player component for '" + target.NickName + "'"));
				return;
			}
			ItemInstanceData val = new ItemInstanceData(Guid.NewGuid());
			ItemInstanceDataHandler.AddInstanceData(val);
			ItemSlot val2 = default(ItemSlot);
			if (player.AddItem(item.ID, val, out val2))
			{
				Debug.Log((object)string.Format("spawnitem: gave '{0}' (ID:{1}) to '{2}' in slot {3}", new object[4] { item.Name, item.ID, target.NickName, val2.itemSlotID }));
				Plugin.Log.LogInfo((object)("Spawned item '" + item.Name + "' for player '" + target.NickName + "'"));
			}
			else
			{
				Debug.LogWarning((object)("spawnitem: failed to give '" + item.Name + "' to '" + target.NickName + "' - inventory full or invalid item"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnitem: failed - " + ex.Message));
			Plugin.Log.LogError((object)("SpawnItem failed: " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GiveItemByName(PhotonPlayer target, string itemNameOrId)
	{
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		if (target == null)
		{
			Debug.LogWarning((object)"giveitembyname: player not found!");
			return;
		}
		ushort result = 0;
		Item val = null;
		if (ushort.TryParse(itemNameOrId, out result))
		{
			if (!ItemDatabase.TryGetItem(result, out val))
			{
				Debug.LogWarning((object)$"giveitembyname: item with ID {result} not found!");
				return;
			}
		}
		else
		{
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"giveitembyname: ItemDatabase not found!");
				return;
			}
			List<KeyValuePair<ushort, Item>> list = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Equals(itemNameOrId, StringComparison.OrdinalIgnoreCase)).ToList();
			if (!list.Any())
			{
				list = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Contains(itemNameOrId, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			if (!list.Any())
			{
				Debug.LogWarning((object)("giveitembyname: no items found matching '" + itemNameOrId + "'"));
				return;
			}
			if (list.Count > 1)
			{
				Debug.LogWarning((object)("giveitembyname: multiple items found matching '" + itemNameOrId + "':"));
				foreach (KeyValuePair<ushort, Item> item in list.Take(5))
				{
					Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
				}
				Debug.LogWarning((object)"Please be more specific or use item ID");
				return;
			}
			result = list.First().Key;
			val = list.First().Value;
		}
		try
		{
			Player player = PlayerHandler.GetPlayer(target);
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("giveitembyname: no Player component for '" + target.NickName + "'"));
				return;
			}
			ItemInstanceData val2 = new ItemInstanceData(Guid.NewGuid());
			ItemInstanceDataHandler.AddInstanceData(val2);
			ItemSlot val3 = default(ItemSlot);
			if (player.AddItem(result, val2, out val3))
			{
				Debug.Log((object)string.Format("giveitembyname: gave '{0}' (ID:{1}) to '{2}' in slot {3}", new object[4]
				{
					(val != null) ? ((UnityEngine.Object)val).name : null,
					result,
					target.NickName,
					val3.itemSlotID
				}));
				Plugin.Log.LogInfo((object)$"Gave item '{((val != null) ? ((UnityEngine.Object)val).name : null)}' (ID:{result}) to player: {target.NickName}");
			}
			else
			{
				Debug.LogWarning((object)$"giveitembyname: failed to give '{((val != null) ? ((UnityEngine.Object)val).name : null)}' (ID:{result}) to '{target.NickName}' - inventory full or invalid item");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("giveitembyname: failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnItemByName(string itemNameOrId)
	{
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		ushort result = 0;
		Item val = null;
		if (ushort.TryParse(itemNameOrId, out result))
		{
			if (!ItemDatabase.TryGetItem(result, out val))
			{
				Debug.LogWarning((object)$"spawnitembyname: item with ID {result} not found!");
				return;
			}
		}
		else
		{
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"spawnitembyname: ItemDatabase not found!");
				return;
			}
			List<KeyValuePair<ushort, Item>> list = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Equals(itemNameOrId, StringComparison.OrdinalIgnoreCase)).ToList();
			if (!list.Any())
			{
				list = instance.itemLookup.Where((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Contains(itemNameOrId, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			if (!list.Any())
			{
				Debug.LogWarning((object)("spawnitembyname: no items found matching '" + itemNameOrId + "'"));
				return;
			}
			if (list.Count > 1)
			{
				Debug.LogWarning((object)("spawnitembyname: multiple items found matching '" + itemNameOrId + "':"));
				foreach (KeyValuePair<ushort, Item> item in list.Take(5))
				{
					Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
				}
				Debug.LogWarning((object)"Please be more specific or use item ID");
				return;
			}
			result = list.First().Key;
			val = list.First().Value;
		}
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"spawnitembyname: no local character found!");
				return;
			}
			Vector3 val2 = localCharacter.Center + localCharacter.data.lookDirection_Flat * 2f;
			ItemDatabase.Add(val, val2);
			Debug.Log((object)$"spawnitembyname: spawned '{((val != null) ? ((UnityEngine.Object)val).name : null)}' (ID:{result}) at position {val2}");
			Plugin.Log.LogInfo((object)$"Spawned item '{((val != null) ? ((UnityEngine.Object)val).name : null)}' (ID:{result})");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("spawnitembyname: failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TickEveryone()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		foreach (Character allCharacter in Character.AllCharacters)
		{
			if (!allCharacter.data.dead)
			{
				try
				{
					GameObject val = PhotonNetwork.Instantiate("BugfixOnYou", Vector3.zero, Quaternion.identity, (byte)0, (object[])null);
					val.GetComponent<PhotonView>().RPC("AttachBug", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)allCharacter).photonView.ViewID });
					num++;
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)("tickeveryone: failed to spawn tick on " + allCharacter.characterName + " - " + ex.Message));
				}
			}
		}
		Debug.Log((object)$"tickeveryone: spawned {num} ticks");
		Plugin.Log.LogInfo((object)$"Spawned {num} ticks on players");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TickPlayer(PhotonPlayer target)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"tick: player not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("tick: no Character for '" + target.NickName + "'"));
			return;
		}
		GameObject val2 = PhotonNetwork.Instantiate("BugfixOnYou", Vector3.zero, Quaternion.identity, (byte)0, (object[])null);
		val2.GetComponent<PhotonView>().RPC("AttachBug", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)val).photonView.ViewID });
		Debug.Log((object)("tick: spawned on '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("Spawned tick on player: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GiveItem(PhotonPlayer target, ushort itemID = 1)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		if (target == null)
		{
			Debug.LogWarning((object)"giveitem: player not found!");
			return;
		}
		try
		{
			Item val = default(Item);
			if (!ItemDatabase.TryGetItem(itemID, out val))
			{
				Debug.LogWarning((object)$"giveitem: item with ID {itemID} not found in database!");
				return;
			}
			Player player = PlayerHandler.GetPlayer(target);
			if ((UnityEngine.Object)(object)player == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)("giveitem: no Player component for '" + target.NickName + "'"));
				return;
			}
			ItemInstanceData val2 = new ItemInstanceData(Guid.NewGuid());
			ItemInstanceDataHandler.AddInstanceData(val2);
			ItemSlot val3 = default(ItemSlot);
			if (player.AddItem(itemID, val2, out val3))
			{
				Debug.Log((object)string.Format("giveitem: gave '{0}' (ID:{1}) to '{2}' in slot {3}", new object[4]
				{
					((UnityEngine.Object)val).name,
					itemID,
					target.NickName,
					val3.itemSlotID
				}));
				Plugin.Log.LogInfo((object)$"Gave item '{((UnityEngine.Object)val).name}' (ID:{itemID}) to player: {target.NickName}");
				PhotonView photonView = ((MonoBehaviourPun)player).photonView;
				if (photonView != null)
				{
					photonView.RPC("SyncInventory", (RpcTarget)1, Array.Empty<object>());
				}
			}
			else
			{
				Debug.LogWarning((object)$"giveitem: failed to give '{((UnityEngine.Object)val).name}' (ID:{itemID}) to '{target.NickName}' - inventory full or invalid item");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("giveitem: failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ClearItems()
	{
		try
		{
			int num = 0;
			IEnumerable<PhotonView> enumerable = from pv in UnityEngine.Object.FindObjectsByType<PhotonView>((FindObjectsSortMode)0)
				where ((UnityEngine.Object)((Component)pv).gameObject).name.Contains("(Clone)")
				select pv;
			foreach (PhotonView item in enumerable)
			{
				try
				{
					PhotonNetwork.Destroy(((Component)item).gameObject);
					num++;
				}
				catch
				{
				}
			}
			string[] array = new string[5] { "SpawnedMushroom", "SpawnedRope", "SpawnedLuggage", "SpawnedTree", "SpawnedRock" };
			string[] array2 = array;
			foreach (string objName in array2)
			{
				IEnumerable<GameObject> enumerable2 = from go in GameObject.FindGameObjectsWithTag("Untagged")
					where ((UnityEngine.Object)go).name.Contains(objName)
					select go;
				foreach (GameObject item2 in enumerable2)
				{
					try
					{
						UnityEngine.Object.Destroy((UnityEngine.Object)(object)item2);
						num++;
					}
					catch
					{
					}
				}
			}
			Debug.Log((object)$"clearitems: removed {num} items and objects");
			Plugin.Log.LogInfo((object)$"Removed {num} spawned items and objects");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("clearitems: failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SearchItems(string searchTerm)
	{
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup == null)
		{
			Debug.LogWarning((object)"searchitems: ItemDatabase not found!");
			return;
		}
		IEnumerable<KeyValuePair<ushort, Item>> enumerable = (from kvp in instance.itemLookup
			where ((UnityEngine.Object)kvp.Value).name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
			orderby ((UnityEngine.Object)kvp.Value).name
			select kvp).Take(15);
		if (enumerable.Any())
		{
			Debug.Log((object)$"searchitems: Found {enumerable.Count()} items matching '{searchTerm}':");
			foreach (KeyValuePair<ushort, Item> item in enumerable)
			{
				Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
			}
			Debug.Log((object)("Hint: Use 'SpawnCommands.SpawnItemByName " + ((UnityEngine.Object)enumerable.First().Value).name + "' to spawn"));
		}
		else
		{
			Debug.LogWarning((object)("searchitems: no items found matching '" + searchTerm + "'"));
			Debug.Log((object)"Hint: Try using partial names like 'flash', 'rope', 'food'");
		}
	}

	[ConsoleCommand]
	public static void ShowPopularItems()
	{
		Debug.Log((object)"showpopularitems: Popular/Useful items to spawn:");
		Debug.Log((object)"  Survival items:");
		Debug.Log((object)"    Flashlight, Lantern, Torch");
		Debug.Log((object)"    Rope, Grappler, ClimbingGloves");
		Debug.Log((object)"    Flare, SmokeGrenade, Whistle");
		Debug.Log((object)"  Food & Medical:");
		Debug.Log((object)"    Apple, Banana, Water, Coffee");
		Debug.Log((object)"    Bandage, Medicine, Antidote");
		Debug.Log((object)"  Tools & Equipment:");
		Debug.Log((object)"    Backpack, Map, Compass, Binoculars");
		Debug.Log((object)"    Knife, Axe, Pickaxe, Shovel");
		Debug.Log((object)"Usage: SpawnCommands.SpawnItemByName <item_name>");
		Plugin.Log.LogInfo((object)"Displayed popular items list");
	}

	[ConsoleCommand]
	public static void ShowItemsByCategory(string category)
	{
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup == null)
		{
			Debug.LogWarning((object)"showitemsbycategory: ItemDatabase not found!");
			return;
		}
		string[] array;
		switch (category.ToLower())
		{
		case "food":
		case "еда":
			array = new string[9] { "apple", "banana", "bread", "meat", "fish", "berry", "water", "coffee", "tea" };
			break;
		case "tools":
		case "инструменты":
			array = new string[6] { "knife", "axe", "pickaxe", "shovel", "hammer", "wrench" };
			break;
		case "medical":
		case "медицина":
			array = new string[5] { "bandage", "medicine", "antidote", "syringe", "pill" };
			break;
		case "survival":
		case "выживание":
			array = new string[7] { "flashlight", "lantern", "torch", "rope", "flare", "whistle", "compass" };
			break;
		case "weapons":
		case "оружие":
			array = new string[7] { "gun", "rifle", "pistol", "bow", "arrow", "knife", "sword" };
			break;
		case "clothes":
		case "одежда":
			array = new string[7] { "jacket", "pants", "shirt", "boots", "gloves", "hat", "helmet" };
			break;
		default:
			array = new string[1] { category.ToLower() };
			break;
		}
		string[] categoryKeywords = array;
		IEnumerable<KeyValuePair<ushort, Item>> enumerable = (from kvp in instance.itemLookup
			where categoryKeywords.Any((string keyword) => ((UnityEngine.Object)kvp.Value).name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
			orderby ((UnityEngine.Object)kvp.Value).name
			select kvp).Take(20);
		if (enumerable.Any())
		{
			Debug.Log((object)$"showitemsbycategory: Found {enumerable.Count()} items in category '{category}':");
			{
				foreach (KeyValuePair<ushort, Item> item in enumerable)
				{
					Debug.Log((object)$"  ID:{item.Key} - {((UnityEngine.Object)item.Value).name}");
				}
				return;
			}
		}
		Debug.LogWarning((object)("showitemsbycategory: no items found for category '" + category + "'"));
		Debug.Log((object)"Available categories: food, tools, medical, survival, weapons, clothes");
	}

	[ConsoleCommand]
	public static void SpawnRandomItem()
	{
		ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
		if (instance?.itemLookup == null)
		{
			Debug.LogWarning((object)"spawnrandomitem: ItemDatabase not found!");
			return;
		}
		KeyValuePair<ushort, Item> keyValuePair = instance.itemLookup.ElementAt(UnityEngine.Random.Range(0, instance.itemLookup.Count));
		SpawnItemByName(((UnityEngine.Object)keyValuePair.Value).name);
		Debug.Log((object)$"spawnrandomitem: spawned random item '{((UnityEngine.Object)keyValuePair.Value).name}' (ID:{keyValuePair.Key})");
	}

	[ConsoleCommand]
	public static void RemoveMyItems()
	{
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"removemyitems: local character not found!");
				return;
			}
			IEnumerable<Item> source = from item in UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0)
				where (UnityEngine.Object)(object)item.holderCharacter == (UnityEngine.Object)(object)localCharacter
				select item;
			int num = 0;
			foreach (Item item in source.ToList())
			{
				if ((UnityEngine.Object)(object)item != (UnityEngine.Object)null && (UnityEngine.Object)(object)((MonoBehaviourPun)item).photonView != (UnityEngine.Object)null)
				{
					PhotonNetwork.Destroy(((MonoBehaviourPun)item).photonView);
					num++;
				}
			}
			Debug.Log((object)$"removemyitems: removed {num} items");
			Plugin.Log.LogInfo((object)$"Removed {num} player items");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("removemyitems: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void FreezeAllItems()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
			int num = 0;
			Item[] array2 = array;
			Rigidbody val2 = default(Rigidbody);
			foreach (Item val in array2)
			{
				if (((Component)val).TryGetComponent<Rigidbody>(out val2))
				{
					val2.isKinematic = true;
					val2.linearVelocity = Vector3.zero;
					val2.angularVelocity = Vector3.zero;
					num++;
				}
			}
			Debug.Log((object)$"freezeallitems: froze {num} items");
			Plugin.Log.LogInfo((object)$"Froze {num} items");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("freezeallitems: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void UnfreezeAllItems()
	{
		try
		{
			Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
			int num = 0;
			Item[] array2 = array;
			Rigidbody val2 = default(Rigidbody);
			foreach (Item val in array2)
			{
				if (((Component)val).TryGetComponent<Rigidbody>(out val2))
				{
					val2.isKinematic = false;
					num++;
				}
			}
			Debug.Log((object)$"unfreezeallitems: unfroze {num} items");
			Plugin.Log.LogInfo((object)$"Unfroze {num} items");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("unfreezeallitems: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpinAllItems()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Item[] array = UnityEngine.Object.FindObjectsByType<Item>((FindObjectsSortMode)0);
			int num = 0;
			Item[] array2 = array;
			Rigidbody val2 = default(Rigidbody);
			Vector3 val3 = default(Vector3);
			foreach (Item val in array2)
			{
				if (((Component)val).TryGetComponent<Rigidbody>(out val2))
				{
					val2.isKinematic = false;
					val3 = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
					val2.AddTorque(val3, (ForceMode)1);
					num++;
				}
			}
			Debug.Log((object)$"spinallitems: spinning {num} items");
			Plugin.Log.LogInfo((object)$"Set {num} items spinning");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spinallitems: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void FlagGun()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"flaggun: local character not found!");
				return;
			}
			Camera main = Camera.main;
			if ((UnityEngine.Object)(object)main == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"flaggun: main camera not found!");
				return;
			}
			Ray val = default(Ray);
			val = new Ray(((Component)main).transform.position, ((Component)main).transform.forward);
			RaycastHit val2 = default(RaycastHit);
			if (Physics.Raycast(val, out val2, 100f))
			{
				Vector3 val3 = val2.point + Vector3.up * 0.1f;
				GameObject val4 = GameObject.CreatePrimitive((PrimitiveType)3);
				((UnityEngine.Object)val4).name = "SpawnedFlag";
				val4.transform.position = val3 + Vector3.up * 2f;
				val4.transform.localScale = new Vector3(2f, 0.1f, 1f);
				val4.GetComponent<Renderer>().material.color = Color.red;
				GameObject val5 = GameObject.CreatePrimitive((PrimitiveType)2);
				((UnityEngine.Object)val5).name = "SpawnedFlagPole";
				val5.transform.position = val3 + Vector3.up * 1f;
				val5.transform.localScale = new Vector3(0.1f, 2f, 0.1f);
				val5.GetComponent<Renderer>().material.color = new Color(0.6f, 0.3f, 0.1f);
				Debug.Log((object)$"flaggun: placed flag at {val3}");
				Plugin.Log.LogInfo((object)$"Flag placed at {val3}");
			}
			else
			{
				Debug.LogWarning((object)"flaggun: no surface found to place flag!");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("flaggun: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void RemoveAllFlags()
	{
		try
		{
			IEnumerable<GameObject> source = from go in GameObject.FindGameObjectsWithTag("Untagged")
				where ((UnityEngine.Object)go).name.Contains("SpawnedFlag")
				select go;
			int num = 0;
			foreach (GameObject item in source.ToList())
			{
				UnityEngine.Object.Destroy((UnityEngine.Object)(object)item);
				num++;
			}
			Debug.Log((object)$"removeallflags: removed {num} flags");
			Plugin.Log.LogInfo((object)$"Removed {num} flags");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("removeallflags: Error - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void DrawWithSelectedItem()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"drawwithitem: local character not found!");
				return;
			}
			Item currentItem = localCharacter.data.currentItem;
			if ((UnityEngine.Object)(object)currentItem == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"drawwithitem: no item in hand!");
				return;
			}
			GameObject val = UnityEngine.Object.Instantiate<GameObject>(((Component)currentItem).gameObject);
			((UnityEngine.Object)val).name = "DrawnItem_" + ((UnityEngine.Object)currentItem).name;
			val.transform.position = ((Component)currentItem).transform.position;
			val.transform.rotation = ((Component)currentItem).transform.rotation;
			Rigidbody val2 = default(Rigidbody);
			if (val.TryGetComponent<Rigidbody>(out val2))
			{
				val2.isKinematic = true;
			}
			Debug.Log((object)("drawwithitem: drew with " + ((UnityEngine.Object)currentItem).name));
			Plugin.Log.LogInfo((object)("Drew with item: " + ((UnityEngine.Object)currentItem).name));
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("drawwithitem: Error - " + ex.Message));
		}
	}

	private static Vector3 GetSpawnPosition()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if ((UnityEngine.Object)(object)MainCamera.instance == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)"GetSpawnPosition: MainCamera not found, using default position");
			return Vector3.zero;
		}
		Transform transform = ((Component)MainCamera.instance).transform;
		float maxRaycastDistance = PluginConfig.MaxRaycastDistance;
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(transform.position, transform.forward, out val, maxRaycastDistance))
		{
			return val.point + val.normal * 0.1f;
		}
		float fallbackDistance = PluginConfig.FallbackDistance;
		return transform.position + transform.forward * fallbackDistance;
	}

	private static GameObject? SafeSpawn(string prefabName, Vector3 position, Quaternion rotation)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			return PhotonNetwork.Instantiate(prefabName, position, rotation, (byte)0, (object[])null);
		}
		catch
		{
			try
			{
				return PhotonNetwork.Instantiate("0_Items/" + prefabName, position, rotation, (byte)0, (object[])null);
			}
			catch
			{
				try
				{
					string text = prefabName.Replace("0_Items/", "");
					return PhotonNetwork.Instantiate(text, position, rotation, (byte)0, (object[])null);
				}
				catch
				{
					Debug.LogError((object)("SafeSpawn: Failed to spawn '" + prefabName + "' with all path variants"));
					return null;
				}
			}
		}
	}

	private static GameObject? SpawnFromPool(SpawnPool pool, Vector3 position, Quaternion rotation)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			GameObject randomItem = LootData.GetRandomItem(pool);
			if ((UnityEngine.Object)(object)randomItem == (UnityEngine.Object)null)
			{
				Debug.LogError((object)$"SpawnFromPool: No items found in pool '{pool}'");
				return null;
			}
			GameObject result = PhotonNetwork.InstantiateItemRoom(((UnityEngine.Object)randomItem).name, position, rotation);
			Debug.Log((object)$"SpawnFromPool: Successfully spawned '{((UnityEngine.Object)randomItem).name}' from pool '{pool}' at {position}");
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)$"SpawnFromPool: Failed to spawn from pool '{pool}': {ex.Message}");
			return null;
		}
	}

	private static Quaternion GetSpawnRotation()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if ((UnityEngine.Object)(object)MainCamera.instance == (UnityEngine.Object)null)
		{
			return Quaternion.identity;
		}
		Transform transform = ((Component)MainCamera.instance).transform;
		Vector3 forward = transform.forward;
		forward.y = 0f;
		if (forward != Vector3.zero)
		{
			return Quaternion.LookRotation(forward);
		}
		return Quaternion.identity;
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnSmallLuggage()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		Quaternion spawnRotation = GetSpawnRotation();
		try
		{
			GameObject val = PhotonNetwork.Instantiate("0_Items/LuggageSmall", spawnPosition, spawnRotation, (byte)0, (object[])null);
			Debug.Log((object)$"spawnsmallluggage: Successfully spawned LuggageSmall at {spawnPosition}");
			Plugin.Log.LogInfo((object)$"Spawned LuggageSmall at {spawnPosition}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnsmallluggage: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnBigLuggage()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		Quaternion spawnRotation = GetSpawnRotation();
		try
		{
			GameObject val = PhotonNetwork.Instantiate("0_Items/LuggageBig", spawnPosition, spawnRotation, (byte)0, (object[])null);
			Debug.Log((object)$"spawnbigluggage: Successfully spawned LuggageBig at {spawnPosition}");
			Plugin.Log.LogInfo((object)$"Spawned LuggageBig at {spawnPosition}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnbigluggage: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnEpicLuggage()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		Quaternion spawnRotation = GetSpawnRotation();
		try
		{
			GameObject val = PhotonNetwork.Instantiate("0_Items/LuggageEpic", spawnPosition, spawnRotation, (byte)0, (object[])null);
			Debug.Log((object)$"spawnepicluggage: Successfully spawned LuggageEpic at {spawnPosition}");
			Plugin.Log.LogInfo((object)$"Spawned LuggageEpic at {spawnPosition}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnepicluggage: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnAncientLuggage()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		Quaternion spawnRotation = GetSpawnRotation();
		try
		{
			GameObject val = PhotonNetwork.Instantiate("0_Items/LuggageAncient", spawnPosition, spawnRotation, (byte)0, (object[])null);
			Debug.Log((object)$"spawnancientluggage: Successfully spawned LuggageAncient at {spawnPosition}");
			Plugin.Log.LogInfo((object)$"Spawned LuggageAncient at {spawnPosition}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnancientluggage: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnLuggageString(string size = "small")
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		Quaternion spawnRotation = GetSpawnRotation();
		string text = size.ToLower() switch
		{
			"small" => "0_Items/LuggageSmall", 
			"big" => "0_Items/LuggageBig", 
			"epic" => "0_Items/LuggageEpic", 
			"ancient" => "0_Items/LuggageAncient", 
			_ => "0_Items/LuggageSmall", 
		};
		try
		{
			GameObject val = PhotonNetwork.Instantiate(text, spawnPosition, spawnRotation, (byte)0, (object[])null);
			Debug.Log((object)$"spawnluggagestring: Successfully spawned {text} at {spawnPosition}");
			Plugin.Log.LogInfo((object)$"Spawned {text} at {spawnPosition}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnluggagestring: Failed - " + ex.Message));
			Debug.Log((object)"Available sizes: small, big, epic, ancient");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnBeeSwarm()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		try
		{
			GameObject val = SafeSpawn("BeeSwarm", spawnPosition, GetSpawnRotation());
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Debug.Log((object)$"spawnbeeswarm: Successfully spawned BeeSwarm at {spawnPosition}");
				Plugin.Log.LogInfo((object)$"Spawned BeeSwarm at {spawnPosition}");
			}
			else
			{
				Debug.LogError((object)"spawnbeeswarm: Failed to spawn BeeSwarm");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnbeeswarm: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnRopeAnchorWithRope(float length = 40f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		length = Mathf.Clamp(length, 5f, 40f);
		try
		{
			GameObject val = SafeSpawn("RopeAnchorWithRope", spawnPosition, GetSpawnRotation());
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				RopeAnchorWithRope component = val.GetComponent<RopeAnchorWithRope>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					component.ropeSegmentLength = length;
					component.SpawnRope();
					Debug.Log((object)$"spawnropeanchorwithrope: Set rope length to {length} segments");
				}
				Debug.Log((object)$"spawnropeanchorwithrope: Successfully spawned RopeAnchorWithRope (length: {length}) at {spawnPosition}");
				Plugin.Log.LogInfo((object)$"Spawned RopeAnchorWithRope with length {length} at {spawnPosition}");
			}
			else
			{
				Debug.LogError((object)"spawnropeanchorwithrope: Failed to spawn RopeAnchorWithRope");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnropeanchorwithrope: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnShitPiton()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 spawnPosition = GetSpawnPosition();
		try
		{
			GameObject val = SafeSpawn("ClimbingSpikeHammered_Shitty", spawnPosition, GetSpawnRotation());
			if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
			{
				Debug.Log((object)$"spawnclimbingspike: Successfully spawned ClimbingSpikeHammered_Shitty at {spawnPosition}");
				Plugin.Log.LogInfo((object)$"Spawned ClimbingSpikeHammered_Shitty at {spawnPosition}");
			}
			else
			{
				Debug.LogError((object)"spawnclimbingspike: Failed to spawn ClimbingSpikeHammered_Shitty");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("spawnclimbingspike: Failed - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SpawnChain()
	{
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Character localCharacter = Character.localCharacter;
			if ((UnityEngine.Object)(object)localCharacter == (UnityEngine.Object)null)
			{
				Debug.LogError((object)"spawnchain: local character not found!");
				return;
			}
			MainCamera instance = MainCamera.instance;
			Camera val = ((instance != null) ? ((Component)instance).GetComponent<Camera>() : null);
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogError((object)"spawnchain: main camera not found!");
				return;
			}
			RaycastHit val2 = default(RaycastHit);
			if (!Physics.Raycast(((Component)val).transform.position, ((Component)val).transform.forward, out val2, 125f))
			{
				Debug.LogError((object)"spawnchain: no target found!");
				return;
			}
			Debug.Log((object)"ChainShooter shoot");
			Vector3 val3 = ((Component)val).transform.position + ((Component)val).transform.forward * 1f;
			Vector3 val4 = ((Component)val).transform.position - Vector3.up * 0.2f;
			RaycastHit val5 = default(RaycastHit);
			if (Physics.Raycast(val3, Vector3.down, out val5, 4f))
			{
				val4 = val5.point + Vector3.up * 1.5f;
			}
			int num = 10;
			Vector2 val6 = default(Vector2);
			Vector3 val7 = default(Vector3);
			for (int i = 0; i < num; i++)
			{
				val6 = new Vector2(-15f, -5f);
				float x = Vector2.Lerp(val6, Vector2.zero, (float)i / ((float)num - 1f)).x;
				x += UnityEngine.Random.Range(-2f, 2f);
				Debug.Log((object)$"from: {val4}, to: {val2.point}, hang: {x}");
				if (!JungleVine.CheckVinePath(val4, val2.point, x, out val7))
				{
					continue;
				}
				try
				{
					GameObject val8 = PhotonNetwork.Instantiate("ChainShootable", val4, Quaternion.identity, (byte)0, (object[])null);
					JungleVine component = val8.GetComponent<JungleVine>();
					if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
					{
						PhotonView photonView = component.photonView;
						if (photonView != null)
						{
							photonView.RPC("ForceBuildVine_RPC", (RpcTarget)3, new object[4]
							{
								val4,
								val2.point,
								x,
								val7
							});
						}
						Debug.Log((object)"Vine built, calling onFinish");
						Debug.Log((object)$"spawnchain: Successfully built chain from {val4} to {val2.point} (attempt {i + 1})");
						Plugin.Log.LogInfo((object)$"Spawned chain from {val4} to {val2.point} (distance: {Vector3.Distance(val4, val2.point):F1}m)");
						return;
					}
					Debug.LogError((object)"spawnchain: JungleVine component not found on ChainShootable");
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("spawnchain: Failed to spawn ChainShootable - " + ex.Message));
				}
			}
			Debug.LogWarning((object)$"spawnchain: Could not find clear path for chain after {num} attempts");
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("spawnchain: Failed - " + ex2.Message));
		}
	}
}
