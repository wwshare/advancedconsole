using System;
using System.Linq;
using UnityEngine;
using Zorro.Core;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class AchievementCommands
{
	[ConsoleCommand]
	public static void GrantAchievement(ACHIEVEMENTTYPE achievementType)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			AchievementManager.Grant(achievementType);
			Debug.Log((object)$"grantachievement: 成就 '{achievementType}' 已授予成功!");
			Plugin.Log.LogInfo((object)$"已授予成就: {achievementType}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)$"grantachievement: 授予成就 '{achievementType}' 失败 - {ex.Message}");
			Plugin.Log.LogError((object)("授予成就失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void GrantAchievementByName(string achievementName)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (Enum.TryParse<ACHIEVEMENTTYPE>(achievementName, true, out ACHIEVEMENTTYPE result))
			{
				AchievementManager.Grant(result);
				Debug.Log((object)$"grantachievementbyname: 成就 '{result}' 已授予成功!");
				Plugin.Log.LogInfo((object)$"已按名称授予成就: {result}");
			}
			else
			{
				Debug.LogWarning((object)("grantachievementbyname: 未找到成就 '" + achievementName + "'! 请使用 ListAchievements 查看可用成就。"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("grantachievementbyname: 授予成就 '" + achievementName + "' - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ClearAllAchievements()
	{
		try
		{
			AchievementManager.ClearAchievements();
			Debug.Log((object)"clearallachievements: 所有成就已成功清除!");
			Plugin.Log.LogInfo((object)"已清除所有成就");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("clearallachievements: 清除成就失败 - " + ex.Message));
			Plugin.Log.LogError((object)("清除成就失败: " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ListAchievements()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Debug.Log((object)"=== 可用成就 ===");
			ACHIEVEMENTTYPE[] array = Enum.GetValues(typeof(ACHIEVEMENTTYPE)).Cast<ACHIEVEMENTTYPE>().Where(delegate(ACHIEVEMENTTYPE a)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Invalid comparison between Unknown and I4
				return (int)a > 0;
			})
				.ToArray();
			ACHIEVEMENTTYPE[] array2 = array;
			foreach (ACHIEVEMENTTYPE val in array2)
			{
				bool flag = false;
				try
				{
					AchievementManager instance = Singleton<AchievementManager>.Instance;
					if ((UnityEngine.Object)(object)instance != (UnityEngine.Object)null)
					{
						flag = instance.IsAchievementUnlocked(val);
					}
				}
				catch
				{
				}
				string arg = (flag ? "[UNLOCKED]" : "[LOCKED]");
				Debug.Log((object)$"  {val} {arg}");
			}
			Debug.Log((object)$"成就总数: {array.Length}");
			Debug.Log((object)"使用 GrantAchievement 或 GrantAchievementByName 解锁成就");
			Plugin.Log.LogInfo((object)$"已列出 {array.Length} 项成就");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("listachievements: 列出成就失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void AchievementInfo(ACHIEVEMENTTYPE achievementType)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Debug.Log((object)$"=== 成就信息: {achievementType} ===");
			string achievementDescription = GetAchievementDescription(achievementType);
			Debug.Log((object)("描述: " + achievementDescription));
			try
			{
				AchievementManager instance = Singleton<AchievementManager>.Instance;
				if ((UnityEngine.Object)(object)instance != (UnityEngine.Object)null)
				{
					bool flag = instance.IsAchievementUnlocked(achievementType);
					Debug.Log((object)("状态: " + (flag ? "UNLOCKED" : "LOCKED")));
					if (instance.runBasedValueData.achievementsEarnedThisRun.Contains(achievementType))
					{
						Debug.Log((object)"本次旅程获得: 是");
					}
				}
			}
			catch
			{
				Debug.Log((object)"状态: 无法检查");
			}
			Plugin.Log.LogInfo((object)$"已显示成就信息: {achievementType}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("achievementinfo: 获取成就信息失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void GrantRandomAchievement()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ACHIEVEMENTTYPE[] array = Enum.GetValues(typeof(ACHIEVEMENTTYPE)).Cast<ACHIEVEMENTTYPE>().Where(delegate(ACHIEVEMENTTYPE a)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Invalid comparison between Unknown and I4
				return (int)a > 0;
			})
				.ToArray();
			if (array.Length == 0)
			{
				Debug.LogWarning((object)"grantrandomachievement: 没有可用成就!");
				return;
			}
			ACHIEVEMENTTYPE val = array[UnityEngine.Random.Range(0, array.Length)];
			AchievementManager.Grant(val);
			Debug.Log((object)$"grantrandomachievement: 已授予随机成就 '{val}'!");
			Plugin.Log.LogInfo((object)$"已授予随机成就: {val}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("grantrandomachievement: 授予随机成就失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void GrantAllAchievements()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ACHIEVEMENTTYPE[] array = Enum.GetValues(typeof(ACHIEVEMENTTYPE)).Cast<ACHIEVEMENTTYPE>().Where(delegate(ACHIEVEMENTTYPE a)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Invalid comparison between Unknown and I4
				return (int)a > 0;
			})
				.ToArray();
			int num = 0;
			ACHIEVEMENTTYPE[] array2 = array;
			foreach (ACHIEVEMENTTYPE val in array2)
			{
				try
				{
					AchievementManager.Grant(val);
					num++;
				}
				catch (Exception ex)
				{
					Debug.LogWarning((object)$"grantallachievements: 授予 {val} 失败 - {ex.Message}");
				}
			}
			Debug.Log((object)$"grantallachievements: 已授予 {num}/{array.Length} 项成就!");
			Plugin.Log.LogInfo((object)$"已授予所有成就: {num}/{array.Length}");
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)("grantallachievements: 授予所有成就失败 - " + ex2.Message));
		}
	}

	[ConsoleCommand]
	public static void AchievementStats()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ACHIEVEMENTTYPE[] array = Enum.GetValues(typeof(ACHIEVEMENTTYPE)).Cast<ACHIEVEMENTTYPE>().Where(delegate(ACHIEVEMENTTYPE a)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Invalid comparison between Unknown and I4
				return (int)a > 0;
			})
				.ToArray();
			int num = 0;
			int num2 = 0;
			AchievementManager instance = Singleton<AchievementManager>.Instance;
			if ((UnityEngine.Object)(object)instance != (UnityEngine.Object)null)
			{
				ACHIEVEMENTTYPE[] array2 = array;
				foreach (ACHIEVEMENTTYPE val in array2)
				{
					try
					{
						if (instance.IsAchievementUnlocked(val))
						{
							num++;
						}
						if (instance.runBasedValueData.achievementsEarnedThisRun.Contains(val))
						{
							num2++;
						}
					}
					catch
					{
					}
				}
			}
			Debug.Log((object)"=== 成就统计 ===");
			Debug.Log((object)$"成就总数: {array.Length}");
			Debug.Log((object)$"已解锁: {num}/{array.Length} ({(float)num / (float)array.Length * 100f:F1}%)");
			Debug.Log((object)$"本次旅程获得: {num2}");
			Debug.Log((object)$"剩余: {array.Length - num}");
			Plugin.Log.LogInfo((object)$"成就统计: 已解锁 {num}/{array.Length}, 本次 {num2}");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("achievementstats: 获取成就统计失败 - " + ex.Message));
		}
	}

	private static string GetAchievementDescription(ACHIEVEMENTTYPE achievementType)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected I4, but got Unknown
		return ((int)achievementType - 1) switch
		{
			0 => "Tried Your Best - Keep trying!", 
			1 => "Beachcomber - Master of the Beach biome", 
			2 => "Trailblazer - Explorer of trails", 
			3 => "Alpinist - Mountain climbing expert", 
			4 => "Volcanology - Volcano and lava expert", 
			5 => "Cooking - Master chef of the wilderness", 
			6 => "Happy Camper - Camping enthusiast", 
			7 => "Bouldering - Rock climbing specialist", 
			8 => "Toxicology - Poison and antidote expert", 
			9 => "Foraging - Master of finding resources", 
			10 => "Esoterica - Hidden knowledge seeker", 
			11 => "Peak - Summit conquerer", 
			12 => "Lone Wolf - Solo adventurer", 
			13 => "Clutch - Rescue specialist", 
			14 => "Balloon - High altitude specialist", 
			15 => "Leave No Trace - Environmental protector", 
			16 => "Speed Climber - Fast ascent specialist", 
			17 => "Bing Bong - Mystery achievement", 
			18 => "Naturalist - Nature observer", 
			19 => "Gourmand - Fine food connoisseur", 
			20 => "Mycology - Mushroom expert", 
			21 => "First Aid - Medical specialist", 
			22 => "Survivalist - Ultimate survival expert", 
			23 => "Animal Serenading - Animal whisperer", 
			24 => "Arborist - Tree expert", 
			25 => "Mentorship - Teacher and guide", 
			26 => "Knot Tying - Rope work specialist", 
			27 => "Emergency Preparedness - Always ready", 
			28 => "Ascender - Climbing equipment master", 
			29 => "Plunderer - Loot collector", 
			30 => "Bookworm - Knowledge seeker", 
			31 => "Endurance - Stamina specialist", 
			_ => "Unknown achievement", 
		};
	}
}
