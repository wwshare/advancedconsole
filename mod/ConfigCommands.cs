using System;
using System.Diagnostics;
using System.IO;
using BepInEx;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class ConfigCommands
{
	[ConsoleCommand]
	public static void ShowConfig()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		UnityEngine.Debug.Log((object)"=== Advanced Console 配置 ===");
		UnityEngine.Debug.Log((object)PluginConfig.GetConfigInfo());
		UnityEngine.Debug.Log((object)"=== 当前按键绑定 ===");
		UnityEngine.Debug.Log((object)$"控制台开关: {PluginConfig.ConsoleToggleKey} (按下以显示/隐藏控制台)");
		UnityEngine.Debug.Log((object)$"控制台 DPI 重置: {PluginConfig.ConsoleDPIResetKey} (按下以重置控制台缩放)");
		UnityEngine.Debug.Log((object)"=== 配置帮助 ===");
		UnityEngine.Debug.Log((object)"使用 'ConfigCommands.ShowKeyCodes' 查看可用按键");
		UnityEngine.Debug.Log((object)"使用 'ConfigCommands.OpenConfigFolder' 打开配置文件所在位置");
		Plugin.Log.LogInfo((object)"已在控制台显示配置");
	}

	[ConsoleCommand]
	public static void SetConsoleToggleKey(KeyCode key)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected I4, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected I4, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected I4, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			UnityEngine.Debug.Log((object)$"setconsoletogglekey: 将控制台开关按键更改为 {key}:");
			UnityEngine.Debug.Log((object)"1. 关闭游戏");
			UnityEngine.Debug.Log((object)"2. 打开配置文件: BepInEx/config/wwshare.AdvancedConsole.cfg");
			UnityEngine.Debug.Log((object)$"3. 找到 [Keybinds] 部分并将 ConsoleToggleKey 更改为 {(int)key}");
			UnityEngine.Debug.Log((object)"4. 保存文件并重启游戏");
			UnityEngine.Debug.Log((object)$"当前按键: {PluginConfig.ConsoleToggleKey} (代码: {(int)PluginConfig.ConsoleToggleKey})");
			UnityEngine.Debug.Log((object)$"目标按键: {key} (代码: {(int)key})");
			Plugin.Log.LogInfo((object)$"已请求更改控制台开关按键: {PluginConfig.ConsoleToggleKey} -> {key}");
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("setconsoletogglekey: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SetConsoleDPIResetKey(KeyCode key)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected I4, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected I4, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected I4, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			UnityEngine.Debug.Log((object)$"setconsoledpiresetkey: 将控制台 DPI 重置按键更改为 {key}:");
			UnityEngine.Debug.Log((object)"1. 关闭游戏");
			UnityEngine.Debug.Log((object)"2. 打开配置文件: BepInEx/config/wwshare.AdvancedConsole.cfg");
			UnityEngine.Debug.Log((object)$"3. 找到 [Keybinds] 部分并将 ConsoleDPIResetKey 更改为 {(int)key}");
			UnityEngine.Debug.Log((object)"4. 保存文件并重启游戏");
			UnityEngine.Debug.Log((object)$"当前按键: {PluginConfig.ConsoleDPIResetKey} (代码: {(int)PluginConfig.ConsoleDPIResetKey})");
			UnityEngine.Debug.Log((object)$"目标按键: {key} (代码: {(int)key})");
			Plugin.Log.LogInfo((object)$"已请求更改控制台 DPI 重置按键: {PluginConfig.ConsoleDPIResetKey} -> {key}");
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("setconsoledpiresetkey: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ReloadConfig()
	{
		try
		{
			PluginConfig.Reload();
			UnityEngine.Debug.Log((object)"reloadconfig: 已从磁盘重新加载配置");
			UnityEngine.Debug.Log((object)"注意: 按键绑定需要重启游戏才能生效");
			ShowConfig();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("reloadconfig: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void SaveConfig()
	{
		try
		{
			PluginConfig.Save();
			UnityEngine.Debug.Log((object)"saveconfig: 配置已保存到磁盘");
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("saveconfig: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ShowKeyCodes()
	{
		UnityEngine.Debug.Log((object)"=== 常用绑定按键代码 ===");
		UnityEngine.Debug.Log((object)"功能键:");
		UnityEngine.Debug.Log((object)string.Format("  F1 = {0}, F2 = {1}, F3 = {2}, F4 = {3}", new object[4] { 282, 283, 284, 285 }));
		UnityEngine.Debug.Log((object)string.Format("  F5 = {0}, F6 = {1}, F7 = {2}, F8 = {3}", new object[4] { 286, 287, 288, 289 }));
		UnityEngine.Debug.Log((object)string.Format("  F9 = {0}, F10 = {1}, F11 = {2}, F12 = {3}", new object[4] { 290, 291, 292, 293 }));
		UnityEngine.Debug.Log((object)"特殊按键:");
		UnityEngine.Debug.Log((object)$"  Tilde (~) = {96}, Tab = {9}");
		UnityEngine.Debug.Log((object)$"  Insert = {277}, Delete = {127}");
		UnityEngine.Debug.Log((object)$"  Home = {278}, End = {279}");
		UnityEngine.Debug.Log((object)$"  Page Up = {280}, Page Down = {281}");
		UnityEngine.Debug.Log((object)"修饰键:");
		UnityEngine.Debug.Log((object)$"  Left Ctrl = {306}, Right Ctrl = {305}");
		UnityEngine.Debug.Log((object)$"  Left Alt = {308}, Right Alt = {307}");
		UnityEngine.Debug.Log((object)$"  Left Shift = {304}, Right Shift = {303}");
		UnityEngine.Debug.Log((object)"用法: ConfigCommands.SetConsoleToggleKey <KeyCode>");
		UnityEngine.Debug.Log((object)"示例: ConfigCommands.SetConsoleToggleKey BackQuote");
	}

	[ConsoleCommand]
	public static void OpenConfigFolder()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Invalid comparison between Unknown and I4
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Invalid comparison between Unknown and I4
		try
		{
			string text = Path.Combine(Paths.ConfigPath, "wwshare.AdvancedConsole.cfg");
			string text2 = Path.GetDirectoryName(text) ?? Paths.ConfigPath;
			UnityEngine.Debug.Log((object)"openconfigfolder: 配置文件位置:");
			UnityEngine.Debug.Log((object)("  " + text));
			UnityEngine.Debug.Log((object)("  目录: " + text2));
			if ((int)Application.platform == 2 || (int)Application.platform == 7)
			{
				try
				{
					Process.Start("explorer.exe", text2);
					UnityEngine.Debug.Log((object)"openconfigfolder: 已在 Windows 资源管理器中打开配置文件夹");
				}
				catch
				{
					UnityEngine.Debug.Log((object)"openconfigfolder: 无法自动打开文件夹。请手动导航到上述路径。");
				}
			}
			else
			{
				UnityEngine.Debug.Log((object)"openconfigfolder: 仅在 Windows 上支持自动打开文件夹。请手动导航到上述路径。");
			}
			Plugin.Log.LogInfo((object)("已请求访问配置文件夹: " + text2));
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("openconfigfolder: 错误 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void CreateExampleConfig()
	{
		try
		{
			string text = Path.Combine(Paths.ConfigPath, "AdvancedConsole_Example.cfg");
			string contents = "# Advanced Console Configuration Example\r\n# This file shows all available settings with descriptions\r\n# Copy values to wwshare.AdvancedConsole.cfg to apply changes\r\n\r\n[Keybinds]\r\n\r\n# Key to toggle console visibility\r\n# Popular choices: F1=282, F2=283, F3=284, F4=285, F5=286\r\n# BackQuote=96 (tilde ~), Tab=9, Insert=277, Delete=127\r\nConsoleToggleKey = 282\r\n\r\n# Key to reset console DPI to default\r\n# Useful if console becomes too small/large due to scaling issues\r\nConsoleDPIResetKey = 283\r\n\r\n[General]\r\n\r\n# Enable extended debug logging for troubleshooting\r\nEnableDebugLogs = true\r\n\r\n# Enable mod detection system to identify other players' mods\r\nEnableModDetection = true\r\n\r\n[ServerInfo]\r\n\r\n# Automatically refresh Server Info page\r\nAutoRefresh = true\r\n\r\n# How often to refresh Server Info in seconds (1-60)\r\nRefreshInterval = 5.0\r\n\r\n[Distance]\r\n\r\n# Maximum raycast distance for teleport and spawn commands (100-10000 meters)\r\n# Used when teleporting to camera target or spawning items at camera position\r\nMaxRaycastDistance = 5000.0\r\n\r\n# Fallback distance when raycast doesn't hit anything (5-100 meters)\r\n# Used when camera raycast goes into empty space\r\nFallbackDistance = 25.0\r\n\r\n# Key Code Reference:\r\n# A-Z = 97-122, 0-9 = 48-57\r\n# F1-F12 = 282-293\r\n# Arrow Keys: Up=273, Down=274, Right=275, Left=276\r\n# Space=32, Enter=13, Escape=27\r\n# For complete list, use: ConfigCommands.ShowKeyCodes\r\n";
			File.WriteAllText(text, contents);
			UnityEngine.Debug.Log((object)"createexampleconfig: 已创建示例配置文件:");
			UnityEngine.Debug.Log((object)("  " + text));
			UnityEngine.Debug.Log((object)"使用此文件作为配置选项的参考");
			Plugin.Log.LogInfo((object)("已创建示例配置: " + text));
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError((object)("createexampleconfig: 错误 - " + ex.Message));
		}
	}
}
