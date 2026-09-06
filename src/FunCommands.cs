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

public static class FunCommands
{
	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void KillPlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"kill: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("kill: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		val.data.dead = true;
		((MonoBehaviourPun)val).photonView.RPC("RPCA_Die", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("kill: 已击杀玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已击杀玩家: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void RevivePlayer(PhotonPlayer target)
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
		val.data.dead = false;
		((MonoBehaviourPun)val).photonView.RPC("RPCA_Respawn", (RpcTarget)0, Array.Empty<object>());
		Debug.Log((object)("revive: 已复活玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已复活玩家: " + target.NickName));
	}

	[ConsoleCommand]
	public static void FreezePlayer(PhotonPlayer target)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"freeze: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("freeze: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Rigidbody val2 = default(Rigidbody);
		if (((Component)val).TryGetComponent<Rigidbody>(out val2))
		{
			val2.linearVelocity = Vector3.zero;
			val2.isKinematic = true;
		}
		Debug.Log((object)("freeze: 已冻结玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已冻结玩家: " + target.NickName));
	}

	[ConsoleCommand]
	public static void UnfreezePlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"unfreeze: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("unfreeze: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Rigidbody val2 = default(Rigidbody);
		if (((Component)val).TryGetComponent<Rigidbody>(out val2))
		{
			val2.isKinematic = false;
		}
		Debug.Log((object)("unfreeze: 已解冻玩家 '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("已解冻玩家: " + target.NickName));
	}

	[ConsoleCommand]
	public static void InvisiblePlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"invisible: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("invisible: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Renderer[] componentsInChildren = ((Component)val).GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer val2 in array)
		{
			val2.enabled = false;
		}
		Debug.Log((object)("invisible: 已将玩家 '" + target.NickName + "' 设为隐身"));
		Plugin.Log.LogInfo((object)("已将玩家设为隐身: " + target.NickName));
	}

	[ConsoleCommand]
	public static void VisiblePlayer(PhotonPlayer target)
	{
		if (target == null)
		{
			Debug.LogWarning((object)"visible: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("visible: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		Renderer[] componentsInChildren = ((Component)val).GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer val2 in array)
		{
			val2.enabled = true;
		}
		Debug.Log((object)("visible: 已使玩家 '" + target.NickName + "' 可见"));
		Plugin.Log.LogInfo((object)("已使玩家可见: " + target.NickName));
	}

	[ConsoleCommand]
	public static void SetPlayerSize(PhotonPlayer target, float scale = 1f)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"setsize: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("setsize: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		((Component)val).transform.localScale = Vector3.one * scale;
		try
		{
			((MonoBehaviourPun)val).photonView.RPC("SyncScaleRPC", (RpcTarget)1, new object[1] { scale });
		}
		catch (Exception)
		{
		}
		Debug.Log((object)$"setsize: 已将玩家 '{target.NickName}' 的大小更改为 {scale}");
		Plugin.Log.LogInfo((object)$"已将玩家 {target.NickName} 的大小更改为 {scale}");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void DropCoconutOnPlayer(PhotonPlayer target)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"dropcoconut: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("dropcoconut: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		try
		{
			Vector3 val2 = val.Head + Vector3.up * 2f;
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"dropcoconut: 未找到物品数据库!");
				return;
			}
			KeyValuePair<ushort, Item> keyValuePair = instance.itemLookup.FirstOrDefault((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Contains("Coconut", StringComparison.OrdinalIgnoreCase));
			if ((UnityEngine.Object)(object)keyValuePair.Value == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"dropcoconut: 在数据库中未找到椰子物品!");
				return;
			}
			GameObject val3 = PhotonNetwork.Instantiate("0_Items/" + ((UnityEngine.Object)keyValuePair.Value).name, val2, Quaternion.identity, (byte)0, (object[])null);
			Rigidbody val4 = default(Rigidbody);
			if ((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null && val3.TryGetComponent<Rigidbody>(out val4))
			{
				val4.linearVelocity = Vector3.down * 20f;
				val4.angularVelocity = UnityEngine.Random.insideUnitSphere * 5f;
			}
			Item val5 = default(Item);
			PhotonView val6 = default(PhotonView);
			if ((UnityEngine.Object)(object)val3 != (UnityEngine.Object)null && val3.TryGetComponent<Item>(out val5) && (UnityEngine.Object)(object)Character.localCharacter != (UnityEngine.Object)null && ((Component)Character.localCharacter).TryGetComponent<PhotonView>(out val6))
			{
				val5.RequestPickup(val6);
			}
			Debug.Log((object)("dropcoconut: 已将高速椰子砸向玩家 '" + target.NickName + "' (速度: 向下 20 m/s)"));
			Plugin.Log.LogInfo((object)("已将高速椰子砸向玩家: " + target.NickName));
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("dropcoconut: 失败 - " + ex.Message));
		}
	}

	public static void CoconutRain(PhotonPlayer target, float radius = 5f, int count = 10, float height = 5f)
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"coconutrain: 未找到玩家!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("coconutrain: 未找到玩家 '" + target.NickName + "'"));
			return;
		}
		try
		{
			ItemDatabase instance = SingletonAsset<ItemDatabase>.Instance;
			if (instance?.itemLookup == null)
			{
				Debug.LogWarning((object)"coconutrain: 未找到物品数据库!");
				return;
			}
			KeyValuePair<ushort, Item> keyValuePair = instance.itemLookup.FirstOrDefault((KeyValuePair<ushort, Item> kvp) => ((UnityEngine.Object)kvp.Value).name.Contains("Coconut", StringComparison.OrdinalIgnoreCase));
			if ((UnityEngine.Object)(object)keyValuePair.Value == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"coconutrain: 在数据库中未找到椰子物品!");
				return;
			}
			Vector3 head = val.Head;
			radius = Mathf.Clamp(radius, 1f, 20f);
			count = Mathf.Clamp(count, 1, 50);
			height = Mathf.Clamp(height, 2f, 20f);
			for (int num = 0; num < count; num++)
			{
				Vector2 val2 = UnityEngine.Random.insideUnitCircle * radius;
				Vector3 val3 = head + new Vector3(val2.x, height, val2.y);
				ItemDatabase.Add(keyValuePair.Value, val3);
			}
			Debug.Log((object)string.Format("coconutrain: 在 '{1}' 周围生成了 {0} 个椰子 (半径: {2}m, 高度: {3}m)", new object[4] { count, target.NickName, radius, height }));
			Plugin.Log.LogInfo((object)$"已在玩家周围制造椰子雨: {target.NickName} ({count} 个椰子)");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("coconutrain: 失败 - " + ex.Message));
		}
	}
}
