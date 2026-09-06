using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class Teleport
{
	[ConsoleCommand]
	public static void To(PhotonPlayer target)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"tp: player not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("tp: no Character for '" + target.NickName + "'"));
			return;
		}
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)localCharacter == (Object)null)
		{
			Debug.LogWarning((object)"tp: no local character!");
			return;
		}
		Vector3 val2 = val.Center + Vector3.up * 1.5f;
		((MonoBehaviourPun)localCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, true });
		Debug.Log((object)("tp: warped to '" + target.NickName + "'"));
		Plugin.Log.LogInfo((object)("Teleported to player: " + target.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void AllToMe()
	{
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)localCharacter == (Object)null)
		{
			Debug.LogWarning((object)"tpalltoMe: no local character!");
			return;
		}
		Plugin plugin = Object.FindFirstObjectByType<Plugin>();
		if ((Object)(object)plugin != (Object)null)
		{
			((MonoBehaviour)plugin).StartCoroutine(TeleportAllToMeWithDelay(localCharacter));
			return;
		}
		Debug.LogWarning((object)"tpalltoMe: Plugin instance not found, falling back to immediate teleport");
		TeleportAllToMeImmediate(localCharacter);
	}

	private static IEnumerator TeleportAllToMeWithDelay(Character localCharacter)
	{
		Vector3 myPosition = localCharacter.Center + Vector3.up * 1.5f;
		int teleportedCount = 0;
		List<Character> charactersToTeleport = Character.AllCharacters.Where((Character c) => (Object)(object)c != (Object)(object)localCharacter && !c.data.dead).ToList();
		Debug.Log((object)$"tpalltoMe: starting sequential teleport of {charactersToTeleport.Count} players");
		foreach (Character item in charactersToTeleport)
		{
			((MonoBehaviourPun)item).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { myPosition, true });
			teleportedCount++;
			PhotonPlayer owner = ((MonoBehaviourPun)item).photonView.Owner;
			string arg = ((owner != null) ? owner.NickName : null) ?? item.characterName;
			Debug.Log((object)$"tpalltoMe: teleported player {teleportedCount}: {arg}");
			if (teleportedCount < charactersToTeleport.Count)
			{
				yield return (object)new WaitForSeconds(1f);
			}
		}
		Debug.Log((object)$"tpalltoMe: completed teleporting {teleportedCount} players to me with 1s intervals");
		Plugin.Log.LogInfo((object)$"Teleported {teleportedCount} players to local player with 1s intervals");
	}

	private static void TeleportAllToMeImmediate(Character localCharacter)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = localCharacter.Center + Vector3.up * 1.5f;
		int num = 0;
		foreach (Character allCharacter in Character.AllCharacters)
		{
			if ((Object)(object)allCharacter != (Object)(object)localCharacter && !allCharacter.data.dead)
			{
				((MonoBehaviourPun)allCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val, true });
				num++;
			}
		}
		Debug.Log((object)$"tpalltoMe: teleported {num} players to me (immediate)");
		Plugin.Log.LogInfo((object)$"Teleported {num} players to local player (immediate)");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PlayerToPlayer(PhotonPlayer from, PhotonPlayer to)
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if (from == null || to == null)
		{
			Debug.LogWarning((object)"tpplayerto: one or both players not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)from) ?? false);
		Character val2 = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)to) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("tpplayerto: no Character for '" + from.NickName + "'"));
			return;
		}
		if ((Object)(object)val2 == (Object)null)
		{
			Debug.LogWarning((object)("tpplayerto: no Character for '" + to.NickName + "'"));
			return;
		}
		Vector3 val3 = val2.Center + Vector3.up * 1.5f;
		((MonoBehaviourPun)val).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val3, true });
		Debug.Log((object)("tpplayerto: teleported '" + from.NickName + "' to '" + to.NickName + "'"));
		Plugin.Log.LogInfo((object)("Teleported " + from.NickName + " to " + to.NickName));
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PlayerToMe(PhotonPlayer target)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"tptoMe: player not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("tptoMe: no Character for '" + target.NickName + "'"));
			return;
		}
		if ((Object)(object)localCharacter == (Object)null)
		{
			Debug.LogWarning((object)"tptoMe: no local character!");
			return;
		}
		Vector3 val2 = localCharacter.Center + Vector3.up * 1.5f;
		((MonoBehaviourPun)val).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, true });
		Debug.Log((object)("tptoMe: teleported '" + target.NickName + "' to me"));
		Plugin.Log.LogInfo((object)("Teleported " + target.NickName + " to local player"));
	}

	[ConsoleCommand]
	public static void RandomPlayer()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Character local = Character.localCharacter;
		if ((Object)(object)local == (Object)null)
		{
			Debug.LogWarning((object)"tprandom: no local character!");
			return;
		}
		List<Character> list = Character.AllCharacters.Where((Character c) => (Object)(object)c != (Object)(object)local && !c.data.dead).ToList();
		if (list.Count == 0)
		{
			Debug.LogWarning((object)"tprandom: no valid targets to warp to!");
			return;
		}
		Character val = list[Random.Range(0, list.Count)];
		Vector3 val2 = val.Center + Vector3.up * 1.5f;
		((MonoBehaviourPun)local).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, true });
		Debug.Log((object)("tprandom: warped to '" + val.characterName + "'"));
		Plugin.Log.LogInfo((object)("Teleported to random player: " + val.characterName));
	}

	private static Vector3 GetTeleportPosition()
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
		if ((Object)(object)MainCamera.instance == (Object)null)
		{
			Debug.LogWarning((object)"GetTeleportPosition: MainCamera not found, using default position");
			return Vector3.zero;
		}
		Transform transform = ((Component)MainCamera.instance).transform;
		float maxRaycastDistance = PluginConfig.MaxRaycastDistance;
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(transform.position, transform.forward, out val, maxRaycastDistance))
		{
			return val.point + val.normal * 1.5f;
		}
		float fallbackDistance = PluginConfig.FallbackDistance;
		return transform.position + transform.forward * fallbackDistance;
	}

	[ConsoleCommand]
	public static void TeleportToPoint()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Character localCharacter = Character.localCharacter;
		if ((Object)(object)localCharacter == (Object)null)
		{
			Debug.LogWarning((object)"tptopoint: no local character!");
			return;
		}
		Vector3 teleportPosition = GetTeleportPosition();
		((MonoBehaviourPun)localCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { teleportPosition, true });
		Debug.Log((object)$"tptopoint: teleported to point {teleportPosition}");
		Plugin.Log.LogInfo((object)$"Teleported to point: {teleportPosition}");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void TeleportPlayerToPoint(PhotonPlayer target)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"tpplayertopoint: player not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogWarning((object)("tpplayertopoint: no Character for '" + target.NickName + "'"));
			return;
		}
		Vector3 teleportPosition = GetTeleportPosition();
		((MonoBehaviourPun)val).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { teleportPosition, true });
		Debug.Log((object)$"tpplayertopoint: teleported '{target.NickName}' to point {teleportPosition}");
		Plugin.Log.LogInfo((object)$"Teleported player {target.NickName} to point: {teleportPosition}");
	}
}
