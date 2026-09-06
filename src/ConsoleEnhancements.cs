using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UIElements;
using Zorro.Core.CLI;

namespace AdvancedConsole;

/// <summary>
/// 控制台 UI 增强: 日志行右键复制、输入框光标更明显。
/// 说明: 命令补全(Tab)由游戏原生的 ConsolePage 已实现, 无需额外补丁。
/// </summary>
[HarmonyPatch]
public static class ConsoleEnhancements
{
	private static readonly HashSet<ConsoleLogElement> _patchedLogElements = new HashSet<ConsoleLogElement>();

	public static void Initialize(Harmony harmony)
	{
		try
		{
			MethodInfo method = typeof(ConsoleLogElement).GetMethod("SetData", BindingFlags.Instance | BindingFlags.Public);
			MethodInfo method2 = typeof(ConsoleEnhancements).GetMethod("SetDataPostfix", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null && method2 != null)
			{
				harmony.Patch(method, (HarmonyMethod)null, new HarmonyMethod(method2));
				Plugin.Log.LogInfo((object)"ConsoleEnhancements: 日志右键复制已启用");
			}
			MethodInfo method3 = typeof(ConsolePage).GetMethod("ConstructConsole", BindingFlags.Instance | BindingFlags.NonPublic);
			MethodInfo method4 = typeof(ConsoleEnhancements).GetMethod("ConstructConsolePostfix", BindingFlags.Static | BindingFlags.NonPublic);
			if (method3 != null && method4 != null)
			{
				harmony.Patch(method3, (HarmonyMethod)null, new HarmonyMethod(method4));
				Plugin.Log.LogInfo((object)"ConsoleEnhancements: 光标样式已应用");
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("ConsoleEnhancements 初始化失败: " + ex.Message));
		}
	}

	private static void SetDataPostfix(ConsoleLogElement __instance)
	{
		try
		{
			if (_patchedLogElements.Contains(__instance))
			{
				return;
			}
			_patchedLogElements.Add(__instance);
			__instance.RegisterCallback<ContextClickEvent>(delegate(ContextClickEvent evt)
			{
				ConsoleLogEntry currentEntry = GetCurrentEntry(__instance);
				CopyToClipboard(currentEntry.Log);
			});
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("日志右键复制补丁失败: " + ex.Message));
		}
	}

	private static ConsoleLogEntry GetCurrentEntry(ConsoleLogElement element)
	{
		try
		{
			FieldInfo field = typeof(ConsoleLogElement).GetField("_consoleLogEntry", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field != null)
			{
				object value = field.GetValue(element);
				if (value is ConsoleLogEntry entry)
				{
					return entry;
				}
			}
		}
		catch
		{
		}
		return default(ConsoleLogEntry);
	}

	private static void ConstructConsolePostfix(ConsolePage __instance)
	{
		try
		{
			FieldInfo field = typeof(ConsolePage).GetField("m_textField", BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				return;
			}
			TextField val = field.GetValue(__instance) as TextField;
			if (val != null)
			{
				val.style.color = new StyleColor(new Color(1f, 1f, 1f));
				VisualElement inputElement = UQueryExtensions.Q<VisualElement>(val, (string)null, "unity-text-field__input");
				if (inputElement != null)
				{
					inputElement.style.color = new StyleColor(new Color(1f, 1f, 1f));
					inputElement.style.backgroundColor = new StyleColor(new Color(0.06f, 0.06f, 0.08f));
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("光标样式补丁失败: " + ex.Message));
		}
	}

	private static void CopyToClipboard(string text)
	{
		try
		{
			if (!string.IsNullOrEmpty(text))
			{
				GUIUtility.systemCopyBuffer = text;
				Debug.Log((object)("已复制到剪贴板"));
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("复制到剪贴板失败: " + ex.Message));
		}
	}
}
