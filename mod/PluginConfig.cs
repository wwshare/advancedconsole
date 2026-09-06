using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace AdvancedConsole;

public static class PluginConfig
{
	private static ConfigFile? _configFile;

	private static ConfigEntry<KeyCode>? _consoleToggleKey;

	private static ConfigEntry<KeyCode>? _consoleDPIResetKey;

	private static ConfigEntry<bool>? _enableDebugLogs;

	private static ConfigEntry<bool>? _enableModDetection;

	private static ConfigEntry<bool>? _autoRefreshServerInfo;

	private static ConfigEntry<float>? _serverInfoRefreshInterval;

	private static ConfigEntry<float>? _maxRaycastDistance;

	private static ConfigEntry<float>? _fallbackDistance;

	public static KeyCode ConsoleToggleKey
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return (KeyCode)(((KeyCode?)_consoleToggleKey?.Value) ?? (KeyCode)282);
		}
	}

	public static KeyCode ConsoleDPIResetKey
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return (KeyCode)(((KeyCode?)_consoleDPIResetKey?.Value) ?? (KeyCode)283);
		}
	}

	public static bool EnableDebugLogs => _enableDebugLogs?.Value ?? true;

	public static bool EnableModDetection => _enableModDetection?.Value ?? true;

	public static bool AutoRefreshServerInfo => _autoRefreshServerInfo?.Value ?? true;

	public static float ServerInfoRefreshInterval => _serverInfoRefreshInterval?.Value ?? 5f;

	public static float MaxRaycastDistance => _maxRaycastDistance?.Value ?? 5000f;

	public static float FallbackDistance => _fallbackDistance?.Value ?? 25f;

	public static void Initialize()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			string text = Path.Combine(Paths.ConfigPath, "wwshare.AdvancedConsole.cfg");
			_configFile = new ConfigFile(text, true);
			_consoleToggleKey = _configFile.Bind<KeyCode>("Keybinds", "ConsoleToggleKey", (KeyCode)282, "Key to toggle the console visibility (Default: F1)");
			_consoleDPIResetKey = _configFile.Bind<KeyCode>("Keybinds", "ConsoleDPIResetKey", (KeyCode)283, "Key to reset console DPI to default value (Default: F2)");
			_enableDebugLogs = _configFile.Bind<bool>("General", "EnableDebugLogs", true, "Enable extended debug logging for troubleshooting");
			_enableModDetection = _configFile.Bind<bool>("General", "EnableModDetection", true, "Enable mod detection system to identify other players' mods");
			_autoRefreshServerInfo = _configFile.Bind<bool>("ServerInfo", "AutoRefresh", true, "Automatically refresh Server Info page every few seconds");
			_serverInfoRefreshInterval = _configFile.Bind<float>("ServerInfo", "RefreshInterval", 5f, "How often to refresh Server Info in seconds (1-60)");
			_maxRaycastDistance = _configFile.Bind<float>("Teleport", "MaxRaycastDistance", 5000f, "Maximum distance for raycast when teleporting/spawning (100-10000 meters)");
			_fallbackDistance = _configFile.Bind<float>("Teleport", "FallbackDistance", 25f, "Distance to teleport/spawn when raycast fails (5-100 meters)");
			if (_serverInfoRefreshInterval.Value < 1f)
			{
				_serverInfoRefreshInterval.Value = 1f;
			}
			if (_serverInfoRefreshInterval.Value > 60f)
			{
				_serverInfoRefreshInterval.Value = 60f;
			}
			if (_maxRaycastDistance.Value < 100f)
			{
				_maxRaycastDistance.Value = 100f;
			}
			if (_maxRaycastDistance.Value > 10000f)
			{
				_maxRaycastDistance.Value = 10000f;
			}
			if (_fallbackDistance.Value < 5f)
			{
				_fallbackDistance.Value = 5f;
			}
			if (_fallbackDistance.Value > 100f)
			{
				_fallbackDistance.Value = 100f;
			}
			Plugin.Log.LogInfo((object)"Configuration initialized successfully!");
			Plugin.Log.LogInfo((object)$"Console toggle key: {ConsoleToggleKey}");
			Plugin.Log.LogInfo((object)$"Console DPI reset key: {ConsoleDPIResetKey}");
			Plugin.Log.LogInfo((object)$"Max raycast distance: {MaxRaycastDistance}m");
			Plugin.Log.LogInfo((object)$"Fallback distance: {FallbackDistance}m");
			_consoleToggleKey.SettingChanged += OnKeyBindingChanged;
			_consoleDPIResetKey.SettingChanged += OnKeyBindingChanged;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to initialize configuration: " + ex.Message));
		}
	}

	private static void OnKeyBindingChanged(object sender, EventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Plugin.Log.LogInfo((object)"Key bindings updated:");
		Plugin.Log.LogInfo((object)$"Console toggle key: {ConsoleToggleKey}");
		Plugin.Log.LogInfo((object)$"Console DPI reset key: {ConsoleDPIResetKey}");
	}

	public static void Save()
	{
		try
		{
			ConfigFile? configFile = _configFile;
			if (configFile != null)
			{
				configFile.Save();
			}
			Plugin.Log.LogInfo((object)"Configuration saved successfully!");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to save configuration: " + ex.Message));
		}
	}

	public static void Reload()
	{
		try
		{
			ConfigFile? configFile = _configFile;
			if (configFile != null)
			{
				configFile.Reload();
			}
			Plugin.Log.LogInfo((object)"Configuration reloaded successfully!");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to reload configuration: " + ex.Message));
		}
	}

	public static string GetConfigInfo()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		return "Advanced Console Configuration:\n" + $"Console Toggle Key: {ConsoleToggleKey}\n" + $"Console DPI Reset Key: {ConsoleDPIResetKey}\n" + $"Debug Logs: {EnableDebugLogs}\n" + $"Mod Detection: {EnableModDetection}\n" + $"Auto Refresh Server Info: {AutoRefreshServerInfo}\n" + $"Server Info Refresh Interval: {ServerInfoRefreshInterval}s\n" + $"Max Raycast Distance: {MaxRaycastDistance}m\n" + $"Fallback Distance: {FallbackDistance}m";
	}
}
