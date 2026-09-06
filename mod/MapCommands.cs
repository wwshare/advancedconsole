using System;
using System.Reflection;
using Photon.Pun;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class MapCommands
{
	[ConsoleCommand]
	public static void ListSegments()
	{
		Debug.Log((object)"listsegments: 可用区域:");
		Debug.Log((object)"  0 - Beach (海滩)");
		Debug.Log((object)"  1 - Tropics (热带)");
		Debug.Log((object)"  2 - Alpine (高山地带)");
		Debug.Log((object)"  3 - Caldera (火山口)");
		Debug.Log((object)"  4 - TheKiln (熔炉)");
		Debug.Log((object)"  5 - Peak (山顶)");
		Debug.Log((object)"用法: MapCommands.JumpToSegment <segment_name_or_number>");
		Plugin.Log.LogInfo((object)"已列出所有可用区域");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void JumpToSegment(string segmentName)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Segment result2;
			if (byte.TryParse(segmentName, out var result))
			{
				if (result > 5)
				{
					Debug.LogWarning((object)$"jumptosegment: 无效的区域编号 {result}。有效范围: 0-5");
					return;
				}
				result2 = (Segment)result;
			}
			else if (!Enum.TryParse<Segment>(segmentName, true, out result2))
			{
				Debug.LogWarning((object)("jumptosegment: 未知区域 '" + segmentName + "'。请使用 ListSegments 查看可用区域"));
				return;
			}
			if (MasterClientUtils.IsMasterClient())
			{
				Debug.Log((object)$"jumptosegment: [房主] 正在为所有玩家发起同步跳转到 {result2}");
				SegmentSync.SyncJumpToSegment(result2);
				return;
			}
			Debug.Log((object)$"jumptosegment: [客户端] 正在请求本地跳转到 {result2}");
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"jumptosegment: 未找到 MapHandler!");
				return;
			}
			MethodInfo method = typeof(MapHandler).GetMethod("JumpToSegment", BindingFlags.Static | BindingFlags.Public);
			if (method != null)
			{
				method.Invoke(null, new object[1] { result2 });
				Debug.Log((object)$"jumptosegment: 本地跳转到区域 {result2} ({segmentName})");
				Plugin.Log.LogInfo((object)$"本地跳转到区域 {result2}");
			}
			else
			{
				Debug.LogWarning((object)"jumptosegment: 在 MapHandler 中未找到 JumpToSegment 方法");
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("jumptosegment: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void GetCurrentSegment()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"getcurrentsegment: 未找到 MapHandler!");
				return;
			}
			Segment currentSegment = val.GetCurrentSegment();
			string segmentDisplayName = GetSegmentDisplayName(currentSegment);
			Debug.Log((object)$"getcurrentsegment: 当前区域为 {currentSegment} ({segmentDisplayName})");
			Plugin.Log.LogInfo((object)$"当前区域: {currentSegment} ({segmentDisplayName})");
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("getcurrentsegment: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void NextSegment()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected I4, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"nextsegment: 未找到 MapHandler!");
				return;
			}
			Segment currentSegment = val.GetCurrentSegment();
			int num = (int)currentSegment + 1;
			if (num > 5)
			{
				Debug.LogWarning((object)"nextsegment: 已在最后一个区域 (Peak)");
			}
			else
			{
				JumpToSegment(((object)(Segment)(byte)num/*cast due to constrained. prefix*/).ToString());
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("nextsegment: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void PreviousSegment()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected I4, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"previoussegment: 未找到 MapHandler!");
				return;
			}
			Segment currentSegment = val.GetCurrentSegment();
			int num = (int)currentSegment - 1;
			if (num < 0)
			{
				Debug.LogWarning((object)"previoussegment: 已在第一个区域 (Beach)");
			}
			else
			{
				JumpToSegment(((object)(Segment)(byte)num/*cast due to constrained. prefix*/).ToString());
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("previoussegment: 失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToBeach()
	{
		JumpToSegment("Beach");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToTropics()
	{
		JumpToSegment("Tropics");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToAlpine()
	{
		JumpToSegment("Alpine");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToCaldera()
	{
		JumpToSegment("Caldera");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToKiln()
	{
		JumpToSegment("TheKiln");
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void GoToPeak()
	{
		JumpToSegment("Peak");
	}

	private unsafe static string GetSegmentDisplayName(Segment segment)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected I4, but got Unknown
		return (int)segment switch
		{
			0 => "Пляж", 
			1 => "Тропики", 
			2 => "Альпийская зона", 
			3 => "Кальдера", 
			4 => "Печь", 
			5 => "Вершина", 
			_ => segment.ToString(), 
		};
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SyncAllPlayersToSegment()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected I4, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Invalid comparison between Unknown and I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Invalid comparison between Unknown and I4
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			MapHandler val = UnityEngine.Object.FindFirstObjectByType<MapHandler>();
			if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
			{
				Debug.LogWarning((object)"syncallplayerstosegment: 未找到 MapHandler!");
				return;
			}
			Segment currentSegment = val.GetCurrentSegment();
			MapHandler.MapSegment[] segments = val.segments;
			if (segments == null || segments.Length == 0)
			{
				Debug.LogWarning((object)"syncallplayerstosegment: 未找到任何区域!");
				return;
			}
			Vector3 val2 = Vector3.zero;
			int num = (int)currentSegment;
			if ((int)currentSegment != 4)
			{
				if ((int)currentSegment == 5)
				{
					val2 = val.respawnThePeak.position;
				}
				else if (num < segments.Length && (UnityEngine.Object)(object)segments[num].reconnectSpawnPos != (UnityEngine.Object)null)
				{
					val2 = segments[num].reconnectSpawnPos.position;
				}
			}
			else
			{
				val2 = ((num < segments.Length && (UnityEngine.Object)(object)segments[num].reconnectSpawnPos != (UnityEngine.Object)null) ? segments[num].reconnectSpawnPos.position : val.respawnThePeak.position);
			}
			if (val2 == Vector3.zero)
			{
				Debug.LogWarning((object)"syncallplayerstosegment: 找不到当前区域的出生点!");
				return;
			}
			int num2 = 0;
			foreach (Character allCharacter in Character.AllCharacters)
			{
				try
				{
					((MonoBehaviourPun)allCharacter).photonView.RPC("WarpPlayerRPC", (RpcTarget)0, new object[2] { val2, false });
					num2++;
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)("syncallplayerstosegment: 传送失败, 玩家 " + allCharacter.characterName + " - " + ex.Message));
				}
			}
			string segmentDisplayName = GetSegmentDisplayName(currentSegment);
			Debug.Log((object)$"syncallplayerstosegment: 已将 {num2} 名玩家传送到 {currentSegment} ({segmentDisplayName})");
			Plugin.Log.LogInfo((object)$"已同步 {num2} 名玩家到区域 {currentSegment}");
		}
		catch (Exception ex2)
		{
			Debug.LogWarning((object)("syncallplayerstosegment: 失败 - " + ex2.Message));
		}
	}
}
