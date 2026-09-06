using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

[HarmonyPatch]
public static class ConsoleLogPatch
{
	private class LogGroupEntry
	{
		[CompilerGenerated]
		private LogType _003CLogType_003Ek__BackingField;

		public string OriginalMessage { get; set; } = "";

		public LogType LogType
		{
			[CompilerGenerated]
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return _003CLogType_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				_003CLogType_003Ek__BackingField = value;
			}
		}

		public DateTime FirstOccurrence { get; set; }

		public DateTime LastOccurrence { get; set; }

		public int Count { get; set; } = 1;

		public List<DateTime> AllOccurrences { get; set; } = new List<DateTime>();

		public string Stacktrace { get; set; } = "";
	}

	private static readonly Dictionary<string, LogGroupEntry> _logGroups = new Dictionary<string, LogGroupEntry>();

	private static readonly TimeSpan _groupingTimeWindow = TimeSpan.FromMinutes(5.0);

	public static void Initialize()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		try
		{
			Harmony val = new Harmony("com.github.wwshare.advancedconsole.logpatch");
			Type typeFromHandle = typeof(DebugUIHandler);
			MethodInfo method = typeFromHandle.GetMethod("AddEntry", BindingFlags.Instance | BindingFlags.Public);
			if (method != null)
			{
				MethodInfo method2 = typeof(ConsoleLogPatch).GetMethod("AddEntryPrefix", BindingFlags.Static | BindingFlags.NonPublic);
				val.Patch((MethodBase)method, new HarmonyMethod(method2), (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
				Plugin.Log.LogInfo((object)"Successfully patched DebugUIHandler.AddEntry");
			}
			else
			{
				Plugin.Log.LogError((object)"Could not find DebugUIHandler.AddEntry method");
			}
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Failed to initialize ConsoleLogPatch: {arg}");
		}
	}

	private static bool AddEntryPrefix(DebugUIHandler __instance, ConsoleLogEntry entry)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (string.IsNullOrWhiteSpace(entry.Log))
			{
				return true;
			}
			string key = $"{entry.LogType}:{entry.Log.Trim()}:{entry.Stacktrace}";
			DateTime now = DateTime.Now;
			if (_logGroups.TryGetValue(key, out LogGroupEntry value))
			{
				if (now - value.LastOccurrence <= _groupingTimeWindow)
				{
					value.Count++;
					value.LastOccurrence = now;
					value.AllOccurrences.Add(now);
					ConsoleLogEntry item = new ConsoleLogEntry
					{
						Log = $"[{value.Count}] {value.OriginalMessage}",
						LogType = value.LogType,
						LogTime = value.FirstOccurrence,
						Frame = entry.Frame,
						Stacktrace = value.Stacktrace
					};
					List<ConsoleLogEntry> logEntries = GetLogEntries(__instance);
					if (logEntries != null)
					{
						for (int num = logEntries.Count - 1; num >= 0; num--)
						{
							ConsoleLogEntry val = logEntries[num];
							if (IsSameGroup(val.Log, val.LogType, val.Stacktrace, value))
							{
								logEntries.RemoveAt(num);
								break;
							}
						}
						logEntries.Add(item);
					}
					return false;
				}
				_logGroups.Remove(key);
			}
			LogGroupEntry logGroupEntry = new LogGroupEntry
			{
				OriginalMessage = entry.Log,
				LogType = entry.LogType,
				FirstOccurrence = now,
				LastOccurrence = now,
				Count = 1,
				Stacktrace = entry.Stacktrace
			};
			logGroupEntry.AllOccurrences.Add(now);
			_logGroups[key] = logGroupEntry;
			return true;
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in AddEntryPrefix: {arg}");
			return true;
		}
	}

	private static bool IsSameGroup(string entryLog, LogType entryLogType, string entryStacktrace, LogGroupEntry group)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		string text = entryLog;
		if (text.StartsWith("[") && text.Contains("] "))
		{
			int num = text.IndexOf("] ");
			if (num > 0)
			{
				text = text.Substring(num + 2);
			}
		}
		if (text.Trim() == group.OriginalMessage.Trim() && entryLogType == group.LogType)
		{
			return entryStacktrace == group.Stacktrace;
		}
		return false;
	}

	private static List<ConsoleLogEntry>? GetLogEntries(DebugUIHandler handler)
	{
		try
		{
			return typeof(DebugUIHandler).GetField("m_logEntries", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(handler) as List<ConsoleLogEntry>;
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error getting log entries: {arg}");
			return null;
		}
	}

	public static void CleanupOldEntries()
	{
		try
		{
			DateTime cutoffTime = DateTime.Now - _groupingTimeWindow;
			List<string> list = (from kvp in _logGroups
				where kvp.Value.LastOccurrence < cutoffTime
				select kvp.Key).ToList();
			foreach (string item in list)
			{
				_logGroups.Remove(item);
			}
			if (list.Count > 0)
			{
				Plugin.Log.LogInfo((object)$"Cleaned up {list.Count} old log groups");
			}
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error cleaning up old entries: {arg}");
		}
	}

	public static void GetLogGroupStats()
	{
		try
		{
			Plugin.Log.LogInfo((object)$"Active log groups: {_logGroups.Count}");
			IEnumerable<LogGroupEntry> enumerable = _logGroups.Values.OrderByDescending((LogGroupEntry g) => g.Count).Take(10);
			foreach (LogGroupEntry item in enumerable)
			{
				Plugin.Log.LogInfo((object)$"[{item.Count}] {item.OriginalMessage.Substring(0, Math.Min(50, item.OriginalMessage.Length))}...");
			}
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error getting log group stats: {arg}");
		}
	}
}
