using System;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public static class SyncCommands
{
	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ForceSync(PhotonPlayer target)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Debug.LogWarning((object)"forcesync: player not found!");
			return;
		}
		Character val = Character.AllCharacters.FirstOrDefault((Character c) => ((object)((MonoBehaviourPun)c).photonView.Owner)?.Equals((object?)target) ?? false);
		if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
		{
			Debug.LogWarning((object)("forcesync: no Character for '" + target.NickName + "'"));
			return;
		}
		Vector3 center = val.Center;
		((MonoBehaviourPun)val).photonView.RPC("WarpPlayerRPC", (RpcTarget)1, new object[2] { center, false });
		Debug.Log((object)("forcesync: force synchronized '" + target.NickName + "' position"));
		Plugin.Log.LogInfo((object)("Force synchronized " + target.NickName + " position"));
	}

	[ConsoleCommand]
	public static void RefreshRPC()
	{
		try
		{
			foreach (Character allCharacter in Character.AllCharacters)
			{
				if ((UnityEngine.Object)(object)allCharacter != (UnityEngine.Object)null && (UnityEngine.Object)(object)((Component)allCharacter).GetComponent<CharacterAfflictionsRPC>() == (UnityEngine.Object)null)
				{
					((Component)allCharacter).gameObject.AddComponent<CharacterAfflictionsRPC>();
				}
			}
			Debug.Log((object)"refreshrpc: refreshed all RPC components");
			Plugin.Log.LogInfo((object)"Refreshed all RPC components");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("refreshrpc: failed - " + ex.Message));
		}
	}
}
