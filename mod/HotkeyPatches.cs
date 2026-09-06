using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedConsole;

public class HotkeyPatches
{
	private static Dictionary<object, bool> _listeningForKey = new Dictionary<object, bool>();

	private static Dictionary<object, Button> _keyButtons = new Dictionary<object, Button>();

	public static void InitializePatches(Harmony harmony)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		try
		{
			Type type = FindConsoleHotkeyCellType();
			if (type == null)
			{
				Plugin.Log.LogError((object)"ConsoleHotkeyCell type not found!");
				return;
			}
			ConstructorInfo constructorInfo = type.GetConstructors().FirstOrDefault((ConstructorInfo c) => c.GetParameters().Length == 2);
			if (constructorInfo == null)
			{
				Plugin.Log.LogError((object)"ConsoleHotkeyCell constructor not found!");
				return;
			}
			MethodInfo method = typeof(HotkeyPatches).GetMethod("ConsoleHotkeyCellConstructorPostfix", BindingFlags.Static | BindingFlags.Public);
			harmony.Patch((MethodBase)constructorInfo, (HarmonyMethod)null, new HarmonyMethod(method), (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
			Plugin.Log.LogInfo((object)"ConsoleHotkeyCell constructor patched successfully!");
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error initializing hotkey patches: {arg}");
		}
	}

	private static Type? FindConsoleHotkeyCellType()
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		Assembly[] array = assemblies;
		foreach (Assembly assembly in array)
		{
			try
			{
				Type[] types = assembly.GetTypes();
				Type[] array2 = types;
				foreach (Type type in array2)
				{
					if (type.Name == "ConsoleHotkeyCell")
					{
						return type;
					}
				}
			}
			catch
			{
			}
		}
		return null;
	}

	public static void ConsoleHotkeyCellConstructorPostfix(object __instance, object hotkey, object debugUIHandler)
	{
		try
		{
			Type type = __instance.GetType();
			if (!(type.Name != "ConsoleHotkeyCell"))
			{
				VisualElement val = (VisualElement)((__instance is VisualElement) ? __instance : null);
				if (val != null)
				{
					val.Clear();
					CreateImprovedUI(__instance, hotkey, debugUIHandler);
				}
			}
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in ConsoleHotkeyCellConstructorPostfix: {arg}");
		}
	}

