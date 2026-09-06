using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;
using Zorro.Core.CLI;

using PhotonPlayer = Photon.Realtime.Player;
namespace AdvancedConsole;

public class AdvancedConsolePage : DebugPage
{
	public class ConsoleCommandInfo
	{
		public string MethodName { get; set; } = "";

		public string FullName { get; set; } = "";

		public string Description { get; set; } = "";

		public ParameterInfo[] Parameters { get; set; } = Array.Empty<ParameterInfo>();

		public MethodInfo Method { get; set; }

		public Type DeclaringType { get; set; }

		public bool RequiresMasterClient { get; set; }

		public bool IsRPCCommand { get; set; }
	}

	private Dictionary<string, List<ConsoleCommandInfo>> _commandGroups = new Dictionary<string, List<ConsoleCommandInfo>>();

	private HashSet<string> _favorites = new HashSet<string>();

	private VisualElement _commandsContainer;

	private DropdownField _groupFilter;

	private TextField _searchField;

	private Button _refreshButton;

	public AdvancedConsolePage()
	{
		((VisualElement)this).name = "AdvancedConsole";
		LoadFavorites();
		ScanForCommands();
		SetupPage();
	}

	private void SetupPage()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Expected O, but got Unknown
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected O, but got Unknown
		Label val = new Label("高级控制台命令");
		((VisualElement)val).style.fontSize = new StyleLength(20f);
		((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val).style.color = new StyleColor(new Color(0.2f, 0.9f, 0.2f));
		((VisualElement)val).style.marginBottom = new StyleLength(8f);
		((VisualElement)val).style.marginTop = new StyleLength(3f);
		Label val2 = val;
		((VisualElement)this).Add((VisualElement)(object)val2);
		VisualElement val3 = new VisualElement();
		val3.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val3.style.marginBottom = new StyleLength(8f);
		val3.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.8f));
		val3.style.paddingTop = new StyleLength(4f);
		val3.style.paddingBottom = new StyleLength(4f);
		val3.style.paddingLeft = new StyleLength(6f);
		val3.style.paddingRight = new StyleLength(6f);
		val3.style.borderTopLeftRadius = new StyleLength(4f);
		val3.style.borderTopRightRadius = new StyleLength(4f);
		val3.style.borderBottomLeftRadius = new StyleLength(4f);
		val3.style.borderBottomRightRadius = new StyleLength(4f);
		VisualElement val4 = val3;
		TextField val5 = new TextField("搜索：");
		((VisualElement)val5).style.flexGrow = new StyleFloat(1f);
		((VisualElement)val5).style.marginRight = new StyleLength(8f);
		((VisualElement)val5).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		((VisualElement)val5).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		_searchField = val5;
		((CallbackEventHandler)_searchField).RegisterCallback<AttachToPanelEvent>((EventCallback<AttachToPanelEvent>)delegate
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			VisualElement val12 = UQueryExtensions.Q<VisualElement>((VisualElement)(object)_searchField, (string)null, "unity-text-field__input");
			if (val12 != null)
			{
				val12.style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
				val12.style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
			}
		}, (TrickleDown)0);
		INotifyValueChangedExtensions.RegisterValueChangedCallback<string>((INotifyValueChanged<string>)(object)_searchField, (EventCallback<ChangeEvent<string>>)OnSearchChanged);
		val4.Add((VisualElement)(object)_searchField);
		List<string> list = new List<string> { "所有分组", "★ 收藏" };
		list.AddRange(_commandGroups.Keys);
		DropdownField val6 = new DropdownField("分类：", list, 0, (Func<string, string>)null, (Func<string, string>)null);
		((VisualElement)val6).style.width = new StyleLength(350f);
		((VisualElement)val6).style.marginRight = new StyleLength(8f);
		((VisualElement)val6).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		((VisualElement)val6).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		_groupFilter = val6;
		INotifyValueChangedExtensions.RegisterValueChangedCallback<string>((INotifyValueChanged<string>)(object)_groupFilter, (EventCallback<ChangeEvent<string>>)OnGroupFilterChanged);
		val4.Add((VisualElement)(object)_groupFilter);
		Button val7 = new Button((Action)RefreshCommands)
		{
			text = "刷新"
		};
		((VisualElement)val7).style.width = new StyleLength(90f);
		((VisualElement)val7).style.backgroundColor = new StyleColor(new Color(0.2f, 0.5f, 0.8f));
		((VisualElement)val7).style.color = new StyleColor(Color.white);
		_refreshButton = val7;
		val4.Add((VisualElement)(object)_refreshButton);
		((VisualElement)this).Add(val4);
		ScrollView val8 = new ScrollView();
		((VisualElement)val8).style.flexGrow = new StyleFloat(1f);
		ScrollView val9 = val8;
		_commandsContainer = new VisualElement();
		((VisualElement)val9).Add(_commandsContainer);
		((VisualElement)this).Add((VisualElement)(object)val9);
		Label val10 = new Label($"共找到 {_commandGroups.Sum<KeyValuePair<string, List<ConsoleCommandInfo>>>((KeyValuePair<string, List<ConsoleCommandInfo>> g) => g.Value.Count)} 个命令，分布在 {_commandGroups.Count} 个分组中");
		((VisualElement)val10).style.fontSize = new StyleLength(11f);
		((VisualElement)val10).style.color = new StyleColor(new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)val10).style.marginTop = new StyleLength(4f);
		Label val11 = val10;
		((VisualElement)this).Add((VisualElement)(object)val11);
		RefreshCommandsDisplay();
	}

	private void ScanForCommands()
	{
		_commandGroups.Clear();
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
					MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
					MethodInfo[] array3 = methods;
					foreach (MethodInfo methodInfo in array3)
					{
						ConsoleCommandAttribute customAttribute = ((MemberInfo)methodInfo).GetCustomAttribute<ConsoleCommandAttribute>();
						if (customAttribute != null)
						{
							AddCommandToGroups(methodInfo, type);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Plugin.Log.LogDebug((object)("扫描程序集出错 " + assembly.FullName + "：" + ex.Message));
			}
		}
		try
		{
			Type type2 = assemblies.SelectMany(delegate(Assembly a)
			{
				try
				{
					return a.GetTypes();
				}
				catch
				{
					return Array.Empty<Type>();
				}
			}).FirstOrDefault((Type t) => t.Name == "ConsoleHandler");
			if (type2 != null)
			{
				List<FieldInfo> list = (from f in type2.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
					where f.FieldType.IsGenericType && (f.FieldType.GetGenericTypeDefinition() == typeof(Dictionary<, >) || f.Name.ToLower().Contains("command"))
					select f).ToList();
				foreach (FieldInfo item in list)
				{
					try
					{
						object value = item.GetValue(null);
						if (value != null)
						{
							ScanRegisteredCommands(value);
						}
					}
					catch (Exception ex2)
					{
						Plugin.Log.LogDebug((object)("访问命令字段出错 " + item.Name + "：" + ex2.Message));
					}
				}
			}
		}
		catch (Exception ex3)
		{
			Plugin.Log.LogDebug((object)("扫描已注册命令出错：" + ex3.Message));
		}
		Plugin.Log.LogInfo((object)$"高级控制台：共找到 {_commandGroups.Sum<KeyValuePair<string, List<ConsoleCommandInfo>>>((KeyValuePair<string, List<ConsoleCommandInfo>> g) => g.Value.Count)} 个命令，分布在 {_commandGroups.Count} 个分组中");
	}

	private void AddCommandToGroups(MethodInfo method, Type type)
	{
		ConsoleCommandInfo item = new ConsoleCommandInfo
		{
			MethodName = method.Name,
			FullName = type.Name + "." + method.Name,
			Description = GetCommandDescription(method),
			Parameters = method.GetParameters(),
			Method = method,
			DeclaringType = type,
			RequiresMasterClient = DoesRequireMasterClient(method),
			IsRPCCommand = IsRPCCommand(method)
		};
		string groupName = GetGroupName(type.Name);
		if (!_commandGroups.ContainsKey(groupName))
		{
			_commandGroups[groupName] = new List<ConsoleCommandInfo>();
		}
		_commandGroups[groupName].Add(item);
	}

	private void ScanRegisteredCommands(object commandDict)
	{
		try
		{
			Type type = commandDict.GetType();
			if (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Dictionary<, >)))
			{
				return;
			}
			Type type2 = type.GetGenericArguments()[0];
			Type type3 = type.GetGenericArguments()[1];
			if (!(type2 == typeof(string)))
			{
				return;
			}
			object obj = type.GetProperty("Keys")?.GetValue(commandDict);
			if (!(obj is IEnumerable<string> enumerable))
			{
				return;
			}
			foreach (string commandName in enumerable)
			{
				if (!commandName.Contains("."))
				{
					continue;
				}
				string[] array = commandName.Split('.');
				if (array.Length >= 2)
				{
					string typeName = array[0];
					string methodName = array[1];
					ConsoleCommandInfo item = new ConsoleCommandInfo
					{
						MethodName = methodName,
						FullName = commandName,
						Description = GetCommandDescription(methodName),
						Parameters = Array.Empty<ParameterInfo>(),
						Method = null,
						DeclaringType = null,
						RequiresMasterClient = false,
						IsRPCCommand = false
					};
					string groupName = GetGroupName(typeName);
					if (!_commandGroups.ContainsKey(groupName))
					{
						_commandGroups[groupName] = new List<ConsoleCommandInfo>();
					}
					ConsoleCommandInfo consoleCommandInfo = _commandGroups[groupName].FirstOrDefault((ConsoleCommandInfo c) => c.FullName == commandName);
					if (consoleCommandInfo == null)
					{
						_commandGroups[groupName].Add(item);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("扫描已注册命令出错：" + ex.Message));
		}
	}

	private string GetCommandDescription(string methodName)
	{
		string text = Regex.Replace(methodName, "([a-z])([A-Z])", "$1 $2").ToLower();
		return char.ToUpper(text[0]) + text.Substring(1);
	}

	private string GetGroupName(string typeName)
	{
		if (typeName.Contains("Teleport"))
		{
			return "传送";
		}
		if (typeName.Contains("Fun"))
		{
			return "趣味命令";
		}
		if (typeName.Contains("Environment"))
		{
			return "环境";
		}
		if (typeName.Contains("Spawn"))
		{
			return "生成与物品";
		}
		if (typeName.Contains("RPC"))
		{
			return "RPC 命令";
		}
		if (typeName.Contains("Status"))
		{
			return "状态效果";
		}
		if (typeName.Contains("Map"))
		{
			return "地图与分段";
		}
		if (typeName.Contains("Info"))
		{
			return "信息";
		}
		if (typeName.Contains("Log"))
		{
			return "控制台与日志";
		}
		if (typeName.Contains("Sync"))
		{
			return "同步命令";
		}
		if (typeName.Contains("Weather"))
		{
			return "天气";
		}
		if (typeName.Contains("Character"))
		{
			return "角色";
		}
		if (typeName.Contains("Item"))
		{
			return "物品";
		}
		if (typeName.Contains("Console"))
		{
			return "控制台与日志";
		}
		if (typeName.Contains("Application"))
		{
			return "系统";
		}
		if (typeName.Contains("Achievement"))
		{
			return "成就";
		}
		if (typeName.Contains("Passport"))
		{
			return "自定义";
		}
		if (typeName.Contains("Ascent"))
		{
			return "登顶";
		}
		if (typeName.Contains("Backpack"))
		{
			return "背包";
		}
		return "其他命令";
	}

	private string GetCommandDescription(MethodInfo method)
	{
		string name = method.Name;
		string text = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2").ToLower();
		return char.ToUpper(text[0]) + text.Substring(1);
	}

	private bool DoesRequireMasterClient(MethodInfo method)
	{
		return method.GetCustomAttribute<MasterClientOnlyAttribute>() != null;
	}

	private bool IsRPCCommand(MethodInfo method)
	{
		Type? declaringType = method.DeclaringType;
		if (((object)declaringType == null || !declaringType.Name.Contains("RPC")) && !method.Name.Contains("RPC"))
		{
			return DoesRequireMasterClient(method);
		}
		return true;
	}

	private void OnSearchChanged(ChangeEvent<string> evt)
	{
		RefreshCommandsDisplay();
	}

	private void OnGroupFilterChanged(ChangeEvent<string> evt)
	{
		RefreshCommandsDisplay();
	}

	private void RefreshCommands()
	{
		ScanForCommands();
		List<string> list = new List<string> { "所有分组", "★ 收藏" };
		list.AddRange(_commandGroups.Keys);
		((BasePopupField<string, string>)(object)_groupFilter).choices = list;
		RefreshCommandsDisplay();
	}

	private void RefreshCommandsDisplay()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		_commandsContainer.Clear();
		string searchText = ((BaseField<string>)(object)_searchField).value?.ToLower() ?? "";
		string value = ((BaseField<string>)(object)_groupFilter).value;
		foreach (KeyValuePair<string, List<ConsoleCommandInfo>> commandGroup in _commandGroups)
		{
			if (value != "所有分组" && value != "★ 收藏" && commandGroup.Key != value)
			{
				continue;
			}
			List<ConsoleCommandInfo> list = commandGroup.Value.Where((ConsoleCommandInfo cmd) => (value != "★ 收藏" || IsFavorite(cmd.FullName)) && (string.IsNullOrEmpty(searchText) || cmd.FullName.ToLower().Contains(searchText) || cmd.Description.ToLower().Contains(searchText))).ToList();
			if (list.Count == 0)
			{
				continue;
			}
			Label val = new Label($"{commandGroup.Key} ({list.Count})");
			((VisualElement)val).style.fontSize = new StyleLength(16f);
			((VisualElement)val).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
			((VisualElement)val).style.color = new StyleColor(new Color(0.2f, 0.8f, 1f));
			((VisualElement)val).style.marginTop = new StyleLength(8f);
			((VisualElement)val).style.marginBottom = new StyleLength(4f);
			((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.05f, 0.05f, 0.05f, 0.7f));
			((VisualElement)val).style.paddingLeft = new StyleLength(6f);
			((VisualElement)val).style.paddingTop = new StyleLength(2f);
			((VisualElement)val).style.paddingBottom = new StyleLength(2f);
			((VisualElement)val).style.borderTopLeftRadius = new StyleLength(3f);
			((VisualElement)val).style.borderTopRightRadius = new StyleLength(3f);
			((VisualElement)val).style.borderBottomLeftRadius = new StyleLength(3f);
			((VisualElement)val).style.borderBottomRightRadius = new StyleLength(3f);
			Label val2 = val;
			_commandsContainer.Add((VisualElement)(object)val2);
			foreach (ConsoleCommandInfo item in list.OrderBy((ConsoleCommandInfo c) => c.MethodName))
			{
				CreateCommandElement(item);
			}
		}
	}

	private void CreateCommandElement(ConsoleCommandInfo command)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Expected O, but got Unknown
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Expected O, but got Unknown
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Expected O, but got Unknown
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Expected O, but got Unknown
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		VisualElement val = new VisualElement();
		val.style.backgroundColor = new StyleColor(new Color(0.08f, 0.08f, 0.08f, 0.6f));
		val.style.marginBottom = new StyleLength(2f);
		val.style.paddingTop = new StyleLength(3f);
		val.style.paddingBottom = new StyleLength(3f);
		val.style.paddingLeft = new StyleLength(6f);
		val.style.paddingRight = new StyleLength(6f);
		val.style.borderTopLeftRadius = new StyleLength(3f);
		val.style.borderTopRightRadius = new StyleLength(3f);
		val.style.borderBottomLeftRadius = new StyleLength(3f);
		val.style.borderBottomRightRadius = new StyleLength(3f);
		val.style.borderLeftWidth = new StyleFloat(2f);
		val.style.borderLeftColor = new StyleColor(new Color(0.2f, 0.6f, 0.9f, 0.8f));
		VisualElement val2 = val;
		VisualElement val3 = new VisualElement();
		val3.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val3.style.alignItems = new StyleEnum<Align>((Align)2);
		VisualElement val4 = val3;
		VisualElement val5 = new VisualElement();
		val5.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val5.style.alignItems = new StyleEnum<Align>((Align)2);
		val5.style.flexGrow = new StyleFloat(1f);
		val5.style.minWidth = new StyleLength(200f);
		VisualElement val6 = val5;
		Label val7 = new Label(command.FullName);
		((VisualElement)val7).style.fontSize = new StyleLength(12f);
		((VisualElement)val7).style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)1);
		((VisualElement)val7).style.color = new StyleColor(new Color(0.9f, 0.9f, 0.9f));
		((VisualElement)val7).style.marginRight = new StyleLength(6f);
		((VisualElement)val7).style.flexShrink = new StyleFloat(0f);
		((VisualElement)val7).style.whiteSpace = new StyleEnum<WhiteSpace>((WhiteSpace)1);
		Label val8 = val7;
		val6.Add((VisualElement)(object)val8);
		if (command.IsRPCCommand)
		{
			Label val9 = new Label("RPC");
			((VisualElement)val9).style.fontSize = new StyleLength(8f);
			((VisualElement)val9).style.color = new StyleColor(new Color(1f, 0.9f, 0.3f));
			((VisualElement)val9).style.backgroundColor = new StyleColor(new Color(0.4f, 0.3f, 0.1f, 0.8f));
			((VisualElement)val9).style.paddingLeft = new StyleLength(3f);
			((VisualElement)val9).style.paddingRight = new StyleLength(3f);
			((VisualElement)val9).style.paddingTop = new StyleLength(1f);
			((VisualElement)val9).style.paddingBottom = new StyleLength(1f);
			((VisualElement)val9).style.marginRight = new StyleLength(3f);
			((VisualElement)val9).style.borderTopLeftRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderTopRightRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderBottomLeftRadius = new StyleLength(2f);
			((VisualElement)val9).style.borderBottomRightRadius = new StyleLength(2f);
			Label val10 = val9;
			val6.Add((VisualElement)(object)val10);
		}
		if (command.RequiresMasterClient)
		{
			Label val11 = new Label("房主");
			((VisualElement)val11).style.fontSize = new StyleLength(8f);
			((VisualElement)val11).style.color = new StyleColor(PhotonNetwork.IsMasterClient ? new Color(0.7f, 1f, 0.7f) : new Color(1f, 0.7f, 0.7f));
			((VisualElement)val11).style.backgroundColor = new StyleColor(PhotonNetwork.IsMasterClient ? new Color(0.1f, 0.4f, 0.1f, 0.8f) : new Color(0.4f, 0.1f, 0.1f, 0.8f));
			((VisualElement)val11).style.paddingLeft = new StyleLength(3f);
			((VisualElement)val11).style.paddingRight = new StyleLength(3f);
			((VisualElement)val11).style.paddingTop = new StyleLength(1f);
			((VisualElement)val11).style.paddingBottom = new StyleLength(1f);
			((VisualElement)val11).style.marginRight = new StyleLength(3f);
			((VisualElement)val11).style.borderTopLeftRadius = new StyleLength(2f);
			((VisualElement)val11).style.borderTopRightRadius = new StyleLength(2f);
			((VisualElement)val11).style.borderBottomLeftRadius = new StyleLength(2f);
			((VisualElement)val11).style.borderBottomRightRadius = new StyleLength(2f);
			Label val12 = val11;
			val6.Add((VisualElement)(object)val12);
		}
		val4.Add(val6);
		Label val13 = new Label(command.Description);
		((VisualElement)val13).style.fontSize = new StyleLength(10f);
		((VisualElement)val13).style.color = new StyleColor(new Color(0.75f, 0.75f, 0.75f));
		((VisualElement)val13).style.width = new StyleLength(200f);
		((VisualElement)val13).style.minWidth = new StyleLength(200f);
		((VisualElement)val13).style.marginRight = new StyleLength(8f);
		Label val14 = val13;
		val4.Add((VisualElement)(object)val14);
		Button favButton = new Button((Action)delegate
		{
			ToggleFavorite(command.FullName);
		});
		favButton.text = (IsFavorite(command.FullName) ? "★" : "☆");
		((VisualElement)favButton).style.width = new StyleLength(26f);
		((VisualElement)favButton).style.height = new StyleLength(20f);
		((VisualElement)favButton).style.fontSize = new StyleLength(13f);
		((VisualElement)favButton).style.backgroundColor = new StyleColor(IsFavorite(command.FullName) ? new Color(0.5f, 0.4f, 0.08f) : new Color(0.16f, 0.16f, 0.16f));
		((VisualElement)favButton).style.color = new StyleColor(IsFavorite(command.FullName) ? new Color(1f, 0.9f, 0.3f) : new Color(0.8f, 0.8f, 0.8f));
		((VisualElement)favButton).style.marginRight = new StyleLength(4f);
		favButton.tooltip = "点击收藏/取消收藏该命令";
		val4.Add(favButton);
		Button copyButton = new Button((Action)delegate
		{
			CopyToClipboard(command.FullName);
		});
		copyButton.text = "复制";
		((VisualElement)copyButton).style.width = new StyleLength(34f);
		((VisualElement)copyButton).style.height = new StyleLength(20f);
		((VisualElement)copyButton).style.fontSize = new StyleLength(9f);
		((VisualElement)copyButton).style.backgroundColor = new StyleColor(new Color(0.16f, 0.16f, 0.22f));
		((VisualElement)copyButton).style.color = new StyleColor(new Color(0.85f, 0.85f, 0.95f));
		((VisualElement)copyButton).style.marginRight = new StyleLength(4f);
		copyButton.tooltip = "复制命令到剪贴板";
		val4.Add(copyButton);
		VisualElement val15 = new VisualElement();
		val15.style.flexDirection = new StyleEnum<FlexDirection>((FlexDirection)2);
		val15.style.alignItems = new StyleEnum<Align>((Align)2);
		val15.style.flexGrow = new StyleFloat(1f);
		VisualElement val16 = val15;
		List<VisualElement> inputFields = new List<VisualElement>();
		ParameterInfo[] parameters = command.Parameters;
		foreach (ParameterInfo parameterInfo in parameters)
		{
			VisualElement val18;
			if (parameterInfo.ParameterType == typeof(PhotonPlayer))
			{
				DropdownField val17 = CreatePlayerDropdown();
				((VisualElement)val17).style.width = new StyleLength(150f);
				((VisualElement)val17).style.marginRight = new StyleLength(4f);
				val18 = (VisualElement)(object)val17;
			}
			else if (parameterInfo.ParameterType == typeof(ConsoleItem))
			{
				DropdownField val19 = CreateItemDropdown();
				((VisualElement)val19).style.width = new StyleLength(150f);
				((VisualElement)val19).style.marginRight = new StyleLength(4f);
				val18 = (VisualElement)(object)val19;
			}
			else if (parameterInfo.ParameterType.IsEnum)
			{
				DropdownField val20 = CreateEnumDropdown(parameterInfo.ParameterType);
				((VisualElement)val20).style.width = new StyleLength(100f);
				((VisualElement)val20).style.marginRight = new StyleLength(4f);
				val18 = (VisualElement)(object)val20;
			}
			else
			{
				TextField val21 = new TextField();
				((VisualElement)val21).style.fontSize = new StyleLength(9f);
				((VisualElement)val21).style.width = new StyleLength(80f);
				((VisualElement)val21).style.marginRight = new StyleLength(4f);
				((VisualElement)val21).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
				((VisualElement)val21).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
				TextField val22 = val21;
				VisualElement val23 = UQueryExtensions.Q<VisualElement>((VisualElement)(object)val22, (string)null, "unity-text-field__input");
				if (val23 != null)
				{
					val23.style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
					val23.style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
				}
				((BaseField<string>)(object)val22).SetValueWithoutNotify(GetDefaultParameterValue(parameterInfo));
				if (string.IsNullOrEmpty(((BaseField<string>)(object)val22).value))
				{
					((BaseField<string>)(object)val22).value = parameterInfo.Name ?? "";
				}
				val18 = (VisualElement)(object)val22;
			}
			inputFields.Add(val18);
			val16.Add(val18);
		}
		Button val24 = new Button((Action)delegate
		{
			ExecuteCommand(command, inputFields);
		})
		{
			text = "▶ 执行"
		};
		((VisualElement)val24).style.width = new StyleLength(85f);
		((VisualElement)val24).style.backgroundColor = new StyleColor(new Color(0.15f, 0.7f, 0.15f));
		((VisualElement)val24).style.color = new StyleColor(Color.white);
		((VisualElement)val24).style.fontSize = new StyleLength(10f);
		((VisualElement)val24).style.marginLeft = new StyleLength(4f);
		Button val25 = val24;
		val16.Add((VisualElement)(object)val25);
		val4.Add(val16);
		val2.Add(val4);
		val2.AddManipulator(new ContextualMenuManipulator(delegate(ContextualMenuPopulateEvent evt)
		{
			evt.menu.AppendAction("复制命令", delegate
			{
				CopyToClipboard(command.FullName);
			});
			evt.menu.AppendAction("复制为执行命令", delegate
			{
				CopyToClipboard(command.FullName + " ");
			});
		}));
		_commandsContainer.Add(val2);
	}

	private static void CopyToClipboard(string text)
	{
		try
		{
			GUIUtility.systemCopyBuffer = text;
			Debug.Log((object)("已复制到剪贴板: " + text));
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("复制到剪贴板失败: " + ex.Message));
		}
	}

	private DropdownField CreatePlayerDropdown()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		List<string> list = new List<string> { "无玩家" };
		if (PhotonNetwork.IsConnected && PhotonNetwork.PlayerList.Length != 0)
		{
			list = PhotonNetwork.PlayerList.Select((PhotonPlayer p) => p.NickName).ToList();
		}
		DropdownField val = new DropdownField(list, 0, (Func<string, string>)null, (Func<string, string>)null);
		((VisualElement)val).style.fontSize = new StyleLength(9f);
		((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		((VisualElement)val).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		return val;
	}

	private DropdownField CreateEnumDropdown(Type enumType)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		List<string> list = Enum.GetNames(enumType).ToList();
		DropdownField val = new DropdownField(list, 0, (Func<string, string>)null, (Func<string, string>)null);
		((VisualElement)val).style.fontSize = new StyleLength(9f);
		((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		((VisualElement)val).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		return val;
	}

	private DropdownField CreateItemDropdown()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		List<ConsoleItem> allItems = ConsoleItem.GetAllItems();
		List<string> list = new List<string>();
		if (allItems.Count > 0)
		{
			list = allItems.Select((ConsoleItem item) => item.Name).ToList();
		}
		else
		{
			list.Add("没有可用物品");
		}
		DropdownField val = new DropdownField(list, 0, (Func<string, string>)null, (Func<string, string>)null);
		((VisualElement)val).style.fontSize = new StyleLength(9f);
		((VisualElement)val).style.backgroundColor = new StyleColor(new Color(0.1f, 0.1f, 0.1f));
		((VisualElement)val).style.color = new StyleColor(new Color(0.95f, 0.95f, 0.95f));
		return val;
	}

	private string GetDefaultParameterValue(ParameterInfo param)
	{
		if (param.HasDefaultValue && param.DefaultValue != null)
		{
			return param.DefaultValue.ToString() ?? "";
		}
		return param.ParameterType.Name switch
		{
			"Single" => "1.0", 
			"Double" => "1.0", 
			"Int32" => "1", 
			"String" => "", 
			"Boolean" => "true", 
			"UInt16" => "1", 
			_ => "", 
		};
	}

	private void ExecuteCommand(ConsoleCommandInfo command, List<VisualElement> inputFields)
	{
		try
		{
			if (command.Method != null)
			{
				object[] array = new object[command.Parameters.Length];
				for (int i = 0; i < command.Parameters.Length; i++)
				{
					ParameterInfo parameterInfo = command.Parameters[i];
					VisualElement val = inputFields[i];
					DropdownField val2 = (DropdownField)(object)((val is DropdownField) ? val : null);
					if (val2 != null)
					{
						if (parameterInfo.ParameterType == typeof(PhotonPlayer))
						{
							string selectedPlayerName = ((BaseField<string>)(object)val2).value;
							PhotonPlayer val3 = PhotonNetwork.PlayerList.FirstOrDefault((PhotonPlayer p) => p.NickName == selectedPlayerName);
							array[i] = val3;
						}
						else if (parameterInfo.ParameterType == typeof(ConsoleItem))
						{
							string selectedItemName = ((BaseField<string>)(object)val2).value;
							List<ConsoleItem> allItems = ConsoleItem.GetAllItems();
							ConsoleItem consoleItem = allItems.FirstOrDefault((ConsoleItem item) => item.Name == selectedItemName);
							array[i] = consoleItem ?? allItems.FirstOrDefault();
						}
						else if (parameterInfo.ParameterType.IsEnum)
						{
							string value = ((BaseField<string>)(object)val2).value;
							array[i] = Enum.Parse(parameterInfo.ParameterType, value);
						}
						continue;
					}
					TextField val4 = (TextField)(object)((val is TextField) ? val : null);
					if (val4 != null)
					{
						string value2 = ((BaseField<string>)(object)val4).value;
						if (string.IsNullOrEmpty(value2) && parameterInfo.HasDefaultValue)
						{
							array[i] = parameterInfo.DefaultValue;
						}
						else
						{
							array[i] = ConvertParameter(value2, parameterInfo.ParameterType) ?? parameterInfo.DefaultValue;
						}
					}
				}
				command.Method.Invoke(null, array);
			}
			else
			{
				ExecuteCommandViaConsoleHandler(command, inputFields);
			}
			Plugin.Log.LogInfo((object)("高级控制台界面：已执行命令 " + command.FullName));
			Debug.Log((object)("高级控制台界面：已执行 " + command.FullName));
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("高级控制台界面：执行命令失败 " + command.FullName + "：" + ex.Message));
			Debug.LogError((object)("高级控制台界面：执行命令失败 " + command.FullName + "：" + ex.Message));
		}
	}

	private void ExecuteCommandViaConsoleHandler(ConsoleCommandInfo command, List<VisualElement> inputFields)
	{
		try
		{
			Type type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly a)
			{
				try
				{
					return a.GetTypes();
				}
				catch
				{
					return Array.Empty<Type>();
				}
			}).FirstOrDefault((Type t) => t.Name == "ConsoleHandler");
			if (type != null)
			{
				MethodInfo methodInfo = type.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((MethodInfo m) => m.Name.Contains("Execute") && m.GetParameters().Length != 0);
				if (methodInfo != null)
				{
					string text = command.FullName;
					if (inputFields.Count > 0)
					{
						List<string> list = new List<string>();
						foreach (VisualElement inputField in inputFields)
						{
							TextField val = (TextField)(object)((inputField is TextField) ? inputField : null);
							if (val != null && !string.IsNullOrEmpty(((BaseField<string>)(object)val).value))
							{
								list.Add(((BaseField<string>)(object)val).value);
								continue;
							}
							DropdownField val2 = (DropdownField)(object)((inputField is DropdownField) ? inputField : null);
							if (val2 != null && !string.IsNullOrEmpty(((BaseField<string>)(object)val2).value))
							{
								list.Add(((BaseField<string>)(object)val2).value);
							}
						}
						if (list.Count > 0)
						{
							text = text + " " + string.Join(" ", list);
						}
					}
					methodInfo.Invoke(null, new object[1] { text });
				}
				else
				{
					Plugin.Log.LogWarning((object)("在 ConsoleHandler 中找不到命令的执行方法 " + command.FullName));
				}
			}
			else
			{
				Plugin.Log.LogWarning((object)("找不到命令的 ConsoleHandler 类型 " + command.FullName));
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogError((object)("通过 ConsoleHandler 执行命令失败：" + ex.Message));
		}
	}

	private object? ConvertParameter(string input, Type targetType)
	{
		if (string.IsNullOrEmpty(input))
		{
			if (!targetType.IsValueType)
			{
				return null;
			}
			return Activator.CreateInstance(targetType);
		}
		if (targetType == typeof(string))
		{
			return input;
		}
		string text = NormalizeNumericInput(input);
		if (targetType == typeof(int))
		{
			if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
			{
				return result;
			}
			return 0;
		}
		if (targetType == typeof(float))
		{
			if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float result2))
			{
				return result2;
			}
			return 0f;
		}
		if (targetType == typeof(double))
		{
			if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double result3))
			{
				return result3;
			}
			return 0.0;
		}
		if (targetType == typeof(byte))
		{
			if (byte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result4))
			{
				return result4;
			}
			return (byte)0;
		}
		if (targetType == typeof(short))
		{
			if (short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out short result5))
			{
				return result5;
			}
			return (short)0;
		}
		if (targetType == typeof(long))
		{
			if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out long result6))
			{
				return result6;
			}
			return 0L;
		}
		if (targetType == typeof(uint))
		{
			if (uint.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint result7))
			{
				return result7;
			}
			return 0u;
		}
		if (targetType == typeof(ulong))
		{
			if (ulong.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong result8))
			{
				return result8;
			}
			return 0uL;
		}
		if (targetType == typeof(ushort))
		{
			if (ushort.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out ushort result9))
			{
				return result9;
			}
			return (ushort)0;
		}
		if (targetType == typeof(decimal))
		{
			if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result10))
			{
				return result10;
			}
			return 0m;
		}
		if (targetType == typeof(bool))
		{
			string text2 = input.Trim().ToLowerInvariant();
			if (text2 == "true" || text2 == "1" || text2 == "yes" || text2 == "是" || text2 == "开启")
			{
				return true;
			}
			if (text2 == "false" || text2 == "0" || text2 == "no" || text2 == "否" || text2 == "关闭")
			{
				return false;
			}
			return bool.TryParse(input, out bool result11) && result11;
		}
		if (targetType.IsEnum)
		{
			try
			{
				return Enum.Parse(targetType, input, ignoreCase: true);
			}
			catch
			{
				try
				{
					if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result12))
					{
						return Enum.ToObject(targetType, result12);
					}
				}
				catch
				{
				}
				return Activator.CreateInstance(targetType);
			}
		}
		try
		{
			return Convert.ChangeType(input, targetType);
		}
		catch
		{
			if (targetType.IsValueType)
			{
				return Activator.CreateInstance(targetType);
			}
			return null;
		}
	}

	private static string NormalizeNumericInput(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return "";
		}
		string text = input.Trim();
		char[] array = text.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			char c = array[i];
			if (c >= '\uff10' && c <= '\uff19')
			{
				array[i] = (char)(c - 65248);
			}
			else if (c == '\uff0e')
			{
				array[i] = '.';
			}
			else if (c == '\uff0c' || c == '\u3002' || c == ',')
			{
				array[i] = '.';
			}
			else if (c == '\uff0d' || c == '\u2212')
			{
				array[i] = '-';
			}
		}
		return new string(array);
	}

	private void LoadFavorites()
	{
		_favorites.Clear();
		try
		{
			string @string = PlayerPrefs.GetString("AdvancedConsole.Favorites", "");
			if (!string.IsNullOrEmpty(@string))
			{
				string[] array = @string.Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						_favorites.Add(text.Trim());
					}
				}
			}
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("加载收藏失败：" + ex.Message));
		}
	}

	private void SaveFavorites()
	{
		try
		{
			PlayerPrefs.SetString("AdvancedConsole.Favorites", string.Join("|", _favorites));
			PlayerPrefs.Save();
		}
		catch (Exception ex)
		{
			Plugin.Log.LogDebug((object)("保存收藏失败：" + ex.Message));
		}
	}

	private bool IsFavorite(string fullName)
	{
		return _favorites.Contains(fullName);
	}

	private void ToggleFavorite(string fullName)
	{
		if (_favorites.Contains(fullName))
		{
			_favorites.Remove(fullName);
		}
		else
		{
			_favorites.Add(fullName);
		}
		SaveFavorites();
		RefreshCommandsDisplay();
	}
}
