using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

public static class LobbyCommands
{
	private static readonly Dictionary<string, string> AvailableScenes = new Dictionary<string, string>
	{
		{ "pretitle", "Pretitle" },
		{ "title", "Title" },
		{ "mainmenu", "MainMenu" },
		{ "wilisland", "WilIsland" },
		{ "airport", "Airport" },
		{ "level0", "Level0" },
		{ "level1", "Level1" },
		{ "level2", "Level2" },
		{ "level3", "Level3" },
		{ "level4", "Level4" },
		{ "level5", "Level5" },
		{ "level6", "Level6" },
		{ "level7", "Level7" },
		{ "level8", "Level8" },
		{ "level9", "Level9" },
		{ "level10", "Level10" },
		{ "level11", "Level11" },
		{ "level12", "Level12" },
		{ "level13", "Level13" },
		{ "level14", "Level14" }
	};

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void SetMap(string sceneName)
	{
		if (!PhotonNetwork.InLobby && !PhotonNetwork.InRoom)
		{
			Debug.LogWarning((object)"setmap: 您必须处于大厅或房间中才能更改地图!");
			return;
		}
		if (string.IsNullOrEmpty(sceneName))
		{
			ShowAvailableMaps();
			return;
		}
		string key = sceneName.ToLower();
		if (!AvailableScenes.TryGetValue(key, out string value))
		{
			Debug.LogWarning((object)("setmap: 未知场景 '" + sceneName + "'。可用场景:"));
			ShowAvailableMaps();
			return;
		}
		try
		{
			Debug.Log((object)("setmap: 正在尝试加载场景 '" + value + "'..."));
			if (MapLoadingFix.LoadMap(value))
			{
				Debug.Log((object)("setmap: 正在加载场景 '" + value + "'... 请稍候。"));
				Plugin.Log.LogInfo((object)("Map changed to: " + value));
				return;
			}
			Debug.LogWarning((object)("setmap: 无法开始加载地图 '" + value + "'"));
			Debug.Log((object)"setmap: 正在尝试备用加载方式...");
			if (MapControlPatches.ForceLoadMapDirect(value))
			{
				Debug.Log((object)("setmap: 已开始为 '" + value + "'"));
				Plugin.Log.LogInfo((object)("Map changed to: " + value + " (备用方式)"));
			}
			else
			{
				Debug.LogError((object)("setmap: 所有加载方式均无法加载 '" + value + "'"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("setmap: 更改地图失败 - " + ex.Message));
		}
	}

	[ConsoleCommand]
	public static void ListMaps()
	{
		ShowAvailableMaps();
	}

	private static void ShowAvailableMaps()
	{
		Debug.Log((object)"可用地图:");
		foreach (KeyValuePair<string, string> availableScene in AvailableScenes)
		{
			Debug.Log((object)("  " + availableScene.Key + " -> " + availableScene.Value));
		}
		Debug.Log((object)"用法: setmap <scene_name>");
	}

	public static Dictionary<string, string> GetAvailableScenes()
	{
		return new Dictionary<string, string>(AvailableScenes);
	}

	[MasterClientOnly(null)]
	public static bool ForceChangeMap(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogWarning((object)"请指定场景名称");
			return false;
		}
		string key = sceneName.ToLower();
		if (!AvailableScenes.TryGetValue(key, out string value))
		{
			Debug.LogWarning((object)("未知场景 '" + sceneName + "'"));
			return false;
		}
		try
		{
			Debug.Log((object)("正在强制更改地图为 '" + value + "'..."));
			if (MapLoadingFix.LoadMap(value))
			{
				Debug.Log((object)("正在切换地图到 '" + value + "'... 请稍候。"));
				Plugin.Log.LogInfo((object)("已发起强制地图更改: " + value));
				return true;
			}
			Debug.Log((object)"正在尝试异步地图加载方式...");
			if (MapLoadingFix.LoadMapAsync(value))
			{
				Debug.Log((object)("已开始异步加载地图 '" + value + "'..."));
				Plugin.Log.LogInfo((object)("已发起异步强制地图更改: " + value));
				return true;
			}
			Debug.Log((object)"正在尝试旧版地图加载方式...");
			if (MapControlPatches.ForceLoadMapDirect(value) || MapControlPatches.ForceLoadMap(value))
			{
				Debug.Log((object)("已开始为地图 '" + value + "'..."));
				Plugin.Log.LogInfo((object)("已发起旧版强制地图更改: " + value));
				return true;
			}
			Debug.LogError((object)("所有地图加载方式均无法加载 '" + value + "'"));
			return false;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("强制更改地图失败 - " + ex.Message));
			return false;
		}
	}

	[ConsoleCommand]
	public static void GetCurrentMap()
	{
		if (PhotonNetwork.InRoom)
		{
			if (((Dictionary<object, object>)(object)((RoomInfo)PhotonNetwork.CurrentRoom).CustomProperties).TryGetValue((object)"scene", out object value))
			{
				Debug.Log((object)$"当前地图: {value}");
			}
			else
			{
				Debug.Log((object)"当前地图: 未设置或未知");
			}
		}
		else if (PhotonNetwork.InLobby)
		{
			Debug.Log((object)"您在大厅中, 当前未加载任何地图。");
		}
		else
		{
			Debug.Log((object)"未连接到任何房间或大厅。");
		}
	}

	[ConsoleCommand]
	[MasterClientOnly(null)]
	public static void ForceLoadScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogWarning((object)"forceloadscene: 请指定场景名称");
			return;
		}
		string key = sceneName.ToLower();
		if (!AvailableScenes.TryGetValue(key, out string value))
		{
			Debug.LogWarning((object)("forceloadscene: 未知场景 '" + sceneName + "'"));
			ShowAvailableMaps();
			return;
		}
		try
		{
			Debug.Log((object)("forceloadscene: 正在强制加载场景 '" + value + "'..."));
			if (MapLoadingFix.LoadMap(value))
			{
				Debug.Log((object)("forceloadscene: 正在加载场景 '" + value + "'... 请稍候。"));
				Plugin.Log.LogInfo((object)("正在强制加载场景: " + value));
				return;
			}
			Debug.LogWarning((object)"forceloadscene: 无法使用主方式开始地图加载");
			Debug.Log((object)"forceloadscene: 正在尝试异步加载方式...");
			if (MapLoadingFix.LoadMapAsync(value))
			{
				Debug.Log((object)("forceloadscene: 已开始异步加载 '" + value + "'... 请稍候。"));
				Plugin.Log.LogInfo((object)("正在异步强制加载场景: " + value));
				return;
			}
			Debug.Log((object)"forceloadscene: 正在尝试旧版加载方式...");
			if (MapControlPatches.ForceLoadMapDirect(value) || MapControlPatches.ForceLoadMap(value))
			{
				Debug.Log((object)("forceloadscene: 已开始为 '" + value + "'"));
			}
			else
			{
				Debug.LogError((object)("forceloadscene: 所有加载方式均无法加载 '" + value + "'"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("forceloadscene: 加载场景失败 - " + ex.Message));
		}
	}
}
