using System;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace AdvancedConsole;

public static class SegmentSync
{
	public static void SyncJumpToSegment(Segment targetSegment)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (!MasterClientUtils.IsMasterClient("SyncJumpToSegment"))
		{
			return;
		}
		try
		{
			Plugin.Log.LogInfo((object)$"Starting synchronized segment jump to {targetSegment}");
			if (TryJumpViaCampfire(targetSegment))
			{
				Plugin.Log.LogInfo((object)$"Successfully initiated campfire-based segment jump to {targetSegment}");
			}
			else if (TryJumpViaDirectTeleport(targetSegment))
			{
				Plugin.Log.LogInfo((object)$"Successfully initiated direct teleport to {targetSegment}");
			}
			else
			{
				TrySetSegmentRoomProperty(targetSegment);
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in synchronized segment jump: " + ex.Message));
			Debug.LogWarning((object)("segmentsync: failed - " + ex.Message));
		}
	}

	private static bool TryJumpViaCampfire(Segment targetSegment)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected I4, but got Unknown
		try
		{
			Campfire[] array = UnityEngine.Object.FindObjectsByType<Campfire>((FindObjectsSortMode)0);
			Campfire[] array2 = array;
			foreach (Campfire val in array2)
			{
				if (!IsCampfireForSegment(val, targetSegment))
				{
					continue;
				}
				Plugin.Log.LogInfo((object)$"Found matching campfire for segment {targetSegment}");
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
				{
					try
					{
						component.RPC("SetSegmentRPC", (RpcTarget)0, new object[1] { (int)targetSegment });
					}
					catch
					{
						component.RPC("Light_Rpc", (RpcTarget)0, Array.Empty<object>());
						NotifySegmentJump(targetSegment);
					}
					return true;
				}
			}
			Plugin.Log.LogInfo((object)$"No matching campfire found for segment {targetSegment}");
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Campfire-based segment jump failed: " + ex.Message));
			return false;
		}
	}

	private static bool IsCampfireForSegment(Campfire campfire, Segment targetSegment)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				return false;
			}
			Vector3 position = ((Component)campfire).transform.position;
			float num = float.MaxValue;
			Segment val2 = (Segment)0;
			MapHandler.MapSegment[] segments = val.segments;
			if (segments != null)
			{
				for (int i = 0; i < segments.Length && i < 6; i++)
				{
					if ((UnityEngine.Object)(object)segments[i]?.reconnectSpawnPos != (UnityEngine.Object)null)
					{
						float num2 = Vector3.Distance(position, segments[i].reconnectSpawnPos.position);
						if (num2 < num)
						{
							num = num2;
							val2 = (Segment)(byte)i;
						}
					}
				}
			}
			if (segments != null && segments.Length > 4 && (UnityEngine.Object)(object)segments[4]?.reconnectSpawnPos != (UnityEngine.Object)null)
			{
				float num3 = Vector3.Distance(position, segments[4].reconnectSpawnPos.position);
				if (num3 < num)
				{
					val2 = (Segment)4;
					num = num3;
				}
			}
			if ((UnityEngine.Object)(object)val.respawnThePeak != (UnityEngine.Object)null)
			{
				float num4 = Vector3.Distance(position, val.respawnThePeak.position);
				if (num4 < num)
				{
					val2 = (Segment)5;
				}
			}
			return val2 == targetSegment && num < 50f;
		}
		catch
		{
			return false;
		}
	}

	private static bool TryJumpViaDirectTeleport(Segment targetSegment)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Plugin.Log.LogWarning((object)"MapHandler not found for direct teleport");
				return false;
			}
			Vector3 segmentSpawnPosition = GetSegmentSpawnPosition(val, targetSegment);
			if (segmentSpawnPosition == Vector3.zero)
			{
				Plugin.Log.LogWarning((object)$"Could not find spawn position for segment {targetSegment}");
				return false;
			}
			Plugin.Log.LogInfo((object)$"Teleporting all players to segment {targetSegment} at position {segmentSpawnPosition}");
			int num = 0;
			foreach (Character allCharacter in Character.AllCharacters)
			{
				try
				{
					((MonoBehaviourPun)allCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { segmentSpawnPosition, false });
					num++;
				}
				catch (Exception ex)
				{
					Plugin.Log.LogWarning((object)("Failed to teleport " + allCharacter.characterName + ": " + ex.Message));
				}
			}
			TryLocalSegmentJump(targetSegment);
			Plugin.Log.LogInfo((object)$"Direct teleport completed: {num} players moved to {targetSegment}");
			return num > 0;
		}
		catch (Exception ex2)
		{
			Plugin.Log.LogWarning((object)("Direct teleport failed: " + ex2.Message));
			return false;
		}
	}

	private static Vector3 GetSegmentSpawnPosition(MapHandler mapHandler, Segment targetSegment)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected I4, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((int)targetSegment == 4)
			{
				MapHandler.MapSegment kilnSegment = (mapHandler.segments != null && mapHandler.segments.Length > 4) ? mapHandler.segments[4] : null;
				return (kilnSegment != null && (UnityEngine.Object)(object)kilnSegment.reconnectSpawnPos != (UnityEngine.Object)null) ? kilnSegment.reconnectSpawnPos.position : Vector3.zero;
			}
			if ((int)targetSegment == 5)
			{
				Transform respawnThePeak = mapHandler.respawnThePeak;
				return (respawnThePeak != null) ? respawnThePeak.position : Vector3.zero;
			}
			int num = (int)targetSegment;
			if (mapHandler.segments != null && num < mapHandler.segments.Length && (UnityEngine.Object)(object)mapHandler.segments[num]?.reconnectSpawnPos != (UnityEngine.Object)null)
			{
				return mapHandler.segments[num].reconnectSpawnPos.position;
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)$"Error getting spawn position for {targetSegment}: {ex.Message}");
		}
		return Vector3.zero;
	}

	private static void TryLocalSegmentJump(Segment targetSegment)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if (!((UnityEngine.Object)(object)val == (UnityEngine.Object)null))
			{
				MethodInfo method = typeof(MapHandler).GetMethod("JumpToSegment", BindingFlags.Static | BindingFlags.Public);
				if (method != null)
				{
					method.Invoke(null, new object[1] { targetSegment });
					Plugin.Log.LogInfo((object)$"Local segment jump completed for {targetSegment}");
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Local segment jump failed: " + ex.Message));
		}
	}

	private static void NotifySegmentJump(Segment targetSegment)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected I4, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PhotonView[] array = UnityEngine.Object.FindObjectsByType<PhotonView>((FindObjectsInactive)1, (FindObjectsSortMode)0);
			PhotonView[] array2 = array;
			foreach (PhotonView val in array2)
			{
				if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null && val.IsMine)
				{
					try
					{
						val.RPC("SyncSegmentJumpRPC", (RpcTarget)0, new object[1] { (int)targetSegment });
						Plugin.Log.LogInfo((object)$"Sent SyncSegmentJumpRPC for segment {targetSegment}");
						break;
					}
					catch
					{
					}
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Failed to send segment jump notification: " + ex.Message));
		}
	}

	private unsafe static void TrySetSegmentRoomProperty(Segment targetSegment)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected I4, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (PhotonNetwork.InRoom)
			{
				Hashtable val = new Hashtable();
				val[(object)"currentSegment"] = (int)targetSegment;
				val[(object)"segmentName"] = targetSegment.ToString();
				PhotonNetwork.CurrentRoom.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
				Plugin.Log.LogInfo((object)$"Set room property for segment {targetSegment}");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Failed to set segment room property: " + ex.Message));
		}
	}

	[PunRPC]
	public static void SyncSegmentJumpRPC(int segmentId)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (segmentId >= 0 && segmentId <= 5)
			{
				Segment val = (Segment)(byte)segmentId;
				Plugin.Log.LogInfo((object)$"Received segment jump RPC for {val}");
				if (!MasterClientUtils.IsMasterClient())
				{
					TryLocalSegmentJump(val);
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in SyncSegmentJumpRPC: " + ex.Message));
		}
	}
}