	private unsafe static void CreateImprovedUI(object cellInstance, object hotkey, object debugUIHandler)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			object obj = cellInstance;
			VisualElement val = (VisualElement)((obj is VisualElement) ? obj : null);
			if (val == null)
			{
				return;
			}
			VisualElement val2 = new VisualElement();
			val2.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
			val2.style.alignItems = new StyleEnum<Align>((Align)2);
			val2.style.marginBottom = new StyleLength(8f);
			val2.style.paddingTop = new StyleLength(8f);
			val2.style.paddingBottom = new StyleLength(8f);
			val2.style.paddingLeft = new StyleLength(5f);
			val2.style.paddingRight = new StyleLength(5f);
			Type type = hotkey.GetType();
			PropertyInfo property = type.GetProperty("KeyCode");
			PropertyInfo commandProperty = type.GetProperty("ConsoleCommand");
			if (property == null || commandProperty == null)
			{
				Plugin.Log.LogError((object)"Could not find KeyCode or ConsoleCommand properties");
				return;
			}
			KeyCode val3 = (KeyCode)property.GetValue(hotkey);
			string text = (string)commandProperty.GetValue(hotkey);
			Button keyButton = new Button();
			((TextElement)keyButton).text = (((int)val3 == 0) ? "Click to set key" : val3.ToString());
			((VisualElement)keyButton).style.width = new StyleLength(150f);
			((VisualElement)keyButton).style.height = new StyleLength(35f);
			((VisualElement)keyButton).style.marginRight = new StyleLength(10f);
			((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0.6f));
			((VisualElement)keyButton).style.color = new StyleColor(Color.white);
			((VisualElement)keyButton).style.borderTopWidth = new StyleFloat(1f);
			((VisualElement)keyButton).style.borderBottomWidth = new StyleFloat(1f);
			((VisualElement)keyButton).style.borderLeftWidth = new StyleFloat(1f);
			((VisualElement)keyButton).style.borderRightWidth = new StyleFloat(1f);
			((VisualElement)keyButton).style.borderTopColor = new StyleColor(Color.gray);
			((VisualElement)keyButton).style.borderBottomColor = new StyleColor(Color.gray);
			((VisualElement)keyButton).style.borderLeftColor = new StyleColor(Color.gray);
			((VisualElement)keyButton).style.borderRightColor = new StyleColor(Color.gray);
			_keyButtons[cellInstance] = keyButton;
			keyButton.clicked += delegate
			{
				StartListeningForKey(cellInstance, keyButton, hotkey, debugUIHandler);
			};
			TextField val4 = new TextField();
			((BaseField<string>)(object)val4).label = "Command:";
			((BaseField<string>)(object)val4).SetValueWithoutNotify(text ?? "");
			((VisualElement)val4).style.flexGrow = new StyleFloat(1f);
			((VisualElement)val4).style.marginRight = new StyleLength(10f);
			((VisualElement)val4).style.height = new StyleLength(35f);
			((VisualElement)val4).style.fontSize = new StyleLength(14f);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>((INotifyValueChanged<string>)(object)val4, (EventCallback<ChangeEvent<string>>)delegate(ChangeEvent<string> evt)
			{
				commandProperty.SetValue(hotkey, evt.newValue);
				SaveHotkeys(debugUIHandler);
			});
			Button val5 = new Button();
			((TextElement)val5).text = "✕";
			((VisualElement)val5).style.width = new StyleLength(30f);
			((VisualElement)val5).style.height = new StyleLength(35f);
			((VisualElement)val5).style.backgroundColor = new StyleColor(new Color(0.8f, 0.3f, 0.3f));
			((VisualElement)val5).style.color = new StyleColor(Color.white);
			((VisualElement)val5).style.borderTopWidth = new StyleFloat(1f);
			((VisualElement)val5).style.borderBottomWidth = new StyleFloat(1f);
			((VisualElement)val5).style.borderLeftWidth = new StyleFloat(1f);
			((VisualElement)val5).style.borderRightWidth = new StyleFloat(1f);
			((VisualElement)val5).style.borderTopColor = new StyleColor(Color.red);
			((VisualElement)val5).style.borderBottomColor = new StyleColor(Color.red);
			((VisualElement)val5).style.borderLeftColor = new StyleColor(Color.red);
			((VisualElement)val5).style.borderRightColor = new StyleColor(Color.red);
			val5.clicked += delegate
			{
				DeleteHotkey(cellInstance, hotkey, debugUIHandler);
			};
			val2.Add((VisualElement)(object)keyButton);
			val2.Add((VisualElement)(object)val4);
			val2.Add((VisualElement)(object)val5);
			val.Add(val2);
			Plugin.Log.LogDebug((object)$"Created improved UI for hotkey: {val3} -> {text}");
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in CreateImprovedUI: {arg}");
		}
	}

	private static void StartListeningForKey(object cellInstance, Button keyButton, object hotkey, object debugUIHandler)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			_listeningForKey[cellInstance] = true;
			((TextElement)keyButton).text = "WAITING FOR INPUT...";
			((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.4f, 0.7f, 0.3f));
			Plugin plugin = UnityEngine.Object.FindFirstObjectByType<Plugin>();
			if ((UnityEngine.Object)(object)plugin != (UnityEngine.Object)null)
			{
				((MonoBehaviour)plugin).StartCoroutine(WaitForKeyPress(cellInstance, keyButton, hotkey, debugUIHandler));
				return;
			}
			Plugin.Log.LogError((object)"Could not find Plugin instance for coroutine");
			_listeningForKey[cellInstance] = false;
			((TextElement)keyButton).text = "ERROR";
			((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.8f, 0.2f, 0.2f));
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in StartListeningForKey: {arg}");
			_listeningForKey[cellInstance] = false;
		}
	}

	private unsafe static IEnumerator WaitForKeyPress(object cellInstance, Button keyButton, object hotkey, object debugUIHandler)
	{
		float timeout = 10f;
		float timeElapsed = 0f;
		while (_listeningForKey.ContainsKey(cellInstance) && _listeningForKey[cellInstance] && timeElapsed < timeout)
		{
			timeElapsed += Time.unscaledDeltaTime;
			if (Input.GetKeyDown((KeyCode)27))
			{
				_listeningForKey[cellInstance] = false;
				Type type = hotkey.GetType();
				KeyCode val = (KeyCode)((type.GetProperty("KeyCode")?.GetValue(hotkey) is KeyCode val2) ? ((int)val2) : 0);
				((TextElement)keyButton).text = (((int)val == 0) ? "Click to set key" : ((object)val).ToString());
				((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0.6f));
				yield break;
			}
			foreach (KeyCode value in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(value) && (int)value != 0 && IsValidKey(value))
				{
					Type type2 = hotkey.GetType();
					type2.GetProperty("KeyCode")?.SetValue(hotkey, (object)value);
					((TextElement)keyButton).text = ((object)value/*cast due to constrained. prefix*/).ToString();
					((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0.6f));
					SaveHotkeys(debugUIHandler);
					_listeningForKey[cellInstance] = false;
					Plugin.Log.LogInfo((object)$"Hotkey set to: {(object)value}");
					yield break;
				}
			}
			yield return null;
		}
		if (_listeningForKey.ContainsKey(cellInstance) && _listeningForKey[cellInstance])
		{
			_listeningForKey[cellInstance] = false;
			Type type3 = hotkey.GetType();
			KeyCode val4 = (KeyCode)((type3.GetProperty("KeyCode")?.GetValue(hotkey) is KeyCode val5) ? ((int)val5) : 0);
			((TextElement)keyButton).text = (((int)val4 == 0) ? "Click to set key" : ((object)val4).ToString());
			((VisualElement)keyButton).style.backgroundColor = new StyleColor(new Color(0.3f, 0.4f, 0.6f));
			Plugin.Log.LogInfo((object)"Hotkey input timed out");
		}
	}

	private static bool IsValidKey(KeyCode keyCode)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		if ((int)keyCode == 0 || (int)keyCode == 27)
		{
			return false;
		}
		return true;
	}

	private static void DeleteHotkey(object cellInstance, object hotkey, object debugUIHandler)
	{
		try
		{
			Plugin.Log.LogInfo((object)"Attempting to delete hotkey...");
			Type type = debugUIHandler.GetType();
			if (type.GetField("Hotkeys", BindingFlags.Instance | BindingFlags.Public)?.GetValue(debugUIHandler) is IList list)
			{
				Plugin.Log.LogInfo((object)$"Found hotkeys list with {list.Count} items");
				bool flag = false;
				try
				{
					flag = list.Contains(hotkey);
					if (flag)
					{
						list.Remove(hotkey);
					}
				}
				catch (Exception arg)
				{
					Plugin.Log.LogError((object)$"Error removing hotkey: {arg}");
				}
				Plugin.Log.LogInfo((object)$"Hotkey removal result: {flag}");
				if (flag)
				{
					SaveHotkeys(debugUIHandler);
					RedrawHotkeysList(debugUIHandler);
					Plugin.Log.LogInfo((object)"Hotkey deleted successfully");
				}
				else
				{
					Plugin.Log.LogWarning((object)"Failed to remove hotkey from list");
				}
			}
			else
			{
				Plugin.Log.LogError((object)"Could not find hotkeys list");
			}
		}
		catch (Exception arg2)
		{
			Plugin.Log.LogError((object)$"Error in DeleteHotkey: {arg2}");
		}
	}

	private static void SaveHotkeys(object debugUIHandler)
	{
		try
		{
			Type type = debugUIHandler.GetType();
			type.GetMethod("SaveHotkeys")?.Invoke(debugUIHandler, null);
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in SaveHotkeys: {arg}");
		}
	}

	private static void RedrawHotkeysList(object debugUIHandler)
	{
		try
		{
			Plugin.Log.LogInfo((object)"Attempting to redraw hotkeys list...");
			Type type = debugUIHandler.GetType();
			object obj = type.GetField("m_currentPage", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(debugUIHandler);
			if (obj != null)
			{
				Type type2 = obj.GetType();
				Plugin.Log.LogInfo((object)("Current page type: " + type2.Name));
				if (type2.Name == "HotkeysPage")
				{
					MethodInfo method = type2.GetMethod("RedrawHotkeysList", BindingFlags.Instance | BindingFlags.NonPublic);
					if (method != null)
					{
						method.Invoke(obj, null);
						Plugin.Log.LogInfo((object)"Successfully called RedrawHotkeysList");
						return;
					}
					object? obj2 = type2.GetField("m_hotkeys", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
					VisualElement val = (VisualElement)((obj2 is VisualElement) ? obj2 : null);
					if (val != null)
					{
						Plugin.Log.LogInfo((object)"Found m_hotkeys field, clearing and rebuilding...");
						val.Clear();
						if (!(type.GetField("Hotkeys", BindingFlags.Instance | BindingFlags.Public)?.GetValue(debugUIHandler) is IList list))
						{
							return;
						}
						foreach (object item in list)
						{
							Type type3 = FindConsoleHotkeyCellType();
							if (type3 != null)
							{
								object obj3 = Activator.CreateInstance(type3, item, debugUIHandler);
								VisualElement val2 = (VisualElement)((obj3 is VisualElement) ? obj3 : null);
								if (val2 != null)
								{
									val.Add(val2);
								}
							}
						}
						Plugin.Log.LogInfo((object)"Successfully rebuilt hotkeys list");
					}
					else
					{
						Plugin.Log.LogWarning((object)"Could not find RedrawHotkeysList method or m_hotkeys field");
					}
				}
				else
				{
					Plugin.Log.LogWarning((object)("Current page is not HotkeysPage: " + type2.Name));
				}
			}
			else
			{
				Plugin.Log.LogWarning((object)"Could not find current page");
			}
		}
		catch (Exception arg)
		{
			Plugin.Log.LogError((object)$"Error in RedrawHotkeysList: {arg}");
		}
	}
}
