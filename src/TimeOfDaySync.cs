using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace AdvancedConsole;

public static class TimeOfDaySync
{
	public static void SyncTimeOfDay(float timeValue)
	{
		if (!MasterClientUtils.IsMasterClient("SyncTimeOfDay"))
		{
			return;
		}
		try
		{
			timeValue = Mathf.Clamp01(timeValue);
			Plugin.Log.LogInfo((object)$"Synchronizing time of day to {timeValue:F2} for all players");
			if (TrySyncViaDayNightManager(timeValue))
			{
				Plugin.Log.LogInfo((object)$"Successfully synced time via DayNightManager: {timeValue:F2}");
				return;
			}
			if (TrySyncViaLightingComponents(timeValue))
			{
				Plugin.Log.LogInfo((object)$"Successfully synced time via lighting components: {timeValue:F2}");
				return;
			}
			if (TrySyncViaLightObjects(timeValue))
			{
				Plugin.Log.LogInfo((object)$"Successfully synced time via light objects: {timeValue:F2}");
				return;
			}
			TrySyncViaRenderSettings(timeValue);
			TrySetTimeRoomProperty(timeValue);
			Plugin.Log.LogInfo((object)$"Time sync completed: {timeValue:F2}");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error syncing time of day: " + ex.Message));
			Debug.LogWarning((object)("timeofday: sync failed - " + ex.Message));
		}
	}

	private static bool TrySyncViaDayNightManager(float timeValue)
	{
		try
		{
			DayNightManager instance = DayNightManager.instance;
			if ((UnityEngine.Object)(object)instance == (UnityEngine.Object)null)
			{
				return false;
			}
			float timeToSet = timeValue * 24f;
			instance.setTimeOfDay(timeToSet);
			PhotonView component = ((Component)instance).GetComponent<PhotonView>();
			if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && PhotonNetwork.IsConnected)
			{
				component.RPC("RPCA_SyncTime", (RpcTarget)1, new object[2] { instance.dayCount, timeToSet });
			}
			instance.UpdateCycle();
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("DayNightManager sync failed: " + ex.Message));
			return false;
		}
	}

	private static bool TrySyncViaLightingComponents(float timeValue)
	{
		try
		{
			PhotonView[] array = UnityEngine.Object.FindObjectsByType<PhotonView>((FindObjectsSortMode)0);
			PhotonView[] array2 = array;
			foreach (PhotonView val in array2)
			{
				if ((((UnityEngine.Object)((Component)val).gameObject).name.Contains("Light") || ((UnityEngine.Object)((Component)val).gameObject).name.Contains("Sun")) && val.IsMine)
				{
					val.RPC("SyncTimeOfDayRPC", (RpcTarget)0, new object[1] { timeValue });
					return true;
				}
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	private static bool TrySyncViaLightObjects(float timeValue)
	{
		try
		{
			Light[] array = UnityEngine.Object.FindObjectsByType<Light>((FindObjectsSortMode)0);
			Light[] array2 = array;
			foreach (Light val in array2)
			{
				PhotonView component = ((Component)val).GetComponent<PhotonView>();
				if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null && component.IsMine)
				{
					component.RPC("SyncTimeRPC", (RpcTarget)0, new object[1] { timeValue });
					return true;
				}
				if (((UnityEngine.Object)val).name.Contains("Sun") || ((UnityEngine.Object)val).name.Contains("Directional"))
				{
					float num = Mathf.Clamp01(Mathf.Cos((timeValue - 0.5f) * 2f * MathF.PI));
					val.intensity = num * 1.2f;
				}
			}
			return array.Length != 0;
		}
		catch
		{
			return false;
		}
	}

	private static void TrySyncViaRenderSettings(float timeValue)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			float num = Mathf.Clamp01(Mathf.Cos((timeValue - 0.5f) * 2f * MathF.PI));
			if ((UnityEngine.Object)(object)RenderSettings.sun != (UnityEngine.Object)null)
			{
				RenderSettings.sun.intensity = num * 1.2f;
				Color color = Color.Lerp(new Color(1f, 0.8f, 0.6f), Color.white, num);
				RenderSettings.sun.color = color;
			}
			Color ambientLight = Color.Lerp(new Color(0.2f, 0.2f, 0.4f), new Color(0.8f, 0.8f, 1f), num);
			RenderSettings.ambientLight = ambientLight;
			Plugin.Log.LogInfo((object)$"Applied visual time effects: intensity {num:F2}");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Failed to apply visual time effects: " + ex.Message));
		}
	}

	private static void TrySetTimeRoomProperty(float timeValue)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		try
		{
			if (PhotonNetwork.InRoom)
			{
				Hashtable val = new Hashtable();
				val[(object)"timeOfDay"] = timeValue;
				val[(object)"timeSync"] = DateTime.UtcNow.Ticks;
				PhotonNetwork.CurrentRoom.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
				Plugin.Log.LogInfo((object)$"Set room time property: {timeValue:F2}");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogWarning((object)("Failed to set time room property: " + ex.Message));
		}
	}

	[PunRPC]
	public static void SyncTimeOfDayRPC(float timeValue)
	{
		try
		{
			Plugin.Log.LogInfo((object)$"Received time sync RPC: {timeValue:F2}");
			if (!PhotonNetwork.IsMasterClient)
			{
				TrySyncViaLightObjects(timeValue);
				TrySyncViaRenderSettings(timeValue);
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in SyncTimeOfDayRPC: " + ex.Message));
		}
	}
}
