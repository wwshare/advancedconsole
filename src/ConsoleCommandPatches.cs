using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HarmonyLib;
using Zorro.Core.CLI;

namespace AdvancedConsole;

/// <summary>
/// 命令解析增强: 支持省略域名的裸方法名、大小写不敏感匹配。
/// 例: 输入 "addhunger 1" 会自动解析为 "StatusCommands.AddHunger 1"。
/// </summary>
[HarmonyPatch]
public static class ConsoleCommandPatches
{
	private static readonly Type _consoleHandlerType = typeof(ConsoleHandler);

	public static void Initialize(Harmony harmony)
	{
		try
		{
			MethodInfo method = _consoleHandlerType.GetMethod("ProcessCommand", BindingFlags.Static | BindingFlags.Public);
			MethodInfo prefix = typeof(ConsoleCommandPatches).GetMethod("ProcessCommandPrefix", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null && prefix != null)
			{
				harmony.Patch(method, new HarmonyMethod(prefix));
				Plugin.Log.LogInfo((object)"ConsoleCommandPatches: 命令解析增强已启用");
			}
			else
			{
				Plugin.Log.LogWarning((object)"ConsoleCommandPatches: 未找到 ProcessCommand 方法");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("ConsoleCommandPatches 初始化失败: " + ex.Message));
		}
	}

	private static bool ProcessCommandPrefix(string command, ref Task<bool> __result)
	{
		try
		{
			if (string.IsNullOrEmpty(command))
			{
				return true;
			}
			string text = command.Trim();
			string firstToken = text;
			string args = "";
			int num = text.IndexOf(' ');
			if (num > 0)
			{
				firstToken = text.Substring(0, num);
				args = text.Substring(num + 1).Trim();
			}
			List<ConsoleCommand> list = ConsoleHandler.ConsoleCommands;
			if (list == null)
			{
				return true;
			}
			string rewritten = null;
			if (firstToken.Contains('.'))
			{
				int num2 = firstToken.IndexOf('.');
				string domain = firstToken.Substring(0, num2);
				string cmd = firstToken.Substring(num2 + 1);
				List<ConsoleCommand> list2 = list.Where((ConsoleCommand c) => string.Equals(c.DomainName, domain, StringComparison.OrdinalIgnoreCase) && string.Equals(c.Command, cmd, StringComparison.OrdinalIgnoreCase)).ToList();
				if (list2.Count == 1 && (list2[0].DomainName != domain || list2[0].Command != cmd))
				{
					rewritten = list2[0].DomainName + "." + list2[0].Command;
				}
			}
			else
			{
				List<ConsoleCommand> list3 = list.Where((ConsoleCommand c) => string.Equals(c.Command, firstToken, StringComparison.OrdinalIgnoreCase)).ToList();
				if (list3.Count == 0)
				{
					list3 = list.Where((ConsoleCommand c) => c.Command.StartsWith(firstToken, StringComparison.OrdinalIgnoreCase)).ToList();
				}
				if (list3.Count == 1)
				{
					rewritten = list3[0].DomainName + "." + list3[0].Command;
				}
				else if (list3.Count > 1)
				{
					Plugin.Log.LogWarning((object)$"命令 '{firstToken}' 存在多个匹配, 请使用完整域名 (如 {list3[0].DomainName}.{list3[0].Command})");
				}
			}
			if (rewritten != null)
			{
				string full = rewritten + ((args.Length > 0) ? (" " + args) : "");
				__result = ConsoleHandler.ProcessCommand(full);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("ProcessCommandPrefix 错误: " + ex.Message));
			return true;
		}
	}
}
