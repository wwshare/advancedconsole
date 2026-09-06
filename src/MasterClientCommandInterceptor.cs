using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Zorro.Core.CLI;

namespace AdvancedConsole;

[HarmonyPatch]
public static class MasterClientCommandInterceptor
{
	private static Harmony? _harmonyInstance;

	private static bool _isInitialized;

	public static void Initialize()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (_isInitialized)
		{
			return;
		}
		try
		{
			_harmonyInstance = new Harmony("AdvancedConsole.MasterClientInterceptor");
			PatchAllMasterClientOnlyMethods();
			_isInitialized = true;
			Plugin.Log.LogInfo((object)"MasterClientCommandInterceptor initialized successfully");
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to initialize MasterClientCommandInterceptor: " + ex.Message));
		}
	}

	private static void PatchAllMasterClientOnlyMethods()
	{
		try
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type[] types = executingAssembly.GetTypes();
			Type[] array = types;
			foreach (Type type in array)
			{
				MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				MethodInfo[] array2 = methods;
				foreach (MethodInfo methodInfo in array2)
				{
					MasterClientOnlyAttribute customAttribute = methodInfo.GetCustomAttribute<MasterClientOnlyAttribute>();
					ConsoleCommandAttribute customAttribute2 = ((MemberInfo)methodInfo).GetCustomAttribute<ConsoleCommandAttribute>();
					if (customAttribute != null && customAttribute2 != null)
					{
						CreateDynamicPatch(methodInfo, customAttribute);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error patching MasterClientOnly methods: " + ex.Message));
		}
	}

	private static void CreateDynamicPatch(MethodInfo originalMethod, MasterClientOnlyAttribute attribute)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		try
		{
			MethodInfo method = typeof(MasterClientCommandInterceptor).GetMethod("MasterClientOnlyPrefix");
			HarmonyMethod val = new HarmonyMethod(method);
			Harmony? harmonyInstance = _harmonyInstance;
			if (harmonyInstance != null)
			{
				harmonyInstance.Patch((MethodBase)originalMethod, val, (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
			}
			Plugin.Log.LogDebug((object)("Patched method: " + originalMethod?.DeclaringType?.Name + "." + originalMethod?.Name));
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Failed to patch method " + (originalMethod?.Name ?? "Unknown") + ": " + ex.Message));
		}
	}

	public static bool MasterClientOnlyPrefix(MethodInfo __originalMethod)
	{
		try
		{
			MasterClientOnlyAttribute customAttribute = __originalMethod.GetCustomAttribute<MasterClientOnlyAttribute>();
			if (customAttribute == null)
			{
				return true;
			}
			string text = customAttribute.CommandName ?? __originalMethod.Name;
			if (MasterClientUtils.IsMasterClient(text))
			{
				return true;
			}
			if (customAttribute.WarnOnly)
			{
				Debug.LogWarning((object)("Warning: You are not the Master Client. Command '" + text + "' may not work properly."));
				Plugin.Log.LogWarning((object)("Non-master client attempted to execute: " + text));
				return true;
			}
			Debug.LogWarning((object)("Only Master Client can execute '" + text + "' command."));
			Plugin.Log.LogWarning((object)("Blocked non-master client from executing: " + text));
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("Error in MasterClientOnlyPrefix: " + ex.Message));
			return true;
		}
	}

	public static void Cleanup()
	{
		if (_harmonyInstance != null)
		{
			_harmonyInstance.UnpatchSelf();
			_isInitialized = false;
			Plugin.Log.LogInfo((object)"MasterClientCommandInterceptor cleaned up");
		}
	}
}
