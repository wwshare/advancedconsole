using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

[BepInProcess("PEAK.exe")]
[BepInPlugin("com.github.wwshare.advancedconsole", "Advanced Peak Console", "1.1.9")]
public class Plugin : BaseUnityPlugin
{
	private Type? _consoleHandlerType;

	private Type? _debugUIType;

	private object? _debugUIHandler;

	private bool _consoleReady;

	private bool _isShowing;

	public const string Id = "com.github.wwshare.advancedconsole";

	internal static ManualLogSource Log { get; private set; }

	internal static Plugin? Instance { get; private set; }

	public static string Name => "Advanced Peak Console";

	public static string Version => "1.1.9";

	private void Awake()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		Instance = this;
		Log = Logger;
		Log.LogInfo((object)"Advanced Console Plugin loaded!");
		PluginConfig.Initialize();
		// 诊断: 最小化构建, 只保留控制台注入+命令扫描, 定位原生崩溃来源
		// Harmony val = new Harmony("com.github.wwshare.advancedconsole");
		// val.PatchAll(Assembly.GetExecutingAssembly());
		// HotkeyPatches.InitializePatches(val);
		// MasterClientCommandInterceptor.Initialize();
		// ConsoleLogPatch.Initialize();
		Log.LogInfo((object)"Harmony patches skipped (diagnostic mode)!");
		((MonoBehaviour)this).StartCoroutine(DoInject());
		// 诊断: 疑似在角色出生时原生崩溃, 暂时禁用
		// ((MonoBehaviour)this).StartCoroutine(InitializeAfflictionsRPC());
	}

	private IEnumerator DoInject()
	{
		for (int i = 0; i < 50; i++)
		{
			_consoleHandlerType = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => SafeGetTypes(a)).FirstOrDefault((Type t) => t.FullName == "Zorro.Core.CLI.ConsoleHandler");
			if (_consoleHandlerType != null)
			{
				break;
			}
			yield return (object)new WaitForSeconds(0.1f);
		}
		if (_consoleHandlerType == null)
		{
			Log.LogError((object)"Failed to find ConsoleHandler type!");
			yield break;
		}
		MethodInfo method = _consoleHandlerType.GetMethod("ScanForConsoleCommands", BindingFlags.Static | BindingFlags.Public);
		MethodInfo method2 = _consoleHandlerType.GetMethod("ScanForTypeParsers", BindingFlags.Static | BindingFlags.Public);
		MethodInfo method3 = _consoleHandlerType.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public);
		object obj = null;
		object obj2 = null;
		try
		{
			obj = method?.Invoke(null, null);
			obj2 = method2?.Invoke(null, null);
		}
		catch (Exception arg2)
		{
			Log.LogWarning((object)$"Failed to scan console commands/parsers: {arg2}");
		}
		try
		{
			if (obj2 != null)
			{
				Type type = obj2.GetType();
				Type type2 = type.GetGenericArguments()[1];
				MethodInfo method4 = type.GetMethod("set_Item", new Type[2]
				{
					typeof(Type),
					type2
				});
				object obj3 = new PlayerCLIParser();
				Type typeFromHandle = typeof(PhotonPlayer);
				method4?.Invoke(obj2, new object[2] { typeFromHandle, obj3 });
				object obj4 = new AchievementTypeCLIParser();
				Type typeFromHandle2 = typeof(ACHIEVEMENTTYPE);
				method4?.Invoke(obj2, new object[2] { typeFromHandle2, obj4 });
				Log.LogInfo((object)"Successfully injected PlayerCLIParser and AchievementTypeCLIParser!");
			}
		}
		catch (Exception arg)
		{
			Log.LogWarning((object)$"Failed to inject CLI parsers: {arg}");
		}
		try
		{
			method3?.Invoke(null, new object[2] { obj, obj2 });
			try
			{
				System.Collections.ICollection commandList = _consoleHandlerType.GetProperty("ConsoleCommands", BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as System.Collections.ICollection;
				Log.LogInfo((object)$"Console command registration complete: {(commandList?.Count ?? -1)} commands available.");
			}
			catch
			{
			}
		}
		catch (Exception arg3)
		{
			Log.LogWarning((object)$"Failed to initialize console handler: {arg3}");
		}
		yield return null;
		yield return null;
		_debugUIType = _consoleHandlerType.Assembly.GetType("Zorro.Core.CLI.DebugUIHandler");
		if (_debugUIType == null)
		{
			Log.LogError((object)"Failed to find DebugUIHandler type!");
			yield break;
		}
		_debugUIHandler = UnityEngine.Object.FindObjectsByType<MonoBehaviour>((FindObjectsSortMode)0).FirstOrDefault((MonoBehaviour mb) => ((object)mb).GetType() == _debugUIType);
		if (_debugUIHandler != null)
		{
			_debugUIType.GetMethod("Hide", BindingFlags.Instance | BindingFlags.Public)?.Invoke(_debugUIHandler, null);
			_consoleReady = true;
			Log.LogInfo((object)"Console injection completed successfully!");
			((MonoBehaviour)this).StartCoroutine(InitializeConsoleUI());
			((MonoBehaviour)this).StartCoroutine(InitializeModDetection());
		}
		else
		{
			Log.LogError((object)"Failed to find DebugUIHandler instance!");
		}
		// 诊断: 疑似在角色出生时原生崩溃, 暂时禁用 Photon 回调组件
		// GameObject val = new GameObject("AdvancedConsole_PhotonCallbacks");
		// val.AddComponent<PhotonCallbacks>();
		// UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)(object)val);
	}

	private void Update()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		KeyCode consoleToggleKey = PluginConfig.ConsoleToggleKey;
		if (_consoleReady && (Input.GetKeyDown(consoleToggleKey) || Input.GetKeyDown(consoleToggleKey)))
		{
			string name = (_isShowing ? "Hide" : "Show");
			(_debugUIType?.GetMethod(name, BindingFlags.Instance | BindingFlags.Public))?.Invoke(_debugUIHandler, null);
			_isShowing = !_isShowing;
			Log.LogInfo((object)string.Format("Console toggled: {0} using key {1}", _isShowing ? "Shown" : "Hidden", consoleToggleKey));
		}
		KeyCode consoleDPIResetKey = PluginConfig.ConsoleDPIResetKey;
		if (_consoleReady && (Input.GetKeyDown(consoleDPIResetKey) || Input.GetKeyDown(consoleDPIResetKey)))
		{
			ResetConsoleDPI();
		}
	}

	private void ResetConsoleDPI()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Type type = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => SafeGetTypes(a)).FirstOrDefault((Type t) => t.Name == "ConsoleSettings");
			if (type != null)
			{
				MethodInfo method = type.GetMethod("SetDPI", BindingFlags.Static | BindingFlags.Public);
				if (method != null)
				{
					method.Invoke(null, new object[1] { 96f });
					Log.LogInfo((object)$"Console DPI reset to default (96) via {PluginConfig.ConsoleDPIResetKey} key!");
					Debug.Log((object)$"[Advanced Console] Console DPI reset to default (96) using {PluginConfig.ConsoleDPIResetKey}!");
				}
				else
				{
					Log.LogWarning((object)"SetDPI method not found in ConsoleSettings");
				}
			}
			else
			{
				Log.LogWarning((object)"ConsoleSettings type not found");
			}
		}
		catch (Exception ex)
		{
			Log.LogError((object)("Failed to reset console DPI: " + ex.Message));
		}
	}

	private static Type[] SafeGetTypes(Assembly assembly)
	{
		try
		{
			return assembly.GetTypes();
		}
		catch (ReflectionTypeLoadException ex)
		{
			return ex.Types.Where((Type t) => t != null).ToArray();
		}
		catch
		{
			return Array.Empty<Type>();
		}
	}

	private IEnumerator InitializeAfflictionsRPC()
	{
		yield return (object)new WaitForSeconds(2f);
		while (true)
		{
			foreach (Character allCharacter in Character.AllCharacters)
			{
				if ((UnityEngine.Object)(object)allCharacter != (UnityEngine.Object)null && (UnityEngine.Object)(object)((Component)allCharacter).GetComponent<CharacterAfflictionsRPC>() == (UnityEngine.Object)null && (UnityEngine.Object)(object)((Component)allCharacter).GetComponent<CharacterAfflictions>() != (UnityEngine.Object)null)
				{
					((Component)allCharacter).gameObject.AddComponent<CharacterAfflictionsRPC>();
					Log.LogInfo((object)("Added CharacterAfflictionsRPC to " + allCharacter.characterName));
				}
			}
			yield return (object)new WaitForSeconds(5f);
		}
	}

	private IEnumerator InitializeConsoleUI()
	{
		yield return (object)new WaitForSeconds(1f);
		try
		{
			ConsoleIntegration.RegisterAdvancedConsolePage();
			Log.LogInfo((object)"Advanced Console UI initialized successfully!");
		}
		catch (Exception ex)
		{
			Log.LogError((object)("Failed to initialize Console UI: " + ex.Message));
		}
	}

	private IEnumerator InitializeModDetection()
	{
		while (!PhotonNetwork.IsConnected)
		{
			yield return (object)new WaitForSeconds(0.5f);
		}
		ModDetection.BroadcastModInfo();
		Log.LogInfo((object)"Mod detection initialized and info broadcasted!");
	}
}
